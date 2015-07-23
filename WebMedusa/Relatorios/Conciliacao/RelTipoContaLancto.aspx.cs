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
using Microsoft.Reporting.WebForms;

namespace Medusa.Relatorios.Conciliacao
{
    public partial class RelTipoContaLancto : BasePage
    {
        protected List<ListaTiposLcto> lista
        {
            get
            {
                if (ViewState["listaTiposLcto"] == null)
                    lista = new List<ListaTiposLcto>();
                return (List<ListaTiposLcto>)ViewState["listaTiposLcto"];
            }
            set
            {
                ViewState["listaTiposLcto"] = value;
            }
        }
      
        protected void GerarRelatorio()
        {
            ReportViewer ReportViewer1 = new ReportViewer();
            ReportViewer1.LocalReport.ReportPath = @"Relatorios\Conciliacao\RelLanctoPorTipo.rdlc";
            ReportParameter[] parameters = new ReportParameter[1];
            parameters[0] = new ReportParameter("DescrFiltroUtilizado", string.Format("data de: {0:d}  a {1:d}", cData1.Value.GetValueOrDefault(), cData2.Value.GetValueOrDefault()));
            ReportViewer1.LocalReport.SetParameters(parameters);
            var r = new RelatorioLanctoTipo();
            ReportDataSource rpd = new ReportDataSource("DS_Lancto", r.GerarRelatorio(lista, cData1.Value.GetValueOrDefault(), cData2.Value.GetValueOrDefault()));
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rpd);
            ReportViewer1.LocalReport.Refresh();

            Session[PageOfReport.SessionName] = ReportViewer1;
            dRelatorio.InnerHtml = PageOfReport.iframe;
            dRelatorio.DataBind();
        }

        protected void btImportar_Click(object sender, EventArgs e)
        {
            GerarRelatorio();
        }


        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TipoLctoBLL tl = new TipoLctoBLL();
                listaLcto.DataSource = tl.GetAll();
                listaLcto.Items.Insert(0, new ListItem("D - Todos os débitos", "D"));
                listaLcto.Items.Insert(0, new ListItem("C - Todos os créditos", "C"));                
                listaLcto.Items.Insert(0, new ListItem("selecione um tipo de lançamento...", "0"));
                listaLcto.DataBind();

                cData1.Value = DateTime.Now.AddMonths(-1);
                cData2.Value = DateTime.Now;
                GerarRelatorio();
            }
        }

        protected void dlTipoLancto_DeleteCommand(object source, DataListCommandEventArgs e)
        {
            lista.RemoveAt(e.Item.ItemIndex);
            dlTipoLancto.DataBind();
        }

        protected void dlTipoLancto_DataBinding(object sender, EventArgs e)
        {
            dlTipoLancto.DataSource = lista;
        }

        protected void btSelecionar_Click(object sender, EventArgs e)
        {
            int id_tipo;
            var tl = new TipoLctoBLL();
            var ds = int.TryParse(listaLcto.SelectedValue, out id_tipo) ?
                tl.Find(it => it.id_tipo_lcto == id_tipo) : tl.Find(it => it.dc == listaLcto.SelectedValue);

            foreach (var t in ds)
            {
                ListaTiposLcto l = new ListaTiposLcto(t.id_tipo_lcto, t.descricao, t.dc);
                if (!lista.Exists(p => p.id_tipo_lcto == tl.ObjEF.id_tipo_lcto))
                    lista.Add(l);
            }
            dlTipoLancto.DataBind();
        }

        protected void btLimpar_Click(object sender, EventArgs e)
        {
            lista = new List<ListaTiposLcto>();
            dlTipoLancto.DataBind();
        }

    }
}