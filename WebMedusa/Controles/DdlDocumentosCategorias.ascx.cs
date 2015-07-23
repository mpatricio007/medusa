using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.BLL;

namespace Medusa.Controles
{
    public partial class DdlDocumentosCategorias : System.Web.UI.UserControl
    {
        public int Id_documento
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

        public int Id_categoria
        {
            get
            {
                if (ViewState[ID] == null)
                    Id_categoria = 0;
                return (int)ViewState[ID];
            }
            set
            {
                ViewState[ID] = value;
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

        public override void DataBind()
        {
            var dcBLL = new DocumentosCategoriaBLL();
            lista.Items.Clear();
            lista.DataSource = dcBLL.GetDocumentosPorCategoria(Id_categoria);
            lista.Items.Insert(0, new ListItem("selecione um documento...", "0"));
            base.DataBind();
        }
    }
}