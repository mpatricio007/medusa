using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using Medusa.DAL;
using Medusa.LIB;

namespace Medusa.Relatorios.RemessaBancaria
{
    public partial class ReportPagtosComOcorrencia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int? de = Convert.ToInt32(HttpContext.Current.Request.QueryString["pk1"].DesCriptografar());
            int? ate = Convert.ToInt32(HttpContext.Current.Request.QueryString["pk2"].DesCriptografar());
            DateTime? dtProcessado = Util.StringToDate(HttpContext.Current.Request.QueryString["pk3"].DesCriptografar());
            DateTime? dtPagtoDe = Util.StringToDate(HttpContext.Current.Request.QueryString["pk4"].DesCriptografar());
            DateTime? dtPagtoAte = Util.StringToDate(HttpContext.Current.Request.QueryString["pk5"].DesCriptografar());

            string strTitulo = String.Format(" {0}{1}{2}{3}",de != 0 || ate != 0 ? "com lotes de " + de.ToString() + " " : String.Empty,
                ate != 0 ? "até " + ate.ToString() + " ": String.Empty,
                dtProcessado.HasValue ? "processados em " + Util.DateToString(dtProcessado) + " " : String.Empty,
                dtPagtoDe.HasValue & dtPagtoAte.HasValue ? "com pagamento de " + Util.DateToString(dtPagtoDe) + " até " + Util.DateToString(dtPagtoAte) : String.Empty);


            var rPagComOco = new ReportViewer();
            rPagComOco.LocalReport.ReportPath = @"Relatorios\RemessaBancaria\RelatorioPagtosComOcorrencia.rdlc";
            var rp = new RelatorioPagtosComOcorrencia();
            rPagComOco.LocalReport.DataSources.Clear();
            rPagComOco.LocalReport.DataSources.Add(new ReportDataSource("dsPagtosComOcorrencia", rp.GerarRelatorioComOcorrencia(de, ate,dtProcessado, dtPagtoDe,dtPagtoAte)));
            rPagComOco.LocalReport.SetParameters(new ReportParameter("parameterTitulo",strTitulo));
            rPagComOco.LocalReport.Refresh();

            Util.ExportReportToPDF(rPagComOco);
        }
    }
}