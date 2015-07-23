using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.DAL;
using Medusa.BLL;
using Medusa.LIB;
using System.Linq.Expressions;
using System.Reflection;

namespace Medusa.Sistemas.RemessaBancaria
{
    public partial class ImportacaoFolha : Page
    {
        protected  void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               // base.Page_Load(sender, e);
            }
        }

        protected void btImportar_Click(object sender, EventArgs e)
        {
            string strMsg = String.Empty;
            var loteBLL = new LotePagBBBLL();
            loteBLL.ImportaFolhaPagtoAntiga(cData.Value.GetValueOrDefault(), cDdlTipoFolhaPagto.id_tipo_folha_pagto, ref strMsg);

            Util.ChamarScript("hideProgressDialog();", "");
            Util.ShowMessage(strMsg);
        }

        protected void btCancelar_Click(object sender, EventArgs e)
        {
            cData.Value = null;
            cDdlTipoFolhaPagto.id_tipo_folha_pagto = 0;
        }
    }
}