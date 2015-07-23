using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class Remessa : IRemessa
    {
        [Key]
        public int id_remessa { get; set; }

        public int id_lote { get; set; }

        [ForeignKey("id_lote")]
        public virtual Lote LoteRemessa { get; set; }

        [Required]
        [MaxLength(30)]
        public string nome_fav_cedente { get; set; }

        [Required]
        public decimal valor { get; set; }

        [Required]
        public int id_projeto { get; set; }

        [ForeignKey("id_projeto")]
        public virtual Projeto Projeto { get; set; }

        [Required]
        [MaxLength(50)]
        public string descricao { get; set; }

        [MaxLength(200)]
        public string motivo_rejeicao { get; set; }

        [Required]
        public int id_forma_pagto { get; set; }

        [ForeignKey("id_forma_pagto")]
        public virtual FormaPagto FormaPagto { get; set; }


        public string aut_bancaria { get; set; }

        //public string aut_bancaria
        //{
        //    get 
        //    {
        //        if (!String.IsNullOrEmpty(Aut_bancaria))
        //            return String.Format("{0}.{1}.{2}.{3}.{4}.{5}",
        //                Aut_bancaria.Substring(0, 1),
        //                Aut_bancaria.Substring(1, 3),
        //                Aut_bancaria.Substring(4, 3),
        //                Aut_bancaria.Substring(7, 3),
        //                Aut_bancaria.Substring(10, 3),
        //                Aut_bancaria.Substring(13, 3));
        //        else
        //            return Aut_bancaria;
        //    }
        //    set { Aut_bancaria = value; }
        //}
        

        [Required]
        public int id_tipo_ret { get; set; }

        [ForeignKey("id_tipo_ret")]
        public virtual TipoRetorno TipoRetorno { get; set; }
        
        public int? id_lcto_conta { get; set; }

        [ForeignKey("id_lcto_conta")]
        public virtual ContaLancto ContaLancto { get; set; }

        public DateTime? data_conciliacao { get; set; }
                
        public int? id_arquivo { get; set; }

        [ForeignKey("id_arquivo")]
        public virtual ArquivosRetorno ArquivoRetorno { get; set; }

        public int? id_tipo_conciliacao { get; set; }

        [ForeignKey("id_tipo_conciliacao")]
        public virtual TipoConciliacao TipoConciliacao { get; set; }
    }

    public class RemessaConfiguration : EntityTypeConfiguration<Remessa>
    {
        public RemessaConfiguration()
        {
            HasKey(it => it.id_remessa);
            Property(it => it.id_lote).IsRequired();
            ToTable("Remessa");
        }
    }
}