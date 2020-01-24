/*
	==============================================
	Criado por : Carlos R. S. Oliveira
	Criado em  : 20/05/2019
	==============================================
*/
using ETLApplication.Controller;
using ETLApplication.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ETLApplication
{
    public static class Global
    {
        public static CultureInfo Culture = new CultureInfo("pt-BR");
        public static string versao = "1.0.0068";
        public static LoginModel Login = new LoginModel();
        public static bool UsuarioSuper = false;
        public static int CommandTimeOut = 0;
        public static bool AutenticacaoPeloWindows = false;
        public static string DataSource = "Starline\\SQLExpress";
        public static string UserID = "sa";
        public static string Password = "Starline123";
        public static string Schema = "StarETL";
        public static string Condicao = "";
        public static int CodGrupoSel = 0;
        public static int CodCategSel = 0;
        public static string TelaAtiva = "";
        public static string FORMATODATA = "dd/MM/yyyy";
        public static string FORMATOHORA = "HH:mm:ss";
        public static string FORMATODATAHORA = FORMATODATA + " " + FORMATOHORA;
        public static DateTime DateTimeNow = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        public static UsuariosModel UsuarioLogado=new UsuariosModel();
        public static int MostrarLogIDArquivo = 0;

        public static string LastCaracterDifereDeBarra(string pTxt)
        {
            string txt = pTxt;
            if (txt.Length > 1)
            {
                if (txt.Substring(txt.Length - 1, 1) != @"\")
                {
                    txt= pTxt+@"\";
                }
            }
            return txt;
        }
        public static void EnviarParaLog(string logar, TextBox pTela, string pEtapa="")
        {
            string NomeArquivoLog = "Log.txt";
            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            path = (Path.GetDirectoryName(path));

            string CaminhoENomeFileLog = Path.Combine(path, NomeArquivoLog);
            string mensagem = $"{DateTime.Now.ToString(FORMATODATAHORA)}: {logar}";

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


            if (pTela!=null) { pTela.Text=File.ReadAllText(CaminhoENomeFileLog); }
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
                Result= Convert.ToDateTime(pDataTime);
            }
            return Result;
        }

        public static int GetOperacao(string pConteudo)
        {
            if (Global.StrToInt(pConteudo) > 0)
            {
                return 2;
            }
            else
            {
                return 1;
            }
        }
        public static bool IsNullOrEmpty(object pObj)
        {
            bool Result = false;
            if (pObj == null)
            {
                Result = true;
            }
            else if (string.IsNullOrEmpty(pObj.ToString()))
            {
                Result = true;
            }
            return Result;
        }
    }
}
