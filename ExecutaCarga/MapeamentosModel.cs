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
    public class MapeamentosModel
    {
        public int id_mapeamento { get; set; }
        public int id_arquivo { get; set; }
        public string nm_coluna { get; set; }
        public int ordem { get; set; }
        public int fixo_inicio { get; set; }
        public int fixo_tamanho { get; set; }
        public int id_indice { get; set; }
        public DateTime data_criacao { get; set; }
        public DateTime data_atualizacao { get; set; }
        public string tp_coluna { get; set; }
        public int tm_coluna { get; set; }
        public int pr_coluna { get; set; }
        public string Mask_Campo { get; set; }
        public string ExpressaoSql { get; set; }

        public List<MapeamentosModel> LstMapeamento(SqlDataReader dr)
        {
            List<MapeamentosModel> lst = new List<MapeamentosModel>();
            try
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        lst.Add(
                             new MapeamentosModel
                             {
                                 id_mapeamento = Convert.ToInt32(dr["id_mapeamento"].ToString()),
                                 id_arquivo = Convert.ToInt32(dr["id_arquivo"].ToString()),
                                 nm_coluna = dr["nm_coluna"].ToString(),
                                 ordem = Convert.ToInt32(dr["ordem"].ToString() == "" ? "0" : dr["ordem"].ToString()),
                                 fixo_inicio = Convert.ToInt32(dr["fixo_inicio"].ToString() == "" ? "0" : dr["fixo_inicio"].ToString()),
                                 fixo_tamanho = Convert.ToInt32(dr["fixo_tamanho"].ToString() == "" ? "0" : dr["fixo_tamanho"].ToString()),
                                 id_indice = Convert.ToInt32(dr["id_indice"].ToString() == "" ? "0" : dr["id_indice"].ToString()),
                                 data_criacao = Convert.ToDateTime(dr["data_criacao"].ToString()),
                                 data_atualizacao = Convert.ToDateTime(dr["data_atualizacao"].ToString()),
                                 tp_coluna = dr["tp_coluna"].ToString(),
                                 tm_coluna = Convert.ToInt32(dr["tm_coluna"].ToString() == "" ? "0" : dr["tm_coluna"].ToString()),
                                 pr_coluna = Convert.ToInt32(dr["pr_coluna"].ToString() == "" ? "0" : dr["pr_coluna"].ToString()),
                                 Mask_Campo = dr["Mask_Campo"].ToString(),
                                 ExpressaoSql = dr["ExpressaoSql"].ToString()
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
