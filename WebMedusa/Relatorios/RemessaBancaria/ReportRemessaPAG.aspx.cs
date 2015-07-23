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
    public partial class ReportRemessaPAG : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int? de = Util.StringToInteiro(HttpContext.Current.Request.QueryString["pk1"].DesCriptografar());
                int? ate = Util.StringToInteiro(HttpContext.Current.Request.QueryString["pk2"].DesCriptografar());
                DateTime? dtPagto = Util.StringToDate(HttpContext.Current.Request.QueryString["pk3"].DesCriptografar());
                string descricao = HttpContext.Current.Request.QueryString["pk4"].DesCriptografar();

                var rRemessaPag = new ReportViewer();
                rRemessaPag.LocalReport.ReportPath = @"Relatorios\RemessaBancaria\RelatorioRemessaPAG.rdlc";
                var rp = new RelatorioRemessaPAG();
                var rrp = new ReportDataSource("dsRemessaPAG", rp.GerarRelatorioAgendados(de, ate, dtPagto, descricao));
                rRemessaPag.LocalReport.DataSources.Clear();
                rRemessaPag.LocalReport.DataSources.Add(rrp);
                rRemessaPag.LocalReport.Refresh();

                Util.ExportReportToPDF(rRemessaPag);
            }
        }
    }
}