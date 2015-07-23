using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class StatusSolicitacao
    {
        [Key]
        public int id_status_solicitacao { get; set; }

        public string descricao { get; set; } 

        public int? ordem { get; set; }

        public ICollection<HistoricoPx> historicos { get; set; }
    }


    public class StatusSolicitacaoConfiguration : EntityTypeConfiguration<StatusSolicitacao>
    {
        public StatusSolicitacaoConfiguration()
        {
            //HasMany(it => it.historicos).WithRequired().WillCascadeOnDelete(false);
            ToTable("StatusSolicitacao");
        }
    }
}