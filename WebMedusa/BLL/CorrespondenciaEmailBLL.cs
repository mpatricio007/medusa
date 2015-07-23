using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;
using Medusa.LIB;

namespace Medusa.BLL
{
    public class CorrespondenciaEmailBLL : AbstractCrudWithLog<CorrespondenciaEmail>
    {
        public bool foiEnviado()
        {
            return ObjEF.enviadoEm.HasValue;
        }

        public override void Delete()
        {
            if (!foiEnviado())
            {
                _dbSet.Remove(objEF);
            }
        }

        public bool ExisteDestinario(string e_mail)
        {
            return (ObjEF.DestinatarioEmails.Where(it => it.email_value == e_mail).ToList().Count() > 0);
        }

        public void ConfirmarLeituraDestinatario(string e_mail)
        {
            DestinatarioEmailBLL deBLL = new DestinatarioEmailBLL();

            foreach (DestinatarioEmail d in ObjEF.DestinatarioEmails.Where(it => it.email_value == e_mail).ToList())
            {
                deBLL.Get(d.id_destinatario);
                deBLL.ConfirmarLeitura();
                deBLL.Update();
                deBLL.SaveChanges();
            }
           
        }

      
        
        public bool Existe(string id)
        {
            Int32 id_correspEmail = 0;

            try
            {
                id_correspEmail = Convert.ToInt32(id);

            }
            catch (Exception)
            {

            }


            Get(id_correspEmail);
            return (ObjEF.assunto != null);
        }

        public void EnviarEmail()
        {

            var sendmail = new SendEmail(ContaEmail.circular);
            foreach (var dest in ObjEF.DestinatarioEmails)
            {

                sendmail.Subject = ObjEF.assunto;
                sendmail.Destinatarios = new string[] { dest.email_value };
                var body = new StringBuilder();
                body.Append("<html><body>");
                body.Append(ObjEF.corpo.ToString().parsetext());
                body.AppendFormat("<p>Para visualizar a circular <a href='http://circular.fusp.org.br/?p1={0}&p2={1}' target='_blank'>clique aqui</a></p>",
                    dest.CorrespondenciaEmail.id_correspEmail.ToString().OldCriptografar(), dest.email_value.OldCriptografar());
                body.Append("</body></html>");

                body.AppendLine();
                sendmail.Body = body.ToString();
                if (sendmail.Send())                    
                    dest.enviado_em = DateTime.Now;                
            }
            ObjEF.enviadoEm = DateTime.Now;
            Update();
            SaveChanges();            
        }
        
        public int QtdeEmailsLidos()
        {
            if (ObjEF.DestinatarioEmails != null)
                return ObjEF.DestinatarioEmails.Where(it => it.confirmacao_leitura != null).Count();
            else
                return 0;
        }

        public int QtdeEmailsNaoLidos()
        {
            if (ObjEF.DestinatarioEmails != null)
                return ObjEF.DestinatarioEmails.Where(it => it.confirmacao_leitura == null).Count();
            else
                return 0;
        }

        public void CriarEmailParaOsQueNaoLeram()
        {
            
            var lista = ObjEF.DestinatarioEmails.Where(it => it.confirmacao_leitura == null).ToList();
            Clone();
            
            ObjEF.enviadoEm = null;
            ObjEF.data = DateTime.Now;
            ObjEF.assunto = String.Format("Enc {0}", ObjEF.assunto);
            ObjEF.DestinatarioEmails = new List<DestinatarioEmail>();

            foreach (var d in lista)
            {
                var dest = (DestinatarioEmail)_dbContext.Entry(d).GetDatabaseValues().ToObject();
                dest.enviado_em = null;
                ObjEF.DestinatarioEmails.Add(dest);
            }

            Add();
            SaveChanges();
        }
    }

    
}
