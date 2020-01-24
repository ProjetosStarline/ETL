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

namespace Facade
{
    public static class Global
    {
        public static int StrToInt(string pStr)
        {
            if (!string.IsNullOrEmpty(pStr))
            {
                return Convert.ToInt32(pStr);
            }
            else
            {
                return 0;
            }
        }
    }
}
