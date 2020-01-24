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
    public class PermissoesModel : GenericModel
    {
        public PermissoesModel()
        {
            NomeTabela = "tb_permissoes";
        }
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]// Esta linha informa que o campo nao pode ser incluso no script de Inclusao ou Alteracao
        public int Id_permissao { get; set; }
        [Required]
        public int Id_perfil { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]// Esta linha informa que o campo nao pode ser incluso no script de Inclusao ou Alteracao
        public string nm_perfil { get; set; }
        [Required]
        public int Id_objeto { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]// Esta linha informa que o campo nao pode ser incluso no script de Inclusao ou Alteracao
        public string nm_objeto { get; set; }
        [StringLength(32)]
        public string Permissao { get; set; }
        [StringLength(16)]
        public string Status { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]// Esta linha informa que o campo nao pode ser incluso no script de Inclusao ou Alteracao
        public DateTime Data_criacao { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]// Esta linha informa que o campo nao pode ser incluso no script de Inclusao ou Alteracao
        public DateTime Data_atualizacao { get; set; }

        public void SetDados(SqlDataReader dr)
        {
            try
            {
                Id_permissao = Convert.ToInt32(dr["Id_permissao"].ToString());
                Id_perfil = Convert.ToInt32(dr["Id_perfil"].ToString());
                Id_objeto = Convert.ToInt32(dr["Id_objeto"].ToString());
                Permissao = dr["Permissao"].ToString();
                Status = dr["Status"].ToString();
                Data_criacao = Convert.ToDateTime(dr["Data_criacao"].ToString());
                Data_atualizacao = Convert.ToDateTime(dr["Data_atualizacao"].ToString());
            }
            finally
            {
                dr.Close();
            }

        }
        public List<PermissoesModel> GetListaPermissoes(SqlDataReader dr)
        {
            List<PermissoesModel> permissoes = new List<PermissoesModel>();
            try
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        permissoes.Add
                        (
                            new PermissoesModel()
                            {
                                Id_permissao = Convert.ToInt32(dr["Id_permissao"].ToString()),
                                Id_perfil = Convert.ToInt32(dr["Id_perfil"].ToString()),
                                Id_objeto = Convert.ToInt32(dr["Id_objeto"].ToString()),
                                Permissao = dr["Permissao"].ToString(),
                                Status = dr["Status"].ToString(),
                                Data_criacao = Convert.ToDateTime(dr["Data_criacao"].ToString()),
                                Data_atualizacao = Convert.ToDateTime(dr["Data_atualizacao"].ToString())
                            }
                        );
                    }
                }
            }
            finally
            {
                dr.Close();
            }
            return permissoes;
        }
        public List<PermissoesModel> GetListaPermCompleta(SqlDataReader dr)
        {
            List<PermissoesModel> permissoes = new List<PermissoesModel>();
            try
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        permissoes.Add
                        (
                            new PermissoesModel()
                            {
                                Id_permissao = Convert.ToInt32(dr["Id_permissao"].ToString()),
                                Id_perfil = Convert.ToInt32(dr["Id_perfil"].ToString()),
                                nm_perfil = dr["nm_perfil"].ToString(),
                                Id_objeto = Convert.ToInt32(dr["Id_objeto"].ToString()),
                                nm_objeto = dr["nm_objeto"].ToString(),
                                Permissao = dr["Permissao"].ToString(),
                                Status = dr["Status"].ToString(),
                                Data_criacao = Convert.ToDateTime(dr["Data_criacao"].ToString()),
                                Data_atualizacao = Convert.ToDateTime(dr["Data_atualizacao"].ToString())
                            }
                        );
                    }
                }
            }
            finally
            {
                dr.Close();
            }
            return permissoes;
        }

        public override void SetDados(DataGridView dgv)
        {
            if (dgv.CurrentRow != null)
            {
                if (dgv.CurrentRow.Cells["ID"].Value.ToString() != "")
                {
                    Id_permissao = Convert.ToInt32(dgv.CurrentRow.Cells["ID"].Value.ToString());
                    Id_perfil = Convert.ToInt32(dgv.CurrentRow.Cells["ID Perfil"].Value.ToString());
                    Id_objeto = Convert.ToInt32(dgv.CurrentRow.Cells["ID Menu"].Value.ToString());
                    Permissao = ConvertSIM_NAO_TO_0_OU_1(dgv.CurrentRow.Cells["Pesquisar"].Value.ToString()) +
                                ConvertSIM_NAO_TO_0_OU_1(dgv.CurrentRow.Cells["Cadastrar"].Value.ToString()) +
                                ConvertSIM_NAO_TO_0_OU_1(dgv.CurrentRow.Cells["Alterar"].Value.ToString()) +
                                ConvertSIM_NAO_TO_0_OU_1(dgv.CurrentRow.Cells["Deletar"].Value.ToString()) +
                                ConvertSIM_NAO_TO_0_OU_1(dgv.CurrentRow.Cells["Visualizar"].Value.ToString());
                    Status = dgv.CurrentRow.Cells["Status"].Value.ToString();
                    Data_criacao = Convert.ToDateTime(dgv.CurrentRow.Cells["Data Criação"].Value.ToString());
                    Data_atualizacao = Convert.ToDateTime(dgv.CurrentRow.Cells["Data Atualização"].Value.ToString());
                }
            }
        }

        public string ConvertSIM_NAO_TO_0_OU_1(string pString)
        {
            if (string.IsNullOrEmpty(pString))
            {
                return "0|";
            }
            else
            {
                return pString.ToUpper() == "SIM" ? "1|" : "0|";
            }
        }
        public override void Clear()
        {
            Id_permissao = 0;
            Id_perfil = 0;
            Id_objeto = 0;
            Permissao = "";
            Status = "";
            Data_criacao = DateTime.Now;
            Data_atualizacao = DateTime.Now;

        }

        public override void GetListaCampos()
        {
            base.GetListaCampos();
            ListaCamposView.Clear();
            ListaCamposView.Add("ID");
            ListaCamposView.Add("ID Perfil");
            ListaCamposView.Add("Nome Perfil");
            ListaCamposView.Add("ID Menu");
            ListaCamposView.Add("Nome Menu");
            ListaCamposView.Add("Pesquisar");
            ListaCamposView.Add("Cadastrar");
            ListaCamposView.Add("Alterar");
            ListaCamposView.Add("Deletar");
            ListaCamposView.Add("Visualizar");
            ListaCamposView.Add("Status");
            ListaCamposView.Add("Data Criação");
            ListaCamposView.Add("Data Atualização");
        }

        public override string GetFieldParaBusca(string pCampoView)
        {
            string Result = "";
            switch (pCampoView)
            {
                case "ID"               : Result = "p.id_permissao"; break;
                case "ID Perfil"        : Result = "p.id_perfil"; break;
                case "Nome Perfil"      : Result = "pe.nm_perfil"; break;
                case "ID Menu"          : Result = "p.id_objeto"; break;
                case "Nome Menu"        : Result = "o.nm_objeto"; break;
                case "Pesquisar"        : Result = "substring(p.permissao,1,1)"; break;
                case "Cadastrar"        : Result = "substring(p.permissao,3,1)"; break;
                case "Alterar"          : Result = "substring(p.permissao,5,1)"; break;
                case "Deletar"          : Result = "substring(p.permissao,7,1)"; break;
                case "Visualizar"       : Result = "substring(p.permissao,9,1)"; break;
                case "Status"           : Result = "p.status"; break;
                case "Data Criação"     : Result = "p.data_criacao"; break;
                case "Data Atualização" : Result = "p.data_atualizacao"; break;
            }
            return Result;
        }

        public override string GetTypeFieldParaBusca(string pCampoView)
        {
            string Result = "";
            switch (pCampoView)
            {
                case "ID": Result = "integer"; break;
                case "ID Perfil": Result = "integer"; break;
                case "Nome Perfil": Result = "String"; break;
                case "ID Menu": Result = "integer"; break;
                case "Nome Menu": Result = "String"; break;
                case "Pesquisar": Result = "String"; break;
                case "Cadastrar": Result = "String"; break;
                case "Alterar": Result = "String"; break;
                case "Deletar": Result = "String"; break;
                case "Visualizar": Result = "String"; break;
                case "Status": Result = "String"; break;
                case "Data Criação": Result = "DateTime"; break;
                case "Data Atualização": Result = "DateTime"; break;
            }
            return Result;
        }
    }
}

