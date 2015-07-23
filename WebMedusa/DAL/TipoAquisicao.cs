using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations;

namespace Medusa.DAL
{
    public class TipoAquisicao
    {
        [Key]
        public int id_tipo_aquisicao { get; set; }

        public string nome { get; set; }

        public string descricao { get; set; }

        public bool status { get; set; }

    }

    public class TipoAquisicaoConfiguration : EntityTypeConfiguration<TipoAquisicao>
    {
        public TipoAquisicaoConfiguration()
        {            
            ToTable("TipoAquisicao");
        }
    }
}