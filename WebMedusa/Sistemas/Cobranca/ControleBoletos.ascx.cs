using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.LIB;
using Medusa.BLL;
using Medusa.DAL;
using BoletoNet;

namespace Medusa.Sistemas.SCP
{
    public partial class ControleBoletos : ControlCrud<BoletoCobrancaBLL>
    {
        
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_boleto";
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
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_boleto);
            this.cValor.Value = ObjBLL.ObjEF.valor;
            this.cDataVencto.Value = ObjBLL.ObjEF.data_vencto;
            this.cDataPagamento.Value = ObjBLL.ObjEF.data_pgto;
            this.cValorPago.Value = ObjBLL.ObjEF.valor_pgto;
            this.cTextoObs.Text = ObjBLL.ObjEF.obs;
            this.cInteiroParcela.Value = ObjBLL.ObjEF.num_parcela;
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_boleto = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.valor = this.cValor.Value.GetValueOrDefault();
            ObjBLL.ObjEF.data_vencto = this.cDataVencto.Value.GetValueOrDefault();
            ObjBLL.ObjEF.data_pgto =  this.cDataPagamento.Value;
            ObjBLL.ObjEF.valor_pgto = this.cValorPago.Value;
            ObjBLL.ObjEF.obs = this.cTextoObs.Text;
            ObjBLL.ObjEF.id_evento_sacado = Convert.ToInt32(this.txtId_evento_sacado.Text);
            ObjBLL.ObjEF.num_parcela = this.cInteiroParcela.Value.GetValueOrDefault();
        }

        protected override void SetGrid()
        {
            var id_evento_sacado = Convert.ToInt32(this.txtId_evento_sacado.Text);
            if (id_evento_sacado != 0)
            {
                var evsBLL = new EventoSacadoBLL();
                evsBLL.Get(id_evento_sacado);
                this.txtSacado.Text = evsBLL.ObjEF.Sacado.PessoaFisica.nome;
                this.txtId_evento_sacado.Text = Convert.ToString(evsBLL.ObjEF.id_evento_sacado);

                Filter f = new Filter()
                {
                    property = "id_evento_sacado",
                    value = Convert.ToString(evsBLL.ObjEF.id_evento_sacado),
                    mode = "=="
                };
                filtros.Add(f);
                base.SetGrid();
                filtros.Remove(f);
            }
        }

        public void setEventoSacado(int id_evento_sacado)
        {
            this.txtId_evento_sacado.Text = Convert.ToString(id_evento_sacado);
            SetView();
            SetGrid();
        }

        protected void grid_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            Util.NovaJanela(String.Format("Boletos.aspx?pk={0}", grid.DataKeys[e.NewSelectedIndex]["id_boleto"].ToString().Criptografar()));
            e.Cancel = true;
        }

        protected void btEnviarEmail_Click(object sender, ImageClickEventArgs e)
        {

            var id_evento_sacado = Convert.ToInt32(this.txtId_evento_sacado.Text);
            try
            {
                string saida;
                Filter f = new Filter()
                {
                    property = "id_evento_sacado",
                    value = Convert.ToString(id_evento_sacado),
                    mode = "=="
                };
                filtros.Add(f);
                ObjBLL.EnviarEmail(ObjBLL.Find(
                filtros,
                (string)ViewState["SortExpression"],
                (string)ViewState["SortDirection"], 0).OfType<BoletoCobranca>().ToList(),
                    out saida);
                filtros.Remove(f);                
                Medusa.LIB.Util.ShowMessage(saida);
            }
            catch (Exception)
            {

                Medusa.LIB.Util.ShowMessage("Erro!");
            }

            
        }
    }
}