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

namespace ETLApplication.Model
{
    public class LoginModel
    {
        public int id_perfil { get; set; }
        public string nm_perfil { get; set; }
        public int idmenu { get; set; }
        public string menu { get; set; }
        public string status { get; set; }
        public bool Pesquisar { get; set; }
        public bool Cadastrar { get; set; }
        public bool Alterar { get; set; }
        public bool Deletar { get; set; }
        public bool Vizualizar { get; set; }

        public void SetDados(SqlDataReader dr)
        {
            try
            {

                id_perfil = Convert.ToInt32(dr["id_perfil"].ToString());
                nm_perfil = dr["nm_perfil"].ToString();
                idmenu = Convert.ToInt32(dr["idmenu"].ToString());
                menu = dr["menu"].ToString();
                status = dr["status"].ToString();
                Pesquisar = dr["Pesquisar"].ToString()=="Sim";
                Cadastrar = dr["Cadastrar"].ToString() == "Sim";
                Alterar = dr["Alterar"].ToString() == "Sim";
                Deletar = dr["Deletar"].ToString() == "Sim";
                Vizualizar = dr["Vizualizar"].ToString() == "Sim";

            }
            finally
            {
                dr.Close();
            }

        }
        public List<LoginModel> GetListaPermissoes(SqlDataReader dr)
        {
            List<LoginModel> permissoes = new List<LoginModel>();
            try
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        permissoes.Add
                        (
                            new LoginModel()
                            {
                                id_perfil = Convert.ToInt32(dr["id_perfil"].ToString()),
                                nm_perfil = dr["nm_perfil"].ToString(),
                                idmenu = Convert.ToInt32(dr["idmenu"].ToString()),
                                menu = dr["menu"].ToString(),
                                status = dr["status"].ToString(),
                                Pesquisar = dr["Pesquisar"].ToString() == "Sim",
                                Cadastrar = dr["Cadastrar"].ToString() == "Sim",
                                Alterar = dr["Alterar"].ToString() == "Sim",
                                Deletar = dr["Deletar"].ToString() == "Sim",
                                Vizualizar = dr["Vizualizar"].ToString() == "Sim"
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

    }
}
