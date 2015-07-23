using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medusa.DAL;
using Medusa.BLL;

namespace Medusa.Relatorios.Conciliacao
{
    public class RelatorioLanctoTipo
    {

        public int id_tipo_lcto { get; set; }
        public string descricaoLancto { get; set; }
        public string descricaoTipoLcto { get; set; }
        public DateTime? data { get; set; }
        public string DC { get; set; }
        public decimal valor { get; set; }
        public string numeroConta { get; set; }
        public string nomeBanco { get; set; }
        public int? projeto { get; set; }
        public string num_nota_fiscal { get; set; }
        public string num_projeto { get; set; }

        public IEnumerable<RelatorioLanctoTipo> GerarRelatorio(List<ListaTiposLcto> l, DateTime di, DateTime df)
        {
            Contexto ctx = new Contexto();
            var ds = from t in l
                     select t.id_tipo_lcto;
            var saida = from lct in ctx.ContaLanctos
                        orderby lct.data
                        where ds.Contains(lct.id_tipo_lcto) & lct.data >= di & lct.data <= df
                        select new RelatorioLanctoTipo
                        {
                            id_tipo_lcto = lct.id_tipo_lcto,
                            descricaoLancto = lct.descricao,
                            descricaoTipoLcto = lct.TipoLcto.descricao,
                            data = lct.data,
                            DC = lct.TipoLcto.dc,
                            valor = lct.TipoLcto.dc == "C" ? lct.valor : (-1) * lct.valor,
                            numeroConta = lct.Conta.numero + " " + lct.Conta.digito,
                            projeto = lct.Conta.Projeto.codigo,
                            nomeBanco = lct.Conta.BancoAgencia.Banco.nome,
                            num_nota_fiscal = lct.num_documento,
                            num_projeto = lct.proj_num,
                        };
            return saida.ToList();
        }
    }
}