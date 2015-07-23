using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class vResumoAutorizacaoPagtos
    {
        public int id_lote { get; set; }

        public decimal valor_credito_cc { get; set; }

        public decimal valor_doc { get; set; }

        public decimal valor_ted { get; set; }

        public decimal valor_credito_cp { get; set; }

        public decimal valor_tit_BB { get; set; }

        public decimal valor_tit_outros { get; set; }

        public decimal valor_imp_cons { get; set; }

        public decimal valor_gps { get; set; }

        public decimal valor_gru { get; set; }

        public decimal total { get; set; }        

        public string decricao_rejeitado { get; set; }

        public decimal rejeitados { get; set; }        
    }    
}