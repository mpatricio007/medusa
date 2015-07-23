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

namespace Medusa.Sistemas.PessoaJuridica
{
    public partial class CategoriaDocumentos : PageCrud<CategoriaDocumentoBLL>
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_cat_doc";
            //valor da chave primária
            PkValue = Convert.ToInt32(txtCodigo.Text);
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
                var dcBLL = new DocumentosCategoriaBLL();
                lista.Items.Clear();
                lista.DataSource = dcBLL.GetAll("nome");
                lista.Items.Insert(0, new ListItem("selecione um documento...", "0"));
                lista.DataBind();
                base.Page_Load(sender, e);
            }
        }

        protected override void Get()
        {
            ObjBLL.Get(PkValue);
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_cat_doc);
            this.ddlCategorias.Id_categoria = ObjBLL.ObjEF.id_categoria;
            this.lista.SelectedValue = Convert.ToString(ObjBLL.ObjEF.id_documento);
            this.ckStatus.Checked = ObjBLL.ObjEF.status; 
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_cat_doc = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.id_categoria = this.ddlCategorias.Id_categoria;
            ObjBLL.ObjEF.id_documento = Convert.ToInt32(this.lista.SelectedValue);
            ObjBLL.ObjEF.status = this.ckStatus.Checked;
        }
    }
}