/*
	==============================================
	Criado por : Carlos R. S. Oliveira
	Criado em  : 20/05/2019
	==============================================
*/
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EtlConexao;

namespace ExecutaCarga
{
    public class EstruturaDbf
    {
        ProcessArqController processArqController = null;
        Conexao conexaoORG = null;
        public EstruturaDbf()
        {
            conexaoORG = new Conexao();
            processArqController = new ProcessArqController();
        }

        public void GetEstruturaDbf(string NomeFile,string NomePlanilha="")
        {
            try
            {
                string pTipoArquivo = "";
                string script = "SELECT * ";
                script += "FROM OPENROWSET( ";
                script += "'Microsoft.ACE.OLEDB.12.0', ";
                if (NomePlanilha.Length > 0)
                {
                    script += $"'Excel 12.0; Database=c:\\input\\FF MDTR\\{NomeFile}', ";
                    script += $"' SELECT * FROM [{NomePlanilha}$]' )";
                    pTipoArquivo = "Excel";
                }
                else
                {
                    script += "'dBase 5.0; Database=c:\\input\\FF MDTR\\', ";
                    script += $"' SELECT * FROM [{NomeFile}]' )";
                    pTipoArquivo = "DBF";
                }

                GeraScript("Begin");
                GeraScript(" Declare @IdArquivo int, @IdServico int;");
                GeraScript(" insert into tb_servicos(status) Values('ativo');");
                GeraScript(" select @IdServico=Max(id_Servico) from tb_servicos;");

                GeraScript(" insert into tb_arquivos(id_pacote, nm_arquivo, mascara_arquivo, tp_carga, tp_arquivo, delimitador, cercador, tb_destino, dir_entrada, dir_saida, rbd_tabela, rbd_indice, status)");
                GeraScript($"    values(1, '{NomeFile.Substring(0, NomeFile.IndexOf('.'))}', '{NomeFile}', 'FULL', '{pTipoArquivo}', null, null, '{NomeFile.Substring(0, NomeFile.IndexOf('.'))}', 'C:\\input\\', 'C:\\output\\', 'NÃO', 'NÃO', 'ativo');");

                GeraScript(" select @IdArquivo=Max(id_Arquivo) from tb_arquivos;");
                GeraScript(" insert into tb_monitoramentos(id_servico, id_arquivo, status) values(@IdServico, @IdArquivo, 'ativo');");

                string InsertInto = $"   insert into tb_mapeamentos(id_arquivo, ordem, fixo_inicio, fixo_tamanho, nm_coluna, tp_coluna,tm_coluna,pr_coluna) values ";

                //processArqController.Configura_Ad_Hoc_Distributed_Queries(Global.conexaoORG, 1);

                conexaoORG.AbrirConexao(conexaoORG.Conn);
                SqlCommand oCmd = new SqlCommand(script, conexaoORG.Conn);
                DataTable dt = new DataTable();
                dt.Load(oCmd.ExecuteReader());
                for (int i = 0; i <= dt.Columns.Count - 1; i++)
                {
                    bool isNumber = false;
                    string Num = dt.Columns[i].ColumnName.Substring(1, 1);
                    try
                    {
                        int num = Convert.ToInt32(Num);
                        isNumber = true;
                    }
                    catch
                    {
                        isNumber = false;
                    }

                    if (NomePlanilha.Length > 0 && dt.Columns[i].ColumnName.Substring(0, 1) == "F" && (isNumber))
                    {
                        break;
                    }
                    else
                    {
                        int width = 0;
                        if (dt.Columns[i].DataType.Name == "String")
                        {
                            foreach (DataRow dtr in dt.Rows)
                            {
                                int len = dtr[dt.Columns[i].ColumnName].ToString().Length;
                                if (len > width)
                                {
                                    width = len;
                                }
                            }
                        }
                        if (width == 0)
                        {
                            //Console.WriteLine($"{dt.Columns[i].ColumnName} {dt.Columns[i].DataType.Name},");
                            GeraScript($"{InsertInto}(@IdArquivo,{i + 1},null,null,'{dt.Columns[i].ColumnName}','{GetDataType(dt.Columns[i].DataType.Name)}',null,null);");
                        }
                        else
                        {
                            //Console.WriteLine($"{dt.Columns[i].ColumnName} {dt.Columns[i].DataType.Name}({width.ToString()}),");
                            GeraScript($"{InsertInto}(@IdArquivo,{i + 1},null,null,'{dt.Columns[i].ColumnName}','{GetDataType(dt.Columns[i].DataType.Name)}',{width.ToString()},null);");
                        }
                        //Console.WriteLine($"Campo:{dt.Columns[i].ColumnName}");
                        //Console.WriteLine($" type:{dt.Columns[i].DataType.Name}");
                        //Console.WriteLine($" Size:{width.ToString()}");
                        //Console.WriteLine($" Prec:{dt.Columns[i]..ToString()}");
                        //Console.WriteLine($" -------------------------------------------");
                    }
                }
                GeraScript("End;");

                conexaoORG.FecharConexao(conexaoORG.Conn);

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //dataGridView1.DataSource = dt;
        }

        public string GetDataType(string pDataType)
        {
            string Result = "";
            switch (pDataType)
            {
                case "String":
                    Result= "varchar";
                    break;
                case "Double":
                    Result = "numeric";
                    break;
                case "DateTime":
                    Result = "datetime";
                    break;
                case "Integer":
                    Result = "int";
                    break;
            }
            return Result;
        }

        public static bool ArquivoEmUso(string caminhoArquivo)
        {
            try
            {
                FileStream fs = File.OpenWrite(caminhoArquivo);
                fs.Close();
                return false;
            }
            catch (System.IO.IOException ex)
            {
                return true;
            }
        }


        public void GeraScript(string pScript)
        {
            string NomeArquivo = "GeraScript.txt";
            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            path = (Path.GetDirectoryName(path));

            string CaminhoENomeFileLog = Path.Combine(path, NomeArquivo);
            if (!File.Exists(CaminhoENomeFileLog))
            {
                FileStream Arquivo = File.Create(CaminhoENomeFileLog);
                Arquivo.Close();
            }
            StreamWriter Log = null;
            if (!ArquivoEmUso(CaminhoENomeFileLog))
            {
                try
                {
                    Log = File.AppendText(CaminhoENomeFileLog);
                    Log.WriteLine(pScript);
                    Log.Close();
                }
                catch
                {
                    //
                }
            }

        }
    }
}
