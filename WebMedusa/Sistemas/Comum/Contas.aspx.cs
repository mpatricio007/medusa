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
    public partial class Contas : PageCrud<ContaBLL>
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
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_conta);
            this.cDdlAgencias1.Id_agencia = ObjBLL.ObjEF.id_agencia;
            this.cTextoNumero.Text = ObjBLL.ObjEF.numero;
            this.txtDigito.Text = ObjBLL.ObjEF.digito;
            this.cDdlTipoConta1.Id_tipoConta = ObjBLL.ObjEF.id_tipoconta;
            this.cDdlProjeto1.Id_projeto = ObjBLL.ObjEF.id_projeto.GetValueOrDefault();
            this.cValorSaldo.Text = Convert.ToString(ObjBLL.ObjEF.saldo_inicial);
            this.cDataSaldo.Value = ObjBLL.ObjEF.data_saldo_inicial;
            if (ObjBLL.ObjEF.status!=null)
                this.rbStatus.SelectedValue = Convert.ToString(ObjBLL.ObjEF.status);
            this.cDataAbertura.Value = ObjBLL.ObjEF.data_abertura;

        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_conta = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.id_agencia = Convert.ToInt32(this.cDdlAgencias1.Id_agencia);
            ObjBLL.ObjEF.numero = this.cTextoNumero.Text;
            ObjBLL.ObjEF.digito = this.txtDigito.Text.ToUpper();
            ObjBLL.ObjEF.id_tipoconta = this.cDdlTipoConta1.Id_tipoConta;

            int? id_projeto = Util.IntToInteiroOrNullable(this.cDdlProjeto1.Id_projeto);

            ObjBLL.ObjEF.id_projeto = id_projeto;      
            
            ObjBLL.ObjEF.saldo_inicial = this.cValorSaldo.Value;
            
            ObjBLL.ObjEF.data_saldo_inicial = this.cDataSaldo.Value;
            ObjBLL.ObjEF.status = Convert.ToBoolean(this.rbStatus.SelectedValue);
            ObjBLL.ObjEF.data_abertura = this.cDataAbertura.Value;
        }

    }
}