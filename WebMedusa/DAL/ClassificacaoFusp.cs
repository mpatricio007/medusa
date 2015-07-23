using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class ClassificacaoFusp
    {
        [Key]
        public int id_classificacao_fusp { get; set; }

        [Required]
        [MaxLength(80)]
        public string nome { get; set; }
    }


    public class ClassificacaoFuspConfiguration : EntityTypeConfiguration<ClassificacaoFusp>
    {
        public ClassificacaoFuspConfiguration()
        {
            ToTable("ClassificacaoFusp");
        }
    }
}