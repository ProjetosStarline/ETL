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
    public partial class frmCadastroBase : Form 
    {
        public PerfisModel perfisModel;
        public UsuariosModel usuariosModel;
        public GruposModel gruposModel;
        public CategoriasModel categoriasModel;
        public PacotesModel PacotesModel;
        public ServicosModel ServicosModel;
        public ArquivosModel arquivosModel;
        public MonitoramentosModel monitoramentosModel;
        public MapeamentosModel mapeamentosModel;
        public PermissoesModel permissoesModel;
        public ObjetosModel objetosModel;
        public ParametrosModel ParametrosModel;
        public enum Operacao { oInsert = 1, oEdit = 2, oDelete = 3, oConsulta = 4, oNenhum = 5 };
        public Operacao operacao = new Operacao();
        //



        public frmCadastroBase()
        {
            InitializeComponent();
            perfisModel = new PerfisModel();
            usuariosModel = new UsuariosModel();
            gruposModel = new GruposModel();
            categoriasModel = new CategoriasModel();
            PacotesModel = new PacotesModel();
            ServicosModel = new ServicosModel();
            arquivosModel = new ArquivosModel();
            monitoramentosModel = new MonitoramentosModel();
            mapeamentosModel = new MapeamentosModel();
            permissoesModel = new PermissoesModel();
            objetosModel = new ObjetosModel();
            ParametrosModel = new ParametrosModel();
            //
            //
            btnOk.Enabled = false;
            operacao = Operacao.oNenhum;
            ControlaBotoes();
        }

        private void pnlBarraBotoes_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            operacao = Operacao.oInsert;
            LimparTela();
            ControlaBotoes();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            operacao = Operacao.oEdit;
            ControlaBotoes();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            operacao = Operacao.oDelete;
            ControlaBotoes();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            ControlaBotoes();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            operacao = Operacao.oNenhum;
            LimparTela();
            ControlaBotoes();
        }

        public virtual void ControlaBotoes()
        {
            btnInserir.Enabled = operacao != Operacao.oInsert && (Global.Login.Cadastrar || Global.UsuarioSuper);
            btnAlterar.Enabled = operacao != Operacao.oEdit && (Global.Login.Alterar || Global.UsuarioSuper);
            btnExcluir.Enabled = operacao != Operacao.oDelete && (Global.Login.Deletar || Global.UsuarioSuper);
            btnPesquisar.Enabled = (operacao != Operacao.oConsulta || operacao == Operacao.oNenhum) && (Global.Login.Pesquisar || Global.UsuarioSuper);

            btnOk.Enabled = (operacao==Operacao.oInsert || operacao == Operacao.oEdit || operacao == Operacao.oDelete);
            groupBox1.Enabled = (operacao == Operacao.oInsert || operacao == Operacao.oEdit);
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            Global.Condicao = "";
            Global.EnviarParaLog("Tela Ativa: " + Global.TelaAtiva, null, GetEtapa(Global.TelaAtiva));
            switch (Global.TelaAtiva)
            {
                case "ETL_Arquivos": Global.Condicao = new ArquivosController().GetCondicaoPesquisa(Global.CodGrupoSel.ToString(),Global.CodCategSel.ToString(),"","") ; break;
                case "ETL_Categorias": Global.Condicao = $" and id_grupo={Global.CodGrupoSel.ToString()}"; break;
//                case "Cadastro_GrupoEmpresarial":; break;
                case "ETL_Pacotes": Global.Condicao = $" and p.id_categoria={Global.CodCategSel.ToString()}"; break;
//                case "Cadastro_Parametros":; break;
 //               case "Cadastro_Perfil":; break;
 //               case "Cadastro_Permissoes":; break;
 //               case "Cadastro_Servicos":; break;
 //               case "Cadastro_Usuario":; break;
            }
            Global.EnviarParaLog("Script a ser executado: " + Global.Condicao,null, GetEtapa(Global.TelaAtiva));

            operacao = Operacao.oConsulta;
            ControlaBotoes();
            CriaClasseModel();
        }

        public virtual void CriaClasseModel()
        {

        }
        public string GetEtapa(string pNomeTelaAtiva)
        {
            string Result = "";
            switch (pNomeTelaAtiva)
            {
                case "Cadastro_Arquivos": Result            = "CadArquivos"; break;
                case "Cadastro_Categorias": Result          = "CadCategorias"; break;
                case "Cadastro_GrupoEmpresarial": Result    = "CadGrupo"; break;
                case "Cadastro_Pacotes": Result             = "CadPacotes"; break;
                case "Cadastro_Parametros": Result          = "CadParametros"; break;
                case "Cadastro_Perfil": Result              = "CadPerfil"; break;
                case "Cadastro_Permissoes": Result          = "CadPermissoes"; break;
                case "Cadastro_Servicos": Result            = "CadServicos"; break;
                case "Cadastro_Usuario": Result             = "CadUsuario"; break;
            }
            return Result;
        }
        public int OperacaoToInt(Operacao oper)
        {
            int result = 0;
            switch (oper)
            {
                case Operacao.oInsert: result = 1; break;
                case Operacao.oEdit: result = 2; break;
                case Operacao.oDelete: result = 3; break;
                case Operacao.oConsulta: result = 4; break;
                case Operacao.oNenhum: result= 5; break;
            }
            return result;
        }

        public virtual void LimparTela()
        {
            perfisModel.Clear();
            usuariosModel.Clear();
            gruposModel.Clear();
            categoriasModel.Clear();
            PacotesModel.Clear();
            ServicosModel.Clear();
            arquivosModel.Clear();
            monitoramentosModel.Clear();
            mapeamentosModel.Clear();
            permissoesModel.Clear();
            objetosModel.Clear();
        }

        public string GetConteudoCampo(string pCampo)
        {
            if (!string.IsNullOrEmpty(pCampo))
            {
                return pCampo;
            }
            return "0";
        }

        private void frmCadastroBase_Load(object sender, EventArgs e)
        {
            btnCancelar_Click(sender, e);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
