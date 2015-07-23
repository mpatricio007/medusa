using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.BLL;

namespace Medusa.Controles
{
    public partial class Endereco : System.Web.UI.UserControl
    {
       

        public string Titulo
        {
            get { return lblTitulo.Text; }
            set { lblTitulo.Text = value; }
        }
        
        public Medusa.DAL.Endereco Value
        {
            get 
            {
                Medusa.DAL.Endereco ender = new DAL.Endereco();
                ender.logradouro = this.cTextoLogradouro.Text;
                ender.numero = this.cTextoNumero.Text;
                ender.complemento = this.cTextoComplemento.Text;
                ender.bairro = this.cTextoBairro.Text;
                ender.pais = this.listaPais.SelectedValue;
                ender.cidade = this.listaPais.SelectedValue == "BRASIL" ? this.listaCidade.SelectedValue : this.cTextoCidade.Text;
                ender.uf = this.listaPais.SelectedValue == "BRASIL" ? this.listaUF.SelectedValue : this.cTextoUF.Text;                
                ender.cep = this.cTextoCep.Text;    
                return ender; 
            }
            set 
            {
                value = value ?? new Medusa.DAL.Endereco();
                value.pais = value.pais ?? "BRASIL";
                this.listaPais.SelectedValue = value.pais;
                listaPais_SelectedIndexChanged(null, null);
                this.cTextoLogradouro.Text = value.logradouro;
                this.cTextoNumero.Text = value.numero;
                this.cTextoComplemento.Text = value.complemento;
                this.cTextoBairro.Text = value.bairro;
                if (value.pais == "BRASIL")
                {
                    value.uf = value.uf ?? "SP";
                    this.listaUF.SelectedValue = value.uf;
                    if (!listaCidade.Items.Contains(new ListItem(value.cidade)))
                        SetCidades(value.uf);

                    value.cidade = value.cidade ?? "São Paulo";
                    this.listaCidade.SelectedValue = value.cidade;
                }
                else
                {
                    this.cTextoCidade.Text = value.cidade;
                    this.cTextoUF.Text = value.uf;
                }
                this.cTextoCep.Text = value.cep;
                         
            }
        }

        public string ValidationGroup
        {
            get
            {
                return rfvLogradouro.ValidationGroup;
            }
            set
            {
                rfvLogradouro.ValidationGroup = value;
                rfvBairro.ValidationGroup = value;
                rfvNumero.ValidationGroup = value;
                rfvCep.ValidationGroup = value;
                cvCidade.ValidationGroup = value;
                cvUF.ValidationGroup = value;
                cTextoUF.ValidationGroup = value;
                cTextoCidade.ValidationGroup = value;
            }
        }
        

        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!IsPostBack)
            {
                PaisBLL p = new PaisBLL();
                listaPais.DataSource = p.GetAll("nome");
                listaPais.Items.Insert(0, new ListItem("selecione um pais...", "0"));
                listaPais.DataBind();
                listaPais.SelectedValue = "BRASIL";

                UFBLL u = new UFBLL();
                listaUF.DataSource = u.GetAll("uf");
                listaUF.Items.Insert(0, new ListItem("UF", "0"));
                listaUF.DataBind();
                listaUF.SelectedValue = "SP";
            }
        }

        protected void listaUF_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetCidades(listaUF.SelectedValue);
        }       

        public void SetCidades(string strUF)
        {
            CidadeBLL c = new CidadeBLL();
            listaCidade.Items.Clear();
            listaCidade.DataSource = c.GetCidadesPorUF(strUF);
            listaCidade.Items.Insert(0, new ListItem("selecione uma cidade...", "0"));
            listaCidade.DataBind();
        }

        protected void listaPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool eh_internacional = !String.IsNullOrEmpty(listaPais.SelectedValue) & listaPais.SelectedValue != "BRASIL";
            this.listaUF.Visible = !eh_internacional;
            this.listaCidade.Visible = !eh_internacional;
            this.cvUF.Visible = !eh_internacional;
            this.cvCidade.Visible = !eh_internacional;
            this.cTextoUF.Visible = eh_internacional;
            this.cTextoCidade.Visible = eh_internacional;
            if (eh_internacional)
                this.cTextoCep.CssClass = String.Empty;// .Attributes.Clear();
            else
            {
                this.cTextoCep.CssClass = "cep";// .Attributes.Add("onkeyup", "formataCEP(this,event);");
            }

        }
    }
}