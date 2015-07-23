using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class FormaPagto
    {
        [Key]
        public int id_forma_pagto { get; set; }
        
        [Required]
        public int id_banco { get; set; }

        [ForeignKey("id_banco")]
        public virtual Banco banco { get; set; }

        [Required]
        [MaxLength(2)]
        public string codigo { get; set; }

        [Required]
        [MaxLength(30)]
        public string nome { get; set; }

        [Required]
        public bool eh_mesmo_banco { get; set; }

        [Required]
        public bool eh_pagamentos { get; set; }

        public decimal? valor_min { get; set; }

        public decimal? valor_max { get; set; }

        //public string finalidade_pagto { get; set; }

        public string cod_camara { get; set; }

        public string finalidade_pagto { get; set; }
    }


    public class FormaPagtoConfiguration : EntityTypeConfiguration<FormaPagto>
    {
        public FormaPagtoConfiguration()
        {
            ToTable("FormaPagto");
        }
    }
}