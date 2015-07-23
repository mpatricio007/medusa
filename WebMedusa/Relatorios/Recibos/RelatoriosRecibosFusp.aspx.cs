using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms.Internal.Soap.ReportingServices2005.Execution;
using Microsoft.Reporting.WebForms;
using Medusa.BLL;
using Medusa.LIB;
using Medusa.DAL;

namespace Medusa.Relatorios.Recibos
{
    public partial class RelatoriosRecibosFusp : BasePage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                var recBLL = new ReciboFuspBLL();
            }
        }

        protected void btGerar_Click(object sender, EventArgs e)
        {
            Util.NovaJanela(String.Format("ReportReciboFusp.aspx?pk1={0}&pk2={1}", Convert.ToString(cInteiroDe.Value.GetValueOrDefault()).Criptografar(),
                Convert.ToString(cInteiroAte.Value.GetValueOrDefault()).Criptografar()));
        }
    }
}