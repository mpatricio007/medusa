using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Medusa.DAL
{
    public class DespesaInovacao : DespesaPessoaFisica
    {
	
        public bool iss { get; set; }         

        public DespesaInovacao()
        {
            id_lancto_tipo = 4;
        }

        //[NotMapped]
        //public override string html
        //{
        //    get
        //    {
        //        return String.Format(@"<a href='../scfp/DespesasInovacao.aspx?pk={0}'>{1}</a>",
        //           id_lancto.ToString().Criptografar(), id_lancto);
        //    }
        //}
    }
    public class DespesaInovacaoConfiguration : EntityTypeConfiguration<DespesaInovacao>
    {
        public DespesaInovacaoConfiguration()
        {
            ToTable("DespesaInovacao");
        }
    }
}