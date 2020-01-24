using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using System.Globalization;

namespace ETLApplication.DAO
{
    public class Conexao
    {
        public SqlConnection Conn;
        public List<SqlParameter> Parametros;
        public Conexao()
        {
            string connectiosStrings = ConfigurationManager.ConnectionStrings["DbStarLineEtl"].ToString();
            Conn = new SqlConnection(connectiosStrings);
            Parametros = new List<SqlParameter>();
        }

        public void LimparParametros()
        {
            Parametros.Clear();
        }
        public void AbrirConexao()
        {
            if (!ConexaoAberta())
            {
                Conn.Open();
            }
        }
        public void FecharConexao()
        {
            if (ConexaoAberta())
            {
                Conn.Close();
            }
        }

        public bool ConexaoAberta()
        {
            return Conn.State == ConnectionState.Open;
        }

        public SqlDataReader ExecutarSelect(string select)
        {
            FecharConexao();
            AbrirConexao();
            SqlCommand cmd = new SqlCommand(select, Conn);
            for(int i = 0; i <= Parametros.Count-1; i++)
            {
                cmd.Parameters.AddWithValue(Parametros[i].ParameterName, Parametros[i].Value);
            }

            return cmd.ExecuteReader();
        }

        public void ExecutarScript(string script)
        {
            try
            {
                Conn.Close();
                Conn.Open();
                SqlCommand cmd = new SqlCommand(script, Conn);
                for (int i = 0; i <= Parametros.Count-1; i++)
                {
                    cmd.Parameters.AddWithValue(Parametros[i].ParameterName, Parametros[i].Value);
                }
                cmd.ExecuteNonQuery();

            }
            finally
            {
                Conn.Close();
            }
        }

        public void GetDadosTodos(string script, DataGridView dgv)
        {
            if (!ConexaoAberta())
            {
                AbrirConexao();
            }
            try
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(script, Conn);
                SqlCommandBuilder buider = new SqlCommandBuilder(dataAdapter);
                DataTable dtable = new DataTable()
                {
                    Locale = CultureInfo.InvariantCulture
                };
                dataAdapter.Fill(dtable);
                dgv.DataSource = dtable;
            }
            finally
            {
                if (ConexaoAberta())
                {
                    FecharConexao();
                }
            }
        }

        public void GetDadosTodos(string script, ComboBox cBox)
        {
            if (!ConexaoAberta())
            {
                AbrirConexao();
            }
            try
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(script, Conn);
                DataTable dtable = new DataTable()
                {
                    Locale = CultureInfo.InvariantCulture
                };
                dataAdapter.Fill(dtable);
                cBox.DataSource = dtable;
                cBox.ValueMember = "";
                cBox.DisplayMember = "";
                cBox.SelectedItem = 0;
            }
            finally
            {
                if (ConexaoAberta())
                {
                    FecharConexao();
                }
            }
        }
    }
}


