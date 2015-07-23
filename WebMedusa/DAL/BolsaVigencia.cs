using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class BolsaVigencia
    {
        [Key]
        public int id_bolsa_vigencia { get; set; }
                
        [Required]
        public int id_bolsa { get; set; }

        [ForeignKey("id_bolsa")]
        public virtual Bolsa bolsa { get; set; }

        [Required]
        public DateTime inicio { get; set; }

        [Required]
        public DateTime termino { get; set; }

        [Required]
        public bool status { get; set; }

        [Required]
        public decimal valor { get; set; }
    }
 

    public class BolsaVigenciaConfiguration : EntityTypeConfiguration<BolsaVigencia>
    {
        public BolsaVigenciaConfiguration()
        {
            ToTable("BolsaVigencia");
        }
    }
}