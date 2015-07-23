using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.LIB;
using Medusa.BLL;

namespace Medusa.Sistemas.Conciliacao
{
    public partial class ResgateConta : PageCrud<ContaBLL>
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_conta";
            //valor da chave primária
            PkValue = Convert.ToInt32(this.txtCodigo.Text);
            // painel do grid
            pGrid = panelGrid;
            // painel do formulário de alteração
            pCadastro = panelCadastro;
            // gridview
            _grid = grid;
            lbMsg = lblMsg;
            _ddlSize = ddlSize;
            _ddlMode = ddlMode;
            _ddlOptions = ddlOptions;
            _txtProcura = txtProcura;
            _btExcluir = btExcluiAplicacao;
            _btExcluir0 = btExcluiAplicacao;
            _btProcurar = btSearch;
            _ckFilter = ckFilter;
            _dataListFiltros = DataListFiltros;

            if (!IsPostBack)
            {
                TipoLctoBLL tlBLL = new TipoLctoBLL();
                rdTipo.DataTextField = "descricao";
                rdTipo.DataValueField = "id_tipo_lcto";
                rdTipo.DataSource = tlBLL.Find(it => it.id_tipo_lcto == 5 || it.id_tipo_lcto == 7);
                rdTipo.DataBind();
                cDataTermino.Value = DateTime.Now;
                base.Page_Load(sender, e);
            }
        }

        protected override void Get()
        {
            ObjBLL.Get(PkValue);
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_conta);
            this.txtConta.Text = ObjBLL.ObjEF.ToString();
            SetAddResgate();
            gridResgates.DataBind();

            decimal saldo = ObjBLL.GetSaldoNaData(cDataTermino.Value.GetValueOrDefault());
            cDataAplicacao.Value = cDataTermino.Value;
            cValorAplicacao.Value =  saldo > 0 ? saldo : saldo * (-1);
            this.rdTipo.SelectedValue = saldo < 0 ? "7" : "5";

        }

        protected override void Set()
        {
        }

        protected override void btCancelar_Click(object sender, EventArgs e)
        {
            GetResgate(0);
            SetAddResgate();
        }

        protected void GetResgate(Int32 id_aplicacao)
        {
            ContaAplicacaoBLL caBLL = new ContaAplicacaoBLL();
            caBLL.Get(id_aplicacao);
            this.txtId_aplicacao.Text = Convert.ToString(caBLL.ObjEF.id_aplicacao);
            if (id_aplicacao != 0)
                this.cDataAplicacao.Value = caBLL.ObjEF.data;
            else
                this.cDataAplicacao.Value = DateTime.Now;
            this.cValorAplicacao.Value = caBLL.ObjEF.valor;
            this.cTextoDescricao.Text = caBLL.ObjEF.descricao;
            this.txtCodigo.Text = Convert.ToString(caBLL.ObjEF.id_conta);          
            if(caBLL.ObjEF.id_tipo_lcto != 0)
                this.rdTipo.SelectedValue = Convert.ToString(caBLL.ObjEF.id_tipo_lcto);
        }

        protected void SetResgate(ContaAplicacaoBLL caBLL)
        {
            caBLL.ObjEF.id_conta = Convert.ToInt32(PkValue);
            caBLL.ObjEF.id_aplicacao = Convert.ToInt32(this.txtId_aplicacao.Text);
            caBLL.ObjEF.data = this.cDataAplicacao.Value.GetValueOrDefault();
            caBLL.ObjEF.id_tipo_lcto = Convert.ToInt32(this.rdTipo.SelectedValue);
            caBLL.ObjEF.descricao = this.cTextoDescricao.Text;
            caBLL.ObjEF.valor = Convert.ToDecimal(this.cValorAplicacao.Value);             
        }

        protected void btSalvaAplicacao_Click(object sender, EventArgs e)
        {
            ContaAplicacaoBLL caBLL = new ContaAplicacaoBLL();
            caBLL.Get(Convert.ToInt32(this.txtId_aplicacao.Text));
            if (!caBLL.ObjEF.ContaLancto.conciliado)
            {
                SetResgate(caBLL);
                caBLL.Update();
                caBLL.SaveChanges();
                GetResgate(0);
                gridResgates.DataBind();
                SetAddResgate();
            }
        }

        protected void btInsereAplicacao_Click(object sender, EventArgs e)
        {
            ContaAplicacaoBLL caBLL = new ContaAplicacaoBLL();
            SetResgate(caBLL);          
            caBLL.Add();
            caBLL.SaveChanges();
            GetResgate(0);
            gridResgates.DataBind();
        }

      
        protected void btExcluiAplicacao_Click(object sender, EventArgs e)
        {
            ContaAplicacaoBLL clBLL = new ContaAplicacaoBLL();
            clBLL.Get(Convert.ToInt32(this.txtId_aplicacao.Text));
            if (!clBLL.ObjEF.ContaLancto.conciliado)
            {
                clBLL.Delete();
                if (clBLL.SaveChanges())
                    msg("exclusão efetuada");
                else
                    msgError("erro exclusão");
                GetResgate(0);
                SetAddResgate();
                gridResgates.DataBind();
            }
        }

        protected virtual void SetAddResgate()
        {
            lbMsg.Text = String.Empty;
            btAlterarAplicacao.Visible = false;
            btInsereAplicacao.Visible = true;
            btExcluiAplicacao.Visible = false;            
        }

        protected virtual void SetModifyResgate()
        {
            lbMsg.Text = String.Empty;
            btAlterarAplicacao.Visible = true;
            btInsereAplicacao.Visible = false;
            btExcluiAplicacao.Visible = true;            
        }

        protected override void SetAdd()
        {
            lbMsg.Text = String.Empty;
            pGrid.Visible = false;
            pCadastro.Visible = true;
            panelResgates.Visible = false;
        }

        protected override void SetModify()
        {
            lbMsg.Text = String.Empty;
            pCadastro.Visible = true;
            pGrid.Visible = false;
            panelResgates.Visible = true;
        }

        protected override void btProcurar_Click(object sender, EventArgs e)
        {
            base.btProcurar_Click(sender, e);
            panelResgates.Visible = false;
        }
  
        protected void btFiltrar_Click(object sender, EventArgs e)
        {
            gridResgates.DataBind();
        }

        protected void gridResgates_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GetResgate(Convert.ToInt32(gridResgates.DataKeys[e.NewEditIndex]["id_aplicacao"].ToString()));
            SetModifyResgate();
            e.Cancel = true;
        }

        protected void gridResgates_DataBinding(object sender, EventArgs e)
        {

            if (Convert.ToInt32(PkValue) != 0)
            {
                panelResgates.Visible = true;
                gridResgates.DataKeyNames = new string[] { "id_aplicacao" };          
                gridResgates.DataSource = ObjBLL.GetAplicacoesPeriodo(cData1.Value.GetValueOrDefault(),cData2.Value.GetValueOrDefault());
            }
            else panelResgates.Visible = false;
        }

        protected override void SetGrid()
        {
            int size = 10 * Convert.ToInt32(this._ddlSize.SelectedValue);

            _grid.DataKeyNames = new string[] { PRIMARY_KEY };
            
            _grid.Caption = String.Format("Escolha a Conta Corrente para Resgate na data {0}", cDataTermino.Text);
            _grid.DataSource = ObjBLL.Lista_SaldoContas(
                cDataTermino.Value.GetValueOrDefault(),
                filtros,
                (string)ViewState["SortExpression"],
                (string)ViewState["SortDirection"], size);
            _grid.DataBind();
        }

    
    }
}