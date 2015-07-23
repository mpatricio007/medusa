using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class Providencia
    {
        [Key]
        public int id_providencia { get; set; }

        [MaxLength(50)]
        public string nome { get; set; }

        public int id_status_final { get; set; }
        [ForeignKey("id_status_final")]
        public virtual StatusEntrada StatusFinal { get; set; }

        private ICollection<SetorCompetente> setoresCompetentes;

        [Invisible]
        public virtual ICollection<SetorCompetente> SetoresCompetentes
        {
            get
            {
                if (setoresCompetentes == null)
                    setoresCompetentes = new List<SetorCompetente>();
                return setoresCompetentes;
            }
            set { setoresCompetentes = value; }
        }
    }


    public class ProvidenciaConfiguration : EntityTypeConfiguration<Providencia>
    {
        public ProvidenciaConfiguration()
        {
            ToTable("Providencia");
        }
    }
}