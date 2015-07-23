using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class HistoricoEmailAdmto
    {
        [Key]
        public int id_hist_email_admto { get; set; }

        public int? id_adiantamento { get; set; }
        [ForeignKey("id_adiantamento")]
        public virtual Adiantamento Adiantamento { get; set; }

        public int? id_email_padrao { get; set; }
        [ForeignKey("id_email_padrao")]
        public virtual EmailPadrao EmailPadrao { get; set; }

        public int? id_usuario { get; set; }
        [ForeignKey("id_usuario")]
        public virtual Usuario Usuario { get; set; }

        public DateTime? data { get; set; }

        public string assunto { get; set; }
    }


    public class HistoricoEmailAdmtoConfiguration : EntityTypeConfiguration<HistoricoEmailAdmto>
    {
        public HistoricoEmailAdmtoConfiguration()
        {
            ToTable("HistoricoEmailAdmto");
        }
    }
}