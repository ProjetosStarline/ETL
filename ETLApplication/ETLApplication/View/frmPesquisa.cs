using ETLApplication.Controller;
using ETLApplication.Model;
using ETLApplication.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ETLApplication
{
    public partial class frmPesquisa : Form
    {
        CadastroBase<GenericModel> cadastroBase;
        PerfisController perfisController;
        UsuariosController usuariosController;
        PermissoesController permissoesController;
        GruposController gruposController;
        CategoriasController categoriasController;
        PacotesController pacotesController;
        ServicosController servicosController;
        ArquivosController arquivosController;
        MapeamentosController mapeamentosController;
        ParametrosController parametrosController;

        public GenericModel ClasseUtilisada;
        private List<string> ListaTipoDosCampos;
        public frmPesquisa(GenericModel Classe)
        {
            InitializeComponent();
            cadastroBase = new CadastroBase<GenericModel>();
            ClasseUtilisada = Classe;
            switch (ClasseUtilisada.NomeTabela)
            {
                case "tb_perfis":
                    perfisController = new PerfisController();
                    break;
                case "tb_usuarios":
                    usuariosController = new UsuariosController();
                    break;
                case "tb_permissoes":
                    permissoesController = new PermissoesController();
                    break;
                case "tb_grupos":
                    gruposController = new GruposController();
                    break;
                case "tb_categorias":
                    categoriasController = new CategoriasController();
                    break;
                case "tb_pacotes":
                    pacotesController = new PacotesController();
                    break;
                case "tb_servicos":
                    servicosController = new ServicosController();
                    break;
                case "tb_arquivos":
                    arquivosController = new ArquivosController();
                    break;
                case "tb_mapeamentos":
                    mapeamentosController = new MapeamentosController();
                    break;
                case "tb_parametros":
                    parametrosController = new ParametrosController();
                    break;
            }
        }

        private void frmPesquisa_Load(object sender, EventArgs e)
        {
            
        }

        private void frmPesquisa_Shown(object sender, EventArgs e)
        {
            switch (ClasseUtilisada.NomeTabela)
            {
                case "tb_permissoes":Text = "Pesquisa Permissões"; break;
                case "tb_perfis": Text = "Pesquisa Perfis"; break;
                case "tb_usuarios": Text = "Pesquisa Usuários"; break;
                case "tb_grupos": Text = "Pesquisa Empresas"; break;
                case "tb_categorias": Text = "Pesquisa Filiais"; break;
                case "tb_parametros": Text = "Pesquisa Parâmetros"; break;
                case "tb_servicos": Text = "Pesquisa Serviços"; break;
                case "tb_pacotes": Text = "Pesquisa Pacotes"; break;
                case "tb_arquivos": Text = "Pesquisa Arquivos"; break;
            }

            List<string> ListaCampos = cadastroBase.GetListaCampos(ClasseUtilisada);
            ListaTipoDosCampos = cadastroBase.GetListaTipoDosCampos();
            //if (ClasseUtilisada.NomeTabela == "tb_permissoes" ||
            //    ClasseUtilisada.NomeTabela == "tb_perfis" ||
            //    ClasseUtilisada.NomeTabela == "tb_usuarios" ||
            //    ClasseUtilisada.NomeTabela == "tb_grupos" ||
            //    ClasseUtilisada.NomeTabela == "tb_categorias" ||
            //    ClasseUtilisada.NomeTabela == "tb_parametros" ||
            //    ClasseUtilisada.NomeTabela == "tb_servicos" ||
            //    ClasseUtilisada.NomeTabela == "tb_pacotes" ||
            //    ClasseUtilisada.NomeTabela == "tb_arquivos" ||
            //    ClasseUtilisada.NomeTabela == "tb_mapeamentos")
            //{
            //    ClasseUtilisada.GetListaCampos();
            //    cbCampos.DataSource = ClasseUtilisada.ListaCamposView;
            //}
            //else
            //{
            //    cbCampos.DataSource = ListaCampos;
            //}

            ClasseUtilisada.GetListaCampos();
            cbCampos.DataSource = ClasseUtilisada.ListaCamposView;
            if (cbTipoDaBusca.Items.Count > 0) { cbTipoDaBusca.SelectedIndex = 0; }
        }

        private void cbCampos_DropDown(object sender, EventArgs e)
        {
            
        }

        private void cbCampos_SelectedIndexChanged(object sender, EventArgs e)
        {
            edtBusca.Clear();
        }

        private void btnBusca_Click(object sender, EventArgs e)
        {
            string cbCamposText = cbCampos.Text;
            string edtBuscaText = edtBusca.Text;
            if (cbCampos.Text.Length>0)
            {
                if (cbTipoDaBusca.Text.Length > 0)
                {
                    //string Operacao = "";
                    //string Condicao = edtBusca.Text;
                    string TipoDoCampo = ClasseUtilisada.GetTypeFieldParaBusca(cbCampos.Text);

                    //if (TipoDoCampo == "String" || TipoDoCampo == "Datetime") { Condicao = "'" + edtBusca.Text + "'"; }
                    //else
                    //if (TipoDoCampo == "Integer" || TipoDoCampo == "bool") { Condicao = edtBusca.Text; }

                    //if (cbTipoDaBusca.Text.IndexOf("=") > 0) { Operacao = "=";}
                    //else
                    //if (cbTipoDaBusca.Text.IndexOf("<>") > 0) { Operacao = "<>"; }
                    //else
                    //if (cbTipoDaBusca.Text.IndexOf("<") > 0) { Operacao = "<"; }
                    //else
                    //if (cbTipoDaBusca.Text.IndexOf(">") > 0) { Operacao = ">"; }
                    //else
                    //if (cbTipoDaBusca.Text.IndexOf("<=") > 0) { Operacao = "<="; }
                    //else
                    //if (cbTipoDaBusca.Text.IndexOf(">=") > 0) { Operacao = ">="; }
                    //else { Operacao = "Like";Condicao = "'%" + cbCampos.Text + "%'"; }
                    cadastroBase.ScriptDefault = "";
                    edtBuscaText = edtBusca.Text;
                    cbCamposText = ClasseUtilisada.GetFieldParaBusca(cbCampos.Text);
                    switch (ClasseUtilisada.NomeTabela)
                    {
                        case "tb_permissoes":
                            cadastroBase.ScriptDefault = permissoesController.GetSelect();
                            if (cbCampos.Text=="Pesquisar"|| cbCampos.Text=="Cadastrar"|| cbCampos.Text=="Alterar"|| cbCampos.Text=="Deletar"|| cbCampos.Text == "Visualizar")
                            {
                                switch (edtBusca.Text.ToUpper())
                                {
                                    case "SIM":
                                        edtBuscaText = "1";
                                        break;
                                    case "NÃO":
                                        edtBuscaText = "0";
                                        break;
                                    case "NAO":
                                        edtBuscaText = "0";
                                        break;
                                }
                            }
                            break;
                        case "tb_perfis":
                            cadastroBase.ScriptDefault = perfisController.GetSelect();
                            break;
                        case "tb_usuarios":
                            cadastroBase.ScriptDefault = usuariosController.GetSelect();
                            break;
                        case "tb_grupos":
                            cadastroBase.ScriptDefault = gruposController.GetSelect();
                            break;
                        case "tb_categorias":
                            cadastroBase.ScriptDefault = categoriasController.GetSelect();
                            break;
                        case "tb_parametros":
                            cadastroBase.ScriptDefault = parametrosController.GetSelect();
                            break;
                        case "tb_servicos":
                            cadastroBase.ScriptDefault = servicosController.GetSelect();
                            break;
                        case "tb_pacotes":
                            cadastroBase.ScriptDefault = pacotesController.GetSelect();
                            break;
                        case "tb_arquivos":
                            cadastroBase.ScriptDefault = arquivosController.GetSelect();
                            break;
                        case "tb_mapeamentos":
                            cadastroBase.ScriptDefault = mapeamentosController.GetSelect();
                            break;
                    }

                    if (TipoDoCampo == "DateTime")
                    {
                        try
                        {
                            DateTime dt = Convert.ToDateTime(edtBusca.Text);
                            edtBusca.Text = dt.ToString();
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show("Data inválida, favor digitar novamente.");
                            edtBusca.Focus();
                            return;
                        }
                    }

                    cadastroBase.Pesquisar(ClasseUtilisada.NomeTabela, cbCamposText, TipoDoCampo,cbTipoDaBusca.Text, edtBuscaText, dataGridView1,Global.Condicao);
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            ClasseUtilisada.SetDados(dataGridView1);
            Close();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ClasseUtilisada.SetDados(dataGridView1);
            Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ClasseUtilisada.Clear();
            Close();
        }

        public void DigitoOK(string pConteudo, KeyPressEventArgs e)
        {
            if (pConteudo.Contains(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
                //MessageBox.Show($"Caracter {e.KeyChar} inválido, favor digitar novamente");
                edtBusca.Focus();
            }
        }

        private void edtBusca_KeyPress(object sender, KeyPressEventArgs e)
        {
            //string SomenteLetras = " AÁÂÃBCÇDEÉÊFGHIÍJKLMNOÓÔÕPQRSTUÚVWXYZaáâãbcçdeéêfghiíjklmnoóôõpqrstuúvwxyz0123456789.,:/\b";
            string SomenteNumeros= "0123456789\b";
            string SomenteDatas = " 0123456789:/\b";
            string SomenteValor = "0123456789.,\b";

            string TipoDoCampo = ClasseUtilisada.GetTypeFieldParaBusca(cbCampos.Text);  //ListaTipoDosCampos[cbCampos.SelectedIndex];
            //if (TipoDoCampo == "String"){DigitoOK(SomenteLetras, e);}
            //else
            if (TipoDoCampo == "DateTime") { DigitoOK(SomenteDatas, e); }
            else
            if (TipoDoCampo == "integer") { DigitoOK(SomenteNumeros, e); }
            else
            if (TipoDoCampo == "float") { DigitoOK(SomenteValor, e); }
            else
            {
                e.Handled = false;
            }


        }
    }

 }
