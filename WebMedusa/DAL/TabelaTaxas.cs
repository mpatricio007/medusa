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
    public class TabelaTaxas
    {
        [Key]
        public int id_tabela { get; set; }

        public DateTime data_ini { get; set; }

        public DateTime data_fim { get; set; }

        public bool cumulativo_mensal { get; set; }
        
        public int id_taxa { get; set; }

        [ForeignKey("id_taxa")]
        public virtual Taxa taxa { get; set; }        

        public virtual ICollection<FaixaTaxas> faixas { get; set; }

        public virtual ICollection<BaseCalculoDedutivel> dedutiveis { get; set; }
    }


    public class TabelaTaxasConfiguration : EntityTypeConfiguration<TabelaTaxas>
    {
        public TabelaTaxasConfiguration()
        {
            ToTable("TabelaTaxas");
        }
    }
}