/*
	==============================================
	Criado por : Carlos R. S. Oliveira
	Criado em  : 20/05/2019
	==============================================
*/
using ETLApplication.Controller.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.DirectoryServices.ActiveDirectory;
using ETLApplication.Model;
using System.Reflection;
using System.Data.SqlClient;
using EtlConexao;

namespace ETLApplication.Controller
{
    
    public class CadastroBase<T> where T:class
    {
        public string ScriptDefault { get; set; }
        public Conexao conexao = new Conexao();
        public GenericModel Gmodel;

        GeraCRUD<T> geraCRUD=new GeraCRUD<T>();
        
        public CadastroBase()
        {
            Gmodel = new GenericModel();
        }

        public bool Gravar(T pObj)
        {
            string script = geraCRUD.GetInsert(pObj);
            try
            {
                conexao.ExecutarScript(script);
                return true;
            }
            catch(Exception ex)
            {
                return false;
                throw new Exception("ERRO: Não foi possível gravar na tabela ["+ex.ToString()+"]");
            }

        }

        public bool Alterar(T pObj)
        {
            string script = geraCRUD.GetUpdate(pObj);
            try
            {
                conexao.ExecutarScript(script);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception("ERRO: Não foi possível alterar na tabela [" + ex.ToString() + "]");
            }
        }
        public bool Deletar(T pObj)
        {
            string script = geraCRUD.GetDelete(pObj);
            try
            {
                conexao.ExecutarScript(script);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception("ERRO: Não foi possível deletar o registro na tabela [" + ex.ToString() + "]");
            }
        }

        public List<string> GetListaCampos(T pObj)
        {
            return geraCRUD.GetListaCampos(pObj);
        }
        public List<string> GetListaTipoDosCampos()
        {
            return geraCRUD.ListaTipoDosCampos;
        }

        public string ScriptPesquisar(string NomeTabela,string NomeCampo, string TipoCampo, string pOperacao,string ConteudoDaBusca)
        {
            string Operacao = "";
            string Condicao = ConteudoDaBusca;
            //string TipoDoCampo = ListaTipoDosCampos[cbCampos.SelectedIndex];
            string alias = "";
            if (NomeTabela.Contains("tb_permissoes"))
            {
                alias = "p.";
            }

            if (TipoCampo == "String") { Condicao = "'" + ConteudoDaBusca + "'"; }
            else
            if (TipoCampo == "DateTime")
            {
                NomeCampo = "Convert(date," + alias+NomeCampo + ",103)";
                Condicao = "Convert(date,'" + ConteudoDaBusca + "',103)";
            }
            else
            if (TipoCampo == "integer" || TipoCampo == "bool") { Condicao = ConteudoDaBusca; }

            if (pOperacao== "Igual =") { Operacao = "="; }
            else
            if (pOperacao == "Diferente <>") { Operacao = "<>"; }
            else
            if (pOperacao== "Menor que <") { Operacao = "<"; }
            else
            if (pOperacao == "Maior que >") { Operacao = ">"; }
            else
            if (pOperacao == "Menor ou Igual <=") { Operacao = "<="; }
            else
            if (pOperacao == "Maior ou Igual >=") { Operacao = ">="; }
            else 
            if (pOperacao=="Começar com")  { Operacao = "Like"; Condicao = "'" + ConteudoDaBusca + "%'"; } 
            else { Operacao = "Like"; Condicao = "'%" + ConteudoDaBusca + "%'"; }



            string script = "";
            if (string.IsNullOrEmpty(ScriptDefault))
            {
                script = "SELECT * FROM " + NomeTabela + " WHERE 1=1";
            }
            else
            {
                script = ScriptDefault;
            }

            if (ConteudoDaBusca.Length > 0)
            {
                script += " and "+ NomeCampo + " " + Operacao+" " + Condicao;
            }

            return script;
        }

        public void Pesquisar(string NomeTabela, string NomeCampo, string TipoCampo, string Operacao, string ConteudoDaBusca,DataGridView dgv,string pWhere="")
        {
            string NomeCampoOrder = NomeCampo;
            string script = ScriptPesquisar(NomeTabela, NomeCampo, TipoCampo,Operacao,ConteudoDaBusca);
            script += pWhere;
            script += " order by " + NomeCampoOrder;
            conexao.GetDadosTodos(script,dgv);
        }

        static object RetornaClasse(string nome)
        {
            var classe = Assembly.GetExecutingAssembly().GetTypes().First(x => x.Name == nome);
            var instancia = Activator.CreateInstance(classe);
            return instancia;
        }

        public string GetNomeTabela(T pObj)
        {
            return geraCRUD.GetNomeTabela(pObj);
        }

        public string PersisteNoBanco(T pObj,int Operacao)
        {
            string script = "";
            string msg="";
            string nmOperacao="";
            switch (Operacao)
            {
                case 1: script = geraCRUD.GetInsert(pObj); nmOperacao = "Inclusão"; break;
                case 2: script = geraCRUD.GetUpdate(pObj); nmOperacao = "Alteração"; break;
                case 3: script = geraCRUD.GetDelete(pObj); nmOperacao = "Exclusão"; break;
            }
            try
            {
                if (!conexao.ConexaoAberta(conexao.Conn)) { conexao.AbrirConexao(conexao.Conn); }
                if (Operacao == 1)
                {
                    try
                    {
                        Conexao.ExecutarScript(script, conexao.Conn);
                        msg = "Cadastro efetuado com sucesso.";
                    }
                    catch(Exception ex)
                    {
                        msg = ex.Message;
                    }
                }
                else
                if (Operacao == 2)
                {
                    try
                    {
                        Conexao.ExecutarScript(script, conexao.Conn);
                        string update = $"Update {geraCRUD.GetNomeTabela(pObj)} set Data_atualizacao=Getdate() where {geraCRUD.FieldPrimaryKey(pObj)}={geraCRUD.GetConteudoPrimaryKey(pObj)}";
                        Conexao.ExecutarScript(update, conexao.Conn);
                        msg = "Alteração efetuado com sucesso.";
                    }
                    catch (Exception ex)
                    {
                        msg = ex.Message;
                    }
                }
                else
                {
                    DialogResult dlg = MessageBox.Show("Deseja realmente DELETAR este registro?", "Exclusão de Registro.", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (dlg.ToString().ToUpper() == "YES")
                    {
                        conexao.ExecutarScript(script);
                        msg = "Exclusão efetuada com sucesso.";
                    }
                }
            }
            catch (Exception ex)
            {
                msg = "ERRO: Não foi possível concluir a " + nmOperacao + " ["+ex.ToString()+"]";
            }
            finally
            {
                conexao.FecharConexao(conexao.Conn);
            }
            return msg;
        }

        public bool FieldIsPK(T pObj,string pNomeCampo)
        {
            return geraCRUD.FieldPrimaryKey(pObj).ToUpper() == pNomeCampo.ToUpper();
        }

        public int GetLastReg(T pObj)
        {
            int Result = 0;
            using (SqlDataReader dr = conexao.ExecutarSelect(geraCRUD.GetLastID(pObj)))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if (dr["LstID"].ToString() != "")
                        {
                            Result = Convert.ToInt32(dr["LstID"].ToString());
                        }
                    }
                }
            }
            return Result;
        }

        public string GetNomePerfil(int pIdPerfil)
        {
            PerfisModel perfilModel = new PerfisModel();

            string script = "Select * from tb_Perfis where Id_perfil=@Id_Perfil and lower(status)='ativo'";
            conexao.LimparParametros();
            conexao.Parametros.Add(new SqlParameter() { ParameterName = "Id_Perfil", Value = pIdPerfil });
            perfilModel.SetDados(conexao.ExecutarSelect(script));
            if (perfilModel.Nm_perfil == null)
            {
                return "";
            }
            else
            {
                return perfilModel.Nm_perfil;
            }
        }

        public int GetidPerfil(string pnmPerfil)
        {
            PerfisModel perfilModel = new PerfisModel();

            string script = "Select * from tb_Perfis where nm_perfil=@nm_Perfil and lower(status)='ativo'";
            conexao.LimparParametros();
            conexao.Parametros.Add(new SqlParameter() { ParameterName = "nm_Perfil", Value = pnmPerfil });
            perfilModel.SetDados(conexao.ExecutarSelect(script));

            return perfilModel.Id_perfil;
        }
        public int GetidPacote(string pnmPacote)
        {
            PacotesModel pacotesModel = new PacotesModel();

            string script = "Select * from tb_pacotes where nm_pacote=@nm_Pacote and lower(status)='ativo'";
            conexao.LimparParametros();
            conexao.Parametros.Add(new SqlParameter() { ParameterName = "nm_Pacote", Value = pnmPacote });
            pacotesModel.SetDados(conexao.ExecutarSelect(script));

            return pacotesModel.id_pacote;
        }
        public int GetIdPacote(int pIdArquivo)
        {
            PacotesModel pacotesModel = new PacotesModel();

            string script = "Select p.* from tb_arquivos a, tb_pacotes p where a.id_pacote=p.id_pacote and a.id_arquivo=@IdArquivo and lower(a.status)='ativo'";
            conexao.LimparParametros();
            conexao.Parametros.Add(new SqlParameter() { ParameterName = "IdArquivo", Value = pIdArquivo });
            pacotesModel.SetDados(conexao.ExecutarSelect(script));

            return pacotesModel.id_pacote;
        }
        public string GetNomePacotes(int pIdPacote)
        {
            PacotesModel pacotesModel = new PacotesModel();

            string script = "Select * from tb_Pacotes where Id_Pacote=@Id_Pacote and lower(status)='ativo'";
            conexao.LimparParametros();
            conexao.Parametros.Add(new SqlParameter() { ParameterName = "Id_Pacote", Value = pIdPacote });
            pacotesModel.SetDados(conexao.ExecutarSelect(script));
            if (pacotesModel.nm_pacote == null)
            {
                return "";
            }
            else
            {
                return pacotesModel.nm_pacote;
            }
        }

        public int GetidServico(string pnmServico)
        {
            ServicosModel servicosModel = new ServicosModel();

            string script = "Select * from tb_Servicos where nm_Servico=@nm_Servico";
            conexao.LimparParametros();
            conexao.Parametros.Add(new SqlParameter() { ParameterName = "nm_Servico", Value = pnmServico });
            servicosModel.SetDados(conexao.ExecutarSelect(script));

            return servicosModel.id_servico;
        }
        public string GetNomeServico(int pIdServico)
        {
            ServicosModel servicosModel = new ServicosModel();

            string script = "Select * from tb_Servicos where Id_Servico=@Id_Servico and lower(status)='ativo'";
            conexao.LimparParametros();
            conexao.Parametros.Add(new SqlParameter() { ParameterName = "Id_Servico", Value = pIdServico });
            servicosModel.SetDados(conexao.ExecutarSelect(script));
            if (servicosModel.nm_servico == null)
            {
                return "";
            }
            else
            {
                return servicosModel.nm_servico;
            }
        }

        public void GetLista(string select,Object obj)
        {
            if(obj.GetType().Name== "ComboBox")
            {
                conexao.GetDadosTodos(select + " and lower(status)='ativo'", (ComboBox)obj);
            }
            else if (obj.GetType().Name == "DataGridView")
            {
                conexao.GetDadosTodos(select , (DataGridView)obj);
            }
        }

        public string GetSelect(T obj)
        {
            return geraCRUD.GetSelectAll(obj);
        }

        public string isNullResultZero(string pConteudo)
        {
            return pConteudo == "" ? "0" : pConteudo;
        }

        public string isNullResultZero(object pConteudo)
        {
            if (pConteudo == null || pConteudo.ToString() == "")
            {
                return "0";
            }
            else 
            {
                return pConteudo.ToString();
            }
        }
        public string isNullResultVazio(object pConteudo)
        {
            if (pConteudo == null)
            {
                return "";
            }
            else
            {
                return pConteudo.ToString();
            }
        }

        public string SelectNomeServico(int pIdArquivo)
        {
            string Result = "";
            string script = "Select s.nm_servico ";
            script += " from tb_monitoramentos mon, tb_servicos s";
            script += " where mon.id_servico = s.id_servico";
            script += $"  and mon.id_arquivo = {pIdArquivo.ToString()}";
            using (SqlDataReader dr = conexao.ExecutarSelect(script))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Result = dr["nm_servico"].ToString();
                    }
                }
            }
            return Result;
        }

        public void SetAncestral(CheckBox pCb,TextBox pEdt,int pID,int pIdFilho,GenericModel pModel)
        {
            string script = "";
            script += "select g.id_grupo,c.id_categoria,p.id_pacote,s.id_servico,a.id_arquivo,ma.id_mapeamento";
            script += " from tb_grupos g";
            script += " inner join tb_categorias c on(c.id_grupo = g.id_grupo)";
            script += " left join tb_pacotes p on(c.id_categoria = p.id_categoria)";
            script += " left join tb_arquivos a on(a.id_pacote = p.id_pacote)";
            script += " left join tb_monitoramentos mo on(mo.id_arquivo = a.id_arquivo)";
            script += " left join tb_servicos s on(s.id_servico = mo.id_servico)";
            script += " left join tb_mapeamentos ma on(ma.id_arquivo = a.id_arquivo)";
            script += " inner join tb_UsuariosXcategorias uc on(uc.id_categoria = c.id_categoria)";
            script += " where 1 = 1";
            script += $"  and uc.id_usuario = {Global.UsuarioLogado.Id_usuario.ToString()}";
            if (pID != 0)
            {
                switch (pModel.NomeTabela)
                {
                    case "tb_grupos": script += $"  and g.id_grupo = {pID.ToString()}";break;
                    case "tb_categorias": script += $"  and c.id_categoria = {pID.ToString()}"; break;
                    case "tb_pacotes": script += $"  and p.id_pacote = {pID.ToString()}"; break;
                    case "tb_servicos": script += $"  and s.id_servico = {pID.ToString()}"; break;
                    case "tb_arquivos": script += $"  and a.id_arquivo = {pID.ToString()}"; break;
                    case "tb_mapeamentos": script += $"  and ma.id_mapeamento = {pID.ToString()}"; break;
                }

            }
            if (pIdFilho != 0)
            {
                switch (pModel.NomeTabela)
                {
                    case "tb_grupos"        : script += $"  and c.id_categoria = {pIdFilho.ToString()}"; break;
                    case "tb_categorias"    : script += $"  and p.id_pacote = {pIdFilho.ToString()}"; break;
                    case "tb_pacotes"       : script += $"  and s.id_servico = {pIdFilho.ToString()}"; break;
                    case "tb_servicos"      : script += $"  and a.id_arquivo = {pIdFilho.ToString()}"; break;
                    case "tb_arquivos"      : script += $"  and ma.id_mapeamento = {pIdFilho.ToString()}"; break;
                }

            }

            pEdt.Text = "";
            using (SqlDataReader dr = conexao.ExecutarSelect(script))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        switch (pModel.NomeTabela)
                        {
                            case "tb_categorias"    : pEdt.Text = dr["id_grupo"].ToString(); break;
                            case "tb_pacotes"       : pEdt.Text = dr["id_categoria"].ToString(); break;
                            case "tb_servicos"      : pEdt.Text = dr["id_pacote"].ToString(); break;
                            case "tb_arquivos"      : pEdt.Text = dr["id_servico"].ToString(); break;
                            case "tb_mapeamentos"   : pEdt.Text = dr["id_arquivo"].ToString(); break;
                        }
                    }
                }
            }
            switch (pModel.NomeTabela)
            {
                case "tb_grupos": pCb.Checked = Global.StrToInt(pEdt.Text) > 0; break;
                case "tb_categorias": pCb.Checked = Global.StrToInt(pEdt.Text) > 0; break;
                case "tb_pacotes": pCb.Checked = Global.StrToInt(pEdt.Text) > 0; break;
                case "tb_servicos": pCb.Checked = Global.StrToInt(pEdt.Text) > 0; break;
                case "tb_arquivos": pCb.Checked = Global.StrToInt(pEdt.Text) > 0; break;
                case "tb_mapeamentos": pCb.Checked=Global.StrToInt(pEdt.Text)>0; break;
            }
            pEdt.Visible = pCb.Checked;
        }
    }
}
