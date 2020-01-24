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

namespace ETLApplication.View
{
    public partial class frmAlterarSenha : Form
    {
        UsuariosController usuarioController;
        public frmAlterarSenha()
        {
            InitializeComponent();
            usuarioController = new UsuariosController();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimparTela();
        }

        public void LimparTela()
        {
            edtSenhaAtual.Clear();
            edtSenha.Clear();
            edtConfirma.Clear();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {

            if (AlterarSenha())
            {
                LimparTela();
                Close();
            }
        }

        public bool AlterarSenha()
        {
            bool Result = false;
            UsuariosModel oUsuario = usuarioController.UsuarioExiste(Global.UsuarioLogado.Login);
            string SenhaDecripto = oUsuario.Senha;

            if (oUsuario.Id_usuario > 0)
            {
                if (edtSenha.Text == edtConfirma.Text)
                {
                    if (edtSenhaAtual.Text == SenhaDecripto)
                    {
                        try
                        {
                            usuarioController.AlterarSenha(oUsuario.Id_usuario, edtSenha.Text);
                            MessageBox.Show("Senha Alterada com sucesso.");
                            Result = true;
                            new frmLogin().ShowDialog();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Erro ao alterar a senha. Motivo: {ex.Message}");
                        }
                    }
                    else
                    {
                        MessageBox.Show($"A senha está incorreta, favor digitá-la novamente.");
                        edtSenha.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("A senha está diferente da confirmação, favor digitar novamente.");
                    edtConfirma.Focus();
                }
            }
            return Result;
        }
    }
}
