using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class vUsuariosDemonstrativo
    {
        //public int usuario_id { get; set; }

        [Key]        
        public string cpf { get; set; }

        public string nome { get; set; }

        public string  email { get; set; }
        
        public string perfil { get; set; }
        
        public string senha { get; set; }

        public virtual ICollection<vUsuariosProjetosDemonstrativo> vUsuariosProjetosDemonstrativo { get; set; }

    }


    public class vUsuariosDemonstrativoConfiguration : EntityTypeConfiguration<vUsuariosDemonstrativo>
    {
        public vUsuariosDemonstrativoConfiguration()
        {
            ToTable("vUsuariosDemonstrativo");
        }
    }
}