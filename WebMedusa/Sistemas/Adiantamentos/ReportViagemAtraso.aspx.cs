using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using Medusa.BLL;
using Medusa.Relatorios.Adiantamentos;
using Medusa.LIB;

namespace Medusa.Sistemas.Adiantamentos
{
    public partial class ReportViagemAtraso : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btGerar_Click(sender, e);
            }
        }

        protected void btGerar_Click(object sender, EventArgs e)
        {
            RelatorioAdiantamentoViagem r = new RelatorioAdiantamentoViagem();
            ReportDataSource rpd = new ReportDataSource("dsDiariasAtraso", r.GetAdiantamentosEmAtraso(cDdlProjeto.Id_projeto,
                DdlStatusAdiantamentos1.Id_status_admto,
                cData1.Value));
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportParameter rp = new ReportParameter("titulo", r.GetTitulo(cDdlProjeto.Id_projeto,
                DdlStatusAdiantamentos1.Id_status_admto,
                cData1.Value));
            ReportViewer1.LocalReport.SetParameters(rp);
            ReportViewer1.LocalReport.DataSources.Add(rpd);
            ReportViewer1.LocalReport.Refresh();
            ReportViewer1.Visible = true;
        } 
    }
}