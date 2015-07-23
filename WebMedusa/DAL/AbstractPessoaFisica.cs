using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations; using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public abstract class AbstractPessoaFisica
    {
        public int id_pessoa { get; set; }

        [ForeignKey("id_pessoa")]
        public virtual PessoaFisica PessoaFisica { get; set; }

      
    }
}
