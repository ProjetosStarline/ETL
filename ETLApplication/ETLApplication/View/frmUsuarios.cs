using ETLApplication.Controller;
using ETLApplication.DAO;
using ETLApplication.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace ETLApplication.View
{
    public partial class frmUsuarios : ETLApplication.View.frmCadastroBase
    {
        
        CadastroBase<UsuariosModel> cadastroBase;
        public frmUsuarios()
        {
            InitializeComponent();
            
            cadastroBase = new CadastroBase<UsuariosModel>();
            HabilitaCampos();
        }
        public void HabilitaCampos()
        {
            edtIdUsuario.Enabled = !cadastroBase.FieldIsPK(usuariosModel, "Id_usuario");
        }

        public override void CriaClasseModel()
        {
            new frmPesquisa(usuariosModel).ShowDialog();
            MostrarNatela();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (cbxPerfil.Text.Length == 0)
            {
                MessageBox.Show("Favor informar o Perfil.");
                cbxPerfil.Focus();
                return;
            }

            if (edtNmUsuario.Text.Length == 0)
            {
                MessageBox.Show("Favor informar o Nome do Usuário.");
                edtNmUsuario.Focus();
                return;
            }

            if (edtLogin.Text.Length == 0)
            {
                MessageBox.Show("Favor informar o Login do Usuário.");
                edtLogin.Focus();
                return;
            }
            if (edtSenha.Text.Length == 0)
            {
                MessageBox.Show("Favor informar a Senha do Usuário.");
                edtSenha.Focus();
                return;
            }

            if (operacao==Operacao.oInsert && edtConfirma.Text.Length == 0)
            {
                MessageBox.Show("Favor informar a Confirmação da Senha do Usuário.");
                edtConfirma.Focus();
                return;
            }

            usuariosModel.Id_perfil = cadastroBase.GetidPerfil(cbxPerfil.Text);
            usuariosModel.Nm_usuario = edtNmUsuario.Text;
            usuariosModel.Status = cbStatus.Text;
            usuariosModel.Login = edtLogin.Text;
            if (operacao == Operacao.oInsert)
            {
                usuariosModel.Senha = Crypto.Encriptar(edtSenha.Text);
            }
            else
            {
                usuariosModel.Senha = edtSenha.Text;
            }
            usuariosModel.Data_criacao = Convert.ToDateTime(edtDataCriacao.Text);
            usuariosModel.Data_atualizacao = Convert.ToDateTime(edtDataAtualizacao.Text);
            usuariosModel.Email = edtEmail.Text;
            usuariosModel.RecebeNotificacao =ckbRecebeNotificacao.Checked?"SIM":"NAO";

            //string msgRetorno = "";
            if (operacao == Operacao.oInsert)
            {
                if (new UsuariosController().UsuarioCadastrado(edtLogin.Text))
                {
                    MessageBox.Show("Usuário já se encontra cadastrado, favor informar outro nome para Login.");
                    edtLogin.Focus();
                    return;
                }
                string Msg = "";
                if (edtSenha.Text == edtConfirma.Text)
                {
                    Msg = cadastroBase.PersisteNoBanco(usuariosModel, OperacaoToInt(operacao));
                    int idUsuarioCad = new UsuariosController().GetIdUsuario(usuariosModel.Nm_usuario);
                    if(new UsuariosController().GravaPermissaoAcessoFiliais(DgvUsoCateg, idUsuarioCad))
                    {
                        MessageBox.Show(Msg);
                        LimparTela();
                    }
                }
                else
                {
                    MessageBox.Show("A senha está diferente da confirmação, favor digitá-la novamente.");
                    operacao = Operacao.oInsert;
                    edtSenha.Focus();
                }
            }
            else
            {
                if (operacao == Operacao.oEdit)
                {
                    if(new UsuariosController().GravaPermissaoAcessoFiliais(DgvUsoCateg, usuariosModel.Id_usuario))
                    {
                        MessageBox.Show(cadastroBase.PersisteNoBanco(usuariosModel, OperacaoToInt(operacao)));
                    }
                }
                else
                {
                    MessageBox.Show(cadastroBase.PersisteNoBanco(usuariosModel, OperacaoToInt(operacao)));
                }
                LimparTela();
            }

            ControlaBotoes();
        }

        private void MostrarNatela()
        {
            edtIdUsuario.Text = usuariosModel.Id_usuario.ToString();
            cbxPerfil.Text = cadastroBase.GetNomePerfil(usuariosModel.Id_perfil);
            edtNmUsuario.Text = usuariosModel.Nm_usuario;
            edtLogin.Text = usuariosModel.Login;
            edtSenha.Text = usuariosModel.Senha;
            edtEmail.Text = usuariosModel.Email;
            cbStatus.Text = usuariosModel.Status;
            edtDataCriacao.Text = usuariosModel.Data_criacao.ToString();
            edtDataAtualizacao.Text = usuariosModel.Data_atualizacao.ToString();
            ckbRecebeNotificacao.Checked = usuariosModel.RecebeNotificacao == "SIM";
            new UsuariosController().MostrarFiliais(DgvUsoCateg, usuariosModel.Id_usuario);
        }

        public override void LimparTela()
        {
            edtIdUsuario.Clear();
            edtNmUsuario.Clear();
            edtLogin.Clear();
            edtSenha.Clear();
            edtConfirma.Clear();
            edtEmail.Clear();
            cbStatus.SelectedItem=0;
            edtDataCriacao.Text = Global.DateTimeToStr(Global.DateTimeNow);
            edtDataAtualizacao.Text = Global.DateTimeToStr(Global.DateTimeNow);
            //cbxPerfil.SelectedIndex = 0;
            cbxPerfil.Text = "";
            base.LimparTela();
            new UsuariosController().MostrarFiliais(DgvUsoCateg, 0);
            cbTodasFiliais.Checked=false;
            ckbRecebeNotificacao.Checked = false;

        }

        public override void ControlaBotoes()
        {
            base.ControlaBotoes();
            if (edtIdUsuario != null)
            {
                btnAlterar.Enabled = operacao != Operacao.oEdit && edtIdUsuario.Text.Length   != 0 && edtIdUsuario.Text!="0" && (Global.Login.Alterar || Global.UsuarioSuper); 
                btnExcluir.Enabled = operacao != Operacao.oDelete && edtIdUsuario.Text.Length != 0 && edtIdUsuario.Text != "0" && (Global.Login.Deletar || Global.UsuarioSuper);

                edtLogin.Enabled = operacao == Operacao.oInsert;
                edtSenha.Enabled = operacao == Operacao.oInsert;
                edtConfirma.Enabled = operacao == Operacao.oInsert;
            }
        }
        private void frmUsuarios_Load(object sender, EventArgs e)
        {
            CadastroBase<PerfisModel> cbPerfis = new CadastroBase<PerfisModel>();
            string select = cbPerfis.GetSelect(new PerfisModel());
            cbPerfis.GetLista(select,cbxPerfil);
            cbxPerfil.ValueMember = "id_perfil";
            cbxPerfil.DisplayMember = "nm_perfil";
            if (cbxPerfil.Items.Count > 0)
            {
                cbxPerfil.SelectedIndex = 0;
            }

            new UsuariosController().MostrarFiliais(DgvUsoCateg, 0);
           
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimparTela();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            ControlaBotoes();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            
        }

        private void edtSenha_TextChanged(object sender, EventArgs e)
        {
            lblSenhaCripto.Text = "";
            using (AesManaged aes = new AesManaged())
            {
                lblSenhaCripto.Text = Crypto.Encriptar(edtSenha.Text);
                lblTamanhoSenha.Text = lblSenhaCripto.Text.Length.ToString();
            }
        }

        private void cbTodasFiliais_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DgvUsoCateg.Rows.Count; i++)
            {
                DgvUsoCateg.Rows[i].Cells["Permitir"].Value = cbTodasFiliais.Checked ? "SIM" : "NÃO";
            }

        }
    }
   

}
