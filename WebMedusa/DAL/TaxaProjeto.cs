using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class TaxaProjeto
    {
        [Key]
        public int id_taxa{ get; set; }

        [Required]
        [MaxLength(50)]
        public string nome { get; set; }

        public decimal? taxa { get; set; }

        public int id_projeto { get; set; }
        [ForeignKey("id_projeto")]
        public virtual Projeto Projeto { get; set; }

        public string deb_ctb { get; set; }

        public string cred_ctb { get; set; }

        public int id_plano_conta { get; set; }
        [ForeignKey("id_plano_conta")]
        public virtual PlanoConta PlanoConta { get; set; }

        public DateTime? data_inicio { get; set; }

        [NotMapped]
        public string strTaxaProjeto 
        {
            get
            {
                return String.Format("nome: {0}, taxa: {1}%",nome,taxa);
            }   
        }

    }


    public class TaxaProjetoConfiguration : EntityTypeConfiguration<TaxaProjeto>
    {
        public TaxaProjetoConfiguration()
        {
            ToTable("TaxaProjeto");
        }
    }
}