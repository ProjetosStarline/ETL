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
    public class CategoriasModel : GenericModel
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_categoria { get; set; }
        public int id_grupo { get; set; }
        public string nm_categoria { get; set; }
        public string descr_categorias { get; set; }
        public string status { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public DateTime data_criacao { get; set; }
        public DateTime data_atualizacao { get; set; }

        public string Smtp_Host { get; set; }
        public int Smtp_Port { get; set; }
        public string Smtp_EnableSSL { get; set; }
        public string Smtp_eMail { get; set; }
        public string Smtp_Senha { get; set; }
        public string EnviarSoErros { get; set; }

        public CategoriasModel()
        {
            NomeTabela = "tb_categorias";
        }

        public void SetDados(SqlDataReader dr)
        {
            try
            {
                id_categoria = Convert.ToInt32(isNullResultVazio(dr["id_categoria"]));
                id_grupo = Convert.ToInt32(isNullResultVazio(dr["id_grupo"]));
                nm_categoria = isNullResultVazio(dr["nm_categoria"]);
                descr_categorias = isNullResultVazio(dr["descr_categorias"]);
                status = isNullResultVazio(dr["status"]);
                data_criacao = Convert.ToDateTime(dr["Data_criacao"].ToString());
                data_atualizacao = Convert.ToDateTime(dr["Data_atualizacao"].ToString());

                Smtp_Host = isNullResultVazio(dr["Smtp_Host"]);
                Smtp_Port = Global.StrToInt(dr["Smtp_Port"].ToString()); 
                Smtp_EnableSSL = isNullResultVazio(dr["Smtp_EnableSSL"]);
                Smtp_eMail = isNullResultVazio(dr["Smtp_eMail"]);
                Smtp_Senha = isNullResultVazio(dr["Smtp_Senha"]);
                EnviarSoErros = isNullResultVazio(dr["EnviarSoErros"]);
            }
            finally
            {
                dr.Close();
            }

        }
        public List<CategoriasModel> GetListaPermissoes(SqlDataReader dr)
        {
            List<CategoriasModel> categorias = new List<CategoriasModel>();
            try
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        categorias.Add
                        (
                            new CategoriasModel()
                            {
                                id_categoria = Convert.ToInt32(isNullResultVazio(dr["id_categoria"])),
                                id_grupo = Convert.ToInt32(isNullResultVazio(dr["id_grupo"])),
                                nm_categoria = isNullResultVazio(dr["nm_categoria"]),
                                descr_categorias = isNullResultVazio(dr["descr_categorias"]),
                                status = isNullResultVazio(dr["status"]),
                                data_criacao = Convert.ToDateTime(dr["Data_criacao"].ToString()),
                                data_atualizacao = Convert.ToDateTime(dr["Data_atualizacao"].ToString()),

                                Smtp_Host = isNullResultVazio(dr["Smtp_Host"]),
                                Smtp_Port = Global.StrToInt(dr["Smtp_Port"].ToString()),
                                Smtp_EnableSSL = isNullResultVazio(dr["Smtp_EnableSSL"]),
                                Smtp_eMail = isNullResultVazio(dr["Smtp_eMail"]),
                                Smtp_Senha = isNullResultVazio(dr["Smtp_Senha"]),
                                EnviarSoErros = isNullResultVazio(dr["EnviarSoErros"])
                            }
                        );
                    }
                }
            }
            finally
            {
                dr.Close();
            }
            return categorias;
        }

        public override void SetDados(DataGridView dgv)
        {
            if (dgv.CurrentRow != null)
            {
                if (dgv.CurrentRow.Cells["ID"].Value.ToString() != "")
                {
                    id_categoria = Convert.ToInt32(isNullResultZero(dgv.CurrentRow.Cells["ID"].Value.ToString()));
                    id_grupo = Convert.ToInt32(isNullResultZero(dgv.CurrentRow.Cells["ID Empresa"].Value.ToString()));
                    nm_categoria = isNullResultVazio(dgv.CurrentRow.Cells["Filial"].Value);
                    descr_categorias = isNullResultVazio(dgv.CurrentRow.Cells["Descrição"].Value);
                    status = isNullResultVazio(dgv.CurrentRow.Cells["status"].Value);
                    data_criacao = Convert.ToDateTime(dgv.CurrentRow.Cells["Data Criação"].Value.ToString());
                    data_atualizacao = Convert.ToDateTime(dgv.CurrentRow.Cells["Data Atualização"].Value.ToString());

                    Smtp_Host = isNullResultVazio(dgv.CurrentRow.Cells["Host"].Value.ToString());
                    Smtp_Port = Global.StrToInt(dgv.CurrentRow.Cells["Porta"].Value.ToString());
                    Smtp_EnableSSL = isNullResultVazio(dgv.CurrentRow.Cells["Habilita SSL"].Value.ToString());
                    Smtp_eMail = isNullResultVazio(dgv.CurrentRow.Cells["Usuário Smtp"].Value.ToString());
                    Smtp_Senha = isNullResultVazio(dgv.CurrentRow.Cells["Senha Smtp"].Value.ToString());
                    EnviarSoErros = isNullResultVazio(dgv.CurrentRow.Cells["Enviar Só Erros"].Value.ToString());
                }
            }
        }
        public override void Clear()
        {
            id_categoria = 0;
            id_grupo = 0;
            nm_categoria = "";
            descr_categorias = "";
            status = "";
            data_criacao = DateTime.Now;
            data_atualizacao = DateTime.Now;

            Smtp_Host = "";
            Smtp_Port = 587;
            Smtp_EnableSSL = "SIM";
            Smtp_eMail = "";
            Smtp_Senha = "";
            EnviarSoErros = "SIM";


        }

        public override void GetListaCampos()
        {
            base.GetListaCampos();
            ListaCamposView.Clear();
            ListaCamposView.Add("ID Empresa");
            ListaCamposView.Add("Empresa");
            ListaCamposView.Add("ID");
            ListaCamposView.Add("Filial");
            ListaCamposView.Add("Descrição");
            ListaCamposView.Add("Status");
            ListaCamposView.Add("Data Criação");
            ListaCamposView.Add("Data Atualização");

            ListaCamposView.Add("Host");
            ListaCamposView.Add("Porta");
            ListaCamposView.Add("Habilita SSL");
            ListaCamposView.Add("Usuário Smtp");
            ListaCamposView.Add("Senha Smtp");
            ListaCamposView.Add("Enviar Só Erros");
        }

        public override string GetFieldParaBusca(string pCampoView)
        {
            string Result = "";
            switch (pCampoView)
            {
                case "ID Empresa": Result = "e.id_grupo"; break;
                case "Empresa": Result = "e.nm_grupo"; break;
                case "ID": Result = "c.id_categoria"; break;
                case "Filial": Result = "c.nm_categoria"; break;
                case "Descrição": Result = "c.descr_categorias"; break;
                case "Status": Result = "c.status"; break;
                case "Data Criação": Result = "c.data_criacao"; break;
                case "Data Atualização": Result = "c.data_atualizacao"; break;

                case "Host": Result = "c.Host"; break;
                case "Porta": Result = "c.Porta"; break;
                case "Habilita SSL": Result = "c.Enable_SSL"; break;
                case "Usuário Smtp": Result = "c.Smtp_eMail"; break;
                case "Senha Smtp": Result = "c.Smtp_Senha"; break;
                case "Enviar Só Erros": Result = "c.EnviarSoErros"; break;

            }
            return Result;
        }

        public override string GetTypeFieldParaBusca(string pCampoView)
        {
            string Result = "";
            switch (pCampoView)
            {
                case "ID Empresa": Result = "integer"; break;
                case "Empresa": Result = "String"; break;
                case "ID": Result = "integer"; break;
                case "Filial": Result = "String"; break;
                case "Descrição": Result = "String"; break;
                case "Status": Result = "String"; break;
                case "Data Criação": Result = "String"; break;
                case "Data Atualização": Result = "String"; break;

                case "Host": Result = "String"; break;
                case "Porta": Result = "integer"; break;
                case "Habilita SSL": Result = "String"; break;
                case "Usuário Smtp": Result = "String"; break;
                case "Senha Smtp": Result = "String"; break;
                case "Enviar Só Erros": Result = "String"; break;

            }
            return Result;
        }
    }
}
