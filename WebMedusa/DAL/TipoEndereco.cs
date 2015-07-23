using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class TipoEndereco
    {
        [Key]
        public int id_tipo_ender { get; set; }

        [Required]
        [MaxLength(50)]
        public string descricao { get; set; }

    }


    public class TipoEnderecoConfiguration : EntityTypeConfiguration<TipoEndereco>
    {
        public TipoEnderecoConfiguration()   
        {
            ToTable("TipoEndereco");
        }
    }
}