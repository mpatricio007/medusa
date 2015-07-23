using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class DestinatarioEmail
    {
        [Key]
        public int id_destinatario { get; set; }

        [Required]
        public int id_correspEmail { get; set; }

        [ForeignKey("id_correspEmail")]
        public virtual CorrespondenciaEmail CorrespondenciaEmail { get; set; }

        [Required]
        public string email_value { get; set; }

        [Required]
        public string nome_destinatario { get; set; }

        public DateTime? confirmacao_leitura { get; set; }

        public DateTime? enviado_em { get; set; }

        [Required]
        [MaxLength(20)]
        public string tipo { get; set; }
    } 


    public class DestinatarioEmailConfiguration : EntityTypeConfiguration<DestinatarioEmail>
    {
        public DestinatarioEmailConfiguration()
        {
            ToTable("DestinatarioEmail");
        }
    }
}