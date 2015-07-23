using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class Devolucao
    {
        [Key]
        public int id_devolucao { get; set; }

        public int numero { get; set; }

        public int protocolo { get; set; }

        public string beneficiario { get; set; }

        public decimal valor_total { get; set; }

        public DateTime data { get; set; }

        public int id_projeto { get; set; }

        [ForeignKey("id_projeto")]
        public virtual Projeto Projeto { get; set; }

        public int id_tipo_devolucao { get; set; }

        [ForeignKey("id_tipo_devolucao")]
        public virtual TipoDevolucao TipoDevolucao { get; set; }

        //public int id_motivo { get; set; }

        //[ForeignKey("id_motivo")]
        //public virtual Motivo Motivo { get; set; }

        public int id_usuario { get; set; }

        [ForeignKey("id_usuario")]
        public virtual Usuario Usuario { get; set; }

        private ICollection<MotivoDevolucao> motivoDevolucoes;
        [Search("motivos", "descricao_motivo")]
        public virtual ICollection<MotivoDevolucao> MotivoDevolucoes
        {
            get
            {
                if (motivoDevolucoes == null)
                    motivoDevolucoes = new List<MotivoDevolucao>();
                return motivoDevolucoes;
            }

            set
            {
                motivoDevolucoes = value;
            }
        }
    }


    public class DevolucaoConfiguration : EntityTypeConfiguration<Devolucao>
    {
        public DevolucaoConfiguration()
        {
            ToTable("Devolucao");
        }
    }
}