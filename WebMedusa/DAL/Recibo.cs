using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using Medusa.LIB;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class Recibo
    {
        [Key]
        public int id_recibo { get; set; }

        public string nome { get; set; }

        public DateTime data { get; set; }

        public string tipo_pagamento { get; set; }

        public string descricao { get; set; }

        public decimal valor { get; set; }

        public string observacao { get; set; }

        public string cpfUsuario { get; set; }

        [ForeignKey("cpfUsuario")]
        public virtual vUsuariosDemonstrativo Usuario { get; set; }

        public int id_recibo_curso { get; set; }

        [ForeignKey("id_recibo_curso")]
        public virtual ReciboCurso ReciboCurso { get; set; }

        public string motivo_cancel { get; set; }

        public bool? status_recibo { get; set; }

        private ICollection<ReciboCheque> recibocheques;

        public virtual ICollection<ReciboCheque> ReciboCheques
        {
            get
            {
                if (recibocheques == null)
                    recibocheques = new List<ReciboCheque>();
                return recibocheques;
            }
            set { recibocheques = value; }
        }

        public int intTipo { get; set; }

        public CNPJ cnpj { get; set; }

        public CPF cpf { get; set; }

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
                return tipo == TipoInscricao.CPF ? String.Format("CPF: {0}", cpf.Value) : String.Format("CNPJ: {0}", cnpj.Value);
            }
        }
    }

    public class ReciboConfiguration : EntityTypeConfiguration<Recibo>
    {
        public ReciboConfiguration()
        {
            ToTable("Recibo");
        }
    }
}