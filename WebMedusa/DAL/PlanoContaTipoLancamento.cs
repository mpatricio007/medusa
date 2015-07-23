using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class PlanoContaTipoLancamento
    {
        [Key]
        public int id_lanTipo_Pc { get; set; }

        public int id_plano_conta { get; set; }
        [ForeignKey("id_plano_conta")]
        public virtual PlanoConta plconta { get; set; }

        public int id_lancto_tipo { get; set; }
        [ForeignKey("id_lancto_tipo")]
        public virtual LancamentoTipo tipo { get; set; }

                         

        
    }

    public class PlanoContaTipoLancamentoConfiguration : EntityTypeConfiguration<PlanoContaTipoLancamento>
    {
        public PlanoContaTipoLancamentoConfiguration()
        {
            ToTable("PlanoContaTipoLancamento");
        }
    }
}