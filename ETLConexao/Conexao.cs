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
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace EtlConexao
{
    public class Conexao
    {
        public static string ConnectionStrings = ConfigurationManager.ConnectionStrings["DbStarLineEtl"].ToString();
        public SqlConnection Conn;
        public List<SqlParameter> Parametros;
        public Conexao()
        {
            try
            {
                Conn = new SqlConnection(ConnectionStrings);
                Parametros = new List<SqlParameter>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void LimparParametros()
        {
            try
            {
                Parametros.Clear();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void AbrirConexao(SqlConnection pConn)
        {
            try
            {
                if (!ConexaoAberta(pConn))
                {
                    pConn.Open();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void FecharConexao(SqlConnection pConn)
        {
            try
            {
                if (ConexaoAberta(pConn))
                {
                    pConn.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ConexaoAberta(SqlConnection pConn)
        {
            try
            {
                return pConn.State == ConnectionState.Open;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public SqlDataReader ExecutarSelect(string select, int TimeOut = 0)
        {
            SqlDataReader dr = null;
            try
            {
                SqlConnection _Conn = new SqlConnection(ConnectionStrings);
                _Conn.Open();
                SqlCommand cmd = new SqlCommand(select, _Conn);
                cmd.CommandTimeout = TimeOut;
                for (int i = 0; i <= Parametros.Count - 1; i++)
                {
                    cmd.Parameters.AddWithValue(Parametros[i].ParameterName, Parametros[i].Value);
                }
                dr = cmd.ExecuteReader();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dr;
        }


        public void ExecutarScript(string script,int TimeOut=0)
        {
            try
            {
                using (SqlConnection _Conn = new SqlConnection(ConnectionStrings))
                {
                    _Conn.Open();
                    SqlCommand cmd = new SqlCommand(script, _Conn);
                    cmd.CommandTimeout = TimeOut;
                    for (int i = 0; i <= Parametros.Count - 1; i++)
                    {
                        cmd.Parameters.AddWithValue(Parametros[i].ParameterName, Parametros[i].Value);
                    }
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void ExecutarScript(string script, SqlConnection _Conn)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(script, _Conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static SqlDataReader ExecutarSelect(string select, SqlConnection _Conn)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(select, _Conn);
                return cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public void GetDadosTodos(string script, DataGridView dgv)
        {
            SqlConnection _Conn = new SqlConnection(ConnectionStrings);
            try
            {
                _Conn.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(script, _Conn);
                SqlCommandBuilder buider = new SqlCommandBuilder(dataAdapter);
                DataTable dtable = new DataTable()
                {
                    Locale = CultureInfo.InvariantCulture
                };
                dataAdapter.Fill(dtable);
                dgv.DataSource = dtable;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _Conn.Close();
            }
        }

        public void GetDadosTodos(string script, ComboBox cBox)
        {
            SqlConnection _Conn = new SqlConnection(ConnectionStrings);
            try
            {
                _Conn.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(script, _Conn);
                //SqlCommandBuilder buider = new SqlCommandBuilder(dataAdapter);
                DataTable dtable = new DataTable()
                {
                    Locale = CultureInfo.InvariantCulture
                };
                dataAdapter.Fill(dtable);
                cBox.DataSource = dtable;
                //cBox.ValueMember = "Id_perfil";
                //cBox.DisplayMember = "Nm_perfil";
                cBox.SelectedItem = "";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _Conn.Close();
            }
        }

        public static bool TransacaoAtiva { get; set; }
        public static bool IniciaTransacao(string NomeTransacao,SqlConnection _Conn)
        {
            try
            {
                if (_Conn.State != ConnectionState.Open)
                {
                    _Conn.Open();
                };
                ExecutarScript("BEGIN TRAN " + NomeTransacao, _Conn);
                TransacaoAtiva= true;
            }
            catch (Exception ex)
            {
                TransacaoAtiva = false;
                throw new Exception(ex.Message);
            }
            return TransacaoAtiva;
        }

        public static bool CommitaTransacao(string NomeTransacao, SqlConnection _Conn)
        {
            bool Result = false;
            try
            {
                if (_Conn.State!=ConnectionState.Open)
                {
                    _Conn.Open();
                };

                ExecutarScript("COMMIT TRAN " + NomeTransacao, _Conn);
                TransacaoAtiva=false;
                Result= true;
            }
            catch (Exception ex)
            {
                Result = false;
                throw new Exception(ex.Message);
            }
            return Result;
        }
        public static bool ConcelaTransacao(string NomeTransacao, SqlConnection _Conn)
        {
            bool Result = false;
            try
            {
                if (_Conn.State != ConnectionState.Open)
                {
                    _Conn.Open();
                };
                ExecutarScript("ROLLBACK TRAN " + NomeTransacao, _Conn);
                TransacaoAtiva = false;
                Result= true;
            }
            catch (Exception ex)
            {
                Result = false;
                throw new Exception(ex.Message);
            }
            return Result;
        }

        public static bool ExecutaCommandText(string pCommandText, string _ConnectionStrings,ref string erro )
        {
            bool Result = false; ;
            SqlConnection _Conn = new SqlConnection(_ConnectionStrings);
            try
            {
                _Conn.Open();
                string script = pCommandText;
                SqlCommand cmd = new SqlCommand(pCommandText, _Conn);
                cmd.CommandTimeout = 0;
                cmd.ExecuteNonQuery();
                Result = true;
            }
            catch(Exception ex)
            {
                Result = false;
                erro = ex.Message;
                throw new Exception(ex.Message);
            }
            finally
            {
                _Conn.Close();
            }
            return Result;
        }
    }
}


