using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class ReferenciaBancaria
    {
        [Key]
        public int id_referencia_bancaria { get; set; }

        public int id_fornecedor { get; set; }
        [ForeignKey("id_fornecedor")]
        public virtual Fornecedor Fornecedor { get; set; }

        [MaxLength(100)]
        public string banco { get; set; }

        [MaxLength(50)]
        public string contato { get; set; }

        [MaxLength(10)]
        public string agencia { get; set; }

        private Telefone tel;
        public Telefone telefone
        {
            get
            {
                if (tel == null)
                    tel = new Telefone();
                return tel;
            }
            set { tel = value; }
        }
    }


    public class ReferenciaBancariaConfiguration : EntityTypeConfiguration<ReferenciaBancaria>
    {
        public ReferenciaBancariaConfiguration()
        {
            ToTable("ReferenciaBancaria");
        }
    }
}