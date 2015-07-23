using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class EventoSacado
    {
        [Key]
        public int id_evento_sacado { get; set; }

        public int id_evento { get; set; }
        
        [ForeignKey("id_evento")]        
        public virtual Evento Evento { get; set; }

        public int id_sacado { get; set; }
                
        [ForeignKey("id_sacado")]
        public virtual Sacado Sacado { get; set; }

        private ICollection<BoletoCobranca> boletos;
        
        public virtual ICollection<BoletoCobranca> Boletos
        {
            get
            {
                if (boletos == null)
                    boletos = new List<BoletoCobranca>();
                return boletos;
            }
            set { boletos = value; }
        }
    }

    public class EventoSacadoConfiguration : EntityTypeConfiguration<EventoSacado>
    {
        public EventoSacadoConfiguration()
        {
            ToTable("EventoSacado");
        }
    }
}