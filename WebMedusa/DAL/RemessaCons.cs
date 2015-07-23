using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using Medusa.LIB;
using System.ComponentModel.DataAnnotations.Schema;


namespace Medusa.DAL
{
    public class RemessaCons : Remessa
    {
        [ForeignKey("id_lote")]
        public virtual LoteCons Lote { get; set; }


        [Required]
        public string codbarra { get; set; }

        [NotMapped]
        public CodigoBarrasConsumo Guia
        {
            get
            {
                return new CodigoBarrasConsumo
                {
                    RepresentacaoNumerica = codbarra
                };
            }
            set { codbarra = value.RepresentacaoNumerica; }

        }

        [Required]
        public DateTime dataVencto { get; set; }
    }
    public class RemessaConsConfiguration : EntityTypeConfiguration<RemessaCons>
    {
        public RemessaConsConfiguration()
        {
            ToTable("RemessaCons");
        }
    }
}