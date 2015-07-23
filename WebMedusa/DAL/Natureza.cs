using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class Natureza
    {
        [Key]
        public int id_natureza { get; set; }

        [Required]
        [MaxLength(50)]
        public string nome { get; set; }

    }


    public class NaturezaConfiguration : EntityTypeConfiguration<Natureza>
    {
        public NaturezaConfiguration()
        {
            Property(n => n.id_natureza).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            ToTable("Natureza");
        }
    }
}