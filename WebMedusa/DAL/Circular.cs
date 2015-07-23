using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class Circular:Correspondencia
    {
        [Required]
        public string tags { get; set; }
        [Required]
        public bool ativa { get; set; }
    }
} 