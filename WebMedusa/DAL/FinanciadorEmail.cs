using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Medusa.DAL
{
    public class FinanciadorEmail
    {
        [Key]
        public int id_email { get; set; }
        
        public Email email { get; set; }

        [Required]        
        public int id_financiador { get; set; }
      
        public FinanciadorEmail()
        {

        }

        public FinanciadorEmail(Email em)
        {
            email = em;            
        }
    }
}