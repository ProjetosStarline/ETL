using ETLApplication.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ETLApplication.View
{
    public partial class frmVisualizarLog : Form
    {
        public frmVisualizarLog()
        {
            InitializeComponent();
        }

        private void frmVisualizarLog_Load(object sender, EventArgs e)
        {
            VisualizaAndamentoController vac = new VisualizaAndamentoController();
            vac.MostrarLogArqNaGrid(Global.MostrarLogIDArquivo, dgvMostrarLog);
        }
    }
}
