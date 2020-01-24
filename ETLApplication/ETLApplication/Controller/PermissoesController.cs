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
using System.Windows.Forms;

namespace ETLApplication.Controller
{
    public class PermissoesController
    {
        Conexao conexao = null;
        public CadastroBase<PermissoesModel> CadPermissoesBase;
        public PermissoesController()
        {
            conexao = new Conexao();
            CadPermissoesBase = new CadastroBase<PermissoesModel>();
        }


        public SqlDataReader GetObjetosMenu(int IdPerfil, int IdObjeto)
        {
            string script = new GeraCRUD<PermissoesModel>().GetSelectAll(new PermissoesModel());
            script += $" and id_perfil={IdPerfil.ToString()}";
            script += $" and id_Objeto={IdObjeto.ToString()}";
            return conexao.ExecutarSelect(script);
        }

        public void SetPermissoes(CheckBox obj, int IdPerfil, int IdObjeto)
        {
            if (obj.GetType().Name == "CheckBox")
            {
                bool _checked = false;
                List<PermissoesModel> lstPermissoes = new PermissoesModel().GetListaPermissoes(GetObjetosMenu(IdPerfil, IdObjeto));
                foreach (var lst in lstPermissoes)
                {
                    _checked = (lst.Id_perfil == IdPerfil &&
                               lst.Id_objeto == IdObjeto &&
                               lst.Permissao == obj.Text);
                }

                obj.Checked = _checked;
            }
        }

        public List<PermissoesModel> GetListPermCompleta(int id_permissoes)
        {
            string script = "select p.id_permissao,p.id_perfil, pe.nm_perfil,p.id_objeto, o.nm_objeto,p.permissao, p.status, p.data_criacao,p.data_atualizacao";
            script += " from tb_permissoes p, tb_objetos o, tb_perfis pe";
            script += " where p.id_perfil = pe.id_perfil";
            script += "  and p.id_objeto = o.id_objeto";
            script += $"  and p.id_permissao = {id_permissoes.ToString()}";
            List<PermissoesModel> lstPermissoes = new PermissoesModel().GetListaPermCompleta(conexao.ExecutarSelect(script));
            return lstPermissoes;
        }

        public int GetIdPermissao(int IdPerfil, int IdObjeto)
        {
            int Result = 0;
            string script = $"select * from tb_permissoes where id_perfil={IdPerfil.ToString()} and id_objeto={IdObjeto.ToString()}";
            using (SqlDataReader dr = conexao.ExecutarSelect(script))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Result = Convert.ToInt32(dr["id_permissao"].ToString());
                    }
                }
            }
            return Result;
        }
        public void MostraPermissoesDoBanco(int IdPerfil, int IdObjeto, CheckBox pPesquisar, CheckBox pCadastrar, CheckBox pAlterar, CheckBox pDeletar, CheckBox pVisualizar)
        {
            string _permissoes = "";
            List<PermissoesModel> lstPermissoes = new PermissoesModel().GetListaPermissoes(GetObjetosMenu(IdPerfil, IdObjeto));
            foreach (var lst in lstPermissoes)
            {
                _permissoes = lst.Permissao;
            }
            MostraPermNatela(_permissoes, pPesquisar, pCadastrar, pAlterar, pDeletar, pVisualizar);
        }

        public void MostraPermNatela(string Permissoes, CheckBox pPesquisar, CheckBox pCadastrar, CheckBox pAlterar, CheckBox pDeletar, CheckBox pVisualizar)
        {
            pPesquisar.Checked = false;
            pCadastrar.Checked = false;
            pAlterar.Checked = false;
            pDeletar.Checked = false;
            pVisualizar.Checked = false;
            if (Permissoes.IndexOf("|") > 0)
            {
                pPesquisar.Checked = Permissoes.Substring(0, Permissoes.IndexOf("|")) == "1";
                Permissoes = Permissoes.Remove(0, Permissoes.IndexOf("|") + 1);

                pCadastrar.Checked = Permissoes.Substring(0, Permissoes.IndexOf("|")) == "1";
                Permissoes = Permissoes.Remove(0, Permissoes.IndexOf("|") + 1);

                pAlterar.Checked = Permissoes.Substring(0, Permissoes.IndexOf("|")) == "1";
                Permissoes = Permissoes.Remove(0, Permissoes.IndexOf("|") + 1);

                pDeletar.Checked = Permissoes.Substring(0, Permissoes.IndexOf("|")) == "1";
                Permissoes = Permissoes.Remove(0, Permissoes.IndexOf("|") + 1);

                pVisualizar.Checked = Permissoes.Substring(0, Permissoes.IndexOf("|")) == "1";
            }

        }

        public string GetSelect()
        {
            string script = "select p.id_permissao ID,p.id_perfil 'ID Perfil', pe.nm_perfil 'Nome Perfil',p.id_objeto 'ID Menu', o.nm_objeto 'Nome Menu',";

            script += "(case when substring(p.permissao,1,1)= '0' then 'Não' else 'Sim' end) Pesquisar ,";
            script += "(case when substring(p.permissao,3,1)= '0' then 'Não' else 'Sim' end) Cadastrar ,";
            script += "(case when substring(p.permissao,5,1)= '0' then 'Não' else 'Sim' end) Alterar   ,";
            script += "(case when substring(p.permissao,7,1)= '0' then 'Não' else 'Sim' end) Deletar   ,";
            script += "(case when substring(p.permissao,9,1)= '0' then 'Não' else 'Sim' end) Visualizar";

            script += ", p.status Status, p.data_criacao 'Data Criação',p.data_atualizacao 'Data Atualização'";
            script += " from tb_permissoes p, tb_objetos o, tb_perfis pe";
            script += " where p.id_perfil = pe.id_perfil";
            script += "  and p.id_objeto = o.id_objeto";
            return script;
        }


    }
}
