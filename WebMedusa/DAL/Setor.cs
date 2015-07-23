using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;


namespace Medusa.DAL
{
    [Serializable]
    public class Setor
    {
        [Key]
        public int id_setor { get; set; }

        [Required]
        [MaxLength(10)]
        public string sigla { get; set; }

        [Required]
        [MaxLength(50)]
        public string nome { get; set; }

        public virtual ICollection<UsuarioFusp> UsuarioFusp { get; set; }
  
    }

    public class SetorConfiguration : EntityTypeConfiguration<Setor>
    {
        public SetorConfiguration()
        {
            ToTable("Setor");
        }
    }
}