using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ZAMOW
{
    class Mail
    {
        private string smtpServer = "";
        private string mail = "";
        private string username = "";
        private string password = "";
        private bool ssl = false;
        private int port = 587;

        public Mail(string smtpServer, string mail, string username, string password, bool ssl, int port)
        {
            this.smtpServer = smtpServer;
            this.mail = mail;
            this.username = username;
            this.password = password;
            this.ssl = ssl;
            this.port = port;
        }    

        public void SendMail(string recipientAdress, string subject, string text)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(smtpServer);

                mailMessage.From = new MailAddress(mail);
                mailMessage.To.Add(recipientAdress);
                mailMessage.Subject = subject;
                mailMessage.Body = text;

                SmtpServer.Port = port;
                SmtpServer.Credentials = new System.Net.NetworkCredential(username, password);
                SmtpServer.EnableSsl = ssl;

                SmtpServer.Send(mailMessage);               
            }
            catch (Exception ex)
            {
                throw new MyException(ex, "błąd podczas wysyłania maila!");
            }
        }
    }
}
