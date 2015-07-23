using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{    
    public class BancoAgencia
    {
        [Key]
        public int id_agencia { get; set; }

        [Required]        
        public int id_banco  { get; set; }

        [ForeignKey("id_banco")]
        public virtual Banco Banco { get; set; }
                
        [MaxLength(10)]
        public string numero { get; set; }
                
        [MaxLength(1)]
        public string digito { get; set; }
                
        [MaxLength(30)]
        public string nome { get; set; }

        [MaxLength(14)]
        public string num_convenio { get; set; }

        public virtual ICollection<Conta> Conta { get; set; }

        
        [NotMapped]
        public string StrAgencia
        {
            get { return this.ToString(); }           
        }

        [NotMapped]
        public string StrAgenciaDigito { get { return String.Format("{0}-{1}", numero, digito); } }
        

        public override string ToString()
        {
            return String.Format("Banco:{0} nº {1}-{2} {3}", Banco.nome, numero, digito, nome);
        }
    }


    public class BancoAgenciaConfiguration : EntityTypeConfiguration<BancoAgencia>
    {
        public BancoAgenciaConfiguration()
        {            
            ToTable("BancoAgencia");
        }
    }
}