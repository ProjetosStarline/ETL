/*
	==============================================
	Criado por : Carlos R. S. Oliveira
	Criado em  : 20/05/2019
	==============================================
*/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ETLApplication.Model
{
    public class ParametrosModel:GenericModel
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_parametro { get; set; }
        public string nm_parametro { get; set; }
        public string valor { get; set; }
        public string status { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public DateTime data_criacao { get; set; }
        public DateTime data_atualizacao { get; set; }
        public ParametrosModel()
        {
            NomeTabela = "tb_parametros";
        }
        public void SetDados(SqlDataReader dr)
        {
            try
            {
                id_parametro = Convert.ToInt32(isNullResultVazio(dr["id_parametro"]));
                nm_parametro = isNullResultVazio(dr["nm_parametro"]);
                valor = isNullResultVazio(dr["valor"]);
                status = isNullResultVazio(dr["status"]);
                data_criacao = Convert.ToDateTime(dr["Data_criacao"].ToString());
                data_atualizacao = Convert.ToDateTime(dr["Data_atualizacao"].ToString());
            }
            finally
            {
                dr.Close();
            }

        }
        public List<ParametrosModel> GetListaPermissoes(SqlDataReader dr)
        {
            List<ParametrosModel> parametros = new List<ParametrosModel>();
            try
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        parametros.Add
                        (
                            new ParametrosModel()
                            {
                                id_parametro = Convert.ToInt32(isNullResultVazio(dr["id_parametro"])),
                                nm_parametro = isNullResultVazio(dr["nm_parametro"]),
                                valor = isNullResultVazio(dr["valor"]),
                                status = isNullResultVazio(dr["status"]),
                                data_criacao = Convert.ToDateTime(dr["Data_criacao"].ToString()),
                                data_atualizacao = Convert.ToDateTime(dr["Data_atualizacao"].ToString())
                            }
                        );
                    }
                }
            }
            finally
            {
                dr.Close();
            }
            return parametros;
        }

        public override void SetDados(DataGridView dgv)
        {
            if (dgv.CurrentRow != null)
            {
                if (dgv.CurrentRow.Cells["ID"].Value.ToString() != "")
                {
                    id_parametro = Convert.ToInt32(isNullResultZero(dgv.CurrentRow.Cells["ID"].Value.ToString()));
                    nm_parametro = isNullResultVazio(dgv.CurrentRow.Cells["Parâmetro"].Value);
                    valor = isNullResultVazio(dgv.CurrentRow.Cells["Valor"].Value);
                    status = isNullResultVazio(dgv.CurrentRow.Cells["status"].Value);
                    data_criacao = Convert.ToDateTime(dgv.CurrentRow.Cells["Data Criação"].Value.ToString());
                    data_atualizacao = Convert.ToDateTime(dgv.CurrentRow.Cells["Data Atualização"].Value.ToString());
                }
            }
        }
        public override void Clear()
        {
            id_parametro = 0;
            nm_parametro = "";
            valor = "";
            status = "";
            data_criacao = DateTime.Now;
            data_atualizacao = DateTime.Now;

        }

        public override void GetListaCampos()
        {
            base.GetListaCampos();
            ListaCamposView.Clear();
            ListaCamposView.Add("ID");
            ListaCamposView.Add("Parâmetro");
            ListaCamposView.Add("Valor");
            ListaCamposView.Add("Status");
            ListaCamposView.Add("Data Criação");
            ListaCamposView.Add("Data Atualização");
        }

        public override string GetFieldParaBusca(string pCampoView)
        {
            string Result = "";
            switch (pCampoView)
            {
                case "ID": Result = "id_parametro"; break;
                case "Parâmetro": Result = "nm_parametro"; break;
                case "Valor": Result = "Valor"; break;
                case "Status": Result = "status"; break;
                case "Data Criação": Result = "data_criacao"; break;
                case "Data Atualização": Result = "data_atualizacao"; break;
            }
            return Result;
        }

        public override string GetTypeFieldParaBusca(string pCampoView)
        {
            string Result = "";
            switch (pCampoView)
            {
                case "ID": Result = "integer"; break;
                case "Parâmetro": Result = "String"; break;
                case "Valor": Result = "String"; break;
                case "Status": Result = "String"; break;
                case "Data Criação": Result = "DateTime"; break;
                case "Data Atualização": Result = "DateTime"; break;
            }
            return Result;
        }
    }
}
