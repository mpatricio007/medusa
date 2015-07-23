using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using Medusa.BLL;
using Medusa.LIB;

namespace Medusa.Relatorios.Almoxarifado
{
    public partial class RelatorioRequisicaoQuantidade : BasePage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //ReportViewer1.Visible = false;
            }
        }

        protected void btImportar_Click(object sender, EventArgs e)
        {
            GerarRelatorio();
        }

        protected void GerarRelatorio()
        {
            ReportViewer ReportViewer1 = new ReportViewer();

            //ReportViewer1.Visible = true;
            ReportViewer1.LocalReport.ReportPath = @"Relatorios\Almoxarifado\RelatorioRequisicaoQuantidade.rdlc";
            ReportParameter[] parameters = new ReportParameter[1];
            parameters[0] = new ReportParameter("DescrFiltroUtilizado", string.Format("data de: {0:d}  a {1:d}", cDataDe.Value.GetValueOrDefault(), cDataAte.Value.GetValueOrDefault()));
            ReportViewer1.LocalReport.SetParameters(parameters);

            //ReportViewer1.Visible = true;
            var re = new RelatorioRequisicaoQtde();
            ReportDataSource rpd = new ReportDataSource("dsRequisicaoQtde", re.GerarRelatorio(cDataDe.Value.GetValueOrDefault(), cDataAte.Value.GetValueOrDefault()));
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rpd);
            ReportViewer1.LocalReport.Refresh();

            Session[PageOfReport.SessionName] = ReportViewer1;
            dRelatorio.InnerHtml = PageOfReport.iframe;
            dRelatorio.DataBind();
        }
    }
}