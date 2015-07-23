using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class CarregarFolhaBolsaNova
    {
        public int id { get; set; }
        public int projeto { get; set; }
        public string nome_bolsista { get; set; }
        public string cpf_bolsista { get; set; }
        public string bolsa { get; set; }
        public string banco { get; set; }
        public string agencia { get; set; }
        public string digagencia { get; set; }
        public string conta { get; set; }
        public string digcta { get; set; }
        public decimal valor { get; set; }
    }
}