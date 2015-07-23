using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class IdentificacaoSegmento
    {
        [Key]
        public int id_identificacao_segmento { get; set; }

        public string codigo { get; set; }

        public string nome { get; set; }

        private ICollection<IdentficacaoSegPlanConta> identficacaoSegmentoPlanosContas;
        public virtual ICollection<IdentficacaoSegPlanConta> IdentficacaoSegmentoPlanosContas
        {
            get
            {
                if (identficacaoSegmentoPlanosContas == null)
                    identficacaoSegmentoPlanosContas = new List<IdentficacaoSegPlanConta>();
                return identficacaoSegmentoPlanosContas;
            }

            set
            {
                identficacaoSegmentoPlanosContas = value;
            }
        }
    }


    public class IdentificacaoSegmentoConfiguration : EntityTypeConfiguration<IdentificacaoSegmento>
    {
        public IdentificacaoSegmentoConfiguration()
        {
            ToTable("IdentificacaoSegmento");
        }
    }
}