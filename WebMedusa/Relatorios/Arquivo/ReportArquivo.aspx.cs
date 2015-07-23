using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using Medusa.LIB;
using Medusa.DAL;

namespace Medusa.Relatorios.Arquivo
{
    public partial class ReportArquivo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int de = Convert.ToInt32(HttpContext.Current.Request.QueryString["pk1"].DesCriptografar());
                int ate = Convert.ToInt32(HttpContext.Current.Request.QueryString["pk2"].DesCriptografar());
                var rvArquivo = new ReportViewer();
                rvArquivo.LocalReport.ReportPath = @"Relatorios\Arquivo\REtiquetaArq.rdlc";
                var a = new EtiquetaArquivo();
                ReportDataSource rpd = new ReportDataSource("dsArquivo", a.GetEtiquetas(de, ate));
                rvArquivo.LocalReport.DataSources.Clear();
                rvArquivo.LocalReport.DataSources.Add(rpd);
                rvArquivo.LocalReport.Refresh();

                Util.ExportReportToPDF(rvArquivo);
            }
        }
    }
}