using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    [Serializable]
    public class LocalizacaoVolume
    {
        [Key]
        public int id_localizacao { get; set; }

        [Required]
        [MaxLength(30)]
        public string nome { get; set; }

    }


    public class LocalizacaoVolumeConfiguration : EntityTypeConfiguration<LocalizacaoVolume>
    {
        public LocalizacaoVolumeConfiguration()
        {
            ToTable("LocalizacaoVolume");
        }
    }
}