using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.BLL;
using Medusa.LIB;
using Medusa.DAL;

namespace Medusa.Controles
{
    public partial class ControleContratoPagamento : ControlCrud<ContratoPagamentoBLL>
    {
        public int Id_contrato
        {
            get
            {
                if (ViewState[ID] == null)
                    Id_contrato = 0;
                return (int)ViewState[ID];
            }
            set
            {
                ViewState[ID] = value;
            }
        }

        protected override void Page_Load(object sender, EventArgs e)
        {

            // chave primária da tabela
            PRIMARY_KEY = "id_contrato_pagamento";
            //valor da chave primária
            PkValue = Convert.ToInt32(this.txtCodigoContrato.Text);
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
            this.txtCodigoContrato.Text = Convert.ToString(ObjBLL.ObjEF.id_contrato_pagamento);
            this.cInteiroParcela.Value = ObjBLL.ObjEF.num_parcela;
            this.cDataVencimento.Value = ObjBLL.ObjEF.data_vencimento;
            this.cDataPagamento.Value = ObjBLL.ObjEF.data_pagamento;
            this.cValor.Value = ObjBLL.ObjEF.valor;
            this.cTextoObs.Text = ObjBLL.ObjEF.observacao;
            this.RbCancelado.SelectedValue = Convert.ToString(ObjBLL.ObjEF.cancelado);
        }

        protected override void  Set()
        {
            ObjBLL.ObjEF.id_contrato_pagamento = Convert.ToInt32(this.txtCodigoContrato.Text);
            ObjBLL.ObjEF.id_contrato = Id_contrato;
            ObjBLL.ObjEF.num_parcela = Convert.ToInt32(this.cInteiroParcela.Value);
            ObjBLL.ObjEF.data_vencimento = Convert.ToDateTime(this.cDataVencimento.Value);
            ObjBLL.ObjEF.data_pagamento = Convert.ToDateTime(this.cDataPagamento.Value);
            ObjBLL.ObjEF.valor = Convert.ToDecimal(this.cValor.Value);
            ObjBLL.ObjEF.observacao = this.cTextoObs.Text;
            ObjBLL.ObjEF.cancelado = Convert.ToBoolean(this.RbCancelado.SelectedValue);
        }

        protected override void SetGrid()
        {
            Filter f = new Filter()
            {
                property = "id_contrato",
                value = Convert.ToString(Id_contrato),
                mode = "=="
            };
            filtros.Add(f);
            base.SetGrid();
            filtros.Remove(f);
        }

        public override void DataBind()
        {
            SetView();
            SetGrid();
            base.DataBind();
        } 
    }
}