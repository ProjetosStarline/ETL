/*
	==============================================
	Criado por : Carlos R. S. Oliveira
	Criado em  : 20/05/2019
	==============================================
*/
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ETLApplication.Model
{
    public class GenericModel
    {
        public string NomeTabela{ get; set; }
        public List<string> ListaCamposView = new List<string>();

        public GenericModel()
        {
            NomeTabela = "";
        }

        public virtual void SetDados(SqlDataReader dr)
        {

        }
        public virtual void SetDados(DataGridView dgv)
        {

        }

        public string isNullResultZero(string pConteudo)
        {
            return pConteudo == "" ? "0" : pConteudo;
        }
        public string isNullResultVazio(object pConteudo)
        {
            string Result = "";
            if (pConteudo == null )
            {
                Result ="";
            }
            else 
            {
                Result = pConteudo.ToString();
            }

            return Result;
        }

        public virtual void GetListaCampos()
        {
            List<string> ListaCamposView = new List<string>();
            ListaCamposView.Clear();
        }

        public virtual string GetFieldParaBusca(string pCampoView)
        {
            return "";
        }
        public virtual string GetTypeFieldParaBusca(string pCampoView)
        {
            return "";
        }

        public virtual void Clear()
        {

        }
    }
}
