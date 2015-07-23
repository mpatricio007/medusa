using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.DAL;
using Medusa.BLL;
using Medusa.LIB;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Reporting.WebForms;

namespace Medusa.Relatorios.Conciliacao
{
    public partial class RelSaldoContasTipo : BasePage
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            GerarRelatorio();
        }

        protected void GerarRelatorio()
        {
            ReportViewer ReportViewer1 = new ReportViewer();
            ReportViewer1.LocalReport.ReportPath = @"Relatorios\Conciliacao\RelSaldoTipoConta.rdlc";
            ReportParameter[] parameters = new ReportParameter[1];
            DateTime dt = DateTime.Now;
            if (cData1.Value.HasValue) dt = cData1.Value.GetValueOrDefault();
            parameters[0] = new ReportParameter("DescrFiltroUtilizado", string.Format("data de referência: {0:d}", dt));
            ReportViewer1.LocalReport.SetParameters(parameters);
            var r = new RelatorioSaldoContasTipo();
            ReportDataSource rpd = new ReportDataSource("DS_SaldoContas", r.GerarRelatorio(dt));
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rpd);
            ReportViewer1.LocalReport.Refresh();

            Session[PageOfReport.SessionName] = ReportViewer1;
            dRelatorio.InnerHtml = PageOfReport.iframe;
            dRelatorio.DataBind();
        }
        protected void btImportar_Click(object sender, EventArgs e)
        {
            GerarRelatorio();
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) cData1.Value = DateTime.Now;
        }

    

    

        
    }
}