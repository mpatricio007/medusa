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

namespace Medusa.Relatorios.RemessaBancaria
{
    public partial class RRemessaGRU : BasePage
    {
        protected void btGerarRelatorio_Click(object sender, EventArgs e)
        {
            Cache.Remove("lotes");
            dRelatorio.InnerHtml = String.Format("<iframe src=\"../../Relatorios/RemessaBancaria/ReportRemessaGRU.aspx?pk1={0}&pk2={1}&pk3={2}\" width=\"100%\" height=\"1000px\"></iframe>",
                Convert.ToString(cInteiroDe.Value.GetValueOrDefault()).Criptografar(),
                Convert.ToString(cInteiroAte.Value.GetValueOrDefault()).Criptografar()
                , Util.DateToString(cData1.Value).Criptografar());
            dRelatorio.DataBind();
            Util.ChamarScript("hideProgressDialog();", "hide");
        }

        protected void btGerarArquivo_Click(object sender, EventArgs e)
        {
            rbNovoEnvioLote_SelectedIndexChanged(null, null);

            btGerarRelatorio_Click(sender, e);
            var loteBLL = new LoteGRUBLL();
            Cache["lotes"] = loteBLL.GerarArquivosRemessa(cInteiroDe.Value.GetValueOrDefault(), cInteiroAte.Value.GetValueOrDefault(), cData1.Value);

            tbReenvio.Visible = ((List<LoteGRU>)Cache["lotes"]).Count() > 0;
            cTextoJustificativa.Text = String.Empty;

            SetGridLotesEnviados();

            Util.ChamarScript("openLotesDialog();", "");
        }

        public void SetGridLotesEnviados()
        {
            gridLotesEnviados.DataKeyNames = new string[] { "id_lote" };
            gridLotesEnviados.DataSource = (List<LoteGRU>)Cache["lotes"];
            gridLotesEnviados.DataBind();
        }
        protected void rbNovoEnvioLote_SelectedIndexChanged(object sender, EventArgs e)
        {
            trObs.Visible = (rbNovoEnvioLote.SelectedValue == Convert.ToString(true));
        }

        protected void gridLotesEnviados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridLotesEnviados.PageIndex = e.NewPageIndex;
            SetGridLotesEnviados();
        }

        protected void gridLotesEnviados_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int Id_lote = Convert.ToInt32(gridLotesEnviados.DataKeys[e.RowIndex]["id_lote"]);
            Cache["lotes"] = ((List<LotePagBB>)Cache["lotes"]).Where(it => it.id_lote != Id_lote).ToList();
            if (((List<LotePagBB>)Cache["lotes"]).Count() > 0)
                SetGridLotesEnviados();
            else
                Util.ChamarScript("closeLotesDialog();", "");
            e.Cancel = true;

        }

        protected void btOk_Click(object sender, EventArgs e)
        {
            if (rbNovoEnvioLote.SelectedValue == Convert.ToString(true))
            {
                foreach (var item in (List<LoteGRU>)Cache["lotes"])
                {
                    var lBLL = new LoteGRUBLL();
                    lBLL.Get(item.id_lote);
                    lBLL.GerarArquivo(cTextoJustificativa.Text);
                }
            }
            Util.ChamarScript("closeLotesDialog();", "");
            btGerarRelatorio_Click(null, null);
        }
    }
}