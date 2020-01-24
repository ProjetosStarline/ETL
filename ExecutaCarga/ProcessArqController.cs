//==============================================
//Classe     : ClassStarLine
//Descrição  : Metodos para ser utilizados na automação de teste
//Criado por : Carlos R. S. Oliveira
//Criado em  : 20/04/2019
//==============================================
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EtlConexao;
using ETLApplication.Controller;

namespace ExecutaCarga
{
    public class ProcessArqController
    {

        public ProcessArqController()
        {
        }
        public int GetQtdReg(int pIdArquivo)
        {
            int qtd = 0;
            SqlConnection _Conn = new SqlConnection(Global.ConnectionsString);
            SqlDataReader dr = null;
            try
            {
                _Conn.Open();
                string select = $"select max(ordem) LastOrdem from tb_mapeamentos where id_arquivo={pIdArquivo.ToString()} ";
                SqlCommand cmd = new SqlCommand(select, _Conn);
                cmd.CommandTimeout = Global.CommandTimeOut;
                dr = cmd.ExecuteReader();
                if (dr.HasRows && dr.Read())
                {
                    qtd = Convert.ToInt32(dr["LastOrdem"].ToString());
                }
            }
            finally
            {
                dr.Close();
                _Conn.Close();
            }
            return qtd;
        }
        public List<MapeamentosModel> Mapeamentos(int pIdArquivo)
        {
            SqlConnection _Conn = new SqlConnection(Global.ConnectionsString);
            SqlCommand cmd = null;
            List<MapeamentosModel> lst = null;
            try
            {
                _Conn.Open();
                cmd = new SqlCommand($"select * from tb_mapeamentos where id_arquivo = {pIdArquivo.ToString()} order by ordem", _Conn);
                cmd.CommandTimeout = Global.CommandTimeOut;
                lst = new MapeamentosModel().LstMapeamento(cmd.ExecuteReader());
            }
            finally
            {
                _Conn.Close();
            }
            return lst;
        }

        public void GeraBackup(string pBancoDados,string pNomePathBackup,bool pBaseORG=true)
        {
            string PathBackup = pNomePathBackup + "\\" + pBancoDados;
            if (!Directory.Exists(PathBackup))
            {
                Directory.CreateDirectory(PathBackup);
            }
            string FileName = $"BKP_{DateTime.Now.ToString("yyyyMMdd_hhmmss")}";

            string script = "";
            script += $"BACKUP DATABASE {pBancoDados}";
            script += $"TO DISK = '{PathBackup}\\{FileName}.bak'";
            script += "WITH ";//FORMAT,  serve para formatar a media caso esteja usando pela primeira vez
            script += $"MEDIANAME = '{Global.Slugify(PathBackup)}',";
            script += $"NAME = 'Full Backup of {pBancoDados}';";

            SqlConnection _Conn;
            if (pBaseORG)
            {
                _Conn = new SqlConnection(Global.ConnectionsString);
            }
            else
            {
                _Conn = new SqlConnection(ConnectionsStringDST());
            }
            SqlCommand cmd = null;
            try
            {
                _Conn.Open();
                cmd = new SqlCommand(script, _Conn);
                cmd.CommandTimeout = Global.CommandTimeOut;
                cmd.ExecuteNonQuery();
                Global.EnviarParaLog($"Backup {pBancoDados} criado com sucesso.", "GeraBackup");
            }
            catch(Exception ex)
            {
                Global.EnviarParaLog($"Erro: Não foi possível criar o Backup {pBancoDados}. Motivo {ex.Message}", "GeraBackup");
            }
            finally
            {
                _Conn.Close();
            }

        }

        public bool BackupGerado(string pBancoDados)
        {
            bool Result = false;
            string script = "select Mensage from tb_logs";
            script += $" where CONVERT(datetime, data, 103) >= CONVERT(datetime, '{DateTime.Now.ToString("dd/mm/yyyy")} 00:00:00', 103)";
            script += $"   and CONVERT(datetime, data, 103) <= CONVERT(datetime, '{DateTime.Now.ToString("dd/mm/yyyy")} 23:59:59', 103)";
            script += $"   and upper(Mensagem)= upper('Backup {pBancoDados} criado com sucesso.')";
            SqlConnection _Conn = new SqlConnection(Global.ConnectionsString);
            SqlCommand cmd = null;
            try
            {
                _Conn.Open();
                cmd = new SqlCommand(script, _Conn);
                cmd.CommandTimeout = Global.CommandTimeOut;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Result = dr["Mensage"].ToString() != "";
                    }
                }
            }
            finally
            {
                _Conn.Close();
            }
            return Result;
        }
        public void OrquetraBackup(string pBancoDados)
        {
            ParametrosController parametros = new ParametrosController();
            int CodParametro = parametros.GetIdParametro("Data_Hora_Backup(dd/mm/yyyy hh:mm:ss)");
            if (CodParametro == 0)
            {
                parametros.GravaNovoParametro("Data_Hora_Backup(dd/mm/yyyy hh:mm:ss)", "03:00:00");
            }
            CodParametro = parametros.GetIdParametro("Path_Backup_StarEtl");
            if (CodParametro == 0)
            {
                parametros.GravaNovoParametro("Path_Backup_StarEtl", @"C:\StarEtl\");
            }
            CodParametro = parametros.GetIdParametro("Path_Backup_StarBI");
            if (CodParametro == 0)
            {
                parametros.GravaNovoParametro("Path_Backup_StarBI", @"C:\StarBI\");
            }

            bool Backpgerado = BackupGerado(pBancoDados);
            DateTime DataHoraDoBackup = Convert.ToDateTime(parametros.GetValorParametro("Data_Hora_Backup(dd/mm/yyyy hh:mm:ss)").ToString());

            if (DateTime.Now >= DataHoraDoBackup && !Backpgerado)
            {
                string Pathbackup = parametros.GetValorParametro("Path_Backup_StarEtl");
                if (Pathbackup.Length > 0)
                {
                    GeraBackup(pBancoDados, Pathbackup);
                }

                Pathbackup = parametros.GetValorParametro("Path_Backup_StarBI");
                if (Pathbackup.Length > 0)
                {
                    GeraBackup(pBancoDados, Pathbackup,false);
                }
            }

        }
        public void OrquestraExpurgoLog()
        {
            ParametrosController parametros = new ParametrosController();
            int QtdeDiasExpurgoLog = parametros.GetIdParametro("QtdeDiasExpurgoLog");
            if (QtdeDiasExpurgoLog == 0)
            {
                parametros.GravaNovoParametro("QtdeDiasExpurgoLog", "30");
            }
            QtdeDiasExpurgoLog = Convert.ToInt32(parametros.CadParametrosBase.isNullResultZero(parametros.GetValorParametro("QtdeDiasExpurgoLog")));

            DateTime DataExpurgoLog = DateTime.Now.AddDays(QtdeDiasExpurgoLog*(-1));
            if (QtdArqParaExpurgo(DataExpurgoLog) > 0)
            {
                string script = $"Delete from tb_logs where CONVERT(datetime,data,103)<=CONVERT(datetime,'{DataExpurgoLog.ToString("dd/MM/yyyy")} 23:59:59',103)";
                try
                {

                    Global.EnviarParaLog($"Script Expurgo de Log a ser executado: {script}", "OrquestraExpurgoLog");
                    parametros.CadParametrosBase.conexao.ExecutarScript(script);
                    Global.EnviarParaLog("Expurgo de Log concluído com sucesso.", "OrquestraExpurgoLog");
                }
                catch (Exception ex)
                {
                    Global.EnviarParaLog($"Não foi possível concluir o expurgo de Log. Motivo: {ex.Message}", "OrquestraExpurgoLog");
                }
            }
        }

        public int QtdArqParaExpurgo(DateTime pDataExpurgoLog)
        {
            int Result = 0;
            string script = $"Select Count(*) Qtd from tb_logs where CONVERT(datetime,data,103)<=CONVERT(datetime,'{pDataExpurgoLog.ToString("dd/MM/yyyy")} 23:59:59',103)";
            SqlConnection _Conn = new SqlConnection(Global.ConnectionsString);
            SqlCommand cmd = null;
            try
            {
                _Conn.Open();
                cmd = new SqlCommand(script, _Conn);
                cmd.CommandTimeout = Global.CommandTimeOut;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if (!string.IsNullOrEmpty(dr["Qtd"].ToString()))
                        {
                            Result = Convert.ToInt32(dr["Qtd"].ToString());
                        }
                    }
                }
            }
            finally
            {
                _Conn.Close();
            }
            return Result;
        }
        public void OrquestraExpurgoArq()
        {
            List<string> lstArquivos = new List<string>();
            ParametrosController parametros = new ParametrosController();
            int QtdeDiasExpurgoArq = parametros.GetIdParametro("QtdeDiasExpurgoArq");
            if (QtdeDiasExpurgoArq == 0)
            {
                parametros.GravaNovoParametro("QtdeDiasExpurgoArq", "30");
            }
            QtdeDiasExpurgoArq = Convert.ToInt32(parametros.CadParametrosBase.isNullResultZero(parametros.GetValorParametro("QtdeDiasExpurgoArq")));
            DateTime DataExpurgoArq = DateTime.Now.AddDays(QtdeDiasExpurgoArq * (-1));


            try
            {
                bool ArqExpurgado = false;
                List<ArquivosModel> drArq = GetArquivosParaExpurgo(DataExpurgoArq);
                foreach (var drArquivos in drArq)
                {
                    lstArquivos = BuscaArquivosPelaMascara(drArquivos.dir_entrada.ToString(), drArquivos.mascara_arquivo + "*");//BuscaArquivos(dir);
                    foreach (var lArq in lstArquivos)
                    {
                        try
                        {
                            File.Delete(lArq.ToString());
                            parametros.CadParametrosBase.conexao.ExecutarScript($"Update tb_arquivos set ArqExcluido='SIM' where id_arquivo={drArquivos.id_arquivo.ToString()}");
                            ArqExpurgado = true;
                        }
                        catch
                        {
                            ArqExpurgado = false;
                            parametros.CadParametrosBase.conexao.ExecutarScript($"Update tb_arquivos set ArqExcluido='NÃO' where id_arquivo={drArquivos.id_arquivo.ToString()}");
                        }
                    }
                }
                if (ArqExpurgado)
                {
                    Global.EnviarParaLog("Expurgo de Arquivo concluído com sucesso.", "OrquestraExpurgoArq");
                }
            }
            catch (Exception ex)
            {
                Global.EnviarParaLog($"Não foi possível concluir o expurgo de Arquivo. Motivo: {ex.Message}", "OrquestraExpurgoArq");
            }

        }
        public List<MonitoramentosModel> GetMonitoramentos(string pIdServico)
        {
            SqlConnection _Conn = new SqlConnection(Global.ConnectionsString);
            SqlCommand cmd = null;
            List<MonitoramentosModel> lst = null;
            try
            {
                _Conn.Open();
                cmd = new SqlCommand($"select * from tb_monitoramentos where id_servico = {pIdServico} order by id_arquivo", _Conn);
                cmd.CommandTimeout = Global.CommandTimeOut;
                lst = new MonitoramentosModel().ListMonitoramentos(cmd.ExecuteReader());
            }
            finally
            {
                _Conn.Close();
            }
            return lst;
        }

        public List<ArquivosModel> GetArquivos(int pIdArquivo)
        {
            SqlConnection _Conn = new SqlConnection(Global.ConnectionsString);
            SqlCommand cmd = null;
            List<ArquivosModel> lst = null;
            try
            {
                _Conn.Open();
                cmd = new SqlCommand($"select * from tb_arquivos where id_arquivo = {pIdArquivo.ToString()} and lower(status)='ativo' ", _Conn);
                cmd.CommandTimeout = Global.CommandTimeOut;
                lst = new ArquivosModel().LstArquivos(cmd.ExecuteReader());
            }
            finally
            {
                _Conn.Close();
            }

            return lst;
        }

        public List<ArquivosModel> GetArquivosParaExpurgo(DateTime pData)
        {
            SqlConnection _Conn = new SqlConnection(Global.ConnectionsString);
            SqlCommand cmd = null;
            List<ArquivosModel> lst = null;
            string script = $"select * from tb_arquivos where CONVERT(datetime,data_criacao,103)<=CONVERT(datetime,'{pData.ToString("dd/MM/yyyy")} 23:59:59',103) and (ArqExcluido='NÃO' or ArqExcluido is null)";
            try
            {
                _Conn.Open();
                cmd = new SqlCommand(script, _Conn);
                cmd.CommandTimeout = Global.CommandTimeOut;
                lst = new ArquivosModel().LstArquivos(cmd.ExecuteReader());
            }
            catch(Exception ex)
            {
                Global.EnviarParaLog($"Erro ao executar o Script {script}. Motivo: {ex.Message}", "GetArqToExpurgo");
            }
            finally
            {
                _Conn.Close();
            }

            return lst;
        }

        public string GetParametro(string pParametro)
        {
            string Result = "";
            SqlConnection _Conn = new SqlConnection(Global.ConnectionsString);
            SqlDataReader dr = null;
            try
            {
                _Conn.Open();
                SqlCommand cmd = new SqlCommand($"select valor from tb_parametros where nm_parametro = '{pParametro}' and lower(status)='ativo'", _Conn);
                cmd.CommandTimeout = Global.CommandTimeOut;
                dr = cmd.ExecuteReader();
                if (dr.HasRows && dr.Read())
                {
                    Result = dr["valor"].ToString();
                }
                else
                {
                    Global.EnviarParaLog($"Erro: Parâmetro de configuração [{pParametro}] não foi definido na tabela tb_parametros.", "GetParametro");
                }
            }
            catch (Exception ex)
            {
                Global.EnviarParaLog($"Erro: {ex.Message}.", "GetParametro");
            }
            finally
            {
                dr.Close();
                _Conn.Close();
            }
            return Result;
        }

        //public void DropTabela(string pNomeTabela,string pConnectionString)
        //{
        //    SqlConnection _Conn = new SqlConnection(pConnectionString);
        //    try
        //    {
        //        _Conn.Open();
        //        SqlCommand cmd = new SqlCommand("Drop table " + pNomeTabela, _Conn);
        //        cmd.CommandTimeout = Global.CommandTimeOut;  
        //        cmd.ExecuteNonQuery();
        //    }
        //    finally
        //    {
        //        _Conn.Close();
        //    }
        //}

        public void DropTabela(string pNomeTabela, string pConnectionString)
        {
            SqlConnection _Conn = new SqlConnection(pConnectionString);
            try
            {
                _Conn.Open();
                SqlCommand cmd = new SqlCommand("Drop table " + pNomeTabela, _Conn);
                cmd.CommandTimeout = Global.CommandTimeOut;
                cmd.ExecuteNonQuery();
            }
            finally
            {
                _Conn.Close();
            }
        }

        public string GetNomePacote(int pIdCategoria)
        {
            SqlConnection _Conn = new SqlConnection(Global.ConnectionsString);
            try
            {
                _Conn.Open();
                SqlCommand cmd = new SqlCommand($"select nm_pacote from tb_pacotes where id_categoria = {pIdCategoria.ToString()} and lower(status)='ativo'", _Conn);
                cmd.CommandTimeout = Global.CommandTimeOut;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows && dr.Read())
                {
                    string vl = dr["nm_pacote"].ToString();
                    dr.Close();
                    _Conn.Close();
                    return vl;
                }
                else
                {
                    _Conn.Close();
                    Global.EnviarParaLog($"Erro: Não foi encontrado Pacotes com a Categoria especificada.", "GetNomePacote");
                    return "";
                }
            }
            catch (Exception ex)
            {
                _Conn.Close();
                Global.EnviarParaLog($"Erro: {ex.Message}.", "GetNomePacote");
                return "";
            }
        }

        public List<MonitoramentosModel> DrMonitoramentos(string pIdServico)
        {
            SqlConnection _Conn = new SqlConnection(Global.ConnectionsString);
            SqlCommand cmd = null;
            List <MonitoramentosModel> lst = null;
            try
            {
                _Conn.Open();
                cmd = new SqlCommand($"select * from tb_monitoramentos where id_servico = {pIdServico} and lower(status)='ativo'", _Conn);
                cmd.CommandTimeout = Global.CommandTimeOut;
                lst = new MonitoramentosModel().ListMonitoramentos(cmd.ExecuteReader());
            }
            finally
            {
                _Conn.Close();
            }
            return lst;
        }

        public void Configura_Ad_Hoc_Distributed_Queries(EtlConexao.Conexao con, int OnOff)
        {
            try
            {
                con.ExecutarScript("sp_configure 'show advanced options', 1;");
                con.ExecutarScript("RECONFIGURE;");
                con.ExecutarScript($"sp_configure 'Ad Hoc Distributed Queries', {OnOff.ToString()};");
                con.ExecutarScript("RECONFIGURE;");
                Global.EnviarParaLog($"Comando para Habilitar xp_cmdshell executado com sucesso.", "Conf_Ad_Hoc_Dist");
            }
            catch(Exception ex)
            {
                Global.EnviarParaLog($"ERRO ao Configurar Ad Hoc Distributed Queries. MOTIVO:{ex.Message}.", "Conf_Ad_Hoc_Dist");
            }
        }
        public void HabilitarXP_CmdShell(SqlConnection con,int OnOff)
        {
            string _OnOff = "Desabilitado";
            if (OnOff == 1)
            {
                _OnOff = "Habilitado";
            }
            try
            {
                SqlCommand cmd = null;
                cmd = new SqlCommand("sp_configure 'show advanced options', 1;",con);
                cmd.CommandTimeout = Global.CommandTimeOut;
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("RECONFIGURE;", con);
                cmd.CommandTimeout = Global.CommandTimeOut;
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand($"sp_configure 'xp_cmdshell', {OnOff.ToString()};", con);
                cmd.CommandTimeout = Global.CommandTimeOut;
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("RECONFIGURE;", con);
                cmd.CommandTimeout = Global.CommandTimeOut;
                cmd.ExecuteNonQuery();
                Global.EnviarParaLog($"Comando xp_cmdshell {_OnOff} com sucesso.", "HabilXP_CmdShell");
            }
            catch(Exception ex)
            {
                Global.EnviarParaLog($"Erro,comando xp_cmdshell não {_OnOff}. MOTIVO:{ex.Message}.", "HabilXP_CmdShell");
            }
}
        public bool GeraSaidaDoArquivo(SqlConnection con,string pNomeTabelaDST,string pPathOutput,string pSchemaDST,int pIdArquivo=0)
        {
            bool Result = false;
            //Comando para Habilitar xp_cmdshell
            HabilitarXP_CmdShell(con, 1);

            //Se não existe o diretorio, emtão, cria
            if (!Directory.Exists(pPathOutput))
            {
                Directory.CreateDirectory(pPathOutput);
            }

            //Utilizando queryout, pode-se exportar o resultado de uma query
            try
            {
                string DataSource = "";
                string InitCatalog = "";
                string UserID = "";
                string Password = "";
                bool AutenticacaoPeloWindows = false;

                GetDadosdoBancoDST(pSchemaDST,ref DataSource, ref InitCatalog, ref UserID, ref Password, ref AutenticacaoPeloWindows);
                string cmdSelect = $"SELECT * FROM {pSchemaDST}.dbo.{pNomeTabelaDST}";
                string PathFileOutPut = $"{pPathOutput}{pNomeTabelaDST+"_"+DateTime.Now.ToString("yyyyMMdd_HHmmss")}.csv";
                string strDataSource = DataSource;
                string strUsuario = UserID == "" ? "" : $"{UserID}";
                string strPassword = Password == "" ? "" : $"{Password}";
                string ddConexao = AutenticacaoPeloWindows ? $"-T -S{strDataSource}" : $"-U{strUsuario} -P{strPassword} -S{strDataSource}";
                string AspasDupla=@"""";

                string comando = $"EXEC master.dbo.xp_cmdshell 'bcp {AspasDupla+cmdSelect+AspasDupla} queryout {AspasDupla+PathFileOutPut+ AspasDupla} -c -t; {ddConexao}'";

                Global.EnviarParaLog($"Comando para gerar arquivo CSV :{comando}","GeraSaidaDoArq",true, pIdArquivo);

                SqlCommand cmd = new SqlCommand(comando, con);
                cmd.CommandTimeout = Global.CommandTimeOut;
                cmd.ExecuteNonQuery();

                Global.EnviarParaLog($"Comando para gerar arquivo CSV executado com sucesso.", "GeraSaidaDoArq", true, pIdArquivo);
                Result = true;
            }
            catch (Exception ex)
            {
                Result = false;
                Global.EnviarParaLog($"Erro ao executar o comando para gerar arquivo CSV. MOTIVO: {ex.Message}.", "GeraSaidaDoArq", true, pIdArquivo);
            }

            //Comando para Desabilitar xp_cmdshell
            HabilitarXP_CmdShell(con, 0);
            /*

//Utilizando queryout, pode-se exportar o resultado de uma query
EXEC master.dbo.xp_cmdshell 'bcp "SELECT * FROM starbi.dbo.APP721A_4" queryout "C:\output\bcp_queryout.csv" -c -t; -T -SStarline\SQLExpress'


--Comando para desabilitar xp_cmdshell
sp_configure 'show advanced options', 1;
RECONFIGURE;
GO

sp_configure 'xp_cmdshell', 0;
RECONFIGURE;
GO                 

Onde:
– out e queryout permitem definir a forma de exportação dos dados. OUT exporta um objeto e o QUERYOUT o resultado de uma query.
– -c define que todos os campos serão exportados como caracter (string)
– -t; permite definir o separador dos campos, não limitando-se a apenas 1 caractere como separador. No primeiro exemplo, estou utilizando o “;” como separador de coluna.
– -T serve para informar que a conexão será realizada no modo Trusted Connection (Autenticação Windows). Caso você queira utilizar autenticação SQL Server, basta utilizar -Uusuario e -Psenha.
– -S serve para informar o servidor\instância que você deseja se conectar.

SQLShackDemoATC.dbo.SQLShackDemo    - Este é o nome da tabela que pretendemos exportar. Também podemos usar a opção -d para incluir o nome do banco de dados.
c:\SODetail_Out.txt                 - este é o arquivo de saída para o qual os dados são despejados
-T                                  - Autenticação confiável do Windows
-t,                                 - define vírgula como o separador de campos
-w                                  - usa formato de dados de largura larga
-b1000                              - Exporta os dados em lotes de 1000 linhas

 * 
 * */
            return Result;
        }

        public bool GravaLog(int pIdArquivo,string pEtapa,string pMensagem, string pCmdSql)
        {
            string script = "";
            SqlConnection _Conn = new SqlConnection(Global.ConnectionsString);
            bool Result = false;
            try
            {
                _Conn.Open();
                script = "insert into tb_logs(id_arquivo,etapa,data,Mensagem,ComandoSql) values(@id_arquivo,@etapa,@data,@Mensagem,@ComandoSql)";
                List<SqlParameter> Parametros = new List<SqlParameter>();
                Parametros.Clear();
                Parametros.Add(new SqlParameter() { ParameterName = "id_arquivo", Value = pIdArquivo });
                Parametros.Add(new SqlParameter() { ParameterName = "etapa", Value = pEtapa });
                Parametros.Add(new SqlParameter() { ParameterName = "data", Value = DateTime.Now });
                Parametros.Add(new SqlParameter() { ParameterName = "Mensagem", Value = pMensagem });
                Parametros.Add(new SqlParameter() { ParameterName = "ComandoSql", Value = pCmdSql });
                SqlCommand cmd = new SqlCommand(script,_Conn);
                cmd.CommandTimeout = Global.CommandTimeOut;
                for (int i = 0; i <= Parametros.Count - 1; i++)
                {
                    cmd.Parameters.AddWithValue(Parametros[i].ParameterName, Parametros[i].Value);
                }
                cmd.ExecuteNonQuery();
                Result = true;
            }
            catch(Exception ex)
            {
                //Global.EnviarParaArquivoLog("ErroAoGravarLog", $"insert into tb_logs(id_arquivo, etapa, data, Mensagem, ComandoSql) values({pIdArquivo}, {pEtapa}, {DateTime.Now.ToString()}, {pMensagem}, {pCmdSql})");
                Global.EnviarParaLog($"Erro ao gravar o log({script}). MOTIVO: {ex.Message}.", "GravaLog");
                Result = false;
            }
            finally
            {
                _Conn.Close();
            }
            return Result;
        }


        public List<ServicosModel> GetListaServicos()
        {
            SqlConnection _Conn = new SqlConnection(Global.ConnectionsString);
            List<ServicosModel> lst = null;
            try
            {
                _Conn.Open();
                string script = "Select * from tb_servicos where status='ativo' and situacao='Ocioso'";
                SqlCommand cmd = new SqlCommand(script, _Conn);
                cmd.CommandTimeout = Global.CommandTimeOut;
                lst = new ServicosModel().ListaServicos(cmd.ExecuteReader());
            }
            finally
            {
                _Conn.Close();
            }

            return lst;

        }

        public void AtualizaListaServicosComErro()
        {
            bool Result = false;
            string script = $"Update tb_servicos set situacao='Ocioso' where status='ativo' and situacao<>'Ocioso'";
            string Erro = "";
            Result = Conexao.ExecutaCommandText(script, Global.ConnectionsString, ref Erro);
            if (!Result)
            {
                Global.EnviarParaLog("ERRO: Não foi possivel atualizar a lista de Servicos. MOTIVO: " + Erro, "Atual_ListaServ");
            }
            Erro = "";
        }

        public void AtualizaSituacaoThread(string pIdServico, string pSituacao)
        {
            string script = $"Update tb_servicos set situacao='{pSituacao}' where id_servico={pIdServico}";
            string Erro = "";
            bool Result=Conexao.ExecutaCommandText(script, Global.ConnectionsString,ref Erro);
            if (!Result)
            {
                Global.EnviarParaLog("ERRO: Não foi possivel atualizar a Situação do Processamentio no Serviço. MOTIVO: " + Erro, "Atual_ListaServ");
            }
            Erro = "";
        }

        public string ConnectionsStringDST(string pNomeDBDST = "")
        {
            return GetDadosDaConexaoDST(pNomeDBDST);
                //GetParametro("connection_string_base_destino");



        }

        public bool DataBaseCriada(string pSchemaDST)
        {
            bool Result = false;
            string script = $"SELECT database_id FROM sys.databases WHERE NAME = '{pSchemaDST}' ";
            using (SqlConnection Conn = new SqlConnection(Global.ConnectionsString))
            {
                Conn.Open();
                SqlCommand cmd = new SqlCommand(script, Conn);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    Result = Global.Strtoint(dr["database_id"].ToString())>0;
                }
            }

            return Result;
        }
        public bool CriarDataBaseDst(string pSchemaDST,int pIdArquivo=0)
        {
            string script = $"CREATE DATABASE {pSchemaDST}";
            bool Result=false;
            using (SqlConnection Conn = new SqlConnection(Global.ConnectionsString))
            {
                Conn.Open();
                SqlCommand cmd = new SqlCommand(script, Conn);
                try
                {
                    cmd.ExecuteNonQuery();
                    Result = true;
                    Global.EnviarParaLog($"DataBase Destino '{pSchemaDST}' criada com sucesso.", "CriarDataBaseDst", true, pIdArquivo);
                }
                catch (Exception ex)
                {
                    Global.EnviarParaLog($"Erro ao criar o DataBase Destino '{pSchemaDST}'. Motivo: {ex.Message}", "CriarDataBaseDst",true, pIdArquivo);
                }
            }

            return Result;

        }

        public void CriarTabela_ArquivosCarregados(string pSchema,int pIdArquivo=0)
        {
            string NomeTabela = "ARQUIVOS_CARREGADOS";
            string script = $"CREATE TABLE {NomeTabela}(";
            script += "Tabela nvarchar(128),";
            script += "Diretorio nvarchar(128),";
            script += "Arquivo nvarchar(128)";
            script += ")";

            if (!TabelaCriada(pSchema,NomeTabela))
            {
                using (SqlConnection Conn = new SqlConnection(ConnectionsStringDST(pSchema)))
                {
                    Conn.Open();
                    SqlCommand cmd = new SqlCommand(script, Conn);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        Global.EnviarParaLog($"Tabela '{NomeTabela}' criada com sucesso.", "Arq_Carregados", true, pIdArquivo);
                    }
                    catch (Exception ex)
                    {
                        Global.EnviarParaLog($"Erro ao criar a Tabela '{NomeTabela}'. Motivo: {ex.Message}", "Arq_Carregados", true, pIdArquivo);
                    }
                }
            }
        }

        public void PopulaARQUIVOSCARREGADOS(string pSchemaDST,string ptabela,string pDiretorio, string pArquivo,int pIdArquivo = 0)
        {
            string script = $"INSERT INTO ARQUIVOS_CARREGADOS(TABELA,DIRETORIO,ARQUIVO) VALUES('{ptabela}','{pDiretorio}','{pArquivo}')";
            using (SqlConnection Conn = new SqlConnection(ConnectionsStringDST(pSchemaDST)))
            {
                Conn.Open();
                SqlCommand cmd = new SqlCommand(script, Conn);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Global.EnviarParaLog($"Erro ao popular a Tabela ARQUIVOS_CARREGADOS. Motivo: {ex.Message}", "PopARQCARREGADOS", true, pIdArquivo);
                }
            }

        }

        public string GetDadosDaConexaoDST(string pNomeDBDST="")
        {
            string Result = "";

            string ConnStringDST = GetParametro("connection_string_base_destino");
            bool AutenticacaoPeloWindows = ConnStringDST.IndexOf("Integrated Security=True") > 0;
            string DataSource = "";
            string InitCatalog = "";
            string UserID = "";
            string Password = "";
            if (ConnStringDST.Length > 0)
            {
                string connStr = ConnStringDST.Remove(0, ConnStringDST.IndexOf("=") + 1);
                DataSource = connStr.Substring(0, connStr.IndexOf(";"));
                connStr = connStr.Remove(0, connStr.IndexOf("=") + 1);
                InitCatalog = connStr.Substring(0, connStr.IndexOf(";"));
                if (!AutenticacaoPeloWindows)
                {
                    connStr = connStr.Remove(0, connStr.IndexOf("=") + 1);
                    UserID = connStr.Substring(0, connStr.IndexOf(";"));
                    connStr = connStr.Remove(0, connStr.IndexOf("=") + 1);
                    Password = connStr.Trim();
                }

                if (!string.IsNullOrEmpty(pNomeDBDST))
                {
                    InitCatalog = pNomeDBDST;
                }

                if (AutenticacaoPeloWindows)
                {
                    Result = $"Data Source={DataSource};Initial Catalog={InitCatalog};Integrated Security=True";
                }
                else
                {
                    Result = $"Data Source={DataSource};Initial Catalog={InitCatalog};User ID={UserID};Password={Password}";
                }
            }
            else
            {
                Global.EnviarParaLog($"Erro ao obter a ConnectionsStringDST", "GetDadosConDST", true, 0);
            }

            return Result;
        }

        public void GetDadosdoBancoDST(string pNomeDBDST, ref string DataSource, ref string InitCatalog, ref string UserID, ref string Password, ref bool AutenticacaoPeloWindows)
        {
            string ConnStringDST = GetParametro("connection_string_base_destino");
            AutenticacaoPeloWindows = ConnStringDST.IndexOf("Integrated Security=True") > 0;
            DataSource = "";
            InitCatalog = "";
            UserID = "";
            Password = "";
            if (ConnStringDST.Length > 0)
            {
                string connStr = ConnStringDST.Remove(0, ConnStringDST.IndexOf("=") + 1);
                DataSource = connStr.Substring(0, connStr.IndexOf(";"));
                connStr = connStr.Remove(0, connStr.IndexOf("=") + 1);
                InitCatalog = connStr.Substring(0, connStr.IndexOf(";"));
                if (!AutenticacaoPeloWindows)
                {
                    connStr = connStr.Remove(0, connStr.IndexOf("=") + 1);
                    UserID = connStr.Substring(0, connStr.IndexOf(";"));
                    connStr = connStr.Remove(0, connStr.IndexOf("=") + 1);
                    Password = connStr.Trim();
                }

                if (!string.IsNullOrEmpty(pNomeDBDST))
                {
                    InitCatalog = pNomeDBDST;
                }

            }
            else
            {
                Global.EnviarParaLog($"Erro ao obter a ConnectionsStringDST", "GetDadosConDST", true, 0);
            }
        }
        private List<string> BuscaArquivosPelaMascara(string dir, string Mascara)
        {

            List<string> lst = new List<string>();
            string[] dirs = Directory.GetFiles(dir, Mascara);
            if (dir.Substring(dir.Length - 1, 1) != "\\")
            {
                dir += "\\";
            }

            foreach (string FileName in dirs)
            {
                lst.Add(FileName);
                Global.EnviarParaLog($"{FileName}", "BuscaArqMascara");
            }
            return lst;
        }
        public bool TabelaCriada(string pSchema, string pNomeTabela)
        {
            bool Result = false;
            SqlConnection _Conn = new SqlConnection(GetDadosDaConexaoDST(pSchema));
            _Conn.Open();
            string script = $"select table_name Result from INFORMATION_SCHEMA.TABLES where TABLE_CATALOG='{pSchema}' and table_name='{pNomeTabela}'";
            SqlCommand cmd = new SqlCommand(script, _Conn);
            cmd.CommandTimeout = Global.CommandTimeOut;
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows && dr.Read())
            {
                string vNomeTabela = dr["Result"].ToString();
                if (vNomeTabela == "")
                {
                    Result = false;
                }
                else
                {
                    Result = true;
                }
            }

            _Conn.Close();
            return Result;
        }

    }
}
