using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class CarregarFolhaAntiga
    {
        [Key]
        public Int64 id { get; set; }

        public Decimal ?PENSAOPF { get; set; }
        public Decimal ?DIF012004 { get; set; }
        public Int64 codfol { get; set; }
        public String mesanofol { get; set; }
        public String tipofol { get; set; }
        public Int64 codpf { get; set; }
        public DateTime datapf { get; set; }
        public Int64 Codproj { get; set; }
        public Decimal valorpf { get; set; }
        public Decimal ?irrfpf { get; set; }
        public Decimal ?isspf { get; set; }
        public String  ctaproj { get; set; }
        public String  nomepes { get; set; }
        public String Nomeproj { get; set; }
        public Decimal ?seguropf { get; set; }
        public String agenciaproj { get; set; }
        public String bancoproj { get; set; }
        public Decimal ?inssretpf { get; set; }
        public String bcopes { get; set; }
        public String agenpes { get; set; }
        public String digag { get; set; }
        public String tipoctapes { get; set; }
        public String ctapes { get; set; }
        public String digcta { get; set; }
        public String cpfpes { get; set; }
        public String descricao { get; set; }

        //campos adicionados para importação da folha direto para o despesa

        //public Decimal? valorbruto { get; set; }
        //public Int32? ndeppes { get; set; }
        //public Decimal? valorret { get; set; }


    }


    public class CarregarFolhaAntigaConfiguration : EntityTypeConfiguration<CarregarFolhaAntiga>
    {
        public CarregarFolhaAntigaConfiguration()
        {
            ToTable("vPagtosFolha");
        }
    }
}