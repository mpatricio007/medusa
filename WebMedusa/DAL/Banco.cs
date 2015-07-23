using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class Banco
    {
        [Key]
        public int id_banco { get; set; }

        [Required]
        [MaxLength(3)]
        public string codigo { get; set; }

        [Required]
        [MaxLength(30)]
        public string nome { get; set; }
        
        public virtual ICollection<BancoAgencia> BancoAgencia { get; set; }

        public virtual ICollection<TipoLcto> TiposLctos { get; set; }

        [NotMapped]
        public string StrCodigoNome
        {
            get
            {
                return String.Format("{0} - {1}", codigo, nome);
            }
        }
    }


    public class BancoConfiguration : EntityTypeConfiguration<Banco>
    {
        public BancoConfiguration()
        {
            HasMany(b => b.TiposLctos).WithRequired(t => t.Banco).WillCascadeOnDelete(false);
            ToTable("Banco");
        }
    }
}