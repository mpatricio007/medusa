using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class HistoricoPx
    {
        [Key]
        public int id_hpx { get; set; }

        [Required]
        public int id_sol_proj { get; set; }
        [ForeignKey("id_sol_proj")]
        public virtual ProjetoSolicitacao ProjetoSolicitacao { get; set; }
        
        [Required]
        public DateTime data { get; set; }
        
        [MaxLength(100)]
        public string observacao { get; set; }

        [Required]
        public int id_usuario { get; set; }

        [ForeignKey("id_usuario")]
        public virtual UsuarioFusp Usuario { get; set; }

        
        public int id_status_solicitacao { get; set; }

        [ForeignKey("id_status_solicitacao")]
        public virtual StatusSolicitacao StatusSolicitacao { get; set; }

    }


    public class HistoricoPxConfiguration : EntityTypeConfiguration<HistoricoPx>
    {
        public HistoricoPxConfiguration()
        {            
            ToTable("HistoricoPx");
        }
    }
}