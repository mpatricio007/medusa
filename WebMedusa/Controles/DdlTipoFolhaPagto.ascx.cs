using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.BLL;

namespace Medusa.Controles
{
    public partial class DLLTipoFolhaPagto : System.Web.UI.UserControl
    {
        public int id_tipo_folha_pagto
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

        public bool Enabled
        {
            get { return lista.Enabled; }
            set { lista.Enabled = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TipoFolhaPagtoBLL fp = new TipoFolhaPagtoBLL();
                lista.DataSource = fp.GetAll("nome");
                lista.Items.Insert(0, new ListItem("selecione um tipo de folha de pagamento...", "0"));
                lista.DataBind();
            }
        }
    }
}