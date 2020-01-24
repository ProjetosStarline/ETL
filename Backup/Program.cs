/*
	==============================================
	Criado por : Carlos R. S. Oliveira
	Criado em  : 20/05/2019
	==============================================
*/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Backup
{
    class Program
    {
        
        static void Main(string[] args)
        {
            BackupController backup = new BackupController();
            Process[] processo = Process.GetProcessesByName("Backup");
            int ID = 0;
            for (int i=0;i <= processo.Length - 1; i++)
            {
                if (backup.BackuCadastrado(processo[i].Id) == 0)
                {
                    ID = processo[i].Id;
                }
            }

            if (ID > 0)
            {
                backup.CadastrarBackup(ID);
            }

            DerrubaProcessosExecutando(processo,ID);

            //for (int i = 0; i <= 100000000; i++)
            //{
            //    Console.WriteLine($"escrevendo no console da aplicação, linha: {i} ID: {ID.ToString()}");
            //}
            backup.OrquetraBackup();
            backup.BackupConcluido(ID);
        }
        public static void DerrubaProcessosExecutando(Process[] processo,int idProc)
        {
            for (var i = 0; i <= processo.Length - 1; i++)
            {
                if (processo[i].ProcessName == "Backup" && idProc != processo[i].Id)
                {
                    processo[i].Kill();
                }
            }
        }
    }
}
