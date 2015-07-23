using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class vUsuariosProjetosDemonstrativo
    {
        //[Key]
        public string cpf { get; set; }


        [ForeignKey("cpf")]
        public virtual vUsuariosDemonstrativo vUsuariosDemonstrativo { get; set; }

        //[Key]
        public int id_projeto { get; set; }

        [ForeignKey("id_projeto")]
        public virtual Projeto Projeto { get; set; }
    }

    public class vUsuariosProjetosDemonstrativoConfiguration : EntityTypeConfiguration<vUsuariosProjetosDemonstrativo>
    {
        public vUsuariosProjetosDemonstrativoConfiguration()
        {
            HasKey(it => new { it.cpf, it.id_projeto });
            ToTable("vUsuariosProjetosDemonstrativo");
        }
    }
}