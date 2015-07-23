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
    public partial class DdlMateriais : System.Web.UI.UserControl
    {
        public DAL.RequisicaoMaterial Value
        {
            get
            {
                //return new DAL.RequisicaoMaterial(Util.StringToInteiro(txtQuantidade.Text).GetValueOrDefault(), (DAL.MaterialConsumo)Enum.Parse(typeof(DAL.MaterialConsumo), ddlMaterial.SelectedValue));
                return new DAL.RequisicaoMaterial();
            }
            set
            {
                txtQuantidade.Text = Util.InteiroToString(value.quantidade);

                if (value.id_material != 0)
                    ddlMaterial.SelectedValue = Enum.GetName(typeof(DAL.MaterialConsumo), value.id_material);
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

        public override void Focus()
        {
            this.ddlMaterial.Focus();
            this.txtQuantidade.Focus();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlMaterial.DataSource = Enum.GetNames(typeof(DAL.MaterialConsumo));
                ddlMaterial.DataBind();
                //MaterialConsumoBLL m = new MaterialConsumoBLL();
                //ddlMaterial.DataSource = m.GetAll("descricao");
                //ddlMaterial.Items.Insert(0, new ListItem("selecione um material...", "0"));
                //ddlMaterial.DataBind();
            }
        }
    }
}