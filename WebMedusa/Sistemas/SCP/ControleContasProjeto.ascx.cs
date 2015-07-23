using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.LIB;
using Medusa.BLL;
using Medusa.DAL;
using BoletoNet;

namespace Medusa.Sistemas.SCP
{
    public partial class ControleContasProjeto : ControlCrud<ContasProjetoBLL>
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
            PRIMARY_KEY = "id_conta_projeto";
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
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_conta_projeto);
            this.cDdlConta1.Id_Conta = ObjBLL.ObjEF.id_conta;
            ckContaPagadora.Checked = ObjBLL.ObjEF.conta_pagadora;
            //tabs.ActiveTabIndex = 0;
            //if (ObjBLL.Exists())
            //{
            //    cLog1.PermitePesquisa = false;
            //    cLog1.Id_entidade = ObjBLL.ObjEF.id_conta_projeto;
            //    cLog1.TEntidade = ObjBLL.ObjEF.GetType();
            //    cLog1.DataBind();
            //}
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_conta_projeto =Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.id_conta = this.cDdlConta1.Id_Conta;
            ObjBLL.ObjEF.id_projeto = Id_projeto;
            ObjBLL.ObjEF.conta_pagadora = ckContaPagadora.Checked;
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

        public override void DataBind()
        {
            base.DataBind();
            SetView();
            SetGrid();
        }

        protected override void SetAdd()
        {
            //tbLog.Visible = false;
            base.SetAdd();
            btCriar.Visible = false;
        }

        protected override void SetModify()
        {
            //tbLog.Visible = true;
            base.SetModify();
            btCriar.Visible = false;
        }

        protected override void SetView()
        {
            base.SetView();
            btCriar.Visible = true;
        }

        //protected void tabs_ActiveTabChanged(object sender, EventArgs e)
        //{
        //    if (tabs.ActiveTabIndex == 1)
        //        cLog1.DataBind();
        //}
    }
}