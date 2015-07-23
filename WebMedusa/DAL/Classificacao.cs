using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class Classificacao
    {
        [Key]
        public int id_classificacao { get; set; }

        [Required]
        [MaxLength(50)]
        public string nome { get; set; }

    }


    public class ClassificacaoConfiguration : EntityTypeConfiguration<Classificacao>
    {
        public ClassificacaoConfiguration()
        {
            Property(c => c.id_classificacao).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            ToTable("Classificacao");
        }
    }
}