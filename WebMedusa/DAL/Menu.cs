using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class Menu
    {
        [Key]
        public int id_menu { get; set; }

        [Required]
        [MaxLength(50)]
        public string descricao { get; set; }

        [Required]
        [MaxLength(50)]
        public string nome { get; set; }

        [Required]        
        public int id_sistema { get; set; }

        public int? ordem { get; set; }

        [ForeignKey("id_sistema")]
        public virtual Sistema Sistema { get; set; }

        public virtual ICollection<MenuPagina> MenuPaginas { get; set; }
    }

    public class MenuConfiguration : EntityTypeConfiguration<Menu>
    {
        public MenuConfiguration()
        {   
            ToTable("Menu");
        }
    }
}