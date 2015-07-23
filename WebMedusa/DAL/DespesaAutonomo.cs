using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Medusa.DAL
{
    public class DespesaAutonomo : DespesaPessoaFisica
    {

        public decimal deducaoINSS { get; set; }       	
        public bool iss { get; set; }
          
        public DespesaAutonomo()
        {
            id_lancto_tipo = 1;
        }
        //[NotMapped]
        //public override string html
        //{
        //    get
        //    {
        //        return String.Format(@"<a href='../scfp/DespesasAutonomos.aspx?pk={0}'>{1}</a>",
        //           id_lancto.ToString().Criptografar(), id_lancto);
        //    }
        //}
        
    }
    public class DespesaAutonomoConfiguration : EntityTypeConfiguration<DespesaAutonomo>
    {
        public DespesaAutonomoConfiguration()
        {            
            ToTable("DespesaAutonomo");
        }
    }
}