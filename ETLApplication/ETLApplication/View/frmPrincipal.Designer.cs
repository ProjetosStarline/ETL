namespace ETLApplication
{
    partial class frmPrincipal
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
            this.Menu = new System.Windows.Forms.MenuStrip();
            this.Sistema = new System.Windows.Forms.ToolStripMenuItem();
            this.Sistema_Configuracao = new System.Windows.Forms.ToolStripMenuItem();
            this.Sistema_Configuracao_SelecionarEmpresa = new System.Windows.Forms.ToolStripMenuItem();
            this.Sistema_Usuarios = new System.Windows.Forms.ToolStripMenuItem();
            this.Sistema_Usuarios_AlterarSenha = new System.Windows.Forms.ToolStripMenuItem();
            this.Sistema_Usuarios_MudarUsuario = new System.Windows.Forms.ToolStripMenuItem();
            this.Sistema_Sair = new System.Windows.Forms.ToolStripMenuItem();
            this.Cadastro = new System.Windows.Forms.ToolStripMenuItem();
            this.Cadastro_Parametros = new System.Windows.Forms.ToolStripMenuItem();
            this.Cadastro_Empresa = new System.Windows.Forms.ToolStripMenuItem();
            this.Cadastro_Filial = new System.Windows.Forms.ToolStripMenuItem();
            this.Cadastro_Perfil = new System.Windows.Forms.ToolStripMenuItem();
            this.Cadastro_Usuario = new System.Windows.Forms.ToolStripMenuItem();
            this.Cadastro_Permissoes = new System.Windows.Forms.ToolStripMenuItem();
            this.ETL = new System.Windows.Forms.ToolStripMenuItem();
            this.ETL_Servicos = new System.Windows.Forms.ToolStripMenuItem();
            this.ETL_Pacotes = new System.Windows.Forms.ToolStripMenuItem();
            this.ETL_Arquivos = new System.Windows.Forms.ToolStripMenuItem();
            this.Processos = new System.Windows.Forms.ToolStripMenuItem();
            this.Processos_ExportarTabelas = new System.Windows.Forms.ToolStripMenuItem();
            this.Processos_ImportarTabelas = new System.Windows.Forms.ToolStripMenuItem();
            this.Processos_VisualizarAndamento = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblCategoria = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblSeparador = new System.Windows.Forms.Label();
            this.lblGrupo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlRodape = new System.Windows.Forms.Panel();
            this.pnlUsuarioLogado = new System.Windows.Forms.Panel();
            this.lblUsuarioLogado = new System.Windows.Forms.Label();
            this.pnlBackupData = new System.Windows.Forms.Panel();
            this.lblBackupData = new System.Windows.Forms.Label();
            this.pnlBackupSucesso = new System.Windows.Forms.Panel();
            this.lblBackupSucesso = new System.Windows.Forms.Label();
            this.Menu.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlRodape.SuspendLayout();
            this.pnlUsuarioLogado.SuspendLayout();
            this.pnlBackupData.SuspendLayout();
            this.pnlBackupSucesso.SuspendLayout();
            this.SuspendLayout();
            // 
            // Menu
            // 
            this.Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Sistema,
            this.Cadastro,
            this.ETL,
            this.Processos});
            this.Menu.Location = new System.Drawing.Point(0, 0);
            this.Menu.Name = "Menu";
            this.Menu.Size = new System.Drawing.Size(1179, 24);
            this.Menu.TabIndex = 0;
            this.Menu.Text = "Menu";
            // 
            // Sistema
            // 
            this.Sistema.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Sistema_Configuracao,
            this.Sistema_Usuarios,
            this.Sistema_Sair});
            this.Sistema.Name = "Sistema";
            this.Sistema.Size = new System.Drawing.Size(60, 20);
            this.Sistema.Text = "Sistema";
            // 
            // Sistema_Configuracao
            // 
            this.Sistema_Configuracao.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Sistema_Configuracao_SelecionarEmpresa});
            this.Sistema_Configuracao.Name = "Sistema_Configuracao";
            this.Sistema_Configuracao.Size = new System.Drawing.Size(146, 22);
            this.Sistema_Configuracao.Text = "Configuração";
            // 
            // Sistema_Configuracao_SelecionarEmpresa
            // 
            this.Sistema_Configuracao_SelecionarEmpresa.Name = "Sistema_Configuracao_SelecionarEmpresa";
            this.Sistema_Configuracao_SelecionarEmpresa.Size = new System.Drawing.Size(176, 22);
            this.Sistema_Configuracao_SelecionarEmpresa.Text = "Selecionar Empresa";
            this.Sistema_Configuracao_SelecionarEmpresa.Click += new System.EventHandler(this.Configuracao_SelecionarGrupo_Click);
            // 
            // Sistema_Usuarios
            // 
            this.Sistema_Usuarios.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Sistema_Usuarios_AlterarSenha,
            this.Sistema_Usuarios_MudarUsuario});
            this.Sistema_Usuarios.Name = "Sistema_Usuarios";
            this.Sistema_Usuarios.Size = new System.Drawing.Size(146, 22);
            this.Sistema_Usuarios.Text = "Usuários";
            // 
            // Sistema_Usuarios_AlterarSenha
            // 
            this.Sistema_Usuarios_AlterarSenha.Name = "Sistema_Usuarios_AlterarSenha";
            this.Sistema_Usuarios_AlterarSenha.Size = new System.Drawing.Size(152, 22);
            this.Sistema_Usuarios_AlterarSenha.Text = "Alterar Senha";
            this.Sistema_Usuarios_AlterarSenha.Click += new System.EventHandler(this.Sistema_Usuarios_AlterarSenha_Click);
            // 
            // Sistema_Usuarios_MudarUsuario
            // 
            this.Sistema_Usuarios_MudarUsuario.Name = "Sistema_Usuarios_MudarUsuario";
            this.Sistema_Usuarios_MudarUsuario.Size = new System.Drawing.Size(152, 22);
            this.Sistema_Usuarios_MudarUsuario.Text = "Mudar Usuário";
            this.Sistema_Usuarios_MudarUsuario.Click += new System.EventHandler(this.Sistema_Usuarios_MudarUsuario_Click);
            // 
            // Sistema_Sair
            // 
            this.Sistema_Sair.Name = "Sistema_Sair";
            this.Sistema_Sair.Size = new System.Drawing.Size(146, 22);
            this.Sistema_Sair.Text = "Sair";
            this.Sistema_Sair.Click += new System.EventHandler(this.sairToolStripMenuItem_Click);
            // 
            // Cadastro
            // 
            this.Cadastro.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Cadastro_Parametros,
            this.Cadastro_Empresa,
            this.Cadastro_Filial,
            this.Cadastro_Perfil,
            this.Cadastro_Usuario,
            this.Cadastro_Permissoes});
            this.Cadastro.Name = "Cadastro";
            this.Cadastro.Size = new System.Drawing.Size(147, 20);
            this.Cadastro.Text = "Cadastro Administrativo";
            // 
            // Cadastro_Parametros
            // 
            this.Cadastro_Parametros.Name = "Cadastro_Parametros";
            this.Cadastro_Parametros.Size = new System.Drawing.Size(180, 22);
            this.Cadastro_Parametros.Text = "Parâmetros";
            this.Cadastro_Parametros.Click += new System.EventHandler(this.parâmetrosToolStripMenuItem_Click);
            // 
            // Cadastro_Empresa
            // 
            this.Cadastro_Empresa.Name = "Cadastro_Empresa";
            this.Cadastro_Empresa.Size = new System.Drawing.Size(180, 22);
            this.Cadastro_Empresa.Text = "Empresa";
            this.Cadastro_Empresa.Click += new System.EventHandler(this.Cadastro_GrupoEmpresarial_Click);
            // 
            // Cadastro_Filial
            // 
            this.Cadastro_Filial.Name = "Cadastro_Filial";
            this.Cadastro_Filial.Size = new System.Drawing.Size(180, 22);
            this.Cadastro_Filial.Text = "Filial";
            this.Cadastro_Filial.Click += new System.EventHandler(this.Cadastro_Categorias_Click);
            // 
            // Cadastro_Perfil
            // 
            this.Cadastro_Perfil.Name = "Cadastro_Perfil";
            this.Cadastro_Perfil.Size = new System.Drawing.Size(180, 22);
            this.Cadastro_Perfil.Text = "Perfil";
            this.Cadastro_Perfil.Click += new System.EventHandler(this.Cadastro_Perfil_Click);
            // 
            // Cadastro_Usuario
            // 
            this.Cadastro_Usuario.Name = "Cadastro_Usuario";
            this.Cadastro_Usuario.Size = new System.Drawing.Size(180, 22);
            this.Cadastro_Usuario.Text = "Usuário";
            this.Cadastro_Usuario.Click += new System.EventHandler(this.Cadastro_Usuario_Click);
            // 
            // Cadastro_Permissoes
            // 
            this.Cadastro_Permissoes.Name = "Cadastro_Permissoes";
            this.Cadastro_Permissoes.Size = new System.Drawing.Size(180, 22);
            this.Cadastro_Permissoes.Text = "Permissões";
            this.Cadastro_Permissoes.Click += new System.EventHandler(this.Cadastro_Permissoes_Click);
            // 
            // ETL
            // 
            this.ETL.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ETL_Servicos,
            this.ETL_Pacotes,
            this.ETL_Arquivos});
            this.ETL.Name = "ETL";
            this.ETL.Size = new System.Drawing.Size(38, 20);
            this.ETL.Text = "ETL";
            // 
            // ETL_Servicos
            // 
            this.ETL_Servicos.Name = "ETL_Servicos";
            this.ETL_Servicos.Size = new System.Drawing.Size(121, 22);
            this.ETL_Servicos.Text = "Serviços";
            this.ETL_Servicos.Click += new System.EventHandler(this.Cadastro_Servicos_Click);
            // 
            // ETL_Pacotes
            // 
            this.ETL_Pacotes.Name = "ETL_Pacotes";
            this.ETL_Pacotes.Size = new System.Drawing.Size(121, 22);
            this.ETL_Pacotes.Text = "Pacotes";
            this.ETL_Pacotes.Click += new System.EventHandler(this.Cadastro_Pacotes_Click);
            // 
            // ETL_Arquivos
            // 
            this.ETL_Arquivos.Name = "ETL_Arquivos";
            this.ETL_Arquivos.Size = new System.Drawing.Size(121, 22);
            this.ETL_Arquivos.Text = "Arquivos";
            this.ETL_Arquivos.Click += new System.EventHandler(this.Cadastro_Arquivos_Click);
            // 
            // Processos
            // 
            this.Processos.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Processos_ExportarTabelas,
            this.Processos_ImportarTabelas,
            this.Processos_VisualizarAndamento});
            this.Processos.Name = "Processos";
            this.Processos.Size = new System.Drawing.Size(71, 20);
            this.Processos.Text = "Processos";
            // 
            // Processos_ExportarTabelas
            // 
            this.Processos_ExportarTabelas.Name = "Processos_ExportarTabelas";
            this.Processos_ExportarTabelas.Size = new System.Drawing.Size(189, 22);
            this.Processos_ExportarTabelas.Tag = "1";
            this.Processos_ExportarTabelas.Text = "Exportar Tabelas";
            this.Processos_ExportarTabelas.Click += new System.EventHandler(this.Processos_ExportarImportarTabelas_Click);
            // 
            // Processos_ImportarTabelas
            // 
            this.Processos_ImportarTabelas.Name = "Processos_ImportarTabelas";
            this.Processos_ImportarTabelas.Size = new System.Drawing.Size(189, 22);
            this.Processos_ImportarTabelas.Tag = "2";
            this.Processos_ImportarTabelas.Text = "Importar Tabelas";
            this.Processos_ImportarTabelas.Click += new System.EventHandler(this.Processos_ImportarTabelas_Click);
            // 
            // Processos_VisualizarAndamento
            // 
            this.Processos_VisualizarAndamento.Name = "Processos_VisualizarAndamento";
            this.Processos_VisualizarAndamento.Size = new System.Drawing.Size(189, 22);
            this.Processos_VisualizarAndamento.Text = "Visualizar Andamento";
            this.Processos_VisualizarAndamento.Click += new System.EventHandler(this.visualizarAndamentoToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblCategoria);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lblSeparador);
            this.panel1.Controls.Add(this.lblGrupo);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1179, 36);
            this.panel1.TabIndex = 2;
            // 
            // lblCategoria
            // 
            this.lblCategoria.AutoSize = true;
            this.lblCategoria.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblCategoria.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategoria.ForeColor = System.Drawing.Color.Chocolate;
            this.lblCategoria.Location = new System.Drawing.Point(539, 0);
            this.lblCategoria.Name = "lblCategoria";
            this.lblCategoria.Size = new System.Drawing.Size(391, 31);
            this.lblCategoria.TabIndex = 4;
            this.lblCategoria.Text = "Filial da empresa Farmaceutica";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(454, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 31);
            this.label2.TabIndex = 3;
            this.label2.Text = "Filial:";
            // 
            // lblSeparador
            // 
            this.lblSeparador.AutoSize = true;
            this.lblSeparador.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblSeparador.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSeparador.Location = new System.Drawing.Point(432, 0);
            this.lblSeparador.Name = "lblSeparador";
            this.lblSeparador.Size = new System.Drawing.Size(22, 31);
            this.lblSeparador.TabIndex = 2;
            this.lblSeparador.Text = " ";
            // 
            // lblGrupo
            // 
            this.lblGrupo.AutoSize = true;
            this.lblGrupo.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblGrupo.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGrupo.ForeColor = System.Drawing.Color.Maroon;
            this.lblGrupo.Location = new System.Drawing.Point(138, 0);
            this.lblGrupo.Name = "lblGrupo";
            this.lblGrupo.Size = new System.Drawing.Size(294, 31);
            this.lblGrupo.TabIndex = 1;
            this.lblGrupo.Text = "Empresa Farmaceutica";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Empresa:";
            // 
            // pnlRodape
            // 
            this.pnlRodape.Controls.Add(this.pnlUsuarioLogado);
            this.pnlRodape.Controls.Add(this.pnlBackupData);
            this.pnlRodape.Controls.Add(this.pnlBackupSucesso);
            this.pnlRodape.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlRodape.Location = new System.Drawing.Point(0, 428);
            this.pnlRodape.Name = "pnlRodape";
            this.pnlRodape.Size = new System.Drawing.Size(1179, 22);
            this.pnlRodape.TabIndex = 4;
            // 
            // pnlUsuarioLogado
            // 
            this.pnlUsuarioLogado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlUsuarioLogado.Controls.Add(this.lblUsuarioLogado);
            this.pnlUsuarioLogado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlUsuarioLogado.Location = new System.Drawing.Point(0, 0);
            this.pnlUsuarioLogado.Name = "pnlUsuarioLogado";
            this.pnlUsuarioLogado.Size = new System.Drawing.Size(394, 22);
            this.pnlUsuarioLogado.TabIndex = 3;
            // 
            // lblUsuarioLogado
            // 
            this.lblUsuarioLogado.AutoSize = true;
            this.lblUsuarioLogado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblUsuarioLogado.Location = new System.Drawing.Point(0, 0);
            this.lblUsuarioLogado.Name = "lblUsuarioLogado";
            this.lblUsuarioLogado.Size = new System.Drawing.Size(35, 13);
            this.lblUsuarioLogado.TabIndex = 0;
            this.lblUsuarioLogado.Text = "label3";
            // 
            // pnlBackupData
            // 
            this.pnlBackupData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBackupData.Controls.Add(this.lblBackupData);
            this.pnlBackupData.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlBackupData.Location = new System.Drawing.Point(394, 0);
            this.pnlBackupData.Name = "pnlBackupData";
            this.pnlBackupData.Size = new System.Drawing.Size(200, 22);
            this.pnlBackupData.TabIndex = 2;
            // 
            // lblBackupData
            // 
            this.lblBackupData.AutoSize = true;
            this.lblBackupData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBackupData.Location = new System.Drawing.Point(0, 0);
            this.lblBackupData.Name = "lblBackupData";
            this.lblBackupData.Size = new System.Drawing.Size(35, 13);
            this.lblBackupData.TabIndex = 0;
            this.lblBackupData.Text = "label3";
            // 
            // pnlBackupSucesso
            // 
            this.pnlBackupSucesso.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBackupSucesso.Controls.Add(this.lblBackupSucesso);
            this.pnlBackupSucesso.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlBackupSucesso.Location = new System.Drawing.Point(594, 0);
            this.pnlBackupSucesso.Name = "pnlBackupSucesso";
            this.pnlBackupSucesso.Size = new System.Drawing.Size(585, 22);
            this.pnlBackupSucesso.TabIndex = 1;
            // 
            // lblBackupSucesso
            // 
            this.lblBackupSucesso.AutoSize = true;
            this.lblBackupSucesso.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBackupSucesso.Location = new System.Drawing.Point(0, 0);
            this.lblBackupSucesso.Name = "lblBackupSucesso";
            this.lblBackupSucesso.Size = new System.Drawing.Size(35, 13);
            this.lblBackupSucesso.TabIndex = 0;
            this.lblBackupSucesso.Text = "label3";
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1179, 450);
            this.Controls.Add(this.pnlRodape);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Menu);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.Menu;
            this.Name = "frmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ETL Starline";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.frmPrincipal_Activated);
            this.Load += new System.EventHandler(this.frmPrincipal_Load);
            this.Shown += new System.EventHandler(this.frmPrincipal_Shown);
            this.Menu.ResumeLayout(false);
            this.Menu.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlRodape.ResumeLayout(false);
            this.pnlUsuarioLogado.ResumeLayout(false);
            this.pnlUsuarioLogado.PerformLayout();
            this.pnlBackupData.ResumeLayout(false);
            this.pnlBackupData.PerformLayout();
            this.pnlBackupSucesso.ResumeLayout(false);
            this.pnlBackupSucesso.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip Menu;
        private System.Windows.Forms.ToolStripMenuItem Sistema;
        private System.Windows.Forms.ToolStripMenuItem Sistema_Configuracao;
        private System.Windows.Forms.ToolStripMenuItem Sistema_Usuarios;
        private System.Windows.Forms.ToolStripMenuItem Sistema_Usuarios_AlterarSenha;
        private System.Windows.Forms.ToolStripMenuItem Sistema_Usuarios_MudarUsuario;
        private System.Windows.Forms.ToolStripMenuItem Sistema_Sair;
        private System.Windows.Forms.ToolStripMenuItem Cadastro;
        private System.Windows.Forms.ToolStripMenuItem Cadastro_Perfil;
        private System.Windows.Forms.ToolStripMenuItem Cadastro_Usuario;
        private System.Windows.Forms.ToolStripMenuItem Cadastro_Permissoes;
        private System.Windows.Forms.ToolStripMenuItem Cadastro_Empresa;
        private System.Windows.Forms.ToolStripMenuItem Cadastro_Filial;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCategoria;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSeparador;
        private System.Windows.Forms.Label lblGrupo;
        private System.Windows.Forms.ToolStripMenuItem Sistema_Configuracao_SelecionarEmpresa;
        private System.Windows.Forms.ToolStripMenuItem Cadastro_Parametros;
        private System.Windows.Forms.ToolStripMenuItem Processos;
        private System.Windows.Forms.ToolStripMenuItem Processos_ExportarTabelas;
        private System.Windows.Forms.Panel pnlRodape;
        private System.Windows.Forms.Panel pnlUsuarioLogado;
        private System.Windows.Forms.Panel pnlBackupData;
        private System.Windows.Forms.Panel pnlBackupSucesso;
        private System.Windows.Forms.Label lblUsuarioLogado;
        private System.Windows.Forms.Label lblBackupData;
        private System.Windows.Forms.Label lblBackupSucesso;
        private System.Windows.Forms.ToolStripMenuItem ETL;
        private System.Windows.Forms.ToolStripMenuItem ETL_Servicos;
        private System.Windows.Forms.ToolStripMenuItem ETL_Pacotes;
        private System.Windows.Forms.ToolStripMenuItem ETL_Arquivos;
        private System.Windows.Forms.ToolStripMenuItem Processos_ImportarTabelas;
        private System.Windows.Forms.ToolStripMenuItem Processos_VisualizarAndamento;
    }
}