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
    public partial class RelatorioDepIdentificadoF : BasePage
    {
      
        protected void GerarRelatorio()
        {
            ReportViewer ReportViewer1 = new ReportViewer();
            ReportViewer1.LocalReport.ReportPath = @"Relatorios\Conciliacao\RelatorioDepIdentificadoR.rdlc";
            ReportParameter[] parameters = new ReportParameter[1];
            parameters[0] = new ReportParameter("DescrFiltroUtilizado", string.Format("data de: {0:d}  a {1:d}", cData1.Value.GetValueOrDefault(), cData2.Value.GetValueOrDefault()));
            ReportViewer1.LocalReport.SetParameters(parameters);
            var r = new RelatorioDepIdentificado();
            ReportDataSource rpd = new ReportDataSource("DS_DepIdentif", r.GerarRelatorio(cData1.Value.GetValueOrDefault(), cData2.Value.GetValueOrDefault()));
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
            if (!IsPostBack)
            {                
                cData1.Value = DateTime.Now;
                cData2.Value = DateTime.Now;
                GerarRelatorio();
            }
        }

       
    }
}