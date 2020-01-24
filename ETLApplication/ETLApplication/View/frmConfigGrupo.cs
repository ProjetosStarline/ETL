using ETLApplication.Controller;
using ETLApplication.Model;
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
    public partial class frmConfigGrupo : Form
    {
        GruposController gruposController;
        CategoriasController categoriasController;
        ParametrosController parametrosController;
        ParametrosModel parametrosModel;
        public frmConfigGrupo()
        {
            InitializeComponent();
            gruposController = new GruposController();
            categoriasController = new CategoriasController();
            parametrosController = new ParametrosController();
            parametrosModel = new ParametrosModel();
        }

        private void frmConfigGrupo_Load(object sender, EventArgs e)
        {
            string select = gruposController.GetSelectGrupos(Global.UsuarioLogado.Id_usuario);
            gruposController.CadGruposBase.conexao.GetDadosTodos(select, cbGrupos);
            //gruposController.CadGruposBase.GetLista(select, cbGrupos);
            cbGrupos.ValueMember = "CodEmpresa";
            cbGrupos.DisplayMember = "Empresa";
            //cbGrupos.SelectedIndex = 0;
        }

        private void cbGrupos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string select = categoriasController.GetFiliasDaEmpresa(Global.StrToInt(categoriasController.GetIdGrupo(cbGrupos.Text).ToString()));
            categoriasController.CadCategoriasBase.conexao.GetDadosTodos(select, cbCategorias);
            cbCategorias.ValueMember = "id_categoria";
            cbCategorias.DisplayMember = "nm_categoria";
            //cbCategorias.SelectedIndex = 0;

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (cbGrupos.Text.Length == 0)
            {
                MessageBox.Show("Favor informar a Empresa.");
                cbGrupos.Focus();
                return;
            }
            if (cbCategorias.Text.Length == 0)
            {
                MessageBox.Show("Favor informar a Filial.");
                cbCategorias.Focus();
                return;
            }

            if (parametrosController.GetIdParametro("Empresa_Selecionada") > 0)
            {
                parametrosController.GravaAlteracaoParametro("Empresa_Selecionada", categoriasController.GetIdGrupo(cbGrupos.Text).ToString());
            }
            else
            {
                parametrosController.GravaNovoParametro("Empresa_Selecionada", categoriasController.GetIdGrupo(cbGrupos.Text).ToString());
            }

            if (parametrosController.GetIdParametro("Filial_Selecionada") > 0)
            {
                parametrosController.GravaAlteracaoParametro("Filial_Selecionada", categoriasController.GetIdCategoria(cbCategorias.Text).ToString());
            }
            else
            {
                parametrosController.GravaNovoParametro("Filial_Selecionada", categoriasController.GetIdCategoria(cbCategorias.Text).ToString());
            }
            Close();
        }
    }
}
