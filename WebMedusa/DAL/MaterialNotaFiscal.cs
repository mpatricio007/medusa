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
    [Serializable]
    public class MaterialNotaFiscal
    {
        [Key]
        public int id_material_nf { get; set; }

        public int id_material { get; set; }

        [ForeignKey("id_material")]
        public virtual MaterialConsumo MaterialConsumo { get; set; }

        public int id_nf_material { get; set; }

        [ForeignKey("id_nf_material")]
        public virtual NfMaterial NfMaterial { get; set; }

        public int  quantidade { get; set; }


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
    }


    public class MaterialNotaFiscalConfiguration : EntityTypeConfiguration<MaterialNotaFiscal>
    {
        public MaterialNotaFiscalConfiguration()
        {
            ToTable("MaterialNotaFiscal");
        }
    }
}