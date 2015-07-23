using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class TransferenciaProjeto : Despesa
    {
        public TransferenciaProjeto() 
        {
            id_lancto_tipo = 15;
        }

        public int id_projeto_trans { get; set; }

        [ForeignKey("id_projeto_trans")]
        public virtual Projeto ProjetoTrans { get; set; }

      
        

        //[NotMapped]
        //public override string html
        //{
        //    get
        //    {
        //        return String.Format(@"<a href='../scfp/TransferenciasEntreProjetos.aspx?pk={0}'>{1}</a>",
        //           id_lancto.ToString().Criptografar(), id_lancto);
        //    }
        //}
    }

    public class TransferenciaProjetoConfiguration : EntityTypeConfiguration<TransferenciaProjeto>
    {
        public TransferenciaProjetoConfiguration()
        {
            ToTable("TransferenciaProjeto");
        }
    }
}