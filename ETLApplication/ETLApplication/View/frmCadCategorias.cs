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
using Facade;

namespace ETLApplication.View
{
    public partial class frmCadCategorias : ETLApplication.View.frmCadastroBase
    {
        CategoriasController categoriasController;
        public frmCadCategorias()
        {
            InitializeComponent();
            categoriasController = new CategoriasController();
            HabilitaCampos();
        }

        public void HabilitaCampos()
        {
            edtCodigo.Enabled = !categoriasController.CadCategoriasBase.FieldIsPK(categoriasModel, "id_categoria");
        }

        public override void ControlaBotoes()
        {
            base.ControlaBotoes();
            if (edtCodigo != null)
            {
                btnAlterar.Enabled = operacao != Operacao.oEdit && edtCodigo.Text.Length   != 0 && edtCodigo.Text!="0" && (Global.Login.Alterar || Global.UsuarioSuper); 
                btnExcluir.Enabled = operacao != Operacao.oDelete && edtCodigo.Text.Length != 0 && edtCodigo.Text != "0" && (Global.Login.Deletar || Global.UsuarioSuper); 
            }
        }

        public override void LimparTela()
        {
            edtCodigo.Clear();
            cbGrupo.Text = "" ;
            if (cbStatus.Items.Count > 0)
            {
                cbStatus.SelectedIndex = 0;
            }
            edtNome.Clear();
            edtDescricao.Clear();
            edtDataCriacao.Text = Global.DateTimeToStr(Global.DateTimeNow);
            edtDataAtualizacao.Text = Global.DateTimeToStr(Global.DateTimeNow);
            edtHostSmtp.Clear();
            edtPortaSmtp.Text = "587";
            CkbEnableSSL.Checked = true;
            edtUsuarioSmtp.Clear();
            edtSenhaSmtp.Clear();
            ckbEnviarSomenteErros.Checked = false;
            base.LimparTela();
        }
        public void SetDados()
        {
            categoriasModel.id_categoria = Convert.ToInt32(categoriasController.CadCategoriasBase.isNullResultZero(edtCodigo.Text));
            categoriasModel.id_grupo = categoriasController.GetIdGrupo(cbGrupo.Text);
            categoriasModel.nm_categoria = edtNome.Text;
            categoriasModel.descr_categorias = edtDescricao.Text;
            categoriasModel.status = cbStatus.Text;
            categoriasModel.data_atualizacao = DateTime.Now;

            categoriasModel.Smtp_Host       = edtHostSmtp.Text;
            categoriasModel.Smtp_Port       = Global.StrToInt(edtPortaSmtp.Text);
            categoriasModel.Smtp_EnableSSL  = CkbEnableSSL.Checked? "SIM" : "NAO";
            categoriasModel.Smtp_eMail      = edtUsuarioSmtp.Text;
            categoriasModel.Smtp_Senha      = Crypto.Encriptar(edtSenhaSmtp.Text);
            categoriasModel.EnviarSoErros   = ckbEnviarSomenteErros.Checked? "SIM" : "NAO";

        }

        public void GetDados()
        {
            edtCodigo.Text           =  categoriasModel.id_categoria.ToString();
            cbGrupo.Text             =  categoriasController.GetNmGrupo(categoriasModel.id_grupo);
            edtNome.Text             =  categoriasModel.nm_categoria;
            edtDescricao.Text        =  categoriasModel.descr_categorias;
            cbStatus.Text            =  categoriasModel.status;
            edtDataAtualizacao.Text  =  categoriasModel.data_atualizacao.ToString();
            edtDataCriacao.Text      =  categoriasModel.data_criacao.ToString();

            edtHostSmtp.Text = categoriasModel.Smtp_Host;
            edtPortaSmtp.Text =categoriasModel.Smtp_Port.ToString();
            CkbEnableSSL.Checked=categoriasModel.Smtp_EnableSSL=="SIM";
            edtUsuarioSmtp.Text=categoriasModel.Smtp_eMail;
            edtSenhaSmtp.Text=categoriasModel.Smtp_Senha;
            ckbEnviarSomenteErros.Checked =categoriasModel.EnviarSoErros =="SIM";
        }

        private void PersistirDados()
        {
            SetDados();
            string msg=categoriasController.CadCategoriasBase.PersisteNoBanco(categoriasModel, OperacaoToInt(operacao));
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
                MessageBox.Show("Favor informar o Nome da Categoria.");
                edtNome.Focus();
                return;
            }
            if (operacao == Operacao.oDelete)
            {
                if (categoriasController.ExistemFilhos(Global.StrToInt(edtCodigo.Text)))
                {
                    MessageBox.Show("Antes de excluir este registro, é necessário excluir os Pacotes relacionados a esta Filial.");
                    return;
                }

                if (categoriasController.ExistemFilhosUsuariosXCategorias(Global.StrToInt(edtCodigo.Text)))
                {
                    categoriasController.DeletaUsuariosXCategorias(Global.StrToInt(edtCodigo.Text));
                    //MessageBox.Show("Antes de excluir este registro, é necessário tirar o acesso deste usuário para esta Filial.");
                    //return;
                }
                
            }
            PersistirDados();
        }

        public override void CriaClasseModel()
        {
            new frmPesquisa(categoriasModel).ShowDialog();
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

        private void frmCadCategorias_Load(object sender, EventArgs e)
        {
            CadastroBase<GruposModel> gruposModel = new CadastroBase<GruposModel>();
            string select = gruposModel.GetSelect(new GruposModel());
            gruposModel.GetLista(select, cbGrupo);
            cbGrupo.ValueMember = "id_grupo";
            cbGrupo.DisplayMember = "nm_grupo";
            if (cbGrupo.Items.Count > 0)
            {
                cbGrupo.SelectedIndex = 0;
            }
        }
    }
}
