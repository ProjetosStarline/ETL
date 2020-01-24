namespace ETLApplication.View
{
    partial class frmCadCategorias
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
            this.label12 = new System.Windows.Forms.Label();
            this.edtCodigo = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.edtDataAtualizacao = new System.Windows.Forms.MaskedTextBox();
            this.edtDataCriacao = new System.Windows.Forms.MaskedTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.edtDescricao = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.edtNome = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbGrupo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.edtPortaSmtp = new System.Windows.Forms.TextBox();
            this.edtHostSmtp = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.CkbEnableSSL = new System.Windows.Forms.CheckBox();
            this.edtUsuarioSmtp = new System.Windows.Forms.TextBox();
            this.edtSenhaSmtp = new System.Windows.Forms.TextBox();
            this.ckbEnviarSomenteErros = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.pnlBarraBotoes.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.cbGrupo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbStatus);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.edtCodigo);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.edtDescricao);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.edtNome);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Size = new System.Drawing.Size(440, 354);
            // 
            // btnOk
            // 
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // pnlBarraBotoes
            // 
            this.pnlBarraBotoes.Size = new System.Drawing.Size(440, 36);
            // 
            // btnPesquisar
            // 
            this.btnPesquisar.Click += new System.EventHandler(this.btnPesquisar_Click);
            // 
            // cbStatus
            // 
            this.cbStatus.DisplayMember = "ativo";
            this.cbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStatus.FormattingEnabled = true;
            this.cbStatus.Items.AddRange(new object[] {
            "ativo",
            "inativo"});
            this.cbStatus.Location = new System.Drawing.Point(290, 51);
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.Size = new System.Drawing.Size(121, 21);
            this.cbStatus.TabIndex = 2;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(247, 55);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(40, 13);
            this.label12.TabIndex = 44;
            this.label12.Text = "Status:";
            // 
            // edtCodigo
            // 
            this.edtCodigo.Location = new System.Drawing.Point(91, 55);
            this.edtCodigo.Name = "edtCodigo";
            this.edtCodigo.Size = new System.Drawing.Size(66, 20);
            this.edtCodigo.TabIndex = 1;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(13, 59);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(72, 13);
            this.label13.TabIndex = 41;
            this.label13.Text = "Código:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.edtDataAtualizacao);
            this.groupBox3.Controls.Add(this.edtDataCriacao);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Enabled = false;
            this.groupBox3.Location = new System.Drawing.Point(3, 309);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(419, 39);
            this.groupBox3.TabIndex = 40;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Data";
            // 
            // edtDataAtualizacao
            // 
            this.edtDataAtualizacao.Location = new System.Drawing.Point(288, 9);
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
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(220, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Atualização";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(11, 17);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(43, 13);
            this.label11.TabIndex = 5;
            this.label11.Text = "Criação";
            // 
            // edtDescricao
            // 
            this.edtDescricao.Location = new System.Drawing.Point(91, 129);
            this.edtDescricao.Name = "edtDescricao";
            this.edtDescricao.Size = new System.Drawing.Size(320, 20);
            this.edtDescricao.TabIndex = 5;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(13, 133);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(72, 13);
            this.label10.TabIndex = 38;
            this.label10.Text = "Descrição:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // edtNome
            // 
            this.edtNome.Location = new System.Drawing.Point(91, 103);
            this.edtNome.Name = "edtNome";
            this.edtNome.Size = new System.Drawing.Size(320, 20);
            this.edtNome.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(13, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 36;
            this.label3.Text = "Nome:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbGrupo
            // 
            this.cbGrupo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGrupo.FormattingEnabled = true;
            this.cbGrupo.Location = new System.Drawing.Point(91, 79);
            this.cbGrupo.Name = "cbGrupo";
            this.cbGrupo.Size = new System.Drawing.Size(319, 21);
            this.cbGrupo.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 46;
            this.label1.Text = "Empresa:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ckbEnviarSomenteErros);
            this.groupBox2.Controls.Add(this.edtSenhaSmtp);
            this.groupBox2.Controls.Add(this.edtUsuarioSmtp);
            this.groupBox2.Controls.Add(this.CkbEnableSSL);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.edtPortaSmtp);
            this.groupBox2.Controls.Add(this.edtHostSmtp);
            this.groupBox2.Location = new System.Drawing.Point(0, 167);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(424, 136);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Envio de eMail";
            // 
            // edtPortaSmtp
            // 
            this.edtPortaSmtp.Location = new System.Drawing.Point(92, 45);
            this.edtPortaSmtp.Name = "edtPortaSmtp";
            this.edtPortaSmtp.Size = new System.Drawing.Size(55, 20);
            this.edtPortaSmtp.TabIndex = 8;
            // 
            // edtHostSmtp
            // 
            this.edtHostSmtp.Location = new System.Drawing.Point(92, 19);
            this.edtHostSmtp.Name = "edtHostSmtp";
            this.edtHostSmtp.Size = new System.Drawing.Size(308, 20);
            this.edtHostSmtp.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(49, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 51;
            this.label2.Text = "Porta:";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(12, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 19);
            this.label4.TabIndex = 50;
            this.label4.Text = "Host  Smtp";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(12, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 19);
            this.label5.TabIndex = 49;
            this.label5.Text = "Senha Smtp:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(6, 87);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 19);
            this.label6.TabIndex = 48;
            this.label6.Text = "Usuário Smtp:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CkbEnableSSL
            // 
            this.CkbEnableSSL.AutoSize = true;
            this.CkbEnableSSL.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CkbEnableSSL.Location = new System.Drawing.Point(22, 68);
            this.CkbEnableSSL.Name = "CkbEnableSSL";
            this.CkbEnableSSL.Size = new System.Drawing.Size(84, 17);
            this.CkbEnableSSL.TabIndex = 9;
            this.CkbEnableSSL.Text = "Habilita SSL";
            this.CkbEnableSSL.UseVisualStyleBackColor = true;
            // 
            // edtUsuarioSmtp
            // 
            this.edtUsuarioSmtp.Location = new System.Drawing.Point(92, 86);
            this.edtUsuarioSmtp.Name = "edtUsuarioSmtp";
            this.edtUsuarioSmtp.Size = new System.Drawing.Size(308, 20);
            this.edtUsuarioSmtp.TabIndex = 10;
            // 
            // edtSenhaSmtp
            // 
            this.edtSenhaSmtp.Location = new System.Drawing.Point(92, 109);
            this.edtSenhaSmtp.Name = "edtSenhaSmtp";
            this.edtSenhaSmtp.PasswordChar = '*';
            this.edtSenhaSmtp.Size = new System.Drawing.Size(109, 20);
            this.edtSenhaSmtp.TabIndex = 11;
            // 
            // ckbEnviarSomenteErros
            // 
            this.ckbEnviarSomenteErros.AutoSize = true;
            this.ckbEnviarSomenteErros.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbEnviarSomenteErros.Location = new System.Drawing.Point(258, 109);
            this.ckbEnviarSomenteErros.Name = "ckbEnviarSomenteErros";
            this.ckbEnviarSomenteErros.Size = new System.Drawing.Size(140, 17);
            this.ckbEnviarSomenteErros.TabIndex = 12;
            this.ckbEnviarSomenteErros.Text = "Enviar somente os Erros";
            this.ckbEnviarSomenteErros.UseVisualStyleBackColor = true;
            // 
            // frmCadCategorias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 390);
            this.Name = "frmCadCategorias";
            this.Text = "Cadastro de Filial";
            this.Load += new System.EventHandler(this.frmCadCategorias_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnlBarraBotoes.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbGrupo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbStatus;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox edtCodigo;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.MaskedTextBox edtDataAtualizacao;
        private System.Windows.Forms.MaskedTextBox edtDataCriacao;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox edtDescricao;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox edtNome;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox edtPortaSmtp;
        private System.Windows.Forms.TextBox edtHostSmtp;
        private System.Windows.Forms.TextBox edtSenhaSmtp;
        private System.Windows.Forms.TextBox edtUsuarioSmtp;
        private System.Windows.Forms.CheckBox CkbEnableSSL;
        private System.Windows.Forms.CheckBox ckbEnviarSomenteErros;
    }
}