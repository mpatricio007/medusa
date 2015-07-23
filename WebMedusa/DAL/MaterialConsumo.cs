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
    public class MaterialConsumo
    {
        [Key]
        public int id_material { get; set; }

        public string descricao { get; set; }

        public int qtde_total { get; set; }

        public int qtde_minima { get; set; }

        public decimal valor { get; set; }

        public int id_unidade_medida { get; set; }

        [ForeignKey("id_unidade_medida")]
        public virtual UnidadeMedida UnidadeMedida { get; set; }

        public int id_tipo_material { get; set; }

        [ForeignKey("id_tipo_material")]
        public virtual TipoMaterial TipoMaterial { get; set; }

        private ICollection<MaterialNotaFiscal> materialNotaFiscais;
        public virtual ICollection<MaterialNotaFiscal> MaterialNotaFiscais
        {
            get
            {
                if (materialNotaFiscais == null)
                    materialNotaFiscais = new List<MaterialNotaFiscal>();
                return materialNotaFiscais;
            }

            set
            {
                materialNotaFiscais = value;
            }
        }

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
        public int Total
        {
            get
            {
                return MaterialNotaFiscais.Sum(it => it.quantidade) - RequisicaoMateriais.Where(it => it.id_ultimo_status == StatusRequisicaoMaterialBLL.Atendido).Sum(it => it.quantidade);
            }
        }
    }

    public class MaterialConsumoConfiguration : EntityTypeConfiguration<MaterialConsumo>
    {
        public MaterialConsumoConfiguration()
        {
            ToTable("MaterialConsumo");
        }
    }
} 