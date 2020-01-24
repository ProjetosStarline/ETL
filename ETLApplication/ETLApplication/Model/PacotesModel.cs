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
    public class PacotesModel:GenericModel
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_pacote { get; set; }
        public int id_categoria { get; set; }
        public string nm_pacote { get; set; }
        public string descr_pacotes { get; set; }
        public string status { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public DateTime data_criacao { get; set; }
        public DateTime data_atualizacao { get; set; }
        public PacotesModel()
        {
            NomeTabela = "tb_pacotes";
        }

        public void SetDados(SqlDataReader dr)
        {
            try
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        id_pacote = Convert.ToInt32(dr["id_pacote"].ToString());
                        id_categoria = Convert.ToInt32(isNullResultZero(dr["id_categoria"].ToString()));
                        nm_pacote = dr["nm_pacote"].ToString();
                        descr_pacotes = dr["descr_pacotes"].ToString();
                        status = dr["Status"].ToString();
                        data_criacao = Convert.ToDateTime(dr["Data_criacao"].ToString());
                        data_atualizacao = Convert.ToDateTime(dr["Data_atualizacao"].ToString());
                    }
                }
            }
            finally
            {
                dr.Close();
            }

        }
        public List<PacotesModel> GetListaPermissoes(SqlDataReader dr)
        {
            List<PacotesModel> pacotes = new List<PacotesModel>();
            try
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        pacotes.Add
                        (
                            new PacotesModel()
                            {
                                id_pacote = Convert.ToInt32(dr["id_pacote"].ToString()),
                                id_categoria = Convert.ToInt32(dr["id_categoria"].ToString()),
                                nm_pacote = dr["nm_pacote"].ToString(),
                                descr_pacotes = dr["descr_pacotes"].ToString(),
                                status = dr["Status"].ToString(),
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
            return pacotes;
        }

        public override void SetDados(DataGridView dgv)
        {
            if (dgv.CurrentRow != null)
            {
                if (dgv.CurrentRow.Cells["ID"].Value.ToString() != "")
                {
                    id_pacote = Convert.ToInt32(dgv.CurrentRow.Cells["ID"].Value.ToString());
                    id_categoria = Convert.ToInt32(dgv.CurrentRow.Cells["ID Filial"].Value.ToString());
                    nm_pacote = dgv.CurrentRow.Cells["Pacote"].Value.ToString();
                    descr_pacotes = dgv.CurrentRow.Cells["Descrição"].Value.ToString();
                    status = dgv.CurrentRow.Cells["status"].Value.ToString();
                    data_criacao = Convert.ToDateTime(dgv.CurrentRow.Cells["Data Criação"].Value.ToString());
                    data_atualizacao = Convert.ToDateTime(dgv.CurrentRow.Cells["Data Atualização"].Value.ToString());
                }
            }
        }
        public override void Clear()
        {
            id_pacote = 0;
            id_categoria = 0;
            nm_pacote = "";
            descr_pacotes = "";
            status = "";
            data_criacao = DateTime.Now;
            data_atualizacao = DateTime.Now;

        }

        public override void GetListaCampos()
        {
            base.GetListaCampos();
            ListaCamposView.Clear();
            ListaCamposView.Add("ID Filial");
            ListaCamposView.Add("Filial");
            ListaCamposView.Add("ID");
            ListaCamposView.Add("Pacote");
            ListaCamposView.Add("Descrição");
            ListaCamposView.Add("Status");
            ListaCamposView.Add("Data Criação");
            ListaCamposView.Add("Data Atualização");
        }

        public override string GetFieldParaBusca(string pCampoView)
        {
            string Result = "";
            switch (pCampoView)
            {
                case "ID Filial": Result = "c.id_categoria"; break;
                case "Filial": Result = "c.nm_categoria"; break;
                case "ID": Result = "p.id_pacote"; break;
                case "Pacote": Result = "p.nm_pacote"; break;
                case "Descrição": Result = "p.descr_pacotes"; break;
                case "Status": Result = "p.status"; break;
                case "Data Criação": Result = "p.data_criacao"; break;
                case "Data Atualização": Result = "p.data_atualizacao"; break;
            }
            return Result;
        }

        public override string GetTypeFieldParaBusca(string pCampoView)
        {
            string Result = "";
            switch (pCampoView)
            {
                case "ID Filial": Result = "integer"; break;
                case "Filial": Result = "String"; break;
                case "ID": Result = "integer"; break;
                case "Pacote": Result = "String"; break;
                case "Descrição": Result = "String"; break;
                case "Status": Result = "String"; break;
                case "Data Criação": Result = "DateTime"; break;
                case "Data Atualização": Result = "DateTime"; break;
            }
            return Result;
        }
    }
}
