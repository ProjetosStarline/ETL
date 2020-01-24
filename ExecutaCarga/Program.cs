/*
	==============================================
	Criado por : Carlos R. S. Oliveira
	Criado em  : 20/05/2019
	==============================================
*/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace ExecutaCarga
{
    class Program
    {
        public static object Enviroment { get; private set; }

        static void Main()
        {
            
            if (GetProcessosExecutando() == 0)
            {
                ProcessExecutaCarga processExecutaCarga = new ProcessExecutaCarga();
                processExecutaCarga.processArqController.AtualizaListaServicosComErro();
                while (true)
                {
                    List <ServicosModel> LstServicos = processExecutaCarga.processArqController.GetListaServicos();
                    for (int i = 0; i <= LstServicos.Count - 1; i++)
                    {
                        string Codservico = LstServicos[i].id_servico.ToString();
                        
                        Global.EnviarParaLog($"ProcessarServico({Codservico})", "Main");
                        new Thread(() => ProcessarServico(Codservico)).Start();
                    }
                    //processExecutaCarga.processArqController.OrquetraBackup(Global.SchemaORG);
                    //processExecutaCarga.processArqController.OrquetraBackup(Global.SchemaDST);
                    processExecutaCarga.processArqController.OrquestraExpurgoLog();
                    processExecutaCarga.processArqController.OrquestraExpurgoArq();

                    int Pooling_Servico = processExecutaCarga.GetIntervaloProcessamento();
                    Thread.Sleep(Pooling_Servico * 1000);
                }
            }
            else
            {
                Global.EnviarParaLog($"ExecutaCarga já está sendo executado ", "Main");
                Environment.Exit(0);
            }

        }

        static void ProcessarServico(string pServico)
        {
            ProcessArqController procArqController = new ProcessArqController();
            try
            {
                procArqController.AtualizaSituacaoThread(pServico, "Em Execução");
                ProcessExecutaCarga processExecutaCarga = new ProcessExecutaCarga();
                Global.EnviarParaLog($"ExecutaCarga: {pServico} versão: {Global.versao}", "ProcessarServico");
                Global.EnviarParaLog($"INICIO DO PROCESSAMENTO DO SERVIÇO {pServico}", "ProcessarServico");
                processExecutaCarga.OrquestraProcessamento(pServico);
            }
            finally
            {
                procArqController.AtualizaSituacaoThread(pServico, "Ocioso");
            }
        }

        static int GetProcessosExecutando()
        {
            int _ProcessExecutando = 0;
            Process[] processo = Process.GetProcessesByName("ExecutaCarga");

            for (var i = 0; i <= processo.Length - 1; i++)
            {
                if (processo[i].ProcessName == "ExecutaCarga")
                {
                    _ProcessExecutando = i;
                }
            }

            return _ProcessExecutando;
        }
    }
    
}
