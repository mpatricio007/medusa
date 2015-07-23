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
using Medusa.Relatorios.RemessaBancaria;
using Microsoft.Reporting.WebForms;
using Medusa.Relatorios;

namespace Medusa.Sistemas.RemessaBancaria
{
    public partial class ComprovantesGuias : PageCrud<RemessaConsBLL>
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
            SetReport();
            filtros.Remove(f1);
        }

        protected override void SetAdd()
        {
            base.SetAdd();
            btImprimFiltrados.Visible = false;
        }

        protected override void SetModify()
        {
            base.SetModify();
            btImprimFiltrados.Visible = false;
        }

        protected override void SetView()
        {
            base.SetView();
            btImprimFiltrados.Visible = true;
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
            var rep = new ReportViewer();
            rep.LocalReport.ReportPath = "Relatorios\\RemessaBancaria\\RelatorioComprovantePagtogGuias.rdlc";
            var r = new RelatorioRemessaCons();
            var rpd = new ReportDataSource("dsComprovanteGuias", r.GerarRelatorioComprovantes(ObjBLL.Find(
                filtros,
                (string)ViewState["SortExpression"],
                (string)ViewState["SortDirection"], 0).OfType<RemessaCons>().ToList()));
            rep.LocalReport.DataSources.Clear();
            rep.LocalReport.DataSources.Add(rpd);
            rep.LocalReport.Refresh();

            filtros.Remove(f);

            Session[PageOfReport.SessionName] = rep;
            dReport.InnerHtml = PageOfReport.iframe;
            dReport.DataBind();
        }        
    }
}