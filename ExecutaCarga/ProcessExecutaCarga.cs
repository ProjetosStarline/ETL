/*
	==============================================
	Criado por : Carlos R. S. Oliveira
	Criado em  : 20/05/2019
	==============================================
*/
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ETLApplication.Controller;
using EtlConexao;
using Facade;

namespace ExecutaCarga
{
    public class ProcessExecutaCarga
    {
        EnviarEmailController dadosEnviarEmail = new EnviarEmailController();
        public ProcessArqController processArqController;
        public static string ConnectionsStringDST = "";


        public ProcessExecutaCarga()
        {
            processArqController = new ProcessArqController();
            Global.EnviarParaLog($"Processo do Arquivo criado com sucesso.", true, 0);
            //Global.conexaoORG = new Conexao(ConfigurationManager.ConnectionStrings["DbStarLineEtl"].ToString());
            //string ConnectionStringDst = GetConnectionStringDST();
            //Global.conexaoDST = new Conexao(ConnectionStringDst);

            //if (pIdServico != null)
            //{
            //    SetIdArquivo();
            //    SetNomeTabelaDST(ProcessArq.IdArquivo);

            //}



            ConnectionsStringDST = processArqController.GetDadosDaConexaoDST();

            //ConnectionsStringDST = GetConnectionStringDST();
            //Global.AutenticacaoPeloWindows = ConnectionsStringDST.IndexOf("Integrated Security=True") > 0;
            //Global.EnviarParaLog($"ConnectionsStringDST={ConnectionsStringDST.Length.ToString()}", true, 0);
            //if (ConnectionsStringDST.Length > 0)
            //{
            //    string connStr = ConnectionsStringDST.Remove(0, ConnectionsStringDST.IndexOf("=") + 1);
            //    Global.EnviarParaLog($"connStr={connStr.Length.ToString()}", true, 0);
            //    Global.DataSource = connStr.Substring(0, connStr.IndexOf(";"));
            //    Global.EnviarParaLog($"DataSource={Global.DataSource}", true, 0);
            //    connStr = connStr.Remove(0, connStr.IndexOf("=") + 1);
            //    Global.SchemaDST = connStr.Substring(0, connStr.IndexOf(";"));
            //    Global.EnviarParaLog($"SchemaDST={Global.SchemaDST}", true, 0);
            //    if (Global.AutenticacaoPeloWindows)
            //    {
            //        Global.EnviarParaLog($"AutenticacaoPeloWindows=true", true, 0);
            //        Global.UserID = "";
            //        Global.Password = "";
            //    }
            //    else
            //    {
            //        Global.EnviarParaLog($"AutenticacaoPeloWindows=false", true, 0);
            //        connStr = connStr.Remove(0, connStr.IndexOf("=") + 1);
            //        Global.UserID = connStr.Substring(0, connStr.IndexOf(";"));
            //        Global.EnviarParaLog($"UserID={Global.UserID}", true, 0);
            //        connStr = connStr.Remove(0, connStr.IndexOf("=") + 1);
            //        Global.Password = connStr.Trim();
            //        Global.EnviarParaLog($"Password={Global.Password}", true, 0);
            //    }
            //}
            //else
            //{
            //    Global.EnviarParaLog($"Erro ao obter a ConnectionsStringDST", true, 0);
            //}

        }
        //public string GetConnectionStringDST()
        //{
        //    string Result= processArqController.GetParametro("connection_string_base_destino");
        //    if (Result.Length == 0)
        //    {
        //        ParametrosController parametros = new ParametrosController();
        //        parametros.GravaNovoParametro("connection_string_base_destino", "Data Source=<Ip do servidor>;Initial Catalog=<Nome da base de Dados>;User ID=<Usuario>;Password=<Senha>[;Integrated Security=True]");
        //        Result = processArqController.GetParametro("connection_string_base_destino");
        //    }

        //    return Result;
        //}

        //public string GetConnectionStringDST(string pDataSource,string pInitCatalog,string pUserId,string pPassword,bool pIntegratedSecurity = true)
        //{
        //    string Result = "";
        //    if (pIntegratedSecurity)
        //    {
        //        Result = $"Data Source={pDataSource};Initial Catalog={pInitCatalog};Integrated Security=True";
        //    }
        //    else
        //    {
        //        Result = $"Data Source={pDataSource};Initial Catalog={pInitCatalog};User ID={pUserId};Password={pPassword}";
        //    }
        //    return Result;
        //}

        public int GetIntervaloProcessamento()
        {
            
            string Result = processArqController.GetParametro("pooling_servico");
            if (Result.Length == 0)
            {
                ParametrosController parametros = new ParametrosController();
                parametros.GravaNovoParametro("pooling_servico", "60");
                Result = processArqController.GetParametro("pooling_servico");
            }
            return Convert.ToInt32(Result == "" ? "0" : Result);
        }

        public string GetDir_Aplicacao()
        {

            string Result = processArqController.GetParametro("Dir_Aplicacao");
            if (Result.Length == 0)
            {
                ParametrosController parametros = new ParametrosController();
                parametros.GravaNovoParametro("Dir_Aplicacao", @"C:\");
                Result = processArqController.GetParametro("Dir_Aplicacao");
            }
            return Result == "" ? @"C:\" : Result;
        }

        public void OrquestraProcessamento(string pIdServico)
        {
            bool FoiProcessado = false;
            try
            {
                string DirAplicacao = GetDir_Aplicacao();
                Global.PathInputError = DirAplicacao+"ETL\\INPUT_ERROR";
                Global.PathFormatFile = DirAplicacao+"ETL\\BCPFORMAT";

                MonitoramentosModel monitoramentos = new MonitoramentosModel();
                List<MonitoramentosModel> lstMonitor = processArqController.GetMonitoramentos(pIdServico);
                //string NomeDoPacote = processArqController.GetNomePacote(1);

                if (lstMonitor.Count()>0 )
                {
                    processArqController.AtualizaSituacaoThread(pIdServico, "Processando");
                    foreach (var drMonitoramentos in lstMonitor)
                    {
                        ArquivosModel arquivosModal = new ArquivosModel();
                        int IdArquivo = drMonitoramentos.id_arquivo;
                        //Global.IdDoArquivo = ProcessArq.IdArquivo.ToString();
                        string NomeTabelaSTG = "STAGE_ETL_" + drMonitoramentos.id_arquivo.ToString();

                        List<ArquivosModel> drArq = processArqController.GetArquivos(drMonitoramentos.id_arquivo);
                        if (drArq.Count()>0)
                        {
                            
                            foreach (var drArquivos in drArq)
                            {
                                if (string.IsNullOrEmpty(drArquivos.ConexaoBusiness))
                                {
                                    //Se ConexaoBusiness estiver vazio, popula com o BI padrao
                                    string ds = ""; string ic = ""; string uid = ""; string p = ""; bool apw = false;
                                    processArqController.GetDadosdoBancoDST("", ref ds, ref ic, ref uid, ref p, ref apw);
                                    drArquivos.ConexaoBusiness = ic;
                                }

                                if (!processArqController.DataBaseCriada(drArquivos.ConexaoBusiness))
                                {
                                    Global.EnviarParaLog($"Preparando para criar o Banco de Dados {drArquivos.ConexaoBusiness}.", "OrquestraProcmto", true, IdArquivo);
                                    processArqController.CriarDataBaseDst(drArquivos.ConexaoBusiness, drArquivos.id_arquivo);
                                }

                                if (!processArqController.DataBaseCriada(drArquivos.ConexaoBusiness))
                                {
                                    Global.EnviarParaLog($"Banco de dados {drArquivos.ConexaoBusiness} não foi criado.", "OrquestraProcmto", true, IdArquivo);
                                    return;
                                }
                                else
                                {
                                    Global.EnviarParaLog($"Banco de Dados {drArquivos.ConexaoBusiness} criado.", "OrquestraProcmto", true, IdArquivo);
                                }

                                processArqController.CriarTabela_ArquivosCarregados(drArquivos.ConexaoBusiness, drArquivos.id_arquivo);

                                string NomeTabelaDST = GetNomeTabelaDST(drArquivos.tb_destino.ToString() , drArquivos.id_arquivo);// drArquivos.tb_destino.ToString()+"_"+ drArquivos.id_arquivo.ToString();
                                List<string> lstArquivos = new List<string>();
                                //DirectoryInfo dir = new DirectoryInfo($"{drArquivos.dir_entrada.ToString()}");
                                lstArquivos = ProcessArq.GetListaArquivos(drArquivos.dir_entrada.ToString(), drArquivos.mascara_arquivo, IdArquivo);//BuscaArquivosPelaMascara(drArquivos.dir_entrada.ToString() , drArquivos.mascara_arquivo, IdArquivo);//BuscaArquivos(dir);
                                foreach (var lArq in lstArquivos)
                                {
                                    if (drArquivos.tp_carga.ToUpper() == "FULL")
                                    {
                                        if (ProcessArq.TabelaCriada(drArquivos.ConexaoBusiness, NomeTabelaDST))
                                        {
                                            try
                                            {
                                                DropTabela(drArquivos.ConexaoBusiness,NomeTabelaDST, IdArquivo);
                                                Global.EnviarParaLog($"Tabela {NomeTabelaDST}) apagada com sucesso.",true,IdArquivo);
                                            }
                                            catch (Exception ex)
                                            {
                                                Global.EnviarParaLog($"Erro ao apagar a tabela: {NomeTabelaDST}. MOTIVO: {ex.Message}", true, IdArquivo);
                                            }

                                        }
                                    }

                                    string NomeArquivo = lArq;
                                    string Delimitador = drArquivos.delimitador.ToString();
                                    Global.EnviarParaLog($"ATENÇÃO: Arquivo que será executado: {NomeArquivo}.", "OrquestraProcmto", true, IdArquivo);
                                    //Altera o Banco para utilizar Opçoes bulk_logged
                                    ProcessArq.AlterDatabaseToBulk_logged(drArquivos.ConexaoBusiness);
                                    bool Result=false;
                                    //ProcessArq.LineFeed = drArquivos.LineFeed;
                                    //ProcessArq.FirstLine = drArquivos.FirstLine;
                                    Global.EnviarParaLog($"Dados do LineFeed({drArquivos.LineFeed}) utilizado.", "OrquestraProcmto", true, IdArquivo);
                                    switch (drArquivos.tp_arquivo.ToUpper())
                                    {
                                        case "DBF":
                                            //Processa arquivos DBF
                                            ProcessArq.TipoArquivo = Global.TipoArquivo.DBF;
                                            Result=ProcessArq.ProcessaEmLoteDBF(drArquivos.ConexaoBusiness,NomeTabelaDST,NomeArquivo,IdArquivo);
                                            FoiProcessado = true;
                                            break;
                                        case "EXCEL":
                                            //Processa arquivos EXCEL
                                            string NomePlanilha = drArquivos.nm_Planilha.ToString();
                                            ProcessArq.TipoArquivo = Global.TipoArquivo.EXCEL;
                                            Result = ProcessArq.ProcessaEmLoteXLSX(drArquivos.ConexaoBusiness, NomeTabelaDST,NomePlanilha,NomeArquivo,IdArquivo);
                                            FoiProcessado = true;
                                            break;
                                        case "DELIMITADO":
                                            //Processa arquivos DELIMITADO
                                            Result=ProcessarArqDelimitadoOuFixo(true,drArquivos.ConexaoBusiness,NomeTabelaSTG,NomeTabelaDST,Global.PathFormatFile,Delimitador,drArquivos.cercador,NomeArquivo,IdArquivo, Global.Behavior.DELIMITADO,drArquivos.FirstLine,drArquivos.LineFeed);
                                            FoiProcessado = true;
                                            break;
                                        case "FIXO":
                                            //Processa arquivos FIXO
                                            Result=ProcessarArqDelimitadoOuFixo(true, drArquivos.ConexaoBusiness,NomeTabelaSTG, NomeTabelaDST, Global.PathFormatFile, Delimitador, drArquivos.cercador, NomeArquivo, IdArquivo, Global.Behavior.FIXO, drArquivos.FirstLine,drArquivos.LineFeed);
                                            FoiProcessado = true;
                                            break;

                                    }
                                    Global.EnviarParaLog($"Processamento do Arquivo {NomeArquivo} Concluído", "OrquestraProcmto", true, IdArquivo);
                                    Global.EnviarParaLog($"Move Para Processados({Result.ToString()})", "OrquestraProcmto", true, IdArquivo);
                                    if (drArquivos.tp_arquivo != "EXCEL")
                                    {
                                        ProcessArq.MoveParaProcessados(NomeArquivo, IdArquivo, Result);
                                    }
                                    else
                                    {
                                        //Tratativa quando for Excel
                                        if((drArquivos.FlagPlanTemVariasAbas == "NAO")||(drArquivos.FlagPlanTemVariasAbas=="SIM" && drArquivos.FlagUltimaAbaPlanilha == "SIM"))
                                        {
                                            ProcessArq.MoveParaProcessados(NomeArquivo, IdArquivo, Result);
                                        }
                                    }

                                    if (Result)
                                    {
                                        processArqController.PopulaARQUIVOSCARREGADOS(drArquivos.ConexaoBusiness, drArquivos.tb_destino, drArquivos.dir_entrada, Path.GetFileName(NomeArquivo), IdArquivo);
                                        if (drArquivos.tp_arquivo.ToUpper()== "DELIMITADO" || drArquivos.tp_arquivo.ToUpper() == "FIXO")
                                        {
                                            try
                                            {
                                                processArqController.DropTabela(NomeTabelaSTG,processArqController.ConnectionsStringDST(drArquivos.ConexaoBusiness));
                                                Global.EnviarParaLog($"Tabela stage {NomeTabelaSTG}) apagada com sucesso.", "OrquestraProcmto", true, IdArquivo);
                                            }
                                            catch(Exception ex)
                                            {
                                                Global.EnviarParaLog($"Erro ao apagar a tabela stage: {NomeTabelaSTG}. MOTIVO: {ex.Message}", "OrquestraProcmto", true, IdArquivo);
                                            }
                                        }
                                        if (drArquivos.dir_saida.Length > 0)
                                        {
                                            SqlConnection _Conn = new SqlConnection(Global.ConnectionsString);
                                            _Conn.Open();
                                            try
                                            {
                                                processArqController.GeraSaidaDoArquivo(_Conn, NomeTabelaDST, drArquivos.dir_saida,drArquivos.ConexaoBusiness,IdArquivo);
                                            }
                                            finally
                                            {
                                                _Conn.Close();
                                            }
                                            
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    Global.EnviarParaLog($"Erro: Não foi encontrado Código do Arquivo para o Serviço {pIdServico}.", "OrquestraProcmto");
                }

            }
            catch (Exception ex)
            {
                Global.EnviarParaLog($"Erro: {ex.Message}.", "OrquestraProcmto");
            }
            finally
            {
                processArqController.AtualizaSituacaoThread(pIdServico, "Ocioso");
                if (FoiProcessado)
                {
                    EnviarEmail(Global.Strtoint(pIdServico));
                }
            }

        }


        //public void SetNomeTabelaDST(int pIdArquivo)
        //{
        //    string NomeTabelaDST = "";
        //    string PathInput= "";
        //    string PathOutput = "";
        //    ArquivosModel arquivosModal = new ArquivosModel();
        //    List<ArquivosModel> drArq = processArqController.GetArquivos(pIdArquivo);
        //    if (drArq.Count() > 0)
        //    {
        //        foreach (var drArquivos in drArq)
        //        {
        //            Global.NomeTabelaDST = drArquivos.tb_destino.ToString() + "_" + drArquivos.id_arquivo.ToString();
        //            Global.PathInput = drArquivos.dir_entrada.ToString();
        //            Global.PathOutput = drArquivos.dir_saida.ToString();
        //        }
        //    }
        //}

        //public void SetIdArquivo()
        //{
        //    ProcessArq.IdArquivo = 0;
        //    MonitoramentosModel monitoramentos = new MonitoramentosModel();
        //    List<MonitoramentosModel> lstMonitor = monitoramentos.ListMonitoramentos(processArqController.DrMonitoramentos());
        //    if (lstMonitor.Count() > 0)
        //    {
        //        foreach (var dr in lstMonitor)
        //        {
        //            ProcessArq.IdArquivo = dr.id_arquivo;
        //        }
        //    }
        //}
        private List<string> BuscaArquivos(DirectoryInfo dir)
        {
            List<string> lst = new List<string>();
            foreach (FileInfo file in dir.GetFiles())
            {
                lst.Add(file.FullName);
            }

            // busca arquivos do proximo sub-diretorio
            foreach (DirectoryInfo subDir in dir.GetDirectories())
            {
                BuscaArquivos(subDir);
            }

            return lst;
        }

        private List<string> BuscaArquivosPelaMascara(string dir,string Mascara,int pIdArquivo=0)
        {
            
            List<string> lst = new List<string>();
            string[] dirs = Directory.GetFiles(dir, Mascara);
            if (dir.Substring(dir.Length-1, 1) != "\\")
            {
                dir += "\\";
            }

            Global.EnviarParaLog($"Lista dos arquivos encontrados com a mascara: {dir + Mascara}", true, pIdArquivo);
            foreach (string FileName in dirs)
            {
                lst.Add(FileName);
                Global.EnviarParaLog($"{FileName}", true, pIdArquivo);
            }
            return lst;
        }

        private bool DropTabela(string pSchemaDST,string NomeTabela,int pIdArquivo=0)
        {
            try
            {
                processArqController.DropTabela(NomeTabela,processArqController.ConnectionsStringDST(pSchemaDST));
                return true;
            }
            catch(Exception ex)
            {
                Global.EnviarParaLog("ERRO: Não foi possível Truncar a tabela "+ NomeTabela + " ["+ex.Message+"]", true, pIdArquivo);
                return false;
            }
        }

        private bool ProcessarArqDelimitadoOuFixo(bool pEmLote, string pSchemaDST,string pNomeTabelaSTG, string pNomeTabelaDST, string pPathFormatFile, string pDelimitador,string pFieldQuote, string pNomeArquivo, int pIdArquivo, Global.Behavior pBehavior,int pFirstLine=1,string pLineFeed="\\r\\n")
        {
            bool Result = false;
            try
            {
//                Global.TipoArquivo TipoArquivo = Global.TipoArquivo.TXT;
                if (pEmLote)
                {
                    Result=ProcessArq.ProcessaEmLoteSTG(pSchemaDST, pNomeTabelaSTG,pNomeTabelaDST,pPathFormatFile,pDelimitador, pFieldQuote, pNomeArquivo,pIdArquivo,pBehavior, pFirstLine,pLineFeed);
                }
                else
                {
                    Result=ProcessArq.ProcessaTXT(pSchemaDST,pNomeTabelaDST, pNomeArquivo,pIdArquivo,pDelimitador,pBehavior);
                }
            }
            catch (Exception ex)
            {
                Result = false;
                Global.EnviarParaLog("ERRO: Não foi possível processar o Arquivo " + pNomeArquivo + " [" + ex.Message + "]", "ProcArqDELOuFixo", true, pIdArquivo);
            }
            return Result;
        }

        public string GetNomeTabelaDST(string pNomeTabela,int pIdArquivo)
        {
            string Result = pNomeTabela;
            string UsarId = processArqController.GetParametro("Concatena_ID_Tabela_Destino(SIM/NAO)");
            if (UsarId.Length == 0)
            {
                ParametrosController parametros = new ParametrosController();
                parametros.GravaNovoParametro("Concatena_ID_Tabela_Destino(SIM/NAO)", "NAO");
                UsarId = processArqController.GetParametro("Concatena_ID_Tabela_Destino(SIM/NAO)");
            }

            if (UsarId == "SIM")
            {
                Result += "_" + pIdArquivo.ToString();
            }

            return Result;
        }

        public void EnviarEmail(int pIdServico=0)
        {
            dadosEnviarEmail.EnviaEmail(pIdServico);
        }
    }
}
