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
    public class ArquivosModel:GenericModel
    {
        public ArquivosModel()
        {
            NomeTabela = "tb_arquivos";
        }
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_arquivo { get; set; }
        public int id_pacote { get; set; }
        public string nm_arquivo { get; set; }
        public string mascara_arquivo { get; set; }
        public string tp_carga { get; set; }
        public string tp_arquivo { get; set; }
        public string delimitador { get; set; }
        public string cercador { get; set; }
        public string tb_destino { get; set; }
        public string dir_entrada { get; set; }
        public string dir_saida { get; set; }
        public string rbd_tabela { get; set; }
        public string rbd_indice { get; set; }
        public string nm_Planilha { get; set; }
        public string status { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public DateTime data_criacao { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public DateTime data_atualizacao { get; set; }
        public string LineFeed { get; set; }
        public int FirstLine { get; set; }
        public string ConexaoBusiness{ get; set; }

        public string FlagPlanTemVariasAbas { get; set; }
        public string FlagUltimaAbaPlanilha { get; set; }

        public void SetDados(SqlDataReader dr)
        {
            try
            {
                id_arquivo          = Convert.ToInt32(dr["id_arquivo"].ToString());
                id_pacote           = Convert.ToInt32(dr["id_pacote"].ToString());
                nm_arquivo          = dr["nm_arquivo"].ToString();
                mascara_arquivo     = dr["mascara_arquivo"].ToString();
                tp_carga            = dr["tp_carga"].ToString();
                tp_arquivo          = dr["tp_arquivo"].ToString();
                delimitador         = dr["delimitador"].ToString();
                cercador            = dr["cercador"].ToString();
                tb_destino          = dr["tb_destino"].ToString();
                dir_entrada         = dr["dir_entrada"].ToString();
                dir_saida           = dr["dir_saida"].ToString();
                rbd_tabela          = dr["rbd_tabela"].ToString();
                rbd_indice          = dr["rbd_indice"].ToString();
                nm_Planilha         = dr["nm_Planilha"].ToString();
                status              = dr["status"].ToString();
                data_criacao        = Convert.ToDateTime(dr["data_criacao"].ToString());
                data_atualizacao    = Convert.ToDateTime(dr["data_atualizacao"].ToString());
                LineFeed            = dr["LineFeed"].ToString();
                FirstLine           = Global.StrToInt(dr["FirstLine"].ToString());
                ConexaoBusiness     = dr["ConexaoBusiness"].ToString();
                FlagPlanTemVariasAbas = dr["FlagPlanTemVariasAbas"].ToString();
                FlagUltimaAbaPlanilha = dr["FlagUltimaAbaPlanilha"].ToString();

            }
            finally
            {
                dr.Close();
            }

        }
        public List<ArquivosModel> GetListaArquivos(SqlDataReader dr)
        {
            List<ArquivosModel> arquivos = new List<ArquivosModel>();
            try
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        arquivos.Add
                        (
                            new ArquivosModel()
                            {
                                id_arquivo = Convert.ToInt32(dr["id_arquivo"].ToString()),
                                id_pacote = Convert.ToInt32(dr["id_pacote"].ToString()),
                                nm_arquivo = dr["nm_arquivo"].ToString(),
                                mascara_arquivo = dr["mascara_arquivo"].ToString(),
                                tp_carga = dr["tp_carga"].ToString(),
                                tp_arquivo = dr["tp_arquivo"].ToString(),
                                delimitador = dr["delimitador"].ToString(),
                                cercador = dr["cercador"].ToString(),
                                tb_destino = dr["tb_destino"].ToString(),
                                dir_entrada = dr["dir_entrada"].ToString(),
                                dir_saida = dr["dir_saida"].ToString(),
                                rbd_tabela = dr["rbd_tabela"].ToString(),
                                rbd_indice = dr["rbd_indice"].ToString(),
                                nm_Planilha = dr["nm_Planilha"].ToString(),
                                status = dr["status"].ToString(),
                                data_criacao = Convert.ToDateTime(dr["data_criacao"].ToString()),
                                data_atualizacao = Convert.ToDateTime(dr["data_atualizacao"].ToString()),
                                LineFeed = dr["LineFeed"].ToString(),
                                FirstLine = Global.StrToInt(dr["FirstLine"].ToString()),
                                ConexaoBusiness=dr["ConexaoBusiness"].ToString(),
                                FlagPlanTemVariasAbas = dr["FlagPlanTemVariasAbas"].ToString(),
                                FlagUltimaAbaPlanilha = dr["FlagUltimaAbaPlanilha"].ToString()
                            }
                        );
                    }
                }
            }
            finally
            {
                dr.Close();
            }
            return arquivos;
        }

        public void SetDados(ArquivosModel am)
        {
            try
            {
                id_arquivo = am.id_arquivo;
                id_pacote = am.id_pacote;
                nm_arquivo = am.nm_arquivo;
                mascara_arquivo = am.mascara_arquivo;
                tp_carga = am.tp_carga;
                tp_arquivo = am.tp_arquivo;
                delimitador = am.delimitador;
                cercador = am.cercador;
                tb_destino = am.tb_destino;
                dir_entrada = am.dir_entrada;
                dir_saida = am.dir_saida;
                rbd_tabela = am.rbd_tabela;
                rbd_indice = am.rbd_indice;
                nm_Planilha = am.nm_Planilha;
                status = am.status;
                data_criacao = am.data_criacao;
                data_atualizacao = am.data_atualizacao;
                LineFeed = am.LineFeed;
                FirstLine = am.FirstLine;
                ConexaoBusiness = am.ConexaoBusiness;
                FlagPlanTemVariasAbas = am.FlagPlanTemVariasAbas;
                FlagUltimaAbaPlanilha = am.FlagUltimaAbaPlanilha;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public override void SetDados(DataGridView dgv)
        {
            if (dgv.CurrentRow != null)
            {
                if (dgv.CurrentRow.Cells["ID"].Value.ToString() != "")
                {
                    id_arquivo = Convert.ToInt32(         dgv.CurrentRow.Cells["ID"].Value.ToString());
                    id_pacote =  Convert.ToInt32(         dgv.CurrentRow.Cells["ID Pacote"].Value.ToString());
                    nm_arquivo =                          dgv.CurrentRow.Cells["Arquivo"].Value.ToString();
                    mascara_arquivo =                     dgv.CurrentRow.Cells["Máscara"].Value.ToString();
                    tp_carga =                            dgv.CurrentRow.Cells["Tipo Carga"].Value.ToString();
                    tp_arquivo =                          dgv.CurrentRow.Cells["Tipo Arquivo"].Value.ToString();
                    delimitador =                         dgv.CurrentRow.Cells["Delimitador"].Value.ToString();
                    cercador =                            dgv.CurrentRow.Cells["Cercador"].Value.ToString();
                    tb_destino =                          dgv.CurrentRow.Cells["Tabela Destino"].Value.ToString();
                    dir_entrada =                         dgv.CurrentRow.Cells["Dir. Entrada"].Value.ToString();
                    dir_saida =                           dgv.CurrentRow.Cells["Dir. Saída"].Value.ToString();
                    //rbd_tabela =                          dgv.CurrentRow.Cells["rbd_tabela"].Value.ToString();
                    //rbd_indice =                          dgv.CurrentRow.Cells["rbd_indice"].Value.ToString();
                    status =                              dgv.CurrentRow.Cells["status"].Value.ToString();
                    data_criacao = Convert.ToDateTime(    dgv.CurrentRow.Cells["Data Criação"].Value.ToString());
                    data_atualizacao = Convert.ToDateTime(dgv.CurrentRow.Cells["Data Atualização"].Value.ToString());
                    nm_Planilha =                         dgv.CurrentRow.Cells["Planilha"].Value.ToString();
                    LineFeed=                             dgv.CurrentRow.Cells["LineFeed"].Value.ToString();
                    FirstLine =                           Global.StrToInt(dgv.CurrentRow.Cells["FirstLine"].Value.ToString());
                    ConexaoBusiness =                     dgv.CurrentRow.Cells["ConexaoBusiness"].Value.ToString();
                    FlagPlanTemVariasAbas =               dgv.CurrentRow.Cells["Flag Planilha Tem Várias Abas"].Value.ToString();
                    FlagUltimaAbaPlanilha =               dgv.CurrentRow.Cells["Flag última Aba da Planilha"].Value.ToString();

                }

            }
        }

        public override void Clear()
        {
            id_arquivo      = 0;
            id_pacote       = 0;
            nm_arquivo      = "";
            mascara_arquivo ="";
            tp_carga        = "";
            tp_arquivo      = "";
            delimitador     = "";
            cercador        = "";
            tb_destino  = "";
            dir_entrada = "";
            dir_saida = "";
            rbd_tabela = "";
            rbd_indice = "";
            nm_Planilha = "";
            status = "";
            data_criacao = DateTime.Now;
            data_atualizacao = DateTime.Now;
            LineFeed = "";
            FirstLine = 0;
            ConexaoBusiness = "";
            FlagPlanTemVariasAbas = "";
            FlagUltimaAbaPlanilha = "";
        }

        public override void GetListaCampos()
        {
            base.GetListaCampos();
            ListaCamposView.Clear();
            ListaCamposView.Add("ID");
            ListaCamposView.Add("ID Pacote");
            ListaCamposView.Add("Pacote");
            ListaCamposView.Add("ID Serviço");
            ListaCamposView.Add("Serviço");
            ListaCamposView.Add("Arquivo");
            ListaCamposView.Add("Máscara");
            ListaCamposView.Add("Tipo Carga");
            ListaCamposView.Add("Tipo Arquivo");
            ListaCamposView.Add("Delimitador");
            //ListaCamposView.Add("Cercador");
            ListaCamposView.Add("Tabela Destino");
            ListaCamposView.Add("Dir. Entrada");
            ListaCamposView.Add("Dir. Saída");
            //ListaCamposView.Add("rbd_tabela");
            //ListaCamposView.Add("rbd_indice");
            ListaCamposView.Add("Planilha");
            ListaCamposView.Add("Status");
            ListaCamposView.Add("Data Criação");
            ListaCamposView.Add("Data Atualização");
            ListaCamposView.Add("LineFeed");
            ListaCamposView.Add("FirstLine");
            ListaCamposView.Add("ConexaoBusiness");
            ListaCamposView.Add("Flag Planilha Tem Várias Abas");
            ListaCamposView.Add("Flag última Aba da Planilha");


        }

        public override string GetFieldParaBusca(string pCampoView)
        {
            string Result = "";
            switch (pCampoView)
            {
                case "ID": Result = "a.id_arquivo"; break;
                case "ID Pacote": Result = "p.id_pacote"; break;
                case "Pacote": Result = "p.nm_pacote"; break;
                case "ID Serviço": Result = "s.id_servico"; break;
                case "Serviço": Result = "s.nm_servico"; break;
                case "Arquivo": Result = "a.nm_arquivo"; break;
                case "Máscara": Result = "a.mascara_arquivo"; break;
                case "Tipo Carga": Result = "a.tp_carga"; break;
                case "Tipo Arquivo": Result = "a.tp_arquivo"; break;
                case "Delimitador": Result = "a.delimitador"; break;
                case "Tabela Destino": Result = "a.tb_destino"; break;
                case "Dir. Entrada": Result = "a.dir_entrada"; break;
                case "Dir. Saída": Result = "a.dir_saida"; break;
                case "Planilha": Result = "a.nm_Planilha"; break;
                case "Status": Result = "a.Status"; break;
                case "Data Criação": Result = "a.data_criacao"; break;
                case "Data Atualização": Result = "a.data_atualizacao"; break;
                case "LineFeed": Result = "a.LineFeed"; break;
                case "FirstLine": Result = "a.FirstLine"; break;
                case "ConexaoBusiness": Result = "a.ConexaoBusiness"; break;
                case "Flag Planilha Tem Várias Abas": Result = "a.FlagPlanTemVariasAbas"; break;
                case "Flag. última Aba da Planilha": Result = "a.FlagUltimaAbaPlanilha"; break;
            }
            return Result;
        }

        public override string GetTypeFieldParaBusca(string pCampoView)
        {
            string Result = "";
            switch (pCampoView)
            {
                case "ID": Result = "integer"; break;
                case "ID Pacote": Result = "integer"; break;
                case "Pacote": Result = "String"; break;
                case "ID Serviço": Result = "String"; break;
                case "Serviço": Result = "String"; break;
                case "Arquivo": Result = "String"; break;
                case "Máscara": Result = "String"; break;
                case "Tipo Carga": Result = "String"; break;
                case "Tipo Arquivo": Result = "String"; break;
                case "Delimitador": Result = "String"; break;
                case "Tabela Destino": Result = "String"; break;
                case "Dir. Entrada": Result = "String"; break;
                case "Dir. Saída": Result = "String"; break;
                case "Planilha": Result = "String"; break;
                case "Status": Result = "String"; break;
                case "Data Criação": Result = "DateTime"; break;
                case "Data Atualização": Result = "DateTime"; break;
                case "LineFeed": Result = "String"; break;
                case "FirstLine": Result = "integer"; break;
                case "ConexaoBusiness": Result = "String"; break;
                case "Flag Planilha Tem Várias Abas": Result = "String"; break;
                case "Flag. última Aba da Planilha": Result = "String"; break;

            }
            return Result;
        }

    }
}
