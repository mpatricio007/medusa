using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class Representante
    {
        public string nome { get; set; }

        public CPF cpf { get; set; }

        public string rg { get; set; }

        public Email email { get; set; }

        public int id_fornecedor { get; set; }

        [ForeignKey("id_fornecedor")]
        public virtual Fornecedor Fornecedor { get; set; }
    }
}