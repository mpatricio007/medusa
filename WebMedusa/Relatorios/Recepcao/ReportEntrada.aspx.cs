using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using Medusa.DAL;
using Medusa.BLL;
using Medusa.LIB;

namespace Medusa.Relatorios.Recepcao
{
    public partial class ReportEntrada : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int de = Convert.ToInt32(HttpContext.Current.Request.QueryString["pk1"].DesCriptografar());
                int ate = Convert.ToInt32(HttpContext.Current.Request.QueryString["pk2"].DesCriptografar());
                int ano = Convert.ToInt32(HttpContext.Current.Request.QueryString["pk3"].DesCriptografar());

                var rvEntrada = new ReportViewer();
                rvEntrada.LocalReport.ReportPath = @"Relatorios\Recepcao\RelatorioEntrada.rdlc";
                var re = new RelatorioEntrada();
                ReportDataSource rpd = new ReportDataSource("dsEntradas", re.GerarRelatorio(de, ate, ano));
                rvEntrada.LocalReport.DataSources.Clear();
                rvEntrada.LocalReport.DataSources.Add(rpd);
                rvEntrada.LocalReport.Refresh();

                Util.ExportReportToPDF(rvEntrada);
            }
        }
    }
}


