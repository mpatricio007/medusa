using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class Taxa
    {
        [Key]
        public int id_taxa { get; set; }

        public string nome { get; set; }

        public virtual ICollection<TabelaTaxas> Tabelas { get; set; }

        [NotMapped]
        public int? id_plano_conta { get; set; }
          
        public virtual PlanoConta PlanoConta { get; set; }
    }

    public class TaxaConfiguration : EntityTypeConfiguration<Taxa>
    {
        public TaxaConfiguration()
        {
            HasOptional<PlanoConta>(u => u.PlanoConta).WithOptionalDependent(c => c.Taxa).Map(p => p.MapKey("id_plano_conta"));
            ToTable("Taxa");
        }
    }
}