using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class Autonomo : AbstractPessoaFisica
    {
        [Key]
        public int id_autonomo { get; set; }

        public int num_dependente { get; set; }

        public string StrNome
        {
            get
            {                
                return PessoaFisica.nome;
            }
        }


        //private ICollection<DespesaAutonomo> pagtos;

        //public virtual ICollection<DespesaAutonomo> Pagtos
        //{
        //    get
        //    {
        //        if (pagtos == null)
        //            pagtos = new List<DespesaAutonomo>();
        //        return pagtos;
        //    }
        //    set { pagtos = value; }
        //}

    }


    public class AutonomoConfiguration : EntityTypeConfiguration<Autonomo>
    {
        public AutonomoConfiguration()
        {
            ToTable("Autonomo");
        }
    }
}
