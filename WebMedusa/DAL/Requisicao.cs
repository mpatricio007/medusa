using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel;
using Medusa.BLL;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class Requisicao
    {
        [Key]
        public int id_requisicao { get; set; }

        public DateTime data { get; set; }

        public int id_status_requisicao { get; set; }

        [ForeignKey("id_status_requisicao")]
        public virtual StatusRequisicao StatusRequisicao { get; set; }

        public int id_usuario { get; set; }

        [ForeignKey("id_usuario")]
        public virtual UsuarioFusp Usuario { get; set; }

        private ICollection<RequisicaoMaterial> requisicaoMaterial;
        [Search("materiais", "MaterialCosumo.descricao")]
        public virtual ICollection<RequisicaoMaterial> RequisicaoMateriais
        {
            get
            {
                if (requisicaoMaterial == null)
                    requisicaoMaterial = new List<RequisicaoMaterial>();
                return requisicaoMaterial;
            }

            set
            {
                requisicaoMaterial = value;
            }
        }

        [NotMapped]
        public string Status 
        {
            get
            {
                return RequisicaoMateriais.Where(it => it.id_ultimo_status == StatusRequisicaoMaterialBLL.AguardandoAtendimento).Count() > 0
                    ? RequisicaoBLL.EmAberto : RequisicaoBLL.Encerrado;// == RequisicaoMaterialBLL.Aguardando).Count() > 0 ? RequisicaoBLL.EmAberto : RequisicaoBLL.Encerrado;
            }
       }

        //private ICollection<HistoricoRequisicao> historicos;

        //public virtual ICollection<HistoricoRequisicao> Historicos
        //{
        //    get
        //    {
        //        if (historicos == null)
        //            historicos = new List<HistoricoRequisicao>();
        //        return historicos;
        //    }
        //    set { historicos = value; }
        //}

    }

    public class RequisicaoConfiguration : EntityTypeConfiguration<Requisicao>
    {
        public RequisicaoConfiguration()
        {
            ToTable("Requisicao");
        }
    }
}