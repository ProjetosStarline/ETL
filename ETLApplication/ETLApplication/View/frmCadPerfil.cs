using ETLApplication.Controller;
using ETLApplication.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace ETLApplication.View
{
    public partial class frmCadPerfil : ETLApplication.View.frmCadastroBase
    {
        //CadastroBase<PerfisModel> cadastroBase;
        PerfisController perfisController;
        public frmCadPerfil()
        {
            InitializeComponent();
            //cadastroBase = new CadastroBase<PerfisModel>();
            perfisController = new PerfisController();
            HabilitaCampos();
        }

        public override void CriaClasseModel()
        {
            new frmPesquisa(perfisModel).ShowDialog();
            MostrarNatela();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (edtNmPerfil.Text.Length == 0)
            {
                MessageBox.Show("Favor informar o Nome do Perfil.");
                edtNmPerfil.Focus();
                return;
            }

            if (operacao == Operacao.oDelete)
            {
                if (perfisController.ExistemFilhos(Global.StrToInt(edtIdPerfil.Text)))
                {
                    MessageBox.Show("Antes de excluir este registro, é necessário excluir os Usuários e Permissões relacionados a este Perfil.");
                    return;
                }
            }

            perfisModel.Id_perfil = Convert.ToInt32(GetConteudoCampo(edtIdPerfil.Text)); 
            perfisModel.Nm_perfil = edtNmPerfil.Text;
            perfisModel.Status = cbStatus.Text;
            perfisModel.Data_criacao = Convert.ToDateTime(edtDataCriacao.Text);
            perfisModel.Data_atualizacao = Convert.ToDateTime(edtDataAtualizacao.Text);
            string msgRetorno=  perfisController.CadPerfisBase.PersisteNoBanco(perfisModel,OperacaoToInt(operacao));
            LimparTela();
            if (!Global.IsNullOrEmpty(msgRetorno))
            {
                MessageBox.Show(msgRetorno);
            }

            ControlaBotoes();
        }

        private void MostrarNatela()
        {
            edtIdPerfil.Text= perfisModel.Id_perfil.ToString();
            edtNmPerfil.Text  = perfisModel.Nm_perfil;
            cbStatus.SelectedIndex = perfisModel.Status=="ativo"?0:1;
            edtDataCriacao.Text= perfisModel.Data_criacao.ToString();
            edtDataAtualizacao.Text = perfisModel.Data_atualizacao.ToString();

        }

        public override void LimparTela()
        {
            edtIdPerfil.Clear();
            edtNmPerfil.Clear();
            cbStatus.SelectedIndex=0;
            edtDataCriacao.Text = Global.DateTimeToStr(Global.DateTimeNow);
            edtDataAtualizacao.Text = Global.DateTimeToStr(Global.DateTimeNow);
            base.LimparTela();
        }


        public void HabilitaCampos()
        {
            edtIdPerfil.Enabled = !perfisController.CadPerfisBase.FieldIsPK(perfisModel, "Id_Perfil");
        }

        public override void ControlaBotoes()
        {
            base.ControlaBotoes();
            if (edtIdPerfil != null)
            {
                btnAlterar.Enabled = operacao != Operacao.oEdit && edtIdPerfil.Text.Length   != 0 && edtIdPerfil.Text != "0" && (Global.Login.Alterar || Global.UsuarioSuper);
                btnExcluir.Enabled = operacao != Operacao.oDelete && edtIdPerfil.Text.Length != 0 && edtIdPerfil.Text != "0" && (Global.Login.Deletar || Global.UsuarioSuper);
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            ControlaBotoes();
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            LimparTela();
        }
    }
}
