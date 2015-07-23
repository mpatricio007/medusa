using Microsoft.VisualStudio.TestTools.UnitTesting;
using Medusa.LIB;

namespace Teste
{
    [TestClass]
    public class TestMail
    {
        [TestMethod]
        public void Send()
        {
            var s = new SendEmail(ContaEmail.boleto)
            {
                Destinatarios = new[] {"patricia@fusp.org.br"},
                Body = "Teste de envio",
                Subject = "Assunto Teste"
            };
            s.Send();

        }
    }
}
