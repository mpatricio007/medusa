using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel;

namespace Medusa.DAL
{
    public class TipoFolhaPagto
    {
        [Key]
        public int id_tipo_folha_pagto { get; set; }

        public string nome { get; set; }

        public string sigla { get; set; }
    }


    public class TipoFolhaPagtoConfiguration : EntityTypeConfiguration<TipoFolhaPagto>
    {
        public TipoFolhaPagtoConfiguration()
        {
            ToTable("TipoFolhaPagto");
        }
    }
}