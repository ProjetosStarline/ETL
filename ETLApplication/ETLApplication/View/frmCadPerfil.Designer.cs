namespace ETLApplication.View
{
    partial class frmCadPerfil
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.edtDataAtualizacao = new System.Windows.Forms.MaskedTextBox();
            this.edtDataCriacao = new System.Windows.Forms.MaskedTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.edtIdPerfil = new System.Windows.Forms.MaskedTextBox();
            this.edtNmPerfil = new System.Windows.Forms.TextBox();
            this.cbStatus = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.pnlBarraBotoes.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbStatus);
            this.groupBox1.Controls.Add(this.edtNmPerfil);
            this.groupBox1.Controls.Add(this.edtIdPerfil);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Size = new System.Drawing.Size(429, 280);
            // 
            // btnOk
            // 
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // pnlBarraBotoes
            // 
            this.pnlBarraBotoes.Size = new System.Drawing.Size(429, 36);
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
            this.label1.Location = new System.Drawing.Point(12, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Código";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Nome";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Status";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.edtDataAtualizacao);
            this.groupBox2.Controls.Add(this.edtDataCriacao);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Enabled = false;
            this.groupBox2.Location = new System.Drawing.Point(15, 207);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(408, 67);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Data";
            // 
            // edtDataAtualizacao
            // 
            this.edtDataAtualizacao.Location = new System.Drawing.Point(295, 30);
            this.edtDataAtualizacao.Name = "edtDataAtualizacao";
            this.edtDataAtualizacao.Size = new System.Drawing.Size(109, 20);
            this.edtDataAtualizacao.TabIndex = 8;
            this.edtDataAtualizacao.ValidatingType = typeof(System.DateTime);
            // 
            // edtDataCriacao
            // 
            this.edtDataCriacao.Location = new System.Drawing.Point(84, 30);
            this.edtDataCriacao.Name = "edtDataCriacao";
            this.edtDataCriacao.Size = new System.Drawing.Size(110, 20);
            this.edtDataCriacao.TabIndex = 7;
            this.edtDataCriacao.ValidatingType = typeof(System.DateTime);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(227, 37);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Atualização";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Criação";
            // 
            // edtIdPerfil
            // 
            this.edtIdPerfil.Location = new System.Drawing.Point(92, 46);
            this.edtIdPerfil.Mask = "00000";
            this.edtIdPerfil.Name = "edtIdPerfil";
            this.edtIdPerfil.Size = new System.Drawing.Size(100, 20);
            this.edtIdPerfil.TabIndex = 7;
            this.edtIdPerfil.ValidatingType = typeof(int);
            // 
            // edtNmPerfil
            // 
            this.edtNmPerfil.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.edtNmPerfil.Location = new System.Drawing.Point(91, 75);
            this.edtNmPerfil.Name = "edtNmPerfil";
            this.edtNmPerfil.Size = new System.Drawing.Size(237, 20);
            this.edtNmPerfil.TabIndex = 8;
            // 
            // cbStatus
            // 
            this.cbStatus.DisplayMember = "ATIVO";
            this.cbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStatus.FormattingEnabled = true;
            this.cbStatus.Items.AddRange(new object[] {
            "ativo",
            "inativo"});
            this.cbStatus.Location = new System.Drawing.Point(92, 105);
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.Size = new System.Drawing.Size(121, 21);
            this.cbStatus.TabIndex = 9;
            // 
            // frmCadPerfil
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(429, 316);
            this.Name = "frmCadPerfil";
            this.Text = "Cadastro de Perfil";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnlBarraBotoes.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox edtNmPerfil;
        private System.Windows.Forms.MaskedTextBox edtIdPerfil;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.MaskedTextBox edtDataAtualizacao;
        private System.Windows.Forms.MaskedTextBox edtDataCriacao;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbStatus;
    }
}
