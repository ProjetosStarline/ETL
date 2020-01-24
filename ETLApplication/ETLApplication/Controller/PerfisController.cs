/*
	==============================================
	Criado por : Carlos R. S. Oliveira
	Criado em  : 20/05/2019
	==============================================
*/
using EtlConexao;
using ETLApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ETLApplication.Controller
{
    public class PerfisController
    {
        Conexao conexao = null;
        public CadastroBase<PerfisModel> CadPerfisBase;
        public PerfisController()
        {
            conexao = new Conexao();
            CadPerfisBase = new CadastroBase<PerfisModel>();
        }

        public bool ExistemFilhos(int pIdPerfil)
        {
            bool Result = false;
            string script = "select sum(tbl.qtd) qtd from(";
            script += $"    select count(*) qtd from tb_usuarios where id_perfil = {pIdPerfil.ToString()}";
            script += "    union all";
            script += $"    select count(*) qtd from tb_permissoes where id_perfil = {pIdPerfil.ToString()}";
            script += ") tbl";
            using (SqlDataReader dr = conexao.ExecutarSelect(script))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Result = Global.StrToInt(dr["qtd"].ToString()) >= 1;
                    }
                }
            }
            return Result;
        }

        public string GetSelect()
        {
            string script = "select id_perfil 'ID', nm_perfil 'Nome Perfil',";
            script += " status Status, data_criacao 'Data Criação',data_atualizacao 'Data Atualização'";
            script += " from tb_perfis ";
            script += " where 1 = 1";
            return script;

        }

    }
}
