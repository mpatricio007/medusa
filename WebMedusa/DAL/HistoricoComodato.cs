using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class HistoricoComodato
    {
        [Key]
        public int id_historico { get; set; }

        public DateTime data { get; set; }

        public string observacao { get; set; }

        public int id_status_comodato { get; set; }

        [ForeignKey("id_status_comodato")]
        public virtual StatusComodato StatusComodatos { get; set; }

        public int id_usuario { get; set; }

        [ForeignKey("id_usuario")]
        public virtual UsuarioFusp usuario { get; set; }

        public int id_comodato { get; set; }

        [ForeignKey("id_comodato")]
        public virtual Comodato comodato { get; set; }
    }

    public class HistoricoComodatoConfiguration : EntityTypeConfiguration<HistoricoComodato>
    {
        public HistoricoComodatoConfiguration()
        {
            ToTable("HistoricoComodato");
        }
    }
}