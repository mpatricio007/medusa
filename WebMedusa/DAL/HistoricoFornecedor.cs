using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class HistoricoFornecedor
    {
        [Key]
        public int id_hist_fornecedor { get; set; }

        public int id_fornecedor { get; set; }

        [ForeignKey("id_fornecedor")]
        public virtual Fornecedor fornecedor { get; set; }

        public DateTime data { get; set; }

        public string observacao { get; set; }

        public int id_status_fornecedor { get; set; }

        [ForeignKey("id_status_fornecedor")]
        public virtual StatusFornecedor StatusFornecedor { get; set; }

        public int? id_usuario { get; set; }

        [ForeignKey("id_usuario")]
        public virtual UsuarioFusp Usuario { get; set; }

    }


    public class HistoricoFornecedorConfiguration : EntityTypeConfiguration<HistoricoFornecedor>
    {
        public HistoricoFornecedorConfiguration()
        {
            ToTable("HistoricoFornecedor");
        }
    }
}