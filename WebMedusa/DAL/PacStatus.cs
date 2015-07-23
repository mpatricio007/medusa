using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public enum PacAcao
    {
        reprovar = 0,
        aprovar = 1,
        cancelar = 2,
        editar = 3
    }

    public class PacStatus
    {
        [Key]
        public int id_pac_status { get; set; }

        public string descricao { get; set; }

        [Required]
        public int id_modalidade_compra { get; set; }
        [ForeignKey("id_modalidade_compra")]
        public virtual ModalidadeCompra ModalidadeCompra { get; set; }
        
        public int ordem { get; set; }

        public bool ativo { get; set; }

        public bool visivel { get; set; }
    }

    public class PacStatusConfiguration : EntityTypeConfiguration<PacStatus>
    {
        public PacStatusConfiguration()
        {
            //HasKey(it => it.tipo).HasKey(t => t.ordem);
            ToTable("PacStatus");
        }
    }
}