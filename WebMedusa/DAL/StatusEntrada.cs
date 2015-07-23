using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using Medusa.DAL;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class StatusEntrada
    {
        [Key]
        public int id_status_entrada { get; set; }

        public string nome { get; set; }

        public bool escolhe_destinatarios { get; set; }

        public int ordem { get; set; }

        public bool exige_protocolo { get; set; }

        private ICollection<PossivelProvidencia> possiveisProvidencias;

        [Invisible]
        public virtual ICollection<PossivelProvidencia> PossiveisProvidencias
        {
            get
            {
                if (possiveisProvidencias == null)
                    possiveisProvidencias = new List<PossivelProvidencia>();
                return possiveisProvidencias;
            }
            set { possiveisProvidencias = value; }
        }
    }


    public class StatusEntradaConfiguration : EntityTypeConfiguration<StatusEntrada>
    {
        public StatusEntradaConfiguration()
        {
            ToTable("StatusEntrada");
        }
    }
}