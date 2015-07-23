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

namespace Medusa.Sistemas.Conciliacao
{
    public partial class ImportarAgenda : BasePage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
        } 

        protected void btImportar_Click(object sender, EventArgs e)
         {
            CarregarPagAgendados cpa = new CarregarPagAgendados();
            Util.ShowMessage(cpa.GerarLancamentosBB(cData1.Value.GetValueOrDefault()));
        }

      
    }
}