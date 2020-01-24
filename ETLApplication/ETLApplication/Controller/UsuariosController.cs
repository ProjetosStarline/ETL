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
using ETLApplication.DAO;
using System.Windows.Forms;

namespace ETLApplication.Controller
{
    public class UsuariosController
    {
        public Conexao conexao = new Conexao();
        public CadastroBase<UsuariosModel> CadUsuariosBase;
        public UsuariosController()
        {
            CadUsuariosBase = new CadastroBase<UsuariosModel>();

        }
        public int UsuarioExiste(string pLogin, string pSenha)
        {
            string select = $"select id_usuario,status from tb_usuarios where Login='{pLogin}' and Senha='{pSenha}'";
            //conexao.LimparParametros();
            //conexao.Parametros.Add(new SqlParameter() { ParameterName = "Login", Value = pLogin });
            //conexao.Parametros.Add(new SqlParameter() { ParameterName = "Senha", Value = pSenha });
            SqlDataReader dr = conexao.ExecutarSelect(select);
            if (dr.HasRows && dr.Read())
            {
                if (dr["id_usuario"].ToString() == "inativo")
                {
                    return -1;
                }
                else
                {
                    return Convert.ToInt32(dr["id_usuario"].ToString());
                }
            }
            return 0;
        }
        public bool UsuarioCadastrado(string pLogin)
        {
            bool Result=false;
            string select = $"select id_usuario from tb_usuarios where Login='{pLogin}' ";
            conexao.LimparParametros();
            conexao.Parametros.Add(new SqlParameter() { ParameterName = "Login", Value = pLogin });
            SqlDataReader dr = conexao.ExecutarSelect(select);
            if (dr.HasRows && dr.Read())
            {
                int id = Convert.ToInt16(Global.StrToInt(dr["id_usuario"].ToString()));
                Result= id>0;
            }
            return Result;
        }
        public UsuariosModel UsuarioExiste(string pLogin)
        {
            string select = $"select * from tb_usuarios where Login='{pLogin}' ";
            conexao.LimparParametros();
            conexao.Parametros.Add(new SqlParameter() { ParameterName = "Login", Value = pLogin });
            UsuariosModel usu = new UsuariosModel();
            SqlDataReader dr = conexao.ExecutarSelect(select);
            usu.SetDados(dr);
            return usu;
        }
        public List<LoginModel> GetListaPermissoes(int pIdUsuario)
        {
            string script = "";
            script += "select p.id_Perfil,p.nm_perfil,o.id_objeto idmenu,o.nm_objeto menu, pe.status, ";
            script += "(case when substring(pe.permissao,1,1)= '0' then 'Não' else 'Sim' end) Pesquisar,";
            script += "(case when substring(pe.permissao,3,1)= '0' then 'Não' else 'Sim' end) Cadastrar,";
            script += "(case when substring(pe.permissao,5,1)= '0' then 'Não' else 'Sim' end) Alterar,";
            script += "(case when substring(pe.permissao,7,1)= '0' then 'Não' else 'Sim' end) Deletar,";
            script += "(case when substring(pe.permissao,9,1)= '0' then 'Não' else 'Sim' end) Vizualizar";
            script += " from tb_perfis p, tb_usuarios u, tb_permissoes pe, tb_objetos o";
            script += " where p.id_perfil = u.id_perfil";
            script += "  and pe.id_perfil = p.id_perfil";
            script += "  and o.id_objeto = pe.id_objeto";
            script += "  and p.status = pe.status";
            script += "  and u.status = p.status";
            script += "  and u.status = 'ativo'";
            script += $"  and u.id_usuario={pIdUsuario.ToString()}";

            return new LoginModel().GetListaPermissoes(conexao.ExecutarSelect(script));
        }

        public LoginModel Permissao(List<LoginModel> pLst, string pMenu)
        {
            LoginModel login = new LoginModel();
            foreach (var lst in pLst)
            {
                if (lst.menu == pMenu)
                {
                    login.id_perfil = lst.id_perfil;
                    login.nm_perfil = lst.nm_perfil;
                    login.idmenu = lst.idmenu;
                    login.menu = lst.menu;
                    login.status = lst.status;
                    login.Pesquisar = lst.Pesquisar;
                    login.Cadastrar = lst.Cadastrar;
                    login.Alterar = lst.Alterar;
                    login.Deletar = lst.Deletar;
                    login.Vizualizar = lst.Vizualizar;
                    break;
                }
            }
            return login;

        }
        public int GetIdUsuario(string pNmUsuario)
        {
            int Result = 0;
            string script = $"select id_usuario from tb_usuarios where nm_usuario ='{pNmUsuario}' and status='ativo'";
            using (SqlDataReader dr = conexao.ExecutarSelect(script))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Result = Global.StrToInt(dr["id_usuario"].ToString());
                    }
                }
            }
            return Result;
        }

        public bool AlterarSenha(int pIdUsuario,string pNovaSenha)
        {
            bool Result = false;
            try
            {
                string script = $"Update tb_usuarios set senha='{Crypto.Encriptar(pNovaSenha)}' where id_usuario={pIdUsuario.ToString()}";
                CadUsuariosBase.conexao.ExecutarScript(script);
                Result = true;
            }
            catch (Exception ex)
            {
                Result = false;
                throw new Exception($"Erro ao alterar a senha do usuário. Motivo: [{ex.Message}]");
            }
            return Result;
        }

        public UsuariosModel GetUsuario(int pIdUsuario)
        {
            UsuariosModel usuario = new UsuariosModel();
            string script = $"Select * from tb_usuarios where id_usuario={pIdUsuario.ToString()} and status='ativo'";
            usuario.SetDados(CadUsuariosBase.conexao.ExecutarSelect(script));
            return usuario;
        }

        public void MostrarFiliais(DataGridView dgv, int pIdUsuario)
        {
            dgv.Rows.Clear();
            string script = $"select id_categoria,nm_categoria Filial, 'NÃO' permitir from tb_Categorias where id_categoria not in(select id_categoria from tb_usuariosXCategorias where id_usuario={pIdUsuario.ToString()})";
                   script+= " union all";
                   script+= $" select c.id_categoria,c.nm_categoria Filial, 'SIM' permitir from tb_Categorias c, tb_usuariosXCategorias uc where uc.id_categoria = c.id_categoria and uc.id_usuario = {pIdUsuario.ToString()}";
            using (SqlDataReader dr = conexao.ExecutarSelect(script))
            {
                int Count = 0;
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        dgv.Rows.Add();
                        dgv.Rows[Count].Cells["ID"].Value = Global.StrToInt(dr["id_categoria"].ToString());
                        dgv.Rows[Count].Cells["Filial"].Value = dr["Filial"].ToString();
                        dgv.Rows[Count].Cells["Permitir"].Value = dr["permitir"].ToString();
                        Count++;
                    }
                }
            }
        }
        public List<UsuXCategModel> GetFiliais(int pIdUsuario)
        {
            List<UsuXCategModel> Result = new List<UsuXCategModel>();
            string script = $"select 0 Id_UsuCateg,0 id_usuario,id_categoria,nm_categoria Filial, 'NÃO' permitir from tb_Categorias where id_categoria not in(select id_categoria from tb_usuariosXCategorias where id_usuario={pIdUsuario.ToString()})";
            script += " union all";
            script += $" select uc.Id_UsuCateg,uc.id_usuario,c.id_categoria,c.nm_categoria Filial, 'SIM' permitir from tb_Categorias c, tb_usuariosXCategorias uc where uc.id_categoria = c.id_categoria and uc.id_usuario = {pIdUsuario.ToString()}";
            using (SqlDataReader dr = conexao.ExecutarSelect(script))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Result.Add(
                            new UsuXCategModel() {
                                Id_UsuCateg = Global.StrToInt(dr["Id_UsuCateg"].ToString()),
                                Id_Usuario = Global.StrToInt(dr["Id_Usuario"].ToString()),
                                Id_Categoria = Global.StrToInt(dr["Id_Categoria"].ToString()),
                                Nm_Categoria = dr["Nm_Categoria"].ToString(),
                                Permitir = Global.StrToInt(dr["Permitir"].ToString())
                            }
                        );
                    }
                }
            }
            return Result;
        }

        public bool GravaPermissaoAcessoFiliais(DataGridView dgv, int pIdUsuario)
        {
            bool Result=false;
            string script=$"Delete from tb_UsuariosXCategorias where id_usuario=@idUsuario";
            conexao.LimparParametros();
            conexao.Parametros.Add(new SqlParameter() { ParameterName = "idUsuario", Value = pIdUsuario });
            try
            {
                conexao.ExecutarScript(script);
                for(int i=0;i< dgv.Rows.Count; i++)
                {
                    if (dgv.Rows[i].Cells["Permitir"].Value.ToString() == "SIM")
                    {
                        script = "insert into tb_UsuariosXCategorias(id_usuario,id_categoria) values(@idUsuario,@idCategoria)";
                        conexao.LimparParametros();
                        conexao.Parametros.Add(new SqlParameter() { ParameterName = "idUsuario", Value = pIdUsuario });
                        conexao.Parametros.Add(new SqlParameter() { ParameterName = "idCategoria", Value = dgv.Rows[i].Cells["ID"].Value });
                        conexao.ExecutarScript(script);
                    }
                }
                Result = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Erro ao permitir o acesso a Filial para o Usuário. MOtivo: {ex.Message}");
                Result = false;
            }   
            return Result;
        }

        public string GetSelect()
        {
            string script = "select p.id_perfil 'ID Perfil',p.nm_perfil 'Perfil', u.id_usuario 'ID', u.nm_usuario 'Usuário',u.Login,u.Senha,u.eMail 'eMail',";
            script += " u.status Status, u.data_criacao 'Data Criação',u.data_atualizacao 'Data Atualização',RecebeNotificacao 'Recebe Notificação'";
            script += " from tb_usuarios u, tb_perfis p ";
            script += " where p.id_perfil = u.id_perfil ";
            return script;

        }

        public bool UsuarioIsAdmin(int pIdUsuario)
        {
            bool Result = false;
            string script = $"Select p.id_perfil from tb_usuarios u,tb_perfis p where u.id_perfil=p.id_perfil and u.id_usuario={pIdUsuario.ToString()} and p.status='ativo' and upper(p.nm_perfil) like upper('ADMIN%')";
            using (SqlDataReader dr = conexao.ExecutarSelect(script))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Result = Global.StrToInt(dr["id_perfil"].ToString()) > 0;
                    }
                }
            }
            return Result;
        }

    }
}
