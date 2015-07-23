using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{    
    public class UsuarioFusp : Usuario
    { 

        [MaxLength(3)]
        public string ramal { get; set; }
                
        public int id_setor { get; set; }

        [ForeignKey("id_setor")]
        public virtual Setor Setor { get; set; }

        [MaxLength(10)]
        public string chapa { get; set; }

        public virtual ICollection<UsuarioTipoLcto> UsuarioTipoLcto { get; set; }
    }

    public class UsuarioFuspConfiguration : EntityTypeConfiguration<UsuarioFusp>
    {
        public UsuarioFuspConfiguration()
        {            
            ToTable("UsuarioFusp");
        }
    }
}