/*
	==============================================
	Criado por : Carlos R. S. Oliveira
	Criado em  : 20/05/2019
	==============================================
*/
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExecutaCarga
{
    public class ArquivosModel
    {
        public int id_arquivo { get; set; }
        public int id_pacote { get; set; }
        public string nm_arquivo { get; set; }
        public string mascara_arquivo { get; set; }
        public string tp_carga { get; set; }
        public string tp_arquivo { get; set; }
        public string delimitador { get; set; }
        public string cercador { get; set; }
        public string tb_destino { get; set; }
        public string dir_entrada { get; set; }
        public string dir_saida { get; set; }
        public string rbd_tabela { get; set; }
        public string rbd_indice { get; set; }
        public string status { get; set; }
        public DateTime data_criacao { get; set; }
        public DateTime data_atualizacao { get; set; }
        public string nm_Planilha { get; set; }
        public string LineFeed { get; set; }
        public int FirstLine { get; set; }
        public string ConexaoBusiness { get; set; }
        public string FlagPlanTemVariasAbas { get; set; }
        public string FlagUltimaAbaPlanilha { get; set; }



        public List<ArquivosModel> LstArquivos(SqlDataReader dr)
        {
            List<ArquivosModel> lst = new List<ArquivosModel>();
            try
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        lst.Add(
                                new ArquivosModel
                                {
                                    id_arquivo = Convert.ToInt32(dr["id_arquivo"].ToString()),
                                    id_pacote = Convert.ToInt32(dr["id_pacote"].ToString()),
                                    nm_arquivo = dr["nm_arquivo"].ToString(),
                                    mascara_arquivo = dr["mascara_arquivo"].ToString(),
                                    tp_carga = dr["tp_carga"].ToString(),
                                    tp_arquivo = dr["tp_arquivo"].ToString(),
                                    delimitador = dr["delimitador"].ToString(),
                                    cercador = dr["cercador"].ToString(),
                                    tb_destino = dr["tb_destino"].ToString(),
                                    dir_entrada = dr["dir_entrada"].ToString(),
                                    dir_saida = dr["dir_saida"].ToString(),
                                    rbd_tabela = dr["rbd_tabela"].ToString(),
                                    rbd_indice = dr["rbd_indice"].ToString(),
                                    status = dr["status"].ToString(),
                                    data_criacao = Convert.ToDateTime(dr["data_criacao"].ToString()),
                                    data_atualizacao = Convert.ToDateTime(dr["data_atualizacao"].ToString()),
                                    nm_Planilha = dr["nm_Planilha"].ToString(),
                                    LineFeed = dr["LineFeed"].ToString(),
                                    FirstLine = Convert.ToInt32(dr["FirstLine"].ToString()),
                                    ConexaoBusiness = dr["ConexaoBusiness"].ToString(),
                                    FlagPlanTemVariasAbas = dr["FlagPlanTemVariasAbas"].ToString(),
                                    FlagUltimaAbaPlanilha = dr["FlagUltimaAbaPlanilha"].ToString()

                                }
                        );
                    }
                }
            }
            finally
            {
                dr.Close();
            }
            return lst;
        }
    }
}

