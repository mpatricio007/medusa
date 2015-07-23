using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class LancamentoTipo
    {
        [Key]
        public int id_lancto_tipo { get; set; }

        public string nome { get; set; }

        public bool status { get; set; }

        public int? id_pagina { get; set; }

        [ForeignKey("id_pagina")]
        public virtual Pagina Pagina { get; set; }
                
        public virtual ICollection<Lancamento> lanctos { get; set; }

        public virtual ICollection<PlanoContaTipoLancamento> PlanoContaTipoLanctos { get; set; }
    }

    public class LancamentoTipoConfiguration : EntityTypeConfiguration<LancamentoTipo>
    {
        public LancamentoTipoConfiguration()
        {
            ToTable("LancamentoTipo");
        }
    }
}