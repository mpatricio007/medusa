using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class EmprestimoVolume
    {
        [Key]
        public int id_emprestimo { get; set; }

        [Required]
        public DateTime dt_retirada { get; set; }
                
        public DateTime? dt_devolucao { get; set; }

        [Required]
        public int id_usuario_retirada { get; set; }

        [ForeignKey("id_usuario_retirada")]
        public virtual UsuarioFusp usuario_retirada { get; set; }

        public int? id_usuario_devolucao { get; set; }

        [ForeignKey("id_usuario_devolucao")]
        public virtual UsuarioFusp usuario_devolucao { get; set; }
             
        [MaxLength(200)]
        public string observacao { get; set; }

        [Required]
        public int id_volume { get; set; }

        [ForeignKey("id_volume")]
        public virtual Volume volume { get; set; }

    }


    public class EmprestimoVolumeConfiguration : EntityTypeConfiguration<EmprestimoVolume>
    {
        public EmprestimoVolumeConfiguration()
        {
            HasOptional(e => e.usuario_devolucao).WithMany().HasForeignKey(e => e.id_usuario_devolucao).WillCascadeOnDelete(false);
            HasRequired(e => e.usuario_retirada).WithMany().HasForeignKey(e => e.id_usuario_retirada).WillCascadeOnDelete(false);
            ToTable("EmprestimoVolume");
        }
    }
}