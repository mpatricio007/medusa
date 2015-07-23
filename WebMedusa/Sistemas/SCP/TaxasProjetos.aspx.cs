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

namespace Medusa.Sistemas.SCP
{
    public partial class TaxasProjetos : PageCrud<TaxaProjetoBLL>
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_taxa";
            //valor da chave primária
            PkValue = Convert.ToInt32(this.cInteiro1.Text);
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
            this.cInteiro1.Text = Convert.ToString(ObjBLL.ObjEF.id_taxa);
            this.cTextoNome.Text = ObjBLL.ObjEF.nome;
            cValorTaxa.Value = ObjBLL.ObjEF.taxa;
            cDdlProjeto1.Id_projeto = ObjBLL.ObjEF.id_projeto;
            cTextoDebCtb.Text = ObjBLL.ObjEF.deb_ctb;
            cTextoCredCtb.Text = ObjBLL.ObjEF.cred_ctb;
            cDdlPlanoConta1.Id_plano_conta = ObjBLL.ObjEF.id_plano_conta;
            cDataInicio.Value = ObjBLL.ObjEF.data_inicio;

        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_taxa = Convert.ToInt32(this.cInteiro1.Text);
            ObjBLL.ObjEF.nome = this.cTextoNome.Text;
            ObjBLL.ObjEF.taxa = cValorTaxa.Value;
            ObjBLL.ObjEF.id_projeto = cDdlProjeto1.Id_projeto;
            ObjBLL.ObjEF.deb_ctb = cTextoDebCtb.Text;
            ObjBLL.ObjEF.cred_ctb = cTextoCredCtb.Text;
            ObjBLL.ObjEF.id_plano_conta = cDdlPlanoConta1.Id_plano_conta;
            ObjBLL.ObjEF.data_inicio = cDataInicio.Value;

        }
    }
}