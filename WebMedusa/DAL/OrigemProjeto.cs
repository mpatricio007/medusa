using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class OrigemProjeto
    {
        [Key]
        public int id_origem { get; set; }

        [Required]
        [MaxLength(30)]
        public string descricao { get; set; }

    }


    public class OrigemProjetoConfiguration : EntityTypeConfiguration<OrigemProjeto>
    {
        public OrigemProjetoConfiguration()
        {
            ToTable("OrigemProjeto");
        }
    }
}