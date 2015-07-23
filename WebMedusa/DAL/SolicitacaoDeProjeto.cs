using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class SolicitacaoDeProjeto
    {
        [Key]
        public int id_sol_de_proj { get; set; }

        [Required]
        [MaxLength(50)]
        public string nome { get; set; }

        private ICollection<Notificacao> nofificacoes;

        public virtual ICollection<Notificacao> Notificacoes
        {
            get
            {
                if (nofificacoes == null)
                    nofificacoes = new List<Notificacao>();
                return nofificacoes;
            }
            set { nofificacoes = value; }
        }
    }

    public class SolicitacaoDeProjetoConfiguration : EntityTypeConfiguration<SolicitacaoDeProjeto>
    {
        public SolicitacaoDeProjetoConfiguration()
        {
            ToTable("SolicitacaoDeProjeto");
        }
    }
}