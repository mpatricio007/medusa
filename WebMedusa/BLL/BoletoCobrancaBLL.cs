using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medusa.DAL;
using Medusa.LIB;
using System.Text;

namespace Medusa.BLL
{
    public class BoletoCobrancaBLL:AbstractCrudWithLog<BoletoCobranca>
    {

        public bool BaixarBoleto(out string msg)
        {
            msg = String.Empty;
            bool rt = false;
            if (ObjEF.id_boleto != 0)
            {
                if (!JahBaixado())
                {
                    Update();
                    rt = SaveChanges();
                    if (rt)
                        msg = "baixa efetuada com sucesso!";
                    else
                        msg = "erro";
                }
                else
                {
                    msg = "boleto já baixado!";
                }
            }
            else
            {
                msg = "boleto inexistente!";
                rt = false;
            }
            return rt;
            
        }

        public bool JahBaixado()
        {
            var oldBoleto = (BoletoCobranca)_dbContext.Entry(ObjEF).GetDatabaseValues().ToObject();
            oldBoleto = oldBoleto ?? new BoletoCobranca();
            return oldBoleto.valor_pgto.HasValue && oldBoleto.data_pgto.HasValue;
        }

        //public bool EnviarEmail(out string saida)
        //{
        //    var sendmail = new SendEmail(ContaEmail.boleto);
        //    sendmail.Subject = "Boleto Online FUSP";
        //    sendmail.Destinatarios = /*ObjEF.EventoSacado.Sacado.PessoaFisica.Emails.Select(it => it.email.value).ToArray();//*/ new string[] { "marcelo@fusp.org.br" };
        //    var body = new StringBuilder();
        //    body.Append("<html><body>");
        //    //body.Append("<p>Há alguns dias, encaminhamos a V.Sa. , por correio, os boletos bancários para ressarcimento à FUSP referente aos honorários advocatícios correspondentes ao Processo TC-28395/026/11.</p>");
        //    //body.Append("<p>Alguns boletos foram gerados com código de barras incorreto. Assim, disponibilizamos a V.Sa. os boletos corretos que podem ser obtidos clicando");
        //    //body.AppendFormat(" <a href='http://boleto.fusp.org.br/?pk={0}'>aqui</a>.</p>", ObjEF.id_boleto.ToString().Criptografar());
        //    body.Append("<p><strong>Boleto Online FUSP</strong></p><br />");
        //    body.Append("<p>Prezado(a) Senhor(a),</p>");
        //    body.Append("<p>Você recebe boletos da FUSP via correio e, para sua comodidade, também disponibilizamos a versão online.</p>");
        //    //body.Append("<p>Informamos que alguns boletos foram gerados com código de barras errado. Se for o caso, imprima o boleto correto aqui.</p>");
        //    body.Append("<p><strong>Dados de Cobrança</strong></p>");
        //    body.AppendFormat("<p><strong>Sacado:</strong> {0}</p>", ObjEF.EventoSacado.Sacado.PessoaFisica.nome);
        //    body.AppendFormat("<p><strong>Data de Vencimento:</strong> {0:d}</p>", ObjEF.data_vencto);
        //    body.AppendFormat("<p><strong>Valor:</strong> {0:N2}</p>", ObjEF.valor);
        //    body.AppendFormat("<p><strong>Parcela:</strong> {0}</p>", ObjEF.num_parcela);
        //    body.AppendFormat("<p>Para imprimir o boleto <a href='http://boleto.fusp.org.br/?pk={0}'>clique aqui</a></p>", ObjEF.id_boleto.ToString().Criptografar());
        //    body.Append("<br /><p>Atenciosamente,<br/>Fundação de Apoio à Universidade de São Paulo</p>");
        //    body.Append("</body></html>");
        //    body.AppendLine();
        //    sendmail.Body = body.ToString();
        //    if (sendmail.Send())
        //    {
        //        var emails = new StringBuilder();
        //        emails.Append("Boleto enviado para os e-mails:");
        //        foreach (var item in sendmail.Destinatarios)
        //            emails.AppendLine(item);
        //        saida = emails.ToString();
        //        return true;
        //    }
        //    else
        //    {
        //        saida = "Erro ao envia e-mail!";
        //        return false;
        //    }
        //}

        public bool EnviarEmail(List<BoletoCobranca> lstBoletos,out string saida)
        {
            var sendmail = new SendEmail(ContaEmail.boleto);
            sendmail.Subject = "Boleto Online FUSP";
            var eventoSacado = lstBoletos.FirstOrDefault().EventoSacado;
            sendmail.Destinatarios =  eventoSacado.Sacado.PessoaFisica.Emails.Select(it => it.email.value).ToArray();//*/ new string[] { "mpatricio007@gmail.com" };
            var body = new StringBuilder();            
            body.Append("<html><body>");
            body.Append("<p><strong>Boleto Online FUSP</strong></p><br />");
            body.Append("<p>Prezado(a) Senhor(a),</p>");
            body.AppendFormat("<p>Você está participando do {0} e, para sua comodidade disponibilizamos o acesso aos boletos de cobrança online.</p>",eventoSacado.Evento.nome);            
            body.Append("<p><strong>Dados de Cobrança</strong></p>");
            body.AppendFormat("<p><strong>Sacado:</strong> {0}</p>", eventoSacado.Sacado.PessoaFisica.nome);
            body.Append("<table border=\'0\' cellspacing=\'0\' cellpadding=\'0\' width=\'540\' style=\'width:405.0pt\'>");
            body.Append("<tr>");
            body.Append("<td><strong>parcela</strong></td>");
            body.Append("<td><strong>vencimento</strong></td>");
            body.Append("<td><strong>valor</strong></td>");
            body.Append("<td></td>");
            body.Append("</tr>");
            foreach (var item in lstBoletos.OrderBy(it => it.num_parcela))
            {
                body.Append("<tr>");
                body.AppendFormat("<td>{0}</td>",item.num_parcela);
                body.AppendFormat("<td>{0:d}</td>",item.data_vencto);
                body.AppendFormat("<td>{0:N2}</td>",item.valor);
                body.AppendFormat("<td><a href=\'http://boleto.fusp.org.br/?pk={0}\'>clique aqui</a></td>",item.id_boleto.ToString().Criptografar());
                body.Append("</tr>");
            }
            body.Append("</table>");

       
            body.Append("<br /><p>Atenciosamente,");
            body.Append("<br /><br />Patricia Bednarek");
            body.Append("<br />Gestor Financeiro");
            body.Append("<br />FUSP - Fundação de Apoio à USP - fusp@fusp.org.br");
            body.Append("<br />Tel. 11 - 3035-0563");
            body.Append("<br />e-mail: patricia@fusp.org.br</p>");
            body.Append("</body></html>");
            body.AppendLine();
            sendmail.Body = body.ToString();
            if (sendmail.Send())
            {
                var emails = new StringBuilder();
                emails.Append("Boletos enviados para os e-mails: ");
                foreach (var item in sendmail.Destinatarios)
                    emails.Append(String.Format("{0} ",item));
                emails.Append("com sucesso!");
                saida = emails.ToString();
                return true;
            }
            else
            {
                saida = "Erro ao envia e-mail!";
                return false;
            }
        }

        public bool Exists()
        {
            return ObjEF.id_boleto != 0;
        }
    }
}