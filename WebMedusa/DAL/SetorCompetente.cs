using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class SetorCompetente
    {
        [Key]
        public int id_setor_competente { get; set; }

        public int id_providencia { get; set; }
        [ForeignKey("id_providencia")]
        public virtual Providencia ProvidenciaDeCompetencia { get; set; }

        public int id_setor { get; set; }
        [ForeignKey("id_setor")]
        public virtual Setor Setor { get; set; }
    }


    public class SetorCompetenteConfiguration : EntityTypeConfiguration<SetorCompetente>
    {
        public SetorCompetenteConfiguration()
        {
            ToTable("SetorCompetente");
        }
    }
}