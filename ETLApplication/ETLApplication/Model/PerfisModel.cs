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
    public class PerfisModel: GenericModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]// Esta linha informa que o campo nao pode ser incluso no script de Inclusao ou Alteracao
        [Required]
        public int Id_perfil {get;set;}
        [StringLength(64)]
        public string Nm_perfil { get; set; }
        [StringLength(16)]
        public string Status { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]// Esta linha informa que o campo nao pode ser incluso no script de Inclusao ou Alteracao
        public DateTime Data_criacao { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]// Esta linha informa que o campo nao pode ser incluso no script de Inclusao ou Alteracao
        public DateTime Data_atualizacao { get; set; }
        //public string NomeTabela { get; set; }
        
        public PerfisModel()
        {
            NomeTabela = "tb_perfis";
        }
        public override void SetDados(SqlDataReader dr)
        {
            if (dr.HasRows && dr.Read())
            {
                Id_perfil   = Convert.ToInt32(dr["id_perfil"].ToString());
                Nm_perfil   = dr["nm_perfil"].ToString();
                Status      = dr["status"].ToString();
                Data_criacao = Convert.ToDateTime( dr["data_criacao"].ToString());
                Data_atualizacao = Convert.ToDateTime( dr["data_atualizacao"].ToString());
            }
            dr.Close();
        }
        public override void SetDados(DataGridView dgv)
        {
            if (dgv.CurrentRow != null)
            {
                if (dgv.CurrentRow.Cells["ID"].Value.ToString() != "")
                {
                    Id_perfil = Convert.ToInt32(dgv.CurrentRow.Cells["ID"].Value.ToString());
                    Nm_perfil = dgv.CurrentRow.Cells["Nome Perfil"].Value.ToString();
                    Status = dgv.CurrentRow.Cells["Status"].Value.ToString();
                    Data_criacao = Convert.ToDateTime(dgv.CurrentRow.Cells["Data Criação"].Value.ToString());
                    Data_atualizacao = Convert.ToDateTime(dgv.CurrentRow.Cells["Data Atualização"].Value.ToString());
                }
            }
        }
        public override void Clear()
        {
            Id_perfil = 0;
            Nm_perfil = "";
            Status = "";
            Data_criacao = DateTime.Now;
            Data_atualizacao = DateTime.Now;

        }
        public override void GetListaCampos()
        {
            base.GetListaCampos();
            ListaCamposView.Clear();
            ListaCamposView.Add("ID");
            ListaCamposView.Add("Nome Perfil");
            ListaCamposView.Add("Status");
            ListaCamposView.Add("Data Criação");
            ListaCamposView.Add("Data Atualização");
        }

        public override string GetFieldParaBusca(string pCampoView)
        {
            string Result = "";
            switch (pCampoView)
            {
                case "ID": Result = "id_perfil"; break;
                case "Nome Perfil": Result = "nm_perfil"; break;
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
                case "Nome Perfil": Result = "String"; break;
                case "Status": Result = "String"; break;
                case "Data Criação": Result = "DateTime"; break;
                case "Data Atualização": Result = "DateTime"; break;
            }
            return Result;
        }
    }
}
