using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class PACDestino
    {
        [Key]
        public int id_destino { get; set; }

        [Required]
        [MaxLength(50)]
        public string descricao { get; set; }

    }


    public class PACDestinoConfiguration : EntityTypeConfiguration<PACDestino>
    {
        public PACDestinoConfiguration()
        {
            ToTable("PACDestino");
        }
    }
}