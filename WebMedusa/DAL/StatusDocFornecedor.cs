using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel;

namespace Medusa.DAL
{
    public class StatusDocFornecedor
    {
        [Key]
        public int id_status_docFornecedor { get; set; }

        public string nome { get; set; }
    }


    public class StatusDocFornecedorConfiguration : EntityTypeConfiguration<StatusDocFornecedor>
    {
        public StatusDocFornecedorConfiguration()
        {
            ToTable("StatusDocFornecedor");
        }
    }
}