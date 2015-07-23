using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    [Serializable]
    public class Volume
    {
        [Key]
        public int id_volume { get; set; }

        [Required]
        [MaxLength(500)]
        public string descricao { get; set; }

        [Required]
        [MaxLength(35)]
        public string nome { get; set; }

        [MaxLength(10)]
        public string projeto { get; set; }

        [ForeignKey("id_localizacao")]
        public virtual LocalizacaoVolume localizacao { get; set; }

        [Required]
        public int id_localizacao { get; set; }

        [ForeignKey("id_tipo")]
        public virtual TipoVolume tipo { get; set; }

        [Required]
        public int id_tipo { get; set; }

        public int? num { get; set; }

        public string projetoA { get; set; }

        public string coordenador { get; set; }

        public string codigo_metrofile { get; set; }

        [NotMapped]
        public string HtmlPaginaVolume
        {
            get
            {
                return String.Format(@"<a href='../arquivo/volume.aspx?pk={0}'>{1}</a>",
                   id_volume.ToString().Criptografar(), id_volume);
            }
        }

    }


    public class VolumeConfiguration : EntityTypeConfiguration<Volume>
    {
        public VolumeConfiguration()
        {
            ToTable("Volume");
        }
    }
}