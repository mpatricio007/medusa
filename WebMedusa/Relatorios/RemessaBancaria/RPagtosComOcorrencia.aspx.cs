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
    public partial class RPagtosComOcorrencia : BasePage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                base.Page_Load(sender, e);
                lblMsg.Text = String.Empty;
                string dt = Request.QueryString["pk"];
                if (!String.IsNullOrEmpty(dt))
                {
                    cData1.Value = Util.StringToDate(dt.DesCriptografar());
                    btGerarRelatorio0_Click(sender, e);
                }
            }

        }
        protected void btGerarRelatorio0_Click(object sender, EventArgs e)
        {
            if (cInteiroDe.Value.GetValueOrDefault() <= cInteiroAte.Value.GetValueOrDefault())
            {
                lblMsg.Text = String.Empty;
                dRelatorio.InnerHtml = String.Format("<iframe src=\"../../Relatorios/RemessaBancaria/ReportPagtosComOcorrencia.aspx?pk1={0}&pk2={1}&pk3={2}&pk4={3}&pk5={4}\" width=\"100%\" height=\"1000px\"></iframe>",
                    Convert.ToString(cInteiroDe.Value.GetValueOrDefault()).Criptografar(),
                    Convert.ToString(cInteiroAte.Value.GetValueOrDefault()).Criptografar(),
                    Util.DateToString(cData1.Value).Criptografar(),
                    Util.DateToString(cData2.Value).Criptografar(),
                    Util.DateToString(cData3.Value).Criptografar());
                dRelatorio.DataBind();
            }
            else
            {
                lblMsg.BackColor = System.Drawing.Color.Red;
                lblMsg.ForeColor = System.Drawing.Color.White;
                lblMsg.Text = "** interválo inválido!";
                dRelatorio.InnerHtml = String.Empty;
                dRelatorio.DataBind();
            }

        }
    }
}