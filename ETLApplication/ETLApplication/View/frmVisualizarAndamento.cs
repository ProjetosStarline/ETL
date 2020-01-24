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
    public partial class frmVisualizarAndamento : Form
    {
        int IDArquivo = 0;
        int IDPacote = 0;
        ArquivosController arquivos;
        public frmVisualizarAndamento()
        {
            InitializeComponent();
            arquivos = new ArquivosController();
        }

        private void frmVisualizarAndamento_Load(object sender, EventArgs e)
        {
            PacotesController pacotes = new PacotesController();
            pacotes.LoadComboPacotes(cbPacote);
            if (cbPacote.Items.Count > 0)
            {
                cbPacote.SelectedIndex = 0;
            }

            LoadArquivos(Convert.ToInt32(cbPacote.SelectedValue));
            HabilitarBotao();
            timer1.Enabled = true;

        }

        public void LoadArquivos(int pIdPacote)
        {
            arquivos.SetListaArquivos(cbArquivo, pIdPacote);
            HabilitarBotao();
        }

        private void cbPacote_SelectedValueChanged(object sender, EventArgs e)
        {
            IDPacote = new PacotesController().GetIdPacote(cbPacote.Text);
            if (IDPacote>0)
            {
                IDPacote = Convert.ToInt32(IDPacote);
                LoadArquivos(IDPacote);

            }
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            VisualizaAndamentoController vac = new VisualizaAndamentoController();
            if (cbArquivo.Text== "Todos")
            {
                vac.MostrarNaGrid(IDPacote,0, dtpDataInicial.Value, dtpDataFinal.Value, dataGridView1);
            }
            else
            {
                vac.MostrarNaGrid(IDPacote, IDArquivo, dtpDataInicial.Value, dtpDataFinal.Value, dataGridView1);
            }
            HabilitarBotao();
        }

        public void HabilitarBotao()
        {
            btnAtualizar.Enabled = cbArquivo.Text.Length > 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //btnAtualizar_Click(sender, e);
        }

        private void cbArquivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            IDArquivo = 0;
            if (cbArquivo.Text != "Todos")
            {
                IDArquivo = arquivos.GetIdArquivo(cbArquivo.Text);
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            Global.MostrarLogIDArquivo = Global.StrToInt(dataGridView1.CurrentRow.Cells["ID"].Value.ToString());
            new frmVisualizarLog().ShowDialog();
        }
    }
}
