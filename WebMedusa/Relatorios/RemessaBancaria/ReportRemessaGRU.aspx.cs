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
    public partial class ReportRemessaGRU : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int? de = Util.StringToInteiro(HttpContext.Current.Request.QueryString["pk1"].DesCriptografar());
                int? ate = Util.StringToInteiro(HttpContext.Current.Request.QueryString["pk2"].DesCriptografar());
                DateTime? dtPagto = Util.StringToDate(HttpContext.Current.Request.QueryString["pk3"].DesCriptografar());

                var rRemessaGRU = new ReportViewer();
                rRemessaGRU.LocalReport.ReportPath = @"Relatorios\RemessaBancaria\RelatorioRemessaGRU.rdlc";
                var rrgru = new RelatorioRemessaGRU();
                ReportDataSource rgru = new ReportDataSource("dsRemessaGRU", rrgru.GerarRelatorio(de, ate, dtPagto));
                rRemessaGRU.LocalReport.DataSources.Clear();
                rRemessaGRU.LocalReport.DataSources.Add(rgru);
                rRemessaGRU.LocalReport.Refresh();

                Util.ExportReportToPDF(rRemessaGRU);
            }
        }
    }
}