using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class FinalidadePagto
    {
        [Key]
        public int id_fin_pagto { get; set; }

        [Required]
        [MaxLength(2)]
        public string codigo { get; set; }

        [Required]
        [MaxLength(30)]
        public string nome { get; set; }

        [Required]
        public bool eh_outros { get; set; }

        [Required]
        public int id_banco { get; set; }

        [ForeignKey("id_banco")]
        public virtual Banco Banco { get; set; }
    }
        
    public class FinalidadePagtoConfiguration : EntityTypeConfiguration<FinalidadePagto>
    {
        public FinalidadePagtoConfiguration()
        {

            ToTable("FinalidadePagto");          
        }
    }
}