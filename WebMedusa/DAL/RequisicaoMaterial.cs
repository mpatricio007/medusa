using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    [Serializable]
    public class RequisicaoMaterial 
    {
        [Key]
        public int id_requisicao_material { get; set; }

        public int id_material { get; set; }
                
        [ForeignKey("id_material")]
        public virtual MaterialConsumo MaterialConsumo { get; set; }

        public int quantidade { get; set; }

        //public int id_usuario { get; set; }

        //[ForeignKey("id_usuario")]
        //public virtual UsuarioFusp Usuario { get; set; }

        public int id_requisicao { get; set; }

        [ForeignKey("id_requisicao")]
        public virtual Requisicao Requisicao { get; set; }

        public int id_ultimo_status { get; set; }

        [ForeignKey("id_ultimo_status")]
        public virtual StatusRequisicaoMaterial StatusRequisicaoMaterial { get; set; }

        private ICollection<HistoricoRequisicao> historicos;

        public virtual ICollection<HistoricoRequisicao> Historicos
        {
            get
            {
                if (historicos == null)
                    historicos = new List<HistoricoRequisicao>();
                return historicos;
            }
            set { historicos = value; }
        }

        private string strMaterial;

        [NotMapped]
        public string StrMaterial
        {
            get
            {
                if (MaterialConsumo != null)
                    return MaterialConsumo.descricao; 
                return strMaterial;
            }
            set { strMaterial = value; }
        }
        

        //public RequisicaoMaterial(int strValue, MaterialConsumo matCon)
        //{
        //    id_material = Convert.ToInt32(matCon);
        //    quantidade = strValue;
        //}

        //public RequisicaoMaterial()
        //{ }

        //private ICollection<RequisicaoMaterial> materiais;
        //public virtual ICollection<RequisicaoMaterial> Materiais
        //{
        //    get
        //    {
        //        if (materiais == null)
        //            materiais = new List<RequisicaoMaterial>();
        //        return materiais;
        //    }
        //    set { materiais = value; }
        //}
    }

    public class RequisicaoMaterialConfiguration : EntityTypeConfiguration<RequisicaoMaterial>
    {
        public RequisicaoMaterialConfiguration()
        {
            ToTable("RequisicaoMaterial");
        }
    }
}
