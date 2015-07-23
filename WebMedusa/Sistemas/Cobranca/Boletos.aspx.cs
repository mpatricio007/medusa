using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoletoNet;
using Medusa.BLL;
using Medusa.DAL;
using Medusa.LIB;

namespace Medusa.Sistemas.Cobranca
{
    public partial class Boletos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var bolBLL = new BoletoCobrancaBLL();

                int pkValue = 0;
                int pkEvento = 0;
                try
                {
                    pkValue = Convert.ToInt32(Request.QueryString["pk"].DesCriptografar());
                    pkEvento = Convert.ToInt32(Request.QueryString["ev"].DesCriptografar());                               
                }
                catch (Exception)
                {
                    if (pkValue == 0)
                        pkValue = Convert.ToInt32(Request.QueryString["pk"].OldDesCriptografar());

                    if (pkEvento == 0)
                        pkEvento = Convert.ToInt32(Request.QueryString["ev"].OldDesCriptografar());                               
                }
                

                


                if (pkValue != 0)               
                    ImprimirBoletos(bolBLL.Find(it => it.id_boleto == pkValue).OfType<BoletoCobranca>());
                

                else if(pkEvento != 0)                
                    ImprimirBoletos(bolBLL.Find(it => it.EventoSacado.id_evento == pkEvento).OfType<BoletoCobranca>());             
            }
        }

        protected void ImprimirBoletos(IEnumerable<BoletoCobranca> boletos)
        {
            var div = new Panel();           
            foreach (var item in boletos)
            {
                if (!item.data_pgto.HasValue & !item.valor_pgto.HasValue)
                {
                    Cedente c = new Cedente(Constantes.CNPJ_FUSP, Constantes.NOME_FUSP);
                    c.ContaBancaria = new ContaBancaria();
                    c.ContaBancaria.Conta = item.EventoSacado.Evento.Conta.numero + item.EventoSacado.Evento.Conta.digito;

                    c.ContaBancaria.Agencia = item.EventoSacado.Evento.Conta.BancoAgencia.numero;
                    c.ContaBancaria.DigitoAgencia = item.EventoSacado.Evento.Conta.BancoAgencia.digito;
                    c.Codigo = Convert.ToInt32(item.EventoSacado.Evento.Conta.BancoAgencia.num_convenio);

                    var num_doc = String.Format("{0:000000000000}", item.id_boleto);
                    Boleto b = new Boleto(item.data_vencto, item.valor, "102", num_doc, c);
                    //Esse campo é devolvido no arquivo retorno.
                    
                    b.NumeroDocumento = num_doc;
                    
                    b.Sacado = new BoletoNet.Sacado(item.EventoSacado.Sacado.PessoaFisica.cpf.ToString(), item.EventoSacado.Sacado.PessoaFisica.nome);
                    var pesEnd = item.EventoSacado.Sacado.PessoaFisica.Enderecos.FirstOrDefault();
                    b.Sacado.Endereco.End = String.Format("{0} {1} {2}", pesEnd.endereco.logradouro, pesEnd.endereco.numero, pesEnd.endereco.complemento);
                    b.Sacado.Endereco.Bairro = pesEnd.endereco.bairro;
                    b.Sacado.Endereco.Cidade = pesEnd.endereco.cidade;
                    b.Sacado.Endereco.CEP = pesEnd.endereco.cep;
                    b.Sacado.Endereco.UF = pesEnd.endereco.uf;                    
                    
                    var boletoBancario = new BoletoBancario();
                    
                    //Espécie Documento - [R] Recibo
                    b.EspecieDocumento = new EspecieDocumento_Santander(17);
                    boletoBancario.CodigoBanco = Convert.ToInt16(item.EventoSacado.Evento.Conta.BancoAgencia.Banco.codigo);
                    b.Instrucoes.Add(new Instrucao(boletoBancario.CodigoBanco)
                    {
                        Descricao = item.EventoSacado.Evento.instrucao
                    });
                    boletoBancario.Boleto = b;
                    boletoBancario.MostrarCodigoCarteira = true;
                    boletoBancario.Boleto.Valida();
          
                    boletoBancario.MostrarComprovanteEntrega = (Request.Url.Query == "?show");
                    div.Controls.Add(boletoBancario);
                }
                else
                {
                    var lb = new Label();
                    lb.Text = String.Format("Boleto de cobrança quitado! Data de Pagamento: {0:d} Valor Pago: {1:N2}", item.data_pgto, item.valor_pgto);
                    div.Controls.Add(lb);
                }  
                this.Controls.Add(div);
                div = new Panel();
                div.CssClass = "quebra";
            }
        }
    }
}
