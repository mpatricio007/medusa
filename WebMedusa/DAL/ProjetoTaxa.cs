using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class ProjetoTaxa
    {
        [Key]
        public int id_projeto_taxa { get; set; }

        [Required]
        public int id_taxa { get; set; }
        [ForeignKey("id_taxa")]
        public virtual TaxaProjeto TaxaProjeto { get; set; }

        [Required]
        public int id_projeto { get; set; }
        [ForeignKey("id_projeto")]
        public virtual Projeto Projeto { get; set; }
    }


    public class ProjetoTaxaConfiguration : EntityTypeConfiguration<ProjetoTaxa>
    {
        public ProjetoTaxaConfiguration()
        {
            ToTable("ProjetoTaxa");
        }
    }
}