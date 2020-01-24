using ETLApplication.Controller;
using ETLApplication.Model;
using EtlConexao;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace ETLApplication
{
    public partial class frmCadArquivos : ETLApplication.View.frmCadastroBase
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern uint GetShortPathName(
            [MarshalAs(UnmanagedType.LPTStr)]
            string lpszLongPath,
            [MarshalAs(UnmanagedType.LPTStr)]
            StringBuilder lpszShortPath,
            uint cchBuffer);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern uint GetShortPathName(string lpszLongPath, char[] lpszShortPath, int cchBuffer);

        // To ensure that paths are not limited to MAX_PATH, use this signature within .NET
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "GetShortPathNameW", SetLastError = true)]
        static extern int GetShortPathName(string pathName, System.Text.StringBuilder shortName, int cbShortName);


        //CadastroBase<ArquivosModel> cadastroBase;

        CadastroBase<MonitoramentosModel> cadMonitBase;
        MapeamentosController cadMaptoControl;
        ArquivosController arquivosController;
        string NomeORG = "";
        string NomePadrao = "DBF_TEMP.DBF";
        string PathDoArqPadrao = "";
        public frmCadArquivos()
        {
            InitializeComponent();
            arquivosController = new ArquivosController();
            cadMonitBase = new CadastroBase<MonitoramentosModel>();
            cadMaptoControl = new MapeamentosController();
            //cadastroBase = new CadastroBase<ArquivosModel>();
            HabilitaCampos();
        }
        public void HabilitaCampos()
        {
            edtIdArquivos.Enabled = !arquivosController.CadArquivosBase.FieldIsPK(arquivosModel, "Id_arquivo");
            ckbPossuiVariasAba.Enabled = cbTpArquivo.Text == "EXCEL";
            ckbUltimaAba.Enabled = cbTpArquivo.Text == "EXCEL";
        }

        public void HabilitaPnlParaTipoDeArquivo()
        {
            ckbPossuiVariasAba.Enabled = false;
            ckbUltimaAba.Enabled = false;

            pnlParaTipoDeArquivo.Left = 209;
            pnlParaTipoDeArquivo.Top = cbTpArquivo.Top;
            pnlParaTipoDeArquivo.Width = 170;
            pnlParaTipoDeArquivo.Height = 22;
            pnlParaTipoDeArquivo.Visible = false;
            if (cbTpArquivo.Text == "DELIMITADO")
            {
                pnlParaTipoDeArquivo.Visible = true;
                lblDelimitador.Visible = true;
                edtDelimitador.Visible = true;
                lblNomePlanilha.Visible = false;
                edtNomePlanilha.Visible = false;
                lblDelimitador.Left = 3;
                lblDelimitador.Top = 3;
                edtDelimitador.Left = 80;
                edtDelimitador.Top = 1;
                edtDelimitador.Focus();
            }
            else if (cbTpArquivo.Text == "EXCEL")
            {
                ckbPossuiVariasAba.Enabled = true;
                ckbUltimaAba.Enabled = true;

                pnlParaTipoDeArquivo.Visible = true;
                lblNomePlanilha.Visible = true;
                edtNomePlanilha.Visible = true;
                lblDelimitador.Visible = false;
                edtDelimitador.Visible = false;
                lblNomePlanilha.Left = 3;
                lblNomePlanilha.Top = 3;
                edtNomePlanilha.Left = 80;
                edtNomePlanilha.Top = 1;

                edtNomePlanilha.Focus();
            }

        }

        private void cbTpArquivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            HabilitaPnlParaTipoDeArquivo();
            edtDirEntrada_TextChanged(sender, e);
        }

        public override void ControlaBotoes()
        {
            base.ControlaBotoes();
            if (edtIdArquivos != null)
            {
                btnAlterar.Enabled = operacao != Operacao.oEdit && edtIdArquivos.Text.Length != 0 && edtIdArquivos.Text != "0" && (Global.Login.Alterar || Global.UsuarioSuper);
                btnExcluir.Enabled = operacao != Operacao.oDelete && edtIdArquivos.Text.Length != 0 && edtIdArquivos.Text != "0" && (Global.Login.Deletar || Global.UsuarioSuper);
            }
            if (ckbPossuiVariasAba != null)
            {
                ckbPossuiVariasAba.Enabled = cbTpArquivo.Text == "EXCEL";
            }
            if (ckbUltimaAba != null)
            {
                ckbUltimaAba.Enabled = cbTpArquivo.Text == "EXCEL";
            }
        }

        private void frmCadArquivos_Load(object sender, EventArgs e)
        {
            CadastroBase<ServicosModel> servicosModel = new CadastroBase<ServicosModel>();
            string select = servicosModel.GetSelect(new ServicosModel());
            servicosModel.GetLista(select, cbServicos);
            cbServicos.ValueMember = "id_servico";
            cbServicos.DisplayMember = "nm_servico";
            if (cbServicos.Items.Count > 0)
            {
                cbServicos.SelectedIndex = 0;
            }

            PacotesController pacotes = new PacotesController();
            pacotes.PreencheComboPacotes(cbPacote);
            if (cbPacote.Items.Count > 0)
            {
                cbPacote.SelectedIndex = 0;
            }

        }

        public override void LimparTela()
        {
            edtIdArquivos.Clear();
            if (cbStatus.Items.Count>0) { cbStatus.SelectedIndex = 0; }
            cbServicos.Text="";
            cbPacote.Text="";
            edtBaseBusiness.Text = "";
            edtNomeArquivo.Clear();
            edtMascaraArquivo.Clear();
            if (cbTpCarga.Items.Count > 0){ cbTpCarga.SelectedIndex = 0; }
            if (cbTpArquivo.Items.Count > 0) { cbTpArquivo.SelectedIndex = 0; }
            edtTabelaDestino.Clear();
            edtDirEntrada.Clear();
            edtDirSaida.Clear();
            cbRbdTabela.Checked = false;
            cbRbdIndice.Checked = false;
            edtDataCriacao.Text = Global.DateTimeToStr(Global.DateTimeNow);
            edtDataAtualizacao.Text = Global.DateTimeToStr(Global.DateTimeNow);
            cbIngnorarCabecalho.Checked = false;
            cbLineFeed.SelectedIndex = 2;
            dgvMapeamentos.Rows.Clear();
            edtBaseBusiness.Clear();
            edtFieldQuote.Clear();
            ckbPossuiVariasAba.Checked = false;
            ckbUltimaAba.Checked = false;
            base.LimparTela();
        }

        public void SetDados()
        {
            arquivosModel.id_arquivo= Convert.ToInt32(arquivosController.CadArquivosBase.isNullResultZero(edtIdArquivos.Text));
            arquivosModel.status= cbStatus.Text;
            arquivosModel.id_pacote = arquivosController.CadArquivosBase.GetidPacote(cbPacote.Text);
            arquivosModel.nm_arquivo= edtNomeArquivo.Text;
            arquivosModel.mascara_arquivo= edtMascaraArquivo.Text;
            arquivosModel.tp_carga= cbTpCarga.Text;
            arquivosModel.tp_arquivo= cbTpArquivo.Text;
            arquivosModel.tb_destino= edtTabelaDestino.Text;
            arquivosModel.dir_entrada= edtDirEntrada.Text;
            arquivosModel.dir_saida= edtDirSaida.Text;
            arquivosModel.rbd_tabela=  cbRbdTabela.Checked ? "SIM" : "NÃO";
            arquivosModel.rbd_indice=  cbRbdIndice.Checked ? "SIM" : "NÃO";
            arquivosModel.delimitador = "";
            arquivosModel.nm_Planilha = "";
            arquivosModel.LineFeed = cbLineFeed.Text.Substring(0,cbLineFeed.Text.IndexOf("=")-1);
            arquivosModel.FirstLine = cbIngnorarCabecalho.Checked ? 2 : 1;
            if (cbTpArquivo.Text == "DELIMITADO")
            {
                arquivosModel.delimitador = edtDelimitador.Text;
            }
            if (cbTpArquivo.Text == "EXCEL")
            {
                arquivosModel.nm_Planilha = edtNomePlanilha.Text;

            }
            
            arquivosModel.ConexaoBusiness = edtBaseBusiness.Text;
            arquivosModel.cercador = edtFieldQuote.Text;

            arquivosModel.FlagPlanTemVariasAbas = ckbPossuiVariasAba.Checked ? "SIM" : "NAO";
            arquivosModel.FlagUltimaAbaPlanilha = ckbUltimaAba.Checked ? "SIM" : "NAO";

        }

        public bool SetDadosMapeamento(int pIdArquivo)
        {
            bool Result = false;
            if (dgvMapeamentos.RowCount > 1)
            {
                if (operacao == Operacao.oEdit)
                {
                    cadMaptoControl.DeletarMapeamentos(pIdArquivo);
                }

                for (int i = 0; i <= dgvMapeamentos.RowCount - 1; i++)
                {
                    if (dgvMapeamentos.Rows[i].Cells[1].Value != null)
                    {
                        mapeamentosModel.id_mapeamento = Convert.ToInt32(arquivosController.CadArquivosBase.isNullResultZero(dgvMapeamentos.Rows[i].Cells[2].Value));
                        mapeamentosModel.id_arquivo = pIdArquivo;
                        mapeamentosModel.nm_coluna = arquivosController.CadArquivosBase.isNullResultVazio(dgvMapeamentos.Rows[i].Cells[1].Value);
                        mapeamentosModel.ordem = Convert.ToInt32(arquivosController.CadArquivosBase.isNullResultZero(dgvMapeamentos.Rows[i].Cells[2].Value));
                        mapeamentosModel.fixo_inicio = Convert.ToInt32(arquivosController.CadArquivosBase.isNullResultZero(dgvMapeamentos.Rows[i].Cells[3].Value));
                        mapeamentosModel.fixo_tamanho = Convert.ToInt32(arquivosController.CadArquivosBase.isNullResultZero(dgvMapeamentos.Rows[i].Cells[4].Value));
                        mapeamentosModel.tp_coluna = arquivosController.CadArquivosBase.isNullResultVazio(dgvMapeamentos.Rows[i].Cells[5].Value);
                        mapeamentosModel.tm_coluna = Convert.ToInt32(arquivosController.CadArquivosBase.isNullResultZero(dgvMapeamentos.Rows[i].Cells[6].Value));
                        mapeamentosModel.pr_coluna = Convert.ToInt32(arquivosController.CadArquivosBase.isNullResultZero(dgvMapeamentos.Rows[i].Cells[7].Value));
                        mapeamentosModel.MASK_CAMPO = arquivosController.CadArquivosBase.isNullResultVazio(dgvMapeamentos.Rows[i].Cells[8].Value);
                        mapeamentosModel.ExpressaoSql = arquivosController.CadArquivosBase.isNullResultVazio(dgvMapeamentos.Rows[i].Cells[9].Value);
                        try
                        {
                            string MsgMapeamentos = cadMaptoControl.CadMaptoBase.PersisteNoBanco(mapeamentosModel, OperacaoToInt(Operacao.oInsert));
                            Result = MsgMapeamentos.IndexOf("sucesso.") > 0;
                        }
                        catch (Exception ex)
                        {
                            Result = false;
                            MessageBox.Show($"Erro ao gravar os Mapeamentos. Motivo: {ex.Message}");
                            break;
                        }
                    }
                }
            }
            else
            {
                Result = true;
            }
            return Result;
        }

        public void SetDadosMapeamentos(int pIdArquivo)
        {
            if (pIdArquivo == 0)
            {
                dgvMapeamentos.Rows.Clear();
            }
            else
            {
                dgvMapeamentos.Rows.Clear();
                List<MapeamentosModel> LstMapeamentos = 
                    new MapeamentosModel().GetListaMapeamentos(
                        cadMaptoControl.CadMaptoBase.conexao.ExecutarSelect(
                             cadMaptoControl.CadMaptoBase.GetSelect(mapeamentosModel)
                             +$" and id_arquivo={pIdArquivo.ToString()}"
                        )
                    );
                
                for (int i = 0; i <= LstMapeamentos.Count - 1; i++)
                {
                    if (LstMapeamentos[i].nm_coluna.Length > 0)
                    {
                        dgvMapeamentos.Rows.Add();
                        dgvMapeamentos.Rows[i].Cells[0].Value = LstMapeamentos[i].id_arquivo;
                        dgvMapeamentos.Rows[i].Cells[1].Value = LstMapeamentos[i].nm_coluna;
                        dgvMapeamentos.Rows[i].Cells[2].Value = LstMapeamentos[i].ordem;
                        dgvMapeamentos.Rows[i].Cells[3].Value = LstMapeamentos[i].fixo_inicio;
                        dgvMapeamentos.Rows[i].Cells[4].Value = LstMapeamentos[i].fixo_tamanho;
                        dgvMapeamentos.Rows[i].Cells[5].Value = LstMapeamentos[i].tp_coluna;
                        dgvMapeamentos.Rows[i].Cells[6].Value = LstMapeamentos[i].tm_coluna;
                        dgvMapeamentos.Rows[i].Cells[7].Value = LstMapeamentos[i].pr_coluna;
                        dgvMapeamentos.Rows[i].Cells[8].Value = LstMapeamentos[i].MASK_CAMPO;
                        dgvMapeamentos.Rows[i].Cells[9].Value = LstMapeamentos[i].ExpressaoSql;
                    }
                }
            }
        }

        public void GetDados()
        {
            edtIdArquivos.Text = arquivosModel.id_arquivo.ToString() ;
            cbStatus.SelectedIndex = arquivosModel.status == "ativo" ? 0 : 1;
            cbPacote.Text = arquivosController.CadArquivosBase.GetNomePacotes(arquivosModel.id_pacote);      
            edtNomeArquivo.Text = arquivosModel.nm_arquivo;
            edtMascaraArquivo.Text = arquivosModel.mascara_arquivo;
            cbTpCarga.SelectedIndex = arquivosModel.tp_carga == "FULL" ? 0 : 1;
            cbTpArquivo.Text = arquivosModel.tp_arquivo;
            edtDelimitador.Text = arquivosModel.delimitador;
            edtNomePlanilha.Text = arquivosModel.nm_Planilha;
            edtTabelaDestino.Text = arquivosModel.tb_destino;
            edtDirEntrada.Text = arquivosModel.dir_entrada;
            edtDirSaida.Text = arquivosModel.dir_saida;
            cbRbdTabela.Checked = arquivosModel.rbd_tabela == "SIM";
            cbRbdIndice.Checked = arquivosModel.rbd_indice == "SIM";
            edtDataCriacao.Text = arquivosModel.data_criacao.ToString();
            edtDataAtualizacao.Text = arquivosModel.data_atualizacao.ToString();
            cbServicos.Text = arquivosController.CadArquivosBase.SelectNomeServico(arquivosModel.id_arquivo);
            cbLineFeed.SelectedIndex = arquivosModel.LineFeed=="\\r\\n"?2: arquivosModel.LineFeed == "\\n" ? 1:0;
            cbIngnorarCabecalho.Checked = arquivosModel.FirstLine == 2;
            edtBaseBusiness.Text = arquivosModel.ConexaoBusiness;
            edtFieldQuote.Text = arquivosModel.cercador;
            SetDadosMapeamentos(arquivosModel.id_arquivo);

            ckbPossuiVariasAba.Checked = arquivosModel.FlagPlanTemVariasAbas == "SIM";
            ckbUltimaAba.Checked = arquivosModel.FlagUltimaAbaPlanilha=="SIM";

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string Msg = "";
            bool CadOk = false;
            SetDados();
            if (operacao == Operacao.oDelete)
            {
                //Conexao.IniciaTransacao("DelArquivo", arquivosController.CadArquivosBase.conexao.Conn);
                try
                {
                    arquivosController.DeletarMonitoramentos(arquivosModel.id_arquivo);
                    cadMaptoControl.DeletarMapeamentos(arquivosModel.id_arquivo);
                    Msg = arquivosController.CadArquivosBase.PersisteNoBanco(arquivosModel, OperacaoToInt(operacao));
                    //Conexao.CommitaTransacao("DelArquivo", arquivosController.CadArquivosBase.conexao.Conn);
                    LimparTela();
                    if (!Global.IsNullOrEmpty(Msg))
                    {
                        MessageBox.Show(Msg);
                    }
                }
                catch (Exception ex)
                {
                    //Conexao.ConcelaTransacao("DelArquivo", arquivosController.CadArquivosBase.conexao.Conn);
                    MessageBox.Show($"{Msg}. Motivo: {ex.Message}");
                }
            }
            else
            {
                if (cbServicos.Text.Length == 0)
                {
                    MessageBox.Show("Favor informar em qual Serviço será cadastrado o Arquivo.");
                    cbServicos.Focus();
                    return;
                }

                if (cbPacote.Text.Length == 0)
                {
                    MessageBox.Show("Favor informar em qual Pacote será cadastrado o Arquivo.");
                    cbPacote.Focus();
                    return;
                }

                if (edtNomeArquivo.Text.Length == 0)
                {
                    MessageBox.Show("Favor informar o Nome do Arquivo.");
                    edtNomeArquivo.Focus();
                    return;
                }

                if (edtMascaraArquivo.Text.Length == 0)
                {
                    MessageBox.Show("Favor informar a Máscara do Arquivo.");
                    edtMascaraArquivo.Focus();
                    return;
                }

                if (cbTpCarga.Text.Length == 0)
                {
                    MessageBox.Show("Favor informar o Tipo da Carga.");
                    cbTpCarga.Focus();
                    return;
                }

                if (cbTpArquivo.Text.Length == 0)
                {
                    MessageBox.Show("Favor informar o Tipo do Arquivo.");
                    cbTpArquivo.Focus();
                    return;
                }

                if (cbTpArquivo.Text=="DELIMITADO" && edtDelimitador.Text.Length==0)
                {
                    MessageBox.Show("Favor informar  o Delimitador.");
                    edtDelimitador.Focus();
                    return;
                }

                if (cbTpArquivo.Text == "EXCEL" && edtNomePlanilha.Text.Length == 0)
                {
                    MessageBox.Show("Favor informar o nome da planilha.");
                    edtNomePlanilha.Focus();
                    return;
                }

                if (edtDirEntrada.Text.Length == 0)
                {
                    MessageBox.Show("Favor informar o diretório de entrada dos arquivos.");
                    edtDirEntrada.Focus();
                    return;
                }

                if (dgvMapeamentos.Rows.Count==1 && dgvMapeamentos.Rows[0].Cells[1].Value==null)
                {
                    MessageBox.Show("Favor inserir pelo menos 1 Mapeamento.");
                    edtDirEntrada.Focus();
                    return;
                }

                if (edtTabelaDestino.Text.Length == 0)
                {
                    MessageBox.Show("Favor informar o Nome da Tabela Destino.");
                    edtTabelaDestino.Focus();
                    return;
                }

                if (edtBaseBusiness.Text.Length > 0)
                {
                    DialogResult Result=MessageBox.Show($"Este procedimento irá criar uma Base de Dados com o nome {edtBaseBusiness.Text}. Deseja continuar?","",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                    if (Result == DialogResult.No)
                    {
                        edtBaseBusiness.Focus();
                        return;
                    }
                }

                //Conexao.IniciaTransacao("CadArquivo", arquivosController.CadArquivosBase.conexao.Conn);
                try
                {
                    if (operacao == Operacao.oInsert)
                    {
                        if (arquivosController.ArquivoCadastrado(edtNomeArquivo.Text, arquivosModel.id_pacote) )
                        {
                            if (!(cbTpArquivo.Text == "EXCEL" && ckbPossuiVariasAba.Checked))
                            {
                                MessageBox.Show("Já foi cadastrado um arquivo com este nome. Favor informar outro nome.");
                                edtNomeArquivo.Focus();
                                return;
                            }
                        }
                        if (arquivosController.TabelaDSTCadastrada(edtTabelaDestino.Text))
                        {
                            MessageBox.Show("Já foi cadastrado um arquivo com este Nome de Tabela Destino. Favor informar outro nome.");
                            edtTabelaDestino.Focus();
                            return;
                        }

                    }
                    Msg = arquivosController.CadArquivosBase.PersisteNoBanco(arquivosModel, OperacaoToInt(operacao));
                    CadOk = true;
                }
                catch (Exception ex)
                {
                    CadOk = false;
                    MessageBox.Show($"{Msg}. Motivo: {ex.Message}");
                }

                if (operacao == Operacao.oInsert)
                {
                    monitoramentosModel.id_arquivo = arquivosController.CadArquivosBase.GetLastReg(arquivosModel);
                }
                else
                {
                    monitoramentosModel.id_arquivo = Convert.ToInt32(arquivosController.CadArquivosBase.isNullResultZero(edtIdArquivos.Text));
                }

                monitoramentosModel.id_servico = arquivosController.CadArquivosBase.GetidServico(cbServicos.Text);
                monitoramentosModel.status = cbStatus.Text;
                try
                {
                    string msgMonitoramento = cadMonitBase.PersisteNoBanco(monitoramentosModel, OperacaoToInt(operacao));
                    bool cadMaptoOk = SetDadosMapeamento(monitoramentosModel.id_arquivo);
                    if (CadOk && cadMaptoOk)
                    {
                        //Conexao.CommitaTransacao("CadArquivo", arquivosController.CadArquivosBase.conexao.Conn);
                        LimparTela();
                        MessageBox.Show(Msg);
                    }
                    else
                    {
                        //Conexao.ConcelaTransacao("CadArquivo", arquivosController.CadArquivosBase.conexao.Conn);
                        MessageBox.Show($"Erro inesperado ao cadastrar o arquivo. Favor verificar.");
                    }
                }
                catch (Exception ex)
                {
                    //Conexao.ConcelaTransacao("CadArquivo", arquivosController.CadArquivosBase.conexao.Conn);
                    MessageBox.Show($"{Msg}. Motivo: {ex.Message}");
                }

            }
        }

        public override void CriaClasseModel()
        {
            new frmPesquisa(arquivosModel).ShowDialog();
            MostrarNatela();
        }


        private void MostrarNatela()
        {
           //LimparTela();
            GetDados();
        }

        private void edtDirEntrada_TextChanged(object sender, EventArgs e)
        {
        }

        private void edtNomeArquivo_TextChanged(object sender, EventArgs e)
        {
            edtDirEntrada_TextChanged(sender, e);
        }

        private void edtIdArquivos_TextChanged(object sender, EventArgs e)
        {
            operacao = Operacao.oNenhum;
            ControlaBotoes();
        }

        private void dgvMapeamentos_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            int Linha = dgvMapeamentos.Rows.Count;
            dgvMapeamentos.Rows[Linha-1].Cells[2].Value = Linha;
        }

        public bool RenomeiaSeNomeMaiorOitoDig(string pNomeArquivo)
        {
            //Quando o arquivo dbf tem o nome maior que 8 digitos da erro na importação
            //esse tratamento é feito somente par arquivos com o nome maior q 8 digitos
            bool NomeArqRenomeado = false;
            NomeORG = pNomeArquivo;
            string OnlyFileName = Path.GetFileName(pNomeArquivo);
            OnlyFileName = OnlyFileName.Substring(0, OnlyFileName.IndexOf('.'));
            NomePadrao = Path.GetFileName(pNomeArquivo);

             
            NomeArqRenomeado = false;
            if (OnlyFileName.Length > 8)
            {
                NomePadrao = Path.GetFileName(GetShortPathName(Path.GetDirectoryName(pNomeArquivo) +"\\"+ Path.GetFileName(pNomeArquivo)));
                //    NomePadrao = "DBF_TEMP.DBF";
                //    PathDoArqPadrao = Path.GetDirectoryName(pNomeArquivo) + $"\\{NomePadrao}";
                //    try
                //    {
                //        File.Move(NomeORG, PathDoArqPadrao);
                //        NomeArqRenomeado = true;
                //    }
                //    catch (Exception ex)
                //    {
                //        MessageBox.Show($"Erro ao renomear o arquivo: {NomeORG}. Motivo:{ex.Message}");
                //    }
            }
            return NomeArqRenomeado;
        }

        private void edtDirEntrada_Leave(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(edtDirEntrada.Text))
            {
                ParametrosController parametrosController = new ParametrosController();
                if (parametrosController.GetIdParametro("Sugerir_Mapeamento_Do_Arquivo") == 0)
                {
                    parametrosController.GravaNovoParametro("Sugerir_Mapeamento_Do_Arquivo", "SIM");
                }
                if (parametrosController.GetValorParametro("Sugerir_Mapeamento_Do_Arquivo") == "SIM")
                {
                    if (cbTpArquivo.Text == "EXCEL" || 
                        cbTpArquivo.Text == "DBF" && (operacao==Operacao.oInsert && dgvMapeamentos.Rows.Count==1 && Global.IsNullOrEmpty(dgvMapeamentos.Rows[0].Cells[1].Value)))
                    {
                        ArquivosController arquivosController = new ArquivosController();
                        edtDirEntrada.Text = Global.LastCaracterDifereDeBarra(edtDirEntrada.Text);

                        lblNameFileShort.Text = GetShortPathName(edtDirEntrada.Text + edtMascaraArquivo.Text);

                        bool NomeArqRenomeado = RenomeiaSeNomeMaiorOitoDig(edtDirEntrada.Text+edtMascaraArquivo.Text);

                        List<MapeamentosModel> LstMapeamentos = arquivosController.GetEstruturaFile(edtDirEntrada.Text + NomePadrao, edtNomePlanilha.Text);
                        for (int i = 0; i <= LstMapeamentos.Count - 1; i++)
                        {
                            dgvMapeamentos.Rows.Add();
                            dgvMapeamentos.Rows[i].Cells[0].Value = LstMapeamentos[i].id_arquivo;
                            dgvMapeamentos.Rows[i].Cells[1].Value = LstMapeamentos[i].nm_coluna;
                            dgvMapeamentos.Rows[i].Cells[2].Value = LstMapeamentos[i].ordem;
                            dgvMapeamentos.Rows[i].Cells[3].Value = LstMapeamentos[i].fixo_inicio;
                            dgvMapeamentos.Rows[i].Cells[4].Value = LstMapeamentos[i].fixo_tamanho;
                            dgvMapeamentos.Rows[i].Cells[5].Value = LstMapeamentos[i].tp_coluna;
                            dgvMapeamentos.Rows[i].Cells[6].Value = LstMapeamentos[i].tm_coluna;
                            dgvMapeamentos.Rows[i].Cells[7].Value = LstMapeamentos[i].pr_coluna;
                            dgvMapeamentos.Rows[i].Cells[8].Value = LstMapeamentos[i].MASK_CAMPO;
                            dgvMapeamentos.Rows[i].Cells[9].Value = LstMapeamentos[i].ExpressaoSql;
                        }
                        //if (NomeArqRenomeado)
                        //{
                        //    File.Move(PathDoArqPadrao, NomeORG);
                        //}
                    }
                }
            }

        }

        private void edtDirSaida_Leave(object sender, EventArgs e)
        {
            edtDirSaida.Text = Global.LastCaracterDifereDeBarra(edtDirSaida.Text);
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            LimparTela();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void edtMascaraArquivo_TextChanged(object sender, EventArgs e)
        {

        }

        public static string GetShortPathName(string longFileName)
        {
            var sb1 = new StringBuilder();
            int sz = GetShortPathName(longFileName, sb1, 0);
            if (sz == 0)
                throw new Win32Exception();
            var sb = new StringBuilder(sz + 1);
            sz = GetShortPathName(longFileName, sb, sb.Capacity);
            if (sz == 0)
                throw new Win32Exception();
            return sb.ToString();
        }
    }
}
 