/*
	==============================================
	Criado por : Carlos R. S. Oliveira
	Criado em  : 20/05/2019
	==============================================
*/
using EtlConexao;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETLApplication.Controller
{
    public class LogsController
    {
        Conexao conexao; 
        public LogsController()
        {
            conexao = new Conexao(); 
        }
        public bool GravaLog(int pIdArquivo, string pEtapa, string pMensagem, string pCmdSql)
        {
            string script = "";
            SqlConnection _Conn = new SqlConnection(conexao.Conn.ConnectionString);
            bool Result = false;
            try
            {
                _Conn.Open();
                script = "insert into tb_logs(id_arquivo,etapa,data,Mensagem,ComandoSql) values(@id_arquivo,@etapa,@data,@Mensagem,@ComandoSql)";
                List<SqlParameter> Parametros = new List<SqlParameter>();
                Parametros.Clear();
                Parametros.Add(new SqlParameter() { ParameterName = "id_arquivo", Value = pIdArquivo });
                Parametros.Add(new SqlParameter() { ParameterName = "etapa", Value = pEtapa });
                Parametros.Add(new SqlParameter() { ParameterName = "data", Value = DateTime.Now });
                Parametros.Add(new SqlParameter() { ParameterName = "Mensagem", Value = pMensagem });
                Parametros.Add(new SqlParameter() { ParameterName = "ComandoSql", Value = pCmdSql });
                SqlCommand cmd = new SqlCommand(script, _Conn);
                cmd.CommandTimeout = Global.CommandTimeOut;
                for (int i = 0; i <= Parametros.Count - 1; i++)
                {
                    cmd.Parameters.AddWithValue(Parametros[i].ParameterName, Parametros[i].Value);
                }
                cmd.ExecuteNonQuery();
                Result = true;
            }
            catch (Exception ex)
            {
                //Global.EnviarParaArquivoLog("ErroAoGravarLog", $"insert into tb_logs(id_arquivo, etapa, data, Mensagem, ComandoSql) values({pIdArquivo}, {pEtapa}, {DateTime.Now.ToString()}, {pMensagem}, {pCmdSql})");
                Global.EnviarParaLog($"Erro ao gravar o log. MOTIVO: {ex.Message}.",null,"GravarLog");
                Result = false;
            }
            finally
            {
                _Conn.Close();
            }
            return Result;
        }

        public string GetBackupGerado()
        {
            string Result = "Backup não Gerado.";
            string script = "select Mensagem from tb_backup";
            script += $" where CONVERT(date, data_criacao, 101)=CONVERT(date, GetDate(), 101)";
            script += $" and status='Gerado'";
            SqlConnection _Conn = new SqlConnection(conexao.Conn.ConnectionString);
            SqlCommand cmd = null;
            try
            {
                _Conn.Open();
                cmd = new SqlCommand(script, _Conn);
                cmd.CommandTimeout = Global.CommandTimeOut;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Result = dr["Mensagem"].ToString();
                    }
                }
            }
            finally
            {
                _Conn.Close();
            }
            return Result;
        }

        public string GetBackupData()
        {
            string Result = "Backup não Gerado.";
            string script = "select distinct CONVERT(date, data_criacao, 103) Data from tb_backup";
            script += $" where CONVERT(date, data_criacao, 103)=CONVERT(date, GetDate(), 103)";
            script += $"   and status='Gerado'";
            SqlConnection _Conn = new SqlConnection(conexao.Conn.ConnectionString);
            SqlCommand cmd = null;
            try
            {
                _Conn.Open();
                cmd = new SqlCommand(script, _Conn);
                cmd.CommandTimeout = Global.CommandTimeOut;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Result = dr["Data"].ToString();
                    }
                }
            }
            finally
            {
                _Conn.Close();
            }
            return Result;
        }
    }
}
