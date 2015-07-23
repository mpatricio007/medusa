using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using Medusa.BLL;

namespace Medusa.DAL
{
    public class vDespesasToDbf
    {        
        
        [Key]
        public Int64 id { get; set; }

        public int id_lancto { get; set; }

        public Int32 projeto { get; set; }

        public string nome { get; set; }

        public DateTime data_pagto { get; set; }

        public string codigo { get; set; }

        public string rp { get; set; }

        public Decimal debitoProjeto { get; set; }

        public Decimal creditoProjeto { get; set; }


        public int id_lancto_tipo { get; set; }

        [ForeignKey("id_lancto_tipo")]
        public virtual LancamentoTipo tipo { get; set; }


        //[NotMapped]
        //public string HtmlPaginaDespesa 
        //{
        //    get 
        //    {
        //        return String.Format("<a href='{0}'>selecionar</a>");
        //    }
        //}

        [NotMapped]
        public string HtmlPaginaProjeto
        {
            get
            {
                var p = new ProjetoBLL();
                p.GetProjeto(projeto);
                //return String.Format(@"<a href='..\..\sistemas\scp\ProjetosDefinitivos.aspx?pk={0}'>{1}</a>", p.ObjEF.id_projeto.ToString().Criptografar(), projeto);
                return p.ObjEF.HtmlPaginaProjeto;
            }
        }
    }

    public class vDespesasToDbfConfiguration : EntityTypeConfiguration<vDespesasToDbf>
    {
        public vDespesasToDbfConfiguration()
        {
            ToTable("vDespesasToDbf");
        }
    }
}