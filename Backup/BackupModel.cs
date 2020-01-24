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

namespace Backup
{
    class BackupModel
    {
        public int Id_backup { get; set; }
        public int ID_Processo { get; set; }
        public string NomeApp { get; set; }
        public string PathBackup { get; set; }
        public string Nomebackup { get; set; }
        public DateTime Data_Criacao { get; set; }
        public string Mensagem { get; set; }
        public string Status { get; set; }
        public void SetDados(SqlDataReader dr)
        {
            try
            {
                if (dr.HasRows && dr.Read())
                {
                    Id_backup = Convert.ToInt32(dr["Id_backup"].ToString());
                    ID_Processo = Convert.ToInt32(dr["ID_Processo"].ToString());
                    NomeApp = dr["NomeApp"].ToString();
                    PathBackup = dr["PathBackup"].ToString();
                    Nomebackup = dr["Nomebackup"].ToString();
                    Mensagem = dr["Mensagem"].ToString();
                    Status = dr["status"].ToString();
                    Data_Criacao = Convert.ToDateTime(dr["Data_Criacao"].ToString());
                }
            }
            finally
            {
                dr.Close();
            }
        }
    }
}
