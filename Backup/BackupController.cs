/*
	==============================================
	Criado por : Carlos R. S. Oliveira
	Criado em  : 20/05/2019
	==============================================
*/
using ETLApplication;
using ETLApplication.Controller;
using EtlConexao;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backup
{
    class BackupController
    {
        public int IdBackup { get; set; }
        string PathBackup = "";
        string FileName = $"BKP_{DateTime.Now.ToString("yyyyMMdd_hhmmss")}";
        Conexao conexao;
        BackupModel backup;
        public BackupController()
        {
            conexao = new Conexao();
            backup = new BackupModel();
        }
        public void OrquetraBackup()
        {
            ParametrosController parametros = new ParametrosController();
            int CodParametro = 0;
            CodParametro = parametros.GetIdParametro("Path_Backup_StarEtl");
            if (CodParametro == 0)
            {
                parametros.GravaNovoParametro("Path_Backup_StarEtl", @"C:\StarEtl");
            }
            string Pathbackup = parametros.GetValorParametro("Path_Backup_StarEtl");

            int CodBancoDados = parametros.GetIdParametro("Nome_DataBase_Backup");
            if (CodBancoDados == 0)
            {
                parametros.GravaNovoParametro("Nome_DataBase_Backup", @"StarEtl");
            }
            string BancoDados = parametros.GetValorParametro("Nome_DataBase_Backup");


            if (Pathbackup.Length > 0)
            {
                GeraBackup(BancoDados, Pathbackup);
            }

        }
        public bool BackupGerado(string pBancoDados)
        {
            bool Result = false;
            string script = "select Mensagem from tb_logs";
            script += $" where CONVERT(date, data, 101)=CONVERT(date, GetDate(), 101)";
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
                        Result = dr["Mensagem"].ToString() != "";
                    }
                }
            }
            finally
            {
                _Conn.Close();
            }
            return Result;
        }
        public void GeraBackup(string pBancoDados, string pNomePathBackup, bool pBaseORG = true)
        {
            
            if (pNomePathBackup.Substring(pNomePathBackup.Length-1,1)=="\\")
            {
                PathBackup = pNomePathBackup + pBancoDados;
            }
            else
            {
                PathBackup = pNomePathBackup + "\\" + pBancoDados;
            }
            if (!Directory.Exists(PathBackup))
            {
                Directory.CreateDirectory(PathBackup);
            }
            

            string script = "";
            script += $"BACKUP DATABASE {pBancoDados}";
            script += $" TO DISK = '{PathBackup}\\{FileName}.bak'";
            script += " WITH ";//FORMAT,  serve para formatar a media caso esteja usando pela primeira vez
            script += $" MEDIANAME = '{Global.Slugify(PathBackup)}',";
            script += $" NAME = 'Full Backup of {pBancoDados}';";

            SqlConnection _Conn = new SqlConnection(Global.ConnectionsString);
            

            SqlCommand cmd = null;
            try
            {

                _Conn.Open();
                cmd = new SqlCommand(script, _Conn);
                cmd.CommandTimeout = Global.CommandTimeOut;
                cmd.ExecuteNonQuery();
                Global.EnviarParaLog($"Backup {pBancoDados} criado com sucesso.","Backup");
            }
            catch (Exception ex)
            {
                Global.EnviarParaLog($"Erro: Não foi possível criar o Backup {pBancoDados}. Motivo {ex.Message}", "Backup");
            }
            finally
            {
                _Conn.Close();
            }

        }

        public bool CadastrarBackup(
            int pID_Processo,
            string pMensagem="Em processo de geração do Backup",
            string pStatus= "Gerando"
            )
        {
            bool Result =false;
            string script = $"insert into tb_backup(" +
                $"ID_Processo," +
                $"NomeApp," +
                $"PathBackup," +
                $"Nomebackup," +
                $"Mensagem," +
                $"Status" +
                $") Values(" +
                $"{pID_Processo}," +
                $"'Backup'," +
                $"'{PathBackup}'," +
                $"'{FileName}.bak'," +
                $"'{pMensagem}'," +
                $"'{pStatus}'" +
                $")";
            try
            {
                if (BackuCadastrado(pID_Processo) == 0)
                {
                    conexao.ExecutarScript(script);
                }
                Result = true;
            }
            catch
            {
                Result = false;
            }

            return Result;
        }

        public List<int> GetListaIdProcessos()
        {
            List<int> lst = null;
            string script = "select ID_Processo from tb_backup where convert(date,data_criacao,103) = convert(date,GetDate(),103) and status='Gerando' order by Id_backup";
            using (SqlDataReader dr = conexao.ExecutarSelect(script))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        lst.Add(Global.StrToInt(dr["ID_Processo"].ToString()));
                    }
                }
            }
            return lst;
        }


        public int BackuCadastrado(int pIdProcesso)
        {
            int Result = 0;
            string script = $"select id_backup from tb_backup where id_processo={pIdProcesso.ToString()} and convert(date,data_criacao,103) = convert(date,GetDate(),103)";
            using (SqlDataReader dr = conexao.ExecutarSelect(script))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Result=Global.StrToInt(dr["id_backup"].ToString());
                    }
                }
            }
            return Result;
        }

        public void BackupConcluido(int pIdProcesso)
        {
            int idBackup = BackuCadastrado(pIdProcesso);
            string script = $"update tb_backup set  Mensagem='Backup Concluído.',status='Gerado' where id_backup={idBackup.ToString()}";
            conexao.ExecutarScript(script);
        }

     }
}
