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
using Medusa.Relatorios.RemessaBancaria;
using Medusa.Relatorios;

namespace Medusa.Sistemas.RemessaBancaria
{
    public partial class ComprovantesGpsSemBarras : PageCrud<RemessaGpsSemCodBarraBLL>
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_remessa";
            //valor da chave primária
            //PkValue = Convert.ToInt32(this.txtCodigo.Text);
            // painel do grid
            pGrid = panelGrid;
            // painel do formulário de alteração
            pCadastro = panelCadastro;
            // gridview
            _grid = grid;
            lbMsg = new Label();
            _btAlterar = new Button();
            _btAlterar0 = new Button();
            _btInserir = new Button();
            _btInserir0 = new Button();
            _btExcluir = new Button();
            _btExcluir0 = new Button();

            _ddlSize = ddlSize;
            _ddlMode = ddlMode;
            _ddlOptions = ddlOptions;
            _txtProcura = txtProcura;
            _btProcurar = btSearch;
            _ckFilter = ckFilter;
            _dataListFiltros = DataListFiltros;

            if (!IsPostBack)
            {
                base.Page_Load(sender, e);
            }
        }

        protected override void Get()
        {
            Filter f1 = new Filter()
            {
                property = "id_remessa",
                value = Convert.ToString(PkValue),
                mode = "=="
            };
            filtros.Add(f1);
            dRelatorio.Visible = true;
            SetReport();
            filtros.Remove(f1);
        }

        protected override void Set()
        {
        }

        protected void btImprimirSelecionados_Click(object sender, ImageClickEventArgs e)
        {
            SetAdd();
            SetReport();
        }

        protected override void SetGrid()
        {
            Filter f = new Filter()
            {
                property = "TipoConciliacao.imprime_comprovante",
                value = "true",
                mode = "=="

            };
            filtros.Add(f);
            base.SetGrid();
            filtros.Remove(f);
        }
        protected void SetReport()
        {
            Filter f = new Filter()
            {
                property = "TipoConciliacao.imprime_comprovante",
                value = "true",
                mode = "=="
            };
            filtros.Add(f);
            ReportViewer ReportViewer1 = new ReportViewer();
            ReportViewer1.LocalReport.ReportPath = @"Relatorios\RemessaBancaria\RelatorioComprovanteGpsSemBarras.rdlc";
            var r = new RelatorioRemessaLoteGpsSemBarras();
            ReportDataSource rpd = new ReportDataSource("dsRemessaGps", r.GerarRelatorioComprovantes(ObjBLL.Find(
                filtros,
                (string)ViewState["SortExpression"],
                (string)ViewState["SortDirection"], 0).OfType<RemessaGpsSemCodBarra>().ToList()));
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rpd);
            ReportViewer1.LocalReport.Refresh();

            Session[PageOfReport.SessionName] = ReportViewer1;
            dRelatorio.InnerHtml = PageOfReport.iframe;
            dRelatorio.DataBind();
            filtros.Remove(f);
        }
    }
}