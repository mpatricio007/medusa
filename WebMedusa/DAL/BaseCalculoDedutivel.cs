using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class BaseCalculoDedutivel
    {
        [Key]
        public int id_bc_dedutivel { get; set; }

        public int id_tabela { get; set; }

        [ForeignKey("id_tabela")]
        public virtual TabelaTaxas tabela { get; set; }

        public int id_taxa { get; set; }

        [ForeignKey("id_taxa")]
        public virtual Taxa taxa { get; set; }
    }


    public class BaseCalculoDedutivelConfiguration : EntityTypeConfiguration<BaseCalculoDedutivel>
    {
        public BaseCalculoDedutivelConfiguration()
        {
            ToTable("BaseCalculoDedutivel");
        }
    }
}