/*
	==============================================
	Criado por : Carlos R. S. Oliveira
	Criado em  : 20/05/2019
	==============================================
*/
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ETLApplication.Controller.DAO
{
    public class GeraCRUD<T>
    {
        public List<string> ListaTipoDosCampos { get; set; }
        public GeraCRUD()
        {
            ListaTipoDosCampos = new List<string>();
        }

        public string GetInsert(T obj)
        {
            string Campos = "";
            string Valores = "";
            int QtdCampos = 0;
            string NomeDaTabela = "";

            Type tp = obj.GetType();
            PropertyInfo[] properties = tp.GetProperties();
            foreach (PropertyInfo ptusu in tp.GetProperties())
            {
                Object[] PropCustom = ptusu.GetCustomAttributes(true);
                string NomeCampo = ptusu.Name;
                string TipoCampo = tp.GetProperty(NomeCampo).PropertyType.Name;
                var Conteudo = ptusu.GetValue(obj);
                bool Pk = false;
                bool Identity = false;
                Pk = PropCustom.Length > 0 && AtributoExiste(PropCustom, "KeyAttribute");

                Identity = AtributoExiste(PropCustom, "DatabaseGeneratedAttribute");
                if (Identity) { Pk = false; }

                //Pega o Nome da tabela
                if (NomeCampo == "NomeTabela")
                {
                    NomeDaTabela = Conteudo.ToString();
                }
                else
                {
                    //Pega a Chave primária da tabela
                    if (Pk)
                    {
                        //TipoCampo = "Int32";
                        Conteudo = Conteudo.ToString();
                    }
                    if (!Identity)
                    {
                        if (Conteudo != null)
                        {
                            Campos = QtdCampos == 0 ? Campos + NomeCampo : Campos + "," + NomeCampo;
                            if (TipoCampo == "char" || TipoCampo == "String")
                            {
                                Valores = QtdCampos == 0 ? Valores + "'" + Conteudo + "'" : Valores + ",'" + Conteudo + "'";
                            }
                            else if (TipoCampo == "byte" || TipoCampo == "int" || TipoCampo == "Int32" || TipoCampo == "long" || TipoCampo == "Int64" ||
                                    TipoCampo == "Sbyte" || TipoCampo == "Short" || TipoCampo == "Int16" || TipoCampo == "Uint" || TipoCampo == "UInt32" || TipoCampo == "Ulong" ||
                                    TipoCampo == "UInt64" || TipoCampo == "Ushort" || TipoCampo == "UInt16" || TipoCampo == "decimal" || TipoCampo == "double" || TipoCampo == "float")
                            {
                                Valores = QtdCampos == 0 ? Valores + Conteudo : Valores + "," + Conteudo;
                            }
                            else if (TipoCampo == "DateTime")
                            {
                                string vlr = "GetDate()";
                                Valores = QtdCampos == 0 ? Valores + vlr : Valores + "," + vlr;

                            }
                            else if (TipoCampo == "bool")
                            {
                                if (Conteudo.ToString() == "True")
                                {
                                    Valores = QtdCampos == 0 ? Valores + "1" : Valores + ",1";
                                }
                                else
                                {
                                    Valores = QtdCampos == 0 ? Valores + "0" : Valores + ",0";
                                }

                            }
                        }

                        QtdCampos++;
                    }
                }

                
            }

            string script = "insert into " + NomeDaTabela + "(" + Campos + ") Values(" + Valores + ")";

            return script;
        }

        public bool AtributoExiste(Object[] PropCustom,string Atributo)
        {
            bool Result = false;
            for (int i = 0; i <= PropCustom.Length - 1; i++)
            {
                if (!string.IsNullOrEmpty(PropCustom[i].GetType().Name))
                {
                    string CNT = PropCustom[i].GetType().Name;
                    if (CNT == Atributo)
                    {
                        Result = true;
                        break;
                    }
                }
            }
            return Result;
        }
        public string GetUpdate(T obj)
        {
            string Valores = "";
            string ChavePrimaria="";
            int QtdCampos = 0;
            string VlrChvPrimaria = "";
            string NomeDaTabela = "";

            Type tp = obj.GetType();
            PropertyInfo[] properties = tp.GetProperties();
            foreach (PropertyInfo ptusu in tp.GetProperties())
            {
                Object[] PropCustom = ptusu.GetCustomAttributes(true);
                string NomeCampo = ptusu.Name;
                string TipoCampo = tp.GetProperty(NomeCampo).PropertyType.Name;
                var Conteudo = ptusu.GetValue(obj);
                bool Pk = false;
                bool Identity = false;
                Pk = PropCustom.Length > 0 && AtributoExiste(PropCustom, "KeyAttribute"); 

                Identity = AtributoExiste(PropCustom, "DatabaseGeneratedAttribute");
                //if (Identity) { Pk = false; }

                //Pega o Nome da tabela
                if (NomeCampo == "NomeTabela")
                {
                    NomeDaTabela = Conteudo.ToString();
                }
                else
                {
                    //Pega a Chave primária da tabela
                    //if (NomeCampo == "ID_Registro")
                    if (Pk)
                    {
                        ChavePrimaria = NomeCampo;
                        //TipoCampo = "Int32";
                        Conteudo = Conteudo.ToString();
                        VlrChvPrimaria = Conteudo.ToString();
                        if (TipoCampo == "char" || TipoCampo == "String")
                        {
                            VlrChvPrimaria = "'"+Conteudo.ToString()+"'";

                        }
                    }
                    else
                    {
                        if (!Identity)
                        {
                            if (Conteudo != null)
                            {
                                if (TipoCampo == "char" || TipoCampo == "String")
                                {
                                    Valores = QtdCampos == 0 ? Valores + NomeCampo + "='" + Conteudo + "'" : Valores + "," + NomeCampo + "='" + Conteudo + "'";
                                }
                                else if (TipoCampo == "byte" || TipoCampo == "int" || TipoCampo == "Int32" || TipoCampo == "long" || TipoCampo == "Int64" ||
                                        TipoCampo == "Sbyte" || TipoCampo == "Short" || TipoCampo == "Int16" || TipoCampo == "Uint" || TipoCampo == "UInt32" || TipoCampo == "Ulong" ||
                                        TipoCampo == "UInt64" || TipoCampo == "Ushort" || TipoCampo == "UInt16" || TipoCampo == "decimal" || TipoCampo == "double" || TipoCampo == "float")
                                {
                                    Valores = QtdCampos == 0 ? Valores + NomeCampo + "=" + Conteudo : Valores + "," + NomeCampo + "=" + Conteudo;
                                }
                                else if (TipoCampo == "DateTime")
                                {
                                    string vlr = "=GetDate()";
                                    Valores = QtdCampos == 0 ? Valores + NomeCampo + vlr : Valores + "," + NomeCampo + vlr;
                                }
                                else if (TipoCampo == "bool")
                                {
                                    if (Conteudo.ToString() == "True")
                                    {
                                        Valores = QtdCampos == 0 ? Valores + NomeCampo + "=1" : Valores + "," + NomeCampo + "=1";
                                    }
                                    else
                                    {
                                        Valores = QtdCampos == 0 ? Valores + NomeCampo + "=0" : Valores + "," + NomeCampo + "=0";
                                    }
                                }
                            }
                            QtdCampos++;
                        }
                    }
                }

                
            }

            
            string script = "update " + NomeDaTabela + " set " + Valores + " where " + ChavePrimaria + "="+ VlrChvPrimaria;

            return script;
        }

        public string GetDelete(T obj)
        {
            string ChavePrimaria = "";
            string VlrChvPrimaria = "";
            string NomeDaTabela = "";

            Type tp = obj.GetType();
            PropertyInfo[] properties = tp.GetProperties();
            foreach (PropertyInfo ptusu in tp.GetProperties())
            {
                Object[] PropCustom = ptusu.GetCustomAttributes(true);
                string NomeCampo = ptusu.Name;
                string TipoCampo = tp.GetProperty(NomeCampo).PropertyType.Name;
                var Conteudo = ptusu.GetValue(obj);
                bool Pk = PropCustom.Length > 0 && AtributoExiste(PropCustom, "KeyAttribute");

                //Pega o Nome da tabela
                if (NomeCampo == "NomeTabela")
                {
                    NomeDaTabela = Conteudo.ToString();
                }
                else
                {
                    //Pega a Chave primária da tabela
                    //if (NomeCampo == "ID_Registro")
                    if (Pk)
                    {
                        ChavePrimaria = NomeCampo;
                        //TipoCampo = "Int32";
                        Conteudo = Conteudo.ToString();
                        VlrChvPrimaria = Conteudo.ToString();
                        if (TipoCampo == "char" || TipoCampo == "String")
                        {
                            VlrChvPrimaria = "'" + Conteudo.ToString() + "'";

                        }
                    }
                }
           }

            string script = "delete from " + NomeDaTabela + " where " + ChavePrimaria + "=" + VlrChvPrimaria;

            return script;
        }

        public string GetSelectPorId(T obj)
        {
            string ChavePrimaria = "";
            string VlrChvPrimaria = "";
            string NomeDaTabela = "";

            Type tp = obj.GetType();
            PropertyInfo[] properties = tp.GetProperties();
            foreach (PropertyInfo ptusu in tp.GetProperties())
            {
                Object[] PropCustom = ptusu.GetCustomAttributes(true);
                string NomeCampo = ptusu.Name;
                string TipoCampo = tp.GetProperty(NomeCampo).PropertyType.Name;
                var Conteudo = ptusu.GetValue(obj);
                bool Pk = PropCustom.Length > 0 && AtributoExiste(PropCustom, "KeyAttribute");

                //Pega o Nome da tabela
                if (NomeCampo == "NomeTabela")
                {
                    NomeDaTabela = Conteudo.ToString();
                }
                else
                {
                    //Pega a Chave primária da tabela
                    //if (NomeCampo == "ID_Registro")
                    if (Pk)
                    {
                        ChavePrimaria = NomeCampo;
                        //TipoCampo = "Int32";
                        Conteudo = Conteudo.ToString();
                        VlrChvPrimaria = Conteudo.ToString();
                        if (TipoCampo == "char" || TipoCampo == "String")
                        {
                            VlrChvPrimaria = "'" + Conteudo.ToString() + "'";

                        }
                    }
                }

            }

            string script = "Select * from " + NomeDaTabela + " where " + ChavePrimaria + "=" + VlrChvPrimaria;

            return script;
        }
        public string GetNomeTabela(T obj)
        { 
            Type tp = obj.GetType();
            foreach (PropertyInfo ptusu in tp.GetProperties())
            {
                Object[] PropCustom = ptusu.GetCustomAttributes(true);
                //Pega o Nome da tabela
                if (ptusu.Name == "NomeTabela")
                {
                    return ptusu.GetValue(obj).ToString();
                }
            }
            return null;
        }

        public string GetSelectAll(T obj)
        {
            string NomeDaTabela = "";

            Type tp = obj.GetType();
            PropertyInfo[] properties = tp.GetProperties();
            foreach (PropertyInfo ptusu in tp.GetProperties())
            {
                Object[] PropCustom = ptusu.GetCustomAttributes(true);
                string NomeCampo = ptusu.Name;
                string TipoCampo = tp.GetProperty(NomeCampo).PropertyType.Name;
                var Conteudo = ptusu.GetValue(obj);

                //Pega o Nome da tabela
                if (NomeCampo == "NomeTabela")
                {
                    NomeDaTabela = Conteudo.ToString();
                }

            }

            string script = "Select * from " + NomeDaTabela + " where 1=1";

            return script;
        }
        public string GetLastID(T obj)
        {
            string ChavePrimaria = "";
            string VlrChvPrimaria = "";
            string NomeDaTabela = "";

            Type tp = obj.GetType();
            PropertyInfo[] properties = tp.GetProperties();
            foreach (PropertyInfo ptusu in tp.GetProperties())
            {
                Object[] PropCustom = ptusu.GetCustomAttributes(true);
                string NomeCampo = ptusu.Name;
                string TipoCampo = tp.GetProperty(NomeCampo).PropertyType.Name;
                var Conteudo = ptusu.GetValue(obj);
                bool Pk = PropCustom.Length > 0 && AtributoExiste(PropCustom, "KeyAttribute");

                //Pega o Nome da tabela
                if (NomeCampo == "NomeTabela")
                {
                    NomeDaTabela = Conteudo.ToString();
                }
                else
                {
                    //Pega a Chave primária da tabela
                    //if (NomeCampo == "ID_Registro")
                    if (Pk)
                    {
                        ChavePrimaria = NomeCampo;
                        //TipoCampo = "Int32";
                        Conteudo = Conteudo.ToString();
                        VlrChvPrimaria = Conteudo.ToString();
                        if (TipoCampo == "char" || TipoCampo == "String")
                        {
                            VlrChvPrimaria = "'" + Conteudo.ToString() + "'";

                        }
                    }
                }

            }
            string script = "Select Max("+ChavePrimaria+") LstID from " + NomeDaTabela;

            return script;
        }

        public List<string> GetListaCampos(T obj)
        {
            List<string> ListaCampos = new List<string>();
            string NomeDaTabela = "";
            ListaTipoDosCampos.Clear();

            Type tp = obj.GetType();
            PropertyInfo[] properties = tp.GetProperties();
            foreach (PropertyInfo ptusu in tp.GetProperties())
            {
                Object[] PropCustom = ptusu.GetCustomAttributes(true);
                string NomeCampo = ptusu.Name;
                string TipoCampo = tp.GetProperty(NomeCampo).PropertyType.Name;
                var Conteudo = ptusu.GetValue(obj);

                //Pega o Nome da tabela
                if (NomeCampo == "NomeTabela")
                {
                    NomeDaTabela = Conteudo.ToString();
                }
                else
                {
                    ListaCampos.Add(NomeCampo);
                    if (TipoCampo == "char" || TipoCampo == "String")
                    {
                        ListaTipoDosCampos.Add("String");
                    }
                    else if (TipoCampo == "byte" || TipoCampo == "int" || TipoCampo == "Int32" || TipoCampo == "long" || TipoCampo == "Int64" ||
                            TipoCampo == "Sbyte" || TipoCampo == "Short" || TipoCampo == "Int16" || TipoCampo == "Uint" || TipoCampo == "UInt32" || TipoCampo == "Ulong" ||
                            TipoCampo == "UInt64" || TipoCampo == "Ushort" || TipoCampo == "UInt16" || TipoCampo == "decimal" || TipoCampo == "double" || TipoCampo == "float")
                    {
                        ListaTipoDosCampos.Add("integer");
                    }
                    else if (TipoCampo == "DateTime")
                    {
                        ListaTipoDosCampos.Add("DateTime");
                    }
                    else if (TipoCampo == "bool")
                    {
                        ListaTipoDosCampos.Add("bool");
                    }
                }
            }
            return ListaCampos;
        }

        public string FieldPrimaryKey(T obj)
        {
            string NomeCampo = "";
            Type tp = obj.GetType();
            PropertyInfo[] properties = tp.GetProperties();
            foreach (PropertyInfo ptusu in tp.GetProperties())
            {
                NomeCampo = ptusu.Name;
                Object[] PropCustom = ptusu.GetCustomAttributes(true);
                if (PropCustom.Length > 0 && AtributoExiste(PropCustom, "KeyAttribute"))
                {
                    return NomeCampo;
                }
            }

            return "";
        }

        public string GetConteudoPrimaryKey(T obj)
        {
            string VlrChvPrimaria = "";

            Type tp = obj.GetType();
            PropertyInfo[] properties = tp.GetProperties();
            foreach (PropertyInfo ptusu in tp.GetProperties())
            {
                Object[] PropCustom = ptusu.GetCustomAttributes(true);
                string NomeCampo = ptusu.Name;
                string TipoCampo = tp.GetProperty(NomeCampo).PropertyType.Name;
                var Conteudo = ptusu.GetValue(obj);
                bool Pk = PropCustom.Length > 0 && AtributoExiste(PropCustom, "KeyAttribute");

                if (Pk)
                {
                    //TipoCampo = "Int32";
                    Conteudo = Conteudo.ToString();
                    VlrChvPrimaria = Conteudo.ToString();
                    if (TipoCampo == "char" || TipoCampo == "String")
                    {
                        VlrChvPrimaria = "'" + Conteudo.ToString() + "'";

                    }
                }

            }
            return VlrChvPrimaria;
        }


    }
}