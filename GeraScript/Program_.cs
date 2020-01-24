using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;


namespace GeraScript
{
    class Program
    {
        static void Main(string[] args)
        {

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
}
