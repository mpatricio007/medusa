using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medusa.BLL;
using Medusa.LIB;
using Medusa.DAL;

namespace Medusa.Relatorios.REComodatos
{
    public class RelatorioPatrimonio
    {

        public string num_patrimonio { get; set; }

        public DateTime? data_nf { get; set; }

        public string num_nf { get; set; }

        public string descricao { get; set; }

        public string localizacao { get; set; }

        public decimal? valor { get; set; }

        public string num_comodato { get; set; }

        public int projeto { get; set; }


        PatrimonioBLL ptmBLL = new PatrimonioBLL();

        public RelatorioPatrimonio()
        {
        }

        public RelatorioPatrimonio(Patrimonio p)
        {
            num_patrimonio = p.num_patrimonio;
            data_nf = p.data_nf;
            num_nf = p.nf;
            descricao = p.descricao;
            localizacao = p.Unidade.nome;
            valor = p.valor.GetValueOrDefault();
            num_comodato = p.Comodato.num_comodato;
            projeto = p.Comodato.Projeto.codigo.GetValueOrDefault();
        }

        public IEnumerable<RelatorioPatrimonio> GerarRelatorio(int id_comodato)
        {
            var ctx = new Contexto();
            var p = new List<RelatorioPatrimonio>();
            foreach (var item in ctx.Patrimonios.Where(it => it.id_comodato == id_comodato).OrderBy(it => it.id_patrimonio).ToList())
                p.Add(new RelatorioPatrimonio(item));
            return p;
        }
    }
}

