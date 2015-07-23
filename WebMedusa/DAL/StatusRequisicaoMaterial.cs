using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel;

namespace Medusa.DAL
{
    public class StatusRequisicaoMaterial
    {
        [Key]
        public int id_status_requisicao_material { get; set; }

        public string nome { get; set; }

        public int? ordem { get; set; }
    }


    public class StatusRequisicaoMaterialConfiguration : EntityTypeConfiguration<StatusRequisicaoMaterial>
    {
        public StatusRequisicaoMaterialConfiguration()
        {
            ToTable("StatusRequisicaoMaterial");
        }
    }
}