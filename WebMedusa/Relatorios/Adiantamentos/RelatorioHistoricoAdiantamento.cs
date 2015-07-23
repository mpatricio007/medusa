using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using Medusa.BLL;
using Medusa.DAL;

namespace Medusa.Relatorios.Adiantamentos
{
    public class RelatorioHistoricoAdiantamento
    {
        public DateTime data { get; set; }

        public string observacao { get; set; }

        public string status { get; set; }

        public string usuario { get; set; }

        

        public RelatorioHistoricoAdiantamento()
        {
        }

        public RelatorioHistoricoAdiantamento(HistoricoAdiantamento ha)
        {
            data = ha.data;
            observacao = ha.observacao;
            status = ha.statusAdiantamento.nome;
            usuario = ha.Usuario.PessoaFisica.nome;
        }

        public IEnumerable<RelatorioHistoricoAdiantamento> GetHistoricoAdiantamento(int id_adiantamento)
        {
            var lst = new List<RelatorioHistoricoAdiantamento>();
            var ctx = new Contexto();
            foreach (var item in ctx.HistoricoAdiantamentos.Where(it => it.id_adiantamento == id_adiantamento).ToList())
                lst.Add(new RelatorioHistoricoAdiantamento(item));
            
            return lst;
        }
    }
}