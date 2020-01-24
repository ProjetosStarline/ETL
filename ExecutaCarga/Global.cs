/*
	==============================================
	Criado por : Carlos R. S. Oliveira
	Criado em  : 20/05/2019
	==============================================
*/
using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;


namespace ExecutaCarga
{
    public static class Global
    {
        public enum Behavior { FIXO, DELIMITADO }
        public enum TipoArquivo { TXT , DBF , EXCEL }

        public static string versao = "1.104";
        //public static Conexao conexaoORG = null;
        //public static Conexao conexaoDST = null;
        public static bool AutenticacaoPeloWindows = false;
        public static string DataSource = "Starline\\SQLExpress";
        public static string UserID = "sa";
        public static string Password = "Starload123";
        public static string SchemaDST = "StarBI";
        public static string SchemaORG = "StarETL";
        public static string PathInputError = "C:\\ETL\\INPUT_ERROR";
        public static string PathFormatFile = "C:\\ETL\\BCPFORMAT";
        public static int CommandTimeOut = 0;
        public static string ConnectionsString = ConfigurationManager.ConnectionStrings["DbStarLineEtl"].ToString();
        
        //public static string NomeDoProcesso = "";
        //public static string IdServico;
        //public static string NomeServico;
        //public static string NomeTabelaDST="";
        //public static bool EmLote = true;
        //public static string NomeDoPacote = "";
        //public static string PathInput = "C:\\INPUT\\";
        //public static string PathOutput = "C:\\OUTPUT\\";
        //public static string NomeDoArquivo = "";
        //public static string IdDoArquivo = "";
        //public static string NomePlanilha = "";
        //public static int Pooling_Servico = 0;
        //public static string NomeTabelaSTG = "";
        public static void EnviarParaLog(string logar,bool MostrarNoConsole=true,int pIdArquivo=0)
        {

            //string NomeArquivoLog = NomeDoProcesso+"_"+Global.IdDoArquivo+"_"+ DateTime.Now.ToString("yyyyMMdd_HHmmssmmm")+".txt";
            //string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            //path = (Path.GetDirectoryName(path));

            //string CaminhoENomeFileLog = Path.Combine(path, NomeArquivoLog);
            //if (!File.Exists(CaminhoENomeFileLog))
            //{
            //    FileStream Arquivo = File.Create(CaminhoENomeFileLog);
            //    Arquivo.Close();
            //}
            //string mensagem = $"{DateTime.Now.ToString()}: {logar}";
            //StreamWriter Log = null;
            //if (!ArquivoEmUso(CaminhoENomeFileLog))
            //{
            //    try
            //    {
            //        Log = File.AppendText(CaminhoENomeFileLog);
            //        Log.WriteLine(mensagem);
            //        Log.Close();
            //    }
            //    catch
            //    {
            //        //
            //    }
            //}
            string mensagem = $"{DateTime.Now.ToString()}: {logar}";
            if (MostrarNoConsole) { Console.WriteLine(mensagem); }

            GravarLogNobanco(logar,pIdArquivo);
        }

        public static void GravarLogNobanco(string logar,int pIdArquivo=0)
        {
            ProcessArqController procControl = new ProcessArqController();
            LogsModel logsModel = new LogsModel();
            logsModel.id_arquivo = pIdArquivo;
            logsModel.etapa = "";
            if (logar.ToUpper().IndexOf("INSERT") >= 0 || 
                logar.ToUpper().IndexOf("SELECT") >= 0 || 
                logar.ToUpper().IndexOf("UPDATE") >= 0 || 
                logar.ToUpper().IndexOf("DELETE") >= 0 ||
                logar.ToUpper().IndexOf("CREATE TABLE") >= 0 ||
                logar.ToUpper().IndexOf("DROP TABLE") >= 0)
            {
                logsModel.ComandoSql = logar;
                logsModel.Mensagem = "Comando Sql executado no Banco de Dados.";
            }
            else
            {
                logsModel.ComandoSql = "";
                logsModel.Mensagem = logar;
            }
            procControl.GravaLog(logsModel.id_arquivo, logsModel.etapa, logsModel.Mensagem, logsModel.ComandoSql);
        }

        public static void GravarLogNobanco(string logar,string pEtapa="", int pIdArquivo = 0)
        {
            ProcessArqController procControl = new ProcessArqController();
            LogsModel logsModel = new LogsModel();
            logsModel.id_arquivo = pIdArquivo;
            logsModel.etapa = "";
            if (!string.IsNullOrEmpty(pEtapa))
            {
                if (pEtapa.Length < 16)
                {
                    logsModel.etapa = pEtapa;
                }
                else
                {
                    logsModel.etapa = pEtapa.Substring(0, 15);
                }
            }
               
            if (logar.ToUpper().IndexOf("INSERT") >= 0 ||
                logar.ToUpper().IndexOf("SELECT") >= 0 ||
                logar.ToUpper().IndexOf("UPDATE") >= 0 ||
                logar.ToUpper().IndexOf("DELETE") >= 0 ||
                logar.ToUpper().IndexOf("CREATE TABLE") >= 0 ||
                logar.ToUpper().IndexOf("DROP TABLE") >= 0)
            {
                logsModel.ComandoSql = logar;
                logsModel.Mensagem = "Comando Sql executado no Banco de Dados.";
            }
            else
            {
                logsModel.ComandoSql = "";
                logsModel.Mensagem = logar;
            }
            procControl.GravaLog(logsModel.id_arquivo, logsModel.etapa, logsModel.Mensagem, logsModel.ComandoSql);
        }

        public static void EnviarParaArquivoLog(string pNomeDoArquivoLog,string pLogar)
        {
            string NomeArquivoLog ="ExecutaCarga_" + DateTime.Now.ToString("yyyyMMdd_HHmmssmmm") + ".txt";
            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            path = (Path.GetDirectoryName(path));

            string CaminhoENomeFileLog = Path.Combine(path, NomeArquivoLog);
            if (!File.Exists(CaminhoENomeFileLog))
            {
                FileStream Arquivo = File.Create(CaminhoENomeFileLog);
                Arquivo.Close();
            }
            string mensagem = $"{DateTime.Now.ToString()}: {pLogar}";
            StreamWriter Log = null;
            if (!ArquivoEmUso(CaminhoENomeFileLog))
            {
                try
                {
                    Log = File.AppendText(CaminhoENomeFileLog);
                    Log.WriteLine(mensagem);
                    Log.Close();
                }
                catch
                {
                    //
                }
            }

        }

        public static void EnviarParaLog(string logar,string pEtapa="", bool MostrarNoConsole = true, int pIdArquivo = 0)
        {

            //string NomeArquivoLog = NomeDoProcesso+"_"+Global.IdDoArquivo+"_"+ DateTime.Now.ToString("yyyyMMdd_HHmmssmmm")+".txt";
            //string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            //path = (Path.GetDirectoryName(path));

            //string CaminhoENomeFileLog = Path.Combine(path, NomeArquivoLog);
            //if (!File.Exists(CaminhoENomeFileLog))
            //{
            //    FileStream Arquivo = File.Create(CaminhoENomeFileLog);
            //    Arquivo.Close();
            //}
            //string mensagem = $"{DateTime.Now.ToString()}: {logar}";
            //StreamWriter Log = null;
            //if (!ArquivoEmUso(CaminhoENomeFileLog))
            //{
            //    try
            //    {
            //        Log = File.AppendText(CaminhoENomeFileLog);
            //        Log.WriteLine(mensagem);
            //        Log.Close();
            //    }
            //    catch
            //    {
            //        //
            //    }
            //}
            string mensagem = $"{DateTime.Now.ToString()}: {logar}";
            if (MostrarNoConsole) { Console.WriteLine(mensagem); }

            GravarLogNobanco(logar,pEtapa, pIdArquivo);
        }


        public static bool ArquivoEmUso(string caminhoArquivo)
        {
            try
            {
                FileStream fs = File.OpenWrite(caminhoArquivo);
                fs.Close();
                return false;
            }
            catch (System.IO.IOException ex)
            {
                return true;
            }
        }

        public static string RemoveAcentos(string txt)
        {
            if (string.IsNullOrEmpty(txt))
            {
                return string.Empty;
            }
            Byte[] bytes = Encoding.GetEncoding("iso-8859-8").GetBytes(txt);
            return Encoding.UTF8.GetString(bytes);
        }
        public static string Slugify(string value)
        {
            
            try
            {
                //Passar para letras minusculas 
                value = value.ToLowerInvariant();

                //Tiro todos os acentos
                value = RemoveAcentos(value);

                //Substituo todos os espaços
                value = Regex.Replace(value, @"\s", "-", RegexOptions.Compiled);

                //Removo alguns caracteres especiais, se houver
                value = Regex.Replace(value, @"[^\w\s\p{Pd}]", "", RegexOptions.Compiled);

                //Tiro os Traços do final  
                value = value.Trim('-', '_');

                //Tiro ocorrencias duplas de - ou \_ 
                value = Regex.Replace(value, @"([-_]){2,}", "$1", RegexOptions.Compiled);

                //e retorno o novo valor
                return value;
            }
            catch (Exception ex)
            {
                
                throw new Exception("Slugify", ex);
            }
        }

        public static int Strtoint(string pStr)
        {
            int Result = 0;
            if (!string.IsNullOrEmpty(pStr))
            {
                Result = Convert.ToInt32(pStr);
            }
            return Result;
        }

    }
}
