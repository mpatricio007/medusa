using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class PlanoConta
    {
        [Key]
        public int id_plano_conta { get; set; }

        [MaxLength(6)]
        public string codigo { get; set; }

        [MaxLength(50)]
        public string classe { get; set; }

        [MaxLength(50)]
        public string item { get; set; }

        [MaxLength(50)]
        public string sub_item { get; set; }

        [MaxLength(25)]
        public string conta_contabil { get; set; }

        public int classificacao { get; set; }
        [NotMapped]
        public ClassificacaoPC Classificacao 
        {
            get
            {
                classificacao = classificacao == 0 ? 0 : classificacao;
                return (ClassificacaoPC)classificacao;
            }
            set { classificacao = (int)value; }
        }

        [NotMapped]
        public string strClassificacao 
        {
            get
            {
                return String.Format("{0}",(ClassificacaoPC)classificacao);
            }
        }

        [NotMapped]
        public string strPlanoContas
        {
            get
            {
                return String.Format("Código: {0} classe: {1}, item: {2}, sub item: {3}", codigo, classe, item, sub_item);
            }
        }

        public int? intCredito { get; set; }

        [NotMapped]
        public EnumPlanoConta credito
        {
            get { return (EnumPlanoConta)intCredito; }
            set { intCredito = (int)value; }
        }


        public int? intDebito { get; set; }

        [NotMapped]
        public EnumPlanoConta debito
        {
            get { return (EnumPlanoConta)intDebito; }
            set { intDebito = (int)value; }
        }

        public int? id_projeto_destino { get; set; }
        [ForeignKey("id_projeto_destino")]
        public virtual Projeto projeto_destino { get; set; }


        //public int? id_lancto_tipo { get; set; }

        //[ForeignKey("id_lancto_tipo")]
        //public virtual LancamentoTipo lancamentoTipo { get; set; }


        public virtual Taxa Taxa { get; set; }

        private ICollection<PlanoContaTipoLancamento> planoContaTipoLanctos;

        [Invisible]
        public virtual ICollection<PlanoContaTipoLancamento> PlanoContaTipoLanctos
        {
            get
            {
                if (planoContaTipoLanctos == null)
                    planoContaTipoLanctos = new List<PlanoContaTipoLancamento>();
                return planoContaTipoLanctos;
            }
            set { planoContaTipoLanctos = value; }
        }
    }

    public class PlanoContaConfiguration : EntityTypeConfiguration<PlanoConta>
    {
        public PlanoContaConfiguration()
        {
            ToTable("PlanoConta");
        }
    }
}