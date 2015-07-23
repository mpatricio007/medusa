using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class TarefaLcto : Tarefa
    {
        
        public int id_lcto_conta { get; set; }

        [ForeignKey("id_lcto_conta")]
        public virtual ContaLancto ContaLancto { get; set; }        
         
        [NotMapped]
        public override string tarefaToString
        {
            get { return ContaLancto.id_lcto_conta != 0 ? ContaLancto.HtmlPaginaLcto : String.Empty; }
            
        }
    }

    public class TarefaLctoConfiguration : EntityTypeConfiguration<TarefaLcto>
    {
        public TarefaLctoConfiguration()
        {
                 
            ToTable("TarefaLcto");
        }
    }

}