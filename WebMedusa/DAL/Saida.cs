using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class Saida
    {
        [Key]
        public int id_saida { get; set; }

        public int nprotsai { get; set; }

        public DateTime datasai { get; set; }

        public string obssaida { get; set; }

        public int id_usu_saida { get; set; }

        [ForeignKey("id_usu_saida")]
        public virtual UsuarioFusp UsuarioSaida { get; set; }

        //public int id_entrada { get; set; }

        //[ForeignKey("id_entrada")]
        public virtual Entrada Entrada { get; set; }
  

        public int id_usu_respdevol { get; set; }

        [ForeignKey("id_usu_respdevol")]
        public virtual UsuarioFusp UsuarioResp { get; set; }

        public int ano { get; set; }

        [MaxLength (100)]
        public string destinatario { get; set; }
        
        [NotMapped]
        public int id_entrada { get; set; }
    }

    public class SaidaConfiguration : EntityTypeConfiguration<Saida>
    {
        public SaidaConfiguration()
        {
            HasRequired<Entrada>(u => u.Entrada).WithOptional(it => it.saida).Map(p => p.MapKey("id_entrada"));

            ToTable("Saida");
        }
    }
}