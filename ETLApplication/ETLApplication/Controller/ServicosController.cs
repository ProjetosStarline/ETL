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
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ETLApplication.Controller
{
    class ServicosController
    {
        Conexao conexao = null;
        ParametrosController Parametros;
        public CadastroBase<ServicosModel> CadServicosBase;

        public ServicosController()
        {
            conexao = new Conexao();
            CadServicosBase = new CadastroBase<ServicosModel>();
            Parametros = new ParametrosController();
        }

        public int GetIdServico(string pNome)
        {
            int Result = 0;
            string script = $"Select id_servico from tb_servicos where nm_servico='{pNome}'";
            using (SqlDataReader dr = conexao.ExecutarSelect(script))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Result = Convert.ToInt32(CadServicosBase.isNullResultZero(dr["id_servico"].ToString()));
                    }
                }
            }
            return Result;
        }

        public string GetNmServico(int pIdServico)
        {
            string Result = "";
            string script = $"Select nm_Servico from tb_Servicos where id_Servico='{pIdServico.ToString()}' and status='ativo'";
            using (SqlDataReader dr = conexao.ExecutarSelect(script))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Result = CadServicosBase.isNullResultVazio(dr["nm_Servico"]);
                    }
                }
            }
            return Result;
        }

        public void PreencheCombo(ComboBox pComboBox)
        {
            string select = CadServicosBase.GetSelect(new ServicosModel()) + $" and status='ativo'";
            CadServicosBase.GetLista(select, pComboBox);
            pComboBox.ValueMember = "id_Servico";
            pComboBox.DisplayMember = "nm_Servico";
            if (pComboBox.Items.Count > 0)
            {
                pComboBox.SelectedIndex = 0;
            }
        }
        public string GetCondicaoPesquisa(string pIdGrupo, string pIdCateg, string pIdPacote = "")
        {
            string Result = "";
            Result += " and s.id_servico in(";
            Result += " select distinct mo.id_servico";
            Result += " from tb_arquivos a,";
            Result += " tb_monitoramentos mo, ";
            Result += " tb_Grupos g,";
            Result += " tb_Categorias c,";
            Result += " tb_pacotes p";
            Result += " where a.id_arquivo = mo.id_arquivo";
            Result += " and a.id_pacote = p.id_pacote";
            Result += " and c.id_grupo = g.id_grupo";
            Result += " and c.id_categoria = p.id_categoria";
            if (pIdPacote.Length > 0)
            {
                Result += $" and p.id_pacote = {pIdPacote}";
            }
            if (pIdGrupo.Length > 0)
            {
                Result += $" and c.id_grupo = {pIdGrupo}";
            }
            if (pIdCateg.Length > 0)
            {
                Result += $" and c.id_categoria = {pIdCateg}";
            }
            Result += ") and s.status = 'ativo'";
            return Result;
        }
        public bool ExistemFilhos(int pIdServico)
        {
            bool Result = false;
            string script = $"select Count(mo.id_servico) qtd from tb_arquivos a,tb_monitoramentos mo where a.id_arquivo = mo.id_arquivo and mo.id_servico ={pIdServico.ToString()}";
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
            string script = "select s.id_servico 'ID', s.nm_servico 'Serviço',s.Situacao 'Situação',";
            script += " s.Status, s.data_criacao 'Data Criação',s.data_atualizacao 'Data Atualização'";
            script += " from tb_servicos s ";
            script += " where 1 = 1";
            return script;

        }
    }
}
