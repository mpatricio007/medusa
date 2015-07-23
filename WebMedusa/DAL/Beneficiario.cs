using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class Beneficiario : AbstractPessoaFisica
    {
        [Key]
        public int id_beneficiario { get; set; }

        public int? id_unidade { get; set; }
        [ForeignKey("id_unidade")]
        public virtual Unidade Unidade { get; set; }

        [NotMapped]
        public string HtmlPaginaBeneficiario 
        {
            get
            {
                return String.Format(@"<a href='../adiantamentos/beneficiarios.aspx?pk={0}'>{1}</a>", id_beneficiario.ToString().Criptografar(),PessoaFisica.nome);
            }
        }

    }


    public class BeneficiarioConfiguration : EntityTypeConfiguration<Beneficiario>
    {
        public BeneficiarioConfiguration()
        {
            //HasKey(b => b.id_beneficiario);
            ToTable("Beneficiario");
        }
    }
}