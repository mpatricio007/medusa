using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class TipoLcto
    {
        [Key]
        public int id_tipo_lcto { get; set; }

        [Required]
        [MaxLength(10)]
        public string codigo { get; set; }

        [Required]
        [MaxLength(1)]
        public string dc { get; set; }

        [Required]
        [MaxLength(40)]
        public string descricao { get; set; }
        
        [Required]
        public bool pertenceAdmin { get; set; }

        [Required]
        public bool importar { get; set; }

        [Required]
        public int id_banco { get; set; }

        [ForeignKey("id_banco")]
        public virtual Banco Banco { get; set; }
        
        public virtual ICollection<ContaLancto> ContaLancto { get; set; }

        private ICollection<UsuarioTipoLcto> usuarioTipoLcto;
        public virtual ICollection<UsuarioTipoLcto> UsuarioTipoLcto 
        { 
            get
            {
                if (usuarioTipoLcto == null) usuarioTipoLcto = new List<UsuarioTipoLcto>();
                return usuarioTipoLcto;
            } 
            set
            {
                usuarioTipoLcto = value;
            } 
        }

        [NotMapped]
        public string StrTipoLcto 
        {
            get
            {
                return String.Format("{0} - {1}", dc, descricao);
            }
            
        }
    }

    public class TipoLctoConfiguration : EntityTypeConfiguration<TipoLcto>
    {
        public TipoLctoConfiguration()
        {            
            ToTable("TipoLcto");          
        }
    }
}