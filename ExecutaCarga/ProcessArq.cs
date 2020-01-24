/*
	==============================================
	Criado por : Carlos R. S. Oliveira
	Criado em  : 20/05/2019
	==============================================
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using EtlConexao;

namespace ExecutaCarga
{
    public static class ProcessArq
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern uint GetShortPathName(
            [MarshalAs(UnmanagedType.LPTStr)]
            string lpszLongPath,
            [MarshalAs(UnmanagedType.LPTStr)]
            StringBuilder lpszShortPath,
            uint cchBuffer);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern uint GetShortPathName(string lpszLongPath, char[] lpszShortPath, int cchBuffer);

        // To ensure that paths are not limited to MAX_PATH, use this signature within .NET
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "GetShortPathNameW", SetLastError = true)]
        static extern int GetShortPathName(string pathName, System.Text.StringBuilder shortName, int cbShortName);

        public static string NomeArquivo { get; set; }
        public static int IdArquivo { get; set; }
        private static string NomeTabelaDST { get; set; }
        public static Global.TipoArquivo TipoArquivo { get; set; }
        //public static Global.Behavior Behavior { get; set; }
        public static string Delimitador { get; set; }
        //public static string LineFeed { get; set; }
        //public static int FirstLine { get; set; }

        private static ProcessArqController processArqController = new ProcessArqController();

        public static bool ProcessaTXT(string pSchemaDST, string pNomeTabelaDST,string pNomeArquivo,int pIdArquivo,string pDelimitador,Global.Behavior pBehavior)
        {
            bool Result=false;
            Global.EnviarParaLog($"INICIO DO ARQUIVO {pNomeTabelaDST.ToUpper()}", "ProcessaTXT", true,pIdArquivo);
            if (!FileIsOpen(pNomeArquivo, pIdArquivo))
            {
                
                if (CriarTabelaDST(pSchemaDST,pNomeTabelaDST, pIdArquivo))
                {
                    
                    SqlParameter SqlPar = new SqlParameter();
                    List<MapeamentosModel> lstMap = Mapeamentos(pIdArquivo);
                    List<SqlParameter> Parametros = new List<SqlParameter>();
                    string[] Linhas = File.ReadAllLines(pNomeArquivo);
                    foreach (string Linha in Linhas)
                    {
                        string Campos = "";
                        string Valor = "";
                        int Count = 0;
                        Parametros.Clear();
                        try
                        {
                            string xLinha = Linha;
                            if (pBehavior == Global.Behavior.FIXO || ((pBehavior == Global.Behavior.DELIMITADO) && xLinha.IndexOf(pDelimitador)>0))
                            {
                                foreach (var lMap in lstMap)
                                {
                                    if (Count == 0)
                                    {
                                        Campos = lMap.nm_coluna;
                                        Valor = "@" + lMap.nm_coluna;
                                    }
                                    else
                                    {
                                        Campos += ", " + lMap.nm_coluna;
                                        Valor += ", @" + lMap.nm_coluna;
                                    }
                                    Count += 1;

                                    if (pBehavior == Global.Behavior.FIXO)//FIXO
                                    {
                                        SqlPar.ParameterName = lMap.nm_coluna;
                                        SqlPar.Value = xLinha.Substring(lMap.fixo_inicio - 1, lMap.fixo_tamanho);
                                    }
                                    else//DELIMITADO
                                    {
                                        int fixo_inicio = 0;
                                        int fixo_tamanho = xLinha.IndexOf(pDelimitador);
                                        SqlPar.ParameterName = lMap.nm_coluna;
                                        SqlPar.Value = xLinha.Substring(fixo_inicio, fixo_tamanho);
                                        xLinha = xLinha.Remove(fixo_inicio, fixo_tamanho + 1);
                                    }
                                    Parametros.Add(new SqlParameter() { ParameterName = SqlPar.ParameterName, Value = SqlPar.Value });
                                }
                                SqlConnection conexaoDST = new SqlConnection(Global.ConnectionsString);
                                try
                                {
                                    conexaoDST.Open();
                                    string script = "Insert into " + pNomeTabelaDST + "(" + Campos + ") Values(" + Valor + ")";
                                    SqlCommand cmd = new SqlCommand(script, conexaoDST);
                                    cmd.CommandTimeout = Global.CommandTimeOut;
                                    cmd.ExecuteNonQuery();
                                }
                                finally
                                {
                                    conexaoDST.Close();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Global.EnviarParaLog($"ERRO: Linha [{Linha}] do Arquivo[{pNomeArquivo}] não importada. \n Motivo:[{ex.Message}]", "ProcessaTXT", true, pIdArquivo);
                        }
                    }
                    Result= true;
                }
                else
                {
                    Global.EnviarParaLog($"ERRO: Não foi possivel processar o Arquivo {pNomeArquivo.ToUpper()}.", "ProcessaTXT", true, pIdArquivo);
                    Result= false;
                }
            }
            else
            {
                Global.EnviarParaLog($"O Arquivo {pNomeArquivo.ToUpper()} está em uso.", "ProcessaTXT", true, pIdArquivo);
                Result= false;
            }
            Global.EnviarParaLog($"TÉRMINO DO ARQUIVO {pNomeTabelaDST.ToUpper()}", "ProcessaTXT", true, pIdArquivo);
            return Result;
        }

//        public static void ProcessaEmLote(string pNomeTabelaDST,string pPathFormatFile,string pDelimitador,int pIdArquivo, Global.Behavior pBehavior)
//        {
//            Global.EnviarParaLog($"INICIO DO ARQUIVO EM LOTE {pNomeTabelaDST.ToUpper()}", true, pIdArquivo);
//            if (!FileIsOpen(NomeArquivo, pIdArquivo))
//            {
//                if (CriarTabelaDST(pNomeTabelaDST,pIdArquivo))
//                {
//                    CriaFileBcpFormat(pNomeTabelaDST,pIdArquivo,pDelimitador,pBehavior);


////BULK INSERT CEPBRICK_CSV_1
////FROM 'C:\input\FF MDTR\CEPBRICK_CSV_1.csv'
////WITH(
////  FORMATFILE = 'C:\BCPFormat\FF MDTR\CEPBRICK_CSV_1.xml',
////  FIELDTERMINATOR = ';'
////  , ROWTERMINATOR = '\n'
////, FIRSTROW = 1
////, MAXERRORS = 1000000
////, ERRORFILE = 'C:\input_error\FF MDTR\CEPBRICK_CSV_1.txt'
//// );

//                    //-------------------Path_XML_FORMATFILE
//                    if (File.Exists($"{pPathFormatFile}\\{pNomeTabelaDST}.xml"))
//                    {
//                        string script = $"BULK INSERT {pNomeTabelaDST}";
//                        script += $" FROM '{NomeArquivo}'";
//                        script += $"  WITH (TABLOCK ";
//                        //-------------------Path_XML_FORMATFILE
//                        script += $",  FORMATFILE = '{pPathFormatFile}\\{pNomeTabelaDST}.xml' ";
//                        script += $",  FIRSTROW = {FirstLine.ToString()}";
//                        script += ",  MAXERRORS = 1000000";
//                        if (pDelimitador.Length > 0)
//                        {
//                            script += $",      FIELDTERMINATOR = '{pDelimitador}'";
//                        }
//                        script += $",  ROWTERMINATOR= '{LineFeed}'";
//                        //-------------------Path_XML_ERRORFILE
//                        script += $",  ERRORFILE = '{Global.PathInputError}\\{pNomeTabelaDST}_{DateTime.Now.ToString("ddMMyyyyHHmmss")}.txt'";
//                        script += ")";

//                        SqlConnection conexaoDST = new SqlConnection(Global.ConnectionsString);
//                        try
//                        {
//                            UsingTableLockOnBulkLoad(Global.SchemaDST, pNomeTabelaDST,1,pIdArquivo);
//                            conexaoDST.Open();
//                            SqlCommand cmd = new SqlCommand(script, conexaoDST);
//                            cmd.CommandTimeout = Global.CommandTimeOut;
//                            cmd.ExecuteNonQuery();
//                        }
//                        finally
//                        {
//                            UsingTableLockOnBulkLoad(Global.SchemaDST, pNomeTabelaDST, 0, pIdArquivo);
//                            conexaoDST.Close();
//                        }


//                        Global.EnviarParaLog("COMANDO BULCK EXECUTADO: " + script, true, pIdArquivo);
//                    }
//                    else
//                    {
//                        //-------------------------------------Path_XML_FORMATFILE
//                        Global.EnviarParaLog($"ERRO: Arquivo xml {pPathFormatFile}\\{pNomeTabelaDST}.xml não encontrado.", true, pIdArquivo);
//                    }
//                }
//                else
//                {
//                    Global.EnviarParaLog($"ERRO: Não foi possivel processar o Arquivo {NomeArquivo.ToUpper()}.", true, pIdArquivo);
//                }
//            }
//            else
//            {
//                Global.EnviarParaLog($"O Arquivo {NomeArquivo.ToUpper()} está em uso.", true, pIdArquivo);
//            }
//            Global.EnviarParaLog($"TÉRMINO DO ARQUIVO EM LOTE {pNomeTabelaDST.ToUpper()}", true, pIdArquivo);
//        }

        public static bool ImportarStageParaTabelaDST(string pSchemaDST,string pNomeTabelaSTG, string pNomeTabelaDST,string pNomeArquivo,int pIdArquivo)
        {
            bool Result = false;
            Global.EnviarParaLog($"INICIO DA IMPORTAÇÃO DA STAGE PARA TABELA {pNomeTabelaDST.ToUpper()} no Banco de Dados({pSchemaDST})", "ImpStageTOTabDST", true, pIdArquivo);
            if (!FileIsOpen(pNomeArquivo, pIdArquivo))
            {
                if (CriarTabelaDST(pSchemaDST,pNomeTabelaDST,pIdArquivo))
                {
                    string select = GetSelectInsert(pNomeTabelaSTG, pNomeTabelaDST, pIdArquivo);
                    Global.EnviarParaLog($"Insert Select executado: {select}.", "ImpStageTOTabDST", true, pIdArquivo);
                    try
                    {
                        UsingTableLockOnBulkLoad(pSchemaDST, pNomeTabelaDST,1, pIdArquivo);
                        string Erro = "";
                        Result = Conexao.ExecutaCommandText(select,processArqController.ConnectionsStringDST(pSchemaDST),ref Erro);
                        if (!Result)
                        {
                            Global.EnviarParaLog($"ERRO ao executar Insert Select: {select}. MOTIVO: {Erro}", "ImpStageTOTabDST", true, pIdArquivo);
                        }
                    }
                    catch(Exception ex)
                    {
                        Result = false;
                        Global.EnviarParaLog($"ERRO: Não foi possivel processar o Arquivo {pNomeArquivo.ToUpper()}. Motivo: {ex.Message}", "ImpStageTOTabDST", true, pIdArquivo);
                    }
                    finally
                    {
                        UsingTableLockOnBulkLoad(pSchemaDST, pNomeTabelaDST,0, pIdArquivo);
                    }
                }
                else
                {
                    Result = false;
                    Global.EnviarParaLog($"ERRO: Não foi possivel criar atabela Destino {pNomeTabelaDST.ToUpper()}.", "ImpStageTOTabDST", true, pIdArquivo);
                }
            }
            else
            {
                Result = false;
                Global.EnviarParaLog($"O Arquivo {pNomeArquivo.ToUpper()} está em uso.", "ImpStageTOTabDST", true, pIdArquivo);
            }
            Global.EnviarParaLog($"TÉRMINO DA IMPORTAÇÃO DA STAGE PARA TABELA {pNomeTabelaDST.ToUpper()}", "ImpStageTOTabDST", true, pIdArquivo);
            return Result;
        }

        public static bool ProcessaEmLoteSTG(string pSchemaSTG, string pNomeTabelaSTG,string pNomeTabelaDST, string pPathFormatFile,string pDelimitador, string pFieldQuote, string pNomeArquivo,int pIdArquivo, Global.Behavior pBehavior,int pFirstLine=1, string pLineFeed = "\\r\\n")
        {
            bool Result = false;
            Global.EnviarParaLog($"INICIO DO ARQUIVO EM LOTE {pNomeTabelaSTG.ToUpper()}", "Processa_LoteSTG", true, pIdArquivo);
            if (!FileIsOpen(pNomeArquivo, pIdArquivo))
            {
                if (CriarTabelaSTG(pSchemaSTG,pNomeTabelaSTG, pIdArquivo))
                {
                    CriaFileBcpFormatSTG(pNomeTabelaSTG, pIdArquivo,pDelimitador,pBehavior, pLineFeed);


                    //BULK INSERT CEPBRICK_CSV_1
                    //FROM 'C:\input\FF MDTR\CEPBRICK_CSV_1.csv'
                    //WITH(
                    //  FORMATFILE = 'C:\BCPFormat\FF MDTR\CEPBRICK_CSV_1.xml',
                    //  FIELDTERMINATOR = ';'
                    //  , ROWTERMINATOR = '\n'
                    //, FIRSTROW = 1
                    //, MAXERRORS = 1000000
                    //, ERRORFILE = 'C:\input_error\FF MDTR\CEPBRICK_CSV_1.txt'
                    // );

                    //-------------------Path_XML_FORMATFILE
                    Global.EnviarParaLog($"Verifica se o Arquivo: {pPathFormatFile}\\{pNomeTabelaSTG}.xml existe.", "Processa_LoteSTG", true, pIdArquivo);
                    if (File.Exists($"{pPathFormatFile}\\{pNomeTabelaSTG}.xml"))
                    {
                        Global.EnviarParaLog($"Arquivo: {pPathFormatFile}\\{pNomeTabelaSTG}.xml encontrado.", "Processa_LoteSTG", true, pIdArquivo);
                        try
                        {
                            string script = $"BULK INSERT {pNomeTabelaSTG}";
                            script += $" FROM '{pNomeArquivo}'";
                            script += $"  WITH (TABLOCK ";
                            //-------------------Path_XML_FORMATFILE
                            script += $",  FORMATFILE = '{pPathFormatFile}\\{pNomeTabelaSTG}.xml' ";
                            script += $",  FIRSTROW = {pFirstLine.ToString()}";
                            script += ",  MAXERRORS = 1000000";
                            if (pFieldQuote.Length > 0)
                            {
                                script += $",      FORMAT = 'CSV'";
                                script += $",      FIELDQUOTE = '{pFieldQuote}'";
                            }
                            if (pDelimitador.Length > 0)
                            {
                                script += $",      FIELDTERMINATOR = '{pDelimitador}'";
                            }
                            script += $",  ROWTERMINATOR= '{pLineFeed}'";
                            //-------------------Path_XML_ERRORFILE
                            script += $",  ERRORFILE = '{Global.PathInputError}\\{pNomeTabelaSTG}_{DateTime.Now.ToString("ddMMyyyyHHmmss")}.txt'";
                            script += ")";

                            Global.EnviarParaLog("COMANDO BULCK EXECUTADO: " + script, "Processa_LoteSTG", true, pIdArquivo);
                            UsingTableLockOnBulkLoad(pSchemaSTG, pNomeTabelaSTG,1, pIdArquivo);
                            string Erro = "";
                            Result = Conexao.ExecutaCommandText(script, processArqController.ConnectionsStringDST(pSchemaSTG),ref Erro);
                            if (Result)
                            {
                                Global.EnviarParaLog("Importação do Arquivo para Stage com sucesso.", "Processa_LoteSTG", true, pIdArquivo);
                            }
                            else
                            {
                                Global.EnviarParaLog($"ERRO na importação do Arquivo para Stage. MOTIVO: {Erro}", "Processa_LoteSTG", true, pIdArquivo);
                            }
                            Erro = "";
                        }
                        catch(Exception ex)
                        {
                            Result = false;
                            Global.EnviarParaLog($"Erro na Importação do Arquivo para Stage. Motivo:{ex.Message}", "Processa_LoteSTG", true, pIdArquivo);

                        }
                        finally
                        {
                            UsingTableLockOnBulkLoad(pSchemaSTG, pNomeTabelaSTG,0, pIdArquivo);
                        }

                        try
                        {
                            Result = Result && ImportarStageParaTabelaDST(pSchemaSTG, pNomeTabelaSTG, pNomeTabelaDST, pNomeArquivo, pIdArquivo);
                            if (Result)
                            {
                                Global.EnviarParaLog("Importação da Stage para a tabela destino com sucesso.", "Processa_LoteSTG", true, pIdArquivo);
                            }
                        }
                        catch (Exception ex)
                        {
                            Result = false;
                            Global.EnviarParaLog($"Erro na Importação da Stage para a tabela destino. Motivo:{ex.Message}", "Processa_LoteSTG", true, pIdArquivo);

                        }
                    }
                    else
                    {
                        //-------------------------------------Path_XML_FORMATFILE
                        Global.EnviarParaLog($"ERRO: Arquivo xml {pPathFormatFile}\\{pNomeTabelaSTG}.xml não encontrado.", "Processa_LoteSTG", true, pIdArquivo);
                    }
                }
                else
                {
                    Global.EnviarParaLog($"ERRO: Não foi possível processar o Arquivo {NomeArquivo.ToUpper()}.", "Processa_LoteSTG", true, pIdArquivo);
                }
            }
            else
            {
                Global.EnviarParaLog($"O Arquivo {NomeArquivo.ToUpper()} está em uso.", "Processa_LoteSTG", true, pIdArquivo);
            }
            Global.EnviarParaLog($"TÉRMINO DO ARQUIVO EM LOTE {pNomeTabelaSTG.ToUpper()}", "Processa_LoteSTG", true, pIdArquivo);
            return Result;
        }

        public static bool ProcessaEmLoteXLSX(string pSchemaDST, string pNomeTabelaDST,string pNomePlanilha,string pNomeArquivo,int pIdArquivo)
        {
            bool Result = false;
            string script = "";
            Global.EnviarParaLog($"INICIO DO ARQUIVO EXCEL {pNomeTabelaDST.ToUpper()}", true, pIdArquivo);
            try
            {
                if (!FileIsOpen(pNomeArquivo, pIdArquivo))
                {
                    if (CriarTabelaDST(pSchemaDST,pNomeTabelaDST,pIdArquivo))
                    {
                        string ListaCampos = "";
                        List<MapeamentosModel> lstCampos = Mapeamentos(pIdArquivo);
                        foreach (var lstFilds in lstCampos)
                        {
                            if (lstFilds.ordem == 1)
                            {
                                ListaCampos = $"[{lstFilds.nm_coluna}]";
                            }
                            else
                            {
                                ListaCampos += "," + $"[{lstFilds.nm_coluna}]";
                            }
                        }
                        script += $"INSERT INTO {pNomeTabelaDST.ToUpper()} WITH (TABLOCK)";
                        script += "( ";
                        script += $"  {ListaCampos} ";
                        script += ")";
                        script += $"SELECT {GetCamposSelectFormatado(pIdArquivo)} ";
                        script += $"  FROM OPENROWSET('Microsoft.ACE.OLEDB.12.0',";// --Este parametro referse ao provider do OleDb
                        script += $" 'Excel 12.0; Database={pNomeArquivo}',";// --Primeiro parametro informa  a versao do excel e o segundo parametro informa o caminho de onde está o arquivo xlsx
                        script += $" 'select * from [{pNomePlanilha}$]') ";// --Este parametro é referente o nome da Planilha(aba inferior)
                        UsingTableLockOnBulkLoad(Global.SchemaDST, pNomeTabelaDST,1, pIdArquivo);
                        try
                        {
                            string Erro = "";
                            Result=Conexao.ExecutaCommandText(script, processArqController.ConnectionsStringDST(pSchemaDST),ref Erro);
                            if (Result)
                            {
                                Global.EnviarParaLog("COMANDO OPENROWSET EXCEL EXECUTADO: " + script, "Processa_LoteXLS", true, pIdArquivo);
                            }
                            else
                            {
                                Global.EnviarParaLog($"ERRO AO EXECUTAR O COMANDO OPENROWSET EXCEL {script}. MOTIVO: {Erro}", "Processa_LoteXLS", true, pIdArquivo);
                            }

                        }
                        finally
                        {
                            UsingTableLockOnBulkLoad(pSchemaDST, pNomeTabelaDST,0, pIdArquivo);
                        }
                        //Global.EnviarParaLog($"Tabela  {Global.NomeTabelaDST.ToUpper()} criada.");
                    }
                    else
                    {
                        Result = false;
                        Global.EnviarParaLog($"ERRO: Não foi possivel processar o Arquivo {pNomeArquivo.ToUpper()}.", "Processa_LoteXLS",true, pIdArquivo);
                    }
                }
                else
                {
                    Result = false;
                    Global.EnviarParaLog($"O Arquivo {pNomeArquivo.ToUpper()} está em uso.", "Processa_LoteXLS",true, pIdArquivo);
                }
            }
            catch(Exception ex)
            {
                Result = false;
                Global.EnviarParaLog($"Erro ao Executar o comando OPENROWSET  {script}: [{ex.Message}]", "Processa_LoteXLS", true, pIdArquivo);
            }
            Global.EnviarParaLog($"TÉRMINO DO ARQUIVO EM LOTE OPENROWSET EXCEL {pNomeTabelaDST.ToUpper()}", "Processa_LoteXLS",true, pIdArquivo);
            return Result;
        }
        public static string GetShortPathName(string longFileName,int pIdArquivo=0)
        {
            string Result = "";
            try
            {
                var sb1 = new StringBuilder();
                int sz = GetShortPathName(longFileName, sb1, 0);
                if (sz == 0)
                    throw new Win32Exception();
                var sb = new StringBuilder(sz + 1);
                sz = GetShortPathName(longFileName, sb, sb.Capacity);
                if (sz == 0)
                    throw new Win32Exception();
                Result = sb.ToString();
            }
            catch(Exception ex)
            {
                Global.EnviarParaLog($"Erro ao pegar o ShortName do arquivo. Motivo: {ex.Message}", "GetShortPathName", true, pIdArquivo);
            }
            return Result;
        }

        public static bool ProcessaEmLoteDBF(string pSchemaDST, string pNomeTabelaDST,string pNomeArquivo,int pIdArquivo)
        {
            bool Result = false;
            Global.EnviarParaLog($"INICIO DO ARQUIVO OPENROWSET DBF {pNomeTabelaDST.ToUpper()}", true, pIdArquivo);
            try
            {
                if (!FileIsOpen(pNomeArquivo, pIdArquivo))
                {
                    if (CriarTabelaDST(pSchemaDST,pNomeTabelaDST, pIdArquivo))
                    {
                        string ListaCampos = "";
                        List<MapeamentosModel> lstCampos = Mapeamentos(pIdArquivo);
                        foreach (var lstFilds in lstCampos)
                        {
                            if (lstFilds.ordem == 1)
                            {
                                ListaCampos = $"[{lstFilds.nm_coluna}]";
                            }
                            else
                            {
                                ListaCampos += "," + $"[{lstFilds.nm_coluna}]";
                            }
                        }

                        string script = "";
                        try
                        {
                            //Quando o arquivo dbf tem o nome maior que 8 digitos da erro na importação
                            //esse tratamento é feito somente par arquivos com o nome maior q 8 digitos
                            //bool NomeArqRenomeado = false;
                            string NomeORG = pNomeArquivo;
                            string OnlyFileName = Path.GetFileName(pNomeArquivo);
                            OnlyFileName = OnlyFileName.Substring(0, OnlyFileName.IndexOf('.'));
                            if (OnlyFileName.Length > 8)
                            {
                                //pNomeArquivo = Path.GetDirectoryName(pNomeArquivo) + "\\DBF_TEMP.dbf";
                                pNomeArquivo = GetShortPathName(Path.GetDirectoryName(pNomeArquivo) +"\\"+ Path.GetFileName(pNomeArquivo));
                                //try
                                //{
                                //    File.Move(NomeORG, pNomeArquivo);
                                //    NomeArqRenomeado = true;
                                //}
                                //catch(Exception ex)
                                //{
                                //    Global.EnviarParaLog($"Erro ao renomear o arquivo: {NomeORG}. Motivo:{ex.Message}", true, pIdArquivo);
                                //}
                            }

                            script += $"INSERT INTO {pNomeTabelaDST.ToUpper()} WITH (TABLOCK)";
                            script += "( ";
                            script += $"  {ListaCampos} ";
                            script += ") ";
                            script += $"SELECT {GetCamposSelectFormatado(pIdArquivo)} ";
                            script += $"  FROM OPENROWSET('Microsoft.ACE.OLEDB.12.0',";// --Este parametro referse ao provider do OleDb
                            script += $" 'dBase 5.0; Database={Path.GetDirectoryName(pNomeArquivo)}',";// --Primeiro parametro informa  a versao do excel e o segundo parametro informa o caminho de onde está o arquivo xlsx
                            script += $" 'select * from [{Path.GetFileName(pNomeArquivo)}]') ";// --Este parametro é referente o nome da Planilha(aba inferior)

                            UsingTableLockOnBulkLoad(pSchemaDST, pNomeTabelaDST,1, pIdArquivo);
                            Global.EnviarParaLog("COMANDO OPENROWSET DBF SERA EXECUTADO: " + script, "Processa_LoteDBF", true, pIdArquivo);
                            string Erro = "";
                            Result = Conexao.ExecutaCommandText(script, processArqController.ConnectionsStringDST(pSchemaDST),ref Erro);
                            if (!Result)
                            {
                                Global.EnviarParaLog($"ERRO AO EXECUTAR O COMANDO OPENROWSET: {script} => MOTIVO: {Erro}", "Processa_LoteDBF", true, pIdArquivo);
                            }
                            Erro = "";

                            //if (NomeArqRenomeado)
                            //{
                            //    File.Move(pNomeArquivo, NomeORG);
                            //    NomeArqRenomeado = false;
                            //}
                            //Result = true;
                        }
                        catch (Exception ex)
                        {
                            Result = false;
                            Global.EnviarParaLog($"ERRO: Não foi possível executar o comando OPENROWSET {script}. MOTIVO: {ex.Message}.", "Processa_LoteDBF",true, pIdArquivo);
                        }
                        finally
                        {
                            UsingTableLockOnBulkLoad(pSchemaDST, pNomeTabelaDST,0, pIdArquivo);
                        }
                    }
                    else
                    {
                        Result = false;
                        Global.EnviarParaLog($"ERRO: Não foi possível processar o Arquivo {pNomeArquivo.ToUpper()}.", "Processa_LoteDBF",true, pIdArquivo);
                    }
                }
                else
                {
                    Result = false;
                    Global.EnviarParaLog($"O Arquivo {pNomeArquivo.ToUpper()} está em uso.", "Processa_LoteDBF",true, pIdArquivo);
                }
                
            }
            catch(Exception ex)
            {
                Global.EnviarParaLog($"ERRO: {ex.Message}", "Processa_LoteDBF",true, pIdArquivo);
                Result = false;
            }
            Global.EnviarParaLog($"TÉRMINO DO ARQUIVO EM LOTE OPENROWSET DBF {pNomeTabelaDST.ToUpper()}", "Processa_LoteDBF",true, pIdArquivo);
            return Result;
        }

        public static bool FileIsOpen(string NomeFile,int pIdArquivo=0)
        {
            bool fileopen = false;
            bool Liberado = false;
            while (!Liberado)
            {
                try
                {
                    FileStream fs = File.OpenRead(NomeFile);
                    fs.Close();
                    fileopen = false;
                }
                catch(Exception ex)
                {
                    fileopen = true;
                    Global.EnviarParaLog($"Aguardando a liberação do arquivo: ({NomeFile}) para prosseguir. Motivo: {ex.Message}", true,pIdArquivo);
                    Thread.Sleep(30000);
                }

                Liberado = !fileopen;

            }
            Global.EnviarParaLog($"Arquivo: ({NomeFile}) liberado.", true, pIdArquivo);
            return fileopen;
        }

        private static List<MapeamentosModel> Mapeamentos(int pIdArquivo)
        {
            return processArqController.Mapeamentos(pIdArquivo);
        }


        public static bool TabelaCriada(string pSchema,string pNomeTabela)
        {
            bool Result=false;
            SqlConnection _Conn = new SqlConnection(processArqController.ConnectionsStringDST(pSchema));
            _Conn.Open();
            string script = $"select table_name Result from INFORMATION_SCHEMA.TABLES where TABLE_CATALOG='{pSchema}' and table_name='{pNomeTabela}'";
            SqlCommand cmd = new SqlCommand(script,_Conn);
            cmd.CommandTimeout = Global.CommandTimeOut;
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows && dr.Read())
            {
                string vNomeTabela = dr["Result"].ToString();
                if (vNomeTabela == "")
                {
                    Result = false;
                }
                else
                {
                    Result = true;
                }
            }

            _Conn.Close();
            return Result;
        }
        private static bool CriarTabelaDST(string pSchemaDST, string pNomeTabelaDST,int pIdArquivo)
        {
            bool Result = false;
            bool tabelaDSTCriada = TabelaCriada(pSchemaDST, pNomeTabelaDST);
            List<MapeamentosModel> mapArq= Mapeamentos(pIdArquivo);
            string lstScript = "";
            Global.EnviarParaLog($"Lista de campos de Mapeamento retornou {mapArq.Count} campos", "CriarTabelaDST", true, pIdArquivo);
            if (mapArq.Count()>0)
            {
                Global.EnviarParaLog($"Colhendo dados para a geração do script da criação da tabela destino", "CriarTabelaDST", true, pIdArquivo);
                int lin = 0;
                lstScript=$"CREATE TABLE {pNomeTabelaDST}(";
                foreach (var drTabela in mapArq)
                {
                    if(Convert.ToInt32(drTabela.tm_coluna.ToString()) > 0)
                    {
                        if (lin > 0)
                        {
                            lstScript+=",";
                        }
                        if (Convert.ToInt32(drTabela.pr_coluna.ToString()) > 0)
                        {
                            lstScript += $"[{drTabela.nm_coluna.ToString()}] {drTabela.tp_coluna.ToString()}({drTabela.tm_coluna.ToString()},{drTabela.pr_coluna.ToString()})";
                        }
                        else
                        {
                            lstScript += $"[{drTabela.nm_coluna.ToString()}] {drTabela.tp_coluna.ToString()}({drTabela.tm_coluna.ToString()})";
                        }
                        lin += 1;
                    }
                    else
                    {
                        if (lin > 0)
                        {
                            lstScript += ",";
                        }
                        lstScript += $"[{drTabela.nm_coluna.ToString()}] {drTabela.tp_coluna.ToString()}";
                        lin += 1;
                    }
                }
                lstScript+=$")";
            }
            Global.EnviarParaLog($"SCRIPT CRIACAO DA TABELA DESTINO: {lstScript}", "CriarTabelaDST", true, pIdArquivo);
            try
                {
                if (!tabelaDSTCriada)
                {
                    if (lstScript.Count()>0)
                    {
                        SqlConnection _Conn = new SqlConnection(processArqController.ConnectionsStringDST(pSchemaDST));
                        _Conn.Open();
                        try
                        {
                            Global.EnviarParaLog(lstScript.ToString(), "CriarTabelaDST", true, pIdArquivo);
                            Conexao.ExecutarScript(lstScript.ToString(), _Conn);
                            Global.EnviarParaLog($"Tabela destino[{pNomeTabelaDST}] criada com sucesso  no Banco de Dados({pSchemaDST}).", "CriarTabelaDST", true, pIdArquivo);
                            Result = true;
                        }
                        catch (Exception ex)
                        {
                            Result = false;
                            Global.EnviarParaLog($"ATENÇÃO: Erro ao executar script para criar a tabela destino[{pNomeTabelaDST}]. \n ERRO: {ex.Message}", "CriarTabelaDST", true, pIdArquivo);
                        }
                        finally
                        {
                            _Conn.Close();
                        }
                    }
                    else
                    {
                        Result = false;
                        Global.EnviarParaLog($"ATENÇÃO: Não foi possível pegar os dados para a criação da tabela Destino [{pNomeTabelaDST}].", "CriarTabelaDST", true, pIdArquivo);
                    }
                }
                else
                {
                    Result = true;
                }
            }
            catch(Exception ex)
            {
                Result = false;
                Global.EnviarParaLog($"ATENÇÃO: Não foi possível criar a tabela Destino [{pNomeTabelaDST}]. \n ERRO: {ex.Message}", "CriarTabelaDST", true, pIdArquivo);
            }
            return Result;
        }
        private static bool CriarTabelaSTG(string pSchemaDST,string pNomeTabelaSTG,int pIdArquivo)
        {
            
            int SizeCol = 0;
            bool Result = false;
            Global.EnviarParaLog($"Verifica se a tabela({pNomeTabelaSTG}) está criada  Banco de Dados({pSchemaDST}).", "CriarTabelaSTG", true, IdArquivo);

            bool tabelaSTGCriada = TabelaCriada(pSchemaDST, pNomeTabelaSTG);
            if (tabelaSTGCriada)
            {
                processArqController.DropTabela(pNomeTabelaSTG,processArqController.ConnectionsStringDST(pSchemaDST));
                tabelaSTGCriada = TabelaCriada(pSchemaDST, pNomeTabelaSTG);
                Global.EnviarParaLog($"Tabela({pNomeTabelaSTG}) dropada do Banco de Dados({pSchemaDST}).", "CriarTabelaSTG", true, IdArquivo);
            }
            List<MapeamentosModel> mapArq = Mapeamentos(pIdArquivo);
            string lstScript = "";
            if (mapArq.Count() > 0)
            {
                int lin = 0;
                lstScript = $"CREATE TABLE {pNomeTabelaSTG}(";
                foreach (var drTabela in mapArq)
                {
                    if (Convert.ToInt32(drTabela.tm_coluna.ToString()) > 0)
                    {
                        if (Convert.ToInt32(drTabela.pr_coluna.ToString()) > 0)
                        {
                            SizeCol = drTabela.tm_coluna + drTabela.pr_coluna + 1;
                        }
                        else
                        {
                            SizeCol = drTabela.tm_coluna;
                        }
                    }
                    else
                    {
                        SizeCol = 30;
                    }
                    if (lin > 0)
                    {
                        lstScript += ",";
                    }
                    lstScript += $"[{drTabela.nm_coluna.ToString()}] nvarchar({SizeCol.ToString()})";
                    lin += 1;

                }
                lstScript += $")";
            }
            Global.EnviarParaLog($"SCRIPT CRIACAO DA TABELA STAGE_ETL: {lstScript}", "CriarTabelaSTG", true, pIdArquivo);
            try
            {
                if (!tabelaSTGCriada)
                {
                    if (lstScript.Count() > 0)
                    {
                        SqlConnection _Conn = new SqlConnection(processArqController.ConnectionsStringDST(pSchemaDST));
                        _Conn.Open();
                        try
                        {
                            Global.EnviarParaLog(lstScript.ToString(), "CriarTabelaSTG",true, pIdArquivo);
                            Conexao.ExecutarScript(lstScript.ToString(), _Conn);
                            Global.EnviarParaLog($"Tabela STAGE: {pNomeTabelaSTG} criada com sucesso no Banco de Dados({pSchemaDST}).", "CriarTabelaSTG", true, pIdArquivo);
                            Result = true;
                        }
                        catch (Exception ex)
                        {
                            Result = false;
                            Global.EnviarParaLog($"ATENÇÃO: Erro ao executar script para criar a tabela STAGE_ETL. \n ERRO: {ex.Message}", "CriarTabelaSTG", true, pIdArquivo);
                        }
                        finally
                        {
                            _Conn.Close();
                        }
                    }
                    else
                    {
                        Result = false;
                        Global.EnviarParaLog($"ATENÇÃO: Não foi possível pegar os dados para a criação da tabela [{pNomeTabelaSTG}].", "CriarTabelaSTG", true, pIdArquivo);
                    }
                }
                else
                {
                    Result = true;
                }
            }
            catch (Exception ex)
            {
                Result = false;
                Global.EnviarParaLog($"ATENÇÃO: Não foi possível criar a tabela [{pNomeTabelaSTG}]. \n ERRO: {ex.Message}", "CriarTabelaSTG", true, pIdArquivo);
            }
            return Result;
        }

        public static void MoveParaProcessados(string pNomeArquivo,int pIdArquivo,bool pSucesso=true)
        {
            string PathInput = "";
            //List<ArquivosModel> lstArq = processArqController.GetArquivos(pIdArquivo);
            //foreach (ArquivosModel am in lstArq)
            //{
                PathInput = Path.GetDirectoryName(pNomeArquivo);// am.dir_entrada;
            //}

            string pasta = "";
            if (pSucesso)
            {
                pasta = "Com_Sucesso";
            }
            else
            {
                pasta = "Com_Erro";
            }
            string PathEntrada = PathInput.Substring(PathInput.Length - 1, 1);
            PathEntrada = PathEntrada != "\\" ? PathInput+"\\" : PathInput;

            if (!Directory.Exists(PathEntrada + $"Processados\\{pasta}\\" ))
            {
                Directory.CreateDirectory(PathEntrada + $"Processados\\{pasta}\\");
            }
            string NomeDoArquivo = Path.GetFileName(pNomeArquivo);
            string FileNameOrg = PathEntrada + "\\" + NomeDoArquivo;
            string FileNameDst = PathEntrada + $"Processados\\{pasta}\\" + "\\"+NomeDoArquivo;
            bool Liberado = false;
            while (!Liberado)
            {
                try
                {
                    Liberado = true;
                    File.Move(FileNameOrg, FileNameDst);
                }
                catch(Exception ex)
                {
                    Liberado = false;
                    Global.EnviarParaLog($"Aguardando a liberação do arquivo: ({pNomeArquivo}) para prosseguir. Motivo: {ex.Message}", "MoveTOProcessado", true, pIdArquivo);
                    Thread.Sleep(30000);
                }

            }
            Global.EnviarParaLog($"Arquivo: ({pNomeArquivo}) liberado.", "MoveTOProcessado", true, pIdArquivo);            
        }
        public static void CriaFileBcpFormat(string pNomeTabelaDST,int pIdArquivo,string pDelimitador, Global.Behavior pBehavior,string pLineFeed="\\r\\n")
        {
            string FormatFile = Global.PathFormatFile + "\\" ;
            if (!Directory.Exists(FormatFile))
            {
                Directory.CreateDirectory(FormatFile);
            }

            string input_error = Global.PathInputError + "\\" ;
            if (!Directory.Exists(input_error))
            {
                Directory.CreateDirectory(input_error);
            }
            
            //-------------------Path_XML_FORMATFILE
            string NomeFileFormat = $"{FormatFile}\\{pNomeTabelaDST}.xml";
            using (StreamWriter fileFormat=new StreamWriter(NomeFileFormat))
            {
                fileFormat.WriteLine("<?xml version='1.0'?>");
                fileFormat.WriteLine("<BCPFORMAT   ");
                fileFormat.WriteLine("xmlns='http://schemas.microsoft.com/sqlserver/2004/bulkload/format'   ");
                fileFormat.WriteLine("xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'>  ");
                fileFormat.WriteLine("  <RECORD>  ");
                List<MapeamentosModel> lstMap = Mapeamentos(pIdArquivo);
                int LastReg = processArqController.GetQtdReg(pIdArquivo);
                string[] Field = new string[lstMap.Count];
                foreach (var lst in lstMap)
                {
                    if ( pBehavior == Global.Behavior.FIXO)
                    {
                        if (lst.ordem < LastReg)
                        {
                            fileFormat.WriteLine($"    <FIELD ID='{lst.ordem}' xsi:type='CharFixed' LENGTH='{lst.fixo_tamanho}'/>   ");
                        }
                        else
                        {                                                                                        // colocar \\r novamente
                            fileFormat.WriteLine($"    <FIELD ID='{lst.ordem}' xsi:type='CharTerm' TERMINATOR='{pLineFeed}'/>   ");
                        }
                    }
                    else
                    {
                        
                        if (lst.ordem< LastReg)
                        {
                            if (lst.tm_coluna == 0)
                            {
                                fileFormat.WriteLine($"    <FIELD ID='{lst.ordem}' xsi:type='CharTerm' TERMINATOR='{pDelimitador}' />   ");
                            }
                            else
                            {
                                fileFormat.WriteLine($"    <FIELD ID='{lst.ordem}' xsi:type='CharTerm' TERMINATOR='{pDelimitador}' MAX_LENGTH = '{lst.tm_coluna}' />   ");
                            }
                        }
                        else
                        {
                            if (lst.tm_coluna == 0)
                            {
                                                                                                                    // colocar \\r novamente
                                fileFormat.WriteLine($"    <FIELD ID='{lst.ordem}' xsi:type='CharTerm' TERMINATOR='{pLineFeed}' />   ");
                            }
                            else
                            {
                                                                                                                   // colocar \\r novamente
                                fileFormat.WriteLine($"    <FIELD ID='{lst.ordem}' xsi:type='CharTerm' TERMINATOR='{pLineFeed}' MAX_LENGTH = '{lst.tm_coluna}' />   ");
                            }
                        }
                    }
                }

                fileFormat.WriteLine("  </RECORD>  ");
                fileFormat.WriteLine("  <ROW>  ");
                foreach (var lst in lstMap)
                {
                    fileFormat.WriteLine($"    <COLUMN SOURCE='{lst.ordem}' NAME='{lst.nm_coluna}' xsi:type='{GetDataType(lst.tp_coluna)}'/>  ");
                }

                fileFormat.WriteLine("  </ROW>  ");
                fileFormat.WriteLine("</BCPFORMAT>  ");
            }
        }

        public static void CriaFileBcpFormatSTG(string pNomeTabelaSTG, int pIdArquivo, string pDelimitador, Global.Behavior pBehavior,string pLineFeed="\\r\\n")
        {
            string FormatFile = Global.PathFormatFile + "\\";
            if (!Directory.Exists(FormatFile))
            {
                Directory.CreateDirectory(FormatFile);
            }

            string input_error = Global.PathInputError + "\\";
            if (!Directory.Exists(input_error))
            {
                Directory.CreateDirectory(input_error);
            }

            //-------------------Path_XML_FORMATFILE
            string NomeFileFormat = $"{FormatFile}\\{pNomeTabelaSTG}.xml";
            using (StreamWriter fileFormat = new StreamWriter(NomeFileFormat))
            {
                int SizeCol = 0;
                fileFormat.WriteLine("<?xml version='1.0'?>");
                fileFormat.WriteLine("<BCPFORMAT   ");
                fileFormat.WriteLine("xmlns='http://schemas.microsoft.com/sqlserver/2004/bulkload/format'   ");
                fileFormat.WriteLine("xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'>  ");
                fileFormat.WriteLine("  <RECORD>  ");
                List<MapeamentosModel> lstMap = Mapeamentos(pIdArquivo);
                int LastReg = processArqController.GetQtdReg(pIdArquivo);
                string[] Field = new string[lstMap.Count];
                foreach (var lst in lstMap)
                {
                    if (pBehavior == Global.Behavior.FIXO)
                    {
                        if (lst.ordem < LastReg)
                        {
                            fileFormat.WriteLine($"    <FIELD ID='{lst.ordem}' xsi:type='CharFixed' LENGTH='{lst.fixo_tamanho}'/>   ");
                        }
                        else
                        {
                                                                                                            // colocar \\r novamente
                            fileFormat.WriteLine($"    <FIELD ID='{lst.ordem}' xsi:type='CharTerm' TERMINATOR='{pLineFeed}'/>   ");
                        }
                    }
                    else
                    {
                        if (lst.tm_coluna == 0)
                        {
                            SizeCol = 30;//Se cair aqui é porque o campo é do tipo Inteiro ou Data
                        }
                        else
                        {
                            if (lst.pr_coluna > 0)
                            {
                                //Se cair aqui é porque o campo é do tipo Numeric ou Decimal
                                SizeCol = lst.tm_coluna + lst.pr_coluna + 1;
                            }
                            else
                            {
                                SizeCol = lst.tm_coluna;
                            }
                        }

                        if (lst.ordem < LastReg)
                        {
                            fileFormat.WriteLine($"    <FIELD ID='{lst.ordem}' xsi:type='CharTerm' TERMINATOR='{pDelimitador}' MAX_LENGTH = '{SizeCol.ToString()}' />   ");
                        }
                        else
                        {
                                                                                                              // colocar \\r novamente
                            fileFormat.WriteLine($"    <FIELD ID='{lst.ordem}' xsi:type='CharTerm' TERMINATOR='{pLineFeed}' MAX_LENGTH = '{SizeCol.ToString()}' />   ");
                        }
                    }
                }

                fileFormat.WriteLine("  </RECORD>  ");
                fileFormat.WriteLine("  <ROW>  ");
                foreach (var lst in lstMap)
                {
                    fileFormat.WriteLine($"    <COLUMN SOURCE='{lst.ordem}' NAME='{lst.nm_coluna}' xsi:type='SQLCHAR'/>  ");
                }

                fileFormat.WriteLine("  </ROW>  ");
                fileFormat.WriteLine("</BCPFORMAT>  ");
            }
        }

        
        public static string GetDataType(string pDataType)
        {
            string Result = "";
            switch (pDataType.ToLower())
            {
                case "char":
                    Result = "SQLCHAR";
                    break;
                case "varchar":
                    Result = "SQLCHAR";
                    break;
                case "nchar":
                    Result = "SQLNCHAR";
                    break;
                case "nvarchar":
                    Result = "SQLNCHAR";
                    break;
                case "text":
                    Result = "SQLCHAR";
                    break;
                case "ntext":
                    Result = "SQLNCHAR";
                    break;
                case "binary":
                    Result = "SQLBINARY";
                    break;
                case "varbinary":
                    Result = "SQLBINARY";
                    break;
                case "imagem":
                    Result = "SQLBINARY";
                    break;
                case "datetime":
                    Result = "SQLDATETIME";
                    break;
                case "smalldatetime":
                    Result = "SQLDATETIM4";
                    break;
                case "decimal":
                    Result = "SQLDECIMAL";
                    break;
                case "numeric":
                    Result = "SQLNUMERIC";
                    break;
                case "float":
                    Result = "SQLFLT8";
                    break;
                case "real":
                    Result = "SQLFLT4";
                    break;
                case "int":
                    Result = "SQLINT";
                    break;
                case "bigint":
                    Result = "SQLBIGINT";
                    break;
                case "smallint":
                    Result = "SQLSMALLINT";
                    break;
                case "tinyint":
                    Result = "SQLTINYINT";
                    break;
                case "money":
                    Result = "SQLMONEY";
                    break;
                case "smallmoney":
                    Result = "SQLMONEY4";
                    break;
                case "bit":
                    Result = "SQLBIT";
                    break;
                case "uniqueidentifier":
                    Result = "SQLUNIQUEID";
                    break;
                case "sql_variant":
                    Result = "SQLVARIANT";
                    break;
                case "timestamp":
                    Result = "SQLBINARY";
                    break;
            }
            return Result;
        }

        public static string GetConversaoNumeric(string Mask, string Campo,string Size,string precision)
        {
            string Result = "";
            if (Mask == "#,##0.00")
            {
                Result = $"CONVERT(NUMERIC({Size},{precision}),replace({Campo}, ',', '')) {Campo}";
                
            }
            else if (Mask == "#.##0,00")
            {
                Result = $"CONVERT(NUMERIC({Size},{precision}),replace(replace({Campo}, '.', ''), ',','.')) {Campo}";
            }
            else if (Mask == "###0,00")
            {
                Result = $"CONVERT(NUMERIC({Size},{precision}),replace({Campo}, ',', '.')) {Campo}";
            }
            else if (Mask == "###000")
            {
                Result = $"CONVERT(NUMERIC({ Size},{ precision}), substring({Campo}, 0, len({Campo}) - ({precision}-1)) + '.' + substring({Campo}, len({Campo}) - ({precision}-1), {precision})) {Campo}";
            }
            else
            {
                Result = $"CONVERT(NUMERIC({Size},{precision}),{Campo}) {Campo}";
            }
            return Result;
        }
        public static string GetConversaoData(string Mask,string Campo)
        {
            string Result = "";
            string vCampo = "";
            string Codigo = "";
            if (Mask.ToUpper() != "VAZIO")
            {
                switch (Mask)
                {
                    case "dd/mm/yy":
                        Codigo = "3";
                        vCampo = Campo;
                        break;
                    case "dd-mm-yy":
                        Codigo = "3";
                        vCampo = Campo;
                        break;
                    case "dd.mm.yy":
                        Codigo = "3";
                        vCampo = Campo;
                        break;
                    case "dd/mm/yyyy":
                        Codigo = "103";
                        vCampo = Campo;
                        break;
                    case "dd-mm-yyyy":
                        Codigo = "103";
                        vCampo = Campo;
                        break;
                    case "dd.mm.yyyy":
                        Codigo = "103";
                        vCampo = Campo;
                        break;
                    case "ddmmyy":
                        Codigo = "3";
                        vCampo = $"stuff(stuff({Campo}, 3, 0, '/'), 6, 0, '/')";
                        break;
                    case "ddmmyyyy":
                        Codigo = "103";
                        vCampo = $"stuff(stuff({Campo}, 3, 0, '/'), 6, 0, '/')";
                        break;
                    case "mm/dd/yy":
                        Codigo = "1";
                        vCampo = Campo;
                        break;
                    case "mm-dd-yy":
                        Codigo = "1";
                        vCampo = Campo;
                        break;
                    case "mm.dd.yy":
                        Codigo = "1";
                        vCampo = Campo;
                        break;
                    case "mm/dd/yyyy":
                        Codigo = "101";
                        vCampo = Campo;
                        break;
                    case "mm.dd.yyyy":
                        Codigo = "101";
                        vCampo = Campo;
                        break;
                    case "mm-dd-yyyy":
                        Codigo = "101";
                        vCampo = Campo;
                        break;
                    case "mmddyy":
                        Codigo = "1";
                        vCampo = $"stuff(stuff({Campo},3,0,'/'),6,0,'/')";
                        break;
                    case "mmddyyyy":
                        Codigo = "101";
                        vCampo = $"stuff(stuff({Campo},3,0,'/'),6,0,'/')";
                        break;
                    case "yy/mm/dd":
                        Codigo = "11";
                        vCampo = Campo;
                        break;
                    case "yy-mm-dd":
                        Codigo = "11";
                        vCampo = Campo;
                        break;
                    case "yy.mm.dd":
                        Codigo = "11";
                        vCampo = Campo;
                        break;
                    case "yyyy/mm/dd":
                        Codigo = "111";
                        vCampo = Campo;
                        break;
                    case "yyyy-mm-dd":
                        Codigo = "111";
                        vCampo = Campo;
                        break;
                    case "yyyy.mm.dd":
                        Codigo = "111";
                        vCampo = Campo;
                        break;
                    case "yymmdd":
                        Codigo = "12";
                        vCampo = Campo;
                        break;
                    case "yyyymmdd":
                        Codigo = "112";
                        vCampo = Campo;
                        break;
                    case "hh:mi:ss":
                        Codigo = "108";
                        vCampo = Campo;
                        break;
                    case "hh:mi:ss:mmm":
                        Codigo = "114";
                        vCampo = Campo;
                        break;
                    case "24h:mi:ss:mmm":
                        Codigo = "114";
                        vCampo = Campo;
                        break;
                    case "dd-mm-yyyy hh:mi:ss":
                        Codigo = "108";
                        vCampo = $"substring({Campo},1,charindex(' ', {Campo})-1), 103) + convert(datetime, substring({Campo},charindex(' ', {Campo})+1,8)";
                        break;
                    case "dd-mm-yyyy 24h:mi:ss":
                        Codigo = "108";
                        vCampo = $"substring({Campo},1,charindex(' ', {Campo})-1), 103) + convert(datetime, substring({Campo},charindex(' ', {Campo})+1,8)";
                        break;
                    case "mm-dd-yyyy hh:mi:ss":
                        Codigo = "108";
                        vCampo = $"substring({Campo},1,charindex(' ', {Campo})-1), 101) + convert(datetime, substring({Campo},charindex(' ', {Campo})+1,8)";
                        break;
                    case "mm-dd-yyyy 24h:mi:ss":
                        Codigo = "108";
                        vCampo = $"substring({Campo},1,charindex(' ', {Campo})-1), 101) + convert(datetime, substring({Campo},charindex(' ', {Campo})+1,8)";
                        break;
                    case "yyyy-mm-dd hh:mi:ss":
                        Codigo = "120";
                        vCampo = Campo;
                        break;
                    case "yyyy-mm-dd 24h:mi:ss":
                        Codigo = "120";
                        vCampo = Campo;
                        break;
                    case "yyyy-mm-dd hh:mi:ss.mmm":
                        Codigo = "121";
                        vCampo = Campo;
                        break;
                    case "yyyy-mm-dd 24h:mi:ss.mmm":
                        Codigo = "121";
                        vCampo = Campo;
                        break;
                    case "yyyy-mm-ddThh:mi:ss.mmm":// (no spaces)
                        Codigo = "126";
                        vCampo = Campo;
                        break;
                    case "dd mon yy":
                        Codigo = "6";
                        vCampo = Campo;
                        break;
                    case "dd mon yyyy":
                        Codigo = "106";
                        vCampo = Campo;
                        break;
                    case "dd mon yyyy hh:mi:ss:mmm":
                        Codigo = "113";
                        vCampo = Campo;
                        break;
                    case "dd mon yyyy 24h:mi:ss:mmm":
                        Codigo = "113";
                        vCampo = Campo;
                        break;
                    case "mon dd yyyy hh:mi:ss:mmmAM":
                        Codigo = "109";
                        vCampo = Campo;
                        break;
                    case "mon dd yyyy hh:mi:ss:mmmPM":
                        Codigo = "109";
                        vCampo = Campo;
                        break;
                    case "mon dd yyyy hh:miAM":
                        Codigo = "100";
                        vCampo = Campo;
                        break;
                    case "mon dd yyyy hh:miPM":
                        Codigo = "100";
                        vCampo = Campo;
                        break;
                    case "Mon dd, yy":
                        Codigo = "7";
                        vCampo = Campo;
                        break;
                    case "Mon dd, yyyy":
                        Codigo = "107";
                        vCampo = Campo;
                        break;
                }
                Result = $"convert(datetime, {vCampo}, {Codigo})";
            }
            return Result;
        }


        public static string GetCamposSelect(int pIdArquivo)
        {
            string Campos = "";
            try
            {
                List<MapeamentosModel> mapArq = Mapeamentos(pIdArquivo);
                if (mapArq.Count() > 0)
                {
                    foreach (var drTabela in mapArq)
                    {
                        if (drTabela.ordem == 1)
                        {
                            Campos = drTabela.nm_coluna;
                        }
                        else
                        {
                            Campos += ", " + drTabela.nm_coluna;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Global.EnviarParaLog($"Erro ao pegar os campos do seelct. Motivo: {ex.Message}", "GetCamposSelect", true,pIdArquivo);
            }
            return Campos;
        }
        public static string GetCamposSelectFormatado(int pIdArquivo)
        {
            List<MapeamentosModel> mapArq = Mapeamentos(pIdArquivo);
            string CamposSelect = "";
            try
            {
                if (mapArq.Count() > 0)
                {
                    foreach (var drTabela in mapArq)
                    {
                        if (drTabela.ordem == 1)
                        {
                            if (drTabela.tp_coluna.ToLower() == "numeric" || drTabela.tp_coluna.ToLower() == "decimal")
                            {
                                if (drTabela.Mask_Campo == "ExpressaoSql")
                                {
                                    CamposSelect = $"{drTabela.ExpressaoSql.Replace("[CAMPO]", drTabela.nm_coluna)} {drTabela.nm_coluna}";
                                }
                                else
                                {
                                    CamposSelect = GetConversaoNumeric(drTabela.Mask_Campo, drTabela.nm_coluna, drTabela.tm_coluna.ToString(), drTabela.pr_coluna.ToString());
                                }
                            }
                            else if (drTabela.tp_coluna.ToLower() == "datetime")
                            {
                                if (drTabela.Mask_Campo == "ExpressaoSql")
                                {
                                    CamposSelect = $"{drTabela.ExpressaoSql.Replace("[CAMPO]", drTabela.nm_coluna)} {drTabela.nm_coluna}";
                                }
                                else
                                {
                                    CamposSelect = $"{GetConversaoData(drTabela.Mask_Campo, drTabela.nm_coluna)} {drTabela.nm_coluna}";
                                }
                            }
                            else
                            {
                                if (drTabela.Mask_Campo == "ExpressaoSql")
                                {
                                    CamposSelect = $"{drTabela.ExpressaoSql.Replace("[CAMPO]", drTabela.nm_coluna)} {drTabela.nm_coluna}";
                                }
                                else
                                {
                                    CamposSelect = $"[{drTabela.nm_coluna}]";
                                }

                            }

                        }
                        else
                        {
                            if (drTabela.tp_coluna.ToLower() == "numeric" || drTabela.tp_coluna.ToLower() == "decimal")
                            {
                                if (drTabela.Mask_Campo == "ExpressaoSql")
                                {
                                    CamposSelect += $", {drTabela.ExpressaoSql.Replace("[CAMPO]", drTabela.nm_coluna)} {drTabela.nm_coluna}";
                                }
                                else
                                {
                                    CamposSelect += $",{GetConversaoNumeric(drTabela.Mask_Campo, drTabela.nm_coluna, drTabela.tm_coluna.ToString(), drTabela.pr_coluna.ToString())}";
                                }
                            }
                            else if (drTabela.tp_coluna.ToLower() == "datetime")
                            {
                                if (drTabela.Mask_Campo == "ExpressaoSql")
                                {
                                    CamposSelect += $",{drTabela.ExpressaoSql.Replace("[CAMPO]", drTabela.nm_coluna)} {drTabela.nm_coluna}";
                                }
                                else
                                {
                                    CamposSelect += $",{GetConversaoData(drTabela.Mask_Campo, drTabela.nm_coluna)} {drTabela.nm_coluna}";
                                }
                            }
                            else
                            {
                                if (drTabela.Mask_Campo == "ExpressaoSql")
                                {
                                    CamposSelect += $", {drTabela.ExpressaoSql.Replace("[CAMPO]", drTabela.nm_coluna)} {drTabela.nm_coluna}";
                                }
                                else
                                {
                                    CamposSelect += $",[{drTabela.nm_coluna}]";
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Global.EnviarParaLog($"Erro ao pegar os campos formatados do select. Motivo: {ex.Message}", "GetCampSelFormat", true,pIdArquivo);
            }
            return CamposSelect;
        }

        public static string GetSelectInsert(string pNomeTabelaSTG, string pNomeTabelaDST, int pIdArquivo)
        {
            Global.EnviarParaLog($"Gerando Insert Select", "GetSelectInsert", true, pIdArquivo);

            string lstScript = "";
            lstScript = $"INSERT INTO {pNomeTabelaDST} WITH (TABLOCK)";
            lstScript += $" ({GetCamposSelect(pIdArquivo)}) ";
            lstScript += $"SELECT ";
            lstScript += $" {GetCamposSelectFormatado(pIdArquivo)} ";
            lstScript += $" FROM {pNomeTabelaSTG} ";
            Global.EnviarParaLog($"Insert Select Gerado({lstScript})", "GetSelectInsert", true, pIdArquivo);

            return lstScript;
        }

        public static void UsingTableLockOnBulkLoad(string pSchema,string pNametable, int pOnOff = 1,int pIdArquivo=0)
        {
            string script = $"{pSchema}.dbo.sp_tableoption '{pNametable}', 'table lock on bulk load', {pOnOff.ToString()}";
            try
            {
                using (SqlConnection _Conn = new SqlConnection(processArqController.ConnectionsStringDST(pSchema)))
                {
                    _Conn.Open();
                    Conexao.ExecutarScript(script, _Conn);
                }
            }
            catch(Exception ex)
            {
                Global.EnviarParaLog($"Erro ao executar o comando [{script}]. Motivo: {ex.Message}", "TableLockOnBulk", true, pIdArquivo);
            }
        }

        public static void AlterDatabaseToBulk_logged(string pDataBase)
        {
            if (GetRecoveryModelDesc(pDataBase) != "BULK_LOGGED")
            {
                string script = $"alter database {pDataBase} set recovery bulk_logged with no_wait";
                using (SqlConnection _Conn = new SqlConnection(processArqController.ConnectionsStringDST(pDataBase)))
                {
                    _Conn.Open();
                    Conexao.ExecutarScript(script, _Conn);
                }
            }
        }

        public static string GetRecoveryModelDesc(string pDataBase)
        {
            string Result = "";
            string script = $"select upper(recovery_model_desc) recovery_model_desc from  sys.databases where name='{pDataBase}'";
            using (SqlConnection _Conn = new SqlConnection(processArqController.ConnectionsStringDST(pDataBase)))
            {
                _Conn.Open();
                using (SqlDataReader dr = Conexao.ExecutarSelect(script, _Conn))
                {
                    if (dr.HasRows)
                    {
                        if (dr.Read())
                        {
                            Result =dr["recovery_model_desc"].ToString();
                        }
                    }
                }
            }
            return Result;
        }

        public static List<string> GetListaArquivos(string pPath, string pFilename, int pIdArquivo = 0)
        {
            List<string> ListMask = new List<string>();
            if (pPath.IndexOf("[")!=-1)
            {
                string ParentDir = pPath.Substring(0, pPath.IndexOf("["));
                pPath = pPath.Remove(0, pPath.IndexOf("[") + 1);
                int fin = pPath.IndexOf("]");
                string MaskDir = pPath.Substring(0, fin);

                string BarraFinal = "";
                if (ParentDir.Substring(ParentDir.Length - 1, 1) != "\\")
                {
                    ParentDir = ParentDir.Substring(0, ParentDir.Length - 1);
                    BarraFinal = "\\\\";
                }

                string regexMask = ParentDir.Replace("\\", "\\\\") + BarraFinal + MaskDir.Replace("\\", "\\\\").Replace("*", "[^\\\\]+") + "\\\\" + pFilename.Replace("*", ".*");

                string[] files = Directory.GetFiles(ParentDir, pFilename, SearchOption.AllDirectories);
                foreach (string file in files)
                {
                    if (Regex.IsMatch(file, regexMask, RegexOptions.IgnoreCase))
                    {
                        ListMask.Add(file);
                    }
                }
            }
            else
            {
                ListMask = BuscaArquivosSemMascara(pPath, pFilename, pIdArquivo);
            }
            return ListMask;

        }

        private static List<string> BuscaArquivosSemMascara(string dir, string NomeArquivo, int pIdArquivo = 0)
        {

            List<string> lst = new List<string>();
            string[] dirs =null;
            try
            {
                dirs = Directory.GetFiles(dir, NomeArquivo);
            }
            catch
            {
                dirs = null;
            }
            if (dirs != null)
            {
                if (dir.Substring(dir.Length - 1, 1) != "\\")
                {
                    dir += "\\";
                }

                Global.EnviarParaLog($"Lista dos arquivos encontrados com a mascara: {dir + NomeArquivo}", true, pIdArquivo);
                foreach (string FileName in dirs)
                {
                    lst.Add(FileName);
                    Global.EnviarParaLog($"{FileName}", true, pIdArquivo);
                }
            }
            return lst;
        }


    }
}
