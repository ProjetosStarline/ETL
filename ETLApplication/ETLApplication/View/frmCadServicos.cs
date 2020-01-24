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
    public partial class frmCadServicos : ETLApplication.View.frmCadastroBase
    {
        ServicosController ServicosController;
        public frmCadServicos()
        {
            InitializeComponent();
            ServicosController = new ServicosController();
            HabilitaCampos();
        }

        public void HabilitaCampos()
        {
            edtCodigo.Enabled = !ServicosController.CadServicosBase.FieldIsPK(ServicosModel, "id_servico");
        }

        public override void ControlaBotoes()
        {
            base.ControlaBotoes();
            if (edtCodigo != null)
            {
                btnAlterar.Enabled = operacao != Operacao.oEdit && edtCodigo.Text.Length   != 0 && edtCodigo.Text != "0" && (Global.Login.Alterar || Global.UsuarioSuper);
                btnExcluir.Enabled = operacao != Operacao.oDelete && edtCodigo.Text.Length != 0 && edtCodigo.Text != "0" && (Global.Login.Deletar || Global.UsuarioSuper);
            }
        }

        public override void LimparTela()
        {
            edtCodigo.Clear();
            if (cbStatus.Items.Count > 0) { cbStatus.SelectedIndex = 0; }
            edtNome.Clear();
            edtDataCriacao.Text = Global.DateTimeToStr(Global.DateTimeNow);
            edtDataAtualizacao.Text = Global.DateTimeToStr(Global.DateTimeNow);
            base.LimparTela();
        }
        public void SetDados()
        {
            ServicosModel.id_servico = Convert.ToInt32(ServicosController.CadServicosBase.isNullResultZero(edtCodigo.Text));
            ServicosModel.nm_servico = edtNome.Text;
            ServicosModel.status = cbStatus.Text;
            ServicosModel.data_atualizacao = DateTime.Now;
        }

        public void GetDados()
        {
            edtCodigo.Text = ServicosModel.id_servico.ToString();
            edtNome.Text = ServicosModel.nm_servico;
            cbStatus.Text = ServicosModel.status;
            edtDataAtualizacao.Text = ServicosModel.data_atualizacao.ToString();
            edtDataCriacao.Text = ServicosModel.data_criacao.ToString();
        }

        private void PersistirDados()
        {
            SetDados();
            string msg=ServicosController.CadServicosBase.PersisteNoBanco(ServicosModel, OperacaoToInt(operacao));
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
                MessageBox.Show("Favor informar o Nome do Serviço.");
                edtNome.Focus();
                return;
            }
            if (operacao == Operacao.oDelete)
            {
                if (ServicosController.ExistemFilhos(Global.StrToInt(edtCodigo.Text)))
                {
                    MessageBox.Show("Antes de excluir este registro, é necessário excluir os Arquivos relacionados a este Serviço.");
                    return;
                }
            }
            PersistirDados();
        }

        public override void CriaClasseModel()
        {
            new frmPesquisa(ServicosModel).ShowDialog();
            MostrarNatela();
        }

        private void MostrarNatela()
        {
            //LimparTela();
            GetDados();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            operacao = Operacao.oNenhum;
            ControlaBotoes();
        }

        private void frmCadServicos_Load(object sender, EventArgs e)
        {
        }
    }
}
