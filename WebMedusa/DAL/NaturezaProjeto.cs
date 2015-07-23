using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class NaturezaProjeto
    {
        [Key]
        public int id_nat_projeto { get; set; }

        [Required]
        [MaxLength(50)]
        public string nome { get; set; }

    }


    public class NaturezaProjetoConfiguration : EntityTypeConfiguration<NaturezaProjeto>
    {
        public NaturezaProjetoConfiguration()
        {
            ToTable("NaturezaProjeto");
        }
    }
}