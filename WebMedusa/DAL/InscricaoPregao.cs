using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class InscricaoPregao
    {
        [Key]
        public int id_inscricao_pregao { get; set; }

        public string nome { get; set; }

        public CPF cpf { get; set; }

        public string razao_social { get; set; }

        public string nome_fantasia { get; set; }

        public CNPJ cnpj { get; set; }

        public string email_value { get; set; }

        public string senha { get; set; }

        public int nivel { get; set; }

        public bool status { get; set; }

        public bool primeiro_acesso { get; set; }

        public virtual Endereco ender { get; set; }

        public string tel_comercial_value { get; set; }

        public string tel_residencial_value { get; set; }

        public string tel_celular_value { get; set; }

        public int intTipo { get; set; }

        [NotMapped]
        public TipoInscricao tipo
        {
            get
            {
                intTipo = intTipo == 0 ? 1 : intTipo;
                return (TipoInscricao)intTipo;
            }
            set { intTipo = (int)value; }
        }

        //private ICollection<InscricaoPregaoTelefone> telefone;
        //public virtual ICollection<InscricaoPregaoTelefone> Telefones
        //{
        //    get
        //    {
        //        if (telefone == null)
        //            telefone = new List<InscricaoPregaoTelefone>();
        //        return telefone;
        //    }

        //    set
        //    {
        //        telefone = value;
        //    }
        //}

        //[NotMapped]
        //public bool SetTelefones { get; set; }  
    }

    public class InscricaoPregaoConfiguration : EntityTypeConfiguration<InscricaoPregao>
    {
        public InscricaoPregaoConfiguration()
        {
            //HasMany(f => f.Telefones).WithRequired().HasForeignKey(t => t.id_inscricao_pregao).WillCascadeOnDelete();
            ToTable("InscricaoPregao");
        }
    }
}