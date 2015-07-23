using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class TiposAdiantamento
    {
        [Key]
        public int id_tipo_admto { get; set; }

        public string nome { get; set; }

        public int num_dias { get; set; }

        public int codigo_plano_contas { get; set; }

        public decimal valor_max { get; set; }

        public bool business_days { get; set; }

        //public string msg { get; set; }

        private ICollection<EmailPadrao> emailPadroes;

        [Invisible]
        public virtual ICollection<EmailPadrao> EmailPadroes
        {
            get
            {
                if (emailPadroes == null)
                    emailPadroes = new List<EmailPadrao>();
                return emailPadroes;
            }
            set { emailPadroes = value; }
        }
    }


    public class TiposAdiantamentoConfiguration : EntityTypeConfiguration<TiposAdiantamento>
    {
        public TiposAdiantamentoConfiguration()
        {
            ToTable("TiposAdiantamento");
        }
    }
}