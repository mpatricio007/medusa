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

namespace Medusa.Sistemas.Comum
{
    public partial class Agencias : PageCrud<BancoAgenciaBLL>
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_agencia";
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
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_agencia);
            this.cTextoNome.Text = ObjBLL.ObjEF.nome;
            this.cTextoNumero.Text = ObjBLL.ObjEF.numero;
            this.txtDigito.Text = ObjBLL.ObjEF.digito;
            this.cTextoNumConvenio.Text = ObjBLL.ObjEF.num_convenio;
            if (Convert.ToInt32(PkValue)!=0)
                this.cDdlBancos1.Id_banco = ObjBLL.ObjEF.id_banco;
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_agencia = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.nome = this.cTextoNome.Text;
            ObjBLL.ObjEF.numero = this.cTextoNumero.Text;
            ObjBLL.ObjEF.digito = this.txtDigito.Text;
            ObjBLL.ObjEF.id_banco = this.cDdlBancos1.Id_banco;
            ObjBLL.ObjEF.num_convenio = this.cTextoNumConvenio.Text;
        }

        protected void grid_SelectedIndexChanged(object sender, EventArgs e)
        {

        }




    }
}