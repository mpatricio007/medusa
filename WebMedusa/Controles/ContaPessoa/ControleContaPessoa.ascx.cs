using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.BLL;
using Medusa.DAL;

namespace Medusa.Controles
{
    public partial class ControleContaPessoa : System.Web.UI.UserControl
    {

        public ContaPessoaFisica Value
        {
            get
            {
              
                var contaPF = new ContaPessoaFisica();
                contaPF.agencia = this.textAgencia.Text;
                contaPF.digitoAgencia = this.textDigitoAgencia.Text;
                contaPF.conta = this.textConta.Text;
                contaPF.digitoConta = this.textDigitoConta.Text;
                contaPF.id_banco = Convert.ToInt32(this.cDdlBancos1.Id_banco);
                contaPF.tipoConta = (TipoConta)Enum.Parse(typeof(TipoConta), rbTipoConta.SelectedValue);
                return contaPF;
            }
            set
            {
                value = value ?? new ContaPessoaFisica();

                this.textAgencia.Text = Convert.ToString(value.agencia);
                this.textDigitoAgencia.Text = Convert.ToString(value.digitoAgencia);
                this.textConta.Text = Convert.ToString(value.conta);
                this.textDigitoConta.Text = Convert.ToString(value.digitoConta);
                this.cDdlBancos1.Id_banco = Convert.ToInt32(value.id_banco);
                if(value.intTipoconta.HasValue)
                    this.rbTipoConta.SelectedValue =  Convert.ToString(value.tipoConta);
            }
        }

        public string ValidationGroup
        {
            get
            {
                return cDdlBancos1.ValidationGroup;
            }
            set
            {
                cDdlBancos1.ValidationGroup = value;
                textAgencia.ValidationGroup = value;
                textConta.ValidationGroup = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                rbTipoConta.DataSource = Enum.GetNames(typeof(TipoConta));
                rbTipoConta.SelectedValue = Convert.ToString(TipoConta.cc);

                rbTipoConta.DataBind();
            }
        }

        
    }
}