using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Medusa.DAL
{
    public class FormaPagtoDespesa
    {
        [Key]
        public int id_forma { get; set; }
        
        public string nome { get; set; }        
    }


    public class FormaPagtoDespesaConfiguration : EntityTypeConfiguration<FormaPagtoDespesa>
    {
        public FormaPagtoDespesaConfiguration()
        {
            ToTable("FormaPagtoDespesa");
        }
    }
}