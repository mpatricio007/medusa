using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel;

namespace Medusa.DAL
{
    public class UnidadeMedida
    {
        [Key]
        public int id_unidade_medida { get; set; }

        public string descricao { get; set; }
    }


    public class UnidadeMedidaConfiguration : EntityTypeConfiguration<UnidadeMedida>
    {
        public UnidadeMedidaConfiguration()
        {
            ToTable("UnidadeMedida");
        }
    }
}