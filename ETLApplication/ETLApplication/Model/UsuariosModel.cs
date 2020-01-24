/*
	==============================================
	Criado por : Carlos R. S. Oliveira
	Criado em  : 20/05/2019
	==============================================
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Forms;
using ETLApplication.DAO;

namespace ETLApplication.Model
{
    public class UsuariosModel:GenericModel
    {
        public UsuariosModel()
        {
            NomeTabela = "tb_usuarios";
        }
        [Key]//sempre o primeiro atributo
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]// Esta linha informa que o campo nao pode ser incluso no script de Inclusao ou Alteracao e deve ser o segundo atributo sempre
        [Required]
        public int Id_usuario { get; set; }
        public int Id_perfil { get; set; }
        [StringLength(128)]
        public string Nm_usuario { get; set; }
        [StringLength(32)]
        public string Login { get; set; }
        [StringLength(32)]
        public string Senha { get; set; }
        [StringLength(64)]
        public string Email { get; set; }
        [StringLength(16)]
        public string Status { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]// Esta linha informa que o campo nao pode ser incluso no script de Inclusao ou Alteracao
        public DateTime Data_criacao { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]// Esta linha informa que o campo nao pode ser incluso no script de Inclusao ou Alteracao
        public DateTime Data_atualizacao { get; set; }
        public string RecebeNotificacao { get; set; }

        public void SetDados(SqlDataReader dr)
        {
            try
            {
                if (dr.HasRows && dr.Read())
                {
                    Id_usuario = Convert.ToInt32(dr["Id_usuario"].ToString());
                    Id_perfil = Convert.ToInt32(dr["Id_perfil"].ToString());
                    Nm_usuario = dr["Nm_usuario"].ToString();
                    Login = dr["Login"].ToString();
                    Senha = Crypto.Decriptar(dr["Senha"].ToString());
                    Email = dr["Email"].ToString();
                    Status = dr["Status"].ToString();
                    Data_criacao = Convert.ToDateTime(dr["Data_criacao"].ToString());
                    Data_atualizacao = Convert.ToDateTime(dr["Data_atualizacao"].ToString());
                    RecebeNotificacao= dr["RecebeNotificacao"].ToString();
                }
            }
            finally
            {
                dr.Close();
            }

        }
        public override void SetDados(DataGridView dgv)
        {
            if (dgv.CurrentRow != null)
            {
                if (dgv.CurrentRow.Cells["ID"].Value.ToString() != "")
                {
                    Id_usuario = Convert.ToInt32(dgv.CurrentRow.Cells["ID"].Value.ToString());
                    Id_perfil = Convert.ToInt32(dgv.CurrentRow.Cells["ID Perfil"].Value.ToString());
                    Nm_usuario = dgv.CurrentRow.Cells["Usuário"].Value.ToString();
                    Login = dgv.CurrentRow.Cells["Login"].Value.ToString();
                    Senha = dgv.CurrentRow.Cells["Senha"].Value.ToString();
                    Email = dgv.CurrentRow.Cells["eMail"].Value.ToString();
                    Status = dgv.CurrentRow.Cells["Status"].Value.ToString();
                    Data_criacao = Convert.ToDateTime(dgv.CurrentRow.Cells["Data Criação"].Value.ToString());
                    Data_atualizacao = Convert.ToDateTime(dgv.CurrentRow.Cells["Data Atualização"].Value.ToString());
                    RecebeNotificacao= dgv.CurrentRow.Cells["Recebe Notificação"].Value.ToString();
                }
            }
        }
        public override void Clear()
        {
            Id_usuario = 0;
            Id_perfil = 0;
            Nm_usuario = "";
            Login = "";
            Senha = "";
            Email = "";
            Status = "";
            Data_criacao = DateTime.Now;
            Data_atualizacao = DateTime.Now;
            RecebeNotificacao = "";
        }

        public override void GetListaCampos()
        {
            base.GetListaCampos();
            ListaCamposView.Clear();
            ListaCamposView.Add("ID Perfil");
            ListaCamposView.Add("Perfil");
            ListaCamposView.Add("ID");
            ListaCamposView.Add("Usuário");
            ListaCamposView.Add("Login");
            ListaCamposView.Add("Senha");
            ListaCamposView.Add("eMail");
            ListaCamposView.Add("Status");
            ListaCamposView.Add("Data Criação");
            ListaCamposView.Add("Data Atualização");
            ListaCamposView.Add("Recebe Notificação");
        }

        public override string GetFieldParaBusca(string pCampoView)
        {
            string Result = "";
            switch (pCampoView)
            {
                case "ID Perfil": Result = "p.id_perfil"; break;
                case "Perfil": Result = "p.nm_perfil"; break;
                case "ID": Result = "u.id_usuario"; break;
                case "Usuário": Result = "u.nm_usuario"; break;
                case "Login": Result = "u.Login"; break;
                case "Senha": Result = "u.Senha"; break;
                case "eMail": Result = "u.Email"; break;
                case "Status": Result = "u.status"; break;
                case "Data Criação": Result = "u.data_criacao"; break;
                case "Data Atualização": Result = "u.data_atualizacao"; break;
                case "Recebe Notificação":Result = "u.RecebeNotificacao"; break;
            }
            return Result;
        }

        public override string GetTypeFieldParaBusca(string pCampoView)
        {
            string Result = "";
            switch (pCampoView)
            {
                case "ID Perfil": Result = "integer"; break;
                case "Perfil": Result = "String"; break;
                case "ID": Result = "integer"; break;
                case "Usuário": Result = "String"; break;
                case "Login": Result = "String"; break;
                case "Senha": Result = "String"; break;
                case "eMail": Result = "String"; break;
                case "Status": Result = "String"; break;
                case "Data Criação": Result = "DateTime"; break;
                case "Data Atualização": Result = "DateTime"; break;
                case "Recebe Notificação":Result = "String"; break;
            }
            return Result;
        }
    }
}
