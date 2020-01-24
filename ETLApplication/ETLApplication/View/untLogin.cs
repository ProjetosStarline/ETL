using ETLApplication.Controller;
using ETLApplication.DAO;
using ETLApplication.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ETLApplication
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Global.UsuarioSuper = false;
            edtUsuario.Clear();
            edtSenha.Clear();
            edtUsuario.Focus();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            
            UsuariosController usu = new UsuariosController();
            Global.UsuarioSuper = false;
            int Result = 0;
            UsuariosModel oUsuario = usu.UsuarioExiste(edtUsuario.Text);
            Result = oUsuario.Id_usuario;
            if (oUsuario.Status != "ativo")
            {
                Result = -1;
            }

            if (Result > 0)
            {
                //MessageBox.Show("Usuario cadastrado.");
                //string SenhaDcripto = Crypto.Decriptar(oUsuario.Senha);
                if (edtSenha.Text == oUsuario.Senha)
                {
                    Global.UsuarioLogado = usu.GetUsuario(Result);
                    frmPrincipal.LoginPermissao = usu.GetListaPermissoes(Result);
                    Close();
                }
                else
                {
                    MessageBox.Show("Usuário não cadastrado ou com usuário e senha incorreta.");
                    edtUsuario.Focus();
                }
            }
            else
            {
                if (Result == -1)
                {
                    MessageBox.Show("Usuário com status inativo.");
                }
                else
                {
                    MessageBox.Show("Usuário não cadastrado ou com usuário e senha incorreta.");
                }
                edtUsuario.Focus();
            }

        }
    }
}
