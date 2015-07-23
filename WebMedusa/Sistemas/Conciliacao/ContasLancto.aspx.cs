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

namespace Medusa.Sistemas.Conciliacao
{
    public partial class ContasLancto : PageCrud<ContaBLL>
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
            _btExcluir = btExcluiLancto;
            _btExcluir0 = btExcluiLancto;
            _btProcurar = btSearch;
            _ckFilter = ckFilter;
            _dataListFiltros = DataListFiltros;
            dicProperties = ContaBLL.dicSaldoConta;
            if (!IsPostBack)
            {
                cData1.Value = DateTime.Now.AddDays(-30);
                cData2.Value = DateTime.Now;
                cDataTermino.Value = DateTime.Now;
                base.Page_Load(sender, e);            
            }
        }

        protected override void GetQueryString()
        {
            int id_conta = Convert.ToInt32(Request.QueryString["id_conta"].DesCriptografar());
            DateTime dtDe = Convert.ToDateTime(Request.QueryString["dtDe"].DesCriptografar());
            DateTime dtAte = Convert.ToDateTime(Request.QueryString["dtAte"].DesCriptografar());
            if (id_conta == 0)
            {
                int id_lcto = Convert.ToInt32(Request.QueryString["pk"].DesCriptografar());
                if (id_lcto != 0)
                {
                    
                    GetLancto(id_lcto);
                    Get();
                    SetModify();
                    SetModifyLancto();                    
                }
                else
                    SetView();
            }
            else
            {
                PkValue = id_conta;
                if ((dtDe != DateTime.MinValue) & (dtAte != DateTime.MinValue))
                {
                    cData1.Value = dtDe;
                    cData2.Value = dtAte;
                }
                Get();
                SetModify();
                SetAddLan();
            }
        }
    
        protected override void Get()
        {
            ObjBLL.Get(PkValue);
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_conta);
            this.txtConta.Text = ObjBLL.ObjEF.ToString();
            SetAddLan();
            gridLancto.DataBind();

            
        }

        protected override void Set()
        {
        }

        protected override void btCancelar_Click(object sender, EventArgs e)
        {
            GetLancto(0);
            SetAddLan();
        }


        protected void GetLancto(Int32 id_lancto)
        {
            ContaLanctoBLL clBLL = new ContaLanctoBLL();          
            clBLL.Get(id_lancto);
             
            this.txtId_lcto_conta.Text = Convert.ToString(clBLL.ObjEF.id_lcto_conta);
            if (id_lancto != 0)
            {
                PkValue = Convert.ToInt32(clBLL.ObjEF.Conta.id_conta);
                this.cDataLancto.Value = clBLL.ObjEF.data;
            }
            else
                this.cDataLancto.Value = DateTime.Now;
            this.cDdlTipoLancto1.Id_tipoLcto = clBLL.ObjEF.id_tipo_lcto;
            this.cTextoDescricao.Text = clBLL.ObjEF.descricao;
            this.cValorLancto.Value = clBLL.ObjEF.valor;
            this.cTxtNumDocumento.Text = clBLL.ObjEF.num_documento;
            this.txtObs.Text = clBLL.ObjEF.obs;
            this.cTextoProjeto.Text = clBLL.ObjEF.proj_num;
        }

        protected void SetLancto(ContaLanctoBLL clBLL)
        {
            clBLL.ObjEF.id_conta = Convert.ToInt32(PkValue);
            clBLL.ObjEF.id_lcto_conta = Convert.ToInt32(this.txtId_lcto_conta.Text);
            clBLL.ObjEF.data = (DateTime)this.cDataLancto.Value;
            clBLL.ObjEF.id_tipo_lcto = this.cDdlTipoLancto1.Id_tipoLcto;
            clBLL.ObjEF.descricao = this.cTextoDescricao.Text;
            clBLL.ObjEF.valor = Convert.ToDecimal(this.cValorLancto.Value);
            clBLL.ObjEF.num_documento = this.cTxtNumDocumento.Text;
            clBLL.ObjEF.obs = this.txtObs.Text;
            clBLL.ObjEF.proj_num = this.cTextoProjeto.Text;
        }

        protected void btSalvaPag_Click(object sender, EventArgs e)
        {
            ContaLanctoBLL clBLL = new ContaLanctoBLL();
            clBLL.Get(Convert.ToInt32(this.txtId_lcto_conta.Text));
            if (!clBLL.ContemRemessas())
            {
                SetLancto(clBLL);
                clBLL.Update();
                if (clBLL.SaveChanges())
                {
                    msg("Alteração efetuada com sucesso!");
                    GetLancto(0);
                    gridLancto.DataBind();
                    SetAddLan();

                }
                else
                    msgError("Erro ao alterar lançamento!");
            }
            else
                msgError("Lançamento não pode ser alterado, pois contém pagamentos vinculados!!!");
        }

        protected void btInserePag_Click(object sender, EventArgs e)
        {
            ContaLanctoBLL clBLL = new ContaLanctoBLL();
            SetLancto(clBLL);          
            clBLL.Add();
            if (clBLL.SaveChanges())
            {
                msg("Inclusão efetuada com sucesso!");
                GetLancto(0);
                gridLancto.DataBind();
            }
            else
                msgError("Erro ao incluir lançamento!");
        }

        protected void gridPag_DataBinding(object sender, EventArgs e)
        {
            if (Convert.ToInt32(PkValue) != 0)
            {
                panelLanctos.Visible = true;
                gridLancto.DataKeyNames = new string[] { "Id_lcto_conta" };
                ObjBLL.Get(PkValue);
                
                if (cValor1.Value.HasValue)
                    gridLancto.DataSource = ObjBLL.ExtratoPeriodo(cData1.Value.GetValueOrDefault(), cData2.Value.GetValueOrDefault()).Where(v=> v.valor == cValor1.Value);
                else
                    gridLancto.DataSource = ObjBLL.ExtratoPeriodo(cData1.Value.GetValueOrDefault(), cData2.Value.GetValueOrDefault());
            }
            else panelLanctos.Visible = false;

        }

        protected void btExcluiPag_Click(object sender, EventArgs e)
        {
            ContaLanctoBLL clBLL = new ContaLanctoBLL();
            clBLL.Get(Convert.ToInt32(this.txtId_lcto_conta.Text));
            if (!clBLL.ContemRemessas())
            {
                clBLL.Delete();
                if (clBLL.SaveChanges())
                {
                    msg("Exclusão efetuada com sucesso!");
                    GetLancto(0);
                    SetAddLan();
                    gridLancto.DataBind();
                }
                else
                    msgError("Erro ao excluir lançamento!");               
            }
            else
                msgError("Lançamento não pode ser excluido, pois contém pagamentos vinculados!!!");

        }

        protected virtual void SetAddLan()
        {
            lbMsg.Text = String.Empty;
            btAlterarLancto.Visible = false;
            btInsereLancto.Visible = true;
            btExcluiLancto.Visible = false;
            cDataLancto.Focus();
            //btConciliar.Visible = false;
        }

        protected virtual void SetModifyLancto()
        {
            lbMsg.Text = String.Empty;
            btAlterarLancto.Visible = true;
            btInsereLancto.Visible = false;
            btExcluiLancto.Visible = true;
            cDataLancto.Focus();
            //btConciliar.Visible = true;
        }

        protected override void SetAdd()
        {
            lbMsg.Text = String.Empty;
            pGrid.Visible = false;
            pCadastro.Visible = true;
            panelLanctos.Visible = false;
        }

        protected override void SetModify()
        {
            lbMsg.Text = String.Empty;
            pCadastro.Visible = true;
            pGrid.Visible = false;
            panelLanctos.Visible = true;
        }

        protected override void btProcurar_Click(object sender, EventArgs e)
        {
            base.btProcurar_Click(sender, e);
            panelLanctos.Visible = false;
        }

        protected void gridPag_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GetLancto(Convert.ToInt32(gridLancto.DataKeys[e.NewEditIndex]["Id_lcto_conta"].ToString()));
            SetModifyLancto();
            e.Cancel = true;
            txtProcura.Focus();
        }

        protected void btConciliar_Click(object sender, EventArgs e)
        {
            ContaLanctoBLL clBLL = new ContaLanctoBLL();
            if (clBLL.Conciliar(Convert.ToInt32(this.txtId_lcto_conta.Text)))
            {                
                GetLancto(0);
                SetAddLan();
                gridLancto.DataBind();
            }
        }

        protected void btFiltrar_Click(object sender, EventArgs e)
        {
            gridLancto.DataBind();
        }

        protected override void SetGrid()
        {            
            int size = 10 * Convert.ToInt32(this._ddlSize.SelectedValue);

            _grid.DataKeyNames = new string[] { PRIMARY_KEY };

            _grid.DataSource = ObjBLL.Lista_SaldoContas(
                cDataTermino.Value.GetValueOrDefault(),
                filtros,
                (string)ViewState["SortExpression"],
                (string)ViewState["SortDirection"], size);
            _grid.DataBind();
        }

        protected void gridLancto_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {            
            var panel = (Panel)gridLancto.Rows[e.NewSelectedIndex].FindControl("pRemessas");
            panel.Visible = !panel.Visible;

            var imgButton = (ImageButton)gridLancto.Rows[e.NewSelectedIndex].FindControl("imgAdd");
            imgButton.ImageUrl = panel.Visible ? "../../Shared/images/minus.png" : "../../Shared/images/plus.png";

            var gridRemessas = (GridView)gridLancto.Rows[e.NewSelectedIndex].FindControl("gridRemessas");
            ContaLanctoBLL clBLL = new ContaLanctoBLL();          
            clBLL.Get(Util.StringToInteiro(gridLancto.DataKeys[e.NewSelectedIndex]["Id_lcto_conta"].ToString()));

            if (clBLL.Exists())
                gridRemessas.DataSource = clBLL.ObjEF.Remessas.OrderBy(it => it.Projeto.codigo).ThenBy(it => it.nome_fav_cedente).ToList();
            gridRemessas.EmptyDataText = "nenhuma remessa para este lançamento";
            gridRemessas.DataBind();

            e.Cancel = true;
        }
    }
}