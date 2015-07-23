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
    public partial class RProjeto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int pkValue = Convert.ToInt32(HttpContext.Current.Request.QueryString["pk"].DesCriptografar());
                if ((int)pkValue != 0)
                {
                    var rep = new ReportViewer();
                    rep.LocalReport.ReportPath = @"Relatorios\Projeto\RProjeto.rdlc";
                    var r = new RelatorioProjeto();
                    rep.LocalReport.DataSources.Clear();
                    var id_projeto = Convert.ToInt32(pkValue);
                    rep.LocalReport.DataSources.Add(new ReportDataSource("dsProjeto", r.GerarRelatorio(id_projeto)));
                    rep.LocalReport.DataSources.Add(new ReportDataSource("dsCoordenador", r.GetCoordenadores(id_projeto)));
                    rep.LocalReport.DataSources.Add(new ReportDataSource("dsFinanciadores", r.GetFinanciadores(id_projeto)));
                    rep.LocalReport.DataSources.Add(new ReportDataSource("dsEndereco", r.GetEnderecos(id_projeto)));
                    rep.LocalReport.DataSources.Add(new ReportDataSource("dsDocumento", r.GetDoctos(id_projeto)));
                    rep.LocalReport.DataSources.Add(new ReportDataSource("dsTaxas", r.GetTaxas(id_projeto)));
                    rep.LocalReport.Refresh();



                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string extension;
                    byte[] bytes = rep.LocalReport.Render(
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