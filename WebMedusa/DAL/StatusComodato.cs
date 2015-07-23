using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class StatusComodato
    {
        [Key]
        public int id_status_comodato { get; set; }

        public string nome { get; set; }

        public int? ordem { get; set; }
    }


    public class StatusComodatoConfiguration : EntityTypeConfiguration<StatusComodato>
    {
        public StatusComodatoConfiguration()
        {
            ToTable("StatusComodato");
        }
    }
}