using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class IdentficacaoSegPlanConta
    {
        [Key]
        public int id_identSeg_PlanConta { get; set; }

        public int id_identificacao_segmento { get; set; }
        [ForeignKey("id_identificacao_segmento")]
        public virtual IdentificacaoSegmento IdentificacaoSegmento { get; set; }

        public int id_plano_conta { get; set; }
        [ForeignKey("id_plano_conta")]
        public virtual PlanoConta PlanoConta { get; set; }
    }

    public class IdentficacaoSegPlanContaConfiguration : EntityTypeConfiguration<IdentficacaoSegPlanConta>
    {
        public IdentficacaoSegPlanContaConfiguration()
        {
            ToTable("IdentficacaoSegPlanConta");
        }
    }
}