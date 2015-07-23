using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Medusa.LIB;

namespace Medusa.Sistemas.Conciliacao
{
    public partial class ArquivoRetorno : BasePage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                base.Page_Load(sender, e);
        }

        protected void btCarregarArquivo_Click(object sender, EventArgs e)
        {
            if (fuArquivo.HasFile)
            {
                StreamReader reader = new StreamReader(fuArquivo.PostedFile.InputStream);
                bool rt = false;
                string saida = BLL.LayoutBB.ExtratoBB.ImportarLancamentos(
                BLL.LayoutBB.ExtratoBB.GetArquivoRetorno(reader),out rt);

                lblMsg.ForeColor = rt ? System.Drawing.Color.Green : System.Drawing.Color.Red;                
                lblMsg.Text = saida;
                
                
                //var ds = BLL.LayoutBB.ExtratoBB.GetArquivoRetorno(reader);
                //GridView1.DataSource = ds.OfType<BLL.LayoutBB.LancamentoExtratoBB>();
                //GridView1.DataBind();               
            }
        }
    }
}