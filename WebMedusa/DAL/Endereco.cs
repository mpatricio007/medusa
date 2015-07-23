using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    [ComplexType]
    [Serializable]
    public class Endereco
    {
        [MaxLength(100)]
        public string logradouro { get; set; }

        [MaxLength(10)]
        public string numero { get; set; }

        [MaxLength(50)]
        public string complemento { get; set; }

        [MaxLength(30)]
        public string bairro { get; set; }

        [MaxLength(100)]
        public string cidade { get; set; }

        [MaxLength(10)]
        public string uf { get; set; }

        [MaxLength(9)]
        public string cep { get; set; }

        [MaxLength(30)]
        public string pais { get; set; }

        public override string ToString()
        {   
            return String.Format("{0},{1} {2} {3} {4}-{5} {6} CEP:{7}",
                logradouro,
                numero,
                complemento,
                bairro,
                cidade,
                uf,
                pais,                
                cep);
        }
    }    
}

  