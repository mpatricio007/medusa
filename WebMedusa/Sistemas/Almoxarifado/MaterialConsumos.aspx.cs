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
using Medusa.Relatorios.Almoxarifado;
using Medusa.Relatorios;

namespace Medusa.Sistemas.Almoxarifado
{
    public partial class MaterialConsumos : PageCrud<MaterialConsumoBLL>
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_material";
            //valor da chave primária
            PkValue = Convert.ToInt32(this.txtCodigo.Text);
            // painel do grid
            pGrid = panelGrid;
            // painel do formulário de alteração
            pCadastro = panelCadastro;
            // gridview
            _grid = grid;
            lbMsg = lblMsg;
            _btAlterar = btAlterar;
            _btAlterar0 = btAlterar0;
            _btInserir = btInserir;
            _btInserir0 = btInserir0;
            _btExcluir = btExcluir;
            _btExcluir0 = btExcluir0;
            _ddlSize = ddlSize;
            _ddlMode = ddlMode;
            _ddlOptions = ddlOptions;
            _txtProcura = txtProcura;
            _btProcurar = btSearch;
            _ckFilter = ckFilter;
            _dataListFiltros = DataListFiltros;

            if (!IsPostBack)
            {
                SetView();
                base.Page_Load(sender, e);
            }
        }

        protected override void Get()
        {
            ObjBLL.Get(PkValue);
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_material);
            this.cTextoDescricao.Text = ObjBLL.ObjEF.descricao;
            this.cIntQtdeMinima.Value = ObjBLL.ObjEF.qtde_minima;
            this.lbQtdeTotal.Text = Convert.ToString(ObjBLL.ObjEF.Total);
            this.cDdlUnidadeMedida1.Id_unidade_medida = ObjBLL.ObjEF.id_unidade_medida;
            this.cValor.Value = ObjBLL.ObjEF.valor;
            this.cDdlTipoMateriais.Id_tipo_material = ObjBLL.ObjEF.id_tipo_material;
            //this.cDdlNotasFiscais.Id_nf_material = Convert.ToInt32(ObjBLL.ObjEF.id_nf_material);
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_material = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.descricao = this.cTextoDescricao.Text;
            ObjBLL.ObjEF.qtde_minima = this.cIntQtdeMinima.Value.GetValueOrDefault();
            ObjBLL.ObjEF.id_unidade_medida = this.cDdlUnidadeMedida1.Id_unidade_medida;
            ObjBLL.ObjEF.valor = Convert.ToDecimal(this.cValor.Value);
            ObjBLL.ObjEF.id_tipo_material = this.cDdlTipoMateriais.Id_tipo_material;
            //ObjBLL.ObjEF.id_nf_material = cDdlNotasFiscais.Id_nf_material==0 ? new Nullable<int>() : this.cDdlNotasFiscais.Id_nf_material.GetValueOrDefault();
        }

        protected void btItensDeficientes_Click(object sender, EventArgs e)
        {
            ReportViewer ReportViewer1 = new ReportViewer();

            SetPrint();
            //ReportViewer1.Visible = true;
            ReportViewer1.LocalReport.ReportPath = @"Relatorios\Almoxarifado\RelatorioMaterialDeficientes.rdlc";
            var r = new RelatorioMaterial();
            ReportDataSource rpd = new ReportDataSource("dsMaterial", r.GerarRelatorio(ObjBLL.GetMateriaisDeficientes()));
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rpd);
            ReportViewer1.LocalReport.Refresh();

            Session[PageOfReport.SessionName] = ReportViewer1;
            dRelatorio.InnerHtml = PageOfReport.iframe;
            dRelatorio.DataBind();
        }

        protected void SetPrint()
        {
            pGrid.Visible = false;
            pCadastro.Visible = false;
            dRelatorio.Visible = true;
        }

        protected override void SetAdd()
        {
            dRelatorio.Visible = false;
            base.SetAdd();
        }

        protected override void SetModify()
        {
            dRelatorio.Visible = false;
            base.SetModify();
        }

        protected override void SetView()
        {
            dRelatorio.Visible = false;
            base.SetView();
        }

        protected void btImprimir_Click(object sender, ImageClickEventArgs e)
        {
            ReportViewer ReportViewer1 = new ReportViewer();
            SetPrint();
            Filter f = new Filter()
            {
                property = "material",
                value = this.cTextoDescricao.Text,
                mode = "=="
            };
            filtros.Add(f);

            ReportViewer1.LocalReport.ReportPath = @"Relatorios\Almoxarifado\RelatorioMateriais.rdlc";

            //ReportViewer1.Visible = true;
            var r = new RelatorioMaterial();
            ReportDataSource rpd = new ReportDataSource("dsMaterial", r.GerarRelatorio(ObjBLL.Find(
                filtros,
                (string)ViewState["SortExpression"],
                (string)ViewState["SortDirection"], 0).OfType<MaterialConsumo>()));
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rpd);
            ReportViewer1.LocalReport.Refresh();
            filtros.Remove(f);

            Session[PageOfReport.SessionName] = ReportViewer1;
            dRelatorio.InnerHtml = PageOfReport.iframe;
            dRelatorio.DataBind();
        }

        protected void btExportToExcel_Click(object sender, ImageClickEventArgs e)
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

            gridToExport.DataSource = ObjBLL.Find(
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

    }
}