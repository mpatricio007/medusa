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

namespace Medusa.Sistemas.Bolsa
{
    public partial class BolsaVigencia : PageCrud<BolsaVigenciaBLL>
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_bolsa_vigencia";
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
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_bolsa_vigencia);
            this.cDataInicio.Text = Util.DateToString(ObjBLL.ObjEF.inicio);
            this.cDataTermino.Text = Util.DateToString(ObjBLL.ObjEF.termino);
            this.cDdlBolsas1.Id_bolsa = ObjBLL.ObjEF.id_bolsa;
            this.cValor.Text = Convert.ToString(ObjBLL.ObjEF.valor);
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_bolsa_vigencia = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.inicio = this.cDataInicio.Value.GetValueOrDefault();
            ObjBLL.ObjEF.termino = this.cDataTermino.Value.GetValueOrDefault();
            ObjBLL.ObjEF.id_bolsa = this.cDdlBolsas1.Id_bolsa;
            ObjBLL.ObjEF.valor = Convert.ToDecimal(this.cValor.Text);
        }

        protected void grid_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}