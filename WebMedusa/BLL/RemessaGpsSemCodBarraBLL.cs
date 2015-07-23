using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;
using Medusa.BLL.RemessaBB;

namespace Medusa.BLL
{
    public class RemessaGpsSemCodBarraBLL : RemessaBLL<RemessaGpsSemCodBarra>
    {
        public bool RetornoEhValido(SegmentoN segO)
        {
            bool rt = Exists();
           
            return rt;
        }

        public override bool Agendar(ref string msg)
        {
            ObjEF.id_forma_pagto = FormaPagtoBLL.GUIAS_GPS;
            return base.Agendar(ref msg);
        }
    }
}
