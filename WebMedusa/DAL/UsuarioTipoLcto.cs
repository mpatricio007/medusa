using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class UsuarioTipoLcto
    {
        [Key]
        public int id_usuario_tipolcto { get; set; }

        [Required]
        public int id_usuario { get; set; }

        [ForeignKey("id_usuario")]
        public virtual UsuarioFusp Usuario { get; set; }

        [Required]
        public int id_tipo_lcto { get; set; }

        [ForeignKey("id_tipo_lcto")]
        public virtual TipoLcto TipoLcto { get; set; }
    }

    public class UsuarioTipoLctoConfiguration : EntityTypeConfiguration<UsuarioTipoLcto>
    {
        public UsuarioTipoLctoConfiguration()
        {
            ToTable("UsuarioTipoLcto");
        }
    }
}