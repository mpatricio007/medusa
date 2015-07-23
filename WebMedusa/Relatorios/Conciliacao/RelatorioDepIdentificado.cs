using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medusa.DAL;
using Medusa.BLL;

namespace Medusa.Relatorios.Conciliacao
{
    public class RelatorioDepIdentificado
    {
        public int id_tipo_lcto { get; set; }
        public string descricaoLancto { get; set; }
        public string descricaoTipoLcto { get; set; }
        public DateTime? data { get; set; }
        public string DC { get; set; }
        public decimal valor { get; set; }
        public string numeroConta { get; set; }
        public string nomeBanco { get; set; }
        public string numeroDocto { get; set; }
        public int? projeto { get; set; }

        public IEnumerable<RelatorioDepIdentificado> GerarRelatorio(DateTime di, DateTime df)
        {
            Contexto ctx = new Contexto();


            var saida = from lct in ctx.ContaLanctos
                        join i in ctx.IdentificadorDepositos
                        on lct.num_documento equals i.num_identificador
                        //into lj from i in lj.DefaultIfEmpty()
                        where lct.data >= di & lct.data <= df & lct.TipoLcto.dc == "C"
                        select new RelatorioDepIdentificado
                        {
                            id_tipo_lcto = lct.id_tipo_lcto,
                            descricaoLancto = lct.descricao,
                            descricaoTipoLcto = lct.TipoLcto.descricao,
                            data = lct.data,
                            DC = lct.TipoLcto.dc,
                            valor = lct.valor,
                            numeroConta = lct.Conta.numero + " " + lct.Conta.digito,
                            nomeBanco = lct.Conta.BancoAgencia.Banco.nome,
                            numeroDocto = lct.num_documento,
                            projeto = i.cod_def_projeto
                        };
            return saida.ToList();
        }

    }
}