using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class StatusFornecedor
    {
        [Key]
        public int id_status_fornecedor { get; set; }

        public string nome { get; set; }
        
        public int? ordem { get; set; }

        public bool edicao { get; set; }

        public bool enviar { get; set; }

        public bool gerar_validade { get; set; }

        public bool gerenciavel { get; set; }

    }


    public class StatusFornecedorConfiguration : EntityTypeConfiguration<StatusFornecedor>
    {
        public StatusFornecedorConfiguration()
        {
            ToTable("StatusFornecedor");
        }
    }
}