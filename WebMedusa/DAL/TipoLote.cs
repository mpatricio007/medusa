using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    [Serializable]
    public class TipoLote
    {
        [Key]
        public int id_tipo_lote { get; set; }

        [Required]
        [MaxLength(50)]
        public string nome { get; set; }

        [Required]
        public int id_banco { get; set; }

        [ForeignKey("id_banco")]
        public virtual Banco Banco { get; set; }

    }

    public class TipoLoteConfiguration : EntityTypeConfiguration<TipoLote>
    {
        public TipoLoteConfiguration()
        {
            ToTable("TipoLote");
        }
    }
}