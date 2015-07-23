using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Medusa.DAL
{
    public class DespesaBolsa : DespesaPessoaFisica
    {

        
        public DespesaBolsa()
        {
            id_lancto_tipo = 3;
        }

        //[NotMapped]
        //public override string html
        //{
        //    get
        //    {
        //        return String.Format(@"<a href='../scfp/DespesasBolsas.aspx?pk={0}'>{1}</a>",
        //           id_lancto.ToString().Criptografar(), id_lancto);
        //    }
        //}
        
    }
    public class DespesaBolsaConfiguration : EntityTypeConfiguration<DespesaBolsa>
    {
        public DespesaBolsaConfiguration()
        {  
            ToTable("DespesaBolsa");
        }
    }
}