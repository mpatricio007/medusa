using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using Medusa.BLL;

namespace Medusa.DAL
{
    public class DespesaProjeto : Despesa
    {
        public DespesaProjeto() 
        {
            id_lancto_tipo = 16;
        }

        public string descricao { get; set; }

        public int id_plano_conta { get; set; }

        [ForeignKey("id_plano_conta")]
        public virtual PlanoConta PlanoConta { get; set; }


        //[NotMapped]
        //public override string html
        //{
        //    get
        //    {
        //        return String.Format(@"<a href='../scfp/DespesaProjetos.aspx?pk={0}'>{1}</a>",
        //           id_lancto.ToString().Criptografar(), id_lancto);
        //    }
        //}
    }

    public class DespesaProjetoConfiguration : EntityTypeConfiguration<DespesaProjeto>
    {
        public DespesaProjetoConfiguration()
        {
            ToTable("DespesaProjeto");
        }
    }
}