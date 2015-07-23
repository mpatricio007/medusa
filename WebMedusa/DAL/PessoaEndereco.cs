using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    [Serializable]
    public class PessoaEndereco
    {
        [Key]
        public int id_endereco { get; set; }

        private Endereco ender;

        public Endereco endereco
        {
            get 
            {
                if (ender == null)
                    ender = new Endereco();
                return ender; 
            }
            set { ender = value; }
        }
        
        public int id_pessoa { get; set; }


        [NonSerialized()]
        private Pessoa pessoa;

        [ForeignKey("id_pessoa")]
        public virtual Pessoa Pessoa
        {
            get { return pessoa; }
            set { pessoa = value; }
        }

        public PessoaEndereco()
        {

        }
        
        public PessoaEndereco(Endereco ender)
        {
            endereco = ender;
        }
    }
}