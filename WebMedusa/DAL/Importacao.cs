using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class Importacao
    {
        [Key]
        public int id_importacao { get; set; }

        public int id_usuario { get; set; }
        [ForeignKey("id_usuario")]
        public virtual Usuario Usuario { get; set; }

        public DateTime data_execucao { get; set; }

        public int id_tipo_folha_pagto { get; set; }
        [ForeignKey("id_tipo_folha_pagto")]
        public virtual TipoFolhaPagto TipoFolhaPagto { get; set; }

        public DateTime data_folha { get; set; }       

        public virtual ICollection<Lancamento> lancamentos { get; set; }
    }


    public class ImportacaoConfiguration : EntityTypeConfiguration<Importacao>
    {
        public ImportacaoConfiguration()
        {
            ToTable("Importacao");
        }
    }
}