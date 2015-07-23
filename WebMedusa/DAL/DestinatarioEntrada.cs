using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class DestinatarioEntrada
    {
        [Key]
        public int id_destinatario{ get; set; }

        public int id_usuario { get; set; }
        [ForeignKey("id_usuario")]
        public virtual UsuarioFusp UsuarioFusp { get; set; }

        public int id_historico_entrada { get; set; }
        [ForeignKey("id_historico_entrada")]
        public virtual HistoricoEntrada HistoricoEntrada { get; set; }
    }


    public class DestinatarioEntradaConfiguration : EntityTypeConfiguration<DestinatarioEntrada>
    {
        public DestinatarioEntradaConfiguration()
        {
            ToTable("DestinatarioEntrada");
        }
    }
}