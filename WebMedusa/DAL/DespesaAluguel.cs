using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Medusa.DAL
{
    public class DespesaAluguel : DespesaPessoaFisica
    {

        
        public DespesaAluguel()
        {
            id_lancto_tipo = 5;
        }

        //public override string html
        //{
        //    get { throw new NotImplementedException(); }
        //}
    }
    public class DespesaAluguelConfiguration : EntityTypeConfiguration<DespesaAluguel>
    {
        public DespesaAluguelConfiguration()
        {  
            ToTable("DespesaAluguel");
        }
    }
}