using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public enum TipoConta
    {
        // cc = Conta Corrente; cp = Conta Poupança
        cc = 0,
        cp = 1
    }

    [ComplexType]
    public class ContaPessoaFisica
    {
        public string agencia { get; set; }

        public string digitoAgencia { get; set; }

        public string conta { get; set; }

        public string digitoConta { get; set; }
        
        public int? id_banco { get; set; }

        public int? intTipoconta { get; set; }

        [NotMapped]
        public TipoConta tipoConta
        {
            get { return (TipoConta)intTipoconta; }
            set { intTipoconta = (int)value; }
        }
    }


    public class ContaPessoaFisicaConfiguration : ComplexTypeConfiguration<ContaPessoaFisica>
    {
        public ContaPessoaFisicaConfiguration()
        {
            Property(c => c.id_banco).IsOptional();
            Property(c => c.intTipoconta).IsOptional();            
        }
    }
}