using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.LIB;
using Microsoft.Reporting.WebForms;
using Medusa.BLL;
using Medusa.DAL;

namespace Medusa.Relatorios.REComodatos
{
    public partial class REtiquetasComodatos : BasePage
    {
        protected void GerarRelatorio()
        {
            #region
            //var rvComodato = new ReportViewer();
            //rvComodato.LocalReport.ReportPath = @"Relatorios\REComodatos\REtiquetaComodato.rdlc";
            //var r = new EtiquetaComodato();
            //ReportDataSource rpd = new ReportDataSource("dsComodato", r.GetEtiquetas(cInteiroDe.Value.GetValueOrDefault(), cInteiroAte.Value.GetValueOrDefault()));
            //rvComodato.LocalReport.DataSources.Clear();
            //rvComodato.LocalReport.DataSources.Add(rpd);
            //rvComodato.LocalReport.Refresh();
            


            //    ReportViewer1.Visible = true;
                //    var r = new EtiquetaComodato();
                //    ReportDataSource rpd = new ReportDataSource("dsComodato", r.GetEtiquetas(cInteiroDe.Value.GetValueOrDefault(), 
                //    cInteiroAte.Value.GetValueOrDefault()));
                //    ReportViewer1.LocalReport.DataSources.Clear();  
                //    ReportViewer1.LocalReport.DataSources.Add(rpd);
            //    ReportViewer1.LocalReport.Refresh();
            #endregion
        }

        protected void btImportar_Click(object sender, EventArgs e)
        {
            Util.NovaJanela(String.Format("ReportComodato.aspx?pk1={0}&pk2={1}", Convert.ToString(cInteiroDe.Value.GetValueOrDefault()).Criptografar(),
                Convert.ToString(cInteiroAte.Value.GetValueOrDefault()).Criptografar()));
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
            }
        }
    }
}