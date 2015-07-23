using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.LIB;
using Medusa.BLL;

namespace Medusa.Sistemas.SCP
{
    public partial class ControleTornarPA : BaseControl
    {
        // Delegate
        public delegate void ClickEventHandler(object sender, System.EventArgs e);
        public event ClickEventHandler Click;

        public int Id_solicitacao
        {
            get
            {
                if (ViewState[ID] == null)
                    Id_solicitacao = 0;
                return (int)ViewState[ID];
            }
            set { ViewState[ID] = value; }
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Security();
            }
        }

        protected void btTornarA_Click(object sender, EventArgs e)
        {
            if(Click != null)
                Click(sender,e);
        }
    }
}