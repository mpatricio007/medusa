using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Medusa.DAL
{
    public class FinanciadorTelefone
    {
        [Key]
        public int id_telefone { get; set; }

        public Telefone telefone { get; set; }

        [Required]        
        public int id_financiador { get; set; }       

        public FinanciadorTelefone()
        {
        }

        public FinanciadorTelefone(Telefone tel)
        {
            telefone = tel;
        }
    }
}