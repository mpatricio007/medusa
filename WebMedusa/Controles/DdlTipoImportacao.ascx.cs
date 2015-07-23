using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.BLL;

namespace Medusa.Controles
{
    public partial class DdlTipoImportacao : System.Web.UI.UserControl
    {
        public int Id_tipo
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

        public string Texto
        {
            get
            {
                return lista.SelectedItem.Text ;
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
                TipoImpArquivoBLL t = new TipoImpArquivoBLL();
                lista.DataSource = t.GetAll("descricao");
                lista.Items.Insert(0, new ListItem("selecione um tipo de arquivo...", "0"));
                lista.DataBind();
            }
        }
    }
}