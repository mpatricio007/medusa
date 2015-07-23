using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class CorrespondenciaEmail
    {
        [Key]
        public int id_correspEmail { get; set; }

        [Required]
        public int id_correspondencia { get; set; }

        [ForeignKey("id_correspondencia")]
        public virtual Correspondencia Correspondencia { get; set; }

        [Required]
        public DateTime data { get; set; }       

        [Required]
        [MaxLength(100)]
        public string assunto { get; set; }
        
        public string corpo { get; set; }

        public DateTime? enviadoEm { get; set; }

        public virtual ICollection<DestinatarioEmail> DestinatarioEmails { get; set; }

    }


    public class CorrespondenciaEmailConfiguration : EntityTypeConfiguration<CorrespondenciaEmail>
    {
        public CorrespondenciaEmailConfiguration()
        {
            ToTable("CorrespondenciaEmail");
        }
    }
}