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
    public partial class DdlMaterialConsumo : System.Web.UI.UserControl
    {
        public int Id_material
        {
            get
            {
                return Util.StringToInteiro(lista.SelectedValue).GetValueOrDefault();
            }
            set
            {
                lista.SelectedValue = Util.InteiroToString(value);
            }
        }

        public override void Focus()
        {
            lista.Focus();
        }

        public string strMaterial
        {
            get
            {
                return lista.SelectedItem.Text;
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

        public bool CausesValidation
        {
            get
            {
                return lista.CausesValidation;
            }
            set
            {
                lista.CausesValidation = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MaterialConsumoBLL m = new MaterialConsumoBLL();
                lista.DataSource = m.GetAll("descricao");
                lista.Items.Insert(0, new ListItem("selecione um material...", "0"));
                lista.DataBind();
            }
        }
    }
}