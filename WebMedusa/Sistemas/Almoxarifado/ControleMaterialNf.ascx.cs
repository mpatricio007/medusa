using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.DAL;
using Medusa.LIB;
using Medusa.BLL;
namespace Medusa.Sistemas.Almoxarifado
{
    public partial class ControleMaterialNf : System.Web.UI.UserControl
    {
        public List<MaterialNotaFiscal> Value
        {
            get
            {
                if (Cache[ID] == null)
                    Cache[ID] = new List<MaterialNotaFiscal>();
                return (List<MaterialNotaFiscal>)Cache[ID];
            }
            set
            {
                Cache[ID] = value;
            }
        }

        private int index
        {
            get
            {
                return Convert.ToInt32(txtCodigo.Text);
            }
            set
            {
                txtCodigo.Text = Convert.ToString(value);

            }
        }

        public int Id_nf_material { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Cache.Remove(ID);
                SetAdd();
            }
        }

        protected void SetAdd()
        {
            btInserir.Visible = true;
        }

        protected void SetModify()
        {
            btInserir.Visible = true;
            this.cDdlMaterialConsumo1.Focus();
        }

        protected void Set()
        {
            var req = new MaterialNotaFiscal();
            req.id_material = cDdlMaterialConsumo1.Id_material;
            req.StrMaterial = cDdlMaterialConsumo1.strMaterial;
            req.quantidade = cInteiro1.Value.GetValueOrDefault();

            if (index < 0)
                Value.Add(req);
            else
                Value[index] = req;
        }

        protected void msg(string msg)
        {
            lblMsg.BackColor = System.Drawing.Color.Green;
            lblMsg.ForeColor = System.Drawing.Color.White;
            lblMsg.Text = string.Format("* {0} !", msg);
        }

        protected void msgError(string msg)
        {
            lblMsg.BackColor = System.Drawing.Color.Red;
            lblMsg.ForeColor = System.Drawing.Color.White;
            lblMsg.Text = string.Format("* {0} !", msg);
        }

        public override void DataBind()
        {
            grid.DataBind();
            base.DataBind();

            lblMsg.Text = String.Empty;
            cDdlMaterialConsumo1.Id_material = 0;
            cInteiro1.Value = 0;
            cDdlMaterialConsumo1.Focus();
        }

        protected void btInserir_Click(object sender, EventArgs e)
        {
            string strMSg = String.Empty;
            if (cInteiro1.Value > 0)
            {
                Set();
                DataBind();
                SetAdd();
            }
            else
                msgError("quantidade deve ser maior que zero");
        }

        public void SetGrid()
        {
            grid.DataSource = Value;
            grid.DataBind();
        }

        protected void grid_DataBinding(object sender, EventArgs e)
        {
            grid.DataSource = Value;
        }

        protected void grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Value.RemoveAt(e.RowIndex);
            index = -1;
            grid.DataBind();
        }

        protected void btCancelar_Click(object sender, EventArgs e)
        {
            index = -1;
            grid.DataBind();
            SetAdd();
        }
    }
}