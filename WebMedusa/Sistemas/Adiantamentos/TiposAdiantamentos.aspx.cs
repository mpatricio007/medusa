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

namespace Medusa.Sistemas.Adiantamentos
{
    public partial class TiposAdiantamentos : PageCrud<TiposAdiantamentoBLL>
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_tipo_admto";
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
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_tipo_admto);
            this.cTextoNome.Text = ObjBLL.ObjEF.nome;
            this.cIntNumDias.Text = Convert.ToString(ObjBLL.ObjEF.num_dias);
            this.cIntCodPlano.Text = Convert.ToString(ObjBLL.ObjEF.codigo_plano_contas);
            this.cIntCValorMax.Value = ObjBLL.ObjEF.valor_max;
            this.rbTipoDias.SelectedValue = Convert.ToString(ObjBLL.ObjEF.business_days);
            //this.cTextoMsg.Text = ObjBLL.ObjEF.msg;
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_tipo_admto = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.nome = this.cTextoNome.Text;
            ObjBLL.ObjEF.num_dias = Convert.ToInt32(this.cIntNumDias.Text);
            ObjBLL.ObjEF.codigo_plano_contas = Convert.ToInt32(this.cIntCodPlano.Text);
            ObjBLL.ObjEF.valor_max = Convert.ToDecimal(this.cIntCValorMax.Value);
            ObjBLL.ObjEF.business_days = Convert.ToBoolean(this.rbTipoDias.SelectedValue);
            //ObjBLL.ObjEF.msg = this.cTextoMsg.Text;
        }
    }
}