/*
	==============================================
	Criado por : Carlos R. S. Oliveira
	Criado em  : 20/05/2019
	==============================================
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Facade
{
    public class EnviarEmail
    {
        public static string smtp { get; set; }
        public static int port { get; set; }
        public static string from { get; set; }
        public static string to { get; set; }
        public static string subject { get; set; }
        public static string body { get; set; }
        public static bool priority { get; set; }
        public static bool EnableSSL { get; set; }
        public static string Usuario { get; set; }
        public static string Senha { get; set; }

        public static string FromDisplayNome { get; set; }
        public static string toDisplayNome { get; set; }


        /// <summary>
        /// Servidor de E-mail
        /// </summary>
        public static SmtpClient SmtpClient { get; set; }

        /// <summary>
        /// Conteudo da Mensagem
        /// </summary>
        public static MailMessage MailMessage { get; set; }

        /// <summary>
        /// Método enviar e-mail
        /// </summary>
        /// <param name="smtp"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="priority"></param>
        public static string Enviar_Email()
        {
            try
            {
                // valida o email
                bool bValidaEmail = ValidaEnderecoEmail(to);

                // Se o email não é validao retorna uma mensagem
                if (bValidaEmail == false)
                    return "Email do destinatário inválido: " + to;

                SmtpClient = new SmtpClient();
                SmtpClient.Host = smtp;
                SmtpClient.Port = port;
                SmtpClient.EnableSsl = EnableSSL;
                SmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpClient.UseDefaultCredentials = true;
                Senha = Crypto.Decriptar(Senha);
                SmtpClient.Credentials = new NetworkCredential(Usuario, Senha);

                MailMessage = new MailMessage();
                MailMessage.From = new MailAddress(from, FromDisplayNome, Encoding.UTF8);
                MailMessage.To.Add(new MailAddress(to, toDisplayNome, Encoding.UTF8));

                MailMessage.Subject = subject;
                MailMessage.Body = body;
                MailMessage.IsBodyHtml = true;
                //MailMessage.BodyEncoding = Encoding.UTF8;
                //MailMessage.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

                if (priority == false)
                {
                    MailMessage.Priority = MailPriority.Normal;
                }
                else
                {
                    MailMessage.Priority = MailPriority.High;
                }

                SmtpClient.Send(MailMessage);
            }
            catch (SmtpFailedRecipientException ex)
            {
                return $"Mensagem : {ex.Message} ";
            }
            catch (SmtpException ex)
            {
                return $"Mensagem SMPT Fail : {ex.Message} ";
            }
            catch (Exception ex)
            {
                return $"Mensagem Exception : {ex.Message}";
            }

            string mensagem = "E-mail enviado";
            return mensagem;
        }



        /// <summary>
        /// Transmite uma mensagem de email com um anexo
        /// </summary>
        /// <param name="Destinatario">Destinatario (Recipient)</param>
        /// <param name="Remetente">Remetente (Sender)</param>
        /// <param name="Assunto">Assunto da mensagem (Subject)</param>
        /// <param name="enviaMensagem">Corpo da mensagem(Body)</param>
        /// <param name="anexos">Um array de strings apontando para a localização de cada anexo</param>
        /// <returns>Status da mensagem</returns>
        public static string EnviaMensagemComAnexos(string Destinatario, string Remetente,
            string Assunto, string enviaMensagem, ArrayList anexos)
        {
            try
            {
                // valida o email
                bool bValidaEmail = ValidaEnderecoEmail(Destinatario);

                if (bValidaEmail == false)
                    return "Email do destinatário inválido:" + Destinatario;

                // Cria uma mensagem
                MailMessage mensagemEmail = new MailMessage(
                   Remetente,
                   Destinatario,
                   Assunto,
                   enviaMensagem);

                // The anexos arraylist should point to a file location where
                // the attachment resides - add the anexos to the message
                foreach (string anexo in anexos)
                {
                    Attachment anexado = new Attachment(anexo, MediaTypeNames.Application.Octet);
                    mensagemEmail.Attachments.Add(anexado);
                }

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                // Inclui as credenciais
                client.UseDefaultCredentials = false;
                NetworkCredential cred = new NetworkCredential("carroboliveira@gmail.com", "C@rr0b11");
                client.Credentials = cred;


                // envia a mensagem
                client.Send(mensagemEmail);

                return "Mensagem enviada para " + Destinatario + " às " + DateTime.Now.ToString() + ".";
            }
            catch (Exception ex)
            {
                string erro = ex.InnerException.ToString();
                return ex.Message.ToString() + erro;
            }
        }
        /// <summary>
        /// Confirma a validade de um email
        /// </summary>
        /// <param name="enderecoEmail">Email a ser validado</param>
        /// <returns>Retorna True se o email for valido</returns>
        public static bool ValidaEnderecoEmail(string enderecoEmail)
        {
            try
            {
                //define a expressão regulara para validar o email
                string texto_Validar = enderecoEmail;
                Regex expressaoRegex = new Regex(@"\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}");

                // testa o email com a expressão
                if (expressaoRegex.IsMatch(texto_Validar))
                {
                    // o email é valido
                    return true;
                }
                else
                {
                    // o email é inválido
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
