using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public enum TipoTelefone
    {
        comercial = 0,
        residencial = 1,
        celular = 2,
        fax = 3
    }

    [ComplexType]
    [Serializable]
    public class Telefone
    {
        //[MaxLength(20)]
        public string value { get; set; }

        [MaxLength(10)]
        public string ramal { get; set; }

        public int? tipo { get; set; }

        [NotMapped]
        public TipoTelefone tipoTel
        {
            get { return (TipoTelefone)tipo; }
            set { tipo = (int)value; }
        }

        [NotMapped]
        public string valueToString
        {
            get
            {
                return this.ToString();
            }
        }

        public override string ToString()
        {
            return String.Format("{0}{1} {2}", value, String.IsNullOrEmpty(ramal) ? String.Empty : String.Format(" r.{0}", ramal),
                tipo.HasValue ? Enum.GetName(typeof(TipoTelefone), tipo) : String.Empty);
        } 

        public Telefone()
        {

        }


        public Telefone(string strValue,string StrRamal,TipoTelefone tTel)
        {
            value = strValue;
            ramal = StrRamal;
            tipoTel = tTel;
        }

        public static implicit operator Telefone(PessoaTelefone pe)
        {
            return pe.telefone;
        }
    }
}