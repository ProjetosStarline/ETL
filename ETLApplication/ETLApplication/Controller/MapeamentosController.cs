/*
	==============================================
	Criado por : Carlos R. S. Oliveira
	Criado em  : 20/05/2019
	==============================================
*/
using ETLApplication.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETLApplication.Controller
{
    public class MapeamentosController
    {
        public CadastroBase<MapeamentosModel> CadMaptoBase;
        public MapeamentosController()
        {
            CadMaptoBase = new CadastroBase<MapeamentosModel>();
        }

        public void DeletarMapeamentos(int pIdArquivo)
        {
            string script = $"Delete from tb_mapeamentos where id_arquivo={pIdArquivo}";
            CadMaptoBase.conexao.ExecutarScript(script);
        }
        public string GetSelectMapeamentos(int pIdArquivo)
        {
            string script = "Select mp.*";
            script += " from tb_mapeamentos mp, tb_arquivos a, tb_monitoramentos mon, tb_servicos s";
            script += " where mon.id_arquivo = a.id_arquivo";
            script += "   and mp.id_arquivo = a.id_arquivo";
            script += "   and mon.id_servico = s.id_servico";
            script += "   and mon.status = s.status";
            script += "   and a.status=mon.status";
            script += "   and mon.status = 'ativo'";
            script += $"   and mon.id_arquivo = {pIdArquivo.ToString()}";

            return script;
        }

        public int GetIdMapeamento(int pIdArquivo, string pnmCampo)
        {
            int Result = 0;
            string script = $"select id_mapeamento from tb_mapeamentos where id_arquivo ={pIdArquivo.ToString()} and nm_coluna='{pnmCampo}'";
            using (SqlDataReader dr = CadMaptoBase.conexao.ExecutarSelect(script))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Result = Global.StrToInt(dr["id_mapeamento"].ToString());
                    }
                }
            }
            return Result;
        }

        public string GetSelect()
        {
            string script = "Select ";
            script += " mp.id_mapeamento 'ID',";
            script += " mp.id_arquivo 'ID Arquivo',";
            script += " mp.nm_coluna 'Nome Coluna',";
            script += " mp.ordem 'Ordem',";
            script += " mp.fixo_inicio 'Fixo Início',";
            script += " mp.fixo_tamanho 'Fixo Tamanho',";
            script += " mp.tp_coluna 'Tipo Coluna',";
            script += " mp.tm_coluna 'Tamanho Coluna',";
            script += " mp.pr_coluna 'Qtd. Dig. Decimal',";
            script += " mp.mask_campo 'Máscara do Campo',";
            script += " mp.ExpressaoSql 'Expressão Sql',";
            script += " mp.data_criacao 'Data Criação',";
            script += " mp.data_atualizacao 'Data Atualização'";
            script += " from tb_grupos g,";
            script += " tb_categorias c, ";
            script += " tb_pacotes p,";
            script += " tb_servicos s, ";
            script += " tb_monitoramentos mo,";
            script += " tb_arquivos a, ";
            script += " tb_mapeamentos mp,";
            script += " tb_UsuariosXcategorias uc";
            script += " where c.id_grupo = g.id_grupo";
            script += "   and p.id_categoria = c.id_categoria";
            script += "   and s.id_servico = mo.id_servico";
            script += "   and a.id_arquivo = mo.id_arquivo";
            script += "   and a.id_arquivo = mp.id_arquivo";
            script += "   and p.id_pacote = a.id_pacote";
            script += "   and uc.id_categoria = c.id_categoria";
            script += $"  and uc.id_usuario = {Global.UsuarioLogado.Id_usuario.ToString()}";

            return script;
        }


    }
}
