using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class Edital
    {
        [Key]
        public int id_edital { get; set; }

        [Required]
        [MaxLength(300)]
        public string titulo { get; set; }

        public string edital_link { get; set; }

        public int id_vaga { get; set; }
        [ForeignKey("id_vaga")] 
        public virtual Vaga Vaga { get; set; }

        public bool status { get; set; }

        public DateTime data_publicacao { get; set; }
    }

    public class EditalConfiguration : EntityTypeConfiguration<Edital>
    {
        public EditalConfiguration()
        {
            ToTable("Edital");
        }
    }
}