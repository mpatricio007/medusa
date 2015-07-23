using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;
using Medusa.BLL.RemessaBB;

namespace Medusa.BLL
{
    public class RemessaConsBLL : RemessaBLL<RemessaCons>
    {
        public bool RetornoEhValido(SegmentoO segO)
        {
            bool rt = Exists();
            if (rt)
            {
                rt = segO.cod_barras == ObjEF.Guia.RepresentacaoNumericaSemDig();
            }
            return rt;
        }

        public override bool Agendar(ref string msg)
        {
            ObjEF.id_forma_pagto = FormaPagtoBLL.GUIAS_CONTAS_CONSUMO;
            return base.Agendar(ref msg);
        }
    }
}
