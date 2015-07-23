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

namespace Medusa.Sistemas.SCFP
{
    public partial class PlanoContas : PageCrud<PlanoContaBLL>
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_plano_conta";
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
                base.Page_Load(sender, e);
                rbClassificacao.DataSource = Enum.GetNames(typeof(ClassificacaoPC));
                rbClassificacao.DataBind();
                rbCredito.DataSource = rbDebito.DataSource = Enum.GetNames(typeof(EnumPlanoConta));
                rbCredito.DataBind();
                rbDebito.DataBind();
            }
        }

        protected override void Get()
        {
            ObjBLL.Get(PkValue);
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_plano_conta);
            cTextoCodigo.Text = ObjBLL.ObjEF.codigo;
            this.cTextoClasse.Text = ObjBLL.ObjEF.classe;
            this.cTextoItem.Text = ObjBLL.ObjEF.item;
            cTextoSubItem.Text = ObjBLL.ObjEF.sub_item;
            cTextoContaContabil.Text = ObjBLL.ObjEF.conta_contabil;
            rbClassificacao.SelectedValue = ObjBLL.ObjEF.Classificacao == 0 ? Convert.ToString(ClassificacaoPC.outros) : Convert.ToString(ObjBLL.ObjEF.Classificacao);
            rbCredito.SelectedValue = ObjBLL.ObjEF.intCredito.GetValueOrDefault() == 0 ? Convert.ToString(EnumPlanoConta.SEM_LANCTO) : Convert.ToString(ObjBLL.ObjEF.credito);
            rbDebito.SelectedValue = ObjBLL.ObjEF.intDebito.GetValueOrDefault() == 0 ? Convert.ToString(EnumPlanoConta.SEM_LANCTO) : Convert.ToString(ObjBLL.ObjEF.debito);
            cDdlProjeto1.Id_projeto = ObjBLL.ObjEF.id_projeto_destino.GetValueOrDefault();
            //cDdlProjeto1.DataBind();
            SetGridLanctoTipos();
            cDdlLancamentoTipo1.Id_lancto_tipo = 0;
            cDdlLancamentoTipo1.DataBind();

            //cDdlTipoDespesas1.Id_tipo_despessa = ObjBLL.ObjEF.id_tipo_despesa.GetValueOrDefault();

            //if (ObjBLL.ObjEF.classificacao != 0)
            //    rbClassificacao.SelectedValue = Convert.ToString(ObjBLL.ObjEF.classificacao);
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_plano_conta = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.codigo = cTextoCodigo.Text;
            ObjBLL.ObjEF.classe = this.cTextoClasse.Text;
            ObjBLL.ObjEF.item = this.cTextoItem.Text;
            ObjBLL.ObjEF.sub_item = this.cTextoSubItem.Text;
            ObjBLL.ObjEF.conta_contabil = this.cTextoContaContabil.Text;
            ObjBLL.ObjEF.Classificacao = (ClassificacaoPC)Enum.Parse(typeof(ClassificacaoPC), rbClassificacao.SelectedValue);
            //ObjBLL.ObjEF.id_tipo_despesa = cDdlTipoDespesas1.Id_tipo_despessa;

            ObjBLL.ObjEF.credito = (EnumPlanoConta)Enum.Parse(typeof(EnumPlanoConta), rbCredito.SelectedValue);
            ObjBLL.ObjEF.debito = (EnumPlanoConta)Enum.Parse(typeof(EnumPlanoConta), rbDebito.SelectedValue);
            if(cDdlProjeto1.Id_projeto != 0)
                ObjBLL.ObjEF.id_projeto_destino = cDdlProjeto1.Id_projeto;
        }

        protected override void SetAdd()
        {
            base.SetAdd();
            rbClassificacao.DataBind();
            trLanctoTipos.Visible = false;
        }

        protected override void SetModify()
        {
            base.SetModify();
            rbClassificacao.DataBind();
            trLanctoTipos.Visible = true;
        }

        protected void SetGridLanctoTipos()
        {
            gridLanctoTipos.DataSource = ObjBLL.ObjEF.PlanoContaTipoLanctos.ToList();
            gridLanctoTipos.DataBind();
        }

        protected void gridLanctoTipos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ObjBLL.Get(PkValue);
            PlanoContaTipoLancamentoBLL pc = new PlanoContaTipoLancamentoBLL();
            pc.Get(Convert.ToInt32(gridLanctoTipos.DataKeys[e.RowIndex]["id_lanTipo_Pc"]));
            pc.Delete();
            pc.SaveChanges();
            SetGridLanctoTipos();
        }

        protected void btAddLanctoTipo_Click(object sender, EventArgs e)
        {
            lblMsg.Text = String.Empty;
            ObjBLL.Get(PkValue);
            if (ObjBLL.DataIsValid(cDdlLancamentoTipo1.Id_lancto_tipo))
            {
                PlanoContaTipoLancamentoBLL pc = new PlanoContaTipoLancamentoBLL();
                pc.ObjEF.id_lancto_tipo = cDdlLancamentoTipo1.Id_lancto_tipo;
                pc.ObjEF.id_plano_conta = ObjBLL.ObjEF.id_plano_conta;
                pc.Add();
                pc.SaveChanges();
                ObjBLL.Detach();
                ObjBLL.Get(PkValue);
                SetGridLanctoTipos();
            }
            else
                msgError("plano de conta já possui este tipo de lançamento");
        }
    }
}