using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class HistoricoRequisicao
    {
        [Key]
        public int id_historico_requisicao { get; set; }

        public DateTime data { get; set; }

        public string observacao { get; set; }

        public int quantidade { get; set; }

        public int id_status_requisicao_material { get; set; }

        [ForeignKey("id_status_requisicao_material")]
        public virtual StatusRequisicaoMaterial StatusRequisicaoMaterial { get; set; }

        public int id_usuario { get; set; }

        [ForeignKey("id_usuario")]
        public virtual UsuarioFusp usuario { get; set; }

        public int? id_requisicao_material { get; set; }

        [ForeignKey("id_requisicao_material")]
        public virtual RequisicaoMaterial RequisicaoMaterial { get; set; }
    }

    public class HistoricoRequisicaoConfiguration : EntityTypeConfiguration<HistoricoRequisicao>
    {
        public HistoricoRequisicaoConfiguration()
        {
            ToTable("HistoricoRequisicao");
        }
    }
}