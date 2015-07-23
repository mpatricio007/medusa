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

namespace Medusa.Relatorios.REComodatos
{
    public partial class ReportComodato : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                int de = Convert.ToInt32(HttpContext.Current.Request.QueryString["pk1"].DesCriptografar());
                int ate = Convert.ToInt32(HttpContext.Current.Request.QueryString["pk2"].DesCriptografar());
                var rvComodato = new ReportViewer();
                rvComodato.LocalReport.ReportPath = @"Relatorios\REComodatos\REtiquetaComodato.rdlc";
                var r = new EtiquetaComodato();
                ReportDataSource rpd = new ReportDataSource("dsComodato", r.GetEtiquetas(de,ate));
                rvComodato.LocalReport.DataSources.Clear();
                rvComodato.LocalReport.DataSources.Add(rpd);
                rvComodato.LocalReport.Refresh();

                Util.ExportReportToPDF(rvComodato);
                
            }
        }
    }
}