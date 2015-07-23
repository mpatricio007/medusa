using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class RepresentanteLegal : Representante
    {
        [Key]
        public int id_representante_legal { get; set; }
    }


    public class RepresentanteLegalConfiguration : EntityTypeConfiguration<RepresentanteLegal>
    {
        public RepresentanteLegalConfiguration()
        {
            ToTable("RepresentanteLegal");
        }
    }
}