using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Web.UI;
using System.Web;

namespace Medusa.BLL
{
    public class VolumeBLL : AbstractCrudWithLog<Volume>
    {
        public int[] GetVolumesDeAte(int de, int ate)
        {
            return Find(it => it.id_volume >= de & it.id_volume <= ate).Select(it => it.id_volume).ToArray();
        }
    }
}
