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
using System.Windows.Forms;
using ETLApplication.Model;
using EtlConexao;

namespace ETLApplication.Controller
{
    public class ArquivosController
    {
        Conexao conexao = null;
        public CadastroBase<ArquivosModel> CadArquivosBase;
        public ArquivosController()
        {
            conexao = new Conexao();
            CadArquivosBase = new CadastroBase<ArquivosModel>();

        }
        public List<MapeamentosModel> GetEstruturaFile(string NomeFile, string NomePlanilha = "")
        {
            List<MapeamentosModel> mapeamentos = new List<MapeamentosModel>(); 
            try
            {
                string script = "SELECT top 10 * ";
                script += "FROM OPENROWSET( ";
                script += "'Microsoft.ACE.OLEDB.12.0', ";
                if (NomePlanilha.Length > 0)
                {
                    script += $"'Excel 12.0; Database={NomeFile}', ";
                    script += $"' SELECT * FROM [{NomePlanilha}$]' )";
                }
                else
                {
                    script += $"'dBase 5.0; Database={Path.GetDirectoryName(NomeFile)}', ";
                    script += $"' SELECT * FROM [{Path.GetFileName(NomeFile)}]' )";
                }

                conexao.AbrirConexao(conexao.Conn);
                SqlCommand oCmd = new SqlCommand(script, conexao.Conn);
                oCmd.CommandTimeout = 0;
                DataTable dt = new DataTable();
                dt.Load(oCmd.ExecuteReader());


                //foreach (DataRow row in dt.Rows)
                //{

                    //string txt = row["columnName"].ToString();
                    //string txt = string.Format("ColumnName:{0} DataType:{1} Ordinal:{2} Precision:{3} Size:{4} ",
                    // row["DataTypeName"], row["ColumnOrdinal"],
                    //row["NumericPrecision"], row["ColumnSize"], row["NumericScale"]);
                    //Console.Write(txt);
                //}

                int width = 0;
                for (int i = 0; i <= dt.Columns.Count - 1; i++)
                {
                    bool isNumber = false;
                    string Num = dt.Columns[i].ColumnName.Substring(1, 1);
                    int Size = dt.Columns[i].MaxLength;
                    string DataType = dt.Columns[i].DataType.ToString();
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
                        string TipoDeDados = GetDataType(dt.Columns[i].DataType.Name);
                        width = 0;
                        switch (TipoDeDados)
                        {
                            case "varchar":
                                width = dt.Columns[i].MaxLength;
                                break;
                            case "numeric":
                                width = 18;
                                break;
                        }

                    }

                    mapeamentos.Add(
                        new MapeamentosModel()
                        {
                            id_arquivo = 0,
                            nm_coluna = dt.Columns[i].ColumnName,
                            ordem = i + 1,
                            fixo_inicio = 0,
                            fixo_tamanho = 0,
                            tp_coluna = GetDataType(dt.Columns[i].DataType.Name),
                            tm_coluna = width,
                            pr_coluna = 0,
                            MASK_CAMPO = "",
                            ExpressaoSql = ""
                        }
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao mapear o arquivo {Path.GetFileName(NomeFile)}"+ex.Message);
            }
            finally
            {
                conexao.FecharConexao(conexao.Conn);
            }
            return mapeamentos;
        }

        public string GetDataType(string pDataType)
        {
            string Result = "varchar";
            switch (pDataType)
            {
                case "String":
                    Result = "varchar";
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

        public string GetNomeServico(int pIdArquivo)
        {
            return CadArquivosBase.SelectNomeServico(pIdArquivo);
        }

        public void DeletarMonitoramentos(int pIdArquivo)
        {
            string script = $"Delete from tb_Monitoramentos where id_arquivo={pIdArquivo}";
            conexao.ExecutarScript(script);
        }

        public string GetCondicaoPesquisa(string pIdGrupo,string pIdCateg,string pIdPacote="")
        {
            string Result = "";
            Result += " and a.id_arquivo in(";
            Result += " select distinct mo.id_arquivo";
            Result += " from tb_arquivos a,";
            Result += " tb_monitoramentos mo, ";
            Result += " tb_Grupos g,";
            Result += " tb_Categorias c,";
            Result += " tb_pacotes p";
            Result += " where a.id_arquivo = mo.id_arquivo";
            Result += " and a.id_pacote = p.id_pacote";
            Result += " and c.id_categoria = p.id_categoria";
            if (pIdPacote.Length>0)
            {
                Result += $" and p.id_pacote = {pIdPacote}";
            }
            if (pIdGrupo.Length > 0)
            {
                Result += $" and c.id_categoria = {pIdGrupo}";
            }
            if (pIdCateg.Length > 0)
            {
                Result += $" and c.id_grupo = {pIdCateg}";
            }
            Result += ") and a.status = 'ativo'";
            return Result;
        }

        public string GetCondicaoPesquisa(string pIdGrupo, string pIdCateg, string pIdPacote = "",string pIdServico="")
        {
            string Result = "";
            Result += " and a.id_arquivo in(";
            Result += " select distinct mo.id_arquivo";
            Result += " from tb_arquivos a,";
            Result += " tb_monitoramentos mo, ";
            Result += " tb_Grupos g,";
            Result += " tb_Categorias c,";
            Result += " tb_pacotes p";
            Result += " where a.id_arquivo = mo.id_arquivo";
            Result += " and a.id_pacote = p.id_pacote";
            Result += " and c.id_categoria = p.id_categoria";
            if (pIdPacote.Length > 0)
            {
                Result += $" and p.id_pacote = {pIdPacote}";
            }
            if (pIdGrupo.Length > 0)
            {
                Result += $" and c.id_categoria = {pIdCateg}";
            }
            if (pIdCateg.Length > 0)
            {
                Result += $" and c.id_grupo = {pIdGrupo}";
            }
            if (pIdServico.Length > 0)
            {
                Result += $" and mo.id_servico = {pIdServico}";
            }
            Result += ") and a.status = 'ativo'";
            return Result;
        }

        public int GetIdArquivo(string pNmArquivo)
        {
            int Result = 0;
            string script = $"select id_arquivo from tb_arquivos where nm_arquivo ='{pNmArquivo}' and status='ativo'";
            using (SqlDataReader dr = conexao.ExecutarSelect(script))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Result = Global.StrToInt(dr["id_arquivo"].ToString());
                    }
                }
            }
            return Result;

        }
        public int GetIdArquivo(string pNmArquivo,string pdir_entrada)
        {
            int Result = 0;
            string script = $"select id_arquivo from tb_arquivos where nm_arquivo ='{pNmArquivo}' and dir_entrada ='{pdir_entrada}'";
            using (SqlDataReader dr = conexao.ExecutarSelect(script))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Result = Global.StrToInt(dr["id_arquivo"].ToString());
                    }
                }
            }
            return Result;

        }

        public string GetSelect()
        {
            string  
            script  = "Select distinct ";
            script += " a.id_arquivo 'ID',";
            script += " p.id_pacote 'ID Pacote',";
            script += " p.nm_pacote 'Pacote',";
            script += " s.id_servico 'ID Serviço',";
            script += " s.nm_servico 'Serviço',";
            script += " a.nm_arquivo 'Arquivo',";
            script += " a.mascara_arquivo 'Máscara',";
            script += " a.tp_carga 'Tipo Carga',";
            script += " a.tp_arquivo 'Tipo Arquivo',";
            script += " a.delimitador 'Delimitador',";
            script += " a.cercador 'Cercador',";
            script += " a.tb_destino 'Tabela Destino',";
            script += " a.dir_entrada 'Dir. Entrada',";
            script += " a.dir_saida 'Dir. Saída',";
            script += " a.nm_Planilha 'Planilha',";
            script += " a.Status,";
            script += " a.data_criacao 'Data Criação',";
            script += " a.data_atualizacao 'Data Atualização',";
            script += " a.LineFeed 'LineFeed',";
            script += " a.FirstLine 'FirstLine',";
            script += " a.ConexaoBusiness 'ConexaoBusiness',";
            script += " a.FlagPlanTemVariasAbas 'Flag Planilha Tem Várias Abas',";
            script += " a.FlagUltimaAbaPlanilha 'Flag última Aba da Planilha'";
            script += " from tb_grupos g,";
            script += " tb_categorias c, ";
            script += " tb_pacotes p,";
            script += " tb_servicos s, ";
            script += " tb_monitoramentos mo,";
            script += " tb_arquivos a, ";
            script += " tb_mapeamentos mp,";
            script += " tb_UsuariosXcategorias uc";
            script += " where c.id_grupo = g.id_grupo";
            script += "   and p.id_categoria = c.id_categoria";
            script += "   and s.id_servico = mo.id_servico";
            script += "   and a.id_arquivo = mo.id_arquivo";
            script += "   and a.id_arquivo = mp.id_arquivo";
            script += "   and p.id_pacote = a.id_pacote";
            script += "   and uc.id_categoria = c.id_categoria";
            script += $"  and uc.id_usuario = {Global.UsuarioLogado.Id_usuario.ToString()}";
            return script;

        }
        public void PreencheComboArquivos(ComboBox pCbArquivos,int pIdPacote)
        {
            string select = "";
            select  = "Select distinct";
            select += "   a.id_arquivo,";
            select += "   a.nm_arquivo";
            select += " from ";
            select += "   tb_categorias c,";
            select += "   tb_pacotes p,";
            select += "   tb_arquivos a,";
            select += "   tb_UsuariosXcategorias uc";
            select += " where p.id_categoria = c.id_categoria";
            select += "   and p.id_pacote = a.id_pacote";
            select += "   and uc.id_categoria = c.id_categoria";
            select += $"  and p.id_pacote={pIdPacote.ToString()}";
            select += $"  and p.id_categoria={new ParametrosController().GetValorParametro("Filial_Selecionada").ToString()}";
            select += $"  and uc.id_usuario = {Global.UsuarioLogado.Id_usuario.ToString()}";

            CadArquivosBase.conexao.GetDadosTodos(select, pCbArquivos);
            pCbArquivos.ValueMember = "id_arquivo";
            pCbArquivos.DisplayMember = "nm_arquivo";
            if (pCbArquivos.Items.Count > 0)
            {
                pCbArquivos.SelectedIndex = 0;
            }
        }
        public void SetListaArquivos(ComboBox pCbArquivos, int pIdPacote)
        {
            string select = "";
            select = "Select distinct";
            select += "   a.id_arquivo,";
            select += "   a.nm_arquivo";
            select += " from ";
            select += "   tb_categorias c,";
            select += "   tb_pacotes p,";
            select += "   tb_arquivos a,";
            select += "   tb_UsuariosXcategorias uc";
            select += " where p.id_categoria = c.id_categoria";
            select += "   and p.id_pacote = a.id_pacote";
            select += "   and uc.id_categoria = c.id_categoria";
            select += $"  and p.id_pacote={pIdPacote.ToString()}";
            select += $"  and p.id_categoria={new ParametrosController().GetValorParametro("Filial_Selecionada").ToString()}";
            select += $"  and uc.id_usuario = {Global.UsuarioLogado.Id_usuario.ToString()}";

            pCbArquivos.Items.Clear();
            pCbArquivos.Items.Add("Todos");
            SqlDataReader dr = conexao.ExecutarSelect(select);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    pCbArquivos.Items.Add(dr["nm_arquivo"]);
                }
            }
            pCbArquivos.ValueMember = "id_arquivo";
            pCbArquivos.DisplayMember = "nm_arquivo";
            if (pCbArquivos.Items.Count > 0)
            {
                pCbArquivos.SelectedIndex = 0;
            }
        }

        public bool ArquivoCadastrado(string pNmeArquivo,int pIdPacote)
        {
            bool Result = false;
            string script = $"select id_arquivo from tb_arquivos where id_pacote={pIdPacote.ToString()} and nm_arquivo='{pNmeArquivo}'";
            SqlDataReader dr = conexao.ExecutarSelect(script);
            if (dr.HasRows && dr.Read())
            {
                string value = dr["id_arquivo"].ToString();
                Result = Global.StrToInt(value) > 0;
            }
            return Result;
        }
        public bool TabelaDSTCadastrada(string pNmeTabelaDst)
        {
            bool Result = false;
            string script = $"select id_arquivo from tb_arquivos where tb_destino='{pNmeTabelaDst}'";
            SqlDataReader dr = conexao.ExecutarSelect(script);
            if (dr.HasRows && dr.Read())
            {
                string value = dr["id_arquivo"].ToString();
                Result = Global.StrToInt(value) > 0;
            }
            return Result;
        }
    }
}
