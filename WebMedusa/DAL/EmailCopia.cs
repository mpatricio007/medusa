using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class EmailCopia
    {
        [Key]
        public int id_email_copia { get; set; }

        public Email email { get; set; }

        public int? id_email_padrao { get; set; }
        [ForeignKey("id_email_padrao")]
        public virtual EmailPadrao EmailPadrao { get; set; }
    }


    public class EmailCopiaConfiguration : EntityTypeConfiguration<EmailCopia>
    {
        public EmailCopiaConfiguration()
        {
            ToTable("EmailCopia");
        }
    }
}