using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using Medusa.LIB;

namespace Medusa.DAL
{
    public class DespesaConsumo : DespesaImpostoConsumo
    {
        public DespesaConsumo()
        {
            id_lancto_tipo = 2;
        }

        //[NotMapped]
        //public override string html
        //{
        //    get
        //    {
        //        return String.Format(@"<a href='../scfp/DespesasConsumos.aspx?pk={0}'>{1}</a>",
        //           id_lancto.ToString().Criptografar(), id_lancto);
        //    }
        //}
    }

    public class DespesaConsumoConfiguration : EntityTypeConfiguration<DespesaConsumo>
    {
        public DespesaConsumoConfiguration()
        {
            ToTable("DespesaConsumo");
        }
    }
}