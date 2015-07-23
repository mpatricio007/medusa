using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public enum TipoCoordenador 
    { 
        coordenador = 1, 
        subcoordenador = 2
    }

    public class ProjetoCoordenadores
    {
        [Key]
        public int id_proj_coord { get; set; }

        public int id_coordenador { get; set; }

        [ForeignKey("id_coordenador")]
        public virtual Coordenador Coordenador { get; set; }

        public int id_projeto { get; set; }

        [ForeignKey("id_projeto")]
        public virtual Projeto Projeto { get; set; }

        public DateTime inicio { get; set; }

        public DateTime? termino { get; set; }

        public string observacao { get; set; }

        public int intTipo { get; set; }

        [NotMapped]
        public TipoCoordenador tipo
        {
            get { return (TipoCoordenador)intTipo; }
            set { intTipo = (int)value; }
        }

        [NotMapped]
        public string HtmlPaginaCoordenador
        {
            get
            {
                return String.Format(@"<a href='../scp/coordenadores.aspx?pk={0}'>{1}</a>",
                   id_coordenador.ToString().Criptografar(), Coordenador.PessoaFisica.nome);
            }
        }

    }
    public class ProjetoCoordenadoresConfiguration : EntityTypeConfiguration<ProjetoCoordenadores>
    {
        public ProjetoCoordenadoresConfiguration()
        {
            ToTable("ProjetoCoordenadores");
        }
    }
}