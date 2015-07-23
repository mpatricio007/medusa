using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class RequisitoEncerramento
    {
        [Key]
        public int id_req_enc { get; set; }

        [MaxLength(250)]
        public string descricao { get; set; }

        public DateTime? data_solucao { get; set; }

        public string solucao { get; set; }

        public bool? status { get; set; }

        public int? id_projeto { get; set; }
        [ForeignKey("id_projeto")]
        public virtual Projeto Projeto { get; set; }

        public int id_usuario { get; set; }
        [ForeignKey("id_usuario")]
        public virtual Usuario Usuario { get; set; }
    }

    public class RequisitoEncerramentoConfiguration : EntityTypeConfiguration<RequisitoEncerramento>
    {
        public RequisitoEncerramentoConfiguration()
        {
            ToTable("RequisitoEncerramento");
        }
    }
}