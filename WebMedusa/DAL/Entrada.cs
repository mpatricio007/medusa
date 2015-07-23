using System;using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class Entrada
    {
        [Key]
        public int id_entrada { get; set; }//

        public int nprotent { get; set; }//
       
        public DateTime dataent  { get; set; }//

        public DateTime dataprot { get; set; }//
       
        public int? codproj   { get; set; }//
       
        [MaxLength(50)]
        public string tipodocent { get; set; }//
       
        [MaxLength(50)]
        public string numdocent { get; set; }//
       
        public decimal? valorent { get; set; }//
       
        public string descrent { get; set; }//   
               
        public string obsent { get; set; }//
       
        [MaxLength(100)]
        public string enviadoent { get; set; }//
              
        public int? codproja { get; set; }//
        
        public int id_usu_para { get; set; }

        [ForeignKey("id_usu_para")]
        public virtual UsuarioFusp UsuarioPara { get; set; }//

        public int? id_valor_moeda { get; set; }//

        [ForeignKey("id_valor_moeda")]//
        public virtual Moeda ValorMoeda { get; set; }//

        public int ano { get; set; }//

        public int id_usu_de { get; set; }//

        [ForeignKey("id_usu_de")]
        public virtual UsuarioFusp UsuarioDe { get; set; }

        public int id_usu_entrada { get; set; }//

        [ForeignKey("id_usu_entrada")]//
        public virtual UsuarioFusp UsuarioEntrada { get; set; }//

        #region oldSREC
        //public int? id_projeto { get; set; }
        //[ForeignKey("id_projeto")]
        //public virtual Projeto Projeto { get; set; }

        //public int? id_ultimo_status { get; set; }
        //[ForeignKey("id_ultimo_status")]
        //public virtual StatusEntrada UltimoStatus { get; set; }

        //public int? id_ultimo_para { get; set; }
        //[ForeignKey("id_ultimo_para")]
        //public virtual UsuarioFusp UltimoPara { get; set; }

        //[NotMapped]
        //public virtual string ultima_obs
        //{
        //    get
        //    {
        //        return Historicos.Last().obs;
        //    }
        //}

        //[NotMapped]
        //public virtual UsuarioFusp UltimoDe
        //{
        //    get
        //    {
        //        return Historicos.Last().UsuarioDe;
        //    }
        //}

        //[NotMapped]
        //public string HtmlPaginaEntrada
        //{
        //    get
        //    {
        //        return String.Format(@"<a href='../SREC/Entradas.aspx?pk={0}' >detalhes</a>",
        //           id_entrada.ToString().Criptografar());
        //    }
        //}

        //[NotMapped]
        //public virtual DateTime? data_ultimo_historico
        //{
        //    get
        //    {
        //        return Historicos.Last().data;
        //    }
        //}
        #endregion
        private ICollection<HistoricoEntrada> historicos;

        [Invisible]
        public virtual ICollection<HistoricoEntrada> Historicos
        {
            get
            {
                if (historicos == null)
                    historicos = new List<HistoricoEntrada>();
                return historicos;
            }
            set { historicos = value; }
        }

        public virtual Saida saida { get; set; }

        #region NewSREC

        [NotMapped]
        public virtual HistoricoEntrada EstadoAtual
        {
            get
            {
                return Historicos.OrderByDescending(it => it.id_historico_entrada).DefaultIfEmpty(new HistoricoEntrada()).FirstOrDefault();
            }
        }

        #endregion
    }

    public class EntradaConfiguration : EntityTypeConfiguration<Entrada>
    {
        public EntradaConfiguration()
        {
            ToTable("Entrada");
        }
    }
}