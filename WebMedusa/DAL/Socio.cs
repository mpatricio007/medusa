using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class Socio : Representante
    {
        [Key]
        public int id_socio { get; set; }

        [Required]
        public decimal? cota { get; set; }

        public int intTipo { get; set; }

        [NotMapped]
        public TipoInscricao tipo
        {
            get 
            {
                intTipo = intTipo == 0 ? 1 : intTipo;
                return (TipoInscricao)intTipo; 
            }
            set { intTipo = (int)value; }
        }

        [NotMapped]
        public string strDocumentos
        {
            get
            {
                return tipo == TipoInscricao.CPF ? String.Format("CPF: {0} - RG: {1}",cpf.Value,rg) : String.Format("CNPJ: {0}",cnpj.Value);
            }
        }
        
        public CNPJ cnpj { get; set; }


    }


    public class SocioConfiguration : EntityTypeConfiguration<Socio>
    {
        public SocioConfiguration()
        {
            ToTable("Socio");
        }
    }
}