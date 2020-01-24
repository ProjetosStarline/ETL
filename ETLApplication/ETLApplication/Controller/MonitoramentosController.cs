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

namespace ETLApplication.Controller
{
    public class MonitoramentosController
    {
        public Conexao conexao = null;
        public CadastroBase<MonitoramentosModel> CadMonitoramentosBase;
        public MonitoramentosController()
        {
            conexao = new Conexao();
            CadMonitoramentosBase = new CadastroBase<MonitoramentosModel>();
        }


        public string GetNmServico(int pIdArquivo)
        {
            string Result = "";
            string script = $"select s.nm_servico from tb_monitoramentos m,tb_servicos s where m.id_servico=s.id_servico and  m.id_arquivo ={pIdArquivo.ToString()} and s.status='ativo'";
            using (SqlDataReader dr = conexao.ExecutarSelect(script))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Result = dr["nm_servico"].ToString();
                    }
                }
            }
            return Result;
        }

        public int GetIdServico(int pIdArquivo)
        {
            int Result = 0;
            string script = $"select s.id_servico from tb_monitoramentos m,tb_servicos s where m.id_servico=s.id_servico and  m.id_arquivo ={pIdArquivo.ToString()} and s.status='ativo'";
            using (SqlDataReader dr = conexao.ExecutarSelect(script))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Result = Global.StrToInt(dr["id_servico"].ToString());
                    }
                }
            }
            return Result;
        }
        public int GetIdArquivo(int pIdServico)
        {
            int Result = 0;
            string script = $"select m.id_arquivo from tb_monitoramentos m,tb_servicos s where m.id_servico=s.id_servico and  m.id_servico ={pIdServico.ToString()} and s.status='ativo'";
            using (SqlDataReader dr = conexao.ExecutarSelect(script))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Result = Global.StrToInt(dr["id_arquivo"].ToString());
                    }
                }
            }
            return Result;
        }
        public int GetIdArquivo(int pIdServico,int pIdGrupo,int pIdCateg)
        {
            int Result = 0;
            string script = $"select distinct mo.id_arquivo ";
            script += $"from tb_arquivos a, tb_monitoramentos mo, tb_Grupos g, tb_Categorias c, tb_pacotes p ";
            script += $"where a.id_arquivo = mo.id_arquivo ";
            script += $"and a.id_pacote = p.id_pacote ";
            script += $"and c.id_grupo = g.id_grupo ";
            script += $"and c.id_categoria = p.id_categoria ";
            script += $"and c.id_categoria = {pIdCateg.ToString()} ";
            script += $"and c.id_grupo = {pIdGrupo.ToString()} ";
            script += $"and mo.id_servico = {pIdServico.ToString()}";
            using (SqlDataReader dr = conexao.ExecutarSelect(script))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Result = Global.StrToInt(dr["id_arquivo"].ToString());
                    }
                }
            }
            return Result;
        }

        public bool MonitoramentoCadastrado(int pIdServico,int pIdArquivo)
        {
            bool Result = false;
            string script = $"select id_servico from tb_monitoramentos where id_servico={pIdServico.ToString()} and id_arquivo ={pIdArquivo.ToString()}";
            using (SqlDataReader dr = conexao.ExecutarSelect(script))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Result = Global.StrToInt(dr["id_servico"].ToString())>0;
                    }
                }
            }
            return Result;
        }

    }
}
