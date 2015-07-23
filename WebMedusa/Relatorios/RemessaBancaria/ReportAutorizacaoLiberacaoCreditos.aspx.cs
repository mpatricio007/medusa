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
    public partial class ReportAutorizacaoLiberacaoCreditos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DateTime dt = Convert.ToDateTime(HttpContext.Current.Request.QueryString["pk1"].DesCriptografar());
                var rAutoLibCreditos = new ReportViewer();
                rAutoLibCreditos.LocalReport.ReportPath = @"Relatorios\RemessaBancaria\RelatorioAutorizacaoLiberacaoCreditos.rdlc";
               
                var ralb = new RelatorioAutorizacaoLiberacaoCreditos();
                var rdc = new ReportDataSource("dsResumo", ralb.GerarRelatorio(dt));
                rAutoLibCreditos.LocalReport.SetParameters(new ReportParameter("Titulo", Util.DateToString(dt)));
                rAutoLibCreditos.LocalReport.DataSources.Clear();
                rAutoLibCreditos.LocalReport.DataSources.Add(rdc);
                rAutoLibCreditos.LocalReport.Refresh();
                Util.ExportReportToPDF(rAutoLibCreditos);
            }
        }
        
    }
}