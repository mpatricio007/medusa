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
    public partial class TiposRetorno : PageCrud<TipoRetornoBLL> 
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_tipo_ret";
            //valor da chave primária
            PkValue = Convert.ToInt32(this.txtId_tipoRetorno.Text);
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
            this.txtId_tipoRetorno.Text = Convert.ToString(ObjBLL.ObjEF.id_tipo_ret);
            this.cTextoCodigo.Text = ObjBLL.ObjEF.codigo;
            this.cTextoDescricao.Text = ObjBLL.ObjEF.descricao;
            this.cDdlBancos1.Id_banco = ObjBLL.ObjEF.id_banco;
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_tipo_ret = Convert.ToInt32(this.txtId_tipoRetorno.Text);
            ObjBLL.ObjEF.codigo = this.cTextoCodigo.Text;
            ObjBLL.ObjEF.descricao = this.cTextoDescricao.Text;
            ObjBLL.ObjEF.id_banco = this.cDdlBancos1.Id_banco;
        }

    }
}