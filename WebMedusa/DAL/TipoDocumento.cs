using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class TipoDocumento
    {
        [Key]
        public int id_tipo_documento { get; set; }

        [Required]
        [MaxLength(50)]
        public string nome { get; set; }

        [Required]
        public bool status { get; set; }

    }


    public class TipoDocumentoConfiguration : EntityTypeConfiguration<TipoDocumento>
    {
        public TipoDocumentoConfiguration()
        {
            ToTable("TipoDocumento");
        }
    }
}