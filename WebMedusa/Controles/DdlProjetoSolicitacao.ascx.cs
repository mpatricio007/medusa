using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.BLL;
using Medusa.LIB;

namespace Medusa.Controles
{
    public partial class DdlProjetoSolicitacao : System.Web.UI.UserControl
    {
        public int Id_proj_sol
        {
            get
            {
                return Convert.ToInt32(lista.SelectedValue);
            }
            set
            {
                lista.SelectedValue = Convert.ToString(value);
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

        public bool Enable
        {
            get
            {
                return lista.Enabled;
            }
            set
            {
                lista.Enabled = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ProjetoSolicitacaoBLL ps = new ProjetoSolicitacaoBLL();
                lista.DataSource = ps.GetAll("id_sol_proj");
                lista.Items.Insert(0, new ListItem("selecione uma solicitação de projeto...", "0"));
                lista.DataBind();
            }
        }
    }
}