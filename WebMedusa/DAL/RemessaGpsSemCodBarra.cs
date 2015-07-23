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
    public class RemessaGpsSemCodBarra : Remessa
    {
        [ForeignKey("id_lote")]
        public virtual LoteGPS Lote { get; set; }


        [Required]
        public string cod_receita { get; set; }

        [Required]
        public string id_contribuinte { get; set; }

        public string mes_ano { get; set; }

        [Required]
        public Decimal valor_gps { get; set; }

        [Required]
        public Decimal outras_entidades { get; set; }

        [Required]
        public Decimal atual_monetaria { get; set; }
                
        [Required]
        public DateTime dataVencto { get; set; }
    }
    public class RemessaGpsSemCodBarraConfiguration : EntityTypeConfiguration<RemessaGpsSemCodBarra>
    {
        public RemessaGpsSemCodBarraConfiguration()
        {
            ToTable("RemessaGpsSemCodBarra");
        }
    }
}