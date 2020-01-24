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
    class PacotesModel
    {
        public int id_pacote { get; set; }
        public int id_categoria { get; set; }
        public string nm_pacote { get; set; }
        public string status { get; set; }
        public DateTime data_criacao { get; set; }
        public DateTime data_atualizacao { get; set; }
        public List<PacotesModel> LstMapeamento(SqlDataReader dr)
        {
            List<PacotesModel> lst = new List<PacotesModel>();
            try
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        lst.Add(
                             new PacotesModel
                             {
                                 id_pacote = Convert.ToInt32(dr["id_pacote"].ToString()),
                                 id_categoria = Convert.ToInt32(dr["id_categoria"].ToString()),
                                 nm_pacote = dr["nm_pacote"].ToString(),
                                 status = dr["status"].ToString(),
                                 data_criacao = Convert.ToDateTime(dr["data_criacao"].ToString()),
                                 data_atualizacao = Convert.ToDateTime(dr["data_atualizacao"].ToString())
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
