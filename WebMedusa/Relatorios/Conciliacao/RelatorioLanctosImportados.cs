using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medusa.DAL;


namespace Medusa.Relatorios.Conciliacao
{
    public class RelatorioLanctosImportados
    {
        public string descricaoLancto { get; set; }
        public DateTime? data { get; set; }
        public string DC { get; set; }
        public decimal valor { get; set; }
        public string numeroConta { get; set; }
        public string nomeBanco { get; set; }


        public IEnumerable<RelatorioLanctosImportados> GerarRelatorio(Int32 id_imparq)
        {
            Contexto ctx = new Contexto();
            var saida = from lct in ctx.ContaLanctos
                        where lct.id_imparq==id_imparq
                        select new RelatorioLanctosImportados
                        {
                            descricaoLancto = lct.descricao,
                            data = lct.data,
                            DC = lct.TipoLcto.dc,
                            valor = lct.valor,
                            numeroConta = lct.Conta.numero + " " + lct.Conta.digito,
                            nomeBanco = lct.Conta.BancoAgencia.Banco.nome
                        };
            return saida.ToList();
        }




    }
}