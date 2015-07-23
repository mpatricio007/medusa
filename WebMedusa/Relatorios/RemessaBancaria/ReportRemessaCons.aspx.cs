using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.DAL;
using Microsoft.Reporting.WebForms;
using Medusa.LIB;

namespace Medusa.Relatorios.RemessaBancaria
{
    public partial class ReportRemessaCons : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int? de = Util.StringToInteiro(HttpContext.Current.Request.QueryString["pk1"].DesCriptografar());
                int? ate = Util.StringToInteiro(HttpContext.Current.Request.QueryString["pk2"].DesCriptografar());
                DateTime? dtPagto = Util.StringToDate(HttpContext.Current.Request.QueryString["pk3"].DesCriptografar());

                var rRemessaCons = new ReportViewer();
                rRemessaCons.LocalReport.ReportPath = @"Relatorios\RemessaBancaria\RelatorioRemessaCons.rdlc";
                var rc = new RelatorioRemessaCons();
                ReportDataSource rrc = new ReportDataSource("dsRemessaCons", rc.GerarRelatorioAgendados(de, ate, dtPagto));
                rRemessaCons.LocalReport.DataSources.Clear();
                rRemessaCons.LocalReport.DataSources.Add(rrc);
                rRemessaCons.LocalReport.Refresh();

                Util.ExportReportToPDF(rRemessaCons);

            }
        }
    }
}
