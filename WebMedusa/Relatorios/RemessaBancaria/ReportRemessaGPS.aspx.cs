using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using Medusa.DAL;
using Medusa.LIB;

namespace Medusa.Relatorios.RemessaBancaria
{
    public partial class ReportRemessaGPS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int? de = Util.StringToInteiro(HttpContext.Current.Request.QueryString["pk1"].DesCriptografar());
                int? ate = Util.StringToInteiro(HttpContext.Current.Request.QueryString["pk2"].DesCriptografar());
                DateTime? dtPagto = Util.StringToDate(HttpContext.Current.Request.QueryString["pk3"].DesCriptografar());

                var rRemessaGPS = new ReportViewer();
                rRemessaGPS.LocalReport.ReportPath = @"Relatorios\RemessaBancaria\RelatorioRemessaLoteGpsSemBarras.rdlc";
                var rrgps = new RelatorioRemessaLoteGpsSemBarras();
                ReportDataSource rgps = new ReportDataSource("dsRemessaGPS", rrgps.GerarRelatorio(de, ate, dtPagto));
                rRemessaGPS.LocalReport.DataSources.Clear();
                rRemessaGPS.LocalReport.DataSources.Add(rgps);
                rRemessaGPS.LocalReport.Refresh();

                Util.ExportReportToPDF(rRemessaGPS);
            }
        }
    }
}