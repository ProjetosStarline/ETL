/*
	==============================================
	Criado por : Carlos R. S. Oliveira
	Criado em  : 20/05/2019
	==============================================
*/
using ETLApplication.Controller.DAO;
using EtlConexao;
using ETLApplication.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETLApplication.Controller
{
    class ObjetosController
    {
        public Conexao conexao = new Conexao();
        public CadastroBase<ObjetosModel> CadObjetosBase;
        public ObjetosController()
        {
            CadObjetosBase = new CadastroBase<ObjetosModel>();
        }

        public SqlDataReader GetObjetosMenu()
        {
            return conexao.ExecutarSelect(new GeraCRUD<ObjetosModel>().GetSelectAll(new ObjetosModel()));
        }

        public int GetIdMenu(string pNmObjeto)
        {
            int Result = 0;
            string script = $"select id_objeto from tb_objetos where nm_objeto='{pNmObjeto.ToString()}'";
            using (SqlDataReader dr = conexao.ExecutarSelect(script))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Result = Convert.ToInt32(dr["id_objeto"].ToString());
                    }
                }
            }
            return Result;
        }

        public string GetNmMenu(string pIdObjeto)
        {
            string Result = "";
            string script = $"select nm_objeto from tb_objetos where id_objeto={pIdObjeto.ToString()}";
            using (SqlDataReader dr = conexao.ExecutarSelect(script))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Result = dr["nm_objeto"].ToString();
                    }
                }
            }
            return Result;
        }

        public void CadastrarMenu(string pMenu)
        {
            if (GetIdMenu(pMenu) == 0)
            {
                ObjetosModel Menu = new ObjetosModel();
                Menu.Nm_objeto = pMenu;
                Menu.Status = "ativo";
                Menu.Tp_objeto = "Menu";
                Menu.Data_atualizacao = DateTime.Now;
                CadObjetosBase.PersisteNoBanco(Menu, 1);
            }
        }
    }
}
