using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class TipoRetorno
    {
        [Key]
        public int id_tipo_ret { get; set; }

        [Required]
        [MaxLength(10)]
        public string codigo { get; set; }

        [Required]
        [MaxLength(80)]
        public string descricao { get; set; }

        [Required]
        public int id_banco { get; set; }

        [ForeignKey("id_banco")]
        public virtual Banco Banco { get; set; }

        public bool somar_rejeitados { get; set; }
    }
        
    public class TipoRetornoConfiguration : EntityTypeConfiguration<TipoRetorno>
    {
        public TipoRetornoConfiguration()
        {
            HasRequired(it => it.Banco).WithMany().HasForeignKey(it => it.id_banco).WillCascadeOnDelete(false);
            ToTable("TipoRetorno");          
        }
    }
}