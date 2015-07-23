using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class ContratoBolsa : ContratoPessoaFisica        
    {
        public DateTime? inicio_seguro { get; set; }

        public DateTime? termino_seguro { get; set; }

        public int id_bolsa { get; set; }

        [ForeignKey ("id_bolsa")]
        public virtual Bolsa Bolsa { get; set; }
    }


    public class ContratoBolsaConfiguration : EntityTypeConfiguration<ContratoBolsa>
    {
        public ContratoBolsaConfiguration()
        {   
            ToTable("ContratoBolsa");
        }
    }
}