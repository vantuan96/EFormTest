using System.Configuration;
using System.Net.Mail;

namespace EForm.Utils
{
    public class Email
    {
        private SmtpClient smtpClient;
        private MailMessage mail;

        public Email()
        {
            var smtp_server = ConfigurationManager.AppSettings["SmtpServer"].ToString();
            var smtp_port = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]);
            var email_name = ConfigurationManager.AppSettings["EmailName"].ToString();
            var email_pass = ConfigurationManager.AppSettings["EmailPass"].ToString();
            var email_default = ConfigurationManager.AppSettings["EmailDefault"].ToString();

            this.smtpClient = new SmtpClient
            {
                Host = smtp_server,
                Port = smtp_port,
                Credentials = new System.Net.NetworkCredential(email_name, email_pass),
                DeliveryMethod = SmtpDeliveryMethod.Network,
            };

            this.mail = new MailMessage
            {
                From = new MailAddress(email_default, "EMR Notification"),
                IsBodyHtml = true,
            };
        }

        public void ChangeSubject(string subject)
        {
            this.mail.Subject = subject;
        }

        public void ChangeBody(string body)
        {
            this.mail.Body = body;
        }

        public void ToOne(string mail)
        {
            this.mail.To.Add(mail);
        }

        public void ToMany(string[] lst_mail)
        {
            foreach (string mail in lst_mail)
                this.mail.To.Add(mail.Trim());
        }

        public void Send()
        {
            this.smtpClient.Send(this.mail);
        }
    }
}