using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medusa.BLL
{
    [Serializable]
    public class ListaTiposLcto
    {
        public int id_tipo_lcto { get; set; }
        public string descricao { get; set; }
        public string DC { get; set; }

        public ListaTiposLcto(int intId, string txtDescricao, string txtDC)
        {
            id_tipo_lcto = intId;
            descricao = txtDescricao;
            DC = txtDC;
        }
    }
    
}