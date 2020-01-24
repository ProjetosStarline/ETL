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
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ETLApplication.Controller
{
    class CategoriasController
    {
        Conexao conexao = null;
        ParametrosController Parametros;
        public CadastroBase<CategoriasModel> CadCategoriasBase;

        public CategoriasController()
        {
            conexao = new Conexao();
            CadCategoriasBase = new CadastroBase<CategoriasModel>();
            Parametros = new ParametrosController();
        }

        public int GetIdGrupo(string pNomeGrupo)
        {
            int Result = 0;
            string script = $"Select id_grupo from tb_grupos where nm_grupo='{pNomeGrupo}' and status='ativo'";
            using (SqlDataReader dr=conexao.ExecutarSelect(script))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Result = Convert.ToInt32(CadCategoriasBase.isNullResultZero(dr["id_grupo"].ToString()));
                    }
                }
            }
            return Result;
        }
        public int GetIdGrupo(int pIdCategoria)
        {
            int Result = 0;
            string script = $"Select distinct id_grupo from tb_categorias where id_categoria='{pIdCategoria.ToString()}' and status='ativo'";
            using (SqlDataReader dr = conexao.ExecutarSelect(script))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Result = Convert.ToInt32(CadCategoriasBase.isNullResultZero(dr["id_grupo"].ToString()));
                    }
                }
            }
            return Result;
        }

        public string GetNmGrupo(int pIdGrupo)
        {
            string Result = "";
            string script = $"Select nm_grupo from tb_grupos where id_grupo='{pIdGrupo.ToString()}' and status='ativo'";
            using (SqlDataReader dr = conexao.ExecutarSelect(script))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Result = CadCategoriasBase.isNullResultVazio(dr["nm_grupo"]);
                    }
                }
            }
            return Result;
        }

        public int GetIdCategoria(string pNomeCategoria)
        {
            int Result = 0;
            string script = $"Select id_categoria from tb_categorias where nm_categoria='{pNomeCategoria}'";
            using (SqlDataReader dr = conexao.ExecutarSelect(script))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Result = Convert.ToInt32(CadCategoriasBase.isNullResultZero(dr["id_categoria"].ToString()));
                    }
                }
            }
            return Result;
        }
        public string GetNmCategoria(int pIdCategoria)
        {
            string Result = "";
            string script = $"Select nm_categoria from tb_categorias where id_categoria={pIdCategoria.ToString()} and status='ativo'";
            using (SqlDataReader dr = conexao.ExecutarSelect(script))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Result = CadCategoriasBase.isNullResultVazio(dr["nm_categoria"]);
                    }
                }
            }
            return Result;
        }

        public string GetNmCategoria(int pIdCategoria, int pIdGrupo)
        {
            string Result = "";
            string script = $"Select nm_categoria from tb_categorias where id_categoria={pIdCategoria.ToString()} and id_grupo={pIdGrupo.ToString()} and status='ativo'";
            using (SqlDataReader dr = conexao.ExecutarSelect(script))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Result = CadCategoriasBase.isNullResultVazio(dr["nm_categoria"]);
                    }
                }
            }
            return Result;
        }
        public int GetIdCategoriaDoPacote(int pIdPacote)
        {
            int Result = 0;
            string script = $"Select distinct id_categoria from tb_pacotes where id_pacote='{pIdPacote.ToString()}' and status='ativo'";
            using (SqlDataReader dr = conexao.ExecutarSelect(script))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Result = Convert.ToInt32(CadCategoriasBase.isNullResultZero(dr["id_categoria"].ToString()));
                    }
                }
            }
            return Result;
        }

        public void PreencheCombo(ComboBox pComboBox)
        {
            string select = CadCategoriasBase.GetSelect(new CategoriasModel()) + $" and id_grupo={Parametros.GetValorParametro("Empresa_Selecionada").ToString()} and status='ativo'";
            CadCategoriasBase.GetLista(select, pComboBox);
            pComboBox.ValueMember = "id_categoria";
            pComboBox.DisplayMember = "nm_categoria";
            if (pComboBox.Items.Count > 0)
            {
                pComboBox.SelectedIndex = 0;
            }
        }
        public bool ExistemFilhos(int pIdCategoria)
        {
            bool Result = false;
            string script = $"select count(*) qtd from tb_pacotes where id_categoria ={pIdCategoria.ToString()}";
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

        public bool ExistemFilhosUsuariosXCategorias(int pIdCategoria)
        {
            bool Result = false;
            string script = $"select count(*) qtd from tb_UsuariosXCategorias where id_categoria ={pIdCategoria.ToString()}";
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

        public bool TemAcessoParaEstaFilial(int pidCategoria,int pidUsuario)
        {
            string select = "select distinct g.id_grupo";
            select += " from tb_Categorias c, tb_usuariosXCategorias uc, tb_grupos g";
            select += " where uc.id_categoria = c.id_categoria";
            select += "   and g.id_grupo = c.id_grupo";
            select += "   and g.status = 'ativo'";
            select += $"  and c.id_categoria = {pidCategoria.ToString()}";
            select += $"  and uc.id_usuario = {pidUsuario.ToString()}";

            bool Result = false;
            using (SqlDataReader dr = conexao.ExecutarSelect(select))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Result = Global.StrToInt(dr["id_grupo"].ToString()) >= 1;
                    }
                }
            }
            return Result;

        }

        public string GetSelect()
        {
            string script = "select e.id_grupo 'ID Empresa', e.nm_grupo 'Empresa',c.Id_categoria 'ID',c.nm_categoria 'Filial',c.descr_categorias 'Descrição',";
            script += " c.Status, c.data_criacao 'Data Criação',c.data_atualizacao 'Data Atualização',";
            script += " c.Smtp_Host Host, c.Smtp_Port 'Porta',c.Smtp_EnableSSL 'Habilita SSL',c.Smtp_eMail 'Usuário Smtp',c.Smtp_Senha 'Senha Smtp',c.EnviarSoErros 'Enviar Só Erros'";
            script += " from tb_grupos e,";
            script += " tb_categorias c, ";
            script += " tb_UsuariosXcategorias uc";
            script += " where c.id_grupo = e.id_grupo";
            script += "   and uc.id_categoria = c.id_categoria";
            script += $"  and uc.id_usuario = {Global.UsuarioLogado.Id_usuario.ToString()}";
            return script;

        }

        public string GetFiliasDaEmpresa(int pIdGrupo)
        {
            string script = "select c.Id_categoria,c.nm_categoria";
            script += " from tb_categorias c, tb_grupos e ,tb_UsuariosXCategorias uc";
            script += " where c.id_grupo = e.id_grupo";
            script += $"  and c.id_grupo=e.id_grupo ";
            script += $"  and uc.id_categoria=c.id_categoria ";
            script += $"  and c.id_grupo = {pIdGrupo.ToString()}";
            script += $"  and uc.id_usuario={Global.UsuarioLogado.Id_usuario}";

            return script;

        }


        public string GetCondicaoCategoria(int pIdGrupo)
        {
            string script = $"  and uc.id_usuario = {Global.UsuarioLogado.Id_usuario}";
            script += $"  and e.id_grupo = {pIdGrupo.ToString()}";

            return script;

        }

        public bool PrivilegiosOk(int pIdCategoria)
        {
            bool Result = false;
            string script = "select distinct c.id_categoria 'ID'";
            script += " from tb_grupos e, tb_categorias c, tb_UsuariosXCategorias uc";
            script += " where c.id_grupo = e.id_grupo";
            script += "   and c.status = e.status";
            script += "   and c.status = 'ativo'";
            script += "   and uc.id_categoria = c.id_categoria";
            script += $"  and uc.id_usuario = {Global.UsuarioLogado.Id_usuario}";
            script += $"  and c.id_categoria = {pIdCategoria.ToString()}";
            using (SqlDataReader dr = conexao.ExecutarSelect(script))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Result = Global.StrToInt(dr["ID"].ToString()) > 0;
                        break;
                    }
                }
            }
            return Result;

        }

        public bool DeletaUsuariosXCategorias(int pIdCategoria)
        {
            bool Result = false;
            string script = $"Delete from tb_UsuariosXCategorias where id_categoria ={pIdCategoria.ToString()}";
            try
            {
                conexao.ExecutarScript(script);
                Result = true;
            }
            catch
            {
                Result = false;
            }
            return Result;
        }


    }
}
