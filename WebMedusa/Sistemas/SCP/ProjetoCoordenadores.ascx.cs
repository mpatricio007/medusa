using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.LIB;
using Medusa.BLL;
using Medusa.DAL;

namespace Medusa.Sistemas.SCP
{

    public partial class ProjetoCoordenadores :  ControlCrud<ProjetoCoordenadoresBLL>
    {
        public int Id_projeto
        {
            get
            {
                if (ViewState[ID] == null)
                    Id_projeto = 0;
                return (int)ViewState[ID];
            }
            set { ViewState[ID] = value; }
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_proj_coord";
            //valor da chave primária
            PkValue = Convert.ToInt32(this.txtCodigo.Text);
            // painel do grid
            pGrid = panelGrid;
            // painel do formulário de alteração
            pCadastro = panelCadastro;
            // gridview
            _grid = grid;
            lbMsg = lblMsg;
            _btAlterar = new Button();
            _btAlterar0 = btAlterar0;
            _btInserir = new Button();
            _btInserir0 = btInserir0;
            _btExcluir = new Button();
            _btExcluir0 = btExcluir0;
            _ddlSize = new DropDownList();
            _ddlMode = new DropDownList();
            _ddlOptions = new DropDownList();
            _txtProcura = new TextBox();
            _btProcurar = new Button();
            _ckFilter = new CheckBox();
            _dataListFiltros = new DataList();

            if (!IsPostBack)
            {
                Security();

                ViewState["SortExpression"] = PRIMARY_KEY;
                ViewState["SortDirection"] = "ASC";
                
                SetGrid();

                _btExcluir.OnClientClick = "return confirm('confirma exclusão?')";
                _btExcluir0.OnClientClick = "return confirm('confirma exclusão?')";
            }
        }

        

        protected override void Get()
        {
            ObjBLL.Get(PkValue);
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_proj_coord);
            this.DdlCoordenadores1.Id_coordenador = ObjBLL.ObjEF.id_coordenador;
            this.cDataInicio.Value = ObjBLL.ObjEF.inicio;
            this.cDataTermino.Value = ObjBLL.ObjEF.termino;
            this.cTextoObs.Text = ObjBLL.ObjEF.observacao;
            this.cDdlTipoCoordenadores1.Value = ObjBLL.ObjEF.tipo;
            //tipo coordenador
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.tipo = this.cDdlTipoCoordenadores1.Value;
            ObjBLL.ObjEF.id_projeto = Id_projeto;
            ObjBLL.ObjEF.id_coordenador = this.DdlCoordenadores1.Id_coordenador;
            ObjBLL.ObjEF.inicio = this.cDataInicio.Value.GetValueOrDefault();
            ObjBLL.ObjEF.termino = this.cDataTermino.Value.GetValueOrDefault();
            ObjBLL.ObjEF.observacao = this.cTextoObs.Text;
        }

        protected override void SetGrid()
        {
            Filter f = new Filter()
            {
                property = "id_projeto",
                value = Convert.ToString(Id_projeto),
                mode = "=="
            };
            filtros.Add(f);

            grid.DataKeyNames = new string[] { PRIMARY_KEY };
            grid.DataSource = ObjBLL.Find(
               filtros,
               (string)ViewState["SortExpression"],
               (string)ViewState["SortDirection"], 0);
            grid.DataBind();
            filtros.Remove(f);
        }

        protected override void btInserir_Click(object sender, EventArgs e)
        {
            base.btInserir_Click(sender, e);
            SetGrid();
            SetView();
        }

        protected override void btAlterar_Click(object sender, EventArgs e)
        {
            base.btAlterar_Click(sender, e);
            SetGrid();
            SetView();
        }

        protected override void btExcluir_Click(object sender, EventArgs e)
        {
            base.btExcluir_Click(sender, e);
            SetGrid();
            SetView();
        }

        public override void DataBind()
        {
            SetGrid();
            SetView();
            base.DataBind();
        }

        protected override void btProcurar_Click(object sender, EventArgs e) 
        {
            filtros.Clear();
            filtros.Add(setFilter());
            SetGrid();
            SetView();
        }
        
        protected override Filter setFilter()
        {
            try
            {
                Filter f = new Filter();
                f.property = "Documento.nome";
                f.property_name = "Documento.nome";
                f.value = this._txtProcura.Text.ToUpper();
                f.mode = "StartsWith";
                f.mode_name = "começando com";
                return f;
            }

            catch (Exception)
            {
                return new Filter();
            }
        }        
    }
}