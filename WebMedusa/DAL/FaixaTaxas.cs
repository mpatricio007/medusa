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
    public class FaixaTaxas
    {
        [Key]
        public int id_faixa_taxa { get; set; }

        public decimal faixa_de { get; set; }

        public decimal faixa_ate { get; set; }

        public decimal? valor_max { get; set; }

        public decimal deducao { get; set; }

        public int id_tabela { get; set; }

        [ForeignKey("id_tabela")]
        public virtual TabelaTaxas TabelaImpostos { get; set; }

        public decimal aliquota { get; set; }

        public decimal vlr_minimo { get; set; }
    }


    public class FaixaTaxasConfiguration : EntityTypeConfiguration<FaixaTaxas>
    {
        public FaixaTaxasConfiguration()
        {
            ToTable("FaixaTaxas");
        }
    }
}