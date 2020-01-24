using ETLApplication.Controller;
using ETLApplication.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ETLApplication.View
{
    public partial class frmPermissoes : ETLApplication.View.frmCadastroBase
    {
        public bool Fechado = true;
        public int idObjeto = 0;
        CadastroBase<PermissoesModel> cadastroBase;
        PermissoesController PermControl = new PermissoesController();
        ObjetosController ObjControl = new ObjetosController();
        public frmPermissoes()
        {
            InitializeComponent();
            cadastroBase = new CadastroBase<PermissoesModel>();
            HabilitaCampos();
        }
        public void HabilitaCampos()
        {
            edtIdPermissoes.Enabled = !cadastroBase.FieldIsPK(permissoesModel, "Id_permissao");
        }

        public override void CriaClasseModel()
        {
            new frmPesquisa(permissoesModel).ShowDialog();
            MostrarNatela();
        }

        private void MostrarNatela()
        {
            cbPesquisar.Checked = false;
            cbCadastrar.Checked = false;
            cbAlterar.Checked = false;
            cbDeletar.Checked = false;
            cbVisualizar.Checked = false;


            edtIdPermissoes.Text = permissoesModel.Id_permissao.ToString();
            List<PermissoesModel> lstPerm = PermControl.GetListPermCompleta(permissoesModel.Id_permissao);
            //lblMenu.Text = "";
            //cbxPerfil.Text = "";
            cbStatus.SelectedIndex = 1;
            edtDataCriacao.Text = Global.DateTimeToStr(Global.DateTimeNow);
            edtDataAtualizacao.Text = Global.DateTimeToStr(Global.DateTimeNow);
            foreach (var lst in lstPerm)
            {
                cbxPerfil.Text = cadastroBase.GetNomePerfil(lst.Id_perfil);
                cbStatus.SelectedIndex = lst.Status == "ativo" ? 0 : 1;
                edtDataCriacao.Text = lst.Data_criacao.ToString();
                edtDataAtualizacao.Text = lst.Data_atualizacao.ToString();
                lblMenu.Text = lst.nm_objeto;
                //                dgvPermissoes.CurrentCell = dgvPermissoes.Rows[lst.Id_permissao].Cells[0];
                //posiciona o cursor na linha do Objeto(Menu)
                int row= GetIndexObjeto(lst.Id_objeto.ToString());
                dgvPermissoes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvPermissoes.CurrentCell=dgvPermissoes.Rows[row].Cells["ID"];

                //MostrarPermissoes();
                PermControl.MostraPermissoesDoBanco(lst.Id_perfil, lst.Id_objeto, cbPesquisar, cbCadastrar, cbAlterar, cbDeletar, cbVisualizar);
            }



        }

        public int GetIndexObjeto(string idObjeto)
        {
            int Result = 0;
            for(int i = 0;i<=dgvPermissoes.Rows.Count-1 ;i++)
            {
                var cntd = dgvPermissoes.Rows[i].Cells["ID"].Value;
                if (cntd.ToString() == idObjeto)
                {
                    Result = i;
                    break;
                }
            }
            return Result;
        }

        public override void LimparTela()
        {
            edtIdPermissoes.Clear();
            cbPesquisar.Checked = false;
            cbCadastrar.Checked = false;
            cbAlterar.Checked = false;
            cbDeletar.Checked = false;
            cbVisualizar.Checked = false;
            permissoesModel.Id_perfil = cadastroBase.GetidPerfil(cbxPerfil.Text);
            edtDataCriacao.Text = Global.DateTimeToStr(Global.DateTimeNow);
            edtDataAtualizacao.Text = Global.DateTimeToStr(Global.DateTimeNow);
            lblMenu.Text = "";
            base.LimparTela();
        }


        public string GetPermissoesDaTela(CheckBox pPesquisar, CheckBox pCadastrar, CheckBox pAlterar, CheckBox pDeletar, CheckBox pVisualizar)
        {
            string sPermissoes = "";
            sPermissoes += pPesquisar.Checked ? "1|" : "0|";
            sPermissoes += pCadastrar.Checked ? "1|" : "0|";
            sPermissoes += pAlterar.Checked ? "1|" : "0|";
            sPermissoes += pDeletar.Checked ? "1|" : "0|";
            sPermissoes += pVisualizar.Checked ? "1|" : "0|";
            return sPermissoes;
        }

        private void frmPermissoes_Load(object sender, EventArgs e)
        {
            CadastroBase<PerfisModel> cbPerfis = new CadastroBase<PerfisModel>();
            string select = cbPerfis.GetSelect(new PerfisModel());
            cbPerfis.GetLista(select, cbxPerfil);
            cbxPerfil.ValueMember = "id_Perfil";
            cbxPerfil.DisplayMember = "nm_Perfil";
            if (cbxPerfil.Items.Count > 0) { cbxPerfil.SelectedIndex = 0; }

            if (cbStatus.Items.Count > 0) { cbStatus.SelectedIndex = 0; }
            //dgvPermissoes.DataSource = objetosModel.GetListObjetos(ObjControl.GetObjetosMenu());
            LoadMenu(objetosModel.GetListObjetos(ObjControl.GetObjetosMenu()));



        }

        public void LoadMenu(List<ObjetosModel> pObjModel)
        {
            dgvPermissoes.Rows.Clear();
            for(int i = 0; i < pObjModel.Count; i++)
            {
                dgvPermissoes.Rows.Add();
                dgvPermissoes.Rows[i].Cells["ID"].Value = pObjModel[i].Id_objeto;
                dgvPermissoes.Rows[i].Cells["Menu"].Value = pObjModel[i].Nm_objeto;
            }
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            if (cbxPerfil.Items.Count > 0) { cbxPerfil.SelectedIndex = 0; }
            if (cbStatus.Items.Count > 0) { cbStatus.SelectedIndex = 0; }
        }
        public override void ControlaBotoes()
        {
            base.ControlaBotoes();
            if (edtIdPermissoes != null)
            {
                btnAlterar.Enabled = operacao != Operacao.oEdit && edtIdPermissoes.Text.Length   != 0 && edtIdPermissoes.Text != "0" && (Global.Login.Alterar || Global.UsuarioSuper);
                btnExcluir.Enabled = operacao != Operacao.oDelete && edtIdPermissoes.Text.Length != 0 && edtIdPermissoes.Text != "0" && (Global.Login.Deletar || Global.UsuarioSuper);
            }
            if (btnPermitirAcessoTodosMenus != null)
            {
                btnPermitirAcessoTodosMenus.Visible = operacao == Operacao.oInsert;
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            operacao = Operacao.oNenhum;
            idObjeto = permissoesModel.Id_objeto;
            MostrarNatela();
            ControlaBotoes();
        }
        
        private void dgvPermissoes_Click(object sender, EventArgs e)
        {
            //MostrarPermissoes();
            //LimparTela();
            permissoesModel.Id_perfil = cadastroBase.GetidPerfil(cbxPerfil.Text);
            SetIdObjeto();
            permissoesModel.Id_permissao= PermControl.GetIdPermissao(permissoesModel.Id_perfil, idObjeto);
            MostrarNatela();
//            PermControl.MostraPermissoesDoBanco(permissoesModel.Id_perfil, idObjeto, cbPesquisar, cbCadastrar, cbAlterar, cbDeletar, cbVisualizar);
        }

        //private void MostrarPermissoes()
        //{
        //    cbPesquisar.Checked = false;
        //    cbCadastrar.Checked = false;
        //    cbAlterar.Checked = false;
        //    cbDeletar.Checked = false;
        //    cbVisualizar.Checked = false;
        //    lblMenu.Text = "";
        //    if (dgvPermissoes.CurrentRow != null)
        //    {
        //        string Id_objeto = dgvPermissoes.CurrentRow.Cells[0].Value.ToString();
        //        int Id_perfil = cadastroBase.GetidPerfil(cbxPerfil.Text); ;
        //        if (GetConteudoCampo(Id_objeto).Length > 0)
        //        {
        //            lblMenu.Text = dgvPermissoes.CurrentRow.Cells[1].Value.ToString();
        //            idObjeto = Convert.ToInt32(Id_objeto);
        //            PermControl.SetPermissoes(cbPesquisar , Id_perfil, idObjeto);
        //            PermControl.SetPermissoes(cbCadastrar , Id_perfil, idObjeto);
        //            PermControl.SetPermissoes(cbAlterar    , Id_perfil, idObjeto);
        //            PermControl.SetPermissoes(cbDeletar   , Id_perfil, idObjeto);
        //            PermControl.SetPermissoes(cbVisualizar, Id_perfil, idObjeto);
        //        }
        //    }
        //}

        public void SetIdObjeto()
        {
            lblMenu.Text = "";
            idObjeto = 0;
            if (dgvPermissoes.CurrentRow != null)
            { 
                string Id_objeto = dgvPermissoes.CurrentRow.Cells["ID"].Value.ToString();
                idObjeto = Convert.ToInt32(Id_objeto);
                lblMenu.Text = dgvPermissoes.CurrentRow.Cells["Menu"].Value.ToString();

            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (lblMenu.Text == "")
            {
                MessageBox.Show("Favor selecionar o Menu que receberá as permissões.");
                lblMenu.Focus();
                return;
            }
            //if (dgvPermissoes.SelectedCells[0].Value.ToString() == "")
            //{
            //    MessageBox.Show("Favor selecionar o Menu que receberá as permissões.");
            //    lblMenu.Focus();
            //    return;
            //}
            if (cbxPerfil.Text.Length == 0)
            {
                MessageBox.Show("Favor informar o Perfil.");
                cbxPerfil.Focus();
                return;
            }

            bool PermCad = false;
            permissoesModel.Id_perfil = cadastroBase.GetidPerfil(cbxPerfil.Text);
            permissoesModel.Id_objeto = idObjeto;
            if (operacao == Operacao.oInsert)
            {
                PermCad = PermControl.GetIdPermissao(permissoesModel.Id_perfil, permissoesModel.Id_objeto) > 0;
                if (PermCad)
                {
                    MessageBox.Show("Menu já cadastrado, favor selecionar outro menu para dar permissões.");
                    cbxPerfil.Focus();
                    return;
                }
            }

            permissoesModel.Status = cbStatus.Text;
            permissoesModel.Permissao = GetPermissoesDaTela(cbPesquisar, cbCadastrar, cbAlterar, cbDeletar, cbVisualizar);
            string MsgRetorno = cadastroBase.PersisteNoBanco(permissoesModel, OperacaoToInt(operacao));
            if (!Global.IsNullOrEmpty(MsgRetorno))
            {
                MessageBox.Show(MsgRetorno);
            }
            //LimparTela();
            ControlaBotoes();

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void dgvPermissoes_SelectionChanged(object sender, EventArgs e)
        {
        }

        private void dgvPermissoes_KeyPress(object sender, KeyPressEventArgs e)
        {
            

        }

        private void cbPesquisar_CheckedChanged(object sender, EventArgs e)
        {
            HabilitaChkPermitirTodos();
        }

        private void cbPermitirTodos_Click(object sender, EventArgs e)
        {
            Fechado = true;
            cbPesquisar.Checked = cbPermitirTodos.Checked;
            cbCadastrar.Checked = cbPermitirTodos.Checked;
            cbAlterar.Checked = cbPermitirTodos.Checked;
            cbDeletar.Checked = cbPermitirTodos.Checked;
            cbVisualizar.Checked = cbPermitirTodos.Checked;
            Fechado = false;
            HabilitaChkPermitirTodos();
        }

        private void btnPermitirAcessoTodosMenus_Click(object sender, EventArgs e)
        {
            try
            {
                int idPerfil = cadastroBase.GetidPerfil(cbxPerfil.Text);
                
                for (int i = 0; i < dgvPermissoes.Rows.Count; i++)
                {
                    int idobj = Global.StrToInt(dgvPermissoes.Rows[i].Cells["ID"].Value.ToString());
                    bool PermCad = PermControl.GetIdPermissao(idPerfil, idobj) > 0;
                    if (!PermCad)
                    {
                        permissoesModel.Id_objeto = idobj;
                        permissoesModel.Id_perfil = cadastroBase.GetidPerfil(cbxPerfil.Text);
                        permissoesModel.Status = "ativo";
                        permissoesModel.Permissao = "1|1|1|1|1|";
                        cadastroBase.PersisteNoBanco(permissoesModel, 1);
                    }

                }
                MessageBox.Show("Incluão de permissões para todos os Menus concluída.");
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Erro ao incluir as permissões para o menus. Motivo: {ex.Message}");
            }

        }

        private void cbPesquisar_Click(object sender, EventArgs e)
        {
            HabilitaChkPermitirTodos();
        }

        void HabilitaChkPermitirTodos()
        {
            if (!Fechado)
            {
                cbPermitirTodos.Checked = cbPesquisar.Checked && cbCadastrar.Checked && cbAlterar.Checked && cbDeletar.Checked && cbVisualizar.Checked;
            }
        }

    }
}
