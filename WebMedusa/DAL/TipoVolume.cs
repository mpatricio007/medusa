using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    [Serializable]
    public class TipoVolume
    {
        [Key]
        public int id_tipo { get; set; }

        [Required]
        [MaxLength(30)]
        public string nome { get; set; }

    }


    public class TipoVolumeConfiguration : EntityTypeConfiguration<TipoVolume>
    {
        public TipoVolumeConfiguration()
        {
            ToTable("TipoVolume");
        }
    }
}