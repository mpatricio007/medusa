using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.DAL;
using Microsoft.Reporting.WebForms;
using Medusa.Relatorios.Recibos;
using Medusa.LIB;

namespace Medusa.Sistemas.Recibos
{
    public partial class Report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            int pkValue = Convert.ToInt32(HttpContext.Current.Request.QueryString["pk"].DesCriptografar());
            
            var rvRecibo = new ReportViewer();
            rvRecibo.LocalReport.ReportPath = @"Relatorios\Recibos\RelatorioRecibo.rdlc";
            var r = new RelatorioRecibo();
            ReportDataSource rpd1 = new ReportDataSource("dsRecibos", r.GetRelatorio(pkValue));
            rvRecibo.LocalReport.DataSources.Clear();
            rvRecibo.LocalReport.DataSources.Add(rpd1);
            rvRecibo.LocalReport.Refresh();

            Util.ExportReportToPDF(rvRecibo);

        }
    }
}