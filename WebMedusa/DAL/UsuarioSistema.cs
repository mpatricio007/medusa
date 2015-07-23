using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class UsuarioSistema
    {
        [Key]
        public int id_usuario_sistema { get; set; }

        [Required]        
        public int id_usuario { get; set; }

        [ForeignKey("id_usuario")]
        public virtual Usuario Usuario { get; set; }

        [Required]        
        public int id_sistema { get; set; }

        [ForeignKey("id_sistema")]
        public virtual Sistema Sistema { get; set; }




    }

    public class UsuarioSistemaConfiguration : EntityTypeConfiguration<UsuarioSistema>
    {
        public UsuarioSistemaConfiguration()
        {
            ToTable("UsuarioSistema");
        }
    }
}