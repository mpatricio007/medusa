using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel;
using Medusa.LIB;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class NfMaterial
    {
        [Key]
        public int id_nf_material { get; set; }

        public int numero { get; set; }

        public decimal valor { get; set; }

        public string nomeEmpresa { get; set; }

        public string arquivo { get; set; }

        public DateTime data { get; set; }

        public string obs { get; set; }

        [NotMapped]
        public string fileName
        {
            get
            {

                return arquivo.Split(Convert.ToChar("'"))[1];
            }

        }

        private ICollection<MaterialNotaFiscal> materialNotaFiscais;
        //[Search("materiais", "MaterialCosumo.descricao")]
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

    }

    public class NfMaterialConfiguration : EntityTypeConfiguration<NfMaterial>
    {
        public NfMaterialConfiguration()
        {
            ToTable("NfMaterial");
        }
    }
}