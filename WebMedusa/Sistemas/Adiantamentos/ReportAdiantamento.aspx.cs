using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using Medusa.DAL;
using Medusa.Relatorios.Adiantamentos;
using Medusa.LIB;
using Medusa.BLL;

namespace Medusa.Sistemas.Adiantamentos
{
    public partial class ReportAdiantamento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int pkValue = Convert.ToInt32(HttpContext.Current.Request.QueryString["pk"].DesCriptografar());
                if ((int)pkValue != 0)
                {
                    var aBLL = new AdiantamentoBLL();
                    aBLL.Get(pkValue);
                    var rAdiantamento = new ReportViewer();
                    rAdiantamento.LocalReport.ReportPath = @"Relatorios\Adiantamentos\RelatorioAdiantamento.rdlc";
                    var a = new RelatorioAdiantamento();
                    var relHistorico = new RelatorioHistoricoAdiantamento();
                    var rHstEmailAdmto = new RelatorioHistEmailAdmto();
                    ReportDataSource dsa = new ReportDataSource("dsAdiantamento", a.GerarRelatorio(pkValue));
                    ReportDataSource dsaa = new ReportDataSource("dsAdiantamentosAbertos", a.GetAdiantamentosAbertos());
                    ReportDataSource rhist = new ReportDataSource("dsHistorico", relHistorico.GetHistoricoAdiantamento(pkValue));
                    ReportDataSource rhea = new ReportDataSource("dsHistEmailAdmto", rHstEmailAdmto.GetHistoricoEmailAdmto(pkValue));
                    rAdiantamento.LocalReport.DataSources.Clear();
                    rAdiantamento.LocalReport.DataSources.Add(dsa);
                    rAdiantamento.LocalReport.DataSources.Add(dsaa);
                    rAdiantamento.LocalReport.DataSources.Add(rhist);
                    rAdiantamento.LocalReport.DataSources.Add(rhea);
                    rAdiantamento.LocalReport.Refresh();

                    Util.ExportReportToPDF(rAdiantamento);
                }
            }
        }
    }
}