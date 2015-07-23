using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.DAL;
using Medusa.LIB;
using Medusa.BLL;
using System.Text;

namespace Medusa.Sistemas.SCP
{
    public partial class PesquisaAssin : PageCrud<AssinBLL>
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            lbMsg = lblMsg;
            if (!IsPostBack)
            {
                cInteiroProjeto.Focus();
            }
        }

        protected void btPesquisa_Click(object sender, EventArgs e)
        {
            string strMsg = String.Empty;

            if (ObjBLL.GetAssin(cInteiroProjeto.Value.GetValueOrDefault(), cTextoSubProj.Text, ref strMsg))
                msg(strMsg);
            else
                msgError(strMsg);            
            Get();
            cInteiroProjeto.Focus();
        }

        protected override void Get()
        {
            divAssin.InnerHtml = !String.IsNullOrEmpty(ObjBLL.ObjEF.nome_arquivo) ?
                String.Format("<iframe src=\"{0}\" width=\"100%\" height=\"1000px\"></iframe>", ObjBLL.ObjEF.nome_arquivo) :
                String.Empty;
            divAssin.DataBind();
        }
   
        protected override void Set()
        {
            throw new NotImplementedException();
        }

        protected override void btCancelar_Click(object sender, EventArgs e)
        {
            cInteiroProjeto.Text = String.Empty;
            cInteiroProjeto.Focus();
            divAssin.InnerHtml = String.Empty;
            divAssin.DataBind();
        }

     
    }
}