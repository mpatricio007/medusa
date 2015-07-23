using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class ClassificacaoVaga
    {
        [Key]
        public int id_classificacao_vaga { get; set; }


        [Required]
        [MaxLength(50)]
        public string nome { get; set; }

        public virtual ICollection<Vaga> Vagas { get; set; }
    }


    public class ClassificacaoVagaConfiguration : EntityTypeConfiguration<ClassificacaoVaga>
    {
        public ClassificacaoVagaConfiguration()
        {

            ToTable("ClassificacaoVaga");
        }
    }
}