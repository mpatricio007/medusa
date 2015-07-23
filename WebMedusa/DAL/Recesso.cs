using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class Recesso
    {
        [Key]
        public int id_recesso { get; set; }

        public DateTime data { get; set; }

        public string descricao { get; set; }

        public string tipo { get; set; }
    }


    public class RecessoConfiguration : EntityTypeConfiguration<Recesso>
    {
        public RecessoConfiguration()
        {
            ToTable("Recesso");
        }
    }
}