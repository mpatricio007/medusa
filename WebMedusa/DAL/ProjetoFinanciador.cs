using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class ProjetoFinanciador
    {
        [Key]
        public int id_projeto_financiador { get; set; }

        public int id_financiador { get; set; }

        [ForeignKey("id_financiador")]
        public virtual Financiador Financiador { get; set; }

        public int id_projeto { get; set; }

        [ForeignKey("id_projeto")]
        public virtual Projeto Projeto { get; set; }

        public string observacao { get; set; }


        [NotMapped]
        public string HtmlPaginaFinanciador
        {
            get
            {
                return String.Format(@"<a href='../scp/financiadores.aspx?pk={0}'>{1}</a>",
                   id_financiador.ToString().Criptografar(), Financiador.nome);
            }
        }

    }
    public class ProjetoFinanciadorConfiguration : EntityTypeConfiguration<ProjetoFinanciador>
    {
        public ProjetoFinanciadorConfiguration()
        {
            ToTable("ProjetoFinanciador");
        }
    }
}