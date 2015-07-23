using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using System.Configuration;

namespace Medusa.LIB
{
    public enum ContaEmail
    {
        circular,
        boleto,
        fusp
    }

    public class SendEmail
    {
        private string[] destinatarios;
        private string[] destinatariosBcc;
        private SmtpClient smtpClient;
        private MailMessage message;
        private string smtp;
        private string from;
        private string subject;
        private string body;
        public string erro { get; set; }

        public string[] Destinatarios
        {
            get { return destinatarios; }
            set { destinatarios = value; }
        }

        public string[] DestinatariosBcc
        {
            get
            {
                if (destinatariosBcc == null)
                    destinatariosBcc = new string[] { };
                return destinatariosBcc;
            }
            set { destinatariosBcc = value; }
        }

        public SmtpClient SmtpClient
        {
            get { return smtpClient; }
            set { smtpClient = value; }
        }

        public MailMessage Message
        {
            get { return message; }
            set { message = value; }
        }

        public string Smtp
        {
            get
            {
                if (String.IsNullOrEmpty(smtp))
                    return String.Empty;
                else
                    return smtp;
            }
            set { smtp = value; }
        }

        public string From
        {
            get
            {
                if (String.IsNullOrEmpty(from))
                    return String.Empty;
                else
                    return from;
            }
            set { from = value; }
        }

        public string Subject
        {
            get
            {
                if (String.IsNullOrEmpty(subject))
                    return String.Empty;
                else
                    return subject;
            }
            set { subject = value; }
        }

        public string Body
        {
            get
            {
                if (String.IsNullOrEmpty(body))
                    return String.Empty;
                else
                    return body;
            }
            set { body = value; }
        }

        private ContaEmail contaEmail;

        public SendEmail(ContaEmail c)
        {
            contaEmail = c;
            from = ConfigurationManager.AppSettings[String.Format("from{0}", Enum.GetName(typeof(ContaEmail), contaEmail))];
            smtp = ConfigurationManager.AppSettings[String.Format("smtp{0}", Enum.GetName(typeof(ContaEmail), contaEmail))];
            
            

        }

        private void InsertFrom()
        {
            MailAddress rem = new MailAddress(from, "FUSP - Fundação de Apoio à USP");
            message.From = rem;
        }

        private void InsertTo()
        {
            foreach (string rec in Destinatarios)
            {
                if (!String.IsNullOrEmpty(rec.Trim()))
                    message.To.Add(new MailAddress(rec));
            }

            foreach (string recBcc in DestinatariosBcc)
            {
                if (!String.IsNullOrEmpty(recBcc.Trim()))
                    message.Bcc.Add(new MailAddress(recBcc));
            }
        }

        private void InsertSubject()
        {
            message.Subject = Subject;
        }

        private void InsertBody()
        {
            message.Body = body;
        }

        public void InsertAttachments(string arq)
        {
            if (arq != "") message.Attachments.Add(new Attachment(arq));
        }


        public void InsertAttachments(Attachment attach)
        {
            if (!attach.Equals(null)) message.Attachments.Add(attach);
        }
        public bool Send()
        {
            try
            {
                message = new MailMessage();
                message.IsBodyHtml = true;
                InsertFrom();
                InsertTo();
                InsertSubject();
                InsertBody();
                smtpClient = new SmtpClient(smtp);
                smtpClient.Credentials = new NetworkCredential(ConfigurationManager.AppSettings[String.Format("usuarioSmtp{0}", Enum.GetName(typeof(ContaEmail), contaEmail))], 
                                                                ConfigurationManager.AppSettings[String.Format("senhaSmtp{0}", Enum.GetName(typeof(ContaEmail), contaEmail))]);
                smtpClient.Port = Convert.ToInt32(ConfigurationManager.AppSettings[String.Format("porta{0}", Enum.GetName(typeof(ContaEmail), contaEmail))]);
                smtpClient.Send(message);
                return true;
            }
            catch (Exception e)
            {
                erro = "Erro:" + e;
                return false;
            }
        }
    }

}