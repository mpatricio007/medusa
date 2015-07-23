using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using Medusa.Relatorios.Adiantamentos;
using Medusa.DAL;
using Medusa.LIB;

namespace Medusa.Sistemas.Adiantamentos
{
    public partial class ReportAdiantamentoViagem : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int pkValue = Convert.ToInt32(HttpContext.Current.Request.QueryString["pk"].DesCriptografar());
                if ((int)pkValue != 0)
                {
                    var rAdiantamentoViagem = new ReportViewer();
                    rAdiantamentoViagem.LocalReport.ReportPath = @"Relatorios\Adiantamentos\RelatorioAdiantamentoViagem.rdlc";
                    var rel = new RelatorioAdiantamentoViagem();
                    var relHistorico = new RelatorioHistoricoAdiantamento();
                    ReportDataSource rav = new ReportDataSource("dsAdiantamentoViagem", rel.GerarRelatorioViagem(pkValue));                    
                    ReportDataSource rhist = new ReportDataSource("dsHistorico", relHistorico.GetHistoricoAdiantamento(pkValue));
                    rAdiantamentoViagem.LocalReport.DataSources.Clear();
                    rAdiantamentoViagem.LocalReport.DataSources.Add(rav);                    
                    rAdiantamentoViagem.LocalReport.DataSources.Add(rhist);
                    rAdiantamentoViagem.LocalReport.Refresh();

                    Util.ExportReportToPDF(rAdiantamentoViagem);
                }
            }
        }
    }
}