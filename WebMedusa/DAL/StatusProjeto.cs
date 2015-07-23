using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class StatusProjeto
    {
        [Key]
        public int id_status_projeto { get; set; }

        [Required]
        [MaxLength(50)]
        public string nome { get; set; }
    }


    public class StatusProjetoConfiguration : EntityTypeConfiguration<StatusProjeto>
    {
        public StatusProjetoConfiguration()
        {
            ToTable("StatusProjeto");
        }
    }
}