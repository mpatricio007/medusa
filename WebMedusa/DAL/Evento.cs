using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class Evento
    {
        [Key]
        public int id_evento { get; set; }

        [MaxLength(50)]
        public string nome { get; set; }

        public string descricao { get; set; }

        public string instrucao { get; set; }

        public int id_projeto { get; set; }

        [ForeignKey("id_projeto")]
        public virtual Projeto Projeto { get; set; }

        public int id_conta { get; set; }

        [ForeignKey("id_conta")]
        public virtual Conta Conta { get; set; }

        private ICollection<EventoSacado> eventosSacados;

        public virtual ICollection<EventoSacado> EventosSacados
        {
            get
            {
                if (eventosSacados == null)
                    eventosSacados = new List<EventoSacado>();
                return eventosSacados;
            }
            set { eventosSacados = value; }
        }
    }

    public class EventoConfiguration : EntityTypeConfiguration<Evento>
    {
        public EventoConfiguration()
        {
            ToTable("Evento");
        }
    }
}