/*
	==============================================
	Criado por : Carlos R. S. Oliveira
	Criado em  : 20/05/2019
	==============================================
*/
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ETLApplication.Model;
using ETLApplication.Controller;
using System.Globalization;

namespace Backup
{
    class Global
    {
        public static CultureInfo Culture = new CultureInfo("pt-BR");
        public static string FORMATODATA = "dd/MM/yyyy";
        public static string FORMATOHORA = "HH:mm:ss";
        public static string FORMATODATAHORA = FORMATODATA + " " + FORMATOHORA;
        public static int CommandTimeOut = 0;
        public static string ConnectionsString = ConfigurationManager.ConnectionStrings["DbStarLineEtl"].ToString();
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
        public static void EnviarParaLog(string logar, string pEtapa = "")
        {
            string NomeArquivoLog = "LogBackup.txt";
            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            path = (Path.GetDirectoryName(path));

            string CaminhoENomeFileLog = Path.Combine(path, NomeArquivoLog);
            string mensagem = $"{DateTime.Now.ToString()}: {logar}";

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


            //if (pTela != null) { pTela.Text = File.ReadAllText(CaminhoENomeFileLog); }
            GravarLogNobanco(logar, pEtapa);
        }

        public static void GravarLogNobanco(string logar, string pEtapa = "")
        {
            LogsController logs = new LogsController();
            LogsModel logsModel = new LogsModel();
            logsModel.id_arquivo = 0;
            logsModel.etapa = pEtapa;
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
            logs.GravaLog(logsModel.id_arquivo, logsModel.etapa, logsModel.Mensagem, logsModel.ComandoSql);
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
        public static int StrToInt(string pString)
        {
            CultureInfo.CurrentCulture = Culture;
            int Result = 0;
            if (!string.IsNullOrEmpty(pString))
            {
                Result = Convert.ToInt32(pString);
            }
            return Result;
        }

        public static string DateToStr(DateTime pData)
        {
            CultureInfo.CurrentCulture = Culture;
            string Result = "";
            Result = pData.ToString(FORMATODATA);
            return Result;
        }
        public static string DateTimeToStr(DateTime pDataHora)
        {
            CultureInfo.CurrentCulture = Culture;
            string Result = "";
            Result = pDataHora.ToString(FORMATODATAHORA);
            return Result;
        }
        public static string TimeToStr(DateTime pHora)
        {
            CultureInfo.CurrentCulture = Culture;
            string Result = "";
            Result = pHora.ToString(FORMATOHORA);
            return Result;
        }
        public static DateTime StrToDateTime(string pDataTime)
        {
            CultureInfo.CurrentCulture = Culture;
            DateTime Result = DateTime.Now;
            if (!string.IsNullOrEmpty(pDataTime))
            {
                Result = Convert.ToDateTime(pDataTime);
            }
            return Result;
        }

    }
}
