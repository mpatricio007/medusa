using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class DespesaGRF : DespesaImpostoConsumo
    {
        public DespesaGRF()
        {
            id_lancto_tipo = 11;
        }

        //[NotMapped]
        //public override string html
        //{
        //    get
        //    {
        //        return String.Format(@"<a href='../scfp/DespesasGRFs.aspx?pk={0}'>{1}</a>",
        //           id_lancto.ToString().Criptografar(), id_lancto);
        //    }
        //}
    }


    public class DespesaGRFConfiguration : EntityTypeConfiguration<DespesaGRF>
    {
        public DespesaGRFConfiguration()
        {
            ToTable("DespesaGRF");
        }
    }
}