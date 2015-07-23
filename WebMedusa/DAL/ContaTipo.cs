using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class ContaTipo
    {
        [Key]
        public int id_tipoconta { get; set; }

        [Required]
        [MaxLength(20)]
        public string descricao { get; set; }

        public bool conta_especifico { get; set; }

        public virtual ICollection<Conta> Conta { get; set; }
    }

    public class ContaTipoConfiguration : EntityTypeConfiguration<ContaTipo>
    {
        public ContaTipoConfiguration()
        {
            ToTable("ContaTipo");
        }
    }
}