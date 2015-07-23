using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel;

namespace Medusa.DAL
{
    public class Bolsa
    {
        [Key]
        public int id_bolsa { get; set; }

        [Required]
        [MaxLength(20)]
        public string sigla { get; set; }

        [Required]
        [MaxLength(50)]
        public string nome { get; set; }

        [Required]
        public bool status { get; set; }

        [Search("nº de horas")]
        public int num_horas { get; set; }
    }


    public class BolsaConfiguration : EntityTypeConfiguration<Bolsa>
    {
        public BolsaConfiguration()
        {
            //Property(m => m.id_bolsa).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            ToTable("Bolsa");
        }
    }
}