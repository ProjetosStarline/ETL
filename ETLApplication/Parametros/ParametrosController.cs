using ETLApplication.DAO;
using ETLApplication.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parametros
{
    public class ParametrosController
    {
        Conexao conexao = null;
        public CadastroBase<ParametrosModel> CadParametrosBase;
        public ParametrosModel parametrosModel;
        public ParametrosController()
        {
            conexao = new Conexao();
            CadParametrosBase = new CadastroBase<ParametrosModel>();
            parametrosModel = new ParametrosModel();

        }
        public int GetIdParametro(string pNomeParametro)
        {
            int Result = 0;
            string script = $"Select id_parametro from tb_parametros where nm_parametro='{pNomeParametro}'";
            using (SqlDataReader dr = conexao.ExecutarSelect(script))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Result = Convert.ToInt32(CadParametrosBase.isNullResultZero(dr["id_parametro"].ToString()));
                    }
                }
            }
            return Result;
        }
        public string GetNmParametro(int pIdParametro)
        {
            string Result = "";
            string script = $"Select nm_parametro from tb_grupos where id_parametro='{pIdParametro.ToString()}'";
            using (SqlDataReader dr = conexao.ExecutarSelect(script))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Result = CadParametrosBase.isNullResultVazio(dr["nm_parametro"]);
                    }
                }
            }
            return Result;
        }
        public string GetValorParametro(string pNomeParametro)
        {
            string Result = "";
            string script = $"Select valor from tb_parametros where nm_parametro='{pNomeParametro}'";
            using (SqlDataReader dr = conexao.ExecutarSelect(script))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Result = CadParametrosBase.isNullResultZero(dr["valor"].ToString());
                    }
                }
            }
            return Result;
        }

        public void GravaNovoParametro(string pNomeParametro,string pValor)
        {
            string msg="";
            parametrosModel.nm_parametro = pNomeParametro;
            parametrosModel.valor = pValor;
            parametrosModel.status = "ativo";
            parametrosModel.data_atualizacao = DateTime.Now;
            try
            {
                msg=CadParametrosBase.PersisteNoBanco(parametrosModel, 1/*oInsert*/);
            }
            catch(Exception ex)
            {
                msg += $"ERRO: Não foi possível gravar o parametro {pNomeParametro}. Motivo: {ex.Message}";
            }
        }

        public void GravaAlteracaoParametro(string pNomeParametro, string pValor)
        {
            string msg = "";
            parametrosModel.id_parametro = GetIdParametro(pNomeParametro);
            parametrosModel.nm_parametro = pNomeParametro;
            parametrosModel.valor = pValor;
            parametrosModel.status = "ativo";
            parametrosModel.data_atualizacao = DateTime.Now;
            try
            {
                msg = CadParametrosBase.PersisteNoBanco(parametrosModel, 2/*oEdit*/);
            }
            catch (Exception ex)
            {
                msg += $"ERRO: Não foi possível alterar o parametro {pNomeParametro}. Motivo: {ex.Message}";
            }
        }
    }
}
