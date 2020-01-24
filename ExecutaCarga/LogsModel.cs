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
    class LogsModel
    {
        public int id_arquivo { get; set; }
        public string etapa { get; set; }
        public DateTime data { get; set; }
        public string Mensagem { get; set; }
        public string ComandoSql { get; set; }

        public List<LogsModel> ListLogs(SqlDataReader dr)
        {
            List<LogsModel> lst = new List<LogsModel>();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    
                    lst.Add(
                        new LogsModel
                        {
                            id_arquivo = Convert.ToInt32(dr["id_arquivo"].ToString()),
                            etapa = dr["etapa"].ToString(),
                            data = Convert.ToDateTime(dr["data"].ToString()),
                            Mensagem = dr["Mensagem"].ToString(),
                            ComandoSql = dr["ComandoSql"].ToString()
                        }
                    );
                }
            }
            dr.Close();
            return lst;
        }
    }
}
