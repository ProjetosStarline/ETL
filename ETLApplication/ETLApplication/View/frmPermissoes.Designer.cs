namespace ETLApplication.View
{
    partial class frmPermissoes
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
            this.cbStatus = new System.Windows.Forms.ComboBox();
            this.edtIdPermissoes = new System.Windows.Forms.MaskedTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.edtDataAtualizacao = new System.Windows.Forms.MaskedTextBox();
            this.edtDataCriacao = new System.Windows.Forms.MaskedTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxPerfil = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvPermissoes = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Menu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbPermissoes = new System.Windows.Forms.GroupBox();
            this.btnPermitirAcessoTodosMenus = new System.Windows.Forms.Button();
            this.cbPermitirTodos = new System.Windows.Forms.CheckBox();
            this.cbCadastrar = new System.Windows.Forms.CheckBox();
            this.cbVisualizar = new System.Windows.Forms.CheckBox();
            this.cbDeletar = new System.Windows.Forms.CheckBox();
            this.cbAlterar = new System.Windows.Forms.CheckBox();
            this.cbPesquisar = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblMenu = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.pnlBarraBotoes.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPermissoes)).BeginInit();
            this.gbPermissoes.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblMenu);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.gbPermissoes);
            this.groupBox1.Controls.Add(this.dgvPermissoes);
            this.groupBox1.Controls.Add(this.cbxPerfil);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbStatus);
            this.groupBox1.Controls.Add(this.edtIdPermissoes);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 36);
            this.groupBox1.Size = new System.Drawing.Size(580, 397);
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
            this.pnlBarraBotoes.Dock = System.Windows.Forms.DockStyle.None;
            this.pnlBarraBotoes.Size = new System.Drawing.Size(580, 36);
            // 
            // btnPesquisar
            // 
            this.btnPesquisar.Click += new System.EventHandler(this.btnPesquisar_Click);
            // 
            // btnInserir
            // 
            this.btnInserir.Click += new System.EventHandler(this.btnInserir_Click);
            // 
            // cbStatus
            // 
            this.cbStatus.DisplayMember = "ativo";
            this.cbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStatus.FormattingEnabled = true;
            this.cbStatus.Items.AddRange(new object[] {
            "ativo",
            "inativo"});
            this.cbStatus.Location = new System.Drawing.Point(456, 18);
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.Size = new System.Drawing.Size(121, 21);
            this.cbStatus.TabIndex = 2;
            // 
            // edtIdPermissoes
            // 
            this.edtIdPermissoes.Location = new System.Drawing.Point(57, 19);
            this.edtIdPermissoes.Mask = "00000";
            this.edtIdPermissoes.Name = "edtIdPermissoes";
            this.edtIdPermissoes.Size = new System.Drawing.Size(40, 20);
            this.edtIdPermissoes.TabIndex = 14;
            this.edtIdPermissoes.ValidatingType = typeof(int);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.edtDataAtualizacao);
            this.groupBox2.Controls.Add(this.edtDataCriacao);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Enabled = false;
            this.groupBox2.Location = new System.Drawing.Point(10, 347);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(567, 44);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Data";
            // 
            // edtDataAtualizacao
            // 
            this.edtDataAtualizacao.Location = new System.Drawing.Point(435, 10);
            this.edtDataAtualizacao.Name = "edtDataAtualizacao";
            this.edtDataAtualizacao.Size = new System.Drawing.Size(110, 20);
            this.edtDataAtualizacao.TabIndex = 8;
            this.edtDataAtualizacao.ValidatingType = typeof(System.DateTime);
            // 
            // edtDataCriacao
            // 
            this.edtDataCriacao.Location = new System.Drawing.Point(89, 10);
            this.edtDataCriacao.Name = "edtDataCriacao";
            this.edtDataCriacao.Size = new System.Drawing.Size(110, 20);
            this.edtDataCriacao.TabIndex = 7;
            this.edtDataCriacao.ValidatingType = typeof(System.DateTime);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(367, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Atualização";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Criação";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(413, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Status";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Código";
            // 
            // cbxPerfil
            // 
            this.cbxPerfil.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPerfil.FormattingEnabled = true;
            this.cbxPerfil.Location = new System.Drawing.Point(163, 18);
            this.cbxPerfil.Name = "cbxPerfil";
            this.cbxPerfil.Size = new System.Drawing.Size(235, 21);
            this.cbxPerfil.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(124, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Perfil";
            // 
            // dgvPermissoes
            // 
            this.dgvPermissoes.AllowUserToAddRows = false;
            this.dgvPermissoes.AllowUserToDeleteRows = false;
            this.dgvPermissoes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPermissoes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Menu});
            this.dgvPermissoes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgvPermissoes.Location = new System.Drawing.Point(14, 77);
            this.dgvPermissoes.Name = "dgvPermissoes";
            this.dgvPermissoes.ReadOnly = true;
            this.dgvPermissoes.Size = new System.Drawing.Size(367, 264);
            this.dgvPermissoes.TabIndex = 3;
            this.dgvPermissoes.Click += new System.EventHandler(this.dgvPermissoes_Click);
            this.dgvPermissoes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dgvPermissoes_KeyPress);
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Width = 50;
            // 
            // Menu
            // 
            this.Menu.HeaderText = "Menu";
            this.Menu.Name = "Menu";
            this.Menu.ReadOnly = true;
            this.Menu.Width = 250;
            // 
            // gbPermissoes
            // 
            this.gbPermissoes.Controls.Add(this.btnPermitirAcessoTodosMenus);
            this.gbPermissoes.Controls.Add(this.cbPermitirTodos);
            this.gbPermissoes.Controls.Add(this.cbCadastrar);
            this.gbPermissoes.Controls.Add(this.cbVisualizar);
            this.gbPermissoes.Controls.Add(this.cbDeletar);
            this.gbPermissoes.Controls.Add(this.cbAlterar);
            this.gbPermissoes.Controls.Add(this.cbPesquisar);
            this.gbPermissoes.Location = new System.Drawing.Point(392, 77);
            this.gbPermissoes.Name = "gbPermissoes";
            this.gbPermissoes.Size = new System.Drawing.Size(185, 264);
            this.gbPermissoes.TabIndex = 4;
            this.gbPermissoes.TabStop = false;
            this.gbPermissoes.Text = "Permissões";
            // 
            // btnPermitirAcessoTodosMenus
            // 
            this.btnPermitirAcessoTodosMenus.Location = new System.Drawing.Point(2, 241);
            this.btnPermitirAcessoTodosMenus.Name = "btnPermitirAcessoTodosMenus";
            this.btnPermitirAcessoTodosMenus.Size = new System.Drawing.Size(175, 23);
            this.btnPermitirAcessoTodosMenus.TabIndex = 6;
            this.btnPermitirAcessoTodosMenus.Text = "Permitir acesso a todos os Menus";
            this.btnPermitirAcessoTodosMenus.UseVisualStyleBackColor = true;
            this.btnPermitirAcessoTodosMenus.Click += new System.EventHandler(this.btnPermitirAcessoTodosMenus_Click);
            // 
            // cbPermitirTodos
            // 
            this.cbPermitirTodos.AutoSize = true;
            this.cbPermitirTodos.Location = new System.Drawing.Point(27, 22);
            this.cbPermitirTodos.Name = "cbPermitirTodos";
            this.cbPermitirTodos.Size = new System.Drawing.Size(93, 17);
            this.cbPermitirTodos.TabIndex = 5;
            this.cbPermitirTodos.Text = "Permitir Todos";
            this.cbPermitirTodos.UseVisualStyleBackColor = true;
            this.cbPermitirTodos.Click += new System.EventHandler(this.cbPermitirTodos_Click);
            // 
            // cbCadastrar
            // 
            this.cbCadastrar.AutoSize = true;
            this.cbCadastrar.Location = new System.Drawing.Point(39, 60);
            this.cbCadastrar.Name = "cbCadastrar";
            this.cbCadastrar.Size = new System.Drawing.Size(71, 17);
            this.cbCadastrar.TabIndex = 4;
            this.cbCadastrar.Text = "Cadastrar";
            this.cbCadastrar.UseVisualStyleBackColor = true;
            this.cbCadastrar.CheckedChanged += new System.EventHandler(this.cbPesquisar_CheckedChanged);
            this.cbCadastrar.Click += new System.EventHandler(this.cbPesquisar_Click);
            // 
            // cbVisualizar
            // 
            this.cbVisualizar.AutoSize = true;
            this.cbVisualizar.Location = new System.Drawing.Point(39, 126);
            this.cbVisualizar.Name = "cbVisualizar";
            this.cbVisualizar.Size = new System.Drawing.Size(70, 17);
            this.cbVisualizar.TabIndex = 3;
            this.cbVisualizar.Text = "Visualizar";
            this.cbVisualizar.UseVisualStyleBackColor = true;
            this.cbVisualizar.CheckedChanged += new System.EventHandler(this.cbPesquisar_CheckedChanged);
            this.cbVisualizar.Click += new System.EventHandler(this.cbPesquisar_Click);
            // 
            // cbDeletar
            // 
            this.cbDeletar.AutoSize = true;
            this.cbDeletar.Location = new System.Drawing.Point(39, 103);
            this.cbDeletar.Name = "cbDeletar";
            this.cbDeletar.Size = new System.Drawing.Size(60, 17);
            this.cbDeletar.TabIndex = 2;
            this.cbDeletar.Text = "Deletar";
            this.cbDeletar.UseVisualStyleBackColor = true;
            this.cbDeletar.CheckedChanged += new System.EventHandler(this.cbPesquisar_CheckedChanged);
            this.cbDeletar.Click += new System.EventHandler(this.cbPesquisar_Click);
            // 
            // cbAlterar
            // 
            this.cbAlterar.AutoSize = true;
            this.cbAlterar.Location = new System.Drawing.Point(39, 80);
            this.cbAlterar.Name = "cbAlterar";
            this.cbAlterar.Size = new System.Drawing.Size(56, 17);
            this.cbAlterar.TabIndex = 1;
            this.cbAlterar.Text = "Alterar";
            this.cbAlterar.UseVisualStyleBackColor = true;
            this.cbAlterar.CheckedChanged += new System.EventHandler(this.cbPesquisar_CheckedChanged);
            this.cbAlterar.Click += new System.EventHandler(this.cbPesquisar_Click);
            // 
            // cbPesquisar
            // 
            this.cbPesquisar.AutoSize = true;
            this.cbPesquisar.Location = new System.Drawing.Point(39, 41);
            this.cbPesquisar.Name = "cbPesquisar";
            this.cbPesquisar.Size = new System.Drawing.Size(72, 17);
            this.cbPesquisar.TabIndex = 0;
            this.cbPesquisar.Text = "Pesquisar";
            this.cbPesquisar.UseVisualStyleBackColor = true;
            this.cbPesquisar.CheckedChanged += new System.EventHandler(this.cbPesquisar_CheckedChanged);
            this.cbPesquisar.Click += new System.EventHandler(this.cbPesquisar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Menu";
            // 
            // lblMenu
            // 
            this.lblMenu.AutoSize = true;
            this.lblMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMenu.Location = new System.Drawing.Point(56, 52);
            this.lblMenu.Name = "lblMenu";
            this.lblMenu.Size = new System.Drawing.Size(57, 20);
            this.lblMenu.TabIndex = 19;
            this.lblMenu.Text = "label7";
            // 
            // frmPermissoes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(580, 469);
            this.Name = "frmPermissoes";
            this.Text = "Cadastro de Permissões";
            this.Load += new System.EventHandler(this.frmPermissoes_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnlBarraBotoes.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPermissoes)).EndInit();
            this.gbPermissoes.ResumeLayout(false);
            this.gbPermissoes.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbStatus;
        private System.Windows.Forms.MaskedTextBox edtIdPermissoes;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.MaskedTextBox edtDataAtualizacao;
        private System.Windows.Forms.MaskedTextBox edtDataCriacao;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxPerfil;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvPermissoes;
        private System.Windows.Forms.GroupBox gbPermissoes;
        private System.Windows.Forms.CheckBox cbVisualizar;
        private System.Windows.Forms.CheckBox cbDeletar;
        private System.Windows.Forms.CheckBox cbAlterar;
        private System.Windows.Forms.CheckBox cbPesquisar;
        private System.Windows.Forms.CheckBox cbCadastrar;
        private System.Windows.Forms.Label lblMenu;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbPermitirTodos;
        private System.Windows.Forms.Button btnPermitirAcessoTodosMenus;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Menu;
    }
}
