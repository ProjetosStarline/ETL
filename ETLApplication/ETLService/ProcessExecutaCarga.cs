using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EtlConexao;

namespace ETLService
{
    class ProcessExecutaCarga
    {
        Conexao conexaoORG = null;
        Conexao conexaoDST = null;

        public ProcessExecutaCarga()
        {
            conexaoORG = new Conexao(ConfigurationManager.ConnectionStrings["DbStarLineEtl"].ToString());
            if (conexaoORG.ConexaoAberta())
            {
                conexaoDST = new Conexao(GetConnectionStringDST());
            }
        }
        public string GetConnectionStringDST()
        {
            try
            {
                SqlDataReader dr = conexaoORG.ExecutarSelect("select valor from tb_parametros where nm_parametro = 'connection_string_Base_Destino'");
                if (dr.HasRows && dr.Read())
                {
                    return dr["valor"].ToString();
                }
                else
                {
                    return "";
                    throw new Exception($"Erro: Parâmetro de configuração [connection_string_Base_Destino] não foi definido na tabela tb_parametros.");
                }

            }
            catch (Exception ex)
            {
                return "";
                throw new Exception($"Erro: {ex.Message}.");
            }
        }

        public int GetIntervaloProcessamento()
        {
            try
            {
                SqlDataReader dr = conexaoORG.ExecutarSelect("select valor from tb_parametros where nm_parametro = 'pooling_servico'");
                if (dr.HasRows && dr.Read())
                {
                    return Convert.ToInt32(dr["valor"].ToString());
                }
                else
                {
                    return 0;
                    throw new Exception($"Erro: Parâmetro de configuração [pooling_servico] não foi definido na tabela tb_parametros.");
                }

            }
            catch (Exception ex)
            {
                return 0;
                throw new Exception($"Erro: {ex.Message}.");
            }

        }

        public void OrquestraProcessamento()
        {
            try
            {
                SqlDataReader drMonitoramentos = conexaoORG.ExecutarSelect($"select id_arquivo from tb_monitoramentos where id_servico = {Global.IdServico}");

                if (drMonitoramentos.HasRows )
                {
                    while (drMonitoramentos.Read())
                    {
                        SqlDataReader drArquivos = conexaoORG.ExecutarSelect(
                            "select dir_entrada,mascara_arquivo, " +
                            "tb_destino,tp_carga,tp_arquivo,delimitador, cercador, dir_saida " +
                            $"from tb_arquivos where id_arquivo = {drMonitoramentos["id_arquivo"].ToString()}"
                         );
                        if (drArquivos.HasRows)
                        {
                            while (drArquivos.Read())
                            {
                                List<string> lstArquivos = new List<string>();
                                DirectoryInfo dir = new DirectoryInfo(drArquivos["dir_entrada"].ToString() + "\\" + drArquivos["mascara_arquivo"].ToString() +".*");
                                lstArquivos = BuscaArquivos(dir);
                                foreach (var lArq in lstArquivos)
                                {
                                    if (drArquivos["tb_destino"].ToString() == "FULL")
                                    {
                                        TrucaTabela(drArquivos["tp_carga"].ToString());
                                    }
                                    switch (drArquivos["tp_arquivo"].ToString())
                                    {
                                        case "DBF":
                                            //Processa arquivos DBF
                                            break;
                                        case "EXCEL":
                                            //Processa arquivos DBF
                                            break;
                                        case "DELIMITADO":
                                            //Processa arquivos DBF
                                            break;

                                    }
                                }

                            }
                        }

                            //$"select ordem, --> quais campos da tabela destino inserir" +
                            //$"fixo_inicio --> posição inicial do campo" +
                            //$"fixo_tamanho --> qtd de caracteres" +
                            //$"from tb_mapeamentos where id_arquivo = 1 order by ordem" +

                    }
                }
                else
                {
                    throw new Exception($"Erro: Não foi encontrado Código do Arquivo para o Serviço {Global.IdServico}.");
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"Erro: {ex.Message}.");
            }

        }

        private List<string> BuscaArquivos(DirectoryInfo dir)
        {
            List<string> lst = new List<string>();
            foreach (FileInfo file in dir.GetFiles())
            {
                lst.Add(file.FullName);
            }

            // busca arquivos do proximo sub-diretorio
            foreach (DirectoryInfo subDir in dir.GetDirectories())
            {
                BuscaArquivos(subDir);
            }

            return lst;
        }

        private bool TrucaTabela(string NomeTabela)
        {
            try
            {
                conexaoDST.ExecutarScript("Truncate table "+ NomeTabela);
                return true;
            }
            catch(Exception ex)
            {
                return false;
                throw new Exception("ERRO: Não foi possível Truncar a tabela "+ NomeTabela + " ["+ex.Message+"]");
            }
        }

    }
}
