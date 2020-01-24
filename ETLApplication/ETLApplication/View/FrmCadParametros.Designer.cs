namespace ETLApplication.View
{
    partial class frmCadParametros
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
            this.edtValor = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.edtNome = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.pnlBarraBotoes.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbStatus);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.edtCodigo);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.edtValor);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.edtNome);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Size = new System.Drawing.Size(438, 197);
            // 
            // btnOk
            // 
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // pnlBarraBotoes
            // 
            this.pnlBarraBotoes.Size = new System.Drawing.Size(438, 36);
            // 
            // btnPesquisar
            // 
            this.btnPesquisar.Click += new System.EventHandler(this.btnPesquisar_Click);
            // 
            // btnAlterar
            // 
            this.btnAlterar.Click += new System.EventHandler(this.btnAlterar_Click);
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
            this.cbStatus.Location = new System.Drawing.Point(290, 42);
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.Size = new System.Drawing.Size(121, 21);
            this.cbStatus.TabIndex = 43;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(247, 46);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(40, 13);
            this.label12.TabIndex = 44;
            this.label12.Text = "Status:";
            // 
            // edtCodigo
            // 
            this.edtCodigo.Location = new System.Drawing.Point(91, 46);
            this.edtCodigo.Name = "edtCodigo";
            this.edtCodigo.Size = new System.Drawing.Size(66, 20);
            this.edtCodigo.TabIndex = 42;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(13, 50);
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
            this.groupBox3.Location = new System.Drawing.Point(13, 140);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(407, 39);
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
            // edtValor
            // 
            this.edtValor.Location = new System.Drawing.Point(91, 96);
            this.edtValor.Name = "edtValor";
            this.edtValor.Size = new System.Drawing.Size(320, 20);
            this.edtValor.TabIndex = 39;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(13, 100);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(72, 13);
            this.label10.TabIndex = 38;
            this.label10.Text = "Valor:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // edtNome
            // 
            this.edtNome.Location = new System.Drawing.Point(91, 70);
            this.edtNome.Name = "edtNome";
            this.edtNome.Size = new System.Drawing.Size(320, 20);
            this.edtNome.TabIndex = 37;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(13, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 36;
            this.label3.Text = "Nome:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmCadParametros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 233);
            this.Name = "frmCadParametros";
            this.Text = "Cadastro de Parametros";
            this.Load += new System.EventHandler(this.FrmCadParametros_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnlBarraBotoes.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbStatus;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox edtCodigo;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.MaskedTextBox edtDataAtualizacao;
        private System.Windows.Forms.MaskedTextBox edtDataCriacao;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox edtValor;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox edtNome;
        private System.Windows.Forms.Label label3;
    }
}