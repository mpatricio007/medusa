using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class ContaAplicacao
    {

        [Key]
        public int id_aplicacao { get; set; }

        [Required]
        public DateTime data { get; set; }

        [Required]
        public decimal valor { get; set; }

        [Required]
        [MaxLength(30)]
        public string descricao { get; set; }
        
        public int id_conta { get; set; }

        [ForeignKey("id_conta")]
        public virtual Conta Conta { get; set; }

        [Required]        
        public int id_tipo_lcto { get; set; }

        [ForeignKey("id_tipo_lcto")]
        public virtual TipoLcto TipoLcto { get; set; }

        [Required]        
        public int id_lcto_conta { get; set; }

        [ForeignKey("id_lcto_conta")]
        public virtual ContaLancto ContaLancto { get; set; }
    }

    public class ContaAplicacaoConfiguration : EntityTypeConfiguration<ContaAplicacao>
    {
        public ContaAplicacaoConfiguration()
        {
            
            HasRequired(a => a.ContaLancto).WithMany().HasForeignKey(a => a.id_lcto_conta).WillCascadeOnDelete();            
            ToTable("ContaAplicacao");
        }
    }
}