using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class Inscricao
    {
        [Key]
        public int id_inscricao { get; set; }

        //    [ForeignKey("id_vaga")]
        //    public int id_vaga { get; set; }

        //    [ForeignKey("id_candidato")]
        //    public int id_candidato { get; set; }

        //    public bool aceite { get; set; }

        //}


        public class InscricaoConfiguration : EntityTypeConfiguration<Inscricao>
        {
            public InscricaoConfiguration()
            {
                ToTable("Inscricao");
            }
        }
    }
}