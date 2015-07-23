using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using Medusa.BLL;

namespace Medusa.Relatorios.Cobranca
{
    public partial class RelatorioBoletos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dRelatorio.Visible = false;
            }
        }

        protected void btImportar_Click(object sender, EventArgs e)
        {
            GerarRelatorio();
        }


        protected void GerarRelatorio()
        {
            dRelatorio.Visible = true;
            ReportViewer ReportViewer1 = new ReportViewer();
            ReportParameter[] parameters = new ReportParameter[1];
            ReportViewer1.LocalReport.ReportPath = @"Relatorios\Cobranca\RelatorioBoletoR.rdlc";
            parameters[0] = new ReportParameter("DescrFiltroUtilizado", string.Format("data de: {0:d}  a {1:d}", cDataDe.Value.GetValueOrDefault(), cDataAte.Value.GetValueOrDefault()));
            ReportViewer1.LocalReport.SetParameters(parameters);
            var r = new RelatorioBoleto();
            ReportDataSource rpd = new ReportDataSource("dsBoletos", r.GetBoletos(cDataDe.Value.GetValueOrDefault(), cDataAte.Value.GetValueOrDefault(), cDdlEventos1.Id_evento));
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rpd);
            ReportViewer1.LocalReport.Refresh();

            Session[PageOfReport.SessionName] = ReportViewer1;
            dRelatorio.InnerHtml = PageOfReport.iframe;
            dRelatorio.DataBind();
        }

    }
}