using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class InstrumentoContratual
    {
        [Key]
        public int id_instrumento_contratual { get; set; }

        [Required]
        [MaxLength(50)]
        public string nome { get; set; }

        [Required]
        public bool status { get; set; }

    }


    public class InstrumentoContratualConfiguration : EntityTypeConfiguration<InstrumentoContratual>
    {
        public InstrumentoContratualConfiguration()
        {
            ToTable("InstrumentoContratual");
        }
    }
}