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

namespace Medusa.Relatorios.REComodatos
{
    public partial class REtiquetaComodatos : BasePage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //var comBLL = new ComodatoBLL();
                //ddlAno.DataSource = comBLL.GetAnos();
                //ddlAno.DataBind();
                //ReportViewer1.Visible = false;
            }
        }

        protected void btImportar_Click(object sender, EventArgs e)
        {
            Util.NovaJanela(String.Format("ReportComodato.aspx?pk1={0}&pk2={1}", Convert.ToString(cInteiroDe.Value.GetValueOrDefault()).Criptografar(),
                Convert.ToString(cInteiroAte.Value.GetValueOrDefault()).Criptografar()));
        }


        //protected void GetEtiquetas()
        //{
        //    ReportViewer1.Visible = true;
        //    var c = new EtiquetaComodato();
        //    ReportDataSource rpd = new ReportDataSource("dsComodato", c.GetEtiquetas(cInteiroDe.Value.GetValueOrDefault(),
        //        cInteiroAte.Value.GetValueOrDefault(),
        //        Convert.ToInt32(this.ddlAno.SelectedValue)));
        //    ReportViewer1.LocalReport.DataSources.Clear();
        //    ReportViewer1.LocalReport.DataSources.Add(rpd);
        //    ReportViewer1.LocalReport.Refresh();
        //}

    }
}
