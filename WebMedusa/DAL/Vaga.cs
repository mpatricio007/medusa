using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class Vaga
    {
        [Key]
        public int id_vaga { get; set; }
        [MaxLength(300)]
        public string descricao_vaga { get; set; }

        public int id_projeto { get; set; }

        [ForeignKey("id_projeto")]
        public virtual Projeto Projeto { get; set; }

        public decimal? valor_inscricao_vaga { get; set; }
        public DateTime inicio { get; set; }
        public DateTime termino { get; set; }
        public DateTime?  data_vencto { get; set; }
        public bool status { get; set; }
        [MaxLength(50)]
        public string codigo { get; set; }
        public int id_classificacao_vaga { get; set; }

        public virtual ICollection<Edital> Editais { get; set; }
    }

    public class VagaConfiguration : EntityTypeConfiguration<Vaga>
    {
        public VagaConfiguration()
        {
            ToTable("Vaga");
        }
    }
}