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
    public class ServicosModel:GenericModel
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_servico { get; set; }
        public string nm_servico { get; set; }
        public string situacao { get; set; }
        public string status { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public DateTime data_criacao { get; set; }
        public DateTime data_atualizacao { get; set; }
        public ServicosModel()
        {
            NomeTabela = "tb_servicos";
        }

        public void SetDados(SqlDataReader dr)
        {
            try
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        id_servico = Convert.ToInt32(dr["id_servico"].ToString());
                        status = dr["Status"].ToString();
                        data_criacao = Convert.ToDateTime(dr["Data_criacao"].ToString());
                        data_atualizacao = Convert.ToDateTime(dr["Data_atualizacao"].ToString());
                        nm_servico = "Serviço " + dr["id_servico"].ToString();
                        situacao = dr["situacao"].ToString();
                    }
                }
            }
            finally
            {
                dr.Close();
            }

        }
        public List<ServicosModel> GetListaPermissoes(SqlDataReader dr)
        {
            List<ServicosModel> servicos = new List<ServicosModel>();
            try
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        servicos.Add
                        (
                            new ServicosModel()
                            {
                                id_servico = Convert.ToInt32(dr["id_servico"].ToString()),
                                nm_servico = "Serviço "+dr["id_servico"].ToString(),
                                status = dr["Status"].ToString(),
                                situacao = dr["situacao"].ToString(),
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
            return servicos;
        }

        public override void SetDados(DataGridView dgv)
        {
            if (dgv.CurrentRow != null)
            {
                if (dgv.CurrentRow.Cells["ID"].Value.ToString() != "")
                {
                    id_servico = Convert.ToInt32(dgv.CurrentRow.Cells["ID"].Value.ToString());
                    nm_servico = dgv.CurrentRow.Cells["Serviço"].Value.ToString();
                    situacao = dgv.CurrentRow.Cells["Situação"].Value.ToString();
                    status = dgv.CurrentRow.Cells["Status"].Value.ToString();
                    data_criacao = Convert.ToDateTime(dgv.CurrentRow.Cells["Data Criação"].Value.ToString());
                    data_atualizacao = Convert.ToDateTime(dgv.CurrentRow.Cells["Data Atualização"].Value.ToString());
                }
            }
        }
        public override void Clear()
        {
            id_servico = 0;
            nm_servico = "";
            situacao = "";
            status = "";
            data_criacao = DateTime.Now;
            data_atualizacao = DateTime.Now;

        }
        public override void GetListaCampos()
        {
            base.GetListaCampos();
            ListaCamposView.Clear();
            ListaCamposView.Add("ID");
            ListaCamposView.Add("Serviço");
            ListaCamposView.Add("Situação");
            ListaCamposView.Add("Status");
            ListaCamposView.Add("Data Criação");
            ListaCamposView.Add("Data Atualização");
        }

        public override string GetFieldParaBusca(string pCampoView)
        {
            string Result = "";
            switch (pCampoView)
            {
                case "ID": Result = "s.id_servico"; break;
                case "Serviço": Result = "s.nm_servico"; break;
                case "Situação": Result = "s.Situacao"; break;
                case "Status": Result = "s.status"; break;
                case "Data Criação": Result = "s.data_criacao"; break;
                case "Data Atualização": Result = "s.data_atualizacao"; break;
            }
            return Result;
        }

        public override string GetTypeFieldParaBusca(string pCampoView)
        {
            string Result = "";
            switch (pCampoView)
            {
                case "ID": Result = "integer"; break;
                case "Serviço": Result = "String"; break;
                case "Situação": Result = "String"; break;
                case "Status": Result = "String"; break;
                case "Data Criação": Result = "DateTime"; break;
                case "Data Atualização": Result = "DateTime"; break;
            }
            return Result;
        }
    }
}
