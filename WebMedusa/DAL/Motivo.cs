using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class Motivo
    {
        [Key]
        public int id_motivo { get; set; }

        public int? id_motivo_pai { get; set; }

        [ForeignKey("id_motivo_pai")]
        public Motivo Motivo_pai { get; set; }

        public string descricao_motivo { get; set; }

        public bool temOutros { get; set; }

        public virtual ICollection<Motivo> MotivosFilhos { get; set; }

        public string codigo { get; set; }

        [NotMapped]
        public string item
        {
            get
            {
                return String.Format("{0} - {1}", codigo, descricao_motivo);
            }

        }
    }


    public class MotivoConfiguration : EntityTypeConfiguration<Motivo>
    {
        public MotivoConfiguration()
        {
            ToTable("Motivo");
        }
    }
}