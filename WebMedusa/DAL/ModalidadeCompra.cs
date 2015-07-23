using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class ModalidadeCompra
    {
        [Key]
        public int id_modalidade_compra { get; set; }

        [Required]
        [MaxLength(50)]
        public string nome { get; set; }

        [MaxLength(150)]
        public string descricao { get; set; }

        public bool status { get; set; }


    }

    public class ModalidadeCompraConfiguration : EntityTypeConfiguration<ModalidadeCompra>
    {
        public ModalidadeCompraConfiguration()
        {
            ToTable("ModalidadeCompra");
        }
    }
}