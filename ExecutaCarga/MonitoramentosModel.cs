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
    public class MonitoramentosModel
    {
        public int id_servico { get; set; }
        public int id_arquivo { get; set; }
        //public string nm_servico { get; set; }
        public string status { get; set; }
        public DateTime data_criacao { get; set; }
        public DateTime data_atualizacao { get; set; }

        public List<MonitoramentosModel> ListMonitoramentos(SqlDataReader dr)
        {
            List<MonitoramentosModel> lst = new List<MonitoramentosModel>();
            try
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        lst.Add(
                            new MonitoramentosModel
                            {
                                id_servico = Convert.ToInt32(dr["id_servico"].ToString()),
                                id_arquivo = Convert.ToInt32(dr["id_arquivo"].ToString()),
                            //nm_servico = dr["nm_servico"].ToString(),
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
