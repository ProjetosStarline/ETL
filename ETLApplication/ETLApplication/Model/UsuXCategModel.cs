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

namespace ETLApplication.Model
{
    public class UsuXCategModel
    {
        public int Id_UsuCateg { get; set; }
        public int Id_Usuario { get; set; }
        public int Id_Categoria { get; set; }
        public string Nm_Categoria { get; set; }
        public int Permitir { get; set; }
    }
}
