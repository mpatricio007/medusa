using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class ContasProjeto
    {
        [Key]
        public int id_conta_projeto { get; set; }

        public int id_conta { get; set; }
        [ForeignKey("id_conta")]
        public virtual Conta Conta { get; set; }

        public int id_projeto { get; set; }
        [ForeignKey("id_projeto")]
        public virtual Projeto Projeto { get; set; }

        public bool conta_pagadora { get; set; }
    }

    public class ContasProjetoConfiguration : EntityTypeConfiguration<ContasProjeto>
    {
        public ContasProjetoConfiguration()
        {
            ToTable("ContasProjeto");
        }
    }
}