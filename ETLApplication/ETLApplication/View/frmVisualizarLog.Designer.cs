namespace ETLApplication.View
{
    partial class frmVisualizarLog
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
            this.dgvMostrarLog = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMostrarLog)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvMostrarLog
            // 
            this.dgvMostrarLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMostrarLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMostrarLog.Location = new System.Drawing.Point(0, 0);
            this.dgvMostrarLog.Name = "dgvMostrarLog";
            this.dgvMostrarLog.Size = new System.Drawing.Size(1231, 450);
            this.dgvMostrarLog.TabIndex = 0;
            // 
            // frmVisualizarLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1231, 450);
            this.Controls.Add(this.dgvMostrarLog);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmVisualizarLog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Visualizar Log";
            this.Load += new System.EventHandler(this.frmVisualizarLog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMostrarLog)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvMostrarLog;
    }
}