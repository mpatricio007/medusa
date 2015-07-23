using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class EnderecoProjeto
    {
        [Key]
        public int id_ender_projeto { get; set; }

        //[MaxLength(50)]
        //public string endereco_logradouro { get; set; }

        //[MaxLength(10)]
        //public string endereco_numero { get; set; }

        //[MaxLength(50)]
        //public string endereco_complemento { get; set; }

        //[MaxLength(50)]
        //public string endereco_bairro { get; set; }

        //[MaxLength(50)]
        //public string endereco_cidade { get; set; }

        //[MaxLength(2)]
        //public string endereco_uf { get; set; }

        //[MaxLength(9)]
        //public string endereco_cep { get; set; }

        //[MaxLength(50)]
        //public string endereco_pais { get; set; }

        public Endereco endereco { get; set; }

        public int id_tipo_ender { get; set; }
        [ForeignKey("id_tipo_ender")]
        public virtual TipoEndereco TipoEndereco { get; set; }

        public int id_projeto { get; set; }
        [ForeignKey("id_projeto")]
        public virtual Projeto Projeto { get; set; }
    }


    public class EnderecoProjetoConfiguration : EntityTypeConfiguration<EnderecoProjeto>
    {
        public EnderecoProjetoConfiguration()
        {
            ToTable("EnderecoProjeto");
        }
    }
}