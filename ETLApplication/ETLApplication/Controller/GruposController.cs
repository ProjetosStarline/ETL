/*
	==============================================
	Criado por : Carlos R. S. Oliveira
	Criado em  : 20/05/2019
	==============================================
*/
using ETLApplication.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EtlConexao;

namespace ETLApplication.Controller
{
    public class GruposController
    {
        Conexao conexao = null;
        public CadastroBase<GruposModel> CadGruposBase;
        public GruposController()
        {
            conexao = new Conexao();
            CadGruposBase = new CadastroBase<GruposModel>();
        }

       public int GetIdGrupoDaCategoria(string pIdCategoria)
        {
            int Result = 0;
            string script = $"select id_grupo from tb_categorias where id_categoria ='{pIdCategoria}'";
            using (SqlDataReader dr = conexao.ExecutarSelect(script))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Result = Convert.ToInt32(CadGruposBase.isNullResultZero(dr["id_grupo"].ToString()));
                    }
                }
            }
            return Result;
        }

        public int GetIdGrupo(string pNomeGrupo)
        {
            int Result = 0;
            string script = $"Select id_grupo from tb_grupos where nm_grupo='{pNomeGrupo}'";
            using (SqlDataReader dr = conexao.ExecutarSelect(script))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Result = Convert.ToInt32(CadGruposBase.isNullResultZero(dr["id_grupo"].ToString()));
                    }
                }
            }
            return Result;
        }
        public string GetNmGrupo(int pIdGrupo)
        {
            string Result = "";
            string script = $"Select nm_grupo from tb_grupos where id_grupo='{pIdGrupo.ToString()}'";
            using (SqlDataReader dr = conexao.ExecutarSelect(script))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Result = CadGruposBase.isNullResultVazio(dr["nm_grupo"]);
                    }
                }
            }
            return Result;
        }

        public bool ExistemFilhos(int pIdGrupo)
        {
            bool Result = false;
            string script = $"select count(*) qtd from tb_categorias where id_grupo ={pIdGrupo.ToString()}";
            using (SqlDataReader dr = conexao.ExecutarSelect(script))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Result = Global.StrToInt(dr["qtd"].ToString())>=1;
                    }
                }
            }
            return Result;
        }

        public string GetSelectGrupos(int pIdUsuario)
        {
            string select = "select distinct g.id_grupo CodEmpresa,g.nm_grupo Empresa";
            select += "  from tb_Categorias c, tb_usuariosXCategorias uc, tb_grupos g";
            select += " where uc.id_categoria = c.id_categoria";
            select += "   and g.id_grupo = c.id_grupo";
            select += "   and g.status=c.status";
            select += "   and g.status='ativo'";
            select += $"  and uc.id_usuario = {pIdUsuario.ToString()}";
            return select;
        }

        public string GetSelect()
        {
            string script = "select distinct e.id_grupo 'ID',e.nm_grupo 'Empresa', e.Descr_Grupos 'Descrição',";
            script += " e.status Status, e.data_criacao 'Data Criação',e.data_atualizacao 'Data Atualização', e.Site_Empresa 'Site da Empresa',e.Logo_Empresa 'Logo da Empresa'";
            script += " from tb_grupos e,";
            script += " tb_categorias c, ";
            script += " tb_UsuariosXcategorias uc";
            script += " where c.id_grupo = e.id_grupo";
            script += "   and uc.id_categoria = c.id_categoria";
            script += $"  and uc.id_usuario = {Global.UsuarioLogado.Id_usuario.ToString()}";
            return script;

        }
        public string GetCondicaoGrupo(int pIdGrupo)
        {
            string script = $"  and uc.id_usuario = {Global.UsuarioLogado.Id_usuario}";
            script += $"  and e.id_grupo = {pIdGrupo.ToString()}";

            return script;

        }

        public bool PrivilegiosOk(int pIdGrupo)
        {
            bool Result = false;
            string script = "select distinct e.id_grupo 'ID'";
            script += " from tb_grupos e, tb_categorias c, tb_UsuariosXCategorias uc";
            script += " where c.id_grupo = e.id_grupo";
            script += "   and c.status = e.status";
            script += "   and c.status = 'ativo'";
            script += "   and uc.id_categoria = c.id_categoria";
            script += $"  and uc.id_usuario = {Global.UsuarioLogado.Id_usuario}";
            script += $"  and e.id_grupo = {pIdGrupo.ToString()}";
            using (SqlDataReader dr = conexao.ExecutarSelect(script))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Result = Global.StrToInt(dr["ID"].ToString())>0;
                    }
                }
            }
            return Result;

        }
    }
}
