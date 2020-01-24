using ETLApplication.Controller;
using ETLApplication.DAO;
using ETLApplication.Model;
using ETLApplication.View;
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
    public partial class frmPrincipal : Form
    {

        public static string MenuSelecionado = "";
        public static List<LoginModel> LoginPermissao ;

        public static UsuariosController Usuario;
        ObjetosController oMenu;
        LogsController Logs;
        public frmPrincipal()
        {
            InitializeComponent();
            LoginPermissao = new List<LoginModel>();
            oMenu=new ObjetosController();
            Usuario = new UsuariosController();
            Logs = new LogsController();
            CadastraMenu();
        }

        public void CadastraMenu()
        {
            foreach(ToolStripMenuItem ItemMenu in Menu.Items)
            {
                string MenuPai = ItemMenu.Name;
                oMenu.CadastrarMenu(MenuPai);
                if (ItemMenu.DropDownItems.Count > 0)
                {
                    foreach (ToolStripMenuItem FilhosMenu in ItemMenu.DropDownItems)
                    {
                        string MenuFilho = FilhosMenu.Name;
                        oMenu.CadastrarMenu(MenuFilho);
                        if (FilhosMenu.DropDownItems.Count > 0)
                        {
                            foreach (ToolStripMenuItem SubFilhosMenu in FilhosMenu.DropDownItems)
                            {
                                string SubMenuFilho = SubFilhosMenu.Name;
                                oMenu.CadastrarMenu(SubMenuFilho);
                            }

                        }
                    }
                }
            }
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MenuSelecionado = sender.ToString();
            SetaPermissoesMenu();
            Close();
        }

        private void frmPrincipal_Shown(object sender, EventArgs e)
        {
            Text = $"ETLStarline v{Global.versao}";
            SetaPermissoesMenu();
            Global.UsuarioLogado = null;
            new frmLogin().ShowDialog();
            if (Global.UsuarioLogado != null)
            {
                HabilitarMenu(Global.UsuarioSuper);
                SetaParametrosDefault();
            }
            else
            {
                Close();
            }
        }

        private void Cadastro_Perfil_Click(object sender, EventArgs e)
        {
            if (Global.CodGrupoSel > 0 && Global.CodCategSel > 0)
            {
                MenuSelecionado = "Cadastro_Perfil";
                SetaPermissoesMenu();
                new frmCadPerfil().ShowDialog();
            }
            else
            {
                MessageBox.Show("Necessário selecionar uma Empresa e/ou uma Filial.");
            }
        }

        private void Cadastro_Permissoes_Click(object sender, EventArgs e)
        {
            if (Global.CodGrupoSel > 0 && Global.CodCategSel > 0)
            {
                MenuSelecionado = "Cadastro_Permissoes";
                SetaPermissoesMenu();
                new frmPermissoes().ShowDialog();
            }
            else
            {
                MessageBox.Show("Necessário selecionar uma Empresa e/ou uma Filial.");
            }
        }

        private void Cadastro_Usuario_Click(object sender, EventArgs e)
        {

            if (Global.CodGrupoSel > 0 && Global.CodCategSel > 0 || Usuario.UsuarioIsAdmin(Global.UsuarioLogado.Id_usuario))
            {
                MenuSelecionado = "Cadastro_Usuario";
                SetaPermissoesMenu();
                new frmUsuarios().ShowDialog();
            }
            else
            {
                MessageBox.Show("Necessário selecionar uma Empresa e/ou uma Filial.");
            }
        }

        private void Cadastro_GrupoEmpresarial_Click(object sender, EventArgs e)
        {
            MenuSelecionado = "Cadastro_Empresa";
            SetaPermissoesMenu();
            new frmCadGrupos().ShowDialog();
        }

        private void Cadastro_Categorias_Click(object sender, EventArgs e)
        {
            if (Global.CodGrupoSel > 0 || Usuario.UsuarioIsAdmin(Global.UsuarioLogado.Id_usuario))
            {
                MenuSelecionado = "Cadastro_Filial";
                SetaPermissoesMenu();
                new frmCadCategorias().ShowDialog();
            }
            else
            {
                MessageBox.Show("Necessário selecionar uma Empresa.");
            }
        }

        private void frmPrincipal_Activated(object sender, EventArgs e)
        {
            MostrarGrupoCategoria();
            if (Global.UsuarioLogado!=null)
            {
                lblUsuarioLogado.Text = $"Usuário: {Global.UsuarioLogado.Nm_usuario}";
            }
        }

        private void MostrarGrupoCategoria()
        {
            ParametrosController Parametros = new ParametrosController();
            GruposController Grupo = new GruposController();
            CategoriasController Categoria = new CategoriasController();

            Global.CodGrupoSel = Convert.ToInt32(Parametros.CadParametrosBase.isNullResultZero(Parametros.GetValorParametro("Empresa_Selecionada")));
            Global.CodCategSel = Convert.ToInt32(Categoria.CadCategoriasBase.isNullResultZero(Parametros.GetValorParametro("Filial_Selecionada")));
            if (Global.CodGrupoSel == 0)
            {
                lblGrupo.Text = "Empresa ainda não cadastrado.";
            }
            else
            {
                string NomeGrupo = Grupo.GetNmGrupo(Global.CodGrupoSel);
                if (string.IsNullOrEmpty(NomeGrupo))
                {
                    Global.CodGrupoSel = 0;
                    lblGrupo.Text = "Selecione uma Empresa.";
                }
                else
                {
                    lblGrupo.Text = Global.CodGrupoSel + "-" + NomeGrupo;
                }
            }
            lblSeparador.Text = "  ";
            if (Global.CodCategSel == 0)
            {
                lblCategoria.Text = "Filial ainda não cadastrada.";
            }
            else
            {
                string NomeCateg = Categoria.GetNmCategoria(Global.CodCategSel,Global.CodGrupoSel);
                if (string.IsNullOrEmpty(NomeCateg))
                {
                    Global.CodCategSel = 0;
                    lblCategoria.Text = "Selecione uma Filial.";
                }
                else
                {
                    if (Global.UsuarioLogado != null)
                    {
                        if (Categoria.TemAcessoParaEstaFilial(Global.CodCategSel, Global.UsuarioLogado.Id_usuario))
                        {
                            lblCategoria.Text = Global.CodCategSel + "-" + NomeCateg;

                        }
                        else
                        {
                            Global.CodCategSel = 0;
                            lblCategoria.Text = "Selecione uma Filial.";
                        }
                    }
                }
            }
        }

        public void HabilitarMenu(bool pHabilitar)
        {
            Sistema.Enabled = pHabilitar;
            Sistema_Configuracao.Enabled = pHabilitar;
            Sistema_Configuracao_SelecionarEmpresa.Enabled = pHabilitar;
            Sistema_Usuarios.Enabled = pHabilitar;
            Sistema_Usuarios_AlterarSenha.Enabled = pHabilitar;
            Sistema_Usuarios_MudarUsuario.Enabled = pHabilitar;
            Sistema_Sair.Enabled = true;
            Cadastro.Enabled = pHabilitar;
            Cadastro_Perfil.Enabled = pHabilitar;
            Cadastro_Usuario.Enabled = pHabilitar;
            Cadastro_Permissoes.Enabled = pHabilitar;
            Cadastro_Empresa.Enabled = pHabilitar;
            Cadastro_Filial.Enabled = pHabilitar;
            ETL_Arquivos.Enabled = pHabilitar;
            ETL.Enabled = pHabilitar;
            ETL_Pacotes.Enabled = pHabilitar;
            ETL_Servicos.Enabled = pHabilitar;
            Processos.Enabled = pHabilitar;
            Processos_ExportarTabelas.Enabled = pHabilitar;
            Processos_ImportarTabelas.Enabled = pHabilitar;

            if (!Global.UsuarioSuper)
            {
                foreach (var lst in LoginPermissao)
                {
                    switch (lst.menu)
                    {
                        case "Sistema": Sistema.Enabled = lst.Vizualizar; break;
                        case "Sistema_Configuracao": Sistema_Configuracao.Enabled = lst.Vizualizar; break;
                        case "Sistema_Configuracao_SelecionarEmpresa": Sistema_Configuracao_SelecionarEmpresa.Enabled = lst.Vizualizar; break;
                        case "Sistema_Usuarios": Sistema_Usuarios.Enabled = lst.Vizualizar; break;
                        case "Sistema_Usuarios_AlterarSenha": Sistema_Usuarios_AlterarSenha.Enabled = lst.Vizualizar; break;
                        case "Sistema_Usuarios_MudarUsuario": Sistema_Usuarios_MudarUsuario.Enabled = lst.Vizualizar; break;
                        case "Sistema_Sair": Sistema_Sair.Enabled = lst.Vizualizar; break;
                        case "Cadastro": Cadastro.Enabled = lst.Vizualizar; break;
                        case "Cadastro_Perfil": Cadastro_Perfil.Enabled = lst.Vizualizar; break;
                        case "Cadastro_Usuario": Cadastro_Usuario.Enabled = lst.Vizualizar; break;
                        case "Cadastro_Permissoes": Cadastro_Permissoes.Enabled = lst.Vizualizar; break;
                        case "Cadastro_Empresa": Cadastro_Empresa.Enabled = lst.Vizualizar; break;
                        case "Cadastro_Filial": Cadastro_Filial.Enabled = lst.Vizualizar; break;
                        case "ETL": ETL.Enabled = lst.Vizualizar; break;
                        case "ETL_Servicos": ETL_Servicos.Enabled = lst.Vizualizar; break;
                        case "ETL_Pacotes": ETL_Pacotes.Enabled = lst.Vizualizar; break;
                        case "ETL_Arquivos": ETL_Arquivos.Enabled = lst.Vizualizar; break;
                        case "Processos": Processos.Enabled = lst.Vizualizar; break;
                        case "Processos_ExportarTabelas": Processos_ExportarTabelas.Enabled = lst.Vizualizar; break;
                        case "Processos_ImportarTabelas": Processos_ImportarTabelas.Enabled = lst.Vizualizar; break;
                    }
                }
            }


        }

        public void SetaParametrosDefault()
        {
            ParametrosController Parametros = new ParametrosController();
            if (Parametros.GetValorParametro("connection_string_base_destino").Length == 0)
            {
                Parametros.GravaNovoParametro("connection_string_base_destino", "Data Source=<Ip do servidor>;Initial Catalog=<Nome da base de Dados>;User ID=<Usuario>;Password=<Senha>[;Integrated Security=True]");
            }
            if (Parametros.GetValorParametro("pooling_servico").Length == 0)
            {
                Parametros.GravaNovoParametro("pooling_servico", "60");
            }
            if (Parametros.GetValorParametro("Empresa_Selecionada").Length == 0)
            {
                Parametros.GravaNovoParametro("Empresa_Selecionada", "0");
            }
            if (Parametros.GetValorParametro("Filial_Selecionada").Length == 0)
            {
                Parametros.GravaNovoParametro("Filial_Selecionada", "0");
            }
            if (Parametros.GetValorParametro("QtdeDiasExpurgoLog").Length == 0)
            {
                Parametros.GravaNovoParametro("QtdeDiasExpurgoLog", "30");
            }
            if (Parametros.GetValorParametro("QtdeDiasExpurgoArq").Length == 0)
            {
                Parametros.GravaNovoParametro("QtdeDiasExpurgoArq", "30");
            }
            if (Parametros.GetValorParametro("Sugerir_Mapeamento_Do_Arquivo").Length == 0)
            {
                Parametros.GravaNovoParametro("Sugerir_Mapeamento_Do_Arquivo", "SIM");
            }
            if (Parametros.GetValorParametro("Concatena_ID_Tabela_Destino(SIM/NAO)").Length == 0)
            {
                Parametros.GravaNovoParametro("Concatena_ID_Tabela_Destino(SIM/NAO)", "NAO");
            }
        }

        private void Configuracao_SelecionarGrupo_Click(object sender, EventArgs e)
        {
            MenuSelecionado = "Sistema_Configuracao_SelecionarEmpresa";
            SetaPermissoesMenu();
            new frmConfigGrupo().ShowDialog();
        }

        private void Cadastro_Pacotes_Click(object sender, EventArgs e)
        {
            if (Global.CodGrupoSel>0 && Global.CodCategSel > 0)
            {
                MenuSelecionado = "ETL_Pacotes";
                SetaPermissoesMenu();
                new frmCadPacotes().ShowDialog();
            }
            else
            {
                MessageBox.Show("Necessário selecionar uma Empresa e/ou uma Filial.");
            }
        }

        private void Cadastro_Servicos_Click(object sender, EventArgs e)
        {
            if (Global.CodGrupoSel > 0 && Global.CodCategSel > 0)
            {
                MenuSelecionado = "ETL_Servicos";
                SetaPermissoesMenu();
                new frmCadServicos().ShowDialog();
           }
            else
            {
                MessageBox.Show("Necessário selecionar uma Empresa e/ou uma Filial.");
            }

        }

        private void Cadastro_Arquivos_Click(object sender, EventArgs e)
        {
            if (Global.CodGrupoSel > 0 && Global.CodCategSel > 0)
            {
                MenuSelecionado = "ETL_Arquivos";
                SetaPermissoesMenu();
                new frmCadArquivos().ShowDialog();
            }
            else
            {
                MessageBox.Show("Necessário selecionar uma Empresa e/ou uma Filial.");
            }
        }

        public void SetaPermissoesMenu()
        {
            Global.TelaAtiva = MenuSelecionado;
            Global.Login = Usuario.Permissao(LoginPermissao, MenuSelecionado);
        }

        private void parâmetrosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MenuSelecionado = "Cadastro_Parametros";
            SetaPermissoesMenu();
            new frmCadParametros().ShowDialog();
        }

        private void Processos_ExportarImportarTabelas_Click(object sender, EventArgs e)
        {
            if (Global.CodGrupoSel > 0 && Global.CodCategSel > 0)
            {
                MenuSelecionado = "Processos_ExportarTabelas";
                SetaPermissoesMenu();
                new frmExpImpTabelas().ShowDialog();
            }
            else
            {
                MessageBox.Show("Necessário selecionar uma Empresa e/ou uma Filial.");
            }
        }

        private void Sistema_Usuarios_MudarUsuario_Click(object sender, EventArgs e)
        {
            MenuSelecionado = "Sistema_Usuarios_MudarUsuario";
            SetaPermissoesMenu();
            new frmLogin().ShowDialog();
            if (Global.UsuarioLogado != null)
            {
                HabilitarMenu(Global.UsuarioSuper);
                SetaParametrosDefault();
            }
            else
            {
                Close();
            }
        }

        private void Sistema_Usuarios_AlterarSenha_Click(object sender, EventArgs e)
        {
            if (Global.CodGrupoSel > 0 && Global.CodCategSel > 0)
            {
                MenuSelecionado = "Sistema_Usuarios_AlterarSenha";
                SetaPermissoesMenu();
                new frmAlterarSenha().ShowDialog();
            }
            else
            {
                MessageBox.Show("Necessário selecionar uma Empresa e/ou uma Filial.");
            }

        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            lblBackupSucesso.Text= $"{Logs.GetBackupGerado()}";
            lblBackupData.Text = $"Data: {Logs.GetBackupData()}";
            lblUsuarioLogado.Text= $"Usuário: {Global.UsuarioLogado.Nm_usuario}";
        }

        private void Processos_ImportarTabelas_Click(object sender, EventArgs e)
        {
            MenuSelecionado = "Processos_ImportarTabelas";
            SetaPermissoesMenu();
            new frmExpImpTabelas().ShowDialog();
        }

        private void visualizarAndamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MenuSelecionado = "Processos_VisualizarAndamento";
            SetaPermissoesMenu();
            new frmVisualizarAndamento().ShowDialog();
        }
    }
}

