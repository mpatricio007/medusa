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
    public class DespesaBoleto : Despesa
    {
        public DespesaBoleto()
        {
            id_lancto_tipo = 9;
        }

        [Required]
        public string codbarra { get; set; }

        [NotMapped]
        public CodigoBarrasBoleto Boleto
        {
            get
            {
                return new CodigoBarrasBoleto
                {
                    RepresentacaoNumerica = codbarra
                };
            }
            set { codbarra = value.RepresentacaoNumerica; }

        }

        public DateTime dataVencto { get; set; }

        public string cedente { get; set; }

        public string descricao { get; set; }

        //[NotMapped]
        //public override string html
        //{
        //    get
        //    {
        //        return String.Format(@"<a href='../scfp/DespesasBoletos.aspx?pk={0}'>{1}</a>",
        //           id_lancto.ToString().Criptografar(), id_lancto);
        //    }
        //}
    }

    public class DespesaBoletoConfiguration : EntityTypeConfiguration<DespesaBoleto>
    {
        public DespesaBoletoConfiguration()
        {
            ToTable("DespesaBoleto");
        }
    }
}