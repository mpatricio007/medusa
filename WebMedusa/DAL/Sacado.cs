using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class Sacado : AbstractPessoaFisica
    {
        [Key]
        public int id_sacado { get; set; }

        public DateTime data_cadastro { get; set; }


        private ICollection<EventoSacado> eventosSacados;

        public virtual ICollection<EventoSacado> EventosSacados
        {
            get
            {
                if (eventosSacados == null)
                    eventosSacados = new List<EventoSacado>();
                return eventosSacados;
            }
            set { eventosSacados = value; }
        }

        [NotMapped]
        public string HtmlPaginaSacados
        {
            get
            {
                return String.Format(@"<a href='../cobranca/sacados.aspx?pk={0}'>{1}</a>",
                   id_sacado.ToString().Criptografar(), PessoaFisica.nome );
            }
        }

    }

    public class SacadoConfiguration : EntityTypeConfiguration<Sacado>
    {
        public SacadoConfiguration()
        {            
            ToTable("Sacado");
        }
    }
}