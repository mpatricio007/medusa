using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using Medusa.DAL;
using Medusa.LIB;
using Medusa.BLL;

namespace Medusa.Relatorios.Recibos
{
    public partial class ReportRecibos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int de = Convert.ToInt32(HttpContext.Current.Request.QueryString["pk1"].DesCriptografar());
                int ate = Convert.ToInt32(HttpContext.Current.Request.QueryString["pk2"].DesCriptografar());

                var rvRecibo = new ReportViewer();
                rvRecibo.LocalReport.ReportPath = @"Relatorios\Recibos\RelatorioRecibo.rdlc";
                var re = new RelatorioRecibo();
                ReportDataSource rpd = new ReportDataSource("dsRecibos", re.GerarRelatorio(de, ate));
                rvRecibo.LocalReport.DataSources.Clear();
                rvRecibo.LocalReport.DataSources.Add(rpd);
                rvRecibo.LocalReport.Refresh();

                Util.ExportReportToPDF(rvRecibo);
            }
        }
    }
}