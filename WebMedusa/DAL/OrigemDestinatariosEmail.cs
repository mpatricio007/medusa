using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;


namespace Medusa.DAL
{
    public class OrigemDestinatariosEmail
    {
        [Key]
        public Int64 id { get; set; }
        public string tipo { get; set; }
        public string nome { get; set; } 
        public string email { get; set; }
    }

    public class OrigemDestinatariosEmailConfiguration : EntityTypeConfiguration<OrigemDestinatariosEmail>
    {
        public OrigemDestinatariosEmailConfiguration()
        {
            ToTable("vEmails");
        }
    }

}