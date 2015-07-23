using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class PossivelProvidencia
    {
        [Key]
        public int id_possivel_providencia { get; set; }

        public int id_providencia { get; set; }
        [ForeignKey("id_providencia")]
        public virtual Providencia Providencia { get; set; }

        public int id_status_atual { get; set; }
        [ForeignKey("id_status_atual")]
        public virtual StatusEntrada StatusAtual { get; set; }

        [NotMapped]
        public string strProvidencia 
        {
            get 
            {
                return Providencia.nome;
            }
        }
    }

    public class PossivelProvidenciaConfiguration : EntityTypeConfiguration<PossivelProvidencia>
    {
        public PossivelProvidenciaConfiguration()
        {
            ToTable("PossivelProvidencia");
        }
    }
}