using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class ProjetoSolicitacao
    {
        [Key]
        public int id_sol_proj { get; set; }
        
        public int codigo { get; set; }
                
        public string descricao { get; set; }
         
        public int? id_coordenador { get; set; }

        [ForeignKey("id_coordenador")]
        public virtual Coordenador Coordenador { get; set; }

        public string strCoordenador { get; set; }

        public int? id_financiador { get; set; }

        [ForeignKey("id_financiador")]
        public virtual Financiador  Fianciador { get; set; }

        public string strFinanciador { get; set; }
        
        public string titulo { get; set; }

        public DateTime data_solicitacao { get; set; }

        public int id_usuario { get; set; }

        [ForeignKey("id_usuario")]
        public virtual Usuario Usuario { get; set; }

        public int id_ultimo_status { get; set; }

        [ForeignKey("id_ultimo_status")]
        public virtual StatusSolicitacao StatusSolicitacao { get; set; }

        public virtual ICollection<HistoricoPx> Historicos { get; set; }

        public int? id_tipo_solicitacao { get; set; }
        [ForeignKey("id_tipo_solicitacao")]
        public virtual TipoSolicitacao TipoSolicitacao { get; set; }
        
        [MaxLength(100)]
        public string observacao { get; set; }

        //itens opção proposta:
        public int? id_sub_coordenador { get; set; }
        [ForeignKey("id_sub_coordenador")]
        public virtual Coordenador SubCoordenador { get; set; }

        public string strSubCoordenador { get; set; }

        public int? id_unidade { get; set; }
        [ForeignKey("id_unidade")]
        public virtual Unidade Unidade { get; set; }

        public int? id_departamento { get; set; }
        [ForeignKey("id_departamento")]
        public virtual Departamento Departamento { get; set; }

        public int? id_laboratorio { get; set; }
        [ForeignKey("id_laboratorio")]
        public virtual Laboratorio Laboratorio { get; set; }

        public int? id_moeda { get; set; }
        [ForeignKey("id_moeda")]
        public virtual Moeda Moeda { get; set; }

        public decimal? valor_global { get; set; }

        public DateTime? inicio { get; set; }

        public DateTime? termino { get; set; }

        public int? id_instrumento_contratual { get; set; }
        [ForeignKey("id_instrumento_contratual")]
        public virtual InstrumentoContratual InstrumentoContratual { get; set; }

        public string contrato_patrocinio { get; set; }

        [NotMapped]
        public string strSolicitacao
        {
            get 
            {
                return String.Format("n°: {0} titulo: {1}", codigo, titulo);
            }
        }

        public virtual Projeto Projeto { get; set; }

        [NotMapped]
        public string HtmlPaginaProjetoSolicitacao
        {
            get
            {
                return String.Format(@"<a href='../scp/projetosolicitacoes.aspx?pk={0}'>{1}</a>",
                   id_sol_proj.ToString().Criptografar(), codigo);
            }
        }
    }

    public class ProjetoSolicitacaoConfiguration : EntityTypeConfiguration<ProjetoSolicitacao>
    {
        public ProjetoSolicitacaoConfiguration()
        {            
            ToTable("ProjetoSolicitacao");
        }
    }
}
