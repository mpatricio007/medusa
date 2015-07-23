using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Medusa.BLL.RemessaBB;
using Medusa.LIB;

namespace Medusa
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cData1.Value = DateTime.Now;
                txtData.Text = Util.DateToString(DateTime.Now);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            txtData.Text = Util.DateToString(DateTime.Now);
            txtData.DataBind();
            Label1.Text = Util.DateToString(cData1.Value);
        }
    }
}

       
   