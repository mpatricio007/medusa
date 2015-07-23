using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class Atuacao
    {
        [Key]
        public int id_atuacao { get; set; }

        [Required]
        [MaxLength(150)]
        public string nome { get; set; }

    }


    public class AtuacaoConfiguration : EntityTypeConfiguration<Atuacao>
    {
        public AtuacaoConfiguration()
        {
            Property(a => a.id_atuacao).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            ToTable("Atuacao");
        }
    }
}