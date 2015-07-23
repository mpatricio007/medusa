using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.BLL;
using Medusa.DAL;
using Medusa.LIB;

namespace Medusa.Controles.Pesquisa
{
    public partial class PesquisaConta : System.Web.UI.UserControl
    {
        public int Id_Conta
        {
            get
            {
                return Convert.ToInt32(lista.SelectedValue);
            }
            set
            {
                ContaBLL c = new ContaBLL();
                c.Get(value);
                cDdlProjeto1.Id_projeto = c.ObjEF.id_projeto.GetValueOrDefault();
                cDdlProjeto1_SelectedIndexChanged(null, null);
                lista.SelectedValue = Convert.ToString(value);
            }
        }

        public bool EnableValidator
        {
            get
            {
                return cv.Enabled;
            }
            set
            {
                cv.Enabled = value;

            }
        }


        public string ValidationGroup
        {
            get
            {
                return cv.ValidationGroup;
            }
            set
            {
                cv.ValidationGroup = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }

        protected void cDdlProjeto1_SelectedIndexChanged(object sender, EventArgs e)
        {
             lista.Items.Clear();
             ContaBLL c = new ContaBLL();
             lista.DataSource = c.GetAllAg1897x(cDdlProjeto1.Id_projeto);             
             lista.DataBind();
             lista.Focus();
        }

        public override void Focus()
        {
            cDdlProjeto1.Focus();
        }
    }
}