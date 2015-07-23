using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;
using System.IO;
using Medusa.BLL.RemessaBB;

namespace Medusa.BLL
{
    public class RemessaGruBLL : RemessaBLL<RemessaGru>
    {
        public bool RetornoEhValido(SegmentoO segO)
        {
            bool rt = Exists();
            if (rt)
            {
                rt = true;  //segO.cod_barras == ObjEF.Guia.RepresentacaoNumericaSemDig();
            }
            return rt;
        }

        public override bool Agendar(ref string msg)
        {
            ObjEF.id_forma_pagto = FormaPagtoBLL.GUIAS_GRU;
            return base.Agendar(ref msg);
        }
    }
}
