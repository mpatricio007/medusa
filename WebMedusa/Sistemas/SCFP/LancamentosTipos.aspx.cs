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
    public partial class LancamentosTipos : PageCrud<LancamentoTipoBLL>
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_lancto_tipo";
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
            }
        }

        protected override void Get()
        {
            ObjBLL.Get(PkValue);
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_lancto_tipo);
            this.cTextoNome.Text = ObjBLL.ObjEF.nome;
            this.ckStatus.Checked = ObjBLL.ObjEF.status;
            SetGridPlanosContas();
            cDdlPlanoConta1.Id_plano_conta = 0;
            cDdlPlanoConta1.DataBind();
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_lancto_tipo = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.nome = this.cTextoNome.Text;
            ObjBLL.ObjEF.status = this.ckStatus.Checked;
        }

        protected override void SetAdd()
        {
            base.SetAdd();
            trPlanoContas.Visible = false;
        }

        protected override void SetModify()
        {
            base.SetModify();
            trPlanoContas.Visible = true;
        }

        protected void btAddLanctoTipo_Click(object sender, EventArgs e)
        {
            lblMsg.Text = String.Empty;
            ObjBLL.Get(PkValue);
            if (ObjBLL.DataIsValid(cDdlPlanoConta1.Id_plano_conta))
            {
                PlanoContaTipoLancamentoBLL pc = new PlanoContaTipoLancamentoBLL();
                pc.ObjEF.id_plano_conta = cDdlPlanoConta1.Id_plano_conta;
                pc.ObjEF.id_lancto_tipo = ObjBLL.ObjEF.id_lancto_tipo;
                pc.Add();
                pc.SaveChanges();
                ObjBLL.Detach();
                ObjBLL.Get(PkValue);
                SetGridPlanosContas();
            }
            else
                msgError("plano de conta já possui este tipo de lançamento");
        }

        private void SetGridPlanosContas()
        {
            gridPlanosContas.DataSource = ObjBLL.ObjEF.PlanoContaTipoLanctos;
            gridPlanosContas.DataBind();
        }

        protected void gridPlanosContas_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            PlanoContaTipoLancamentoBLL pc = new PlanoContaTipoLancamentoBLL();
            pc.Get(Convert.ToInt32(gridPlanosContas.DataKeys[e.RowIndex]["id_lanTipo_Pc"]));
            pc.Delete();
            pc.SaveChanges();
            ObjBLL.Get(PkValue);
            SetGridPlanosContas();
        }

        protected override void btInserir_Click(object sender, EventArgs e)
        {
            Set();
            if (ObjBLL.DataIsValid())
            {
                ObjBLL.Add();
                if (ObjBLL.SaveChanges())
                {
                    msg("inclusão efetuada");
                    PkValue = ObjBLL.ObjEF.id_lancto_tipo;
                    Get();
                    SetModify();
                }
                else
                    msgError("erro inclusão");
            }
            else
                msgError("erro inclusão");
        }
    }
}