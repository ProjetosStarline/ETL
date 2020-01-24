namespace ETLApplication
{
    partial class frmPesquisa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPesquisa));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.pnlRodape = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnOk = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.pnlBarraBotoes = new System.Windows.Forms.Panel();
            this.edtBusca = new System.Windows.Forms.MaskedTextBox();
            this.btnBusca = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbTipoDaBusca = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbCampos = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.pnlRodape.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlBarraBotoes.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel3);
            this.groupBox1.Controls.Add(this.pnlRodape);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 56);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(598, 350);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dataGridView1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 16);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(592, 295);
            this.panel3.TabIndex = 3;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(592, 295);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // pnlRodape
            // 
            this.pnlRodape.Controls.Add(this.panel2);
            this.pnlRodape.Controls.Add(this.panel1);
            this.pnlRodape.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlRodape.Location = new System.Drawing.Point(3, 311);
            this.pnlRodape.Name = "pnlRodape";
            this.pnlRodape.Size = new System.Drawing.Size(592, 36);
            this.pnlRodape.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnOk);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(435, 0);
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
            this.panel1.Location = new System.Drawing.Point(513, 0);
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
            // pnlBarraBotoes
            // 
            this.pnlBarraBotoes.Controls.Add(this.edtBusca);
            this.pnlBarraBotoes.Controls.Add(this.btnBusca);
            this.pnlBarraBotoes.Controls.Add(this.label3);
            this.pnlBarraBotoes.Controls.Add(this.label2);
            this.pnlBarraBotoes.Controls.Add(this.cbTipoDaBusca);
            this.pnlBarraBotoes.Controls.Add(this.label1);
            this.pnlBarraBotoes.Controls.Add(this.cbCampos);
            this.pnlBarraBotoes.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBarraBotoes.Location = new System.Drawing.Point(0, 0);
            this.pnlBarraBotoes.Name = "pnlBarraBotoes";
            this.pnlBarraBotoes.Size = new System.Drawing.Size(598, 56);
            this.pnlBarraBotoes.TabIndex = 3;
            // 
            // edtBusca
            // 
            this.edtBusca.Location = new System.Drawing.Point(251, 28);
            this.edtBusca.Name = "edtBusca";
            this.edtBusca.Size = new System.Drawing.Size(258, 20);
            this.edtBusca.TabIndex = 7;
            this.edtBusca.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.edtBusca_KeyPress);
            // 
            // btnBusca
            // 
            this.btnBusca.Location = new System.Drawing.Point(516, 26);
            this.btnBusca.Name = "btnBusca";
            this.btnBusca.Size = new System.Drawing.Size(75, 23);
            this.btnBusca.TabIndex = 6;
            this.btnBusca.Text = "Busca";
            this.btnBusca.UseVisualStyleBackColor = true;
            this.btnBusca.Click += new System.EventHandler(this.btnBusca_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(247, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Conteúdo da Busca";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(150, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Tipo de Busca";
            // 
            // cbTipoDaBusca
            // 
            this.cbTipoDaBusca.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTipoDaBusca.FormattingEnabled = true;
            this.cbTipoDaBusca.Items.AddRange(new object[] {
            "Igual =",
            "Diferente <>",
            "Contém",
            "Maior que >",
            "Menor que <",
            "Maior ou Igual >=",
            "Menor ou Igual <=",
            "Começar com"});
            this.cbTipoDaBusca.Location = new System.Drawing.Point(153, 28);
            this.cbTipoDaBusca.Name = "cbTipoDaBusca";
            this.cbTipoDaBusca.Size = new System.Drawing.Size(91, 21);
            this.cbTipoDaBusca.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Campos";
            // 
            // cbCampos
            // 
            this.cbCampos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCampos.FormattingEnabled = true;
            this.cbCampos.Location = new System.Drawing.Point(26, 28);
            this.cbCampos.Name = "cbCampos";
            this.cbCampos.Size = new System.Drawing.Size(121, 21);
            this.cbCampos.TabIndex = 0;
            this.cbCampos.DropDown += new System.EventHandler(this.cbCampos_DropDown);
            this.cbCampos.SelectedIndexChanged += new System.EventHandler(this.cbCampos_SelectedIndexChanged);
            // 
            // frmPesquisa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 406);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pnlBarraBotoes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPesquisa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pesquisa";
            this.Load += new System.EventHandler(this.frmPesquisa_Load);
            this.Shown += new System.EventHandler(this.frmPesquisa_Shown);
            this.groupBox1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.pnlRodape.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.pnlBarraBotoes.ResumeLayout(false);
            this.pnlBarraBotoes.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel pnlBarraBotoes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbTipoDaBusca;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbCampos;
        private System.Windows.Forms.Button btnBusca;
        private System.Windows.Forms.MaskedTextBox edtBusca;
        private System.Windows.Forms.Panel pnlRodape;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}