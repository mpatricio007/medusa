using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class vContratosPessoa
    {
        [Key]
        public Int64 id_contrato { get; set; }

        public string nome { get; set; }

        public string rg { get; set; }

        public string cpf { get; set; }

        public string tipo { get; set; }

        public string descricao { get; set; }

        public int? projeto { get; set; }

        public decimal? valor { get; set; }

        public DateTime? inicio { get; set; }

        public DateTime? termino { get; set; }

        public string obs { get; set; }

    }

    public class vContratosPessoaConfiguration : EntityTypeConfiguration<vContratosPessoa>
    {
        public vContratosPessoaConfiguration()
        {
            ToTable("vContratosPessoa");
        }
    }
}