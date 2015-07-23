using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class ProjetoA
    {
        [Key]
        public int id_projetoa { get; set; }

        public int codigo { get; set; }

        public DateTime data_abertura { get; set; }

        public string referencia { get; set; }

        public string coordenador { get; set; }

        public string patrocinador { get; set; }

        public string endereco { get; set; }

        public string titulo { get; set; }

        public string observacao { get; set; }
    }
    public class ProjetoAConfiguration : EntityTypeConfiguration<ProjetoA>
    {
        public ProjetoAConfiguration()
        {            
            ToTable("ProjetoA");
        }
    }
}