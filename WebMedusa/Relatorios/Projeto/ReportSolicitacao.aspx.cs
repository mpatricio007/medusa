using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using Medusa.DAL;

namespace Medusa.Relatorios.Projeto
{
    public partial class ReportSolicitacao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int pkValue = Convert.ToInt32(HttpContext.Current.Request.QueryString["pk"].DesCriptografar());
                if ((int)pkValue != 0)
                {
                    var rvSolicitacao = new ReportViewer();
                    rvSolicitacao.LocalReport.ReportPath = @"Relatorios\Projeto\RSolicitacao.rdlc";
                    var r = new RelatorioAbertura();
                    ReportDataSource rpd = new ReportDataSource("dsSolicitacao", r.GerarRelatorio(pkValue));
                    rvSolicitacao.LocalReport.DataSources.Clear();
                    rvSolicitacao.LocalReport.DataSources.Add(rpd);
                    rvSolicitacao.LocalReport.Refresh();

                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string extension;
                    byte[] bytes = rvSolicitacao.LocalReport.Render(
                       "PDF", null, out mimeType, out encoding,
                        out extension,
                       out streamids, out warnings);
                    Response.Clear();
                    Response.ContentType = "Application/pdf";
                    Response.BinaryWrite(bytes);
                    Response.End();
                }
            }
        }
    }
}