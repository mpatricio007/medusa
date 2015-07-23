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

namespace Medusa.Relatorios.RemessaBancaria
{
    public partial class RResumo : BasePage
    {

        protected void btGerarRelatorio_Click(object sender, EventArgs e)
        {
            dRelatorio.InnerHtml = String.Format("<iframe src=\"../../Relatorios/RemessaBancaria/ReportAutorizacaoLiberacaoCreditos.aspx?pk1={0}\" width=\"100%\" height=\"1000px\"></iframe>", Convert.ToString(cData1.Value).Criptografar());
            dRelatorio.DataBind();
            Util.ChamarScript("hideProgressDialog();", "hide");
        }
    }
}