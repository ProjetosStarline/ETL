using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ETLService
{
    public partial class ExecutaCarga : ServiceBase
    {
        public ExecutaCarga()
        {
            InitializeComponent();
           
        }


        protected override void OnStart(string[] args) 
        {
            Global.IdServico = "001";
            Global.NomeServico = "ExecutaCarga" + Global.IdServico;
            EnviarParaLog("Stared");
            tmrExecutaCarga.Interval = 5000; // 60 seconds
            tmrExecutaCarga.Start();
        }

        protected override void OnStop()
        {
            EnviarParaLog("Stoped");

        }

        private void EnviarParaLog(string logar,string NomeArquivoLog= "LogEtl.txt")
        {
            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            path = (Path.GetDirectoryName(path));
            string CaminhoENomeFileLog = Path.Combine(path, NomeArquivoLog);
            if (!File.Exists(CaminhoENomeFileLog))
            {
                FileStream Arquivo = File.Create(CaminhoENomeFileLog);
                Arquivo.Close();
            }
            using (StreamWriter Log = File.AppendText(CaminhoENomeFileLog))
            {
                Log.WriteLine($"{DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()} : {logar}");
            }
        }

        private void tmrExecutaCarga_Tick(object sender, EventArgs e)
        {
            EnviarParaLog("Rodando");
        }
    }
}
