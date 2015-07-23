using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class TipoSolicitacao
    {
        [Key]
        public int id_tipo_solicitacao { get; set; }

        [Required]
        [MaxLength(50)]
        public string nome { get; set; }

       
    }


    public class TipoSolicitacaoConfiguration : EntityTypeConfiguration<TipoSolicitacao>
    {
        public TipoSolicitacaoConfiguration()
        {
            ToTable("TipoSolicitacao");
        }
    }
}