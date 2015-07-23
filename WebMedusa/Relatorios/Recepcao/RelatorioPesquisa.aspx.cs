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
using System.Text;

namespace Medusa.Relatorios.Recepcao
{
    public partial class RelatorioPesquisa : PageCrud<EntradaBLL>
    {
        protected override void Page_Load(object sender, EventArgs e)
        {

            // chave primária da tabela
            PRIMARY_KEY = "id_entrada";
            //valor da chave primária
            //PkValue = Convert.ToInt32(this.txtCodigo.Text);
            // painel do grid
            pGrid = new Panel();
            // painel do formulário de alteração
            pCadastro = pCadastro;
            // gridview
            _grid = new GridView();
            lbMsg = new Label();
            _btAlterar = new Button();
            _btAlterar0 = new Button();
            _btInserir = new Button();
            _btInserir0 = new Button();
            _btExcluir = new Button();
            _btExcluir0 = new Button();
            _ddlSize = new DropDownList();
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
        }

        protected override void Set()
        {
        }

        protected override void SetGrid()
        {//Relatorios\Recepcao\RelatorioPesquisa.rdlc

            if (filtros.Count() > 0)
            {
                //ReportViewer1.Visible = true;
                //var filtro = new StringBuilder();
                //foreach (var item in filtros)
                //    filtro.Append(String.Format("{0} {1} {2}  -  ", item.property_name, item.mode_name, item.value));
                //ReportParameter[] parameters = new ReportParameter[1];
                //parameters[0] = new ReportParameter("Filtro", filtro.ToString());
                //ReportViewer1.LocalReport.SetParameters(parameters);
                //var re = new RelatorioPesquisaEntrada();
                //ReportDataSource rpd = new ReportDataSource("dsPesquisaEntrada", re.GerarRelatorio(ObjBLL.Find(
                //   filtros,
                //   (string)ViewState["SortExpression"],
                //   (string)ViewState["SortDirection"], 0).OfType<Entrada>().ToList()));
                //ReportViewer1.LocalReport.DataSources.Clear();
                //ReportViewer1.LocalReport.DataSources.Add(rpd);
                //ReportViewer1.LocalReport.Refresh();
                ReportViewer ReportViewer1 = new ReportViewer();
                ReportViewer1.LocalReport.ReportPath = @"Relatorios\Recepcao\RelatorioPesquisa.rdlc";
                var filtro = new StringBuilder();
                foreach (var item in filtros)
                    filtro.Append(String.Format("{0} {1} {2}  -  ", item.property_name, item.mode_name, item.value));
                ReportParameter[] parameters = new ReportParameter[1];
                parameters[0] = new ReportParameter("Filtro", filtro.ToString());
                ReportViewer1.LocalReport.SetParameters(parameters);
                var re = new RelatorioPesquisaEntrada();
                ReportDataSource rpd = new ReportDataSource("dsPesquisaEntrada", re.GerarRelatorio(ObjBLL.Find(
                   filtros,
                   (string)ViewState["SortExpression"],
                   (string)ViewState["SortDirection"], 0).OfType<Entrada>().ToList()));
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rpd);
                ReportViewer1.LocalReport.Refresh();

                Session[PageOfReport.SessionName] = ReportViewer1;
                dRelatorio.InnerHtml = PageOfReport.iframe;
                dRelatorio.DataBind();
            }
            //else
            //    ReportViewer1.Visible = false;
        }

        protected override void SetView()
        {
            
        }
    }
}