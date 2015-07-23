using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class Lancamento
    {
        [Key]
        public int id_lancto { get; set; }

        public int id_projeto { get; set; }

        [ForeignKey("id_projeto")]
        public virtual Projeto Projeto { get; set; }

        public decimal valor { get; set; }

        public DateTime data_pagto { get; set; }

        public int id_lancto_tipo { get; set; }

        [ForeignKey("id_lancto_tipo")]
        public virtual LancamentoTipo tipo { get; set; }

        public virtual ICollection<LancamentoItem> Itens { get; set; }

        public int? id_imp { get; set; }

        [ForeignKey("id_imp")]
        public virtual Importacao Imp { get; set; }
    }

    public class LancamentoConfiguration : EntityTypeConfiguration<Lancamento>
    {
        public LancamentoConfiguration()
        {
            ToTable("Lancamento");
        }
    }
}
