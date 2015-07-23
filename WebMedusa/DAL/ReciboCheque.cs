using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;


namespace Medusa.DAL
{
    public class ReciboCheque
    {
        [Key]
        public int id_cheque { get; set; }

        public int num_cheque { get; set; }

        public DateTime data { get; set; }

        public decimal valor { get; set; }

        public int id_recibo { get; set; }

        [ForeignKey("id_recibo")]
        public virtual Recibo Recibo { get; set; }
    }
    
    public class ReciboChequeConfiguration : EntityTypeConfiguration<ReciboCheque>
    {
        public ReciboChequeConfiguration()
        {
            ToTable("ReciboCheque");
        }
    }
}