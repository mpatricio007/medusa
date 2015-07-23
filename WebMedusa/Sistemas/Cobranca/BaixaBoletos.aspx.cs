using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.LIB;
using Medusa.BLL;
using Medusa.DAL;

namespace Medusa.Sistemas.Cobranca
{
    public partial class BaixaBoletos : System.Web.UI.Page
    {
        private BoletoCobrancaBLL ObjBLL = new BoletoCobrancaBLL();
        
        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!IsPostBack)
            {
                this.txtCodigo.Focus();      
            }
        }


        protected void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            Get();
            this.btAlterar0.Focus();
        }

        protected void btCancelar_Click(object sender, EventArgs e)
        {
            this.txtCodigo.Text = "0";
            Get();
            this.txtCodigo.Focus();
        }

        protected void btAlterar_Click(object sender, EventArgs e)
        {
            ObjBLL.Get(Convert.ToInt32(this.txtCodigo.Text));
            Set();
            string mensagem;
            if (ObjBLL.BaixarBoleto(out mensagem))
                msg(mensagem);
            else
                msgError(mensagem);
            this.txtCodigo.Text = "0";
            Get();
            this.txtCodigo.Focus();
        }

        protected void Get()
        {
            ObjBLL.Get(Convert.ToInt32(this.txtCodigo.Text));
            if (ObjBLL.Exists())
            {
                this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_boleto);
                this.lbValor.Text = Util.DecimalToString(ObjBLL.ObjEF.valor);
                this.lbDataVencto.Text = Util.DateToString(ObjBLL.ObjEF.data_vencto);
                this.cDataPagamento.Value = ObjBLL.ObjEF.data_pgto.HasValue ? ObjBLL.ObjEF.data_pgto : DateTime.Now;
                this.cValorPago.Value = ObjBLL.ObjEF.valor_pgto.HasValue ? ObjBLL.ObjEF.valor_pgto : ObjBLL.ObjEF.valor;
                this.cTextoObs.Text = ObjBLL.ObjEF.obs;
                this.lbSacado.Text = ObjBLL.ObjEF.id_boleto != 0 ? ObjBLL.ObjEF.EventoSacado.Sacado.PessoaFisica.nome : String.Empty;
                this.lbParcela.Text = Convert.ToString(ObjBLL.ObjEF.num_parcela);
            }
            else
                msgError("Boleto não encontrado!");
        }

        protected void Set()
        {
            ObjBLL.ObjEF.id_boleto = Convert.ToInt32(this.txtCodigo.Text);          
            ObjBLL.ObjEF.data_pgto = this.cDataPagamento.Value;
            ObjBLL.ObjEF.valor_pgto = this.cValorPago.Value;
            ObjBLL.ObjEF.obs = this.cTextoObs.Text;
        }

        protected virtual void msg(string msg)
        {
            lbMsg.BackColor = System.Drawing.Color.Green;
            lbMsg.ForeColor = System.Drawing.Color.White;
            lbMsg.Text = string.Format("* {0} !", msg);
        }

        protected virtual void msgError(string msg)
        {
            lbMsg.BackColor = System.Drawing.Color.Red;
            lbMsg.ForeColor = System.Drawing.Color.White;
            lbMsg.Text = string.Format("* {0} !?", msg);
        }
    }
}