﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.DAL;
namespace Medusa.Controles
{
    public partial class ListaPessoaEmails : System.Web.UI.UserControl
    {
        public List<DAL.PessoaEmail> Value
        {
            get
            {
                if (ViewState[ID] == null)
                    Value = new List<DAL.PessoaEmail>();
                return (List<DAL.PessoaEmail>)ViewState[ID];
            }
            set
            {
                ViewState[ID] = value;
            }
        }

        private int index
        {
            get
            {
                return Convert.ToInt32(txtId.Text);
            }
            set
            {
                txtId.Text = Convert.ToString(value);

            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                setAdd();
            }
        }

        protected void setAdd()
        {
            btAdd.Visible = true;
            btCancel.Visible = true;
            btExcluir.Visible = false;
            limparCampos();
        }

        protected void setUpdate()
        {
            btAdd.Visible = true;
            btCancel.Visible = true;
            btExcluir.Visible = true; 
            this.cEmail1.Focus();
        }

        protected void btAdd_Click(object sender, EventArgs e)
        {
            set();
            limparCampos();
            gridEmails.DataBind();
            index = -1;
            setAdd();
        }

        protected void gridEmails_DataBinding(object sender, EventArgs e)
        {
            gridEmails.DataSource = Value;
        }

        protected void gridEmails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            index = e.RowIndex;
            get();
            gridEmails.DataBind();
        }

        public override void DataBind()
        {
            gridEmails.DataBind();
            base.DataBind();
        }

        protected void btCancel_Click(object sender, EventArgs e)
        {
            index = -1;
            limparCampos();
            gridEmails.DataBind();
            setAdd();
        }

        private void get()
        {
            DAL.PessoaEmail em = Value[index];
            this.cEmail1.Value = em;
            setUpdate();
        }

        private void set()
        {
            var em = index < 0 ? new DAL.PessoaEmail() : Value[index];
            em.email = this.cEmail1.Value;
            if (index < 0)
                Value.Add(em);
            else
                Value[index] = em;
        }

        protected void btExcluir_Click(object sender, EventArgs e)
        {
            Value.RemoveAt(index);
            limparCampos();
            index = -1;
            setAdd();
            gridEmails.DataBind();            
        }

        protected void limparCampos()
        {
            this.cEmail1.Value = new DAL.Email();
            this.cEmail1.Focus();
        }
    }
}