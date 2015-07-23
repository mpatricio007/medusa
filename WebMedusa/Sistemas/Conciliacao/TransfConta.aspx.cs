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
    public partial class TransfConta : PageCrud<ContaTransfBLL>
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_transf";
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
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_transf);
            this.cTextoDescricao.Text = ObjBLL.ObjEF.descricao;

            if (ObjBLL.ObjEF.id_transf != 0)
                this.cData1.Value = ObjBLL.ObjEF.data;
            else
                this.cData1.Value = DateTime.Now;            

            this.DdlContasCredito.Id_Conta = ObjBLL.ObjEF.id_conta_credito;
            this.DdlContasDebito.Id_Conta = ObjBLL.ObjEF.id_conta_debito;
            
            this.cValor1.Value = ObjBLL.ObjEF.valor;
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_transf = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.descricao = this.cTextoDescricao.Text;
            ObjBLL.ObjEF.data = this.cData1.Value.GetValueOrDefault();
            ObjBLL.ObjEF.id_conta_credito = this.DdlContasCredito.Id_Conta;
            ObjBLL.ObjEF.id_conta_debito = this.DdlContasDebito.Id_Conta;
            ObjBLL.ObjEF.valor = this.cValor1.Value.GetValueOrDefault();
        }

        protected override void btExcluir_Click(object sender, EventArgs e)
        {
            if (cvContas.IsValid)
            {
                ObjBLL.Get(PkValue);
                if (ObjBLL.ObjEF.HasLactoConciliado)
                    msgError("exclusão cancelada! Há lançamentos vinculados a esta transferência que já foram conciliados");
                else
                    base.btExcluir_Click(sender, e);
            }
        }

        protected override void btAlterar_Click(object sender, EventArgs e)
        {
            if (cvContas.IsValid)
            {
                ObjBLL.Get(PkValue);
                if (ObjBLL.ObjEF.HasLactoConciliado)
                    msgError("alteração cancelada! Há lançamentos vinculados a esta transferência que já foram conciliados");
                else
                    base.btAlterar_Click(sender, e);
            }
        }

        protected void cvContas_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = this.DdlContasCredito.Id_Conta != this.DdlContasDebito.Id_Conta;
        }

        protected override void btInserir_Click(object sender, EventArgs e)
        {
             if (cvContas.IsValid)
                 base.btInserir_Click(sender, e);
        }

     }
}