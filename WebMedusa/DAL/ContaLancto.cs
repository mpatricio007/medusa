using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class ContaLancto
    {
        [Key]
        public int id_lcto_conta { get; set; }

        [Required]        
        public int id_conta { get; set; }
        
        [ForeignKey("id_conta")]
        public virtual Conta Conta { get; set; }

        [Required]
        public DateTime data { get; set; }

        [Required]
        public decimal valor { get; set; }

        [Required]
        public string descricao { get; set; }

        [Required]        
        public int id_tipo_lcto { get; set; }

        [ForeignKey("id_tipo_lcto")]
        public virtual TipoLcto TipoLcto { get; set; }

        [Required]
        public bool conciliado { get; set; }
        
        public DateTime? data_conciliado { get; set; }

        public int? id_imparq { get; set; }

        public string num_documento { get; set; }

        public string proj_num { get; set; }

        public string obs { get; set; }

        [ForeignKey("id_imparq")]
        public virtual ImportaArquivo ImportaArquivo { get; set; }

        public virtual ICollection<ContaTransf> Transferencias { get; set; }

        public virtual ICollection<TarefaLcto> Tarefas { get; set; }

        public virtual ICollection<Remessa> Remessas { get; set; }

        [NotMapped]
        public string HtmlPaginaLcto
        {
            get
            {
                return String.Format(@"<a class='TarefaLink' href='../sistemas/conciliacao/ContasLancto.aspx?pk={0}'>{1} {2}</a>", id_lcto_conta.ToString().Criptografar(), this.ToString(),
                    Conta != null ? String.Format("Conta Corrente {0}", Conta.ToString()) : String.Empty);
            }
        }


        public override string ToString()
        {
            return String.Format("{0} {1} {2:c2} {3:d}", TipoLcto != null ? TipoLcto.descricao : String.Empty, descricao, valor, data);
        }
    }

    public class ContaLanctoConfiguration : EntityTypeConfiguration<ContaLancto>
    {
        public ContaLanctoConfiguration()
        {
            //HasMany(l => l.Tarefas).WithRequired(t => t.ContaLancto).WillCascadeOnDelete(true);       
            HasRequired(l => l.Conta).WithMany(c => c.ContaLancto).WillCascadeOnDelete(false);
            HasRequired(l => l.TipoLcto).WithMany(t => t.ContaLancto).WillCascadeOnDelete(false);
            HasMany(l => l.Transferencias).WithMany(t => t.Lancamentos).Map(tl =>
            {
                tl.ToTable("TransfLancto");
                tl.MapLeftKey("id_lcto_conta");
                tl.MapRightKey("id_transf");
            });
            
            ToTable("ContaLancto");
        }
    }
}