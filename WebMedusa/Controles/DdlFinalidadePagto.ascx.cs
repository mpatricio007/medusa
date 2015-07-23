using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.BLL;

namespace Medusa.Controles
{
    public partial class DdlFinalidadePagto : System.Web.UI.UserControl
    {
        public int Id_banco_lote { get; set; }

        public int Id_banco_favorecido { get; set; }

        public int Id_fin_pagto
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
                //lista.Items.Clear();
                //FinalidadePagtoBLL fp = new FinalidadePagtoBLL();
                //lista.DataSource = fp.GetAllMesmoBanco(Id_banco_favorecido == Id_banco_lote);
                //lista.Items.Insert(0, new ListItem("selecione uma finalidade de pagto...", "0"));
                //lista.DataBind();
            }
        }

        public override void DataBind()
        {
            lista.Items.Clear();
            FinalidadePagtoBLL fp = new FinalidadePagtoBLL();
            lista.DataSource = fp.GetAllMesmoBanco(Id_banco_favorecido == Id_banco_lote);
            lista.Items.Insert(0, new ListItem("selecione uma finalidade de pagto...", "0"));
            lista.DataBind();
        }
    }
}