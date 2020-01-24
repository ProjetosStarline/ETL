using ETLApplication.Controller;
using ETLApplication.Model;
using EtlConexao;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace ETLApplication.View
{
    public partial class frmExpImpTabelas : Form
    {
        ExpImpTabelasController expImpTabelas = new ExpImpTabelasController();
        ExpImpTabelasModel ExpImp = new ExpImpTabelasModel();

        GruposModel         gruposModel         ;
        CategoriasModel     categoriasModel     ;
        PacotesModel        pacotesModel        ;
        ServicosModel       servicosModel       ;
        MonitoramentosModel monitoramentosModel ;
        ArquivosModel       arquivosModel       ;
        MapeamentosModel    mapeamentosModel    ;
        PerfisModel         perfisModel         ;
        UsuariosModel       usuariosModel       ;
        ObjetosModel        objetosModel        ;
        PermissoesModel     permissoesModel     ;
        ParametrosModel     parametrosModel     ;

        GruposController gruposController;
        CategoriasController categoriasController;
        PacotesController pacotesController;
        ServicosController servicosController;
        MonitoramentosController monitoramentosController;
        ArquivosController arquivosController;
        MapeamentosController mapeamentosController;
        PerfisController perfisController;
        UsuariosController usuariosController;
        ObjetosController objetosController;
        PermissoesController permissoesController;
        ParametrosController parametrosController;


        public frmExpImpTabelas()
        {
            InitializeComponent();
            gruposModel =new GruposModel();
            categoriasModel =new CategoriasModel();
            pacotesModel =new PacotesModel();
            servicosModel=new ServicosModel();
            monitoramentosModel =new MonitoramentosModel();
            arquivosModel =new ArquivosModel();
            mapeamentosModel =new MapeamentosModel();
            perfisModel =new PerfisModel();
            usuariosModel =new UsuariosModel();
            objetosModel =new ObjetosModel();
            permissoesModel =new PermissoesModel();
            parametrosModel =new ParametrosModel();

            gruposController =new GruposController();
            categoriasController =new CategoriasController();
            pacotesController =new PacotesController();
            servicosController =new ServicosController();
            monitoramentosController =new MonitoramentosController();
            arquivosController =new ArquivosController();
            mapeamentosController =new MapeamentosController();
            perfisController =new PerfisController();
            usuariosController =new UsuariosController();
            objetosController =new ObjetosController();
            permissoesController =new PermissoesController();
            parametrosController =new ParametrosController();
        }

        private void frmExpImpTabelas_Load(object sender, EventArgs e)
        {
            edtGrupo.Text = Global.CodGrupoSel.ToString();
            tb_grupos.Checked = edtGrupo.Text != "";
            edtCategoria.Text = Global.CodCategSel.ToString();
            tb_categorias.Checked = edtGrupo.Text != "";

            rbImportar.Checked = frmPrincipal.MenuSelecionado == "Processos_ImportarTabelas";
            rbExportar.Checked = frmPrincipal.MenuSelecionado == "Processos_ExportarTabelas";
            rbExportar.Visible = false;
            rbImportar.Visible = false;
            if (rbImportar.Checked)
            {
                lblProcesso.Text = "Importar";
                Text = "Importação de Tabelas";
            }
            if (rbExportar.Checked)
            {
                lblProcesso.Text = "Exportar";
                Text = "Exportação de Tabelas";
            }

        }

        private void btnLocalizarArquivo_Click(object sender, EventArgs e)
        {
            if (rbExportar.Checked)
            {
                DialogResult dr = folderBrowserDialog1.ShowDialog();
                if (dr.ToString() == "OK")
                {
                    edtNomeArquivo.Text = folderBrowserDialog1.SelectedPath + "\\";
                }

            }
            else
            {
                DialogResult dr = openFileDialog1.ShowDialog();
                if (dr.ToString() == "OK")
                {
                    edtNomeArquivo.Text = openFileDialog1.FileName;
                }
                string edttabela = "g";
                if (!string.IsNullOrEmpty(edtTabela.Text))
                {
                    edttabela = edtTabela.Text;
                }
                DataTable dt= expImpTabelas.GetDadosXml(edtNomeArquivo.Text, edttabela);
                dgvTabela.DataSource = dt;
                string tst = dt.Columns["nm_grupo"].ToString();
            }

        }

        public void MudaProcesso()
        {
            if (rbExportar.Checked)
            {
                lblExpImp.Text = "Exportar";
                btnExportarImportar.Text = "Exportar";
            }
            else
            {
                lblExpImp.Text = "Importar";
                btnExportarImportar.Text = "Importar";

            }
        }

        private void edtNomeArquivo_TextChanged(object sender, EventArgs e)
        {
            if (rbExportar.Checked)
            {
                //Carregar tabela apos exportar
            }
            else
            {
                //Carregar Arquivo a ser importado
                txtArquivo.Text = File.ReadAllText(edtNomeArquivo.Text);
            }
        }

        private void rbExportar_CheckedChanged(object sender, EventArgs e)
        {
            MudaProcesso();
            tb_grupos.Enabled = rbExportar.Checked;
            tb_categorias.Enabled = rbExportar.Checked;
            tb_pacotes.Enabled = rbExportar.Checked;
            tb_servicos.Enabled = rbExportar.Checked;
            tb_arquivos.Enabled = rbExportar.Checked;
            tb_mapeamentos.Enabled = rbExportar.Checked;
            tb_perfis.Enabled = rbExportar.Checked;
            tb_usuarios.Enabled = rbExportar.Checked;
            tb_objetos.Enabled = rbExportar.Checked;
            tb_permissoes.Enabled = rbExportar.Checked;
            tb_parametros.Enabled = rbExportar.Checked;
        }

        private void rbImportar_CheckedChanged(object sender, EventArgs e)
        {
            MudaProcesso();
            tb_grupos.Checked = false;
            tb_categorias.Checked = false;
            tb_pacotes.Checked = false;
            tb_servicos.Checked = false;
            tb_arquivos.Checked = false;
            tb_mapeamentos.Checked = false;
            tb_perfis.Checked = false;
            tb_usuarios.Checked = false;
            tb_objetos.Checked = false;
            tb_permissoes.Checked = false;
            tb_parametros.Checked = false;
            tb_grupos.Enabled = rbExportar.Checked;
            tb_categorias.Enabled = rbExportar.Checked;
            tb_pacotes.Enabled = rbExportar.Checked;
            tb_servicos.Enabled = rbExportar.Checked;
            tb_arquivos.Enabled = rbExportar.Checked;
            tb_mapeamentos.Enabled = rbExportar.Checked;
            tb_perfis.Enabled = rbExportar.Checked;
            tb_usuarios.Enabled = rbExportar.Checked;
            tb_objetos.Enabled = rbExportar.Checked;
            tb_permissoes.Enabled = rbExportar.Checked;
            tb_parametros.Enabled = rbExportar.Checked;

        }

        public void Exportar(string pcb)
        {
            string NomeTabelaDST = pcb;
            SqlConnection _Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DbStarLineEtl"].ToString());
            _Conn.Open();
            try
            {
                GeraSaidaDoArquivo(_Conn, NomeTabelaDST, edtNomeArquivo.Text);
                string filename = edtNomeArquivo.Text + "\\" + NomeTabelaDST + ".csv";
                if (File.Exists(filename))
                {
                    txtArquivo.Text = File.ReadAllText(filename);
                }
            }
            finally
            {
                _Conn.Close();
            }
        }



        public void HabilitarXP_CmdShell(SqlConnection con, int OnOff)
        {
            string _OnOff = "Desabilitado";
            if (OnOff == 1)
            {
                _OnOff = "Habilitado";
            }
            try
            {
                SqlCommand cmd = null;
                cmd = new SqlCommand("sp_configure 'show advanced options', 1;", con);
                cmd.CommandTimeout = Global.CommandTimeOut;
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("RECONFIGURE;", con);
                cmd.CommandTimeout = Global.CommandTimeOut;
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand($"sp_configure 'xp_cmdshell', {OnOff.ToString()};", con);
                cmd.CommandTimeout = Global.CommandTimeOut;
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("RECONFIGURE;", con);
                cmd.CommandTimeout = Global.CommandTimeOut;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro,comando xp_cmdshell não {_OnOff}. MOTIVO:{ex.Message}.");
            }
        }
        public bool GeraSaidaDoArquivo(SqlConnection con, string pNomeTabelaDST, string pPathOutput)
        {
            SetDadosConexao(ConfigurationManager.ConnectionStrings["DbStarLineEtl"].ToString());
            bool Result = false;
            //Comando para Habilitar xp_cmdshell
            HabilitarXP_CmdShell(con, 1);

            //Utilizando queryout, pode-se exportar o resultado de uma query
            try
            {
                string cmdSelect = $"SELECT * FROM {pNomeTabelaDST}";
                string PathFileOutPut = $"{pPathOutput}{pNomeTabelaDST}.csv";
                string strDataSource = Global.DataSource;
                string strUsuario = Global.UserID == "" ? "" : $"{Global.UserID}";
                string strPassword = Global.Password == "" ? "" : $"{Global.Password}";
                string ddConexao = Global.AutenticacaoPeloWindows ? $"-T -S{strDataSource}" : $"-U{strUsuario} -P{strPassword} -S{strDataSource}";
                string AspasDupla = @"""";

                string comando = $"EXEC master.dbo.xp_cmdshell 'bcp {AspasDupla + cmdSelect + AspasDupla} queryout {AspasDupla + PathFileOutPut + AspasDupla} -c -t; {ddConexao}'";

                SqlCommand cmd = new SqlCommand(comando, con);
                cmd.CommandTimeout = Global.CommandTimeOut;
                cmd.ExecuteNonQuery();

                MessageBox.Show($"Comando para gerar arquivo CSV executado com sucesso.");
                Result = true;
            }
            catch (Exception ex)
            {
                Result = false;
                MessageBox.Show($"Erro ao executar o comando para gerar arquivo CSV. MOTIVO: {ex.Message}.");
            }

            //Comando para Desabilitar xp_cmdshell
            HabilitarXP_CmdShell(con, 0);

            return Result;
        }

        public void SetDadosConexao(string pConnectionString)
        {
            Global.AutenticacaoPeloWindows = pConnectionString.IndexOf("Integrated Security=True") > 0;
            string connStr = pConnectionString.Remove(0, pConnectionString.IndexOf("=") + 1);
            Global.DataSource = connStr.Substring(0, connStr.IndexOf(";"));
            connStr = connStr.Remove(0, connStr.IndexOf("=") + 1);
            Global.Schema = connStr.Substring(0, connStr.IndexOf(";"));
            if (Global.AutenticacaoPeloWindows)
            {
                Global.UserID = "";
                Global.Password = "";
            }
            else
            {
                connStr = connStr.Remove(0, connStr.IndexOf("=") + 1);
                Global.UserID = connStr.Substring(0, connStr.IndexOf(";"));
                connStr = connStr.Remove(0, connStr.IndexOf("=") + 1);
                Global.Password = connStr.Substring(0, connStr.IndexOf(";"));
            }

        }

        private void btnExportarImportar_Click(object sender, EventArgs e)
        {
           
            if (rbExportar.Checked)
            {
                Global.EnviarParaLog($"Exportando as Tabelas selecionadas aguarde...", txtArquivo, "IniExportação");
                if (edtNomeArquivo.Text.Length == 0)
                {
                    MessageBox.Show("Favor informar onde será salvo o arquivo.");
                    edtNomeArquivo.Focus();
                    return;
                }
                if (!tb_grupos.Checked && !tb_categorias.Checked && !tb_pacotes.Checked && 
                    !tb_servicos.Checked && !tb_arquivos.Checked && !tb_mapeamentos.Checked &&
                    !tb_perfis.Checked && !tb_usuarios.Checked && !tb_parametros.Checked       )
                {
                    MessageBox.Show("Favor selecionar uma tabela.");
                    edtNomeArquivo.Focus();
                    return;
                }

                if (tb_grupos.Checked || tb_categorias.Checked || tb_pacotes.Checked ||
                    tb_servicos.Checked || tb_arquivos.Checked || tb_mapeamentos.Checked)
                {
                    GeraXml(expImpTabelas.GetScriptExportar(
                                    tb_grupos, tb_categorias, tb_pacotes, tb_servicos, tb_arquivos, tb_mapeamentos,
                                    edtGrupo, edtCategoria, edtPacote, edtServico, edtArquivo, edtMapeamento));
                }
                if (tb_perfis.Checked || tb_usuarios.Checked)
                {
                    GeraXml(expImpTabelas.GetScriptExportacaoPerfil(tb_perfis, tb_usuarios, edtPerfil, edtUsuario));
                }
                if (tb_parametros.Checked)
                {
                    GeraXml(expImpTabelas.GetScriptExportacaoParametros(tb_parametros, edtParametro));
                }
                Global.EnviarParaLog($"Exportação de tabelas concluida.", txtArquivo, "FimExportação");

            }
            if (rbImportar.Checked)
            {
                if (edtNomeArquivo.Text.Length == 0)
                {
                    MessageBox.Show("Favor informar o Arquivo que deseja importar.");
                    edtNomeArquivo.Focus();
                    return;
                }
                Global.EnviarParaLog($"Importando Xml {edtNomeArquivo.Text} aguarde...", txtArquivo, "IniImportação");
                //Importar(edtNomeArquivo.Text);
                ImportarXml(edtNomeArquivo.Text);
                Global.EnviarParaLog($"Importação do Xml {edtNomeArquivo.Text} concluída...", txtArquivo, "FimImportação");
            }
        }

        public string GetNomeXmlExportadoGrupo()
        {
            string Result = "";
            if (tb_grupos.Checked || tb_categorias.Checked || tb_pacotes.Checked || tb_servicos.Checked || tb_arquivos.Checked || tb_mapeamentos.Checked)
            {
                if (tb_grupos.Checked) { Result = "Empresa"; }
                if (tb_categorias.Checked) { Result = "Filial"; }
                if (tb_pacotes.Checked) { Result = "Pacotes"; }
                if (tb_servicos.Checked) { Result = "Servicos"; }
                if (tb_arquivos.Checked) { Result = "Arquivos"; }
                if (tb_mapeamentos.Checked) { Result = "Mapeamentos"; }
            }

            string Chave = edtGrupo.Text.PadLeft(7, '0') +"-"+edtCategoria.Text.PadLeft(7, '0') + "-" + edtPacote.Text.PadLeft(7, '0') + "-" +
                edtServico.Text.PadLeft(7, '0') + "-" + edtArquivo.Text.PadLeft(7, '0') + "-" + edtMapeamento.Text.PadLeft(7, '0');

            return Result+"_"+Chave;
        }

        public string GetNomeXmlExportadoPerfil()
        {
            string Result = "";
            if (tb_perfis.Checked || tb_usuarios.Checked )
            {
                if (tb_perfis.Checked) { Result = "Perfil"; }
                if (tb_usuarios.Checked) { Result = "Usuarios"; }
            }

            string Chave = edtPerfil.Text.PadLeft(7, '0') + "-" + edtUsuario.Text.PadLeft(7, '0');

            return Result + "_" + Chave;
        }
        public string GetNomeXmlExportadoParametros()
        {
            string Result = "";
            if (tb_parametros.Checked)
            {
                if (tb_parametros.Checked) { Result = "Parametros"; }
            }

            string Chave = edtParametro.Text.PadLeft(7,'0');

            return Result + "_" + Chave;
        }

        public void GeraXml(string script)
        {
            if (!expImpTabelas.conexao.ConexaoAberta(expImpTabelas.conexao.Conn))
            {
                expImpTabelas.conexao.AbrirConexao(expImpTabelas.conexao.Conn);
            }
            try
            {
                SqlCommand cmd = new SqlCommand(script, expImpTabelas.conexao.Conn);
                Global.EnviarParaLog("Gerando Xml aguarde...",txtArquivo,"Exportação");
                XmlReader xmldr = cmd.ExecuteXmlReader();
                if (xmldr.Read())
                {
                    XDocument doc = new XDocument();
                    doc = XDocument.Load(xmldr);
                    string Path = Global.LastCaracterDifereDeBarra(edtNomeArquivo.Text);
                    if (script.ToLower().IndexOf("tb_grupos") > 0)
                    {
                        string NomeArquivo = Path + GetNomeXmlExportado("Empresa") + ".xml";
                        File.WriteAllText(NomeArquivo, doc.ToString());
                        Global.EnviarParaLog($"Arquivo {NomeArquivo} gerado com sucesso.", txtArquivo, "Exportação");
                    }
                    else if (script.ToLower().IndexOf("tb_perfis") > 0)
                    {
                        string NomeArquivo = Path + GetNomeXmlExportado("Perfil") + ".xml";
                        File.WriteAllText(NomeArquivo, doc.ToString());
                        Global.EnviarParaLog($"Arquivo {NomeArquivo} gerado com sucesso.", txtArquivo, "Exportação");
                    }
                    else if (script.ToLower().IndexOf("tb_parametros") > 0)
                    {
                        string NomeArquivo = Path + GetNomeXmlExportado("Parametro") + ".xml";
                        File.WriteAllText(NomeArquivo, doc.ToString());
                        Global.EnviarParaLog($"Arquivo {NomeArquivo} gerado com sucesso.", txtArquivo, "Exportação");
                    }
                }

            }
            finally
            {
                expImpTabelas.conexao.FecharConexao(expImpTabelas.conexao.Conn);
            }

        }

        public string GetNomeXmlExportado(string pNome)
        {
            string Result = "";
            if (pNome=="Empresa")
            {
                Result= GetNomeXmlExportadoGrupo();
            }
            else if (pNome == "Perfil")
            {
                Result = GetNomeXmlExportadoPerfil();
            }
            else if (pNome == "Parametro")
            {
                Result = GetNomeXmlExportadoParametros();
            }
            return Result;
        }
        private void Importar(string pNomeArqXml)
        {
            int Operacao = 1;
            string IdGrupo = "";
            string IdCateg = "";
            string IdPacote = "";
            string IdServico = "";
            string IdArquivo = "";
            string IdPerfil = "";
            string IdUsuario = "";
            string msgRetorno = "";

            int IdGrupoAtual = 0;
            int IdCategAtual = 0;
            int IdPacoteAtual = 0;
            int IdServicoAtual = 0;
            int IdArquivoAtual = 0;
            int IdPerfilAtual = 0;
            int IdUsuarioAtual = 0;

            //XmlDocument DocXml = new XmlDocument();
            //DocXml.Load(pNomeArqXml);
            //foreach(XmlNode Tabela in DocXml.DocumentElement)
            //{
            //    string nmCampo = Tabela.Name;
            //    string vlrCampo = Tabela.InnerText;
            //    try
            //    {
            //        PopulaModel(nmCampo, vlrCampo);
            //    }
            //    catch(Exception ex)
            //    {
            //        Global.EnviarParaLog($"Erro ao gerar a lista do xml. Motivo:{ex.Message}", txtArquivo, "ExpGrupo");
            //    }
            //}

            StreamReader XmlRead = new StreamReader(pNomeArqXml);
            string Linha;
            while ((Linha=XmlRead.ReadLine())!=null)
            {
                string nmCampo = "";
                string vlrCampo = "";
                if (Linha.Trim() == "<g>") { gruposModel.Clear(); }
                else if (Linha.Trim() == "<c>") { categoriasModel.Clear(); }
                else if (Linha.Trim() == "<p>") { pacotesModel.Clear(); }
                else if (Linha.Trim() == "<s>") { servicosModel.Clear(); }
                else if (Linha.Trim() == "<a>") { arquivosModel.Clear(); }
                else if (Linha.Trim() == "<ma>") { mapeamentosModel.Clear(); }
                else if (Linha.Trim() == "<mo>") { monitoramentosModel.Clear(); }

                else if (Linha.Trim() == "<pf>") { perfisModel.Clear(); }
                else if (Linha.Trim() == "<u>") { usuariosModel.Clear(); }
                else if (Linha.Trim() == "<o>") { objetosModel.Clear(); }
                else if (Linha.Trim() == "<pe>") { permissoesModel.Clear(); }

                else if (Linha.Trim() == "pa") { parametrosModel.Clear(); }
                else
                {
                    string Lin = Linha.Trim();
                    nmCampo = Lin.Substring(1, Lin.IndexOf(">") - 1);
                    Lin = Lin.Trim().Replace($"<{nmCampo}>", "");
                    Lin = Lin.Trim().Replace($"</{nmCampo}>", "");
                    vlrCampo = Lin.Trim();
                    
                    PopulaModel(nmCampo, vlrCampo);
                }
                if (Linha.Trim() == "</g>") { ExpImp.Grupos.Add(new GruposModel() {
                    id_grupo = gruposModel.id_grupo,
                    nm_grupo = gruposModel.nm_grupo,
                    status = gruposModel.status,
                    descr_grupos = gruposModel.descr_grupos,
                    data_atualizacao = gruposModel.data_atualizacao
                }); }
                else if (Linha.Trim() == "</c>") { ExpImp.Categorias.Add(new CategoriasModel() {
                    id_categoria = categoriasModel.id_categoria,
                    id_grupo = categoriasModel.id_grupo,
                    nm_categoria = categoriasModel.nm_categoria,
                    status = categoriasModel.status,
                    descr_categorias = categoriasModel.descr_categorias,
                    data_atualizacao = categoriasModel.data_atualizacao
                }); }
                else if (Linha.Trim() == "</p>") { ExpImp.Pacotes.Add(new PacotesModel() {
                    id_pacote = pacotesModel.id_pacote,
                    id_categoria = pacotesModel.id_categoria,
                    nm_pacote = pacotesModel.nm_pacote,
                    status = pacotesModel.status,
                    descr_pacotes = pacotesModel.descr_pacotes,
                    data_atualizacao = pacotesModel.data_atualizacao
                }); }
                else if (Linha.Trim() == "</s>") { ExpImp.Servicos.Add(new ServicosModel() {
                    id_servico = servicosModel.id_servico,
                    nm_servico = servicosModel.nm_servico,
                    status = servicosModel.status,
                    situacao = servicosModel.situacao,
                    data_atualizacao = servicosModel.data_atualizacao
                }); }
                else if (Linha.Trim() == "</a>") { ExpImp.Arquivos.Add(new ArquivosModel() {
                    id_arquivo = arquivosModel.id_arquivo,
                    id_pacote = arquivosModel.id_pacote,
                    nm_arquivo = arquivosModel.nm_arquivo,
                    mascara_arquivo = arquivosModel.mascara_arquivo,
                    tp_carga = arquivosModel.tp_carga,
                    tp_arquivo = arquivosModel.tp_arquivo,
                    delimitador = arquivosModel.delimitador,
                    cercador = arquivosModel.cercador,
                    tb_destino = arquivosModel.tb_destino,
                    dir_entrada = arquivosModel.dir_entrada,
                    dir_saida = arquivosModel.dir_saida,
                    rbd_tabela = arquivosModel.rbd_tabela,
                    rbd_indice = arquivosModel.rbd_indice,
                    nm_Planilha = arquivosModel.nm_Planilha,
                    status = arquivosModel.status,
                    data_atualizacao = arquivosModel.data_atualizacao,
                    FirstLine=arquivosModel.FirstLine,
                    LineFeed=arquivosModel.LineFeed
                }); }
                else if (Linha.Trim() == "</ma>") { ExpImp.Mapeamentos.Add(new MapeamentosModel() {
                    id_mapeamento = mapeamentosModel.id_mapeamento,
                    id_arquivo = mapeamentosModel.id_arquivo,
                    nm_coluna = mapeamentosModel.nm_coluna,
                    ordem = mapeamentosModel.ordem,
                    fixo_inicio = mapeamentosModel.fixo_inicio,
                    fixo_tamanho = mapeamentosModel.fixo_tamanho,
                    tp_coluna = mapeamentosModel.tp_coluna,
                    tm_coluna = mapeamentosModel.tm_coluna,
                    pr_coluna = mapeamentosModel.pr_coluna,
                    MASK_CAMPO = mapeamentosModel.MASK_CAMPO,
                    ExpressaoSql = mapeamentosModel.ExpressaoSql,
                    data_atualizacao = mapeamentosModel.data_atualizacao
                }); }
                else if (Linha.Trim() == "</mo>") { ExpImp.Monitoramentos.Add(new MonitoramentosModel() {
                    id_arquivo = monitoramentosModel.id_arquivo,
                    id_servico = monitoramentosModel.id_servico,
                    status = monitoramentosModel.status,
                    data_atualizacao = monitoramentosModel.data_atualizacao
                }); }
                else if (Linha.Trim() == "</pf>") { ExpImp.Perfis.Add(new PerfisModel() {
                    Id_perfil = perfisModel.Id_perfil,
                    Nm_perfil = perfisModel.Nm_perfil,
                    Status = perfisModel.Status,
                    Data_atualizacao = perfisModel.Data_atualizacao
                }); }
                else if (Linha.Trim() == "</u>") { ExpImp.Usuarios.Add(new UsuariosModel() {
                    Id_usuario = usuariosModel.Id_usuario,
                    Id_perfil = usuariosModel.Id_perfil,
                    Nm_usuario = usuariosModel.Nm_usuario,
                    Login = usuariosModel.Login,
                    Senha = usuariosModel.Senha,
                    Email = usuariosModel.Email,
                    Status = usuariosModel.Status,
                    Data_atualizacao = usuariosModel.Data_atualizacao
                });  }
                else if (Linha.Trim() == "</o>") { ExpImp.Objetos.Add(new ObjetosModel() {
                    Id_objeto = objetosModel.Id_objeto,
                    Nm_objeto = objetosModel.Nm_objeto,
                    Tp_objeto = objetosModel.Tp_objeto,
                    Status = objetosModel.Status,
                    Data_atualizacao = objetosModel.Data_atualizacao
                }); }
                else if (Linha.Trim() == "</pe>") { ExpImp.Permissoes.Add(new PermissoesModel() {
                    Id_permissao = permissoesModel.Id_permissao,
                    Id_perfil = permissoesModel.Id_perfil,
                    Id_objeto = permissoesModel.Id_objeto,
                    Permissao = permissoesModel.Permissao,
                    Status = permissoesModel.Status,
                    Data_atualizacao = permissoesModel.Data_atualizacao
                }); }
                else if (Linha.Trim() == "</pa>") { ExpImp.Parametros.Add(new ParametrosModel() {
                    id_parametro = parametrosModel.id_parametro,
                    nm_parametro = parametrosModel.nm_parametro,
                    valor = parametrosModel.valor,
                    status = parametrosModel.status,
                    data_atualizacao = parametrosModel.data_atualizacao
                }); }
            }


            foreach (GruposModel Grupos in ExpImp.Grupos)
            {
                GruposModel grp = new GruposModel();
                grp.id_grupo = Grupos.id_grupo;
                grp.nm_grupo = Grupos.nm_grupo;
                grp.descr_grupos = Grupos.descr_grupos;
                grp.status = Grupos.status;
                grp.data_criacao = Grupos.data_criacao;
                grp.data_atualizacao = Grupos.data_atualizacao;

            IdGrupoAtual = grp.id_grupo;
                IdGrupo = gruposController.GetIdGrupo(grp.nm_grupo).ToString();
                Operacao = Global.GetOperacao(IdGrupo);
                if (Operacao == 2)
                {
                    grp.id_grupo = Global.StrToInt(IdGrupo);
                }
                try
                {
                    msgRetorno = gruposController.CadGruposBase.PersisteNoBanco(grp, Operacao);
                    Global.EnviarParaLog(msgRetorno, txtArquivo, "ExpGrupo");
                    if (Operacao == 1)
                    {
                        IdGrupo = gruposController.CadGruposBase.GetLastReg(grp).ToString();
                    }
                }
                catch (Exception ex)
                {
                    Global.EnviarParaLog($"{msgRetorno} Motivo:{ex.Message}", txtArquivo, "ExpGrupo");
                }

                foreach (CategoriasModel Categorias in ExpImp.Categorias)
                {
                    CategoriasModel cm = new CategoriasModel();
                    cm.id_categoria     = Categorias.id_categoria;
                    cm.id_grupo         = Categorias.id_grupo;
                    cm.nm_categoria     = Categorias.nm_categoria;
                    cm.descr_categorias = Categorias.descr_categorias;
                    cm.status           = Categorias.status;
                    cm.data_criacao     = Categorias.data_criacao;
                    cm.data_atualizacao = Categorias.data_atualizacao;


                    IdCategAtual = cm.id_categoria;
                    if (Categorias.id_grupo == IdGrupoAtual)
                    {
                        IdCateg = categoriasController.GetIdCategoria(cm.nm_categoria).ToString();
                        Operacao = Global.GetOperacao(IdCateg);
                        if (Operacao == 2)
                        {
                            cm.id_categoria = Global.StrToInt(IdCateg);
                        }
                        try
                        {
                            cm.id_grupo = Global.StrToInt(IdGrupo);
                            msgRetorno = categoriasController.CadCategoriasBase.PersisteNoBanco(cm, Operacao);
                            Global.EnviarParaLog(msgRetorno, txtArquivo, "ExpCategoria");
                            if (Operacao == 1)
                            {
                                IdCateg = categoriasController.CadCategoriasBase.GetLastReg(cm).ToString();
                            }
                        }
                        catch (Exception ex)
                        {
                            Global.EnviarParaLog($"{msgRetorno} Motivo:{ex.Message}", txtArquivo, "ExpCategoria");
                        }
                    }
                    foreach (PacotesModel Pacote in ExpImp.Pacotes)
                    {
                        PacotesModel pm =new PacotesModel();
                        pm.id_pacote = Pacote.id_pacote;
                        pm.id_categoria = Pacote.id_categoria;
                        pm.nm_pacote = Pacote.nm_pacote;
                        pm.descr_pacotes = Pacote.descr_pacotes;
                        pm.status = Pacote.status;
                        pm.data_criacao = Pacote.data_criacao;
                        pm.data_atualizacao = Pacote.data_atualizacao;

                        IdPacoteAtual = pm.id_pacote;
                        if (pm.id_categoria == IdCategAtual)
                        {
                            IdPacote = pacotesController.CadPacotesBase.GetidPacote(pm.nm_pacote).ToString();
                            Operacao = Global.GetOperacao(IdPacote);
                            if (Operacao == 2)
                            {
                                pm.id_pacote = Global.StrToInt(IdPacote);
                            }
                            try
                            {
                                pm.id_categoria = Global.StrToInt(IdCateg);
                                msgRetorno = pacotesController.CadPacotesBase.PersisteNoBanco(pm, Operacao);
                                Global.EnviarParaLog(msgRetorno, txtArquivo, "ExpPacotes");
                                if (Operacao == 1)
                                {
                                    IdPacote = pacotesController.CadPacotesBase.GetLastReg(pm).ToString();
                                }
                            }
                            catch (Exception ex)
                            {
                                Global.EnviarParaLog($"{msgRetorno} Motivo:{ex.Message}", txtArquivo, "ExpCategoria");
                            }

                        }

                        foreach (MonitoramentosModel Monitoramento in ExpImp.Monitoramentos)
                        {
                            foreach(ServicosModel Servicos in ExpImp.Servicos)
                            {
                                ServicosModel sm = new ServicosModel();
                                sm.id_servico          = Servicos.id_servico;
                                sm.nm_servico          = Servicos.nm_servico      ;
                                sm.situacao            = Servicos.situacao        ;
                                sm.status              = Servicos.status          ;
                                sm.data_criacao        = Servicos.data_criacao    ;
                                sm.data_atualizacao    = Servicos.data_atualizacao;

                                IdServicoAtual = sm.id_servico;
                                if (IdServicoAtual == Monitoramento.id_servico)
                                {
                                    IdServico = servicosController.GetIdServico(sm.nm_servico).ToString();
                                    Operacao = Global.GetOperacao(IdServico);
                                    if (Operacao == 2)
                                    {
                                        sm.id_servico = Global.StrToInt(IdServico);
                                    }
                                    try
                                    {
                                        msgRetorno = servicosController.CadServicosBase.PersisteNoBanco(sm, Operacao);
                                        Global.EnviarParaLog(msgRetorno, txtArquivo, "ExpServicos");
                                        if (Operacao == 1)
                                        {
                                            IdServico = servicosController.CadServicosBase.GetLastReg(sm).ToString();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Global.EnviarParaLog($"{msgRetorno} Motivo:{ex.Message}", txtArquivo, "ExpServicos");
                                    }

                                    foreach (ArquivosModel Arquivos in ExpImp.Arquivos)
                                    {
                                        ArquivosModel am = new ArquivosModel();
                                        am.SetDados(Arquivos);
                                        

                                        IdArquivoAtual = am.id_arquivo;
                                        if (Arquivos.id_pacote == IdPacoteAtual)
                                        {
                                            IdArquivo = arquivosController.GetIdArquivo(am.nm_arquivo, am.dir_entrada).ToString();
                                            Operacao = Global.GetOperacao(IdArquivo);
                                            if (Operacao == 2)
                                            {
                                                am.id_arquivo = Global.StrToInt(IdArquivo);
                                            }
                                            try
                                            {
                                                am.id_pacote = Global.StrToInt(IdPacote);
                                                msgRetorno = arquivosController.CadArquivosBase.PersisteNoBanco(am, Operacao);
                                                Global.EnviarParaLog(msgRetorno, txtArquivo, "ExpArquivos");
                                                if (Operacao == 1)
                                                {
                                                    IdArquivo = arquivosController.CadArquivosBase.GetLastReg(am).ToString();
                                                }
                                                try
                                                {
                                                    MonitoramentosModel Monitor = new MonitoramentosModel();
                                                    Monitor.id_servico = Global.StrToInt(IdServico);
                                                    Monitor.id_arquivo = Global.StrToInt(IdArquivo);
                                                    Monitor.status = "ativo";
                                                    if (!monitoramentosController.MonitoramentoCadastrado(Monitor.id_servico, Monitor.id_arquivo))
                                                    {
                                                        msgRetorno = monitoramentosController.CadMonitoramentosBase.PersisteNoBanco(Monitor, 1);
                                                    }
                                                    Global.EnviarParaLog(msgRetorno, txtArquivo, "ExpMonitoramento");
                                                }
                                                catch (Exception ex)
                                                {
                                                    Global.EnviarParaLog($"{msgRetorno} Motivo:{ex.Message}", txtArquivo, "ExpMonitoramentos");
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                Global.EnviarParaLog($"{msgRetorno} Motivo:{ex.Message}", txtArquivo, "ExpArquivos");
                                            }

                                            foreach (MapeamentosModel Mapeamentos in ExpImp.Mapeamentos)
                                            {
                                                MapeamentosModel mm = new MapeamentosModel();
                                                mm.SetDados(Mapeamentos);

                                                if (IdArquivoAtual == mm.id_arquivo)
                                                {
                                                    string IdMapeamento = mapeamentosController.GetIdMapeamento(am.id_arquivo, mm.nm_coluna).ToString();
                                                    Operacao = Global.GetOperacao(IdMapeamento);
                                                    if (Operacao == 2)
                                                    {
                                                        mm.id_mapeamento= Global.StrToInt(IdMapeamento);
                                                    }
                                                    try
                                                    {
                                                        mm.id_arquivo = Global.StrToInt(IdArquivo);
                                                        msgRetorno = mapeamentosController.CadMaptoBase.PersisteNoBanco(mm, Operacao);
                                                        Global.EnviarParaLog(msgRetorno, txtArquivo, "ExpMapeamentos");
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        Global.EnviarParaLog($"{msgRetorno} Motivo:{ex.Message}", txtArquivo, "ExpMapeamentos");
                                                    }
                                                }
                                            }//Fim Cadastro Mapeamentos
                                        }
                                    }//Fim cadastro Arquivos
                                }
                             
                            }//Fim Cadastro de Serviços
                         }//Fim Cadastro Monitoramentos
                    }//Fim Cadastro Pacotes
                }//Fim Cadastro Categoria
            }//Fim Cadastro de Grupos

            foreach (PerfisModel Perfil in ExpImp.Perfis)
            {
                IdPerfil = perfisController.CadPerfisBase.GetidPerfil(Perfil.Nm_perfil).ToString();
                Operacao = Global.GetOperacao(IdPerfil);
                if (Operacao == 2)
                {
                    Perfil.Id_perfil = Global.StrToInt(IdPerfil);
                }
                try
                {
                    msgRetorno = perfisController.CadPerfisBase.PersisteNoBanco(Perfil, Operacao);
                    Global.EnviarParaLog(msgRetorno, txtArquivo, "ExpPerfil");
                    IdPerfil = perfisController.CadPerfisBase.GetLastReg(perfisModel).ToString();
                }
                catch (Exception ex)
                {
                    Global.EnviarParaLog($"{msgRetorno} Motivo:{ex.Message}", txtArquivo, "ExpPerfil");
                }

                foreach (UsuariosModel Usuario in ExpImp.Usuarios)
                {
                    if (Usuario.Id_perfil == Perfil.Id_perfil)
                    {
                        IdUsuario = usuariosController.GetIdUsuario(Usuario.Nm_usuario).ToString();
                        Operacao = Global.GetOperacao(IdUsuario);
                        if (Operacao == 2)
                        {
                            Usuario.Id_usuario = Global.StrToInt(IdUsuario);
                        }
                        try
                        {
                            Usuario.Id_perfil = Global.StrToInt(IdPerfil);
                            msgRetorno = usuariosController.CadUsuariosBase.PersisteNoBanco(Usuario, Operacao);
                            Global.EnviarParaLog(msgRetorno, txtArquivo, "ExpUsuarios");
                        }
                        catch (Exception ex)
                        {
                            Global.EnviarParaLog($"{msgRetorno} Motivo:{ex.Message}", txtArquivo, "ExpUsuarios");
                        }
                    }
                }//Fim Cadastro Usuarios

                foreach (PermissoesModel Permissao in ExpImp.Permissoes)
                {
                    foreach (ObjetosModel Objeto in ExpImp.Objetos)
                    {
                        if (Permissao.Id_perfil == Perfil.Id_perfil && Permissao.Id_objeto==Objeto.Id_objeto)
                        {
                            string IdPermissao = permissoesController.GetIdPermissao(perfisController.CadPerfisBase.GetidPerfil(Perfil.Nm_perfil),objetosController.GetIdMenu(Objeto.Nm_objeto)).ToString();
                            Operacao = Global.GetOperacao(IdPermissao);
                            if (Operacao == 2)
                            {
                                Permissao.Id_permissao = Global.StrToInt(IdPermissao);
                            }
                            try
                            {
                                Permissao.Id_perfil = Global.StrToInt(IdPerfil);
                                Permissao.Id_objeto = objetosController.GetIdMenu(Objeto.Nm_objeto);
                                msgRetorno = permissoesController.CadPermissoesBase.PersisteNoBanco(Permissao, 1);
                                Global.EnviarParaLog(msgRetorno, txtArquivo, "ExpPermissao");
                            }
                            catch (Exception ex)
                            {
                                Global.EnviarParaLog($"{msgRetorno} Motivo:{ex.Message}", txtArquivo, "ExpPermissao");
                            }
                        }
                    }
                }//Fim Cadastro Usuarios

            }//Fim Cadastro Perfil

            foreach (ParametrosModel Parametro in ExpImp.Parametros)
            {
                string IdParametro = parametrosController.GetIdParametro(Parametro.nm_parametro).ToString();
                Operacao = Global.GetOperacao(IdParametro);
                if (Operacao == 2)
                {
                    Parametro.id_parametro = Global.StrToInt(IdParametro);
                }
                try
                {
                        msgRetorno = parametrosController.CadParametrosBase.PersisteNoBanco(Parametro, Operacao);
                        Global.EnviarParaLog(msgRetorno, txtArquivo, "ExpParametro");
                    }
                    catch (Exception ex)
                    {
                        Global.EnviarParaLog($"{msgRetorno} Motivo:{ex.Message}", txtArquivo, "ExpParametro");
                    }
            }//Fim Cadastro Parametros
            ExpImp.Servicos.Clear();
            ExpImp.Permissoes.Clear();
            ExpImp.Perfis.Clear();
            ExpImp.Parametros.Clear();
            ExpImp.Pacotes.Clear();
            ExpImp.Objetos.Clear();
            ExpImp.Monitoramentos.Clear();
            ExpImp.Mapeamentos.Clear();
            ExpImp.Grupos.Clear();
            ExpImp.Categorias.Clear();
            ExpImp.Arquivos.Clear();

        }


        public void PopulaModel(string pNomeCampo, string pValor)
        {
            switch (pNomeCampo)
            {
                case "Grp_Id_Grupo":
                    gruposModel.id_grupo = Global.StrToInt(pValor);
                    gruposModel.data_atualizacao = DateTime.Now;
                    break;
                case "nm_grupo":
                    gruposModel.nm_grupo = pValor;
                    break;
                case "Grp_Status":
                    gruposModel.status = pValor;
                    break;
                case "DESCR_GRUPOS":
                    gruposModel.descr_grupos = pValor;
                    break;
                case "id_categoria":
                    categoriasModel.id_categoria = Global.StrToInt(pValor);
                    categoriasModel.data_atualizacao = DateTime.Now;
                    break;
                case "Cat_Id_Grupo":
                    categoriasModel.id_grupo = Global.StrToInt(pValor);
                    break;
                case "nm_categoria":
                    categoriasModel.nm_categoria = pValor;
                    break;
                case "Cat_Status":
                    categoriasModel.status = pValor;
                    break;
                case "DESCR_CATEGORIAS":
                    categoriasModel.descr_categorias = pValor;
                    break;
                case "id_pacote":
                    pacotesModel.id_pacote = Global.StrToInt(pValor);
                    pacotesModel.data_atualizacao = DateTime.Now;
                    break;
                case "pac_id_categoria":
                    pacotesModel.id_categoria = Global.StrToInt(pValor);
                    break;
                case "nm_pacote":
                    pacotesModel.nm_pacote = pValor;
                    break;
                case "pac_status":
                    pacotesModel.status = pValor;
                    break;
                case "DESCR_PACOTES":
                    pacotesModel.descr_pacotes = pValor;
                    break;
                case "Ser_id_Servico":
                    servicosModel.id_servico = Global.StrToInt(pValor);
                    servicosModel.data_atualizacao = DateTime.Now;
                    break;
                case "NM_SERVICO":
                    servicosModel.nm_servico = pValor;
                    break;
                case "ser_status":
                    servicosModel.status = pValor;
                    break;
                case "Situacao":
                    servicosModel.situacao = pValor;
                    break;
                case "Mon_id_arquivo":
                    monitoramentosModel.id_arquivo =Global.StrToInt(pValor);
                    monitoramentosModel.data_atualizacao = DateTime.Now;
                    break;
                case "mon_id_servico":
                    monitoramentosModel.id_servico = Global.StrToInt(pValor);
                    break;
                case "mon_status":
                    monitoramentosModel.status = pValor;
                    break;
                case "arq_idarquivo":
                    arquivosModel.id_arquivo = Global.StrToInt(pValor);
                    arquivosModel.data_atualizacao = DateTime.Now;
                    break;
                case "arq_id_pacote":
                    arquivosModel.id_pacote = Global.StrToInt(pValor);
                    break;
                case "nm_arquivo":
                    arquivosModel.nm_arquivo = pValor;
                    break;
                case "mascara_arquivo":
                    arquivosModel.mascara_arquivo = pValor;
                    break;
                case "tp_carga":
                    arquivosModel.tp_carga = pValor;
                    break;
                case "tp_arquivo":
                    arquivosModel.tp_arquivo = pValor;
                    break;
                case "tb_destino":
                    arquivosModel.tb_destino = pValor;
                    break;
                case "dir_entrada":
                    arquivosModel.dir_entrada = pValor;
                    break;
                case "dir_saida":
                    arquivosModel.dir_saida = pValor;
                    break;
                case "rbd_tabela":
                    arquivosModel.rbd_tabela = pValor;
                    break;
                case "rbd_indice":
                    arquivosModel.rbd_indice = pValor;
                    break;
                case "arq_status":
                    arquivosModel.status = pValor;
                    break;
                case "delimitador":
                    arquivosModel.delimitador = pValor;
                    break;
                case "LineFeed":
                    arquivosModel.LineFeed = pValor;
                    break;
                case "FirstLine":
                    arquivosModel.FirstLine = Global.StrToInt(pValor);
                    break;
                case "id_mapeamento":
                    mapeamentosModel.id_mapeamento = Global.StrToInt(pValor);
                    mapeamentosModel.data_atualizacao = DateTime.Now;
                    break;
                case "map_id_arquivo":
                    mapeamentosModel.id_arquivo = Global.StrToInt(pValor);
                    break;
                case "nm_coluna":
                    mapeamentosModel.nm_coluna = pValor;
                    break;
                case "ordem":
                    mapeamentosModel.ordem = Global.StrToInt(pValor);
                    break;
                case "fixo_inicio":
                    mapeamentosModel.fixo_inicio = Global.StrToInt(pValor);
                    break;
                case "fixo_tamanho":
                    mapeamentosModel.fixo_tamanho = Global.StrToInt(pValor);
                    break;
                case "tp_coluna":
                    mapeamentosModel.tp_coluna = pValor;
                    break;
                case "tm_coluna":
                    mapeamentosModel.tm_coluna = Global.StrToInt(pValor); 
                    break;
                case "pr_coluna":
                    mapeamentosModel.pr_coluna = Global.StrToInt(pValor);
                    break;
                case "MASK_CAMPO":
                    mapeamentosModel.MASK_CAMPO = pValor;
                    break;
                case "ExpressaoSql":
                    mapeamentosModel.ExpressaoSql = pValor;
                    break;
                case "Per_id_perfil":
                    perfisModel.Id_perfil = Global.StrToInt(pValor);
                    perfisModel.Data_atualizacao = DateTime.Now;
                    break;
                case "nm_perfil":
                    perfisModel.Nm_perfil = pValor;
                    break;
                case "Per_Status":
                    perfisModel.Status = pValor;
                    break;
                case "id_usuario":
                    usuariosModel.Id_usuario = Global.StrToInt(pValor);
                    usuariosModel.Data_atualizacao = DateTime.Now;
                    break;
                case "Usu_id_perfil":
                    usuariosModel.Id_perfil = Global.StrToInt(pValor);
                    break;
                case "nm_usuario":
                    usuariosModel.Nm_usuario = pValor;
                    break;
                case "login":
                    usuariosModel.Login = pValor;
                    break;
                case "senha":
                    usuariosModel.Senha = pValor;
                    break;
                case "email":
                    usuariosModel.Email = pValor;
                    break;
                case "Usu_Status":
                    usuariosModel.Status = pValor;
                    break;
                case "Obj_id_objeto":
                    objetosModel.Id_objeto = Global.StrToInt(pValor);
                    objetosModel.Data_atualizacao = DateTime.Now;
                    break;
                case "nm_objeto":
                    objetosModel.Nm_objeto = pValor;
                    break;
                case "tp_objeto":
                    objetosModel.Tp_objeto = pValor;
                    break;
                case "Obj_Status":
                    objetosModel.Status = pValor;
                    break;
                case "id_permissao":
                    permissoesModel.Id_permissao = Global.StrToInt(pValor);
                    permissoesModel.Data_atualizacao = DateTime.Now;
                    break;
                case "Prm_id_perfil":
                    permissoesModel.Id_perfil = Global.StrToInt(pValor);
                    break;
                case "Prm_id_objeto":
                    permissoesModel.Id_objeto = Global.StrToInt(pValor);
                    break;
                case "permissao":
                    permissoesModel.Permissao = pValor;
                    break;
                case "Prm_Status":
                    permissoesModel.Status = pValor;
                    break;
                case "id_parametro":
                    parametrosModel.id_parametro = Global.StrToInt(pValor);
                    parametrosModel.data_atualizacao = DateTime.Now;
                    break;
                case "nm_parametro":
                    parametrosModel.nm_parametro = pValor;
                    break;
                case "valor":
                    parametrosModel.valor = pValor;
                    break;
                case "status":
                    parametrosModel.status = pValor;
                    break;
                default:
                    break;


                    
            }

        }
        public void ImportarUsuarios(string pPathTabela)
        {
            string[] Arquivo;
            if (File.Exists(pPathTabela))
            {
                UsuariosController usuControl = new UsuariosController();
                List<UsuariosModel> Usuarios = new List<UsuariosModel>();
                Arquivo = File.ReadAllLines(pPathTabela);
                for (int i = 0; i < Arquivo.Length; i++)
                {
                    string[] Valores = Arquivo[i].Split(';');
                    Usuarios.Add(
                        new UsuariosModel()
                        {
                            Id_perfil = Convert.ToInt32(usuControl.CadUsuariosBase.isNullResultZero(Valores[1].ToString())),
                            Id_usuario = Convert.ToInt32(usuControl.CadUsuariosBase.isNullResultZero(Valores[2].ToString())),
                            Nm_usuario = Valores[3].ToString(),
                            Login = Valores[4].ToString(),
                            Senha = Valores[5].ToString(),
                            Email = Valores[6].ToString(),
                            Status = Valores[7].ToString(),
                            Data_criacao = Convert.ToDateTime(Valores[8].ToString()),
                            Data_atualizacao = Convert.ToDateTime(Valores[9].ToString())
                        }
                    );
                }
                foreach (var lst in Usuarios)
                {
                    usuControl.CadUsuariosBase.Gravar(lst);
                }
            }

        }
        public void ImportarServicos(string pPathTabela)
        {
            string[] Arquivo;
            if (File.Exists(pPathTabela))
            {
                ServicosController Control = new ServicosController();
                List<ServicosModel> Model = new List<ServicosModel>();
                Arquivo = File.ReadAllLines(pPathTabela);
                for (int i = 0; i < Arquivo.Length; i++)
                {
                    string[] Valores = Arquivo[i].Split(';');
                    Model.Add(
                        new ServicosModel()
                        {
                            id_servico = Convert.ToInt32(Control.CadServicosBase.isNullResultZero(Valores[1].ToString())),
                            status = Valores[2].ToString(),
                            data_criacao = Convert.ToDateTime(Valores[3].ToString()),
                            data_atualizacao = Convert.ToDateTime(Valores[4].ToString()),
                            nm_servico = Valores[5].ToString(),
                            situacao = Valores[6].ToString()
                        }
                    );
                }
                foreach (var lst in Model)
                {
                    Control.CadServicosBase.Gravar(lst);
                }
            }
        }

        public void ImportarPermissoes(string pPathTabela)
        {
            string[] Arquivo;
            if (File.Exists(pPathTabela))
            {
                PermissoesController Control = new PermissoesController();
                List<PermissoesModel> Model = new List<PermissoesModel>();
                Arquivo = File.ReadAllLines(pPathTabela);
                for (int i = 0; i < Arquivo.Length; i++)
                {
                    string[] Valores = Arquivo[i].Split(';');
                    Model.Add(
                        new PermissoesModel()
                        {
                            Id_permissao = Convert.ToInt32(Control.CadPermissoesBase.isNullResultZero(Valores[1].ToString())),
                            Id_perfil = Convert.ToInt32(Control.CadPermissoesBase.isNullResultZero(Valores[2].ToString())),
                            Id_objeto = Convert.ToInt32(Control.CadPermissoesBase.isNullResultZero(Valores[3].ToString())),
                            Permissao = Valores[4].ToString(),
                            Status = Valores[5].ToString(),
                            Data_criacao = Convert.ToDateTime(Valores[6].ToString()),
                            Data_atualizacao = Convert.ToDateTime(Valores[7].ToString())
                        }
                    );
                }
                foreach (var lst in Model)
                {
                    Control.CadPermissoesBase.Gravar(lst);
                }
            }
        }


        public void ImportarPerfis(string pPathTabela)
        {
            string[] Arquivo;
            if (File.Exists(pPathTabela))
            {
                PerfisController Control = new PerfisController();
                List<PerfisModel> Model = new List<PerfisModel>();
                Arquivo = File.ReadAllLines(pPathTabela);
                for (int i = 0; i < Arquivo.Length; i++)
                {
                    string[] Valores = Arquivo[i].Split(';');
                    Model.Add(
                        new PerfisModel()
                        {
                            Id_perfil = Convert.ToInt32(Control.CadPerfisBase.isNullResultZero(Valores[1].ToString())),
                            Nm_perfil = Valores[2].ToString(),
                            Status = Valores[5].ToString(),
                            Data_criacao = Convert.ToDateTime(Valores[6].ToString()),
                            Data_atualizacao = Convert.ToDateTime(Valores[7].ToString())
                        }
                    );
                }
                foreach (var lst in Model)
                {
                    Control.CadPerfisBase.Gravar(lst);
                }
            }
        }
        public void ImportarParametros(string pPathTabela)
        {
            string[] Arquivo;
            if (File.Exists(pPathTabela))
            {
                ParametrosController Control = new ParametrosController();
                List<ParametrosModel> Model = new List<ParametrosModel>();
                Arquivo = File.ReadAllLines(pPathTabela);
                for (int i = 0; i < Arquivo.Length; i++)
                {
                    string[] Valores = Arquivo[i].Split(';');
                    Model.Add(
                        new ParametrosModel()
                        {
                            id_parametro = Convert.ToInt32(Control.CadParametrosBase.isNullResultZero(Valores[1].ToString())),
                            nm_parametro = Control.CadParametrosBase.isNullResultVazio(Valores[2]),
                            valor = Control.CadParametrosBase.isNullResultVazio(Valores[3]),
                            status = Control.CadParametrosBase.isNullResultVazio(Valores[4]),
                            data_criacao = Convert.ToDateTime(Valores[5].ToString()),
                            data_atualizacao = Convert.ToDateTime(Valores[6].ToString())
                        }
                    );
                }
                foreach (var lst in Model)
                {
                    Control.CadParametrosBase.Gravar(lst);
                }
            }
        }
        public void ImportarPacotes(string pPathTabela)
        {
            string[] Arquivo;
            if (File.Exists(pPathTabela))
            {
                PacotesController Control = new PacotesController();
                List<PacotesModel> Model = new List<PacotesModel>();
                Arquivo = File.ReadAllLines(pPathTabela);
                for (int i = 0; i < Arquivo.Length; i++)
                {
                    string[] Valores = Arquivo[i].Split(';');
                    Model.Add(
                        new PacotesModel()
                        {
                            id_pacote=Convert.ToInt32(Control.CadPacotesBase.isNullResultZero(Valores[1].ToString())),
                            nm_pacote= Control.CadPacotesBase.isNullResultVazio(Valores[2]),
                            status = Control.CadPacotesBase.isNullResultVazio(Valores[3]),
                            data_criacao = Convert.ToDateTime(Valores[4].ToString()),
                            data_atualizacao = Convert.ToDateTime(Valores[5].ToString()),
                            descr_pacotes= Control.CadPacotesBase.isNullResultVazio(Valores[6])
                        }
                    );
                }
                foreach (var lst in Model)
                {
                    Control.CadPacotesBase.Gravar(lst);
                }
            }
        }
        public void ImportarObjetos(string pPathTabela)
        {
            string[] Arquivo;
            if (File.Exists(pPathTabela))
            {
                ObjetosController Control = new ObjetosController();
                List<ObjetosModel> Model = new List<ObjetosModel>();
                Arquivo = File.ReadAllLines(pPathTabela);
                for (int i = 0; i < Arquivo.Length; i++)
                {
                    string[] Valores = Arquivo[i].Split(';');
                    Model.Add(
                        new ObjetosModel()
                        {
                            Id_objeto=Convert.ToInt32(Control.CadObjetosBase.isNullResultZero(Valores[1].ToString())),
                            Nm_objeto=Control.CadObjetosBase.isNullResultVazio(Valores[2]),
                            Tp_objeto = Control.CadObjetosBase.isNullResultVazio(Valores[3]),
                            Status = Control.CadObjetosBase.isNullResultVazio(Valores[4]),
                            Data_criacao = Convert.ToDateTime(Valores[5].ToString()),
                            Data_atualizacao = Convert.ToDateTime(Valores[6].ToString())
                        }
                    );
                }
                foreach (var lst in Model)
                {
                    Control.CadObjetosBase.Gravar(lst);
                }
            }
        }
        public void ImportarMonitoramentos(string pPathTabela)
        {
            string[] Arquivo;
            if (File.Exists(pPathTabela))
            {
                MonitoramentosController Control = new MonitoramentosController();
                List<MonitoramentosModel> Model = new List<MonitoramentosModel>();
                Arquivo = File.ReadAllLines(pPathTabela);
                for (int i = 0; i < Arquivo.Length; i++)
                {
                    string[] Valores = Arquivo[i].Split(';');
                    Model.Add(
                        new MonitoramentosModel()
                        {
                            id_arquivo = Convert.ToInt32(Control.CadMonitoramentosBase.isNullResultZero(Valores[1].ToString())),
                            id_servico = Convert.ToInt32(Control.CadMonitoramentosBase.isNullResultZero(Valores[2].ToString())),
                            status = Control.CadMonitoramentosBase.isNullResultVazio(Valores[3]),
                            data_criacao = Convert.ToDateTime(Valores[4].ToString()),
                            data_atualizacao = Convert.ToDateTime(Valores[5].ToString())
                        }
                    );
                }
                foreach (var lst in Model)
                {
                    Control.CadMonitoramentosBase.Gravar(lst);
                }
            }
        }
        public void ImportarMapeamentos(string pPathTabela)
        {
            string[] Arquivo;
            if (File.Exists(pPathTabela))
            {
                MapeamentosController Control = new MapeamentosController();
                List<MapeamentosModel> Model = new List<MapeamentosModel>();
                Arquivo = File.ReadAllLines(pPathTabela);
                for (int i = 0; i < Arquivo.Length; i++)
                {
                    string[] Valores = Arquivo[i].Split(';');
                    Model.Add(
                        new MapeamentosModel()
                        {
                            id_mapeamento = Convert.ToInt32(Control.CadMaptoBase.isNullResultZero(Valores[1].ToString())),
                            id_arquivo = Convert.ToInt32(Valores[2].ToString()),
                            nm_coluna = Valores[3].ToString(),
                            ordem = Convert.ToInt32(Control.CadMaptoBase.isNullResultZero(Valores[4].ToString())),
                            fixo_inicio = Convert.ToInt32(Control.CadMaptoBase.isNullResultZero(Valores[5].ToString())),
                            fixo_tamanho = Convert.ToInt32(Control.CadMaptoBase.isNullResultZero(Valores[6].ToString())),
                            tp_coluna = Valores[7].ToString(),
                            tm_coluna = Convert.ToInt32(Control.CadMaptoBase.isNullResultZero(Valores[8].ToString())),
                            pr_coluna = Convert.ToInt32(Control.CadMaptoBase.isNullResultZero(Valores[9].ToString())),
                            MASK_CAMPO = Valores[10].ToString(),
                            ExpressaoSql = Valores[11].ToString(),
                            data_criacao = Convert.ToDateTime(Valores[12].ToString()),
                            data_atualizacao = Convert.ToDateTime(Valores[13].ToString())
                        }
                    );
                }
                foreach (var lst in Model)
                {
                    Control.CadMaptoBase.Gravar(lst);
                }
            }
        }
        public void ImportarGrupos(string pPathTabela)
        {
            string[] Arquivo;
            if (File.Exists(pPathTabela))
            {
                GruposController Control = new GruposController();
                List<GruposModel> Model = new List<GruposModel>();
                Arquivo = File.ReadAllLines(pPathTabela);
                for (int i = 0; i < Arquivo.Length; i++)
                {
                    string[] Valores = Arquivo[i].Split(';');
                    Model.Add(
                        new GruposModel()
                        {
                            id_grupo = Convert.ToInt32(Control.CadGruposBase.isNullResultZero(Valores[1].ToString())),
                            nm_grupo = Control.CadGruposBase.isNullResultVazio(Valores[2]),
                            status = Control.CadGruposBase.isNullResultVazio(Valores[3]),
                            data_criacao = Convert.ToDateTime(Valores[4].ToString()),
                            data_atualizacao = Convert.ToDateTime(Valores[5].ToString()),
                            descr_grupos = Control.CadGruposBase.isNullResultVazio(Valores[6])
                        }
                    );
                }
                foreach (var lst in Model)
                {
                    Control.CadGruposBase.Gravar(lst);
                }
            }
        }
        public void ImportarCategorias(string pPathTabela)
        {
            string[] Arquivo;
            if (File.Exists(pPathTabela))
            {
                CategoriasController Control = new CategoriasController();
                List<CategoriasModel> Model = new List<CategoriasModel>();
                Arquivo = File.ReadAllLines(pPathTabela);
                for (int i = 0; i < Arquivo.Length; i++)
                {
                    string[] Valores = Arquivo[i].Split(';');
                    Model.Add(
                        new CategoriasModel()
                        {
                            id_categoria=Convert.ToInt32(Control.CadCategoriasBase.isNullResultZero(Valores[1].ToString())),
                            id_grupo = Convert.ToInt32(Control.CadCategoriasBase.isNullResultZero(Valores[2].ToString())),
                            nm_categoria = Control.CadCategoriasBase.isNullResultVazio(Valores[3]),
                            status = Control.CadCategoriasBase.isNullResultVazio(Valores[4]),
                            data_criacao = Convert.ToDateTime(Valores[5].ToString()),
                            data_atualizacao = Convert.ToDateTime(Valores[6].ToString()),
                            descr_categorias = Control.CadCategoriasBase.isNullResultVazio(Valores[7])
                        }
                    );
                }
                foreach (var lst in Model)
                {
                    Control.CadCategoriasBase.Gravar(lst);
                }
            }
        }
        public void ImportarArquivos(string pPathTabela)
        {
            string[] Arquivo;
            if (File.Exists(pPathTabela))
            {
                ArquivosController Control = new ArquivosController();
                List<ArquivosModel> Model = new List<ArquivosModel>();
                Arquivo = File.ReadAllLines(pPathTabela);
                for (int i = 0; i < Arquivo.Length; i++)
                {
                    string[] Valores = Arquivo[i].Split(';');
                    Model.Add(
                        new ArquivosModel()
                        {
                            id_arquivo = Convert.ToInt32(Control.CadArquivosBase.isNullResultZero(Valores[1].ToString())),
                            id_pacote = Convert.ToInt32(Control.CadArquivosBase.isNullResultZero(Valores[2].ToString())),
                            nm_arquivo = Control.CadArquivosBase.isNullResultVazio(Valores[3].ToString()),
                            mascara_arquivo = Control.CadArquivosBase.isNullResultVazio(Valores[4].ToString()),
                            tp_carga = Control.CadArquivosBase.isNullResultVazio(Valores[5].ToString()),
                            tp_arquivo = Control.CadArquivosBase.isNullResultVazio(Valores[6].ToString()),
                            delimitador = Control.CadArquivosBase.isNullResultVazio(Valores[7].ToString()),
                            cercador = Control.CadArquivosBase.isNullResultVazio(Valores[8].ToString()),
                            tb_destino = Control.CadArquivosBase.isNullResultVazio(Valores[9].ToString()),
                            dir_entrada = Control.CadArquivosBase.isNullResultVazio(Valores[10].ToString()),
                            dir_saida = Control.CadArquivosBase.isNullResultVazio(Valores[11].ToString()),
                            rbd_tabela = Control.CadArquivosBase.isNullResultVazio(Valores[12].ToString()),
                            rbd_indice = Control.CadArquivosBase.isNullResultVazio(Valores[13].ToString()),
                            nm_Planilha = Control.CadArquivosBase.isNullResultVazio(Valores[14].ToString()),
                            status = Control.CadArquivosBase.isNullResultVazio(Valores[15]),
                            data_criacao = Convert.ToDateTime(Valores[16].ToString()),
                            data_atualizacao = Convert.ToDateTime(Valores[17].ToString())
                        }
                    );
                }
                foreach (var lst in Model)
                {
                    Control.CadArquivosBase.Gravar(lst);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Global.Condicao = "";
            if (!string.IsNullOrEmpty(edtGrupo.Text))
            {
                Global.Condicao = $" {new GruposController().GetCondicaoGrupo(Global.StrToInt(edtGrupo.Text))}";
            }
            string value= MostrarPesquisa(Global.StrToInt(btnGrupo.Tag.ToString()));
            if (gruposModel.id_grupo != 0)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (edtGrupo.Text != value)
                    {
                        edtGrupo.Text = value;
                        edtCategoria.Clear();
                        edtPacote.Clear();
                        edtServico.Clear();
                        edtArquivo.Clear();
                        edtMapeamento.Clear();
                    }
                }
            }
        }

        private string MostrarPesquisa(int pTag)
        {
            string Result = "0";
            switch (pTag)
            {
                case 01:
                    gruposModel.Clear();
                    new frmPesquisa(gruposModel).ShowDialog();
                    Result = gruposModel.id_grupo.ToString();
                    break;
                case 02:
                    categoriasModel.Clear();
                    new frmPesquisa(categoriasModel).ShowDialog();
                    Result = categoriasModel.id_categoria.ToString();
                    break;
                case 03:
                    pacotesModel.Clear();
                    new frmPesquisa(pacotesModel).ShowDialog();
                    Result = pacotesModel.id_pacote.ToString();
                    break;
                case 04:
                    servicosModel.Clear();
                    new frmPesquisa(servicosModel).ShowDialog();
                    Result = servicosModel.id_servico.ToString();
                    break;
                case 05:
                    arquivosModel.Clear();
                    new frmPesquisa(arquivosModel).ShowDialog();
                    Result = arquivosModel.id_arquivo.ToString();
                    break;
                case 06:
                    mapeamentosModel.Clear();
                    new frmPesquisa(mapeamentosModel).ShowDialog();
                    Result = mapeamentosModel.id_mapeamento.ToString();
                    break;
                case 07:
                    perfisModel.Clear();
                    new frmPesquisa(perfisModel).ShowDialog();
                    Result = perfisModel.Id_perfil.ToString();
                    break;
                case 08:
                    usuariosModel.Clear();
                    new frmPesquisa(usuariosModel).ShowDialog();
                    Result = usuariosModel.Id_usuario.ToString();
                    break;
                case 09:
                    parametrosModel.Clear();
                    new frmPesquisa(parametrosModel).ShowDialog();
                    Result = parametrosModel.id_parametro.ToString();
                    break;
            }

            return Result;
        }

        private void HabilitaCampoPesquisa(CheckBox pChk,TextBox pTxtBox,Button pBtn)
        {
            pTxtBox.Visible = pChk.Checked;
            pBtn.Visible = pChk.Checked;
            if (!pChk.Checked)
            {
                pTxtBox.Clear();
            }
        }
        private void tb_grupos_CheckedChanged(object sender, EventArgs e)
        {
            HabilitaCampoPesquisa(tb_grupos, edtGrupo, btnGrupo);
        }

        private void tb_categorias_CheckedChanged(object sender, EventArgs e)
        {
            HabilitaCampoPesquisa(tb_categorias, edtCategoria, btnCategoria);
        }

        private void tb_pacotes_CheckedChanged(object sender, EventArgs e)
        {
            HabilitaCampoPesquisa(tb_pacotes, edtPacote, btnPacote);
        }

        private void tb_servicos_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void tb_servicos_CheckedChanged(object sender, EventArgs e)
        {
            HabilitaCampoPesquisa(tb_servicos, edtServico, btnServico);
        }

        private void tb_arquivos_CheckedChanged(object sender, EventArgs e)
        {
            HabilitaCampoPesquisa(tb_arquivos, edtArquivo, btnArquivo);
        }

        private void tb_mapeamentos_CheckedChanged(object sender, EventArgs e)
        {
            HabilitaCampoPesquisa(tb_mapeamentos, edtMapeamento, btnMapeamento);
        }

        private void tb_perfis_CheckedChanged(object sender, EventArgs e)
        {
            HabilitaCampoPesquisa(tb_perfis, edtPerfil, btnPerfil);
        }

        private void tb_usuarios_CheckedChanged(object sender, EventArgs e)
        {
            HabilitaCampoPesquisa(tb_usuarios, edtUsuario, btnUsuario);
        }

        private void btnCategoria_Click(object sender, EventArgs e)
        {
            Global.Condicao ="";
            if (edtGrupo.Visible && edtGrupo.Text != "")
            {
                Global.Condicao = $" {new CategoriasController().GetCondicaoCategoria(Global.StrToInt(edtGrupo.Text))}";
            }

            string value = MostrarPesquisa(Convert.ToInt32(btnCategoria.Tag.ToString()));
            if (categoriasModel.id_categoria!=0)
            {
                gruposController.CadGruposBase.SetAncestral(tb_grupos, edtGrupo, Global.StrToInt(value), Global.StrToInt(edtPacote.Text), categoriasModel);
                if (!string.IsNullOrEmpty(value))
                {
                    if (edtCategoria.Text != value)
                    {
                        edtCategoria.Text = value;

                        edtPacote.Clear();
                        edtServico.Clear();
                        edtArquivo.Clear();
                        edtMapeamento.Clear();
                    }
                }
            }

        }

        private void btnPacote_Click(object sender, EventArgs e)
        {
            Global.Condicao = "";
            if (edtCategoria.Visible && edtCategoria.Text != "")
            {
                Global.Condicao = $" and c.id_categoria={edtCategoria.Text}";
            }

            string value = MostrarPesquisa(Convert.ToInt32(btnPacote.Tag.ToString()));
            if (pacotesModel.id_pacote != 0)
            {
                categoriasController.CadCategoriasBase.SetAncestral(tb_categorias, edtCategoria, Global.StrToInt(edtPacote.Text), Global.StrToInt(edtServico.Text), pacotesModel);
                gruposController.CadGruposBase.SetAncestral(tb_grupos, edtGrupo, Global.StrToInt(edtCategoria.Text), Global.StrToInt(value), categoriasModel);
                if (!string.IsNullOrEmpty(value))
                {
                    if (edtPacote.Text != value)
                    {
                        edtPacote.Text = value;

                        edtServico.Clear();
                        edtArquivo.Clear();
                        edtMapeamento.Clear();
                    }
                }
            }

        }

        private void VerificaDadosAncestral(TextBox pEdtPai,string  pValue,CheckBox pChk)
        {
            if (pEdtPai.Name=="edtCategoria")
            {
                if (pValue == "0")
                {
                    pValue = Global.CodCategSel.ToString();
                }
            }
            if (pEdtPai.Name == "edtGrupo")
            {
               if (pValue == "0")
                {
                    pValue = Global.CodGrupoSel.ToString();
                }
            }
            pEdtPai.Text = pValue;
            pChk.Checked = pEdtPai.Text != "";
        }

        private void btnServico_Click(object sender, EventArgs e)
        {
            servicosModel.Clear();
            Global.Condicao = "";
            if (edtServico.Visible && (edtGrupo.Text != "" || edtCategoria.Text!="" ||edtPacote.Text!=""))
            {
                Global.Condicao = new ServicosController().GetCondicaoPesquisa(edtGrupo.Text, edtCategoria.Text, edtPacote.Text);
            }

            string value = MostrarPesquisa(Convert.ToInt32(btnServico.Tag.ToString()));
            if (servicosModel.id_servico != 0)
            {
                pacotesController.CadPacotesBase.SetAncestral(tb_pacotes, edtPacote, Global.StrToInt(edtServico.Text), Global.StrToInt(edtArquivo.Text), servicosModel);
                categoriasController.CadCategoriasBase.SetAncestral(tb_categorias, edtCategoria, Global.StrToInt(edtPacote.Text), Global.StrToInt(edtServico.Text), pacotesModel);
                gruposController.CadGruposBase.SetAncestral(tb_grupos, edtGrupo, Global.StrToInt(edtCategoria.Text), Global.StrToInt(edtPacote.Text), categoriasModel);
                if (!string.IsNullOrEmpty(value))
                {
                    if (edtServico.Text != value)
                    {
                        edtServico.Text = value;

                        edtArquivo.Clear();
                        edtMapeamento.Clear();
                    }
                }
            }

        }

        private void btnArquivo_Click(object sender, EventArgs e)
        {
            Global.Condicao = "";
            if (edtServico.Visible && edtServico.Text != "")
            {
                Global.Condicao = new ArquivosController().GetCondicaoPesquisa(edtGrupo.Text,edtCategoria.Text,edtPacote.Text,edtServico.Text);
            }

            string value = MostrarPesquisa(Convert.ToInt32(btnArquivo.Tag.ToString()));
            if (arquivosModel.id_arquivo != 0)
            {
                servicosController.CadServicosBase.SetAncestral(tb_servicos, edtServico, Global.StrToInt(value), Global.StrToInt(edtMapeamento.Text), arquivosModel);
                pacotesController.CadPacotesBase.SetAncestral(tb_pacotes, edtPacote, Global.StrToInt(edtServico.Text), Global.StrToInt(value), servicosModel);
                categoriasController.CadCategoriasBase.SetAncestral(tb_categorias, edtCategoria, Global.StrToInt(edtPacote.Text), Global.StrToInt(edtServico.Text), pacotesModel);
                gruposController.CadGruposBase.SetAncestral(tb_grupos, edtGrupo, Global.StrToInt(edtCategoria.Text), Global.StrToInt(edtPacote.Text), categoriasModel);
                if (!string.IsNullOrEmpty(value))
                {
                    if (edtArquivo.Text != value)
                    {
                        edtArquivo.Text = value;

                        edtMapeamento.Clear();
                    }
                }
            }

        }

        private void btnMapeamento_Click(object sender, EventArgs e)
        {
            Global.Condicao = "";
            if (edtArquivo.Visible && edtArquivo.Text != "")
            {
                Global.Condicao = $" and a.id_arquivo={edtArquivo.Text.Trim()} ";
            }
            edtMapeamento.Text = MostrarPesquisa(Convert.ToInt32(btnMapeamento.Tag.ToString()));
            if (mapeamentosModel.id_mapeamento != 0)
            {
                arquivosController.CadArquivosBase.SetAncestral(tb_arquivos, edtArquivo, mapeamentosModel.id_mapeamento, 0, mapeamentosModel);
                servicosController.CadServicosBase.SetAncestral(tb_servicos, edtServico, Global.StrToInt(edtArquivo.Text), Global.StrToInt(edtMapeamento.Text), arquivosModel);
                pacotesController.CadPacotesBase.SetAncestral(tb_pacotes, edtPacote, Global.StrToInt(edtServico.Text), Global.StrToInt(edtArquivo.Text), servicosModel);
                categoriasController.CadCategoriasBase.SetAncestral(tb_categorias, edtCategoria, Global.StrToInt(edtPacote.Text), Global.StrToInt(edtServico.Text), pacotesModel);
                gruposController.CadGruposBase.SetAncestral(tb_grupos, edtGrupo, Global.StrToInt(edtCategoria.Text), Global.StrToInt(edtPacote.Text), categoriasModel);
            }
        }

        private void btnPerfil_Click(object sender, EventArgs e)
        {
            Global.Condicao = "";
            edtPerfil.Text = MostrarPesquisa(Convert.ToInt32(btnPerfil.Tag.ToString()));
            edtUsuario.Clear();
        }

        private void btnUsuario_Click(object sender, EventArgs e)
        {
            Global.Condicao = "";
            if (edtPerfil.Visible && edtPerfil.Text != "")
            {
                Global.Condicao = $" and p.id_perfil={edtPerfil.Text.Trim()} ";
            }
            edtUsuario.Text = MostrarPesquisa(Convert.ToInt32(btnUsuario.Tag.ToString()));
            if (usuariosModel.Id_usuario != 0)
            {
                VerificaDadosAncestral(edtPerfil, usuariosModel.Id_perfil.ToString(), tb_perfis);
            }
        }

        private void tb_parametros_CheckedChanged(object sender, EventArgs e)
        {
            HabilitaCampoPesquisa(tb_parametros, edtParametro, btnParametro);
        }

        private void btnParametro_Click(object sender, EventArgs e)
        {
            Global.Condicao = "";
            edtParametro.Text = MostrarPesquisa(Convert.ToInt32(btnParametro.Tag.ToString()));

        }

        private void edtGrupo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(edtGrupo.Text))
            {
                if (!new GruposController().PrivilegiosOk(Global.StrToInt(edtGrupo.Text)))
                {
                    MessageBox.Show("Desculpe, este usuário não tem permissões para acessar esta Empresa.\n Favor informar outra Empresa.");
                    edtGrupo.Focus();
                }
            }
        }

        private void edtCategoria_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(edtCategoria.Text))
            {
                if (!new CategoriasController().PrivilegiosOk(Global.StrToInt(edtCategoria.Text)))
                {
                    MessageBox.Show("Desculpe, este usuário não tem permissões para acessar esta Filial.\n Favor informar outra Filial.");
                    edtCategoria.Focus();
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DataTable dt = expImpTabelas.GetDadosXml(edtNomeArquivo.Text, edtTabela.Text);
            dgvTabela.DataSource = dt;
        }

        public EnumerableRowCollection SelectCategoria(DataTable pDtFilho,DataTable pDtPai, int pIndex)
        {
            var ConCateg = from p in pDtFilho.AsEnumerable()
                           where p.Field<int>("Cat_Id_Grupo") == Global.StrToInt(pDtPai.Rows[pIndex].ItemArray[0].ToString())
                           select new
                           {
                               id_categoria = p.Field<int>("id_categoria"),
                               Cat_Id_Grupo = p.Field<int>("Cat_Id_Grupo"),
                               nm_categoria = p.Field<string>("nm_categoria"),
                               Cat_Status = p.Field<string>("Cat_Status"),
                               Descr_Categorias = p.Field<string>("Descr_Categorias")
                           };
            return ConCateg;

        }


        public void ImportarXml(string pPathXml)
        {
            DataTable dtGrupo = expImpTabelas.GetDadosXml(pPathXml, "g");
            DataTable dtCateg = expImpTabelas.GetDadosXml(pPathXml, "c");
            DataTable dtPacote = expImpTabelas.GetDadosXml(pPathXml, "p");
            DataTable dtServicos = expImpTabelas.GetDadosXml(pPathXml, "s");
            DataTable dtMonit = expImpTabelas.GetDadosXml(pPathXml, "mo");
            DataTable dtArquivo = expImpTabelas.GetDadosXml(pPathXml, "a");
            DataTable dtMapeamento = expImpTabelas.GetDadosXml(pPathXml, "ma");
            DataTable dtPerfil = expImpTabelas.GetDadosXmlPerfil(pPathXml,"pf");
            DataTable dtUsuario = expImpTabelas.GetDadosXmlPerfil(pPathXml, "u");
            DataTable dtObjeto = expImpTabelas.GetDadosXmlPerfil(pPathXml, "o");
            DataTable dtPermissao = expImpTabelas.GetDadosXmlPerfil(pPathXml, "pe");
            DataTable dtParametros = expImpTabelas.GetDadosXml(pPathXml, "pa");

            //PERFIL
            for (int pf=0;pf<dtPerfil.Rows.Count;pf++)
            {
                PerfisModel Perfil = new PerfisModel();
                Perfil.Id_perfil = Global.StrToInt(dtPerfil.Rows[pf].ItemArray[0].ToString());
                Perfil.Nm_perfil       = dtPerfil.Rows[pf].ItemArray[1].ToString();
                Perfil.Status          = dtPerfil.Rows[pf].ItemArray[2].ToString();
                Perfil.Data_criacao    = DateTime.Now;
                Perfil.Data_atualizacao = DateTime.Now;

                int IdPerfil = new ExpImpTabelasController().ImpPerfil(Perfil, txtArquivo);
                //USUARIOS
                var ConUsuario = from p in dtUsuario.AsEnumerable()
                                 where p.Field<int>("Usu_id_perfil") == Perfil.Id_perfil
                                 select new
                                 {
                                     Usu_id_perfil=p.Field<int>("Usu_id_perfil"),
                                     nm_usuario = p.Field<string>("nm_usuario"),
                                     login = p.Field<string>("login"),
                                     senha = p.Field<string>("senha"),
                                     email = p.Field<string>("email"),
                                     Usu_Status = p.Field<string>("Usu_Status")
                                 };
                foreach (var Usuarios in ConUsuario)
                {
                    UsuariosModel usuarios = new UsuariosModel();
                    usuarios.Id_perfil = Usuarios.Usu_id_perfil;
                    usuarios.Nm_usuario = Usuarios.nm_usuario;
                    usuarios.Login = Usuarios.login;
                    usuarios.Senha = Usuarios.senha;
                    usuarios.Email = Usuarios.email;

                    int IDUsuario = new ExpImpTabelasController().ImpUsuarios(usuarios,txtArquivo);
//PERMISSOES
                    var ConPermissao = from p in dtPermissao.AsEnumerable()
                                     where p.Field<int>("Prm_id_perfil") == Usuarios.Usu_id_perfil
                                     select new
                                     {
                                         id_permissao = p.Field<int>("id_permissao"),
                                         id_perfil = p.Field<int>("Prm_id_perfil"),
                                         id_objeto = p.Field<int>("Prm_id_objeto"),
                                         permissao = p.Field<string>("permissao"),
                                         Status = p.Field<string>("Prm_Status")
                                     };
                    foreach (var Permissao in ConPermissao)
                    {
                        var ConObjeto = from o in dtObjeto.AsEnumerable()
                                         where o.Field<int>("Obj_id_objeto") == Permissao.id_objeto
                                         select new
                                         {
                                             id_objeto = o.Field<int>("Obj_id_objeto"),
                                             nm_objeto = o.Field<string>("nm_objeto"),
                                             tp_objeto = o.Field<string>("tp_objeto"),
                                             Status = o.Field<string>("Obj_Status")
                                         };
                        foreach(var Objeto in ConObjeto)
                        {
                            ObjetosModel Objetos = new ObjetosModel();
                            Objetos.Id_objeto = Objeto.id_objeto;
                            Objetos.Nm_objeto = Objeto.nm_objeto;
                            Objetos.Tp_objeto = Objeto.tp_objeto;
                            Objetos.Status = Objeto.Status;

                            int IDObj = new ExpImpTabelasController().ImpObjeto(Objetos, txtArquivo);

                            PermissoesModel permissoes = new PermissoesModel();
                            permissoes.Id_permissao = Permissao.id_permissao;
                            permissoes.Id_perfil = Permissao.id_perfil;
                            permissoes.Id_objeto = IDObj;
                            permissoes.Permissao = Permissao.permissao;
                            permissoes.Status = Permissao.Status;

                            new ExpImpTabelasController().ImpPermissoes(permissoes, txtArquivo);
                        }

                    }

                }
            }//FIM DO PERFIL

            //PARAMETROS
            for (int p = 0; p < dtParametros.Rows.Count; p++)
            {
                ParametrosModel parametros = new ParametrosModel();
                parametros.id_parametro = Global.StrToInt(dtParametros.Rows[p].ItemArray[0].ToString());
                parametros.nm_parametro = dtParametros.Rows[p].ItemArray[1].ToString();
                parametros.valor = dtParametros.Rows[p].ItemArray[1].ToString();
                parametros.status = dtParametros.Rows[p].ItemArray[3].ToString();

                new ExpImpTabelasController().ImpParametros(parametros,txtArquivo);
            }//FIM DOS PARAMETROS


            for (int g = 0; g < dtGrupo.Rows.Count; g++)
            {
                GruposModel Grupo = new GruposModel();
                Grupo.id_grupo = Global.StrToInt(dtGrupo.Rows[g].ItemArray[0].ToString());// id_grupo;
                Grupo.nm_grupo = dtGrupo.Rows[g].ItemArray[1].ToString();// nm_grupo;
                Grupo.status = dtGrupo.Rows[g].ItemArray[2].ToString();//Status;
                Grupo.descr_grupos = dtGrupo.Rows[g].ItemArray[3].ToString();// descr_grupos;
                Grupo.data_criacao      = DateTime.Now;
                Grupo.data_atualizacao  = DateTime.Now;


                int IdGrupo = new ExpImpTabelasController().ImpGrupo(Grupo,txtArquivo);
//CATEGORIAS/FILIAIS
                var ConCateg = from p in dtCateg.AsEnumerable()
                               where p.Field<int>("Cat_Id_Grupo") == Global.StrToInt(dtGrupo.Rows[g].ItemArray[0].ToString())
                               select new
                               {
                                   id_categoria = p.Field<int>("id_categoria"),
                                   Cat_Id_Grupo = p.Field<int>("Cat_Id_Grupo"),
                                   nm_categoria = p.Field<string>("nm_categoria"),
                                   Cat_Status = p.Field<string>("Cat_Status"),
                                   Descr_Categorias = p.Field<string>("Descr_Categorias")
                               };
                foreach (var categoria in ConCateg)
                {
                    CategoriasModel Categoria = new CategoriasModel();
                    Categoria.id_categoria = categoria.id_categoria;
                    Categoria.id_grupo = categoria.Cat_Id_Grupo;
                    Categoria.nm_categoria = categoria.nm_categoria;
                    Categoria.status = categoria.Cat_Status;
                    Categoria.descr_categorias = categoria.Descr_Categorias;
                    Categoria.data_criacao = DateTime.Now;
                    Categoria.data_atualizacao = DateTime.Now;

                    int IdCategcoria =new ExpImpTabelasController().ImpCategoria(Categoria,IdGrupo.ToString(),txtArquivo);
//PACOTES
                    var ConPacote = from p in dtPacote.AsEnumerable()
                                   where p.Field<int>("pac_id_categoria") == categoria.id_categoria
                                   select new
                                   {
                                       id_pacote = p.Field<int>("id_pacote"),
                                       id_categoria = p.Field<int>("pac_id_categoria"),
                                       nm_pacote = p.Field<string>("nm_pacote"),
                                       descr_pacotes = p.Field<string>("descr_pacotes"),
                                       status = p.Field<string>("pac_status"),
                                       data_criacao = DateTime.Now,
                                       data_atualizacao = DateTime.Now
                                   };
                    foreach (var pacote in ConPacote)
                    {
                        PacotesModel pacotes = new PacotesModel();
                        pacotes.id_pacote = pacote.id_pacote;
                        pacotes.id_categoria = pacote.id_categoria;
                        pacotes.nm_pacote = pacote.nm_pacote;
                        pacotes.descr_pacotes = pacote.descr_pacotes;
                        pacotes.status = pacote.status;
                        pacotes.data_criacao = pacote.data_criacao;
                        pacotes.data_atualizacao = pacote.data_atualizacao;

                        int IdPacote = new ExpImpTabelasController().ImpPacote(pacotes, IdCategcoria.ToString(), txtArquivo);
//ARQUIVOS
                        var ConArquivos = from p in dtArquivo.AsEnumerable()
                                        where p.Field<int>("Arq_id_pacote") == pacote.id_pacote
                                        select new
                                        {
                                            id_arquivo = p.Field<int>("arq_idarquivo"),
                                            id_pacote = p.Field<int>("arq_id_pacote"),
                                            nm_arquivo = p.Field<string>("nm_arquivo"),
                                            mascara_arquivo = p.Field<string>("mascara_arquivo"),
                                            tp_carga = p.Field<string>("tp_carga"),
                                            tp_arquivo = p.Field<string>("tp_arquivo"),
                                            delimitador = p.Field<string>("delimitador"),
                                            cercador = p.Field<string>("cercador"),
                                            tb_destino = p.Field<string>("tb_destino"),
                                            dir_entrada = p.Field<string>("dir_entrada"),
                                            dir_saida = p.Field<string>("dir_saida"),
                                            rbd_tabela = p.Field<string>("rbd_tabela"),
                                            rbd_indice = p.Field<string>("rbd_indice"),
                                            nm_Planilha = p.Field<string>("nm_Planilha"),
                                            status = p.Field<string>("arq_status"),
                                            data_criacao = DateTime.Now,
                                            data_atualizacao = DateTime.Now,
                                            LineFeed = p.Field<string>("LineFeed"),
                                            FirstLine = p.Field<string>("FirstLine"),
                                            ConexaoBusiness = p.Field<string>("ConexaoBusiness")
                                        };
                        foreach (var Arquivos in ConArquivos)
                        {
                            ArquivosModel arquivos = new ArquivosModel();
                            arquivos.id_arquivo      = Arquivos.id_arquivo;
                            arquivos.id_pacote       = Arquivos.id_pacote;
                            arquivos.nm_arquivo      = Arquivos.nm_arquivo;
                            arquivos.mascara_arquivo = Arquivos.mascara_arquivo;
                            arquivos.tp_carga        = Arquivos.tp_carga;
                            arquivos.tp_arquivo      = Arquivos.tp_arquivo;
                            arquivos.delimitador     = Arquivos.delimitador;
                            arquivos.cercador        = Arquivos.cercador;
                            arquivos.tb_destino      = Arquivos.tb_destino;
                            arquivos.dir_entrada     = Arquivos.dir_entrada;
                            arquivos.dir_saida       = Arquivos.dir_saida;
                            arquivos.rbd_tabela      = Arquivos.rbd_tabela;
                            arquivos.rbd_indice      = Arquivos.rbd_indice;
                            arquivos.nm_Planilha     = Arquivos.nm_Planilha;
                            arquivos.status          = Arquivos.status;
                            arquivos.data_criacao    = Arquivos.data_criacao;
                            arquivos.data_atualizacao= Arquivos.data_atualizacao;
                            arquivos.LineFeed        = Arquivos.LineFeed;
                            arquivos.FirstLine       = Convert.ToInt32(Arquivos.FirstLine);
                            arquivos.ConexaoBusiness = Arquivos.ConexaoBusiness;

                            int IdArquivo = new ExpImpTabelasController().ImpArquivos(arquivos, IdPacote.ToString(), txtArquivo);
//MAPEAMENTOS
                            var ConMapeamento = from p in dtMapeamento.AsEnumerable()
                                              where p.Field<int>("Map_id_arquivo") == Arquivos.id_arquivo
                                              select new
                                              {
                                                    id_mapeamento   = p.Field<int>("id_mapeamento"),
                                                    id_arquivo      = p.Field<int>("Map_id_arquivo"),
                                                    nm_coluna       = p.Field<string>("nm_coluna"),
                                                    ordem           = p.Field<int>("ordem"),
                                                    fixo_inicio     = p.Field<int>("fixo_inicio"),
                                                    fixo_tamanho    = p.Field<int>("fixo_tamanho"),
                                                    tp_coluna       = p.Field<string>("tp_coluna"),
                                                    tm_coluna       = p.Field<int>("tm_coluna"),
                                                    pr_coluna       = p.Field<int>("pr_coluna"),
                                                    MASK_CAMPO      = p.Field<string>("MASK_CAMPO"),
                                                    ExpressaoSql    = p.Field<string>("ExpressaoSql"),
                                                    data_criacao    = DateTime.Now,
                                                    data_atualizacao = DateTime.Now,
                                                };
                            foreach (var Mapeamentos in ConMapeamento)
                            {
                                MapeamentosModel Mapeamento = new MapeamentosModel();
                                Mapeamento.id_mapeamento   = Mapeamentos.id_mapeamento;
                                Mapeamento.id_arquivo      = Mapeamentos.id_arquivo;
                                Mapeamento.nm_coluna       = Mapeamentos.nm_coluna;
                                Mapeamento.ordem           = Mapeamentos.ordem;
                                Mapeamento.fixo_inicio     = Mapeamentos.fixo_inicio;
                                Mapeamento.fixo_tamanho    = Mapeamentos.fixo_tamanho;
                                Mapeamento.tp_coluna       = Mapeamentos.tp_coluna;
                                Mapeamento.tm_coluna       = Mapeamentos.tm_coluna;
                                Mapeamento.pr_coluna       = Mapeamentos.pr_coluna;
                                Mapeamento.MASK_CAMPO      = Mapeamentos.MASK_CAMPO;
                                Mapeamento.ExpressaoSql    = Mapeamentos.ExpressaoSql;
                                Mapeamento.data_criacao    = Mapeamentos.data_criacao;
                                Mapeamento.data_atualizacao= Mapeamentos.data_atualizacao;

                                int IdMapeanto = new ExpImpTabelasController().ImpMapeamentos(Mapeamento, IdArquivo.ToString(), txtArquivo);

                            }

                            //MONITORAMENTOS
                            var ConMonitoramento = from p in dtMonit.AsEnumerable()
                                                   where p.Field<int>("mon_id_arquivo") == Arquivos.id_arquivo
                                                   select new
                                                   {
                                                       id_arquivo = p.Field<int>("mon_id_arquivo"),
                                                       id_servico = p.Field<int>("mon_id_Servico"),
                                                       status = p.Field<string>("mon_status"),
                                                       data_criacao = DateTime.Now,
                                                       data_atualizacao = DateTime.Now,
                                                   };
                            foreach(var Monitoramentos in ConMonitoramento)
                            {
                                //SERVICOS
                                int IdServico = 0;
                                var ConServico = from p in dtServicos.AsEnumerable()
                                                 where p.Field<int>("ser_id_servico") == Monitoramentos.id_servico
                                                 select new
                                                 {
                                                     id_servico = p.Field<int>("ser_id_servico"),
                                                     nm_servico = p.Field<string>("nm_servico"),
                                                     situacao = p.Field<string>("situacao"),
                                                     status = p.Field<string>("ser_status"),
                                                     data_criacao = DateTime.Now,
                                                     data_atualizacao = DateTime.Now,
                                                 };
                                foreach(var Servicos in ConServico)
                                {
                                    ServicosModel servicos = new ServicosModel();
                                    servicos.id_servico         = Servicos.id_servico;
                                    servicos.nm_servico         = Servicos.nm_servico      ;
                                    servicos.situacao           = Servicos.situacao        ;
                                    servicos.status             = Servicos.status          ;
                                    servicos.data_criacao       = Servicos.data_criacao    ;
                                    servicos.data_atualizacao   = Servicos.data_atualizacao;
                                    IdServico = new ExpImpTabelasController().ImpServicos(servicos,txtArquivo);
                                }

                                MonitoramentosModel monitoramentos = new MonitoramentosModel();
                                monitoramentos.id_servico      = IdServico;
                                monitoramentos.id_arquivo      = IdArquivo;
                                monitoramentos.status          = Monitoramentos.status;
                                monitoramentos.data_criacao    = Monitoramentos.data_criacao;
                                monitoramentos.data_atualizacao= Monitoramentos.data_atualizacao;
                                new ExpImpTabelasController().ImpMonitoramentos(monitoramentos,txtArquivo);
                            }
                        }
                    }
                }
            }
        }
    }
}
