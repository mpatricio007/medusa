using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.BLL;
using Medusa.DAL;
using Medusa.LIB;

namespace Medusa.Controles
{
    public partial class DdlStatusRequisicaoMaterial : System.Web.UI.UserControl
    {
        public int Id_status_requisicao_Material
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

        public bool Enabled
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
                lista.Items.Insert(0, new ListItem("selecione um status...", "0"));
                lista.DataBind();
            }
        }

        public override void DataBind()
        {
            //if(Id_status_requisicao != 0)
            //{
                lista.Items.Clear();
            StatusRequisicaoMaterialBLL s = new StatusRequisicaoMaterialBLL();

            
            lista.DataSource = s.GetAll("nome").OfType<StatusRequisicaoMaterial>().Where(it => StatusRequisicaoMaterialBLL.LstStatusFree.Contains(it.id_status_requisicao_material));
            lista.Items.Insert(0, new ListItem("selecione um status...", "0"));
            //lista.DataBind();
                //}
            base.DataBind();
        }
    }
}