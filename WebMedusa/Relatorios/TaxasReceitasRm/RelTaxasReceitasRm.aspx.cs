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

namespace Medusa.Relatorios.TaxasReceitasRm
{
    public partial class RelTaxasReceitasRm : BasePage
    {

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                base.Page_Load(sender, e);

                cDataDe.Value = DateTime.Now.AddMonths(-1);
                cDataAte.Value = DateTime.Now;
                cInteiroDe.Value = 1;
                cInteiroAte.Value = 999999;
                GerarRelatorio();
            }
        }

        protected void GerarRelatorio()
        {
            ReportParameter[] parameters = new ReportParameter[1];
            parameters[0] = new ReportParameter("DescrFiltroUtilizado", string.Format("data de: {0:d}  a {1:d}", cDataDe.Value.GetValueOrDefault(), cDataAte.Value.GetValueOrDefault()));
            ReportViewer1.LocalReport.SetParameters(parameters);

            var r = new RTaxasReceitasRm();
            ReportDataSource rpd = new ReportDataSource("dsTaxasReceitasRM", 
                r.GetAllByData(cDataDe.Value.GetValueOrDefault(), 
                cDataAte.Value.GetValueOrDefault(),
                cInteiroDe.Value.GetValueOrDefault(),
                cInteiroAte.Value.GetValueOrDefault()
                ));
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rpd);
            ReportViewer1.LocalReport.Refresh();  
        }

        protected void btGerar_Click(object sender, EventArgs e)
        {
            GerarRelatorio();
        }

  
    }
}