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
    public partial class ReportFusp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            {

                int pkValue = Convert.ToInt32(HttpContext.Current.Request.QueryString["pk"].DesCriptografar());

                var rvReciboFusp = new ReportViewer();
                rvReciboFusp.LocalReport.ReportPath = @"Relatorios\Recibos\RelatorioReciboFusp.rdlc";
                var rf = new RelatorioReciboFusp();
                ReportDataSource rpd1 = new ReportDataSource("dsReciboFusp", rf.GetRelatorio(pkValue));
                rvReciboFusp.LocalReport.DataSources.Clear();
                rvReciboFusp.LocalReport.DataSources.Add(rpd1);
                rvReciboFusp.LocalReport.Refresh();

                Util.ExportReportToPDF(rvReciboFusp);

            }
        }
    }
}