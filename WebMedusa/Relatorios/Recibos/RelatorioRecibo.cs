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
    public class RelatorioRecibo
    {
        public int num_recibo { get; set; }

        public string recebemos_de { get; set; }

        public DateTime data { get; set; }

        public string tipo_pagamento { get; set; }

        public string referente_a { get; set; }

        [MaxLength(200)]
        public decimal valor { get; set; }

        public string valorPorExtenso { get; set; }

        public string curso { get; set; }

        public string usuario { get; set; }

        public string obs { get; set; }

        public int? projeto { get; set; }

        public bool cancelado { get; set; }

        public string tipoDocto { get; set; }

        public string strStatus { get; set; }

        ReciboBLL reBLL = new ReciboBLL();
        public RelatorioRecibo()
        {
        }

        public RelatorioRecibo(Recibo r)
        {
            num_recibo = r.id_recibo;
            recebemos_de = r.nome;
            data = r.data;
            referente_a = r.descricao;
            tipo_pagamento = r.tipo_pagamento;
            valor = r.valor;
            curso = r.ReciboCurso.nome;
            usuario = r.Usuario.nome;
            valorPorExtenso = r.valor.ExtensoEmReal();
            obs = r.observacao;
            projeto = r.ReciboCurso.Projeto.codigo;
            cancelado = r.status_recibo.Value;
            tipoDocto = r.strDocumentos;
            strStatus = !r.status_recibo.GetValueOrDefault() ? "* RECIBO CANCELADO *" : String.Empty;
        }

        public IEnumerable<RelatorioRecibo> GerarRelatorio(int de, int ate)
        {
            var ctx = new Contexto();
            var r = new List<RelatorioRecibo>();
            foreach (var item in ctx.Recibos.Where(it => it.id_recibo >= de & it.id_recibo <= ate).OrderBy(it => it.id_recibo).ToList())
                r.Add(new RelatorioRecibo(item));
            return r;
        }

        public IEnumerable<RelatorioRecibo> GetRelatorio(int id_recibo)
        {
            var lst = new List<RelatorioRecibo>();
            reBLL.Get(id_recibo);
            lst.Add(new RelatorioRecibo(reBLL.ObjEF));
            return lst;
        }

    }
}

