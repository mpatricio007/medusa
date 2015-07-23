using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class TipoConciliacao
    {
        [Key]
        public int id_tipo_conciliacao { get; set; }
        
        [MaxLength(50)]
        public string nome { get; set; }

        public bool? imprime_comprovante { get; set; }


        public bool? tem_autenticacao { get; set; }
        
    }

    public class TipoConciliacaoConfiguration : EntityTypeConfiguration<TipoConciliacao>
    {
        public TipoConciliacaoConfiguration()
        {
            ToTable("TipoConciliacao");
        }
    }
}