/*
	==============================================
	Criado por : Carlos R. S. Oliveira
	Criado em  : 20/05/2019
	==============================================
*/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ETLApplication.Model
{
    public class MapeamentosModel:GenericModel
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_mapeamento { get; set; }
        public int id_arquivo { get; set; }
        public string nm_coluna { get; set; }
        public int ordem { get; set; }
        public int fixo_inicio { get; set; }
        public int fixo_tamanho { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_indice { get; set; }
        public string tp_coluna { get; set; }
        public int tm_coluna { get; set; }
        public int pr_coluna { get; set; }
        public string MASK_CAMPO { get; set; }
        public string ExpressaoSql { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public DateTime data_criacao { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public DateTime data_atualizacao { get; set; }

        public MapeamentosModel()
        {
            NomeTabela = "tb_mapeamentos";
        }
        public void SetDados(SqlDataReader dr)
        {
            try
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        id_mapeamento = Convert.ToInt32(dr["id_mapeamento"].ToString());
                        id_arquivo = Convert.ToInt32(dr["id_arquivo"].ToString());
                        nm_coluna = dr["nm_coluna"].ToString();
                        ordem = Convert.ToInt32(isNullResultZero(dr["ordem"].ToString()));
                        fixo_inicio = Convert.ToInt32(isNullResultZero(dr["fixo_inicio"].ToString()));
                        fixo_tamanho = Convert.ToInt32(isNullResultZero(dr["fixo_tamanho"].ToString()));
                        tp_coluna = dr["tp_coluna"].ToString();
                        tm_coluna = Convert.ToInt32(isNullResultZero(dr["tm_coluna"].ToString()));
                        pr_coluna = Convert.ToInt32(isNullResultZero(dr["pr_coluna"].ToString()));
                        MASK_CAMPO = dr["MASK_CAMPO"].ToString();
                        ExpressaoSql = dr["ExpressaoSql"].ToString();
                        data_criacao = Convert.ToDateTime(dr["data_criacao"].ToString());
                        data_atualizacao = Convert.ToDateTime(dr["data_atualizacao"].ToString());
                    }
                }
            }
            finally
            {
                dr.Close();
            }

        }
        public void SetDados(MapeamentosModel mm)
        {
            try
            {
                id_mapeamento       = mm.id_mapeamento;
                id_arquivo          = mm.id_arquivo      ;
                nm_coluna           = mm.nm_coluna       ;
                ordem               = mm.ordem           ;
                fixo_inicio         = mm.fixo_inicio     ;
                fixo_tamanho        = mm.fixo_tamanho    ;
                tp_coluna           = mm.tp_coluna       ;
                tm_coluna           = mm.tm_coluna       ;
                pr_coluna           = mm.pr_coluna       ;
                MASK_CAMPO          = mm.MASK_CAMPO      ;
                ExpressaoSql        = mm.ExpressaoSql    ;
                data_criacao        = mm.data_criacao    ;
                data_atualizacao    = mm.data_atualizacao;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public List<MapeamentosModel> GetListaMapeamentos(SqlDataReader dr)
        {
            List<MapeamentosModel> mapeamentos = new List<MapeamentosModel>();
            try
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        mapeamentos.Add
                        (
                            new MapeamentosModel()
                            {
                                id_mapeamento = Convert.ToInt32(dr["id_mapeamento"].ToString()),
                                id_arquivo = Convert.ToInt32(dr["id_arquivo"].ToString()),
                                nm_coluna = dr["nm_coluna"].ToString(),
                                ordem = Convert.ToInt32(isNullResultZero(dr["ordem"].ToString())),
                                fixo_inicio = Convert.ToInt32(isNullResultZero(dr["fixo_inicio"].ToString())),
                                fixo_tamanho = Convert.ToInt32(isNullResultZero(dr["fixo_tamanho"].ToString())),
                                tp_coluna = dr["tp_coluna"].ToString(),
                                tm_coluna = Convert.ToInt32(isNullResultZero(dr["tm_coluna"].ToString())),
                                pr_coluna = Convert.ToInt32(isNullResultZero(dr["pr_coluna"].ToString())),
                                MASK_CAMPO = dr["MASK_CAMPO"].ToString(),
                                ExpressaoSql = dr["ExpressaoSql"].ToString(),
                                data_criacao = Convert.ToDateTime(dr["data_criacao"].ToString()),
                                data_atualizacao = Convert.ToDateTime(dr["data_atualizacao"].ToString())
                            }
                        );
                    }
                }
            }
            finally
            {
                dr.Close();
            }
            return mapeamentos;
        }
        public override void SetDados(DataGridView dgv)
        {
            if (dgv.CurrentRow != null)
            {
                if (dgv.CurrentRow.Cells["ID"].Value.ToString() != "")
                {
                    id_mapeamento = Convert.ToInt32(isNullResultZero(dgv.CurrentRow.Cells["ID"].Value.ToString()));
                    id_arquivo = Convert.ToInt32(isNullResultZero(dgv.CurrentRow.Cells["ID Arquivo"].Value.ToString()));
                    nm_coluna = dgv.CurrentRow.Cells["Nome Coluna"].Value.ToString();
                    ordem = Convert.ToInt32(isNullResultZero(dgv.CurrentRow.Cells["Ordem"].Value.ToString()));
                    fixo_inicio = Convert.ToInt32(isNullResultZero(dgv.CurrentRow.Cells["Fixo Início"].Value.ToString()));
                    fixo_tamanho = Convert.ToInt32(isNullResultZero(dgv.CurrentRow.Cells["Fixo Tamanho"].Value.ToString()));
                    //id_indice = Convert.ToInt32(isNullResultZero(dgv.CurrentRow.Cells["id_indice"].Value.ToString()));
                    tp_coluna = dgv.CurrentRow.Cells["Tipo Coluna"].Value.ToString();
                    tm_coluna = Convert.ToInt32(isNullResultZero(dgv.CurrentRow.Cells["Tamanho Coluna"].Value.ToString()));
                    pr_coluna = Convert.ToInt32(isNullResultZero(dgv.CurrentRow.Cells["Qtd. Dig. Decimal"].Value.ToString()));
                    MASK_CAMPO = dgv.CurrentRow.Cells["Máscara do Campo"].Value.ToString();
                    ExpressaoSql = dgv.CurrentRow.Cells["Expressão Sql"].Value.ToString();
                    data_criacao = Convert.ToDateTime(dgv.CurrentRow.Cells["Data Criação"].Value.ToString());
                    data_atualizacao = Convert.ToDateTime(dgv.CurrentRow.Cells["Data Atualização"].Value.ToString());
                }
            }
        }
        public override void Clear()
        {
            id_mapeamento = 0;
            id_arquivo = 0;
            nm_coluna = "";
            ordem = 0;
            fixo_inicio = 0;
            fixo_tamanho = 0;
            id_indice = 0;
            tp_coluna = "";
            tm_coluna = 0;
            pr_coluna = 0;
            MASK_CAMPO = "";
            ExpressaoSql = "";
            data_criacao = DateTime.Now;
            data_atualizacao = DateTime.Now;

        }
        public override void GetListaCampos()
        {
            base.GetListaCampos();
            ListaCamposView.Clear();
            ListaCamposView.Add("ID");
            ListaCamposView.Add("ID Arquivo");
            ListaCamposView.Add("Nome Coluna");
            ListaCamposView.Add("Ordem");
            ListaCamposView.Add("Fixo Início");
            ListaCamposView.Add("Fixo Tamanho");
            ListaCamposView.Add("Tipo Coluna");
            ListaCamposView.Add("Tamanho Coluna");
            ListaCamposView.Add("Qtd. Dig. Decimal");
            ListaCamposView.Add("Máscara do Campo");
            ListaCamposView.Add("Expressão Sql");
            ListaCamposView.Add("Data Criação");
            ListaCamposView.Add("Data Atualização");
        }

        public override string GetFieldParaBusca(string pCampoView)
        {
            string Result = "";
            switch (pCampoView)
            {
                case "ID": Result = "mp.id_mapeamento"; break;
                case "ID Arquivo": Result = "mp.id_arquivo"; break;
                case "Nome Coluna": Result = "mp.nm_coluna"; break;
                case "Ordem": Result = "mp.ordem"; break;
                case "Fixo Início": Result = "mp.fixo_inicio"; break;
                case "Fixo Tamanho": Result = "mp.fixo_tamanho"; break;
                case "Tipo Coluna": Result = "mp.tp_coluna"; break;
                case "Tamanho Coluna": Result = "mp.tm_coluna"; break;
                case "Qtd. Dig. Decimal": Result = "mp.pr_coluna"; break;
                case "Máscara do Campo": Result = "mp.mask_campo"; break;
                case "Expressão Sql": Result = "mp.ExpressaoSql"; break;
                case "Data Criação": Result = "mp.data_criacao"; break;
                case "Data Atualização": Result = "mp.data_atualizacao"; break;
            }
            return Result;
        }

        public override string GetTypeFieldParaBusca(string pCampoView)
        {
            string Result = "";
            switch (pCampoView)
            {
                case "ID": Result = "integer"; break;
                case "ID Arquivo": Result = "integer"; break;
                case "Nome Coluna": Result = "String"; break;
                case "Ordem": Result = "integer"; break;
                case "Fixo Início": Result = "integer"; break;
                case "Fixo Tamanho": Result = "integer"; break;
                case "Tipo Coluna": Result = "integer"; break;
                case "Tamanho Coluna": Result = "String"; break;
                case "Qtd. Dig. Decimal": Result = "integer"; break;
                case "Máscara do Campo": Result = "String"; break;
                case "Expressão Sql": Result = "String"; break;
                case "Data Criação": Result = "DateTime"; break;
                case "Data Atualização": Result = "DateTime"; break;
            }
            return Result;
        }
    }

}

