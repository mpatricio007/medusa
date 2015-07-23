using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class TipoContrato
    {
        [Key]
        public int id_tipo_contrato { get; set; }

        public string nome { get; set; }

        public bool status { get; set; }

    }


    public class TipoContratoConfiguration : EntityTypeConfiguration<TipoContrato>
    {
        public TipoContratoConfiguration()
        {
           ToTable("TipoContrato");
        }
    }
}