using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class Moeda
    {
        [Key]
        public int id_moeda { get; set; }

        [Required]
        [MaxLength(10)]
        public string sigla { get; set; }

        [Required]
        [MaxLength(50)]
        public string nome { get; set; }
    }


    public class MoedaConfiguration : EntityTypeConfiguration<Moeda>
    {
        public MoedaConfiguration()
        {
            Property(m => m.id_moeda).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            ToTable("Moeda");
        }
    }
}