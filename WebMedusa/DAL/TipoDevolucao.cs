using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class TipoDevolucao
    {
        [Key]
        public int id_tipo_devolucao { get; set; }

        public string nome { get; set; }

        public string sigla { get; set; }
    }


    public class TipoDevolucaoConfiguration : EntityTypeConfiguration<TipoDevolucao>
    {
        public TipoDevolucaoConfiguration()
        {
            ToTable("TipoDevolucao");
        }
    }
}