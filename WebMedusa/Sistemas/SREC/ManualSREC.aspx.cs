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
using System.IO;

namespace Medusa.Sistemas.SREC
{
    public partial class ManualSREC : BasePage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            dReport.InnerHtml = String.Format("<iframe src=\"{0}\" width=\"100%\" height=\"1000px\"></iframe>", @"Manual do Usuário [Somente leitura].pdf");
        }
    }
}