/*
	==============================================
	Criado por : Carlos R. S. Oliveira
	Criado em  : 20/05/2019
	==============================================
*/
using EtlConexao;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facade
{
    public class EnviarEmailController
    {
        Conexao conexao;
        public EnviarEmailController()
        {
            conexao = new Conexao();
        }

        public SqlDataReader GetUsrEnviarEmails(int idGrupo)
        {
            string select = "";
            select += "select u.nm_usuario,u.email 'To' ,c.Smtp_eMail 'From' ,c.Smtp_EnableSSL,c.Smtp_Port,c.Smtp_Host,c.Smtp_Senha 'Senha',c.EnviarSoErros, c.nm_categoria";       
            select += " from tb_usuarios u, tb_UsuariosXCategorias uc, tb_categorias c";
            select += " where uc.id_usuario = u.id_usuario";
            select += " and u.RecebeNotificacao = 'SIM'";
            select += " and uc.id_categoria = c.id_categoria";
            if (idGrupo > 0)
            {
                select += $" and c.id_grupo = {idGrupo.ToString()}";
            }

            return conexao.ExecutarSelect(select);   
        }

        public string EnviaEmail(int pIdServico=0)
        {
            string MsgEmailEnviado = "";
            try
            {
                GetDadosParaEnviarEmail DadosFromEnvEmail = new GetDadosParaEnviarEmail();
                string CorpoEmail = DadosFromEnvEmail.GeraFileHtml(pIdServico);
                SqlDataReader dr = GetUsrEnviarEmails(0);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        EnviarEmail.smtp = dr["Smtp_Host"].ToString();
                        EnviarEmail.port = Global.StrToInt(dr["Smtp_Port"].ToString()) == 0 ? 587 : Global.StrToInt(dr["Smtp_Port"].ToString());
                        EnviarEmail.from = dr["From"].ToString();
                        EnviarEmail.to = dr["To"].ToString();
                        EnviarEmail.subject = "Email processamento ExecutaCarga";
                        EnviarEmail.body = CorpoEmail;
                        EnviarEmail.priority = false;
                        EnviarEmail.EnableSSL = dr["Smtp_EnableSSL"].ToString() == "SIM";
                        EnviarEmail.Usuario = dr["From"].ToString();
                        EnviarEmail.Senha = dr["Senha"].ToString();
                        EnviarEmail.FromDisplayNome = $"Filial {dr["nm_categoria"].ToString()}";
                        EnviarEmail.toDisplayNome = dr["nm_usuario"].ToString();

                        MsgEmailEnviado = EnviarEmail.Enviar_Email();
                    }
                };
                return "OK";
            }
            catch (Exception ex)
            {
                return MsgEmailEnviado+" "+ex.Message;
            }
            
        }
    }
}
