namespace ETLService
{
    partial class ExecutaCarga
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.eventLog1 = new System.Diagnostics.EventLog();
            this.tmrExecutaCarga = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).BeginInit();
            // 
            // tmrExecutaCarga
            // 
            this.tmrExecutaCarga.Tick += new System.EventHandler(this.tmrExecutaCarga_Tick);
            // 
            // ExecutaCarga
            // 
            this.ServiceName = "ExecutaCarga";
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).EndInit();

        }

        #endregion
        internal System.Diagnostics.EventLog eventLog1;
        private System.Windows.Forms.Timer tmrExecutaCarga;
    }
}
