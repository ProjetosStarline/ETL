/*
	==============================================
	Criado por : Carlos R. S. Oliveira
	Criado em  : 20/05/2019
	==============================================
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeraScript
{
    public class Classes    //classe generica
    {
        public string Texto { get; set; }
        public string texto2 { get; set; }
        public Classes()
        {
            Texto = "";
        }


        public virtual string SetTexto2(string txt)
        {
            return txt;
        }
    }
    public class Classes1:Classes   //Herda da classe generica
    {
        public Classes1()
        {
            Texto = "Esta é a Classe 1";
        }
    }
    public class Classes2 : Classes
    {
        public Classes2()
        {
            Texto = "Esta é a Classe 2";
            
        }
        public override string SetTexto2(string txt)
        {
            return txt+" txt2";
        }


    }
    public class Classes3 : Classes
    {
        public Classes3()
        {
            Texto = "Esta é a Classe 3";
        }

    }

}
