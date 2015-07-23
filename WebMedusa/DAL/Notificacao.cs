using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class Notificacao
    {
        [Key]
        public int id_notificacao { get; set; }

        public int? id_contato_projeto { get; set; }
        [ForeignKey("id_contato_projeto")]
        public ContatoProjeto ContatoProjeto { get; set; }

        public int? id_sol_de_proj { get; set; }
        [ForeignKey("id_sol_de_proj")]
        public SolicitacaoDeProjeto SolicitacaoDeProjeto { get; set; }
    }


    public class NotificacaoConfiguration : EntityTypeConfiguration<Notificacao>
    {
        public NotificacaoConfiguration()
        {
            ToTable("Notificacao");
        }
    }
}