using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using Medusa.LIB;
using Medusa.DAL;

namespace Medusa.Relatorios.Recepcao
{
    public partial class ReportSaida : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int de = Convert.ToInt32(HttpContext.Current.Request.QueryString["pk1"].DesCriptografar());
                int ate = Convert.ToInt32(HttpContext.Current.Request.QueryString["pk2"].DesCriptografar());
                int ano = Convert.ToInt32(HttpContext.Current.Request.QueryString["pk3"].DesCriptografar());

                var rvSaida = new ReportViewer();
                rvSaida.LocalReport.ReportPath = @"Relatorios\Recepcao\RelatorioEntradaSaida.rdlc";
                var re = new RelatorioEntrada();
                ReportDataSource rpd = new ReportDataSource("dsEntradas", re.GerarRelatorio(de, ate, ano));
                rvSaida.LocalReport.DataSources.Clear();
                rvSaida.LocalReport.DataSources.Add(rpd);
                rvSaida.LocalReport.Refresh();

                Util.ExportReportToPDF(rvSaida);
            }
        }
    }
}