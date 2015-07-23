using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class EmailPadrao
    {
        [Key]
        public int id_email_padrao { get; set; }

        public int? id_status_admto { get; set; }
        [ForeignKey("id_status_admto")]
        public virtual StatusAdiantamento StatusAdiantamento { get; set; }

        [MaxLength(50)]
        public string nome { get; set; }

        [MaxLength(100)]
        public string assunto { get; set; }

        public string corpo { get; set; }

        public int? id_tipo_admto { get; set; }
        [ForeignKey("id_tipo_admto")]
        public virtual TiposAdiantamento TipoAdiantamento { get; set; }

        private ICollection<EmailCopia> emailCopias;

        [Invisible]
        public virtual ICollection<EmailCopia> EmailCopias
        {
            get
            {
                if (emailCopias == null)
                    emailCopias = new List<EmailCopia>();
                return emailCopias;
            }
            set { emailCopias = value; }
        }
    }


    public class EmailPadraoConfiguration : EntityTypeConfiguration<EmailPadrao>
    {
        public EmailPadraoConfiguration()
        {
            ToTable("EmailPadrao");
        }
    }
}