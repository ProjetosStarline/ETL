/*
	==============================================
	Criado por : Carlos R. S. Oliveira
	Criado em  : 20/05/2019
	==============================================
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using ExecutaCarga;

namespace GeraScript
{
    class Program
    {
        static void Main(string[] args)
        {

            //if (args[0] == "--GeraScript")
            //{
            //    string NomeServico = $"GeraScript_{args[1].Substring(0, args[1].IndexOf("."))}";
            //    //Global.EnviarParaLog($"--GeraScript versão: {Global.versao}");
            //    string IdServico = "0";
            //    ProcessExecutaCarga processExecutaCarga = new ProcessExecutaCarga();
            //    EstruturaDbf EstructDbf = new EstruturaDbf();
            //    EstructDbf.GetEstruturaDbf(args[1], args[2]);
            //}
            //else
            //{

            //string parentDirectoryName = "C:\\Temp\\carlos\\";
            //string MascaraDirectoryName = "Mes *";
            //string DirectoryWithMask = "C:\\Temp\\carlos\\Versao 201*\\[Pasta 11-*]";
            //string Filename = "classe*.txt";
            //List<string> lsMask = GetListMask(DirectoryWithMask);
            //foreach (var LstMask in lsMask)
            //{
            //    MascaraDirectoryName = LstMask;
            //    string[] files = Directory.GetDirectories(parentDirectoryName, MascaraDirectoryName, SearchOption.AllDirectories);
            //    foreach (string file in files)
            //    {
            //        string[] Files = Directory.GetFiles(file, Filename, SearchOption.AllDirectories);
            //        foreach (string f in Files)
            //        {
            //            Console.WriteLine(f);
            //        }
            //    }

            /*
            Console.WriteLine("Start");

            string baseDir = "C:\\Temp\\carlos\\";
            string dirMask = "Versao *\\Pasta 1*";
            string fileMask = "classe*.txt";


            string regexMask = baseDir.Replace("\\", "\\\\") + "\\\\" + dirMask.Replace("\\", "\\\\").Replace("*", ".*") + "\\\\" + fileMask.Replace("*", ".*");

            List<string> result = new List<string>();
            string[] files = Directory.GetFiles(baseDir, fileMask, SearchOption.AllDirectories);
            foreach (string file in files)
            {
                if (Regex.IsMatch(file, regexMask, RegexOptions.IgnoreCase))
                {
                    Console.WriteLine(file);
                }
            }

            Console.WriteLine("=======================================================");
            dirMask = "[Versao 201*\\Pasta 1*]";
            foreach (var list in GetListaArquivos(baseDir+ dirMask, fileMask))
            {
                Console.WriteLine(list);
            }
            Console.WriteLine("End");

            Console.ReadKey();
            */

            string NomeClasse = "GeraScript.Classes2";

            Classes cls1 = GetInstance(NomeClasse);
            Console.WriteLine("=======================================================");
            cls1.texto2 = cls1.SetTexto2("Texto especifico da classe3");
            Console.WriteLine(cls1.texto2);

            Console.ReadKey();


        }


        public static Classes GetInstance(string strFullyQualifiedName)
        {
            Type t = Type.GetType( strFullyQualifiedName);
            if (t != null)
            {
                return (Classes)Activator.CreateInstance(t);
            }
            else
            {
                return null;
            }
        }

        public static List<string> GetListMask(string pPath)
        {
            List<string> ListMask = new List<string>();
            string ParentDir = pPath.Substring(0, pPath.IndexOf("[") - 1);
            string _Mask=pPath.Remove(0, pPath.IndexOf("[")+1);
            while (_Mask.Length > 0)
            {
                string Mask = "";
                if (_Mask.IndexOf(";") != -1)
                {
                    Mask = _Mask.Substring(0, _Mask.IndexOf(";"));
                    ListMask.Add(Mask);
                    _Mask = _Mask.Remove(0, _Mask.IndexOf(";")+1);
                }
                else
                {
                    Mask = _Mask.Substring(0, _Mask.IndexOf("]"));
                    ListMask.Add(Mask);
                    _Mask = "";
                }
            }
            return ListMask;
        }

        public static List<string> GetListaArquivos(string pPath,string pFilename)
        {
            List<string> ListMask = new List<string>();
            string ParentDir = pPath.Substring(0, pPath.IndexOf("["));
            pPath= pPath.Remove(0, pPath.IndexOf("[")+1);
            int fin = pPath.IndexOf("]");
            string MaskDir = pPath.Substring(0,fin);

            string BarraFinal = "";
            if (pPath.Substring(ParentDir.Length-1, 1) != "\\")
            {
                ParentDir = ParentDir.Substring(0, ParentDir.Length - 1);
                BarraFinal = "\\\\";
            }

            string regexMask = ParentDir.Replace("\\", "\\\\") + BarraFinal + MaskDir.Replace("\\", "\\\\").Replace("*", ".*") + "\\\\" + pFilename.Replace("*", ".*");

            string[] files = Directory.GetFiles(ParentDir, pFilename, SearchOption.AllDirectories);
            foreach (string file in files)
            {
                if (Regex.IsMatch(file, regexMask, RegexOptions.IgnoreCase))
                {
                    ListMask.Add(file);
                }
            }
            return ListMask;

        }

    }
}
