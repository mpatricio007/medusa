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
using System.Text;
using Medusa.Controles;
using System.Collections;

namespace Medusa.Sistemas.RemessaBancaria
{
    public partial class LoteGru : PageCrud<LoteGRUBLL>
    {
        public delegate void TextChangedEventHandler(object sender, System.EventArgs e);
        public event TextChangedEventHandler TextChanged;

        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_lote";
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
            //RemoveShowAlert();

            if (!IsPostBack)
            {
                btExcluirPagamento.OnClientClick = "return confirm('confirma o estorno da guia?')";
                btExcluirPagamento0.OnClientClick = "return confirm('confirma o estorno da guia?')";
                base.Page_Load(sender, e);
            }
        }

        protected override void SetAdd()
        {
            tabs.ActiveTabIndex = 0;
            tbPagamentos.Visible = false;
            lbMsgPagamento.Text = String.Empty;
            tabs_ActiveTabChanged(null, null);
            base.SetAdd();
        }

        protected override void SetModify()
        {
            tabs.ActiveTabIndex = 0;
            tbPagamentos.Visible = true;
            lbMsgPagamento.Text = String.Empty;
            tabs_ActiveTabChanged(null, null);
            base.SetModify();
        }

        protected override void Get()
        {
            ObjBLL.Get(PkValue);
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_lote);
            this.lbLote.Text = Convert.ToString(ObjBLL.ObjEF.id_lote);
            this.cDataPagto.Value = ObjBLL.ObjEF.data_pgto;
            this.cDataProcessamento.Value = ObjBLL.ObjEF.data_processamento;
            this.cDataEnvio.Value = ObjBLL.ObjEF.data_envio;
            this.cPesqConta1.Id_Conta = ObjBLL.ObjEF.id_conta;

            GetPagamento(0);

            this.cDdlProjeto.Id_projeto = 0;
            this.cDdlProjeto.DataSource = ObjBLL.GetAllProjetos();
            SetGridPagamentos();
            SetAddPagamentos();
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_lote = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.data_pgto = this.cDataPagto.Value;
            ObjBLL.ObjEF.id_conta = this.cPesqConta1.Id_Conta;
        }

        protected override void btInserir_Click(object sender, EventArgs e)
        {
            Set();

            string strMsg = String.Empty;
            if (ObjBLL.DataIsValid(ref strMsg))
            {
                ObjBLL.Add();
                if (ObjBLL.SaveChanges())
                {
                    msg("inclusão efetuada");
                    PkValue = ObjBLL.ObjEF.id_lote;
                    ObjBLL.Detach();
                    Get();
                    SetModify();

                }
                else
                    msgError("erro alteração");
            }
            else
                msgError(strMsg);
        }

        protected override void btAlterar_Click(object sender, EventArgs e)
        {

            ObjBLL.Get(PkValue);
            Set();
            string strMsg = String.Empty;
            if (ObjBLL.DataIsValid(ref strMsg))
            {
                ObjBLL.Update();
                if (ObjBLL.SaveChanges())
                    msg("alteração efetuada");
                else
                    msgError("erro alteração");
            }
            else
                msgError(strMsg);
        }

        protected override void btExcluir_Click(object sender, EventArgs e)
        {
            ObjBLL.Get(PkValue);
            string strMsg = String.Empty;
            if (ObjBLL.DataIsValid(ref strMsg))
            {
                ObjBLL.Delete();
                if (ObjBLL.SaveChanges())
                {
                    msg("exclusão efetuada");
                    Get();
                    SetAdd();
                }
                else
                    msgError("erro alteração");
            }
            else
                msgError(strMsg);
        }

        protected void SetPagamento(RemessaGru rem)
        {
            rem.id_remessa = Convert.ToInt32(this.txtId_gru.Text);
            rem.id_lote = Convert.ToInt32(PkValue);
            rem.nome_fav_cedente = this.cTextoCedente.Text;
            rem.valor_gru = cValorPrincipal.Value.GetValueOrDefault();
            rem.id_projeto = this.cDdlProjeto.Id_projeto;
            rem.descricao = this.cTextoDescricao.Text;
            rem.data_vencto = this.cDataVencto.Value.GetValueOrDefault();
            rem.cod_recolhimento = this.cTextoCodRec.Text;
            rem.num_referencia = this.cIntNumRef.Value.GetValueOrDefault();
            rem.id_contribuinte = this.cTextoIdentificador.Text;
            rem.ug_gestao = this.cTextoUgGestao.Text;
            rem.desc_abatimento = this.cDescAbatimento.Value.GetValueOrDefault();
            rem.outras_deducoes = this.cOutrasDeducoes.Value.GetValueOrDefault();
            rem.mora_multa = this.cMoraMulta.Value.GetValueOrDefault();
            rem.juros_encargos = this.cJurosEncargos.Value.GetValueOrDefault();
            rem.outros_acrescimos = this.cOutrosAcrescimos.Value.GetValueOrDefault();
            rem.Guia = new CodigoBarrasGru(this.txtBoleto.Text);
            rem.valor = rem.valor_gru + rem.desc_abatimento + rem.outras_deducoes + rem.mora_multa + rem.juros_encargos + rem.outros_acrescimos;
            rem.mes_ano = string.Format("{0:00}", this.cDdlMes.Value) + this.cDdlAno.Value.ToString();
        }

        protected void GetPagamento(int intId_remessa)
        {
            RemessaGruBLL remBLL = new RemessaGruBLL();
            remBLL.Get(intId_remessa);
            this.cTextoCedente.Text = remBLL.ObjEF.nome_fav_cedente;
            this.txtId_gru.Text = Convert.ToString(remBLL.ObjEF.id_remessa);
            this.cTextoCedente.Text = remBLL.ObjEF.nome_fav_cedente;
            this.cValorPrincipal.Value = remBLL.ObjEF.valor_gru;
            trExibeMotivo.Visible = remBLL.EstaRejeitada();
            dGravacao3.Visible = dGravacao2.Visible = !remBLL.EstaRejeitada();
            lblMotivo.Text = remBLL.ObjEF.motivo_rejeicao;

            if (this.cDdlProjeto.Enabled)
                this.cDdlProjeto.Id_projeto = remBLL.ObjEF.id_projeto;

            this.cTextoDescricao.Text = remBLL.ObjEF.descricao;
            this.cDataVencto.Value = remBLL.ObjEF.data_vencto;
            cDdlTiposRetorno1.Id_tipo_ret = remBLL.ObjEF.id_tipo_ret;
            this.cTextoIdentificador.Text = remBLL.ObjEF.id_contribuinte;
            this.cTextoCodRec.Text = remBLL.ObjEF.cod_recolhimento;
            this.cIntNumRef.Value = remBLL.ObjEF.num_referencia;
            this.cTextoUgGestao.Text = remBLL.ObjEF.ug_gestao;
            this.cDescAbatimento.Value = remBLL.ObjEF.desc_abatimento;
            this.cOutrasDeducoes.Value = remBLL.ObjEF.outras_deducoes;
            this.cMoraMulta.Value = remBLL.ObjEF.mora_multa;
            this.cJurosEncargos.Value = remBLL.ObjEF.juros_encargos;
            this.txtBoleto.Text = remBLL.ObjEF.Guia.RepresentacaoNumerica;
            this.cOutrosAcrescimos.Value = remBLL.ObjEF.outros_acrescimos;
            if (intId_remessa != 0)
            {
                this.cDdlMes.Value = Convert.ToInt16(remBLL.ObjEF.mes_ano.Substring(0, 2));
                this.cDdlAno.Value = Convert.ToInt32(remBLL.ObjEF.mes_ano.Substring(2, 4));
            }
            rbRejeicao_SelectedIndexChanged(null, null);
            txtBoleto.Focus();

        }

        protected void btInserirPagamento_Click(object sender, EventArgs e)
        {
            string strMsg = String.Empty;
            var rem = new RemessaGruBLL();
            SetPagamento(rem.ObjEF);
            if (ObjBLL.AgendarPagamentoNoLote(rem, ref strMsg))
            {
                GetPagamento(0);
                SetGridPagamentos();
                txtBoleto.Focus();
                msgPagto(strMsg);
            }
            else
            {
                msgPagtoError(strMsg);
            }

        }

        protected void btAlterarPagamento_Click(object sender, EventArgs e)
        {

            string strMsg = String.Empty;
            var rem = new RemessaGruBLL();
            rem.Get(Convert.ToInt32(this.txtId_gru.Text));
            SetPagamento(rem.ObjEF);

            if (ObjBLL.AlterarPagamentoNoLote(rem, ref strMsg))
            {
                GetPagamento(0);
                SetGridPagamentos();
                txtBoleto.Focus();
                msgPagto(strMsg);
            }
            else
            {
                msgPagtoError(strMsg);
            }

        }

        protected void btExcluirPagamento_Click(object sender, EventArgs e)
        {
            string msg = String.Empty;
            var rem = new RemessaGruBLL();
            rem.Get(Convert.ToInt32(this.txtId_gru.Text));
            if (ObjBLL.EstornarPagamentoNoLote(rem, ref msg))
            {
                GetPagamento(0);
                SetAddPagamentos();
                SetGridPagamentos();
                msgPagto(msg);
            }
            else msgPagtoError(msg);
        }

        protected void btCancelarPagamento_Click(object sender, EventArgs e)
        {
            GetPagamento(0);
            SetAddPagamentos();
            SetGridPagamentos();
        }

        protected void btFiltrar_Click(object sender, EventArgs e)
        {
            SetGridPagamentos();
        }

        protected void gridPagamentos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GetPagamento(Convert.ToInt32(gridPagamentos.DataKeys[e.NewEditIndex]["id_remessa"]));
            e.Cancel = true;
            SetGridPagamentos();
            SetModifyPagamentos();
        }

        protected void SetAddPagamentos()
        {
            lbMsgPagamento.Text = String.Empty;
            tabs.ActiveTabIndex = 1;
            btInserirPagamento.Visible = true;
            btInserirPagamento0.Visible = true;
            btExcluirPagamento.Visible = false;
            btExcluirPagamento0.Visible = false;
            btAlterarPagamento.Visible = false;
            btAlterarPagamento0.Visible = false;
            txtBoleto.Focus();
            txtBoleto.Enabled = true;
            btDialogRejeitado.Visible = btDialogRejeitar0.Visible = false;
            tabs_ActiveTabChanged(null, null);
        }

        protected void SetModifyPagamentos()
        {
            txtBoleto.Enabled = false;
            lbMsgPagamento.Text = String.Empty;
            btInserirPagamento.Visible = false;
            btInserirPagamento0.Visible = false;
            btExcluirPagamento.Visible = true;
            btExcluirPagamento0.Visible = true;
            btAlterarPagamento.Visible = true;
            btAlterarPagamento0.Visible = true;
            btDialogRejeitado.Visible = btDialogRejeitar0.Visible = true;
            tabs_ActiveTabChanged(null, null);
        }

        protected void SetGridPagamentos()
        {
            ObjBLL.Get(PkValue);
            gridPagamentos.DataKeyNames = new string[] { "id_remessa" };
            gridPagamentos.DataSource = ObjBLL.ObjEF.Guias.Where(it => it.nome_fav_cedente.Contains(this.txtNomeFavorecido.Text)).ToList();
            gridPagamentos.DataBind();
        }

        protected void msgPagto(string msg)
        {
            lbMsgPagamento.BackColor = System.Drawing.Color.Green;
            lbMsgPagamento.ForeColor = System.Drawing.Color.White;
            lbMsgPagamento.Text = string.Format("* {0} !", msg);
        }

        protected void msgPagtoError(string msg)
        {
            lbMsgPagamento.BackColor = System.Drawing.Color.Red;
            lbMsgPagamento.ForeColor = System.Drawing.Color.White;
            lbMsgPagamento.Text = string.Format("* {0} !?", msg);
        }

        protected void btRejeitar_Click(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(rbRejeicao.SelectedValue))
            {
                string msg = String.Empty;
                var rem = new RemessaGruBLL();
                rem.Get(Convert.ToInt32(this.txtId_gru.Text));
                if (rem.Rejeitar(cTextoMotivo.Text, ref msg))
                {
                    SetGridPagamentos();
                    GetPagamento(rem.ObjEF.id_remessa);
                    msgPagto(msg);
                }
                else
                    msgPagtoError(msg);
            }
            Util.ChamarScript("closeRejeicaoDialog();", "");
        }

        protected void rbRejeicao_SelectedIndexChanged(object sender, EventArgs e)
        {
            trMotivo.Visible = Convert.ToBoolean(rbRejeicao.SelectedValue);
        }

        protected void tabs_ActiveTabChanged(object sender, EventArgs e)
        {
            if (tabs.ActiveTabIndex == 1)
                txtBoleto.Focus();
            else
                cPesqConta1.Focus();
        }

        protected void txtBoleto_TextChanged(object sender, EventArgs e)
        {
            var txt = (TextBox)sender;
            cvBoleto_ServerValidate(null, null);
            //this.txtBoleto.Focus();
            if (cvBoleto.IsValid)
                return;
        }

        protected void cvBoleto_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string msg = cvBoleto.ErrorMessage;
            cvBoleto.IsValid = validaBoleto(out msg);
            cvBoleto.ErrorMessage = msg;

        }

        protected bool validaBoleto(out string msg)
        {
            var rt = false;
            msg = String.Empty;
            var rem = new RemessaGruBLL();
            var guia = new CodigoBarrasGru(this.txtBoleto.Text);
            rem.ObjEF.Guia = guia;
            rem.ObjEF.id_remessa = Convert.ToInt32(PkValue);
            if (rem.ObjEF.Guia.ValidaCodBarra())
            {
                rem.ObjEF.valor = guia.Valor();
                this.cTextoCedente.Focus();
                //this.txtBoleto.Focus();
                rt = true;
            }
            else
            {
                msg = "Guia inválida!";
                //this.txtBoleto.Text = String.Empty;
                this.txtBoleto.Focus();
            }
            return rt;
        }
    }
}