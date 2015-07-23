using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using Medusa.BLL;

namespace Medusa.DAL
{
    public class Despesa : Lancamento
    {

        public int? id_remessa { get; set; }

        [ForeignKey("id_remessa")]
        public virtual Remessa RemessaGerada { get; set; }

        public string rp { get; set; }

        public int? id_forma { get; set; }

        [ForeignKey("id_forma")]
        public virtual FormaPagtoDespesa formaPagto { get; set; }

     
    }


    public class DespesaConfiguration : EntityTypeConfiguration<Despesa>
    {
        public DespesaConfiguration()
        {            
            ToTable("Despesa");
        }
    }
}

