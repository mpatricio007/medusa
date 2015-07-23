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

namespace Medusa.Sistemas.PessoaJuridica
{
    public partial class PesquisaFornecedor : PageCrud<FornecedorBLL>
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_fornecedor";
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
                SetProperties();
                GetColumnsDefault();
                ViewState["SortExpression"] = PRIMARY_KEY;
                ViewState["SortDirection"] = "ASC";


                this.ddlOptions_SelectedIndexChanged(null, null);
                _btExcluir.OnClientClick = "return confirm('confirma exclusão?')";
                _btExcluir0.OnClientClick = "return confirm('confirma exclusão?')";
            }
        }

        

        public override void VerifyRenderingInServerForm(Control control)
        {
            return;
            //base.VerifyRenderingInServerForm(control);
        }
        protected override void Get()
        {
            ObjBLL.Get(PkValue);
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_fornecedor);

            this.lbUsuario.Text = ObjBLL.ObjEF.Usuario.PessoaFisica.nome;
            this.lbUsuarioEmail.Text = ObjBLL.ObjEF.Usuario.PessoaFisica.Emails.FirstOrDefault().email.value;

            this.LbNome.Text = ObjBLL.ObjEF.nome;
            this.LbCnpj.Text = ObjBLL.ObjEF.cnpj.Value;
            this.LbCategoria.Text = ObjBLL.ObjEF.id_categoria.HasValue ? ObjBLL.ObjEF.Categoria.nome : String.Empty;
            this.LbEndereco.Text = ObjBLL.ObjEF.ender.ToString();
            this.lbRamoAtividade.Text = ObjBLL.ObjEF.id_ramo_atividade.HasValue ? ObjBLL.ObjEF.RamoAtividade.nome : String.Empty;
            this.LbNomeFantasia.Text = ObjBLL.ObjEF.nome_fantasia;
            this.LbGrupoEconomico.Text = ObjBLL.ObjEF.grupo_economico;
            this.LbEstadual.Text = ObjBLL.ObjEF.inscr_estadual;
            this.LbMunicipal.Text = ObjBLL.ObjEF.inscr_municipal;
            this.LbHomePage.Text = ObjBLL.ObjEF.home_page;
            LbHomePage.PostBackUrl = ObjBLL.ObjEF.home_page;
            this.LbEmail.Text = ObjBLL.ObjEF.email.value;
            this.LbTelefone.Text = ObjBLL.ObjEF.telefone.ToString();
            this.LbRegistroNumero.Text = ObjBLL.ObjEF.registro_numero;
            this.LbDataAlteracao.Text = Convert.ToString(ObjBLL.ObjEF.data_alteracao);
            this.LbNumero.Text = Convert.ToString(ObjBLL.ObjEF.numero_alteracao);
            this.LbCapital.Text = Convert.ToString(ObjBLL.ObjEF.capital_social);
            this.LbAno.Text = Convert.ToString(ObjBLL.ObjEF.ano_patrimonial);
            this.lbValidade.Text = Util.DateToString(ObjBLL.ObjEF.validade);
            gridRepresentante.DataSource = ObjBLL.ObjEF.RepresentantesLegais.ToList();
            gridRepresentante.DataBind();
            gridDiretor.DataSource = ObjBLL.ObjEF.Diretores.ToList();
            gridDiretor.DataBind();
            gridSocio.DataSource = ObjBLL.ObjEF.Socios.ToList();
            gridSocio.DataBind();

            ControleHistoricoFornecedor1.Id_fornecedor = Convert.ToInt32(ObjBLL.ObjEF.id_fornecedor);
            ControleHistoricoFornecedor1.DataBind();

            ControleHistoricoDocumento2.Id_fornecedor = Convert.ToInt32(ObjBLL.ObjEF.id_fornecedor);
            ControleHistoricoDocumento2.Id_categoria = Convert.ToInt32(ObjBLL.ObjEF.id_categoria);

            ControleHistoricoDocumento2.DataBind();
        }

        protected override void Set()
        {
         
        }

        protected override void SetModify()
        {
            base.SetModify();
            ControleHistoricoFornecedor1.Visible = true;
            btExportToExcel.Visible = false;

        }

        protected override void SetAdd()
        {
            base.SetAdd();
            ControleHistoricoFornecedor1.Visible = false;
            btExportToExcel.Visible = false;
        }

        protected override void SetView()
        {
            base.SetView();
            ControleHistoricoFornecedor1.Visible = false;
            btExportToExcel.Visible = true;
        }

        public void SetProperties()
        {
            _ddlOptions.Items.Clear();

            _ddlOptions.DataTextField = "DisplayName";
            _ddlOptions.DataValueField = "ValueName";

            _ddlOptions.DataSource = ObjBLL.GetProperties();
            _ddlOptions.DataBind();

            ckProperties.DataTextField = _ddlOptions.DataTextField;
            ckProperties.DataValueField = _ddlOptions.DataValueField;
            ckProperties.DataSource = _ddlOptions.DataSource;
            ckProperties.DataBind();
        }

        protected void ckProperties_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetGrid();
        }

        protected void GetColumnsDefault()
        {

            var lst = _grid.Columns.OfType<DataControlField>().Where(it => it.Visible).Select(it => it.SortExpression);
            foreach (var ck in ckProperties.Items.OfType<ListItem>())
            {
                if (lst.Contains(ck.Value))
                {
                    ck.Selected = true;
                    ck.Enabled = false;
                }
            }
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

        protected override void SetGrid()
        {
            var lstCol = _grid.Columns.CloneFields();
            _grid.Columns.Clear();

            foreach (var item in ckProperties.Items.Cast<ListItem>())
            {
                var col = lstCol.OfType<DataControlField>().Where(it => it.SortExpression == item.Value).FirstOrDefault();
                 if (col == null & item.Selected)
                {
                    var c = new TemplateField();
                    c.HeaderText = item.Text;
                    c.SortExpression = item.Value;
                    lstCol.Add(c);
                }

                else if (col != null & !item.Selected)
                {
                    lstCol.Remove(col);
                }
            }

            foreach (var item in lstCol)
            {
                if (item is TemplateField)
                {
                    var t = (TemplateField)item;
                    t.ItemTemplate = new GridViewTemplate(t.SortExpression);
                    _grid.Columns.Add(t);                    
                }
                else
                    _grid.Columns.Add((DataControlField)item);
            }

            var SortExpression = (string)ViewState["SortExpression"];
            SortExpression = SortExpression ?? PRIMARY_KEY;

            var SortDirection = (string)ViewState["SortDirection"];
            SortDirection = SortDirection ?? "ASC";

            Filter f = new Filter()
            {
                property = "StatusFornecedor.gerenciavel",
                value = Convert.ToString(true),
                mode = "=="
            };
            filtros.Add(f);

            int size = 10 * Convert.ToInt32(this._ddlSize.SelectedValue);
            _grid.DataKeyNames = new string[] { PRIMARY_KEY };
            _grid.DataSource = ObjBLL.Find(filtros, SortExpression, SortDirection, size);//.OfType<Fornecedor>().ToList().Where(it => it.StrSocios.Contains(txtProcuraSocio.Text)).ToList();
            _grid.DataBind();
            filtros.Remove(f);
        }
    }
}
