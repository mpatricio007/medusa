using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.Relatorios.PessoaJuridica;
using Microsoft.Reporting.WebForms;
using Medusa.DAL;
using Medusa.LIB;

namespace Medusa.Sistemas.PessoaJuridica
{
    public partial class ReportCRC : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int pkValue = Convert.ToInt32(HttpContext.Current.Request.QueryString["pk"].DesCriptografar());
                if ((int)pkValue != 0)
                {
                    var rvCRC = new ReportViewer();
                    rvCRC.LocalReport.ReportPath = @"Relatorios\PessoaJuridica\RelatorioCRC.rdlc";
                    var r = new RelatorioFornecedor();
                    ReportDataSource rpd = new ReportDataSource("dsRelatorioFornecedor", r.GerarRelatorio(pkValue));
                    ReportDataSource rpd1 = new ReportDataSource("dsDocumentos", r.GetDocumentos(pkValue));
                    ReportDataSource rpd2 = new ReportDataSource("dsSocios", r.GetSocios(pkValue));
                    ReportDataSource rpd3 = new ReportDataSource("dsRepresentante", r.GetRepresentanteLegal(pkValue));
                    rvCRC.LocalReport.DataSources.Clear();
                    rvCRC.LocalReport.DataSources.Add(rpd);
                    rvCRC.LocalReport.DataSources.Add(rpd1);
                    rvCRC.LocalReport.DataSources.Add(rpd2);
                    rvCRC.LocalReport.DataSources.Add(rpd3);
                    rvCRC.LocalReport.Refresh();

                    Util.ExportReportToPDF(rvCRC);

                    //Warning[] warnings;
                    //string[] streamids;
                    //string mimeType;
                    //string encoding;
                    //string extension;
                    //byte[] bytes = rvCRC.LocalReport.Render(
                    //   "PDF", null, out mimeType, out encoding,
                    //    out extension,
                    //   out streamids, out warnings);
                    //Response.Clear();
                    //Response.ContentType = "Application/pdf";
                    //Response.BinaryWrite(bytes);
                    //Response.End();
                }
            }
        }
    }
}