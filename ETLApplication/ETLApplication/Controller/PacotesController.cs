/*
	==============================================
	Criado por : Carlos R. S. Oliveira
	Criado em  : 20/05/2019
	==============================================
*/
using EtlConexao;
using ETLApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ETLApplication.Controller
{
    class PacotesController
    {
        Conexao conexao = null;
        ParametrosController Parametros;
        public CategoriasController Categoria;
        public CadastroBase<PacotesModel> CadPacotesBase;
        public PacotesController()
        {
            conexao = new Conexao();
            CadPacotesBase = new CadastroBase<PacotesModel>();
            Parametros = new ParametrosController();
            Categoria = new CategoriasController();
        }

        public void PreencheComboPacotes(ComboBox pCbPacotes)
        {
            string select = CadPacotesBase.GetSelect(new PacotesModel()) + $" and id_categoria={Parametros.GetValorParametro("Filial_Selecionada").ToString()}";
            CadPacotesBase.GetLista(select, pCbPacotes);
            pCbPacotes.ValueMember = "id_pacote";
            pCbPacotes.DisplayMember = "nm_pacote";
            if (pCbPacotes.Items.Count > 0)
            {
                pCbPacotes.SelectedIndex = 0;
            }
        }
        public void LoadComboPacotes(ComboBox pCbPacotes)
        {
            pCbPacotes.Items.Add("Todos");
            SqlDataReader dr = GetPacotes();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    pCbPacotes.Items.Add(dr["nm_pacote"]);
                }
            }
            pCbPacotes.ValueMember = "id_pacote";
            pCbPacotes.DisplayMember = "nm_pacote";
            if (pCbPacotes.Items.Count > 0)
            {
                pCbPacotes.SelectedIndex = 0;
            }
        }

        public SqlDataReader GetPacotes()
        {
            string select = CadPacotesBase.GetSelect(new PacotesModel()) + $" and id_categoria={Parametros.GetValorParametro("Filial_Selecionada").ToString()}";
            SqlDataReader dr = conexao.ExecutarSelect(select);
            return dr;
        }

        public bool ExistemFilhos(int pIdPacote)
        {
            bool Result = false;
            string script = $"select count(*) qtd from tb_arquivos where id_pacote ={pIdPacote.ToString()}";
            using (SqlDataReader dr = conexao.ExecutarSelect(script))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Result = Global.StrToInt(dr["qtd"].ToString()) >= 1;
                    }
                }
            }
            return Result;
        }

        public string GetSelect()
        {
            string script = "select c.id_categoria 'ID Filial', c.nm_categoria 'Filial',p.id_pacote 'ID', p.nm_pacote 'Pacote',p.Descr_pacotes 'Descrição',";
            script += " p.Status, p.data_criacao 'Data Criação',p.data_atualizacao 'Data Atualização'";
            script += " from tb_grupos g,";
            script += " tb_categorias c, ";
            script += " tb_pacotes p,";
            script += " tb_UsuariosXcategorias uc";
            script += " where c.id_grupo = g.id_grupo";
            script += "   and p.id_categoria = c.id_categoria";
            script += "   and uc.id_categoria = c.id_categoria";
            script += $"  and uc.id_usuario = {Global.UsuarioLogado.Id_usuario.ToString()}";
            return script;

        }
        public int GetIdPacote(string pNmPacote)
        {
            int Result = 0;
            string script = $"select id_Pacote from tb_Pacotes where nm_Pacote ='{pNmPacote}'";
            using (SqlDataReader dr = conexao.ExecutarSelect(script))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Result = Global.StrToInt(dr["id_Pacote"].ToString());
                    }
                }
            }
            return Result;

        }
    }
}
