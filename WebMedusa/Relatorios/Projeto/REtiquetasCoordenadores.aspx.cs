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

namespace Medusa.Relatorios.Projeto
{
    public partial class REtiquetasCoordenadores : BasePage
    {

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                base.Page_Load(sender, e);

                var s = new StatusProjetoBLL();
                rdStatus.DataSource = s.GetAll("nome");
                rdStatus.DataBind();

                rdTipo.DataSource = Enum.GetValues(typeof(TipoCoordenador));
                rdTipo.DataBind();

            }
        }

        protected void GerarRelatorio()
        {
            //var r = new EtiquetaCoordenador();
            //var rpd = new ReportDataSource("dsEtiquetas", r.GetEtiquetas(rdStatus.SelectedValue, rdDefinitivo.SelectedValue, (TipoCoordenador)Enum.Parse(typeof(TipoCoordenador), rdTipo.SelectedValue)));
            //ReportViewer1.LocalReport.DataSources.Clear();
            //ReportViewer1.LocalReport.DataSources.Add(rpd);
            //ReportViewer1.LocalReport.Refresh();
            dRelatorio.Visible = true;
            ReportViewer ReportViewer1 = new ReportViewer();
            ReportViewer1.LocalReport.ReportPath = @"Relatorios\Projeto\REtiquetaCoordenador.rdlc";
            var r = new EtiquetaCoordenador();
            var rpd = new ReportDataSource("dsEtiquetas", r.GetEtiquetas(rdStatus.SelectedValue, rdDefinitivo.SelectedValue, (TipoCoordenador)Enum.Parse(typeof(TipoCoordenador), rdTipo.SelectedValue)));
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
    }
}