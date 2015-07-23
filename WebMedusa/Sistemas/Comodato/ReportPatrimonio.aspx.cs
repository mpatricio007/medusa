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
    public partial class ReportPatrimonio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                #region pa
                //int de = Convert.ToInt32(HttpContext.Current.Request.QueryString["pk1"].DesCriptografar());
                //int ate = Convert.ToInt32(HttpContext.Current.Request.QueryString["pk2"].DesCriptografar());
                //var rvPatrimonio = new ReportViewer();
                //rvPatrimonio.LocalReport.ReportPath = @"Relatorios\REComodatos\RPatrimonio.rdlc";
                //var r = new RelatorioPatrimonio();
                //ReportDataSource rpd = new ReportDataSource("dsPatrimonio", r.GerarRelatorio(de, ate));
                //rvPatrimonio.LocalReport.DataSources.Clear();
                //rvPatrimonio.LocalReport.DataSources.Add(rpd);
                //rvPatrimonio.LocalReport.Refresh();

                //Util.ExportReportToPDF(rvPatrimonio);
                #endregion 
                int pkValue = Convert.ToInt32(HttpContext.Current.Request.QueryString["pk"].DesCriptografar());
                if ((int)pkValue != 0)
                {
                    var rvPatrimonio = new ReportViewer();
                    rvPatrimonio.LocalReport.ReportPath = @"Relatorios\REComodatos\RPatrimonio.rdlc";
                    var p = new RelatorioPatrimonio();
                    ReportDataSource rpd = new ReportDataSource("dsPatrimonio", p.GerarRelatorio(pkValue));
                    rvPatrimonio.LocalReport.DataSources.Clear();
                    rvPatrimonio.LocalReport.DataSources.Add(rpd);
                    rvPatrimonio.LocalReport.Refresh();

                    Util.ExportReportToPDF(rvPatrimonio);
                }
            }
        }
    }
}