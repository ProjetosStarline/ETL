namespace ETLApplication.View
{
    partial class frmUsuarios
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.edtNmUsuario = new System.Windows.Forms.TextBox();
            this.edtIdUsuario = new System.Windows.Forms.MaskedTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.edtDataAtualizacao = new System.Windows.Forms.MaskedTextBox();
            this.edtDataCriacao = new System.Windows.Forms.MaskedTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.edtLogin = new System.Windows.Forms.TextBox();
            this.edtSenha = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.edtEmail = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cbxPerfil = new System.Windows.Forms.ComboBox();
            this.edtConfirma = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cbStatus = new System.Windows.Forms.ComboBox();
            this.lblSenhaCripto = new System.Windows.Forms.Label();
            this.lblTamanhoSenha = new System.Windows.Forms.Label();
            this.DgvUsoCateg = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Filial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Permitir = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.label11 = new System.Windows.Forms.Label();
            this.cbTodasFiliais = new System.Windows.Forms.CheckBox();
            this.ckbRecebeNotificacao = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.pnlBarraBotoes.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvUsoCateg)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ckbRecebeNotificacao);
            this.groupBox1.Controls.Add(this.cbTodasFiliais);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.DgvUsoCateg);
            this.groupBox1.Controls.Add(this.lblTamanhoSenha);
            this.groupBox1.Controls.Add(this.lblSenhaCripto);
            this.groupBox1.Controls.Add(this.cbStatus);
            this.groupBox1.Controls.Add(this.edtConfirma);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.cbxPerfil);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.edtEmail);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.edtSenha);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.edtLogin);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.edtNmUsuario);
            this.groupBox1.Controls.Add(this.edtIdUsuario);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Size = new System.Drawing.Size(847, 433);
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
            this.pnlBarraBotoes.Size = new System.Drawing.Size(847, 36);
            // 
            // btnPesquisar
            // 
            this.btnPesquisar.Click += new System.EventHandler(this.btnPesquisar_Click);
            // 
            // btnExcluir
            // 
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // edtNmUsuario
            // 
            this.edtNmUsuario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.edtNmUsuario.Location = new System.Drawing.Point(65, 104);
            this.edtNmUsuario.Name = "edtNmUsuario";
            this.edtNmUsuario.Size = new System.Drawing.Size(349, 20);
            this.edtNmUsuario.TabIndex = 2;
            // 
            // edtIdUsuario
            // 
            this.edtIdUsuario.Location = new System.Drawing.Point(66, 67);
            this.edtIdUsuario.Mask = "00000";
            this.edtIdUsuario.Name = "edtIdUsuario";
            this.edtIdUsuario.Size = new System.Drawing.Size(100, 20);
            this.edtIdUsuario.TabIndex = 14;
            this.edtIdUsuario.ValidatingType = typeof(int);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.edtDataAtualizacao);
            this.groupBox2.Controls.Add(this.edtDataCriacao);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Enabled = false;
            this.groupBox2.Location = new System.Drawing.Point(14, 315);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(404, 56);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Data";
            // 
            // edtDataAtualizacao
            // 
            this.edtDataAtualizacao.Location = new System.Drawing.Point(261, 19);
            this.edtDataAtualizacao.Name = "edtDataAtualizacao";
            this.edtDataAtualizacao.Size = new System.Drawing.Size(115, 20);
            this.edtDataAtualizacao.TabIndex = 8;
            this.edtDataAtualizacao.ValidatingType = typeof(System.DateTime);
            // 
            // edtDataCriacao
            // 
            this.edtDataCriacao.Location = new System.Drawing.Point(54, 19);
            this.edtDataCriacao.Name = "edtDataCriacao";
            this.edtDataCriacao.Size = new System.Drawing.Size(112, 20);
            this.edtDataCriacao.TabIndex = 7;
            this.edtDataCriacao.ValidatingType = typeof(System.DateTime);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(195, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Atualização";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Criação";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 138);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Status";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Nome";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Código";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 173);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Login";
            // 
            // edtLogin
            // 
            this.edtLogin.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.edtLogin.Location = new System.Drawing.Point(65, 170);
            this.edtLogin.Name = "edtLogin";
            this.edtLogin.Size = new System.Drawing.Size(123, 20);
            this.edtLogin.TabIndex = 4;
            // 
            // edtSenha
            // 
            this.edtSenha.Location = new System.Drawing.Point(65, 196);
            this.edtSenha.Name = "edtSenha";
            this.edtSenha.PasswordChar = '*';
            this.edtSenha.Size = new System.Drawing.Size(123, 20);
            this.edtSenha.TabIndex = 5;
            this.edtSenha.TextChanged += new System.EventHandler(this.edtSenha_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 200);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "Senha";
            // 
            // edtEmail
            // 
            this.edtEmail.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.edtEmail.Location = new System.Drawing.Point(65, 248);
            this.edtEmail.Name = "edtEmail";
            this.edtEmail.Size = new System.Drawing.Size(349, 20);
            this.edtEmail.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(26, 251);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "E-mail";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(207, 72);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(30, 13);
            this.label9.TabIndex = 23;
            this.label9.Text = "Perfil";
            // 
            // cbxPerfil
            // 
            this.cbxPerfil.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPerfil.FormattingEnabled = true;
            this.cbxPerfil.Location = new System.Drawing.Point(241, 67);
            this.cbxPerfil.Name = "cbxPerfil";
            this.cbxPerfil.Size = new System.Drawing.Size(173, 21);
            this.cbxPerfil.TabIndex = 1;
            // 
            // edtConfirma
            // 
            this.edtConfirma.Location = new System.Drawing.Point(66, 222);
            this.edtConfirma.Name = "edtConfirma";
            this.edtConfirma.PasswordChar = '*';
            this.edtConfirma.Size = new System.Drawing.Size(123, 20);
            this.edtConfirma.TabIndex = 6;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(16, 226);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(48, 13);
            this.label10.TabIndex = 25;
            this.label10.Text = "Confirma";
            // 
            // cbStatus
            // 
            this.cbStatus.DisplayMember = "ATIVO";
            this.cbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStatus.FormattingEnabled = true;
            this.cbStatus.Items.AddRange(new object[] {
            "ativo",
            "inativo"});
            this.cbStatus.Location = new System.Drawing.Point(65, 134);
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.Size = new System.Drawing.Size(121, 21);
            this.cbStatus.TabIndex = 3;
            this.cbStatus.ValueMember = "0";
            // 
            // lblSenhaCripto
            // 
            this.lblSenhaCripto.BackColor = System.Drawing.SystemColors.ControlDark;
            this.lblSenhaCripto.Location = new System.Drawing.Point(193, 199);
            this.lblSenhaCripto.Name = "lblSenhaCripto";
            this.lblSenhaCripto.Size = new System.Drawing.Size(225, 17);
            this.lblSenhaCripto.TabIndex = 26;
            this.lblSenhaCripto.Text = "                                         ";
            this.lblSenhaCripto.Visible = false;
            // 
            // lblTamanhoSenha
            // 
            this.lblTamanhoSenha.BackColor = System.Drawing.SystemColors.ControlDark;
            this.lblTamanhoSenha.Location = new System.Drawing.Point(193, 225);
            this.lblTamanhoSenha.Name = "lblTamanhoSenha";
            this.lblTamanhoSenha.Size = new System.Drawing.Size(225, 17);
            this.lblTamanhoSenha.TabIndex = 27;
            this.lblTamanhoSenha.Visible = false;
            // 
            // DgvUsoCateg
            // 
            this.DgvUsoCateg.AllowUserToAddRows = false;
            this.DgvUsoCateg.AllowUserToDeleteRows = false;
            this.DgvUsoCateg.AllowUserToOrderColumns = true;
            this.DgvUsoCateg.AllowUserToResizeColumns = false;
            this.DgvUsoCateg.AllowUserToResizeRows = false;
            this.DgvUsoCateg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvUsoCateg.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Filial,
            this.Permitir});
            this.DgvUsoCateg.Location = new System.Drawing.Point(424, 63);
            this.DgvUsoCateg.Name = "DgvUsoCateg";
            this.DgvUsoCateg.Size = new System.Drawing.Size(418, 364);
            this.DgvUsoCateg.TabIndex = 28;
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Width = 50;
            // 
            // Filial
            // 
            this.Filial.HeaderText = "Filial";
            this.Filial.Name = "Filial";
            this.Filial.ReadOnly = true;
            this.Filial.Width = 250;
            // 
            // Permitir
            // 
            this.Permitir.FalseValue = "NÃO";
            this.Permitir.HeaderText = "Permitir Acesso";
            this.Permitir.Name = "Permitir";
            this.Permitir.TrueValue = "SIM";
            this.Permitir.Width = 50;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(427, 43);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(218, 13);
            this.label11.TabIndex = 29;
            this.label11.Text = "Informe as Filiais que o usuário pode acessar";
            // 
            // cbTodasFiliais
            // 
            this.cbTodasFiliais.AutoSize = true;
            this.cbTodasFiliais.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbTodasFiliais.Location = new System.Drawing.Point(699, 43);
            this.cbTodasFiliais.Name = "cbTodasFiliais";
            this.cbTodasFiliais.Size = new System.Drawing.Size(98, 17);
            this.cbTodasFiliais.TabIndex = 30;
            this.cbTodasFiliais.Text = "Todas as Filiais";
            this.cbTodasFiliais.UseVisualStyleBackColor = true;
            this.cbTodasFiliais.CheckedChanged += new System.EventHandler(this.cbTodasFiliais_CheckedChanged);
            // 
            // ckbRecebeNotificacao
            // 
            this.ckbRecebeNotificacao.AutoSize = true;
            this.ckbRecebeNotificacao.Location = new System.Drawing.Point(66, 274);
            this.ckbRecebeNotificacao.Name = "ckbRecebeNotificacao";
            this.ckbRecebeNotificacao.Size = new System.Drawing.Size(121, 17);
            this.ckbRecebeNotificacao.TabIndex = 31;
            this.ckbRecebeNotificacao.Text = "Recebe Notificação";
            this.ckbRecebeNotificacao.UseVisualStyleBackColor = true;
            // 
            // frmUsuarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(847, 469);
            this.Name = "frmUsuarios";
            this.Text = "Cadastro de Usuários";
            this.Load += new System.EventHandler(this.frmUsuarios_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnlBarraBotoes.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvUsoCateg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox edtNmUsuario;
        private System.Windows.Forms.MaskedTextBox edtIdUsuario;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.MaskedTextBox edtDataAtualizacao;
        private System.Windows.Forms.MaskedTextBox edtDataCriacao;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox edtSenha;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox edtLogin;
        private System.Windows.Forms.TextBox edtEmail;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbxPerfil;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox edtConfirma;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbStatus;
        private System.Windows.Forms.Label lblSenhaCripto;
        private System.Windows.Forms.Label lblTamanhoSenha;
        private System.Windows.Forms.DataGridView DgvUsoCateg;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Filial;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Permitir;
        private System.Windows.Forms.CheckBox cbTodasFiliais;
        private System.Windows.Forms.CheckBox ckbRecebeNotificacao;
    }
}
