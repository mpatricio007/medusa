using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class Correspondencia
    {
        [Key]
        public int id_correspondencia { get; set; }

        [Required]
        public int num { get; set; }       

        [Required]
        [MaxLength(50)]
        public string projeto { get; set; }

        public DateTime data { get; set; }

        [Required]
        [MaxLength(50)]
        public string destinatario { get; set; }
        
        [MaxLength(500)]
        public string descricao { get; set; }
                
        [MaxLength(100)]
        public string arquivo { get; set; }

        [NotMapped]
        public string fileName      
        {
            get
            {
                
                return arquivo.Split(Convert.ToChar("'"))[1];
            }
        
        }

        public int id_usuario { get; set; }

        [ForeignKey("id_usuario")]
        public virtual UsuarioFusp Usuario { get; set; }

        public virtual ICollection<CorrespondenciaEmail> CorrespondenciaEmails { get; set; }


        //public int? id_arquivo { get; set; }

        //[ForeignKey("id_arquivo")]
        //public virtual Arquivo arq { get; set; }
    }


    public class CorrespondenciaConfiguration : EntityTypeConfiguration<Correspondencia>
    {
        public CorrespondenciaConfiguration()
        {
            Map<Carta>(it => it.Requires("id_tipoCorrespondencia").HasValue(1));
            Map<Circular>(it => it.Requires("id_tipoCorrespondencia").HasValue(2));
            Map<Cobranca>(it => it.Requires("id_tipoCorrespondencia").HasValue(3));
            Map<CircularInterna>(it => it.Requires("id_tipoCorrespondencia").HasValue(4));
            Map<CartaBancaria>(it => it.Requires("id_tipoCorrespondencia").HasValue(5));
            ToTable("Correspondencia");
        }
    }
}