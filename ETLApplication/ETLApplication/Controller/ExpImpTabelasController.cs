/*
	==============================================
	Criado por : Carlos R. S. Oliveira
	Criado em  : 20/05/2019
	==============================================
*/
using ETLApplication.Model;
using EtlConexao;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ETLApplication.View.frmCadastroBase;

namespace ETLApplication.Controller
{
    
    class ExpImpTabelasController
    {
        public Conexao conexao;
        public ExpImpTabelasController()
        {
            conexao = new Conexao();
        }
        public string GetScriptExportar(CheckBox pChkGrp, CheckBox pChkCat, CheckBox pChkPac, CheckBox pChkSer, CheckBox pChkArq, CheckBox pChkMap,
                                        TextBox pEdtGrp, TextBox pEdtCat, TextBox pEdtPac, TextBox pEdtSer, TextBox pEdtArq, TextBox pEdtMap)
        {
            string script = "";
            script += "select g.id_grupo Grp_Id_Grupo, g.nm_grupo,g.status Grp_Status, g.DESCR_GRUPOS,";
            script += " c.id_categoria,c.id_grupo Cat_Id_Grupo, c.nm_categoria, c.status Cat_Status, c.DESCR_CATEGORIAS,";
            script += " p.id_pacote, p.id_categoria pac_id_categoria, p.nm_pacote, p.status pac_status, p.DESCR_PACOTES,";
            script += " s.id_servico Ser_id_Servico, s.NM_SERVICO,s.status ser_status, s.Situacao,";
            script += " mo.id_arquivo Mon_id_arquivo, mo.id_servico mon_id_servico, mo.status mon_status,";
            script += " a.id_arquivo arq_idarquivo, a.id_pacote arq_id_pacote, a.nm_arquivo,a.mascara_arquivo,";
            script += " a.tp_carga,a.tp_arquivo,a.delimitador,a.cercador,a.tb_destino,a.dir_entrada,a.dir_saida ,a.rbd_tabela,a.rbd_indice ,a.nm_Planilha,a.status arq_status,a.LineFeed,a.FirstLine,a.ConexaoBusiness,";
            script += " ma.id_mapeamento,ma.id_arquivo map_id_arquivo, ma.nm_coluna,ma.ordem,ma.fixo_inicio,ma.fixo_tamanho,ma.tp_coluna,ma.tm_coluna,ma.pr_coluna,ma.MASK_CAMPO,ma.ExpressaoSql";
            script += " from tb_Grupos g";
            script += " inner join tb_categorias c on(c.id_grupo = g.id_grupo)";
            script += " left join tb_pacotes p on(c.id_categoria = p.id_categoria)";
            script += " left join tb_arquivos a on(a.id_pacote = p.id_pacote)";
            script += " left join tb_monitoramentos mo on(mo.id_arquivo = a.id_arquivo)";
            script += " left join tb_servicos s on(s.id_servico = mo.id_servico)";
            script += " left join tb_mapeamentos ma on(ma.id_arquivo = a.id_arquivo)";
            script += " inner join tb_UsuariosXcategorias uc on(uc.id_categoria = c.id_categoria)";
            script += " where 1 = 1";
            script += $"  and uc.id_usuario = {Global.UsuarioLogado.Id_usuario.ToString()}";
            //script += "   and p.status = mo.status";
            //script += "   and p.status = 'ativo'";
            script += GetScriptCondicao(pChkGrp, pEdtGrp, "g.id_grupo");
            script += GetScriptCondicao(pChkCat, pEdtCat, "c.id_categoria");
            script += GetScriptCondicao(pChkPac, pEdtPac, "p.id_pacote");
            script += GetScriptCondicao(pChkSer, pEdtSer, "s.id_servico");
            script += GetScriptCondicao(pChkArq, pEdtArq, "a.id_arquivo");
            script += GetScriptCondicao(pChkMap, pEdtMap, "ma.id_mapeamento");
            script += " order by g.id_grupo,c.id_categoria,p.id_pacote,s.id_servico,mo.id_arquivo,a.id_arquivo";
            script += " FOR XML auto,ELEMENTS";

            return script;
        }

        public string GetScriptExportacaoPerfil(CheckBox pChkPerf, CheckBox pChkUsu,TextBox pEdtPerf, TextBox pEdtUsu)
        {
            string script = "";
            script += "select pf.id_perfil Per_id_perfil, pf.nm_perfil,pf.status Per_Status,";
            script += "       u.id_usuario,u.id_perfil Usu_id_perfil, u.nm_usuario,u.login,u.senha,u.email,u.status Usu_Status,";
            script += "       o.id_objeto Obj_id_objeto, o.nm_objeto,o.tp_objeto,o.status Obj_Status,";
            script += "       pe.id_permissao, pe.id_perfil Prm_id_perfil, pe.id_objeto Prm_id_objeto, pe.permissao,pe.status Prm_Status";
            script += " from tb_perfis pf, tb_usuarios u, tb_objetos o, tb_permissoes pe";
            script += " where pf.id_perfil = u.id_perfil";
            script += " and pe.id_perfil = u.id_perfil";
            script += " and pe.id_objeto = o.id_objeto";
            script += " and pf.status = u.status";
            script += " and pe.status = o.status";
            script += " and o.status = pe.status";
            script += " and pf.status = 'ativo'";
            script += GetScriptCondicao(pChkPerf, pEdtPerf, "pf.id_perfil");
            script += GetScriptCondicao(pChkUsu, pEdtUsu, "u.id_usuario");
            script += " FOR XML auto,ELEMENTS";

            return script;

        }

        public string GetScriptExportacaoParametros(CheckBox pChkParam, TextBox pEdtParam)
        {
            string script = "";
            script += "select pa.id_parametro,pa.nm_parametro,pa.valor,pa.status";
            script += " from tb_parametros pa";
            script += " where pa.status = 'ativo'";
            script += GetScriptCondicao(pChkParam, pEdtParam, "pa.id_parametro");
            script += " FOR XML auto,ELEMENTS";

            return script;

        }

        public string GetScriptCondicao(CheckBox pChk, TextBox pEdt, string pCampo)
        {
            string Result = "";
            if (pChk.Checked && pEdt.Text != "")
            {
                Result = $"  and {pCampo} = {pEdt.Text.Trim()}";
            }
            return Result;
        }

        public DataTable GetDadosXmlParametros(string pPathXml, string pTabela)
        {
            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable(pTabela);

            try
            {
                //PARAMETROS
                dataTable.Columns.Add("id_parametro", typeof(int));
                dataTable.Columns.Add("nm_parametro", typeof(string));
                dataTable.Columns.Add("valor", typeof(string));
                dataTable.Columns.Add("status", typeof(string));

                dataSet.Tables.Add(dataTable);

                if (File.Exists(pPathXml))
                {
                    foreach (DataTable dtTable in dataSet.Tables)
                        dtTable.BeginLoadData();

                    dataSet.ReadXml(pPathXml);

                    foreach (DataTable dtTable in dataSet.Tables)
                        dtTable.EndLoadData();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


            return dataTable;
        }

        public DataTable GetDadosXmlPerfil(string pPathXml,string pTabela)
        {
            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable(pTabela);

            try
            {
                //PERFIL
                dataTable.Columns.Add("Per_id_perfil", typeof(int));
                dataTable.Columns.Add("nm_perfil", typeof(string));
                dataTable.Columns.Add("Per_Status", typeof(string));

                //USUARIOS
                dataTable.Columns.Add("id_usuario", typeof(int));
                dataTable.Columns.Add("Usu_id_perfil", typeof(int));
                dataTable.Columns.Add("nm_usuario", typeof(string));
                dataTable.Columns.Add("login", typeof(string));
                dataTable.Columns.Add("senha", typeof(string));
                dataTable.Columns.Add("email", typeof(string));
                dataTable.Columns.Add("Usu_Status", typeof(string));

                //OBJETOS
                dataTable.Columns.Add("Obj_id_objeto", typeof(int));
                dataTable.Columns.Add("nm_objeto", typeof(string));
                dataTable.Columns.Add("tp_objeto", typeof(string));
                dataTable.Columns.Add("Obj_Status", typeof(string));

                //PERMISSAO
                dataTable.Columns.Add("id_permissao", typeof(int));
                dataTable.Columns.Add("Prm_id_perfil", typeof(int));
                dataTable.Columns.Add("Prm_id_objeto", typeof(int));
                dataTable.Columns.Add("permissao", typeof(string));
                dataTable.Columns.Add("Prm_Status", typeof(string));

                dataSet.Tables.Add(dataTable);

                if (File.Exists(pPathXml))
                {
                    foreach (DataTable dtTable in dataSet.Tables)
                        dtTable.BeginLoadData();

                    dataSet.ReadXml(pPathXml);

                    foreach (DataTable dtTable in dataSet.Tables)
                        dtTable.EndLoadData();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


            return dataTable;

        }
        public DataTable GetDadosXml(string pPathXml,string pTabela)
        {
            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable(pTabela);
            try
            {
                //GRUPO
                dataTable.Columns.Add("Grp_Id_Grupo", typeof(int));
                dataTable.Columns.Add("nm_grupo", typeof(string));
                dataTable.Columns.Add("Grp_Status", typeof(string));
                dataTable.Columns.Add("descr_Grupos", typeof(string));

                //CATEGORIA
                dataTable.Columns.Add("id_categoria", typeof(int));
                dataTable.Columns.Add("Cat_Id_Grupo", typeof(int));
                dataTable.Columns.Add("nm_categoria", typeof(string));
                dataTable.Columns.Add("Cat_Status", typeof(string));
                dataTable.Columns.Add("Descr_Categorias", typeof(string));

                //PACOTE
                dataTable.Columns.Add("id_pacote", typeof(int));
                dataTable.Columns.Add("pac_id_categoria", typeof(int));
                dataTable.Columns.Add("nm_pacote", typeof(string));
                dataTable.Columns.Add("pac_status", typeof(string));
                dataTable.Columns.Add("Descr_Pacotes", typeof(string));

                //SERVICO
                dataTable.Columns.Add("Ser_id_Servico", typeof(int));
                dataTable.Columns.Add("NM_SERVICO", typeof(string));
                dataTable.Columns.Add("ser_status", typeof(string));
                dataTable.Columns.Add("Situacao", typeof(string));

                //MONITORAMENTO
                dataTable.Columns.Add("Mon_id_arquivo", typeof(int));
                dataTable.Columns.Add("mon_id_servico", typeof(int));
                dataTable.Columns.Add("mon_status", typeof(string));

                //ARQUIVO
                dataTable.Columns.Add("arq_idarquivo", typeof(int));
                dataTable.Columns.Add("arq_id_pacote", typeof(int));
                dataTable.Columns.Add("nm_arquivo", typeof(string));
                dataTable.Columns.Add("mascara_arquivo", typeof(string));
                dataTable.Columns.Add("tp_carga", typeof(string));
                dataTable.Columns.Add("tp_arquivo", typeof(string));
                dataTable.Columns.Add("delimitador", typeof(string));
                dataTable.Columns.Add("cercador", typeof(string));
                dataTable.Columns.Add("tb_destino", typeof(string));
                dataTable.Columns.Add("dir_entrada", typeof(string));
                dataTable.Columns.Add("dir_saida", typeof(string));
                dataTable.Columns.Add("rbd_tabela", typeof(string));
                dataTable.Columns.Add("rbd_indice", typeof(string));
                dataTable.Columns.Add("nm_Planilha", typeof(string));
                dataTable.Columns.Add("arq_status", typeof(string));
                dataTable.Columns.Add("LineFeed", typeof(string));
                dataTable.Columns.Add("FirstLine", typeof(string));
                dataTable.Columns.Add("ConexaoBusiness", typeof(string));

                //MAPEAMENTO
                dataTable.Columns.Add("id_mapeamento", typeof(int));
                dataTable.Columns.Add("map_id_arquivo", typeof(int));
                dataTable.Columns.Add("nm_coluna", typeof(string));
                dataTable.Columns.Add("ordem", typeof(int));
                dataTable.Columns.Add("fixo_inicio", typeof(int));
                dataTable.Columns.Add("fixo_tamanho", typeof(int));
                dataTable.Columns.Add("tp_coluna", typeof(string));
                dataTable.Columns.Add("tm_coluna", typeof(int));
                dataTable.Columns.Add("pr_coluna", typeof(int));
                dataTable.Columns.Add("MASK_CAMPO", typeof(string));
                dataTable.Columns.Add("ExpressaoSql", typeof(string));
                dataSet.Tables.Add(dataTable);

                if (File.Exists(pPathXml))
                {
                    //string xmlData = File.ReadAllText(pPathXml);
                    //System.IO.StringReader xmlSR = new System.IO.StringReader(xmlData);
                    //dataSet.ReadXml(xmlSR, XmlReadMode.IgnoreSchema);

                    foreach (DataTable dtTable in dataSet.Tables)
                        dtTable.BeginLoadData();

                    dataSet.ReadXml(pPathXml);

                    foreach (DataTable dtTable in dataSet.Tables)
                        dtTable.EndLoadData();
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }


            return dataTable;
        }


        public int ImpGrupo(GruposModel pGrupo, TextBox pTela)
        {
            int Result = 0;
            string msgRetorno = "";
//            IdGrupoAtual = pGrupo.id_grupo;
            string IdGrupo = new GruposController().GetIdGrupo(pGrupo.nm_grupo).ToString();
            int Operacao = Global.GetOperacao(IdGrupo);
            if (Operacao == 2)
            {
                pGrupo.id_grupo = Global.StrToInt(IdGrupo);
            }
            try
            {
                msgRetorno = new GruposController().CadGruposBase.PersisteNoBanco(pGrupo, Operacao);
                Global.EnviarParaLog(msgRetorno, pTela, "ImpGrupo");
                if (Operacao == 1)
                {
                    IdGrupo = new GruposController().CadGruposBase.GetLastReg(pGrupo).ToString();
                }
            }
            catch (Exception ex)
            {
                Global.EnviarParaLog($"{msgRetorno} Motivo:{ex.Message}", pTela, "ImpGrupo");
            }
            Result = Global.StrToInt(IdGrupo);
            return Result;
        }
        public int ImpCategoria(CategoriasModel pCategorias,string pIdGrupo, TextBox pTela)
        {
            int Result = 0;
            //int IdCategAtual = pCategorias.id_categoria;
            //if (pCategorias.id_grupo == pIdGrupoXml)
            //{
            string msgRetorno = "";
            string IdCateg = new CategoriasController().GetIdCategoria(pCategorias.nm_categoria).ToString();
            int Operacao = Global.GetOperacao(IdCateg);
            if (Operacao == 2)
            {
                pCategorias.id_categoria = Global.StrToInt(IdCateg);
            }
            try
            {
                pCategorias.id_grupo = Global.StrToInt(pIdGrupo);
                msgRetorno = new CategoriasController().CadCategoriasBase.PersisteNoBanco(pCategorias, Operacao);
                Global.EnviarParaLog(msgRetorno, pTela, "ImpCategoria");
                if (Operacao == 1)
                {
                    IdCateg = new CategoriasController().CadCategoriasBase.GetLastReg(pCategorias).ToString();
                }
            }
            catch (Exception ex)
            {
                Global.EnviarParaLog($"{msgRetorno} Motivo:{ex.Message}", pTela, "ImpCategoria");
            }
            //}
            Result = Global.StrToInt(IdCateg);
            return Result;
        }
        public int ImpPacote(PacotesModel pPacotes, string pIdCategoria, TextBox pTela)
        {
            int Result = 0;
            string msgRetorno = "";
            string IdPacote = new PacotesController().CadPacotesBase.GetidPacote(pPacotes.nm_pacote).ToString();
            int Operacao = Global.GetOperacao(IdPacote);
            if (Operacao == 2)
            {
                pPacotes.id_pacote = Global.StrToInt(IdPacote);
            }
            try
            {
                pPacotes.id_categoria = Global.StrToInt(pIdCategoria);
                msgRetorno = new PacotesController().CadPacotesBase.PersisteNoBanco(pPacotes, Operacao);
                Global.EnviarParaLog(msgRetorno, pTela, "ImpPacotes");
                if (Operacao == 1)
                {
                    IdPacote = new PacotesController().CadPacotesBase.GetLastReg(pPacotes).ToString();
                }
            }
            catch (Exception ex)
            {
                Global.EnviarParaLog($"{msgRetorno} Motivo:{ex.Message}", pTela, "ImpPacotes");
            }
            //}
            Result = Global.StrToInt(IdPacote);
            return Result;
        }
        public int ImpArquivos(ArquivosModel pArquivos, string pIdPacote, TextBox pTela)
        {
            int Result = 0;
            string msgRetorno = "";
            string IdArquivo = new ArquivosController().GetIdArquivo(pArquivos.nm_arquivo,pArquivos.dir_entrada).ToString();
            int Operacao = Global.GetOperacao(IdArquivo);
            if (Operacao == 2)
            {
                pArquivos.id_arquivo = Global.StrToInt(IdArquivo);
            }
            try
            {
                pArquivos.id_pacote = Global.StrToInt(pIdPacote);
                msgRetorno = new ArquivosController().CadArquivosBase.PersisteNoBanco(pArquivos, Operacao);
                Global.EnviarParaLog(msgRetorno, pTela, "ImpArquivos");
                if (Operacao == 1)
                {
                    IdArquivo = new ArquivosController().CadArquivosBase.GetLastReg(pArquivos).ToString();
                }
            }
            catch (Exception ex)
            {
                Global.EnviarParaLog($"{msgRetorno} Motivo:{ex.Message}", pTela, "ImpArquivos");
            }
            //}
            Result = Global.StrToInt(IdArquivo);
            return Result;
        }
        public int ImpMapeamentos(MapeamentosModel pMapeamentos, string pIdArquivo, TextBox pTela)
        {
            int Result = 0;
            string msgRetorno = "";
            string IdMapeamento = new MapeamentosController().GetIdMapeamento(Global.StrToInt(pIdArquivo), pMapeamentos.nm_coluna).ToString();
            int Operacao = Global.GetOperacao(IdMapeamento);
            if (Operacao == 2)
            {
                pMapeamentos.id_mapeamento = Global.StrToInt(IdMapeamento);
            }
            try
            {
                pMapeamentos.id_arquivo = Global.StrToInt(pIdArquivo);
                msgRetorno = new MapeamentosController().CadMaptoBase.PersisteNoBanco(pMapeamentos, Operacao);
                Global.EnviarParaLog(msgRetorno, pTela, "ImpMapeamentos");
                if (Operacao == 1)
                {
                    IdMapeamento = new MapeamentosController().CadMaptoBase.GetLastReg(pMapeamentos).ToString();
                }
            }
            catch (Exception ex)
            {
                Global.EnviarParaLog($"{msgRetorno} Motivo:{ex.Message}", pTela, "ImpMapeamentos");
            }
            //}
            Result = Global.StrToInt(IdMapeamento);
            return Result;
        }
        public int ImpServicos(ServicosModel pServicos, TextBox pTela)
        {
            int Result = 0;
            string msgRetorno = "";
            string IdServico = new ServicosController().CadServicosBase.GetidServico(pServicos.nm_servico).ToString();
            int Operacao = Global.GetOperacao(IdServico);
            if (Operacao == 2)
            {
                pServicos.id_servico = Global.StrToInt(IdServico);
            }
            try
            {
                msgRetorno = new ServicosController().CadServicosBase.PersisteNoBanco(pServicos, Operacao);
                Global.EnviarParaLog(msgRetorno, pTela, "ImpServicos");
                if (Operacao == 1)
                {
                    IdServico = new ServicosController().CadServicosBase.GetLastReg(pServicos).ToString();
                }
            }
            catch (Exception ex)
            {
                Global.EnviarParaLog($"{msgRetorno} Motivo:{ex.Message}", pTela, "ImpServicos");
            }
            //}
            Result = Global.StrToInt(IdServico);
            return Result;
        }
        public void ImpMonitoramentos(MonitoramentosModel pMonitoramentos, TextBox pTela)
        {
            string msgRetorno = "";
            bool MonitoramentoCad = new MonitoramentosController().MonitoramentoCadastrado(pMonitoramentos.id_servico,pMonitoramentos.id_arquivo);
            if (!MonitoramentoCad)
            {
                try
                {
                    msgRetorno = new MonitoramentosController().CadMonitoramentosBase.PersisteNoBanco(pMonitoramentos, 1);
                    Global.EnviarParaLog(msgRetorno, pTela, "ImpMonitoramento");
                }
                catch (Exception ex)
                {
                    Global.EnviarParaLog($"{msgRetorno} Motivo:{ex.Message}", pTela, "ImpMonitoramento");
                }
            }
        }
        public int ImpPerfil(PerfisModel pPerfil, TextBox pTela)
        {
            int Result = 0;
            string msgRetorno = "";
            //            IdGrupoAtual = pGrupo.id_grupo;
            string IdPerfil = new PerfisController().CadPerfisBase.GetidPerfil(pPerfil.Nm_perfil).ToString();
            int Operacao = Global.GetOperacao(IdPerfil);
            if (Operacao == 2)
            {
                pPerfil.Id_perfil = Global.StrToInt(IdPerfil);
            }
            try
            {
                msgRetorno = new PerfisController().CadPerfisBase.PersisteNoBanco(pPerfil, Operacao);
                Global.EnviarParaLog(msgRetorno, pTela, "ImpPerfil");
                if (Operacao == 1)
                {
                    IdPerfil = new PerfisController().CadPerfisBase.GetLastReg(pPerfil).ToString();
                }
            }
            catch (Exception ex)
            {
                Global.EnviarParaLog($"{msgRetorno} Motivo:{ex.Message}", pTela, "ImpPerfil");
            }
            Result = Global.StrToInt(IdPerfil);
            return Result;
        }

        public int ImpUsuarios(UsuariosModel pUsuario, TextBox pTela)
        {
            int Result = 0;
            string msgRetorno = "";

            string IdUsuario = new UsuariosController().GetIdUsuario(pUsuario.Nm_usuario).ToString();
            int Operacao = Global.GetOperacao(IdUsuario);
            if (Operacao == 2)
            {
                pUsuario.Id_usuario = Global.StrToInt(IdUsuario);
            }
            try
            {
                msgRetorno = new UsuariosController().CadUsuariosBase.PersisteNoBanco(pUsuario, Operacao);
                Global.EnviarParaLog(msgRetorno, pTela, "ImpUsuarios");
                if (Operacao == 1)
                {
                    IdUsuario = new UsuariosController().CadUsuariosBase.GetLastReg(pUsuario).ToString();
                }
            }
            catch (Exception ex)
            {
                Global.EnviarParaLog($"{msgRetorno} Motivo:{ex.Message}", pTela, "ImpUsuarios");
            }
            Result = Global.StrToInt(IdUsuario);
            return Result;
        }
        public int ImpObjeto(ObjetosModel pObjetos, TextBox pTela)
        {
            int Result = 0;
            string msgRetorno = "";

            string IdObjeto = new ObjetosController().GetIdMenu(pObjetos.Nm_objeto).ToString();
            int Operacao = Global.GetOperacao(IdObjeto);
            if (Operacao == 2)
            {
                pObjetos.Id_objeto = Global.StrToInt(IdObjeto);
            }
            try
            {
                msgRetorno = new ObjetosController().CadObjetosBase.PersisteNoBanco(pObjetos, Operacao);
                Global.EnviarParaLog(msgRetorno, pTela, "ImpObjetos");
                if (Operacao == 1)
                {
                    IdObjeto = new ObjetosController().CadObjetosBase.GetLastReg(pObjetos).ToString();
                }
            }
            catch (Exception ex)
            {
                Global.EnviarParaLog($"{msgRetorno} Motivo:{ex.Message}", pTela, "ImpObjetos");
            }
            Result = Global.StrToInt(IdObjeto);
            return Result;
        }
        public int ImpPermissoes(PermissoesModel pPermissoes, TextBox pTela)
        {
            int Result = 0;
            string msgRetorno = "";

            string IdPermissao = new PermissoesController().GetIdPermissao(pPermissoes.Id_perfil,pPermissoes.Id_objeto).ToString();
            int Operacao = Global.GetOperacao(IdPermissao);
            if (Operacao == 2)
            {
                pPermissoes.Id_permissao = Global.StrToInt(IdPermissao);
            }
            try
            {
                msgRetorno = new PermissoesController().CadPermissoesBase.PersisteNoBanco(pPermissoes, Operacao);
                Global.EnviarParaLog(msgRetorno, pTela, "ImpPermissoes");
                if (Operacao == 1)
                {
                    IdPermissao = new PermissoesController().CadPermissoesBase.GetLastReg(pPermissoes).ToString();
                }
            }
            catch (Exception ex)
            {
                Global.EnviarParaLog($"{msgRetorno} Motivo:{ex.Message}", pTela, "ImpPermissoes");
            }
            Result = Global.StrToInt(IdPermissao);
            return Result;
        }

        public int ImpParametros(ParametrosModel pParametros, TextBox pTela)
        {
            int Result = 0;
            string msgRetorno = "";

            string IdParametro = new ParametrosController().GetIdParametro(pParametros.nm_parametro).ToString();
            int Operacao = Global.GetOperacao(IdParametro);
            if (Operacao == 2)
            {
                pParametros.id_parametro= Global.StrToInt(IdParametro);
            }
            try
            {
                msgRetorno = new ParametrosController().CadParametrosBase.PersisteNoBanco(pParametros, Operacao);
                Global.EnviarParaLog(msgRetorno, pTela, "ImpParametros");
                if (Operacao == 1)
                {
                    IdParametro = new ParametrosController().CadParametrosBase.GetLastReg(pParametros).ToString();
                }
            }
            catch (Exception ex)
            {
                Global.EnviarParaLog($"{msgRetorno} Motivo:{ex.Message}", pTela, "ImpParametros");
            }
            Result = Global.StrToInt(IdParametro);
            return Result;
        }
    }
}
