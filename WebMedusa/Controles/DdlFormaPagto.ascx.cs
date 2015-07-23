using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.BLL;

namespace Medusa.Controles
{
    public partial class DdlFormaPagto : System.Web.UI.UserControl
    {
        public int Id_banco_lote { get; set; }

        public int Id_banco_favorecido { get; set; }

        public decimal Valor { get; set; }

        public int Id_forma_pagto
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lista.Items.Insert(0, new ListItem("selecione uma forma de pagto...", "0"));
            }
        }

        public override void DataBind()
        {
            lista.Items.Clear();
            var fp = new FormaPagtoBLL();            
            lista.DataSource = fp.GetAllFormasPagtosLotePagBB(Id_banco_lote, Id_banco_favorecido == Id_banco_lote, Valor);
            lista.Items.Insert(0, new ListItem("selecione uma forma de pagto...", "0"));
            lista.DataBind();
        }
    }
}