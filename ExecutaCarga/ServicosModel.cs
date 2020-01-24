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
    public class ServicosModel
    {
        public int id_servico { get; set; }
        public string nm_servico { get; set; }
        public string situacao { get; set; }
        public string status { get; set; }
        public DateTime data_criacao { get; set; }
        public DateTime data_atualizacao { get; set; }

        public List<ServicosModel> ListaServicos(SqlDataReader dr)
        {
            List<ServicosModel> lst = new List<ServicosModel>();
            try
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        lst.Add(
                            new ServicosModel
                            {
                                id_servico = Convert.ToInt32(dr["id_servico"].ToString()),
                                nm_servico = dr["nm_servico"].ToString(),
                                situacao = dr["situacao"].ToString(),
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
