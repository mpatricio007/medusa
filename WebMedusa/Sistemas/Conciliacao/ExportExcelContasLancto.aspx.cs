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
using System.Text;

namespace Medusa.Sistemas.Conciliacao
{
    public partial class ExportExcelContasLancto : PageCrud<ContaLanctoBLL>
    {
        Dictionary<string, string> dicSearch = new Dictionary<string, string>()
        {
            {"Conta","id_conta"},
        };

        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_lcto_conta";
            //valor da chave primária
          //  PkValue = Convert.ToInt32(this.txtCodigo.Text);
            // painel do grid
            pGrid = panelGrid;
            // painel do formulário de alteração
            pCadastro = panelCadastro;
            // gridview
            _grid = grid;
            //lbMsg = lblMsg;
            //_btAlterar = btAlterar;
            //_btAlterar0 = btAlterar0;
            //_btInserir = btInserir;
            //_btInserir0 = btInserir0;
            //_btExcluir = btExcluir;
            //_btExcluir0 = btExcluir0;
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
                ViewState["SortExpression"] = PRIMARY_KEY;
                ViewState["SortDirection"] = "ASC";
                this.ddlOptions_SelectedIndexChanged(null, null);
            }
        }

        protected override void Get()
        {
            
        }

        protected override void Set()
        {

        }

        protected void btExportToExcel_Click(object sender, EventArgs e)
        {
            var gridToExport = _grid;
            gridToExport.AllowPaging = false;
            gridToExport.AllowSorting = false;
            var lstColumns = gridToExport.Columns.OfType<CommandField>().ToList();
            foreach (var item in lstColumns)
                gridToExport.Columns.Remove(item);

            foreach (var item in gridToExport.Columns.OfType<TemplateField>().ToList())
            {
                var t = (TemplateField)item;
                t.ItemTemplate = new GridViewTemplate(t.SortExpression);
            }

            gridToExport.DataSource = ObjBLL.FindConsultaProjeto(
                filtros,
                (string)ViewState["SortExpression"],
                (string)ViewState["SortDirection"], 0);
            gridToExport.DataBind();
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("content-disposition", String.Format("attachment;filename={0}.xls", gridToExport.Caption));
            Response.Charset = "";
            this.EnableViewState = false;

            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

            gridToExport.RenderControl(htw);

            Response.Write(sw.ToString());
            Response.End();
        }

        protected override void btProcurar_Click(object sender, EventArgs e)
        {
            if (!_ckFilter.Checked)
                filtros.Clear();

            if (!String.IsNullOrEmpty(this._txtProcura.Text))
                filtros.Add(setFilter());

            if (_ckFilter.Checked)
            {
                _dataListFiltros.DataBind();
                this._txtProcura.Text = String.Empty;
            }

            SetGrid();
            SetView();
        }

        public override void VerifyRenderingInServerForm(Control control) { }

        protected override void SetGrid()
        {
            if (_ddlSize.SelectedValue == "0")
                _grid.PageSize = 50;
            else
                _grid.PageSize = Convert.ToInt32(_ddlSize.SelectedValue);

            int size = 10 * Convert.ToInt32(this._ddlSize.SelectedValue);
            _grid.DataKeyNames = new string[] { PRIMARY_KEY };
            _grid.DataSource = ObjBLL.FindConsultaProjeto(
                filtros,
                (string)ViewState["SortExpression"],
                (string)ViewState["SortDirection"], size);
            _grid.DataBind();
        }
    }
}
