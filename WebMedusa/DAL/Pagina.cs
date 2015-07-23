using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public enum TipoPagina:int
    {
        pagina = 0,
        controle = 1
    }

    public class Pagina
    {
        [Key]
        public int id_pagina { get; set; }

        [Required]
        [MaxLength(100)]
        public string url { get; set; }

        [Required]
        [MaxLength(50)]
        public string nome { get; set; }

        public int intTipo { get; set; }

        [NotMapped]
        public TipoPagina tipo
        {
            get { return (TipoPagina)intTipo; }
            set { intTipo = (int)value; }
        }

        public virtual ICollection<MenuPagina> MenuPaginas { get; set; }

        public virtual ICollection<Sistema> Sistema { get; set; }
    }

    public class PaginaConfiguration : EntityTypeConfiguration<Pagina>
    {
        public PaginaConfiguration()
        {
            ToTable("Pagina");
        }
    }
}