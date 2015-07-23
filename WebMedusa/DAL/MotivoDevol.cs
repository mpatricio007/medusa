using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class MotivoDevol
    {
        [Key]
        public int id_motivo { get; set; }

        [Required]
        [MaxLength(50)]
        public string descricao { get; set; }

    }

    public class MotivoDevolConfiguration : EntityTypeConfiguration<MotivoDevol>
    {
        public MotivoDevolConfiguration()
        {
            ToTable("MotivoDevol");
        }

    }
}
