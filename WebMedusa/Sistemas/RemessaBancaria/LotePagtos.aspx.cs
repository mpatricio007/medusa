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


namespace Medusa.Sistemas.RemessaBancaria
{
    public partial class LotePagtos : PageCrud<LotePagBBBLL>
    {
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
                base.Page_Load(sender, e);

                btExcluirPagamento.OnClientClick = "return confirm('confirma o estorno do pagamento?')";
                btExcluirPagamento0.OnClientClick = "return confirm('confirma o estorno do pagamento?')";

                rbDocumento.DataSource = Enum.GetNames(typeof(TipoInscricao));
                rbDocumento.DataBind();

                BancoBLL b = new BancoBLL();
                listaBanco.DataSource = b.GetAll("codigo");
                listaBanco.Items.Insert(0, new ListItem("selecione um banco...", "0"));
                listaBanco.DataBind();
            }
        }

        protected override void SetAdd()
        {
            tabs.ActiveTabIndex = 0;
            tbPagamentos.Visible = false;
            lbMsgPagamento.Text = String.Empty;
            tabs_OnActiveTabChanged(null, null);

            base.SetAdd();
        }

        protected override void SetModify()
        {
            tabs.ActiveTabIndex = 0;
            tbPagamentos.Visible = true;
            lbMsgPagamento.Text = String.Empty;
            tabs_OnActiveTabChanged(null, null);

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
            this.cTextoDescricaoLote.Text = ObjBLL.ObjEF.descricao;

            GetPagamento(0);

            //if (ObjBLL.TemContaEspecifica())
            //{
            //    this.cDdlProjeto1.Id_projeto = ObjBLL.ObjEF.Conta.id_projeto.GetValueOrDefault();
            //    this.cDdlProjeto1.Enabled = false;
            //}

            //else
            //{
            //    this.cDdlProjeto1.Id_projeto = 0;
            //    this.cDdlProjeto1.Enabled = true;
            //}

            this.cDdlProjeto1.Id_projeto = 0;
            this.cDdlProjeto1.DataSource = ObjBLL.GetAllProjetos();

            SetGridPagamentos();
            SetAddPagamentos();
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_lote = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.data_pgto = this.cDataPagto.Value;
            ObjBLL.ObjEF.id_conta = this.cPesqConta1.Id_Conta;
            ObjBLL.ObjEF.descricao = this.cTextoDescricaoLote.Text;
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

        protected void SetPagamento(RemessaPAG rem)
        {
            rem.id_remessa = Convert.ToInt32(this.txtId_pagamento.Text);
            rem.id_lote = Convert.ToInt32(PkValue);
            rem.nome_fav_cedente = this.cTextoFavorecido.Text;
            rem.valor = Util.StringToDecimal(txtValor.Text).GetValueOrDefault();         
            rem.id_projeto = this.cDdlProjeto1.Id_projeto;
            rem.descricao = this.cTextoDescricao.Text;
            rem.id_banco = Util.StringToInteiro(listaBanco.SelectedValue).GetValueOrDefault();
            rem.tipoInscr = (TipoInscricao)Enum.Parse(typeof(TipoInscricao), rbDocumento.SelectedValue);
            rem.inscricao = rem.tipoInscr == TipoInscricao.CPF ? cCPF1.Value.Value : cCNPJ1.Value.Value;
            rem.agencia = this.cTextoAgencia.Text;
            rem.digito_agencia = this.cTextoDigitoAgencia.Text;
            rem.conta = this.cTextoConta.Text;
            rem.digito_conta = this.cTextoDigitoConta.Text;
            rem.id_forma_pagto = this.cDdlFormaPagto1.Id_forma_pagto;
        }   

        protected void GetPagamento(int intId_remessa)
        {

            RemessaPAGBLL remBLL = new RemessaPAGBLL();
            remBLL.Get(intId_remessa);
            this.cTextoFavorecido.Text = remBLL.ObjEF.nome_fav_cedente;
            this.txtId_pagamento.Text = Convert.ToString(remBLL.ObjEF.id_remessa);
            this.txtValor.Text = Util.DecimalToString(remBLL.ObjEF.valor);
            rbDocumento.SelectedValue = Convert.ToString(remBLL.ObjEF.tipoInscr);
            cCPF1.Value = remBLL.ObjEF.cpf;
            cCNPJ1.Value = remBLL.ObjEF.cnpj;

            trExibeMotivo.Visible = remBLL.EstaRejeitada();
            dGravacao3.Visible = dGravacao2.Visible = !remBLL.EstaRejeitada();
            lblMotivo.Text = remBLL.ObjEF.motivo_rejeicao;     

            rbDocumento_SelectedIndexChanged(null, null);
            rbRejeicao_SelectedIndexChanged(null, null);

            if (this.cDdlProjeto1.Enabled)
                this.cDdlProjeto1.Id_projeto = remBLL.ObjEF.id_projeto;

            this.cTextoDescricao.Text = remBLL.ObjEF.descricao;            
            cDdlTiposRetorno1.Id_tipo_ret = remBLL.ObjEF.id_tipo_ret;

            

            if (!remBLL.Exists())
            {
                this.listaBanco.SelectedValue = Util.InteiroToString(0);
                this.cTextoAgencia.Text = String.Empty;
                this.cTextoDigitoAgencia.Text = String.Empty;
                this.cTextoConta.Text = String.Empty;
                this.cTextoDigitoConta.Text = String.Empty;
            }
            else
                GetDadosBancarios(remBLL.ObjEF);


            GetFormapagto();
            this.cDdlFormaPagto1.Id_forma_pagto = remBLL.ObjEF.id_forma_pagto;

        }

        protected void btInserirPagamento_Click(object sender, EventArgs e)
        {

            string strMsg = String.Empty;
            var rem = new RemessaPAGBLL();
            SetPagamento(rem.ObjEF);
           
                if (ObjBLL.AgendarPagamentoNoLote(rem, ref strMsg))
                {
                    GetPagamento(0);
                    SetGridPagamentos();
                    cTextoFavorecido.Focus();
                    msgPagto(strMsg);
                }
                else
                    msgPagtoError(strMsg);
             

        }

        protected void btAlterarPagamento_Click(object sender, EventArgs e)
        {

            string strMsg = String.Empty;
            var rem = new RemessaPAGBLL();
            rem.Get(Convert.ToInt32(this.txtId_pagamento.Text));
            SetPagamento(rem.ObjEF);
            
            if (ObjBLL.AlterarPagamentoNoLote(rem, ref strMsg))
            {
                GetPagamento(0);
                SetGridPagamentos();
                cTextoFavorecido.Focus();
                msgPagto(strMsg);
            }
            else msgPagtoError(strMsg);
        }

        protected void btExcluirPagamento_Click(object sender, EventArgs e)
        {
            string msg = String.Empty;
            var rem = new RemessaPAGBLL();
            rem.Get(Convert.ToInt32(this.txtId_pagamento.Text));
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
            cTextoFavorecido.Focus();
            btDialogRejeitado.Visible = btDialogRejeitar0.Visible = false;
            
        }

        protected void SetModifyPagamentos()
        {
           
            lbMsgPagamento.Text = String.Empty;
            btInserirPagamento.Visible = false;
            btInserirPagamento0.Visible = false;
            btExcluirPagamento.Visible = true;
            btExcluirPagamento0.Visible = true;
            btAlterarPagamento.Visible = true;
            btAlterarPagamento0.Visible = true;
            btDialogRejeitado.Visible = btDialogRejeitar0.Visible = true;

        }

        protected void SetGridPagamentos()
        {
            ObjBLL.Get(PkValue);
            gridPagamentos.DataKeyNames = new string[] { "id_remessa" };
            gridPagamentos.DataSource = ObjBLL.ObjEF.Remessas.Where(it => it.nome_fav_cedente.ToUpper().Contains(this.txtNomeFavorecido.Text.ToUpper()) ||
                it.inscricao.Contains(this.txtNomeFavorecido.Text)).ToList();
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

        protected void cCPF1_OnTextChanged(object sender, EventArgs e)
        {
            if (cCPF1.IsValid)
            {
                var remPag = new RemessaPAGBLL();
                remPag.GetAllPorInsc(cCPF1.Value.Value);
                GetDadosBancarios(remPag.ObjEF);
                this.cTextoFavorecido.Focus();
            }
            else
                cCPF1.Focus();
        }

        protected void cCNPJ1_OnTextChanged(object sender, EventArgs e)
        {
            if (cCNPJ1.IsValid)
            {
                var remPag = new RemessaPAGBLL();
                remPag.GetAllPorInsc(cCNPJ1.Value.Value);
                GetDadosBancarios(remPag.ObjEF);
                this.cTextoFavorecido.Focus();
            }
            else
                cCNPJ1.Focus();
        }

        protected void GetDadosBancarios(RemessaPAG rem)
        {
            if (rem.id_remessa != 0)
            {
                this.cTextoFavorecido.Text = rem.nome_fav_cedente;
                this.listaBanco.SelectedValue = Util.InteiroToString(rem.id_banco);
                this.cTextoAgencia.Text = rem.agencia;
                this.cTextoDigitoAgencia.Text = rem.digito_agencia;
                this.cTextoConta.Text = rem.conta;
                this.cTextoDigitoConta.Text = rem.digito_conta;                            

            }
        }

        protected void rbDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            //var tipo = (TipoInscricao)Enum.Parse(typeof(TipoInscricao), rbDocumento.SelectedValue);
            //pCNPJ.Visible = tipo == TipoInscricao.CNPJ;
            //pCNPJ.DataBind();
            //pCPF.Visible = !pCNPJ.Visible;
            //pCPF.DataBind();
            //rbDocumento.Focus();
            
            var tipo = (TipoInscricao)Enum.Parse(typeof(TipoInscricao), rbDocumento.SelectedValue);
            if (tipo == TipoInscricao.CNPJ)
            {
                pCNPJ.Visible = true;
                pCPF.Visible = false;
                cCNPJ1.Focus();
            }
            else
            {
                pCPF.Visible = true;
                pCNPJ.Visible = false;
                cCPF1.Focus();
            }

            pCPF.DataBind();
            pCNPJ.DataBind();
        }

        protected void listaBancos_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetFormapagto();
            //ScriptManager1.SetFocus(cTextoAgencia.ClientID);
            cTextoAgencia.Focus();
        }

        protected void cValor1_TextChanged(object sender, EventArgs e)
        {
            GetFormapagto();
            cDdlProjeto1.Focus();
        }

        public void GetFormapagto()
        {
            ObjBLL.Get(PkValue);
            if (ObjBLL.Exists())
            {
                cDdlFormaPagto1.Id_banco_favorecido = Util.StringToInteiro(listaBanco.SelectedValue).GetValueOrDefault();
                cDdlFormaPagto1.Id_banco_lote = ObjBLL.ObjEF.Conta.BancoAgencia.id_banco;
                cDdlFormaPagto1.Valor = Util.StringToDecimal(txtValor.Text).GetValueOrDefault();
            }
            cDdlFormaPagto1.DataBind();
        }

        protected void btRejeitar_Click(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(rbRejeicao.SelectedValue))
            {
                string msg = String.Empty;
                var rem = new RemessaPAGBLL();
                rem.Get(Convert.ToInt32(this.txtId_pagamento.Text));
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

        protected void tabs_OnActiveTabChanged(object sender, EventArgs e)
        {
            if (tabs.ActiveTabIndex == 1)
                rbDocumento_SelectedIndexChanged(null, null);
            else
                cPesqConta1.Focus();
        }
    }
}
