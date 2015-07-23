using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms.Internal.Soap.ReportingServices2005.Execution;
using Microsoft.Reporting.WebForms;
using Medusa.BLL;
using Medusa.LIB;
using Medusa.DAL;

namespace Medusa.Relatorios.Recepcao
{
    public partial class RelatorioEntradas : BasePage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var entBLL = new EntradaBLL();
                ddlAno.DataSource = entBLL.GetAnos();
                ddlAno.DataBind();                
                //ReportViewer1.Visible = false;
            }
        }

        protected void btImportar_Click(object sender, EventArgs e)
        {
            Util.NovaJanela(String.Format("ReportEntrada.aspx?pk1={0}&pk2={1}&pk3={2}", Convert.ToString(cInteiroDe.Value.GetValueOrDefault()).Criptografar(),
                Convert.ToString(cInteiroAte.Value.GetValueOrDefault()).Criptografar(), ddlAno.SelectedValue));
            //GerarRelatorio();
        }

        //protected void GerarRelatorio()
        //{
        //    ReportViewer1.Visible = true;
        //    var r = new RelatorioEntrada();
        //    ReportDataSource rpd = new ReportDataSource("dsEntradas", r.GerarRelatorio(cInteiroDe.Value.GetValueOrDefault(), 
        //        cInteiroAte.Value.GetValueOrDefault(), 
        //        Convert.ToInt32(this.ddlAno.SelectedValue)));
        //    ReportViewer1.LocalReport.DataSources.Clear();
        //    ReportViewer1.LocalReport.DataSources.Add(rpd);
        //    ReportViewer1.LocalReport.Refresh();  
        //}

    }
}