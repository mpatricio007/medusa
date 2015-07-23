using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel;

namespace Medusa.DAL
{
    public class TipoMaterial
    {
        [Key]
        public int id_tipo_material { get; set; }

        public string nome { get; set; }
    }


    public class TipoMaterialConfiguration : EntityTypeConfiguration<TipoMaterial>
    {
        public TipoMaterialConfiguration()
        {
            ToTable("TipoMaterial");
        }
    }
}