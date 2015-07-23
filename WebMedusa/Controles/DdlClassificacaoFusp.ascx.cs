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
    public partial class DdlClassificacaoFusp : System.Web.UI.UserControl
    {
        public int? Id_classificacao_fusp
        {
            get
            {
                return Util.StringToInteiroOrNullable(lista.SelectedValue);
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ClassificacaoFuspBLL cf = new ClassificacaoFuspBLL();
                lista.DataSource = cf.GetAll("nome");
                lista.Items.Insert(0, new ListItem("selecione uma classificação...", "0"));
                lista.DataBind();
            }
        }
    }
}