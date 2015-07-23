using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class LancamentoItem
    {
        [Key]
        public int id_lancto_item { get; set; }

        public string codigo { get; set; }

        public string descricao { get; set; }         

        public decimal debito { get; set; }

        public decimal credito { get; set; }

        public decimal debitoProjeto { get; set; }

        public decimal creditoProjeto { get; set; }

        public decimal valorDeducao { get; set; }

        public int id_projeto { get; set; }

        [ForeignKey("id_projeto")]
        public virtual Projeto projeto { get; set; }

        public int id_lancto { get; set; }

        [ForeignKey("id_lancto")]
        public virtual Lancamento lancto { get; set; }
    }

    public class LancamentoItemConfiguration : EntityTypeConfiguration<LancamentoItem>
    {
        public LancamentoItemConfiguration()
        {
            ToTable("LancamentoItem");
        }
    }
}
