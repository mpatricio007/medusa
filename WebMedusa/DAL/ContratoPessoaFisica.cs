using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class ContratoPessoaFisica
    {
        [Key]
        public int id_contrato { get; set; }

        public DateTime inicio { get; set; }

        public DateTime? termino { get; set; }

        public decimal valor { get; set; }

        public int qtde_parcelas { get; set; }

        public DateTime? rescisao { get; set; }

        public string descricao { get; set; }

        public string observacao { get; set; }

        public bool ativo { get; set; }

        public int id_projeto { get; set; }

        [ForeignKey("id_projeto")]
        public virtual Projeto Projeto { get; set; }

        public int id_tipo_contrato { get; set; }

        [ForeignKey("id_tipo_contrato")]
        public virtual TipoContrato TipoContrato { get; set; }

        public int id_pessoa { get; set; }

        [ForeignKey("id_pessoa")]
        public virtual PessoaFisica PessoaFisica { get; set; }

        public DateTime? data_relatorio { get; set; }

    }


    public class ContratoPessoaFisicaConfiguration : EntityTypeConfiguration<ContratoPessoaFisica>
    {
        public ContratoPessoaFisicaConfiguration()
        {
            ToTable("ContratoPessoaFisica");
        }
    }
}