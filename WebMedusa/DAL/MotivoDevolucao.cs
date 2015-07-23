using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class MotivoDevolucao
    {
        [Key]
        public int id_motivo_devolucao { get; set; }

        public int id_motivo { get; set; }

        [ForeignKey("id_motivo")]
        public virtual Motivo Motivo { get; set; }

        public int id_devolucao { get; set; }

        [ForeignKey("id_devolucao")]
        public virtual Devolucao Devolucao { get; set; }

        public string obs { get; set; }
    }


    public class MotivoDevolucaoConfiguration : EntityTypeConfiguration<MotivoDevolucao>
    {
        public MotivoDevolucaoConfiguration()
        {
            ToTable("MotivoDevolucao");
        }
    }
}