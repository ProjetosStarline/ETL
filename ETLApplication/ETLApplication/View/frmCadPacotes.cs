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
    public partial class frmCadPacotes : ETLApplication.View.frmCadastroBase
    {
        PacotesController PacotesController;
        public frmCadPacotes()
        {
            InitializeComponent();
            PacotesController = new PacotesController();
            HabilitaCampos();
        }

        public void HabilitaCampos()
        {
            edtCodigo.Enabled = !PacotesController.CadPacotesBase.FieldIsPK(PacotesModel, "id_categoria");
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
            cbCategoria.Text = "";
            if (cbStatus.Items.Count > 0) { cbStatus.SelectedIndex = 0; }
            edtNome.Clear();
            edtDescricao.Clear();
            edtDataCriacao.Text = Global.DateTimeToStr(Global.DateTimeNow);
            edtDataAtualizacao.Text = Global.DateTimeToStr(Global.DateTimeNow);
            base.LimparTela();
        }
        public void SetDados()
        {
            PacotesModel.id_pacote = Convert.ToInt32(PacotesController.CadPacotesBase.isNullResultZero(edtCodigo.Text));
            PacotesModel.id_categoria = PacotesController.Categoria.GetIdCategoria(cbCategoria.Text);
            PacotesModel.nm_pacote = edtNome.Text;
            PacotesModel.descr_pacotes = edtDescricao.Text;
            PacotesModel.status = cbStatus.Text;
            PacotesModel.data_atualizacao = DateTime.Now;
        }

        public void GetDados()
        {
            edtCodigo.Text = PacotesModel.id_pacote.ToString();
            cbCategoria.Text = PacotesController.Categoria.GetNmCategoria(PacotesModel.id_categoria);
            edtNome.Text = PacotesModel.nm_pacote;
            edtDescricao.Text = PacotesModel.descr_pacotes;
            cbStatus.Text = PacotesModel.status;
            edtDataAtualizacao.Text = PacotesModel.data_atualizacao.ToString();
            edtDataCriacao.Text = PacotesModel.data_criacao.ToString();
        }

        private void PersistirDados()
        {
            SetDados();
            string msg = PacotesController.CadPacotesBase.PersisteNoBanco(PacotesModel, OperacaoToInt(operacao));
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
                MessageBox.Show("Favor informar o Nome do pacote.");
                edtNome.Focus();
                return;
            }
            if (cbCategoria.Text.Length == 0)
            {
                MessageBox.Show("Favor informar a Categoria.");
                cbCategoria.Focus();
                return;
            }
            if (operacao == Operacao.oDelete)
            {
                if (PacotesController.ExistemFilhos(Global.StrToInt(edtCodigo.Text)))
                {
                    MessageBox.Show("Antes de excluir este registro, é necessário excluir os Arquivos relacionados a este Pacote.");
                    return;
                }
            }
            PersistirDados();
        }

        public override void CriaClasseModel()
        {
            new frmPesquisa(PacotesModel).ShowDialog();
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

        private void frmCadPacotes_Load(object sender, EventArgs e)
        {
            CategoriasController Categoria = new CategoriasController();
            Categoria.PreencheCombo(cbCategoria);
        }
    }
}
