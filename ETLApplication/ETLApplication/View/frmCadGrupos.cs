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
    public partial class frmCadGrupos : ETLApplication.View.frmCadastroBase
    {
        GruposController gruposController;
        public frmCadGrupos()
        {
            InitializeComponent();
            gruposController = new GruposController();
            HabilitaCampos();

        }
        public void HabilitaCampos()
        {
            edtCodigo.Enabled = !gruposController.CadGruposBase.FieldIsPK(gruposModel, "id_grupo");
        }

        public override void ControlaBotoes()
        {
            base.ControlaBotoes();
            if (edtCodigo != null)
            {
                btnAlterar.Enabled = operacao != Operacao.oEdit && edtCodigo.Text.Length   != 0 && edtCodigo.Text !="0"&& (Global.Login.Alterar || Global.UsuarioSuper);
                btnExcluir.Enabled = operacao != Operacao.oDelete && edtCodigo.Text.Length != 0 && edtCodigo.Text != "0" && (Global.Login.Deletar || Global.UsuarioSuper);
            }
        }

        public override void LimparTela()
        {
            edtCodigo.Clear();
            if (cbStatus.Items.Count>0) { cbStatus.SelectedIndex = 0; }
            edtNome.Clear();
            edtDescricao.Clear();
            edtDataCriacao.Text = Global.DateTimeToStr(Global.DateTimeNow);
            edtDataAtualizacao.Text = Global.DateTimeToStr(Global.DateTimeNow);
            edtSite.Clear();
            edtLogo.Clear();
            base.LimparTela();
        }
        public void SetDados()
        {
            gruposModel.id_grupo = Convert.ToInt32(gruposController.CadGruposBase.isNullResultZero(edtCodigo.Text));
            gruposModel.nm_grupo = edtNome.Text;
            gruposModel.descr_grupos = edtDescricao.Text;
            gruposModel.status = cbStatus.Text;
            gruposModel.data_atualizacao = DateTime.Now;
            gruposModel.Site_Empresa = edtSite.Text;
            gruposModel.Logo_Empresa = edtLogo.Text;
        }

        public void GetDados()
        {
            edtCodigo.Text = gruposModel.id_grupo.ToString();
            edtNome.Text = gruposModel.nm_grupo;
            edtDescricao.Text = gruposModel.descr_grupos;
            cbStatus.Text = gruposModel.status;
            edtDataAtualizacao.Text = gruposModel.data_atualizacao.ToString();
            edtDataCriacao.Text = gruposModel.data_criacao.ToString();
            edtSite.Text = gruposModel.Site_Empresa;
            edtLogo.Text = gruposModel.Logo_Empresa;
        }

        private void PersistirDados()
        {
            SetDados();
            string msg=gruposController.CadGruposBase.PersisteNoBanco(gruposModel, OperacaoToInt(operacao));
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
                MessageBox.Show("Favor informar o Nome do Grupo.");
                edtNome.Focus();
                return;
            }
            if (operacao == Operacao.oDelete)
            {
                if (gruposController.ExistemFilhos(Global.StrToInt(edtCodigo.Text)))
                {
                    MessageBox.Show("Antes de excluir este registro, é necessário excluir as Filiais relacionadas a esta Empresa.");
                    return;
                }
            }
            PersistirDados();
        }

        public override void CriaClasseModel()
        {
            new frmPesquisa(gruposModel).ShowDialog();
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
    }
}
