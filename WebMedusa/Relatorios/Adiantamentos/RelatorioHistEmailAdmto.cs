using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medusa.DAL;

namespace Medusa.Relatorios.Adiantamentos
{
    public class RelatorioHistEmailAdmto
    {
        public string usuario { get; set; }

        public DateTime? data { get; set; }

        public string assunto { get; set; }

        public RelatorioHistEmailAdmto()
        { 
        }

        public RelatorioHistEmailAdmto(HistoricoEmailAdmto he)
        {
            usuario = he.Usuario.PessoaFisica.nome;
            data = he.data.GetValueOrDefault();
            assunto = he.assunto;
        }

        public IEnumerable<RelatorioHistEmailAdmto> GetHistoricoEmailAdmto(int id_adiantamento)
        {
            var lst = new List<RelatorioHistEmailAdmto>();
            var ctx = new Contexto();
            foreach (var item in ctx.HistoricoEmailAdmtos.Where(it => it.id_adiantamento == id_adiantamento).ToList())
                lst.Add(new RelatorioHistEmailAdmto(item));
            return lst;
        }
    }
}