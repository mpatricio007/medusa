using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    [Serializable]
    public class PessoaTelefone
    {
        [Key]
        public int id_telefone { get; set; }

        public Telefone telefone { get; set; }

        
        public int id_pessoa { get; set; }

        [NonSerialized()]
        private Pessoa pessoa;

        [ForeignKey("id_pessoa")]
        public virtual Pessoa Pessoa
        {
            get { return pessoa; }
            set { pessoa = value; }
        }

        public PessoaTelefone()
        {
        }

        public PessoaTelefone(Telefone tel)
        {
            telefone = tel;
        }
    }
}