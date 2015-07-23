using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Medusa.DAL
{
    public class SaldoConta
    {   
        public int id_conta { get; set; }
        public string numero { get; set; }
        public string descricao { get; set; }
        public string digito { get; set; }
        public int? cod_def_projeto { get; set; }
        public string agencia { get; set; }
        public string banco { get; set; }
        public decimal? creditos { get; set; }
        public decimal? debitos { get; set; }
        public decimal? saldo_final { get; set; }
        public decimal? saldo_anterior { get; set; }
    }
}