using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.BLL;
using Medusa.DAL;
using Medusa.LIB;
using System.Text;

namespace Medusa.Sistemas.SCFP.ControlesSCFP
{
    public partial class ControleTabelaTaxas : ControlCrud<TabelaTaxasBLL>
    {
        public int Id_taxa
        {
            get
            {
                if (ViewState[ID] == null)
                    Id_taxa = 0;
                return (int)ViewState[ID];
            }
            set
            {
                ViewState[ID] = value;
            }
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_tabela";
            //valor da chave primária
            PkValue = Convert.ToInt32(this.txtCodigo.Text);
            // painel do grid
            pGrid = new Panel(); ;
            // painel do formulário de alteração
            pCadastro = panelCadastro;
            // gridview
            _grid = new GridView();
            lbMsg = lblMsg;
            _btAlterar = new Button();
            _btAlterar0 = new Button();
            _btInserir = btInserir;
            _btInserir0 = new Button();
            _btExcluir = new Button();
            _btExcluir0 = new Button();
            _ddlSize = new DropDownList();
            _ddlMode = new DropDownList();
            _ddlOptions = new DropDownList();
            _txtProcura = new TextBox();
            _btProcurar = new Button();
            _ckFilter = new CheckBox();
            _dataListFiltros = new DataList();

            if (!IsPostBack)
            {
                //base.Page_Load(sender, e);
                ViewState["SortExpression"] = PRIMARY_KEY;
                ViewState["SortDirection"] = "DESC";

                SetGrid();
                //this.ddlOptions_SelectedIndexChanged(null, null);
                _btExcluir.OnClientClick = "return confirm('confirma exclusão?')";
                _btExcluir0.OnClientClick = "return confirm('confirma exclusão?')";
                GetQueryString();
            }
        }

        protected override void SetView()
        {
            base.SetView();
            panelCadastro.Visible = true;
        }
        protected override void Get()
        {
            ObjBLL.Get(PkValue);
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_tabela);
            this.cDataInicio.Value = ObjBLL.ObjEF.data_ini;
            this.cDataFim.Value = ObjBLL.ObjEF.data_fim;
            this.ckCumulativoMensal.Checked = ObjBLL.ObjEF.cumulativo_mensal;
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_tabela = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.data_ini = this.cDataInicio.Value.GetValueOrDefault();
            ObjBLL.ObjEF.data_fim = this.cDataFim.Value.GetValueOrDefault();
            ObjBLL.ObjEF.cumulativo_mensal = this.ckCumulativoMensal.Checked;
        }

        protected FaixaTaxas SetFaixaTaxa()
        {
            FaixaTaxas ft = new FaixaTaxas();
            ft.faixa_de = this.cValorDe.Value.GetValueOrDefault();
            ft.faixa_ate = this.cValorAte.Value.GetValueOrDefault();
            ft.valor_max = this.cValorMax.Value;
            ft.vlr_minimo = this.cValorMin.Value.GetValueOrDefault();
            ft.aliquota = this.cValorAliquota.Value.GetValueOrDefault();
            ft.deducao = this.cValorDeducao.Value.GetValueOrDefault();
            return ft;
        }

        protected override void SetGrid()
        {
        }

        protected void gridFaixas_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
    }
}