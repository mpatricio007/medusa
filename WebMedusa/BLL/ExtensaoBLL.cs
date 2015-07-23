using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;

namespace Medusa.BLL
{
    public class ExtensaoBLL : Abstract_Crud<Extensao>
    {
        public static Dictionary<string, int> openWith = new ExtensaoBLL().GetAll().OfType<Extensao>().ToDictionary(it => it.extensao, k => k.id_extensao);  
    }
}
