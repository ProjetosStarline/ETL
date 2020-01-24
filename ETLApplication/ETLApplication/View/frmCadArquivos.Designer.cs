namespace ETLApplication
{
    partial class frmCadArquivos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.cbServicos = new System.Windows.Forms.ComboBox();
            this.cbPacote = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.edtNomeArquivo = new System.Windows.Forms.TextBox();
            this.edtMascaraArquivo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbTpCarga = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbTpArquivo = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pnlParaTipoDeArquivo = new System.Windows.Forms.Panel();
            this.edtNomePlanilha = new System.Windows.Forms.TextBox();
            this.lblNomePlanilha = new System.Windows.Forms.Label();
            this.edtDelimitador = new System.Windows.Forms.TextBox();
            this.lblDelimitador = new System.Windows.Forms.Label();
            this.edtTabelaDestino = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.edtDirSaida = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.edtDirEntrada = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbRbdIndice = new System.Windows.Forms.CheckBox();
            this.cbRbdTabela = new System.Windows.Forms.CheckBox();
            this.cbStatus = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.edtDataAtualizacao = new System.Windows.Forms.MaskedTextBox();
            this.edtDataCriacao = new System.Windows.Forms.MaskedTextBox();
            this.lblNameFileShort = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.edtIdArquivos = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.gbMapeamentos = new System.Windows.Forms.GroupBox();
            this.dgvMapeamentos = new System.Windows.Forms.DataGridView();
            this.id_arquivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nm_coluna = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ordem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fixo_inicio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fixo_tamanho = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tp_coluna = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.tm_coluna = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pr_coluna = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MASK_CAMPO = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ExpressaoSql = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbLineFeed = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cbIngnorarCabecalho = new System.Windows.Forms.CheckBox();
            this.edtBaseBusiness = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.edtFieldQuote = new System.Windows.Forms.TextBox();
            this.ckbUltimaAba = new System.Windows.Forms.CheckBox();
            this.ckbPossuiVariasAba = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.pnlBarraBotoes.SuspendLayout();
            this.pnlParaTipoDeArquivo.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.gbMapeamentos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMapeamentos)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ckbUltimaAba);
            this.groupBox1.Controls.Add(this.ckbPossuiVariasAba);
            this.groupBox1.Controls.Add(this.edtFieldQuote);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.edtBaseBusiness);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.cbIngnorarCabecalho);
            this.groupBox1.Controls.Add(this.cbLineFeed);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.gbMapeamentos);
            this.groupBox1.Controls.Add(this.edtIdArquivos);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.cbStatus);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.edtDirSaida);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.edtDirEntrada);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.edtTabelaDestino);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.pnlParaTipoDeArquivo);
            this.groupBox1.Controls.Add(this.cbTpArquivo);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cbTpCarga);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.edtMascaraArquivo);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.edtNomeArquivo);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbPacote);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbServicos);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Size = new System.Drawing.Size(875, 481);
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // btnOk
            // 
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // pnlBarraBotoes
            // 
            this.pnlBarraBotoes.Size = new System.Drawing.Size(875, 36);
            // 
            // btnPesquisar
            // 
            this.btnPesquisar.Click += new System.EventHandler(this.btnPesquisar_Click);
            // 
            // btnInserir
            // 
            this.btnInserir.Click += new System.EventHandler(this.btnInserir_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(152, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Serviço:";
            // 
            // cbServicos
            // 
            this.cbServicos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbServicos.FormattingEnabled = true;
            this.cbServicos.Location = new System.Drawing.Point(152, 59);
            this.cbServicos.Name = "cbServicos";
            this.cbServicos.Size = new System.Drawing.Size(173, 21);
            this.cbServicos.TabIndex = 1;
            // 
            // cbPacote
            // 
            this.cbPacote.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPacote.FormattingEnabled = true;
            this.cbPacote.Location = new System.Drawing.Point(331, 59);
            this.cbPacote.Name = "cbPacote";
            this.cbPacote.Size = new System.Drawing.Size(169, 21);
            this.cbPacote.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(331, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Pacote:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Nome:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // edtNomeArquivo
            // 
            this.edtNomeArquivo.Location = new System.Drawing.Point(82, 86);
            this.edtNomeArquivo.Name = "edtNomeArquivo";
            this.edtNomeArquivo.Size = new System.Drawing.Size(297, 20);
            this.edtNomeArquivo.TabIndex = 4;
            this.edtNomeArquivo.TextChanged += new System.EventHandler(this.edtNomeArquivo_TextChanged);
            // 
            // edtMascaraArquivo
            // 
            this.edtMascaraArquivo.Location = new System.Drawing.Point(518, 86);
            this.edtMascaraArquivo.Name = "edtMascaraArquivo";
            this.edtMascaraArquivo.Size = new System.Drawing.Size(348, 20);
            this.edtMascaraArquivo.TabIndex = 5;
            this.edtMascaraArquivo.TextChanged += new System.EventHandler(this.edtMascaraArquivo_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(459, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Máscara:";
            // 
            // cbTpCarga
            // 
            this.cbTpCarga.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTpCarga.FormattingEnabled = true;
            this.cbTpCarga.Items.AddRange(new object[] {
            "FULL",
            "SEQUENCIAL"});
            this.cbTpCarga.Location = new System.Drawing.Point(82, 112);
            this.cbTpCarga.Name = "cbTpCarga";
            this.cbTpCarga.Size = new System.Drawing.Size(121, 21);
            this.cbTpCarga.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(12, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Tp. Carga:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbTpArquivo
            // 
            this.cbTpArquivo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTpArquivo.FormattingEnabled = true;
            this.cbTpArquivo.Items.AddRange(new object[] {
            "DELIMITADO",
            "FIXO",
            "EXCEL",
            "DBF"});
            this.cbTpArquivo.Location = new System.Drawing.Point(82, 139);
            this.cbTpArquivo.Name = "cbTpArquivo";
            this.cbTpArquivo.Size = new System.Drawing.Size(121, 21);
            this.cbTpArquivo.TabIndex = 9;
            this.cbTpArquivo.SelectedIndexChanged += new System.EventHandler(this.cbTpArquivo_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(12, 147);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Tp. Arquivo:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlParaTipoDeArquivo
            // 
            this.pnlParaTipoDeArquivo.BackColor = System.Drawing.SystemColors.HotTrack;
            this.pnlParaTipoDeArquivo.Controls.Add(this.edtNomePlanilha);
            this.pnlParaTipoDeArquivo.Controls.Add(this.lblNomePlanilha);
            this.pnlParaTipoDeArquivo.Controls.Add(this.edtDelimitador);
            this.pnlParaTipoDeArquivo.Controls.Add(this.lblDelimitador);
            this.pnlParaTipoDeArquivo.Location = new System.Drawing.Point(211, 139);
            this.pnlParaTipoDeArquivo.Name = "pnlParaTipoDeArquivo";
            this.pnlParaTipoDeArquivo.Size = new System.Drawing.Size(170, 21);
            this.pnlParaTipoDeArquivo.TabIndex = 10;
            this.pnlParaTipoDeArquivo.Visible = false;
            // 
            // edtNomePlanilha
            // 
            this.edtNomePlanilha.Location = new System.Drawing.Point(77, 27);
            this.edtNomePlanilha.Name = "edtNomePlanilha";
            this.edtNomePlanilha.Size = new System.Drawing.Size(88, 20);
            this.edtNomePlanilha.TabIndex = 3;
            this.edtNomePlanilha.Visible = false;
            // 
            // lblNomePlanilha
            // 
            this.lblNomePlanilha.AutoSize = true;
            this.lblNomePlanilha.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNomePlanilha.Location = new System.Drawing.Point(2, 29);
            this.lblNomePlanilha.Name = "lblNomePlanilha";
            this.lblNomePlanilha.Size = new System.Drawing.Size(78, 13);
            this.lblNomePlanilha.TabIndex = 2;
            this.lblNomePlanilha.Text = "Nm Planilha:";
            this.lblNomePlanilha.Visible = false;
            // 
            // edtDelimitador
            // 
            this.edtDelimitador.Location = new System.Drawing.Point(77, 1);
            this.edtDelimitador.Name = "edtDelimitador";
            this.edtDelimitador.Size = new System.Drawing.Size(88, 20);
            this.edtDelimitador.TabIndex = 1;
            this.edtDelimitador.Visible = false;
            // 
            // lblDelimitador
            // 
            this.lblDelimitador.AutoSize = true;
            this.lblDelimitador.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDelimitador.Location = new System.Drawing.Point(3, 3);
            this.lblDelimitador.Name = "lblDelimitador";
            this.lblDelimitador.Size = new System.Drawing.Size(74, 13);
            this.lblDelimitador.TabIndex = 0;
            this.lblDelimitador.Text = "Delimitador:";
            this.lblDelimitador.Visible = false;
            // 
            // edtTabelaDestino
            // 
            this.edtTabelaDestino.Location = new System.Drawing.Point(518, 139);
            this.edtTabelaDestino.Name = "edtTabelaDestino";
            this.edtTabelaDestino.Size = new System.Drawing.Size(348, 20);
            this.edtTabelaDestino.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(459, 147);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Destino:";
            // 
            // edtDirSaida
            // 
            this.edtDirSaida.Location = new System.Drawing.Point(518, 167);
            this.edtDirSaida.Name = "edtDirSaida";
            this.edtDirSaida.Size = new System.Drawing.Size(348, 20);
            this.edtDirSaida.TabIndex = 13;
            this.edtDirSaida.Leave += new System.EventHandler(this.edtDirSaida_Leave);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(459, 174);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(58, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Dir. Saída:";
            // 
            // edtDirEntrada
            // 
            this.edtDirEntrada.Location = new System.Drawing.Point(82, 167);
            this.edtDirEntrada.Name = "edtDirEntrada";
            this.edtDirEntrada.Size = new System.Drawing.Size(371, 20);
            this.edtDirEntrada.TabIndex = 12;
            this.edtDirEntrada.Leave += new System.EventHandler(this.edtDirEntrada_Leave);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 171);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(66, 13);
            this.label10.TabIndex = 15;
            this.label10.Text = "Dir. Entrada:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbRbdIndice);
            this.groupBox2.Controls.Add(this.cbRbdTabela);
            this.groupBox2.Location = new System.Drawing.Point(298, 186);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(160, 39);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Reconstruir... ";
            this.groupBox2.Visible = false;
            // 
            // cbRbdIndice
            // 
            this.cbRbdIndice.AutoSize = true;
            this.cbRbdIndice.Location = new System.Drawing.Point(86, 19);
            this.cbRbdIndice.Name = "cbRbdIndice";
            this.cbRbdIndice.Size = new System.Drawing.Size(55, 17);
            this.cbRbdIndice.TabIndex = 22;
            this.cbRbdIndice.Text = "Índice";
            this.cbRbdIndice.UseVisualStyleBackColor = true;
            // 
            // cbRbdTabela
            // 
            this.cbRbdTabela.AutoSize = true;
            this.cbRbdTabela.Location = new System.Drawing.Point(11, 19);
            this.cbRbdTabela.Name = "cbRbdTabela";
            this.cbRbdTabela.Size = new System.Drawing.Size(59, 17);
            this.cbRbdTabela.TabIndex = 21;
            this.cbRbdTabela.Text = "Tabela";
            this.cbRbdTabela.UseVisualStyleBackColor = true;
            // 
            // cbStatus
            // 
            this.cbStatus.DisplayMember = "ativo";
            this.cbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStatus.FormattingEnabled = true;
            this.cbStatus.Items.AddRange(new object[] {
            "ativo",
            "inativo"});
            this.cbStatus.Location = new System.Drawing.Point(744, 60);
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.Size = new System.Drawing.Size(121, 21);
            this.cbStatus.TabIndex = 3;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.edtDataAtualizacao);
            this.groupBox3.Controls.Add(this.edtDataCriacao);
            this.groupBox3.Controls.Add(this.lblNameFileShort);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Enabled = false;
            this.groupBox3.Location = new System.Drawing.Point(459, 187);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(407, 39);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Data";
            // 
            // edtDataAtualizacao
            // 
            this.edtDataAtualizacao.Location = new System.Drawing.Point(243, 10);
            this.edtDataAtualizacao.Name = "edtDataAtualizacao";
            this.edtDataAtualizacao.Size = new System.Drawing.Size(110, 20);
            this.edtDataAtualizacao.TabIndex = 8;
            this.edtDataAtualizacao.ValidatingType = typeof(System.DateTime);
            // 
            // edtDataCriacao
            // 
            this.edtDataCriacao.Location = new System.Drawing.Point(57, 10);
            this.edtDataCriacao.Name = "edtDataCriacao";
            this.edtDataCriacao.Size = new System.Drawing.Size(110, 20);
            this.edtDataCriacao.TabIndex = 7;
            this.edtDataCriacao.ValidatingType = typeof(System.DateTime);
            // 
            // lblNameFileShort
            // 
            this.lblNameFileShort.AutoSize = true;
            this.lblNameFileShort.Location = new System.Drawing.Point(617, 16);
            this.lblNameFileShort.Name = "lblNameFileShort";
            this.lblNameFileShort.Size = new System.Drawing.Size(221, 13);
            this.lblNameFileShort.TabIndex = 29;
            this.lblNameFileShort.Text = "bcjacahschasbcbashkgaskbcnmasbccnmab ";
            this.lblNameFileShort.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(175, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Atualização";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 15);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(43, 13);
            this.label11.TabIndex = 5;
            this.label11.Text = "Criação";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(744, 42);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(37, 13);
            this.label12.TabIndex = 23;
            this.label12.Text = "Status";
            // 
            // edtIdArquivos
            // 
            this.edtIdArquivos.Location = new System.Drawing.Point(82, 59);
            this.edtIdArquivos.Name = "edtIdArquivos";
            this.edtIdArquivos.Size = new System.Drawing.Size(66, 20);
            this.edtIdArquivos.TabIndex = 0;
            this.edtIdArquivos.TextChanged += new System.EventHandler(this.edtIdArquivos_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(12, 63);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(63, 13);
            this.label13.TabIndex = 25;
            this.label13.Text = "Id Arquivos:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // gbMapeamentos
            // 
            this.gbMapeamentos.Controls.Add(this.dgvMapeamentos);
            this.gbMapeamentos.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gbMapeamentos.Location = new System.Drawing.Point(3, 232);
            this.gbMapeamentos.Name = "gbMapeamentos";
            this.gbMapeamentos.Size = new System.Drawing.Size(869, 246);
            this.gbMapeamentos.TabIndex = 27;
            this.gbMapeamentos.TabStop = false;
            this.gbMapeamentos.Text = "Mapeamentos";
            // 
            // dgvMapeamentos
            // 
            this.dgvMapeamentos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvMapeamentos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMapeamentos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id_arquivo,
            this.nm_coluna,
            this.ordem,
            this.fixo_inicio,
            this.fixo_tamanho,
            this.tp_coluna,
            this.tm_coluna,
            this.pr_coluna,
            this.MASK_CAMPO,
            this.ExpressaoSql});
            this.dgvMapeamentos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMapeamentos.Location = new System.Drawing.Point(3, 16);
            this.dgvMapeamentos.Name = "dgvMapeamentos";
            this.dgvMapeamentos.Size = new System.Drawing.Size(863, 227);
            this.dgvMapeamentos.TabIndex = 0;
            this.dgvMapeamentos.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgvMapeamentos_RowsAdded);
            // 
            // id_arquivo
            // 
            this.id_arquivo.HeaderText = "ID Arq.";
            this.id_arquivo.Name = "id_arquivo";
            this.id_arquivo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.id_arquivo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.id_arquivo.Width = 50;
            // 
            // nm_coluna
            // 
            this.nm_coluna.HeaderText = "Nome Coluna";
            this.nm_coluna.Name = "nm_coluna";
            this.nm_coluna.Width = 150;
            // 
            // ordem
            // 
            this.ordem.HeaderText = "Ordem";
            this.ordem.Name = "ordem";
            this.ordem.Width = 50;
            // 
            // fixo_inicio
            // 
            this.fixo_inicio.HeaderText = "FX. INI";
            this.fixo_inicio.Name = "fixo_inicio";
            this.fixo_inicio.Width = 50;
            // 
            // fixo_tamanho
            // 
            this.fixo_tamanho.HeaderText = "FX. TAM.";
            this.fixo_tamanho.Name = "fixo_tamanho";
            this.fixo_tamanho.Width = 50;
            // 
            // tp_coluna
            // 
            this.tp_coluna.HeaderText = "Tipo Coluna";
            this.tp_coluna.Items.AddRange(new object[] {
            "int",
            "numeric",
            "datetime",
            "varchar"});
            this.tp_coluna.Name = "tp_coluna";
            // 
            // tm_coluna
            // 
            this.tm_coluna.HeaderText = "Tam. Col.";
            this.tm_coluna.Name = "tm_coluna";
            this.tm_coluna.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.tm_coluna.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.tm_coluna.Width = 50;
            // 
            // pr_coluna
            // 
            this.pr_coluna.HeaderText = "Qtd. Dec.";
            this.pr_coluna.Name = "pr_coluna";
            this.pr_coluna.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.pr_coluna.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.pr_coluna.Width = 50;
            // 
            // MASK_CAMPO
            // 
            this.MASK_CAMPO.HeaderText = "MASK CAMPO";
            this.MASK_CAMPO.Items.AddRange(new object[] {
            "ExpressaoSql",
            "dd/mm/yy",
            "dd-mm-yy",
            "dd.mm.yy",
            "dd/mm/yyyy",
            "dd-mm-yyyy",
            "dd.mm.yyyy",
            "ddmmyy",
            "ddmmyyyy",
            "mm/dd/yy",
            "mm-dd-yy",
            "mm.dd.yy",
            "mm/dd/yyyy",
            "mm.dd.yyyy",
            "mm-dd-yyyy",
            "mmddyy",
            "mmddyyyy",
            "yy/mm/dd",
            "yy-mm-dd",
            "yy.mm.dd",
            "yyyy/mm/dd",
            "yyyy-mm-dd",
            "yyyy.mm.dd",
            "yymmdd",
            "yyyymmdd",
            "hh:mi:ss",
            "hh:mi:ss:mmm",
            "24h:mi:ss:mmm",
            "dd-mm-yyyy hh:mi:ss",
            "dd-mm-yyyy 24h:mi:ss",
            "mm-dd-yyyy hh:mi:ss",
            "mm-dd-yyyy 24h:mi:ss",
            "yyyy-mm-dd hh:mi:ss",
            "yyyy-mm-dd 24h:mi:ss",
            "yyyy-mm-dd hh:mi:ss.mmm",
            "yyyy-mm-dd 24h:mi:ss.mmm",
            "yyyy-mm-ddThh:mi:ss.mmm",
            "dd mon yy",
            "dd mon yyyy",
            "dd mon yyyy hh:mi:ss:mmm",
            "dd mon yyyy 24h:mi:ss:mmm",
            "mon dd yyyy hh:mi:ss:mmmAM",
            "mon dd yyyy hh:mi:ss:mmmPM",
            "mon dd yyyy hh:miAM",
            "mon dd yyyy hh:miPM",
            "Mon dd, yy",
            "Mon dd, yyyy",
            "#,##0.00",
            "#.##0,00",
            "###0,00",
            "###000",
            "###0.0000",
            "Vazio"});
            this.MASK_CAMPO.Name = "MASK_CAMPO";
            // 
            // ExpressaoSql
            // 
            this.ExpressaoSql.HeaderText = "Expressão Sql";
            this.ExpressaoSql.Name = "ExpressaoSql";
            // 
            // cbLineFeed
            // 
            this.cbLineFeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLineFeed.FormattingEnabled = true;
            this.cbLineFeed.Items.AddRange(new object[] {
            "\\r = CR (Carriage Return) MAC",
            "\\n = LF (Line Feed)  Unix/Mac",
            "\\r\\n = CR + LF - Windows"});
            this.cbLineFeed.Location = new System.Drawing.Point(469, 112);
            this.cbLineFeed.Name = "cbLineFeed";
            this.cbLineFeed.Size = new System.Drawing.Size(271, 21);
            this.cbLineFeed.TabIndex = 7;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(395, 118);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(74, 13);
            this.label14.TabIndex = 28;
            this.label14.Text = "Quebra Linha:";
            // 
            // cbIngnorarCabecalho
            // 
            this.cbIngnorarCabecalho.AutoSize = true;
            this.cbIngnorarCabecalho.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbIngnorarCabecalho.Location = new System.Drawing.Point(745, 114);
            this.cbIngnorarCabecalho.Name = "cbIngnorarCabecalho";
            this.cbIngnorarCabecalho.Size = new System.Drawing.Size(113, 17);
            this.cbIngnorarCabecalho.TabIndex = 8;
            this.cbIngnorarCabecalho.Text = "Ignorar Cabeçalho";
            this.cbIngnorarCabecalho.UseVisualStyleBackColor = true;
            // 
            // edtBaseBusiness
            // 
            this.edtBaseBusiness.Location = new System.Drawing.Point(506, 60);
            this.edtBaseBusiness.Name = "edtBaseBusiness";
            this.edtBaseBusiness.Size = new System.Drawing.Size(231, 20);
            this.edtBaseBusiness.TabIndex = 3;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(506, 42);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(92, 13);
            this.label15.TabIndex = 31;
            this.label15.Text = "Base de Negócio:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(214, 118);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(64, 13);
            this.label16.TabIndex = 32;
            this.label16.Text = "Field Quote:";
            // 
            // edtFieldQuote
            // 
            this.edtFieldQuote.Location = new System.Drawing.Point(287, 111);
            this.edtFieldQuote.Name = "edtFieldQuote";
            this.edtFieldQuote.Size = new System.Drawing.Size(52, 20);
            this.edtFieldQuote.TabIndex = 7;
            // 
            // ckbUltimaAba
            // 
            this.ckbUltimaAba.AutoSize = true;
            this.ckbUltimaAba.Enabled = false;
            this.ckbUltimaAba.Location = new System.Drawing.Point(82, 216);
            this.ckbUltimaAba.Name = "ckbUltimaAba";
            this.ckbUltimaAba.Size = new System.Drawing.Size(162, 17);
            this.ckbUltimaAba.TabIndex = 34;
            this.ckbUltimaAba.Text = "Última Aba do arquivo Excel.";
            this.ckbUltimaAba.UseVisualStyleBackColor = true;
            // 
            // ckbPossuiVariasAba
            // 
            this.ckbPossuiVariasAba.AutoSize = true;
            this.ckbPossuiVariasAba.Enabled = false;
            this.ckbPossuiVariasAba.Location = new System.Drawing.Point(82, 193);
            this.ckbPossuiVariasAba.Name = "ckbPossuiVariasAba";
            this.ckbPossuiVariasAba.Size = new System.Drawing.Size(220, 17);
            this.ckbPossuiVariasAba.TabIndex = 33;
            this.ckbPossuiVariasAba.Text = "O arquivo Excel possui mais de uma aba.";
            this.ckbPossuiVariasAba.UseVisualStyleBackColor = true;
            // 
            // frmCadArquivos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(875, 517);
            this.MinimizeBox = true;
            this.Name = "frmCadArquivos";
            this.Text = "Cadastro de Arquivos";
            this.Load += new System.EventHandler(this.frmCadArquivos_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnlBarraBotoes.ResumeLayout(false);
            this.pnlParaTipoDeArquivo.ResumeLayout(false);
            this.pnlParaTipoDeArquivo.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.gbMapeamentos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMapeamentos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbPacote;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbServicos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox edtMascaraArquivo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox edtNomeArquivo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbTpArquivo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbTpCarga;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel pnlParaTipoDeArquivo;
        private System.Windows.Forms.TextBox edtDelimitador;
        private System.Windows.Forms.Label lblDelimitador;
        private System.Windows.Forms.TextBox edtTabelaDestino;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox edtDirSaida;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox edtDirEntrada;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbRbdIndice;
        private System.Windows.Forms.CheckBox cbRbdTabela;
        private System.Windows.Forms.TextBox edtNomePlanilha;
        private System.Windows.Forms.Label lblNomePlanilha;
        private System.Windows.Forms.ComboBox cbStatus;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.MaskedTextBox edtDataAtualizacao;
        private System.Windows.Forms.MaskedTextBox edtDataCriacao;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox edtIdArquivos;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox gbMapeamentos;
        private System.Windows.Forms.DataGridView dgvMapeamentos;
        private System.Windows.Forms.CheckBox cbIngnorarCabecalho;
        private System.Windows.Forms.ComboBox cbLineFeed;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblNameFileShort;
        private System.Windows.Forms.TextBox edtBaseBusiness;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox edtFieldQuote;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_arquivo;
        private System.Windows.Forms.DataGridViewTextBoxColumn nm_coluna;
        private System.Windows.Forms.DataGridViewTextBoxColumn ordem;
        private System.Windows.Forms.DataGridViewTextBoxColumn fixo_inicio;
        private System.Windows.Forms.DataGridViewTextBoxColumn fixo_tamanho;
        private System.Windows.Forms.DataGridViewComboBoxColumn tp_coluna;
        private System.Windows.Forms.DataGridViewTextBoxColumn tm_coluna;
        private System.Windows.Forms.DataGridViewTextBoxColumn pr_coluna;
        private System.Windows.Forms.DataGridViewComboBoxColumn MASK_CAMPO;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExpressaoSql;
        private System.Windows.Forms.CheckBox ckbUltimaAba;
        private System.Windows.Forms.CheckBox ckbPossuiVariasAba;
    }
}
