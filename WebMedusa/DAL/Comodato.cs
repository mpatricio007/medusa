using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class Comodato
    {
        [Key]
        public int id_comodato { get; set; }

        public int id_projeto { get; set; }

        [ForeignKey("id_projeto")]
        public virtual Projeto Projeto { get; set; }

        [MaxLength(100)]
        public string arquivo { get; set; }

        public string num_comodato { get; set; }

        public int id_ultimo_status { get; set; }

        [ForeignKey("id_ultimo_status")]
        public virtual StatusComodato StatusComodatos { get; set; }

        [NotMapped]
        public string fileName
        {
            get
            {

                return arquivo.Split(Convert.ToChar("'"))[1];
            }

        }

        private ICollection<HistoricoComodato> historicos;

        public virtual ICollection<HistoricoComodato> Historicos
        {
            get
            {
                if (historicos == null)
                    historicos = new List<HistoricoComodato>();
                return historicos;
            }
            set { historicos = value; }
        }

        private ICollection<Patrimonio> patrimonios;

        public virtual ICollection<Patrimonio> Patrimonios
        {
            get
            {
                if (patrimonios == null)
                    patrimonios = new List<Patrimonio>();
                return patrimonios;
            }
            set { patrimonios = value; }
        }

    }


    public class ComodatoConfiguration : EntityTypeConfiguration<Comodato>
    {
        public ComodatoConfiguration()
        {
            ToTable("Comodato");
        }
    }
}