using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medusa.BLL;
using Medusa.DAL;
using Medusa.LIB;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Medusa.Relatorios.Recibos
{
    public class RelatorioReciboFusp
    {
        public int num_recibo { get; set; }

        public string recebemos_de { get; set; }

        public DateTime data { get; set; }

        public string tipo_pagamento { get; set; }

        public string referente_a { get; set; }

        [MaxLength(200)]
        public decimal valor { get; set; }

        public string valorPorExtenso { get; set; }

        public string usuario { get; set; }

        public string obs { get; set; }

        public int? projeto { get; set; }

        public bool cancelado { get; set; }

        public string tipoDocto { get; set; }

        public string strStatus { get; set; }

        ReciboFuspBLL refBLL = new ReciboFuspBLL();
        public RelatorioReciboFusp()
        {
        }

        public RelatorioReciboFusp(ReciboFusp rf)
        {
            num_recibo = rf.id_recibo_fusp;
            recebemos_de = rf.nome;
            data = rf.data;
            referente_a = rf.descricao;
            tipo_pagamento = rf.tipo_pagamento;
            valor = rf.valor;
            usuario = rf.Usuario.PessoaFisica.nome;
            valorPorExtenso = rf.valor.ExtensoEmReal();
            obs = rf.observacao;
            cancelado = rf.status_recibo;
            tipoDocto = rf.strDocumentos;
            projeto = rf.Projeto.codigo;
            strStatus = !rf.status_recibo ? "* RECIBO CANCELADO *" : String.Empty;
        }

        public IEnumerable<RelatorioReciboFusp> GerarRelatorioFusp(int de, int ate)
        {
            var ctx = new Contexto();
            var r = new List<RelatorioReciboFusp>();
            foreach (var item in ctx.RecibosFusp.Where(it => it.id_recibo_fusp >= de & it.id_recibo_fusp <= ate).OrderBy(it => it.id_recibo_fusp).ToList())
                r.Add(new RelatorioReciboFusp(item));
            return r;
        }

        public IEnumerable<RelatorioReciboFusp> GetRelatorio(int id_recibo_fusp)
        {
            var lst = new List<RelatorioReciboFusp>();
            refBLL.Get(id_recibo_fusp);
            lst.Add(new RelatorioReciboFusp(refBLL.ObjEF));
            return lst;
        }

    }
}

