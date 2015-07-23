using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class DespesaGRFR : DespesaImpostoConsumo
    {
        public DespesaGRFR() 
        {
            id_lancto_tipo = 12;
        }

        //[NotMapped]
        //public override string html
        //{
        //    get
        //    {
        //        return String.Format(@"<a href='../scfp/DespesasGRFRs.aspx?pk={0}'>{1}</a>",
        //           id_lancto.ToString().Criptografar(), id_lancto);
        //    }
        //}
    }

    public class DespesaGRFRConfiguration : EntityTypeConfiguration<DespesaGRFR>
    {
        public DespesaGRFRConfiguration()
        {
            ToTable("DespesaGRFR");
        }
    }
}