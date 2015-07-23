using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class DespesaDARF : DespesaImpostoConsumo
    {
        public DespesaDARF()
        {
            id_lancto_tipo = 13;
        }

        //[NotMapped]
        //public override string html
        //{
        //    get
        //    {
        //        return String.Format(@"<a href='../scfp/DespesasDARFs.aspx?pk={0}'>{1}</a>",
        //           id_lancto.ToString().Criptografar(), id_lancto);
        //    }
        //}
    }

    public class DespesaDARFConfiguration : EntityTypeConfiguration<DespesaDARF>
    {
        public DespesaDARFConfiguration()
        {
            ToTable("DespesaDARF");
        }
    }
}