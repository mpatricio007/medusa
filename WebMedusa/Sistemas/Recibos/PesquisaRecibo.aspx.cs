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
using Medusa.Relatorios.Recibos;
using Microsoft.Reporting.WebForms;

namespace Medusa.Sistemas.Recibos
{
    public partial class PesquisaRecibo : PageCrud<ReciboBLL>
    {
        Dictionary<string, string> dicSearch = new Dictionary<string, string>()
        {
            {"curso","id_recibo_curso"},
        };

        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_recibo";
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
                var recBLL = new ReciboCursoBLL();
                listaCurso.DataSource = recBLL.GetAll("nome");       
                listaCurso.Items.Insert(0, new ListItem("selecione um curso...", "0"));

                listaCurso.DataBind();
                base.Page_Load(sender, e);
                _btExcluir.OnClientClick = String.Empty;
                _btExcluir0.OnClientClick = String.Empty;
            }
        }

        protected override void Get()
        {
            var vUsuDemonstrativo = new vUsuariosDemonstrativoBLL();
            ObjBLL.Get(PkValue);
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_recibo);
            this.txtUsuario.Text = ObjBLL.ObjEF.Usuario.nome;
            this.txtData.Text = Convert.ToString(ObjBLL.ObjEF.data);
            this.txtNome.Text = ObjBLL.ObjEF.nome;
            this.txtDescricao.Text = ObjBLL.ObjEF.descricao;
            //this.txtTipoInscricao.Text = ObjBLL.ObjEF.tipo.ToString();
            this.txtValor.Text = Convert.ToString(ObjBLL.ObjEF.valor);
            this.txtMotivo.Text = ObjBLL.ObjEF.motivo_cancel;
            this.txtObs.Text = ObjBLL.ObjEF.observacao;
            this.txtCurso.Text = ObjBLL.ObjEF.ReciboCurso.nome;
            this.txtTipoPagamento.Text = ObjBLL.ObjEF.tipo_pagamento;
            txtCpf.Text = Convert.ToString(ObjBLL.ObjEF.cpf);
            txtCnpj.Text = Convert.ToString(ObjBLL.ObjEF.cnpj);
            dMotivo.Visible = ObjBLL.Exists();
            gridCheque.DataSource = ObjBLL.ObjEF.ReciboCheques.ToList();
            gridCheque.DataBind();
        }

        protected override void Set()
        {
       
        }

        protected void btImprimirRecibo_Click(object sender, ImageClickEventArgs e)
        {
            Util.NovaJanela(String.Format("../../Sistemas/Recibos/Report.aspx?pk={0}", PkValue.ToString().Criptografar()));
        }

        protected override void SetGrid()
        {
            int id_recibo_curso = 0;

            if (!_ckFilter.Checked)
                filtros.Clear();

            switch (ddlOptions.SelectedValue)
            {
                case ("curso"):
                    {
                        int.TryParse(listaCurso.SelectedValue, out id_recibo_curso);
                        break;
                    }
                default:
                    {
                        if (!String.IsNullOrEmpty(this._txtProcura.Text))
                            filtros.Add(setFilter());
                        break;
                    }
            }
            SetView();

            if (_ddlSize.SelectedValue == "0")
                _grid.PageSize = 50;
            else
                _grid.PageSize = Convert.ToInt32(_ddlSize.SelectedValue);


            int size = 10 * Convert.ToInt32(this._ddlSize.SelectedValue);
            _grid.DataKeyNames = new string[] { PRIMARY_KEY };
            _grid.DataSource = ObjBLL.FindConsulta(
                filtros,
                (string)ViewState["SortExpression"],
                (string)ViewState["SortDirection"], size,
                id_recibo_curso);

            _grid.DataBind();
            DataListFiltros.DataBind();
            this._txtProcura.Text = String.Empty;
        }

        protected override void ddlOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddlOptions.SelectedValue)
            {
                case ("curso"):
                    {
                        listaCurso.SelectedValue = "0";
                        GetModes(typeof(string));
                        listaCurso.Visible = true;
                        ddlMode.Visible = txtProcura.Visible = false;
                        break;
                    }
                default:
                    {
                        listaCurso.SelectedValue = "0";
                        ddlMode.Visible = txtProcura.Visible = true;
                        listaCurso.Visible = false;
                        base.ddlOptions_SelectedIndexChanged(sender, e);
                        break;
                    }
            }
        }
    }
}