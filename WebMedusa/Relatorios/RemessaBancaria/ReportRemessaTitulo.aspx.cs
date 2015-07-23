using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using Medusa.LIB;
using Medusa.BLL;
using Medusa.DAL;

namespace Medusa.Relatorios.RemessaBancaria
{
    public partial class ReportRemessaTitulo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int? de = Util.StringToInteiro(HttpContext.Current.Request.QueryString["pk1"].DesCriptografar());
                int? ate = Util.StringToInteiro(HttpContext.Current.Request.QueryString["pk2"].DesCriptografar());
                DateTime? dtPagto = Util.StringToDate(HttpContext.Current.Request.QueryString["pk3"].DesCriptografar());
                var rRemessaTitulo = new ReportViewer();
                rRemessaTitulo.LocalReport.ReportPath = @"Relatorios\RemessaBancaria\RelatorioRemessaTitulo.rdlc";
                var rt = new RelatorioRemessaTitulo();
                ReportDataSource rrt = new ReportDataSource("dsRemessaTit", rt.GerarRelatorioAgendados(de, ate, dtPagto));
                rRemessaTitulo.LocalReport.DataSources.Clear();
                rRemessaTitulo.LocalReport.DataSources.Add(rrt);
                rRemessaTitulo.LocalReport.Refresh();

                Util.ExportReportToPDF(rRemessaTitulo);
            }
        }
    }
}