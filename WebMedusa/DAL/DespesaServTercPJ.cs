using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Medusa.DAL
{
    public class DespesaServTercPJ : DespesaPessoaJuridica
    {

        public DespesaServTercPJ()
        {
            id_lancto_tipo = 10;
        }

        //[NotMapped]
        //public override string html
        //{
        //    get
        //    {
        //        return String.Format(@"<a href='../scfp/DespesasServTercPJ.aspx?pk={0}'>{1}</a>",
        //           id_lancto.ToString().Criptografar(), id_lancto);
        //    }
        //}

    }
    public class DespesaServTercPJConfiguration : EntityTypeConfiguration<DespesaServTercPJ>
    {
        public DespesaServTercPJConfiguration()
        {
            ToTable("DespesaServTercPJ");
        }
    }
}