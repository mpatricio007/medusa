using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class ContatoProjeto
    {
        [Key]
        public int id_contato_projeto { get; set; }

        public int id_contato { get; set; }
        [ForeignKey("id_contato")]
        public virtual Contato Contato { get; set; }

        public int id_projeto { get; set; }
        [ForeignKey("id_projeto")]
        public virtual Projeto Projeto { get; set; }

        private ICollection<Notificacao> notificacoes;

        public virtual ICollection<Notificacao> Notificacoes
        {
            get
            {
                if (notificacoes == null)
                    notificacoes = new List<Notificacao>();
                return notificacoes;
            }
            set { notificacoes = value; }
        }
    }


    public class ContatoProjetoConfiguration : EntityTypeConfiguration<ContatoProjeto>
    {
        public ContatoProjetoConfiguration()
        {
            ToTable("ContatoProjeto");
        }
    }
}