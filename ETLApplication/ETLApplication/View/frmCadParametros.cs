using ETLApplication.Controller;
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
    public partial class frmCadParametros : ETLApplication.View.frmCadastroBase
    {
        ParametrosController ParametrosController;
        public frmCadParametros()
        {
            InitializeComponent();
            ParametrosController = new ParametrosController();
            HabilitaCampos();

        }
        private void FrmCadParametros_Load(object sender, EventArgs e)
        {

        }

        public void HabilitaCampos()
        {
            edtCodigo.Enabled = !ParametrosController.CadParametrosBase.FieldIsPK(ParametrosModel, "id_parametro");
            edtNome.Enabled = operacao!=Operacao.oEdit;
        }

        public override void ControlaBotoes()
        {
            base.ControlaBotoes();
            if (edtCodigo != null)
            {
                btnAlterar.Enabled = operacao != Operacao.oEdit && edtCodigo.Text.Length != 0 && edtCodigo.Text != "0" && (Global.Login.Alterar || Global.UsuarioSuper);
                btnExcluir.Enabled = operacao != Operacao.oDelete && edtCodigo.Text.Length != 0 && edtCodigo.Text != "0" && (Global.Login.Deletar || Global.UsuarioSuper);
            }

            
        }

        public override void LimparTela()
        {
            edtCodigo.Clear();
            if (cbStatus.Items.Count>0) { cbStatus.SelectedIndex = 0; }
            edtNome.Clear();
            edtValor.Clear();
            edtDataCriacao.Text = Global.DateTimeToStr(Global.DateTimeNow);
            edtDataAtualizacao.Text = Global.DateTimeToStr(Global.DateTimeNow);
            base.LimparTela();
            
        }
        public void SetDados()
        {
            ParametrosModel.id_parametro = Convert.ToInt32(ParametrosController.CadParametrosBase.isNullResultZero(edtCodigo.Text));
            ParametrosModel.nm_parametro = edtNome.Text;
            ParametrosModel.valor = edtValor.Text;
            ParametrosModel.status = cbStatus.Text;
            ParametrosModel.data_atualizacao = DateTime.Now;
        }

        public void GetDados()
        {
            edtCodigo.Text = ParametrosModel.id_parametro.ToString();
            edtNome.Text = ParametrosModel.nm_parametro;
            edtValor.Text = ParametrosModel.valor;
            cbStatus.Text = ParametrosModel.status;
            edtDataAtualizacao.Text = ParametrosModel.data_atualizacao.ToString();
            edtDataCriacao.Text = ParametrosModel.data_criacao.ToString();
        }

        private void PersistirDados()
        {
            SetDados();
            string msg=ParametrosController.CadParametrosBase.PersisteNoBanco(ParametrosModel, OperacaoToInt(operacao));
            if (!Global.IsNullOrEmpty(msg))
            {
                MessageBox.Show(msg);
            }

            LimparTela();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (edtNome.Text.Length == 0)
            {
                MessageBox.Show("Favor informar o Nome do Parametro.");
                edtNome.Focus();
                return;
            }
            if (edtValor.Text.Length == 0)
            {
                MessageBox.Show("Favor informar o Valor do Parametro.");
                edtValor.Focus();
                return;
            }
            PersistirDados();
        }

        public override void CriaClasseModel()
        {
            new frmPesquisa(ParametrosModel).ShowDialog();
            MostrarNatela();
        }

        private void MostrarNatela()
        {
            LimparTela();
            GetDados();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            operacao = Operacao.oNenhum;
            ControlaBotoes();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            HabilitaCampos();
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            HabilitaCampos();
        }
    }
}
