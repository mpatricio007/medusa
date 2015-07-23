using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medusa.DAL;
using Medusa.BLL;
using Medusa.LIB;

namespace Medusa.Relatorios.RemessaBancaria
{
    public class RelatorioPagtosComOcorrencia
    {
        public int id_lote { get; set; }

        public int id_remessa { get; set; }

        public string benef_cedente { get; set; }

        public string data_pagto { get; set; }

        public decimal valor { get; set; }

        public string status { get; set; }

        public string forma_pagto { get; set; }

        public string descricao { get; set; }

        public int projeto { get; set; }

        public string conta { get; set; }

        public RelatorioPagtosComOcorrencia()
        { 
        }

        public RelatorioPagtosComOcorrencia(Remessa r)
        {
            id_lote = r.id_lote;

            id_remessa = r.id_remessa;

            benef_cedente = r.nome_fav_cedente;

            data_pagto = Util.DateToString(r.LoteRemessa.data_pgto.GetValueOrDefault());

            valor = r.valor;

            status = String.Format("{0} - {1}",r.TipoRetorno.codigo,r.TipoRetorno.descricao);

            forma_pagto = r.FormaPagto.nome;

            descricao = r.descricao;

            projeto = r.Projeto.codigo.GetValueOrDefault();

            conta = r.LoteRemessa.Conta.StrContaDigito;
        }

        public List<RelatorioPagtosComOcorrencia> GerarRelatorioComOcorrencia(int? de, int? ate, DateTime? dtProcessado, DateTime? dtPagtoDe, DateTime? dtPagtoAte)
        {
            var r = new RemessaBLL<Remessa>();
            List<RelatorioPagtosComOcorrencia> lst = new List<RelatorioPagtosComOcorrencia>();

            foreach (var item in r.GetComOcorrencias(de, ate, dtProcessado, dtPagtoDe,dtPagtoAte))
                lst.Add(new RelatorioPagtosComOcorrencia(item));

            return lst;
        }
    }
}