namespace ETLApplication.View
{
    partial class frmExpImpTabelas
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.gbTabelas = new System.Windows.Forms.GroupBox();
            this.btnParametro = new System.Windows.Forms.Button();
            this.edtParametro = new System.Windows.Forms.TextBox();
            this.btnUsuario = new System.Windows.Forms.Button();
            this.btnPerfil = new System.Windows.Forms.Button();
            this.btnMapeamento = new System.Windows.Forms.Button();
            this.btnArquivo = new System.Windows.Forms.Button();
            this.btnServico = new System.Windows.Forms.Button();
            this.btnPacote = new System.Windows.Forms.Button();
            this.btnCategoria = new System.Windows.Forms.Button();
            this.btnGrupo = new System.Windows.Forms.Button();
            this.edtUsuario = new System.Windows.Forms.TextBox();
            this.edtPerfil = new System.Windows.Forms.TextBox();
            this.edtMapeamento = new System.Windows.Forms.TextBox();
            this.edtArquivo = new System.Windows.Forms.TextBox();
            this.edtServico = new System.Windows.Forms.TextBox();
            this.edtPacote = new System.Windows.Forms.TextBox();
            this.edtCategoria = new System.Windows.Forms.TextBox();
            this.edtGrupo = new System.Windows.Forms.TextBox();
            this.tb_logs = new System.Windows.Forms.CheckBox();
            this.tb_arquivos = new System.Windows.Forms.CheckBox();
            this.tb_categorias = new System.Windows.Forms.CheckBox();
            this.tb_grupos = new System.Windows.Forms.CheckBox();
            this.tb_indices = new System.Windows.Forms.CheckBox();
            this.tb_mapeamentos = new System.Windows.Forms.CheckBox();
            this.tb_objetos = new System.Windows.Forms.CheckBox();
            this.tb_pacotes = new System.Windows.Forms.CheckBox();
            this.tb_parametros = new System.Windows.Forms.CheckBox();
            this.tb_perfis = new System.Windows.Forms.CheckBox();
            this.tb_permissoes = new System.Windows.Forms.CheckBox();
            this.tb_rejeicoes = new System.Windows.Forms.CheckBox();
            this.tb_servicos = new System.Windows.Forms.CheckBox();
            this.tb_usuarios = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblProcesso = new System.Windows.Forms.Label();
            this.rbImportar = new System.Windows.Forms.RadioButton();
            this.rbExportar = new System.Windows.Forms.RadioButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnExportarImportar = new System.Windows.Forms.Button();
            this.btnLocalizarArquivo = new System.Windows.Forms.Button();
            this.edtNomeArquivo = new System.Windows.Forms.TextBox();
            this.lblExpImp = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtArquivo = new System.Windows.Forms.TextBox();
            this.dgvTabela = new System.Windows.Forms.DataGridView();
            this.edtTabela = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.gbTabelas.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTabela)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.gbTabelas);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(210, 516);
            this.panel2.TabIndex = 8;
            // 
            // gbTabelas
            // 
            this.gbTabelas.Controls.Add(this.button1);
            this.gbTabelas.Controls.Add(this.label1);
            this.gbTabelas.Controls.Add(this.edtTabela);
            this.gbTabelas.Controls.Add(this.btnParametro);
            this.gbTabelas.Controls.Add(this.edtParametro);
            this.gbTabelas.Controls.Add(this.btnUsuario);
            this.gbTabelas.Controls.Add(this.btnPerfil);
            this.gbTabelas.Controls.Add(this.btnMapeamento);
            this.gbTabelas.Controls.Add(this.btnArquivo);
            this.gbTabelas.Controls.Add(this.btnServico);
            this.gbTabelas.Controls.Add(this.btnPacote);
            this.gbTabelas.Controls.Add(this.btnCategoria);
            this.gbTabelas.Controls.Add(this.btnGrupo);
            this.gbTabelas.Controls.Add(this.edtUsuario);
            this.gbTabelas.Controls.Add(this.edtPerfil);
            this.gbTabelas.Controls.Add(this.edtMapeamento);
            this.gbTabelas.Controls.Add(this.edtArquivo);
            this.gbTabelas.Controls.Add(this.edtServico);
            this.gbTabelas.Controls.Add(this.edtPacote);
            this.gbTabelas.Controls.Add(this.edtCategoria);
            this.gbTabelas.Controls.Add(this.edtGrupo);
            this.gbTabelas.Controls.Add(this.tb_logs);
            this.gbTabelas.Controls.Add(this.tb_arquivos);
            this.gbTabelas.Controls.Add(this.tb_categorias);
            this.gbTabelas.Controls.Add(this.tb_grupos);
            this.gbTabelas.Controls.Add(this.tb_indices);
            this.gbTabelas.Controls.Add(this.tb_mapeamentos);
            this.gbTabelas.Controls.Add(this.tb_objetos);
            this.gbTabelas.Controls.Add(this.tb_pacotes);
            this.gbTabelas.Controls.Add(this.tb_parametros);
            this.gbTabelas.Controls.Add(this.tb_perfis);
            this.gbTabelas.Controls.Add(this.tb_permissoes);
            this.gbTabelas.Controls.Add(this.tb_rejeicoes);
            this.gbTabelas.Controls.Add(this.tb_servicos);
            this.gbTabelas.Controls.Add(this.tb_usuarios);
            this.gbTabelas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbTabelas.Location = new System.Drawing.Point(0, 43);
            this.gbTabelas.Name = "gbTabelas";
            this.gbTabelas.Size = new System.Drawing.Size(210, 473);
            this.gbTabelas.TabIndex = 9;
            this.gbTabelas.TabStop = false;
            this.gbTabelas.Text = "Tabelas";
            // 
            // btnParametro
            // 
            this.btnParametro.Location = new System.Drawing.Point(181, 195);
            this.btnParametro.Name = "btnParametro";
            this.btnParametro.Size = new System.Drawing.Size(24, 22);
            this.btnParametro.TabIndex = 34;
            this.btnParametro.Tag = "9";
            this.btnParametro.Text = "...";
            this.btnParametro.UseVisualStyleBackColor = true;
            this.btnParametro.Visible = false;
            this.btnParametro.Click += new System.EventHandler(this.btnParametro_Click);
            // 
            // edtParametro
            // 
            this.edtParametro.Location = new System.Drawing.Point(129, 196);
            this.edtParametro.Name = "edtParametro";
            this.edtParametro.Size = new System.Drawing.Size(52, 20);
            this.edtParametro.TabIndex = 33;
            this.edtParametro.Visible = false;
            // 
            // btnUsuario
            // 
            this.btnUsuario.Location = new System.Drawing.Point(181, 172);
            this.btnUsuario.Name = "btnUsuario";
            this.btnUsuario.Size = new System.Drawing.Size(24, 22);
            this.btnUsuario.TabIndex = 32;
            this.btnUsuario.Tag = "8";
            this.btnUsuario.Text = "...";
            this.btnUsuario.UseVisualStyleBackColor = true;
            this.btnUsuario.Visible = false;
            this.btnUsuario.Click += new System.EventHandler(this.btnUsuario_Click);
            // 
            // btnPerfil
            // 
            this.btnPerfil.Location = new System.Drawing.Point(181, 149);
            this.btnPerfil.Name = "btnPerfil";
            this.btnPerfil.Size = new System.Drawing.Size(24, 22);
            this.btnPerfil.TabIndex = 31;
            this.btnPerfil.Tag = "7";
            this.btnPerfil.Text = "...";
            this.btnPerfil.UseVisualStyleBackColor = true;
            this.btnPerfil.Visible = false;
            this.btnPerfil.Click += new System.EventHandler(this.btnPerfil_Click);
            // 
            // btnMapeamento
            // 
            this.btnMapeamento.Location = new System.Drawing.Point(181, 127);
            this.btnMapeamento.Name = "btnMapeamento";
            this.btnMapeamento.Size = new System.Drawing.Size(24, 22);
            this.btnMapeamento.TabIndex = 30;
            this.btnMapeamento.Tag = "6";
            this.btnMapeamento.Text = "...";
            this.btnMapeamento.UseVisualStyleBackColor = true;
            this.btnMapeamento.Visible = false;
            this.btnMapeamento.Click += new System.EventHandler(this.btnMapeamento_Click);
            // 
            // btnArquivo
            // 
            this.btnArquivo.Location = new System.Drawing.Point(181, 105);
            this.btnArquivo.Name = "btnArquivo";
            this.btnArquivo.Size = new System.Drawing.Size(24, 22);
            this.btnArquivo.TabIndex = 29;
            this.btnArquivo.Tag = "5";
            this.btnArquivo.Text = "...";
            this.btnArquivo.UseVisualStyleBackColor = true;
            this.btnArquivo.Visible = false;
            this.btnArquivo.Click += new System.EventHandler(this.btnArquivo_Click);
            // 
            // btnServico
            // 
            this.btnServico.Location = new System.Drawing.Point(181, 81);
            this.btnServico.Name = "btnServico";
            this.btnServico.Size = new System.Drawing.Size(24, 22);
            this.btnServico.TabIndex = 27;
            this.btnServico.Tag = "4";
            this.btnServico.Text = "...";
            this.btnServico.UseVisualStyleBackColor = true;
            this.btnServico.Visible = false;
            this.btnServico.Click += new System.EventHandler(this.btnServico_Click);
            // 
            // btnPacote
            // 
            this.btnPacote.Location = new System.Drawing.Point(181, 59);
            this.btnPacote.Name = "btnPacote";
            this.btnPacote.Size = new System.Drawing.Size(24, 22);
            this.btnPacote.TabIndex = 26;
            this.btnPacote.Tag = "3";
            this.btnPacote.Text = "...";
            this.btnPacote.UseVisualStyleBackColor = true;
            this.btnPacote.Visible = false;
            this.btnPacote.Click += new System.EventHandler(this.btnPacote_Click);
            // 
            // btnCategoria
            // 
            this.btnCategoria.Location = new System.Drawing.Point(181, 37);
            this.btnCategoria.Name = "btnCategoria";
            this.btnCategoria.Size = new System.Drawing.Size(24, 22);
            this.btnCategoria.TabIndex = 25;
            this.btnCategoria.Tag = "2";
            this.btnCategoria.Text = "...";
            this.btnCategoria.UseVisualStyleBackColor = true;
            this.btnCategoria.Visible = false;
            this.btnCategoria.Click += new System.EventHandler(this.btnCategoria_Click);
            // 
            // btnGrupo
            // 
            this.btnGrupo.Location = new System.Drawing.Point(181, 15);
            this.btnGrupo.Name = "btnGrupo";
            this.btnGrupo.Size = new System.Drawing.Size(24, 22);
            this.btnGrupo.TabIndex = 24;
            this.btnGrupo.Tag = "1";
            this.btnGrupo.Text = "...";
            this.btnGrupo.UseVisualStyleBackColor = true;
            this.btnGrupo.Visible = false;
            this.btnGrupo.Click += new System.EventHandler(this.button1_Click);
            // 
            // edtUsuario
            // 
            this.edtUsuario.Location = new System.Drawing.Point(129, 173);
            this.edtUsuario.Name = "edtUsuario";
            this.edtUsuario.Size = new System.Drawing.Size(52, 20);
            this.edtUsuario.TabIndex = 23;
            this.edtUsuario.Visible = false;
            // 
            // edtPerfil
            // 
            this.edtPerfil.Location = new System.Drawing.Point(129, 150);
            this.edtPerfil.Name = "edtPerfil";
            this.edtPerfil.Size = new System.Drawing.Size(52, 20);
            this.edtPerfil.TabIndex = 22;
            this.edtPerfil.Visible = false;
            // 
            // edtMapeamento
            // 
            this.edtMapeamento.Location = new System.Drawing.Point(129, 128);
            this.edtMapeamento.Name = "edtMapeamento";
            this.edtMapeamento.Size = new System.Drawing.Size(52, 20);
            this.edtMapeamento.TabIndex = 21;
            this.edtMapeamento.Visible = false;
            // 
            // edtArquivo
            // 
            this.edtArquivo.Location = new System.Drawing.Point(129, 106);
            this.edtArquivo.Name = "edtArquivo";
            this.edtArquivo.Size = new System.Drawing.Size(52, 20);
            this.edtArquivo.TabIndex = 20;
            this.edtArquivo.Visible = false;
            // 
            // edtServico
            // 
            this.edtServico.Location = new System.Drawing.Point(129, 82);
            this.edtServico.Name = "edtServico";
            this.edtServico.Size = new System.Drawing.Size(52, 20);
            this.edtServico.TabIndex = 18;
            this.edtServico.Visible = false;
            // 
            // edtPacote
            // 
            this.edtPacote.Location = new System.Drawing.Point(129, 60);
            this.edtPacote.Name = "edtPacote";
            this.edtPacote.Size = new System.Drawing.Size(52, 20);
            this.edtPacote.TabIndex = 17;
            this.edtPacote.Visible = false;
            // 
            // edtCategoria
            // 
            this.edtCategoria.Location = new System.Drawing.Point(129, 38);
            this.edtCategoria.Name = "edtCategoria";
            this.edtCategoria.Size = new System.Drawing.Size(52, 20);
            this.edtCategoria.TabIndex = 16;
            this.edtCategoria.Visible = false;
            this.edtCategoria.Leave += new System.EventHandler(this.edtCategoria_Leave);
            // 
            // edtGrupo
            // 
            this.edtGrupo.Location = new System.Drawing.Point(129, 16);
            this.edtGrupo.Name = "edtGrupo";
            this.edtGrupo.Size = new System.Drawing.Size(52, 20);
            this.edtGrupo.TabIndex = 15;
            this.edtGrupo.Visible = false;
            this.edtGrupo.Leave += new System.EventHandler(this.edtGrupo_Leave);
            // 
            // tb_logs
            // 
            this.tb_logs.AutoSize = true;
            this.tb_logs.Location = new System.Drawing.Point(16, 349);
            this.tb_logs.Name = "tb_logs";
            this.tb_logs.Size = new System.Drawing.Size(49, 17);
            this.tb_logs.TabIndex = 14;
            this.tb_logs.Text = "Logs";
            this.tb_logs.UseVisualStyleBackColor = true;
            this.tb_logs.Visible = false;
            // 
            // tb_arquivos
            // 
            this.tb_arquivos.AutoSize = true;
            this.tb_arquivos.Location = new System.Drawing.Point(17, 109);
            this.tb_arquivos.Name = "tb_arquivos";
            this.tb_arquivos.Size = new System.Drawing.Size(67, 17);
            this.tb_arquivos.TabIndex = 13;
            this.tb_arquivos.Text = "Arquivos";
            this.tb_arquivos.UseVisualStyleBackColor = true;
            this.tb_arquivos.CheckedChanged += new System.EventHandler(this.tb_arquivos_CheckedChanged);
            // 
            // tb_categorias
            // 
            this.tb_categorias.AutoSize = true;
            this.tb_categorias.Location = new System.Drawing.Point(17, 41);
            this.tb_categorias.Name = "tb_categorias";
            this.tb_categorias.Size = new System.Drawing.Size(46, 17);
            this.tb_categorias.TabIndex = 12;
            this.tb_categorias.Text = "Filial";
            this.tb_categorias.UseVisualStyleBackColor = true;
            this.tb_categorias.CheckedChanged += new System.EventHandler(this.tb_categorias_CheckedChanged);
            // 
            // tb_grupos
            // 
            this.tb_grupos.AutoSize = true;
            this.tb_grupos.Location = new System.Drawing.Point(6, 19);
            this.tb_grupos.Name = "tb_grupos";
            this.tb_grupos.Size = new System.Drawing.Size(67, 17);
            this.tb_grupos.TabIndex = 11;
            this.tb_grupos.Text = "Empresa";
            this.tb_grupos.UseVisualStyleBackColor = true;
            this.tb_grupos.CheckedChanged += new System.EventHandler(this.tb_grupos_CheckedChanged);
            // 
            // tb_indices
            // 
            this.tb_indices.AutoSize = true;
            this.tb_indices.Location = new System.Drawing.Point(16, 367);
            this.tb_indices.Name = "tb_indices";
            this.tb_indices.Size = new System.Drawing.Size(60, 17);
            this.tb_indices.TabIndex = 10;
            this.tb_indices.Text = "Índices";
            this.tb_indices.UseVisualStyleBackColor = true;
            this.tb_indices.Visible = false;
            // 
            // tb_mapeamentos
            // 
            this.tb_mapeamentos.AutoSize = true;
            this.tb_mapeamentos.Location = new System.Drawing.Point(28, 132);
            this.tb_mapeamentos.Name = "tb_mapeamentos";
            this.tb_mapeamentos.Size = new System.Drawing.Size(93, 17);
            this.tb_mapeamentos.TabIndex = 9;
            this.tb_mapeamentos.Text = "Mapeamentos";
            this.tb_mapeamentos.UseVisualStyleBackColor = true;
            this.tb_mapeamentos.CheckedChanged += new System.EventHandler(this.tb_mapeamentos_CheckedChanged);
            // 
            // tb_objetos
            // 
            this.tb_objetos.AutoSize = true;
            this.tb_objetos.Location = new System.Drawing.Point(3, 312);
            this.tb_objetos.Name = "tb_objetos";
            this.tb_objetos.Size = new System.Drawing.Size(62, 17);
            this.tb_objetos.TabIndex = 7;
            this.tb_objetos.Text = "Objetos";
            this.tb_objetos.UseVisualStyleBackColor = true;
            this.tb_objetos.Visible = false;
            // 
            // tb_pacotes
            // 
            this.tb_pacotes.AutoSize = true;
            this.tb_pacotes.Location = new System.Drawing.Point(28, 63);
            this.tb_pacotes.Name = "tb_pacotes";
            this.tb_pacotes.Size = new System.Drawing.Size(65, 17);
            this.tb_pacotes.TabIndex = 6;
            this.tb_pacotes.Text = "Pacotes";
            this.tb_pacotes.UseVisualStyleBackColor = true;
            this.tb_pacotes.CheckedChanged += new System.EventHandler(this.tb_pacotes_CheckedChanged);
            // 
            // tb_parametros
            // 
            this.tb_parametros.AutoSize = true;
            this.tb_parametros.Location = new System.Drawing.Point(6, 201);
            this.tb_parametros.Name = "tb_parametros";
            this.tb_parametros.Size = new System.Drawing.Size(79, 17);
            this.tb_parametros.TabIndex = 5;
            this.tb_parametros.Text = "Parâmetros";
            this.tb_parametros.UseVisualStyleBackColor = true;
            this.tb_parametros.CheckedChanged += new System.EventHandler(this.tb_parametros_CheckedChanged);
            // 
            // tb_perfis
            // 
            this.tb_perfis.AutoSize = true;
            this.tb_perfis.Location = new System.Drawing.Point(6, 155);
            this.tb_perfis.Name = "tb_perfis";
            this.tb_perfis.Size = new System.Drawing.Size(52, 17);
            this.tb_perfis.TabIndex = 4;
            this.tb_perfis.Text = "Perfis";
            this.tb_perfis.UseVisualStyleBackColor = true;
            this.tb_perfis.CheckedChanged += new System.EventHandler(this.tb_perfis_CheckedChanged);
            // 
            // tb_permissoes
            // 
            this.tb_permissoes.AutoSize = true;
            this.tb_permissoes.Location = new System.Drawing.Point(14, 328);
            this.tb_permissoes.Name = "tb_permissoes";
            this.tb_permissoes.Size = new System.Drawing.Size(79, 17);
            this.tb_permissoes.TabIndex = 3;
            this.tb_permissoes.Text = "Permissões";
            this.tb_permissoes.UseVisualStyleBackColor = true;
            this.tb_permissoes.Visible = false;
            // 
            // tb_rejeicoes
            // 
            this.tb_rejeicoes.AutoSize = true;
            this.tb_rejeicoes.Location = new System.Drawing.Point(16, 385);
            this.tb_rejeicoes.Name = "tb_rejeicoes";
            this.tb_rejeicoes.Size = new System.Drawing.Size(73, 17);
            this.tb_rejeicoes.TabIndex = 2;
            this.tb_rejeicoes.Text = "Rejeições";
            this.tb_rejeicoes.UseVisualStyleBackColor = true;
            this.tb_rejeicoes.Visible = false;
            // 
            // tb_servicos
            // 
            this.tb_servicos.AutoSize = true;
            this.tb_servicos.Location = new System.Drawing.Point(6, 85);
            this.tb_servicos.Name = "tb_servicos";
            this.tb_servicos.Size = new System.Drawing.Size(67, 17);
            this.tb_servicos.TabIndex = 1;
            this.tb_servicos.Text = "Serviços";
            this.tb_servicos.UseVisualStyleBackColor = true;
            this.tb_servicos.CheckedChanged += new System.EventHandler(this.tb_servicos_CheckedChanged);
            this.tb_servicos.TextChanged += new System.EventHandler(this.tb_servicos_TextChanged);
            // 
            // tb_usuarios
            // 
            this.tb_usuarios.AutoSize = true;
            this.tb_usuarios.Location = new System.Drawing.Point(18, 178);
            this.tb_usuarios.Name = "tb_usuarios";
            this.tb_usuarios.Size = new System.Drawing.Size(67, 17);
            this.tb_usuarios.TabIndex = 0;
            this.tb_usuarios.Text = "Usuários";
            this.tb_usuarios.UseVisualStyleBackColor = true;
            this.tb_usuarios.CheckedChanged += new System.EventHandler(this.tb_usuarios_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblProcesso);
            this.panel1.Controls.Add(this.rbImportar);
            this.panel1.Controls.Add(this.rbExportar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(210, 43);
            this.panel1.TabIndex = 8;
            // 
            // lblProcesso
            // 
            this.lblProcesso.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProcesso.ForeColor = System.Drawing.Color.Navy;
            this.lblProcesso.Location = new System.Drawing.Point(7, -2);
            this.lblProcesso.Name = "lblProcesso";
            this.lblProcesso.Size = new System.Drawing.Size(200, 42);
            this.lblProcesso.TabIndex = 5;
            this.lblProcesso.Text = "Importar";
            this.lblProcesso.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rbImportar
            // 
            this.rbImportar.AutoSize = true;
            this.rbImportar.Location = new System.Drawing.Point(9, 24);
            this.rbImportar.Name = "rbImportar";
            this.rbImportar.Size = new System.Drawing.Size(63, 17);
            this.rbImportar.TabIndex = 4;
            this.rbImportar.TabStop = true;
            this.rbImportar.Text = "Importar";
            this.rbImportar.UseVisualStyleBackColor = true;
            this.rbImportar.CheckedChanged += new System.EventHandler(this.rbImportar_CheckedChanged);
            // 
            // rbExportar
            // 
            this.rbExportar.AutoSize = true;
            this.rbExportar.Location = new System.Drawing.Point(9, 6);
            this.rbExportar.Name = "rbExportar";
            this.rbExportar.Size = new System.Drawing.Size(64, 17);
            this.rbExportar.TabIndex = 3;
            this.rbExportar.TabStop = true;
            this.rbExportar.Text = "Exportar";
            this.rbExportar.UseVisualStyleBackColor = true;
            this.rbExportar.CheckedChanged += new System.EventHandler(this.rbExportar_CheckedChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tabControl1);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(210, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(735, 516);
            this.panel3.TabIndex = 9;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnExportarImportar);
            this.panel4.Controls.Add(this.btnLocalizarArquivo);
            this.panel4.Controls.Add(this.edtNomeArquivo);
            this.panel4.Controls.Add(this.lblExpImp);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(735, 43);
            this.panel4.TabIndex = 10;
            // 
            // btnExportarImportar
            // 
            this.btnExportarImportar.Location = new System.Drawing.Point(642, 16);
            this.btnExportarImportar.Name = "btnExportarImportar";
            this.btnExportarImportar.Size = new System.Drawing.Size(90, 24);
            this.btnExportarImportar.TabIndex = 9;
            this.btnExportarImportar.Text = "Exportar";
            this.btnExportarImportar.UseVisualStyleBackColor = true;
            this.btnExportarImportar.Click += new System.EventHandler(this.btnExportarImportar_Click);
            // 
            // btnLocalizarArquivo
            // 
            this.btnLocalizarArquivo.Location = new System.Drawing.Point(618, 17);
            this.btnLocalizarArquivo.Name = "btnLocalizarArquivo";
            this.btnLocalizarArquivo.Size = new System.Drawing.Size(24, 22);
            this.btnLocalizarArquivo.TabIndex = 8;
            this.btnLocalizarArquivo.Text = "...";
            this.btnLocalizarArquivo.UseVisualStyleBackColor = true;
            this.btnLocalizarArquivo.Click += new System.EventHandler(this.btnLocalizarArquivo_Click);
            // 
            // edtNomeArquivo
            // 
            this.edtNomeArquivo.Location = new System.Drawing.Point(1, 18);
            this.edtNomeArquivo.Name = "edtNomeArquivo";
            this.edtNomeArquivo.Size = new System.Drawing.Size(617, 20);
            this.edtNomeArquivo.TabIndex = 7;
            this.edtNomeArquivo.TextChanged += new System.EventHandler(this.edtNomeArquivo_TextChanged);
            // 
            // lblExpImp
            // 
            this.lblExpImp.AutoSize = true;
            this.lblExpImp.Location = new System.Drawing.Point(-3, 2);
            this.lblExpImp.Name = "lblExpImp";
            this.lblExpImp.Size = new System.Drawing.Size(98, 13);
            this.lblExpImp.TabIndex = 6;
            this.lblExpImp.Text = "Path para Exportar:";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 43);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(735, 473);
            this.tabControl1.TabIndex = 11;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtArquivo);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(727, 447);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Log";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvTabela);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(727, 447);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Tabela";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtArquivo
            // 
            this.txtArquivo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtArquivo.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtArquivo.Location = new System.Drawing.Point(3, 3);
            this.txtArquivo.Multiline = true;
            this.txtArquivo.Name = "txtArquivo";
            this.txtArquivo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtArquivo.Size = new System.Drawing.Size(721, 441);
            this.txtArquivo.TabIndex = 12;
            this.txtArquivo.WordWrap = false;
            // 
            // dgvTabela
            // 
            this.dgvTabela.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTabela.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTabela.Location = new System.Drawing.Point(3, 3);
            this.dgvTabela.Name = "dgvTabela";
            this.dgvTabela.Size = new System.Drawing.Size(721, 441);
            this.dgvTabela.TabIndex = 0;
            // 
            // edtTabela
            // 
            this.edtTabela.Location = new System.Drawing.Point(35, 249);
            this.edtTabela.Name = "edtTabela";
            this.edtTabela.Size = new System.Drawing.Size(100, 20);
            this.edtTabela.TabIndex = 35;
            this.edtTabela.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 232);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 36;
            this.label1.Text = "Tabela";
            this.label1.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(136, 247);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 37;
            this.button1.Text = "Abrir";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // frmExpImpTabelas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(945, 516);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmExpImpTabelas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Exportação e Importação de Tabelas";
            this.Load += new System.EventHandler(this.frmExpImpTabelas_Load);
            this.panel2.ResumeLayout(false);
            this.gbTabelas.ResumeLayout(false);
            this.gbTabelas.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTabela)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox gbTabelas;
        private System.Windows.Forms.CheckBox tb_logs;
        private System.Windows.Forms.CheckBox tb_arquivos;
        private System.Windows.Forms.CheckBox tb_categorias;
        private System.Windows.Forms.CheckBox tb_grupos;
        private System.Windows.Forms.CheckBox tb_indices;
        private System.Windows.Forms.CheckBox tb_mapeamentos;
        private System.Windows.Forms.CheckBox tb_objetos;
        private System.Windows.Forms.CheckBox tb_pacotes;
        private System.Windows.Forms.CheckBox tb_parametros;
        private System.Windows.Forms.CheckBox tb_perfis;
        private System.Windows.Forms.CheckBox tb_permissoes;
        private System.Windows.Forms.CheckBox tb_rejeicoes;
        private System.Windows.Forms.CheckBox tb_servicos;
        private System.Windows.Forms.CheckBox tb_usuarios;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbImportar;
        private System.Windows.Forms.RadioButton rbExportar;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnLocalizarArquivo;
        private System.Windows.Forms.TextBox edtNomeArquivo;
        private System.Windows.Forms.Label lblExpImp;
        private System.Windows.Forms.Button btnExportarImportar;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnUsuario;
        private System.Windows.Forms.Button btnPerfil;
        private System.Windows.Forms.Button btnMapeamento;
        private System.Windows.Forms.Button btnArquivo;
        private System.Windows.Forms.Button btnServico;
        private System.Windows.Forms.Button btnPacote;
        private System.Windows.Forms.Button btnCategoria;
        private System.Windows.Forms.Button btnGrupo;
        private System.Windows.Forms.TextBox edtUsuario;
        private System.Windows.Forms.TextBox edtPerfil;
        private System.Windows.Forms.TextBox edtMapeamento;
        private System.Windows.Forms.TextBox edtArquivo;
        private System.Windows.Forms.TextBox edtServico;
        private System.Windows.Forms.TextBox edtPacote;
        private System.Windows.Forms.TextBox edtCategoria;
        private System.Windows.Forms.TextBox edtGrupo;
        private System.Windows.Forms.Button btnParametro;
        private System.Windows.Forms.TextBox edtParametro;
        private System.Windows.Forms.Label lblProcesso;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox txtArquivo;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgvTabela;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox edtTabela;
        private System.Windows.Forms.Button button1;
    }
}