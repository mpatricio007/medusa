using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{    
    public class ContaTransf
    {
        [Key]
        public int id_transf { get; set; }

        [Required]
        public DateTime data { get; set; }

        [Required]
        public decimal valor { get; set; }

        [Required]
        [MaxLength(30)]
        public string descricao { get; set; }
        
        public int id_conta_credito { get; set; }

        [ForeignKey("id_conta_credito")]
        public virtual Conta CreditoConta { get; set; }
        
        
        public int id_conta_debito { get; set; }

        [ForeignKey("id_conta_debito")]
        public virtual Conta DebitoConta { get; set; }

        public virtual ICollection<ContaLancto> Lancamentos { get; set; }                
        
        [NotMapped]
        public bool HasLactoConciliado
        {
            get { return Lancamentos.Where(it => it.conciliado == true).Count() > 0; }            
        }        
    }


    public class ContaTransfConfiguration : EntityTypeConfiguration<ContaTransf>
    {
        public ContaTransfConfiguration()
        {
            HasRequired(a => a.CreditoConta).WithMany().HasForeignKey(u => u.id_conta_credito).WillCascadeOnDelete(false);
            HasRequired(a => a.DebitoConta).WithMany().HasForeignKey(u => u.id_conta_debito).WillCascadeOnDelete(false);
            ToTable("ContaTransf");
        }
    }
}