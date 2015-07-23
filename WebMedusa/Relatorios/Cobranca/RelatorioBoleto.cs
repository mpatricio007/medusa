using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medusa.DAL;
using System.Threading.Tasks;

namespace Medusa.Relatorios.Cobranca
{
    public class RelatorioBoleto
    {
        public int projeto { get; set; }
        public string conta { get; set; }       
        public string sacado { get; set; }
        public DateTime? data_pgto { get; set; }
        public string id_boleto { get; set; }
        public decimal? valor_pgto { get; set; }
        public string evento { get; set; }
        public int num_parcela { get; set; }
        public DateTime data_vencto { get; set; }
        public decimal valor { get; set; }
        public decimal juros { get; set; }
        public RelatorioBoleto()
        {
        
        }

        public RelatorioBoleto(BoletoCobranca bol)
        {
            projeto = bol.EventoSacado.Evento.Projeto.codigo.GetValueOrDefault();
            conta = String.Format("Banco: {0} Ag.:{1}-{2} C/C:{3}-{4}", 
                bol.EventoSacado.Evento.Conta.BancoAgencia.Banco.nome,
                bol.EventoSacado.Evento.Conta.BancoAgencia.numero, 
                bol.EventoSacado.Evento.Conta.BancoAgencia.digito,
                bol.EventoSacado.Evento.Conta.numero, 
                bol.EventoSacado.Evento.Conta.digito);          
            sacado = bol.EventoSacado.Sacado.PessoaFisica.nome;
            data_pgto = bol.data_pgto;
            id_boleto = String.Format("{0:000000000000}", bol.id_boleto);
            valor_pgto = bol.valor_pgto;
            evento = bol.EventoSacado.Evento.descricao;
            num_parcela = bol.num_parcela;
            data_vencto = bol.data_vencto;
            valor = bol.valor;
            juros = bol.juros;
        }

        public IEnumerable<RelatorioBoleto> GetBoletos(DateTime de = new DateTime(), DateTime ate = new DateTime(), int id_evento = 0)
        {
            var l = new List<RelatorioBoleto>();
            var ctx = new Contexto();

            if ((id_evento == 0) & (de - ate == TimeSpan.Zero))
                ctx.BoletosCobrancas.Where(it => it.data_pgto >= de & it.data_pgto <= ate).ToList().ForEach(k => l.Add(new RelatorioBoleto(k)));
            else if ((id_evento == 0) & (de - ate != TimeSpan.Zero))
                ctx.BoletosCobrancas.Where(it => it.data_pgto >= de & it.data_pgto <= ate).ToList().ForEach(k => l.Add(new RelatorioBoleto(k)));

            else if ((id_evento != 0) & (de - ate != TimeSpan.Zero))
                ctx.BoletosCobrancas.Where(it => it.EventoSacado.id_evento == id_evento & it.data_pgto >= de & it.data_pgto <= ate).ToList().ForEach(k => l.Add(new RelatorioBoleto(k)));
            else
                ctx.BoletosCobrancas.Where(it => it.EventoSacado.id_evento == id_evento).ToList().ForEach(k => l.Add(new RelatorioBoleto(k)));
            return l;
        }
    }

       
}