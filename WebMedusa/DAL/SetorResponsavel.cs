using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class SetorResponsavel
    {
        [Key]
        public int id_setor_resp { get; set; }

        [Required]
        public int id_projeto { get; set; }
        [ForeignKey("id_projeto")]
        public virtual Projeto Projeto { get; set; }

        [Required]
        public int id_setor { get; set; }
        [ForeignKey("id_setor")]
        public virtual Setor Setor { get; set; }
    }


    public class SetorResponsavelConfiguration : EntityTypeConfiguration<SetorResponsavel>
    {
        public SetorResponsavelConfiguration()
        {
            ToTable("SetorResponsavel");
        }
    }
}