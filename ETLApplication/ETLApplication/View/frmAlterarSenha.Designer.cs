namespace ETLApplication.View
{
    partial class frmAlterarSenha
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAlterarSenha));
            this.pnlRodape = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnOk = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.edtConfirma = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.edtSenha = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.edtSenhaAtual = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlRodape.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlRodape
            // 
            this.pnlRodape.Controls.Add(this.panel2);
            this.pnlRodape.Controls.Add(this.panel1);
            this.pnlRodape.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlRodape.Location = new System.Drawing.Point(0, 197);
            this.pnlRodape.Name = "pnlRodape";
            this.pnlRodape.Size = new System.Drawing.Size(301, 36);
            this.pnlRodape.TabIndex = 9;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnOk);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(144, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(78, 36);
            this.panel2.TabIndex = 7;
            // 
            // btnOk
            // 
            this.btnOk.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOk.Image = ((System.Drawing.Image)(resources.GetObject("btnOk.Image")));
            this.btnOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOk.Location = new System.Drawing.Point(3, 0);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 36);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancelar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(222, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(79, 36);
            this.panel1.TabIndex = 6;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(4, 0);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 36);
            this.btnCancelar.TabIndex = 6;
            this.btnCancelar.Text = "  Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // edtConfirma
            // 
            this.edtConfirma.Location = new System.Drawing.Point(99, 121);
            this.edtConfirma.Name = "edtConfirma";
            this.edtConfirma.PasswordChar = '*';
            this.edtConfirma.Size = new System.Drawing.Size(123, 20);
            this.edtConfirma.TabIndex = 28;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(15, 121);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(48, 13);
            this.label10.TabIndex = 31;
            this.label10.Text = "Confirma";
            // 
            // edtSenha
            // 
            this.edtSenha.Location = new System.Drawing.Point(99, 95);
            this.edtSenha.Name = "edtSenha";
            this.edtSenha.PasswordChar = '*';
            this.edtSenha.Size = new System.Drawing.Size(123, 20);
            this.edtSenha.TabIndex = 27;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 95);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 13);
            this.label7.TabIndex = 30;
            this.label7.Text = "Nova Senha";
            // 
            // edtSenhaAtual
            // 
            this.edtSenhaAtual.Location = new System.Drawing.Point(99, 69);
            this.edtSenhaAtual.Name = "edtSenhaAtual";
            this.edtSenhaAtual.PasswordChar = '*';
            this.edtSenhaAtual.Size = new System.Drawing.Size(123, 20);
            this.edtSenhaAtual.TabIndex = 26;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 29;
            this.label4.Text = "Senha Atual";
            // 
            // frmAlterarSenha
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(301, 233);
            this.Controls.Add(this.edtConfirma);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.edtSenha);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.edtSenhaAtual);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pnlRodape);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAlterarSenha";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Alteração de Senha";
            this.pnlRodape.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlRodape;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.TextBox edtConfirma;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox edtSenha;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox edtSenhaAtual;
        private System.Windows.Forms.Label label4;
    }
}