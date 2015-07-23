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

namespace Medusa.Sistemas.Adiantamentos
{
    public partial class Adiantamentos : PageCrud<AdiantamentoBLL>
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_adiantamento";
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
            _btExcluir = new Button();
            _btExcluir0 = new Button();
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
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_adiantamento);
            this.cData.Value = ObjBLL.ObjEF.data;
            this.cDataVencto.Value = ObjBLL.ObjEF.data_vencimento;
            this.cValor.Value = ObjBLL.ObjEF.valor;
            this.cDdlProjeto1.Id_projeto = ObjBLL.ObjEF.id_projeto;
            this.cTextoDescricao.Text = ObjBLL.ObjEF.descricao;
            this.cTextoObs.Text = ObjBLL.ObjEF.obs;
            this.cDataPagamento.Value = ObjBLL.ObjEF.data_pagamento;
            this.cDataRd.Value = ObjBLL.ObjEF.data_rd;
            this.cValorTotal.Value = ObjBLL.ObjEF.total_rd;
            this.lbTotalDevRec.Text = Util.DecimalToString(ObjBLL.ObjEF.valor - ObjBLL.ObjEF.total_rd.GetValueOrDefault());
            this.cTextoDescRd.Text = ObjBLL.ObjEF.descricao_rd;
            this.cTextoRp.Text = ObjBLL.ObjEF.rp;

            if (ObjBLL.Exists())
            {
                GetDestAndCopias();
                SetGridHistEmails();
            }
            
            this.cDataVencto.Enabled = btEnviarEmail.Visible = ObjBLL.Exists();
            cDdlBeneficiario1.Id_beneficiario = ObjBLL.ObjEF.id_beneficiario.GetValueOrDefault();
            ControleHistoricoAdiantamento1.Id_adiantamento = ObjBLL.ObjEF.id_adiantamento;
            ControleHistoricoAdiantamento1.DataBind();

            dPrazo.Visible = btRelatorio.Visible = ObjBLL.Exists();
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_adiantamento = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.data = this.cData.Value.GetValueOrDefault();
            ObjBLL.ObjEF.valor = this.cValor.Value.GetValueOrDefault();
            ObjBLL.ObjEF.id_projeto = this.cDdlProjeto1.Id_projeto;
            ObjBLL.ObjEF.descricao = this.cTextoDescricao.Text;
            ObjBLL.ObjEF.obs = this.cTextoObs.Text;
            ObjBLL.ObjEF.id_beneficiario = cDdlBeneficiario1.Id_beneficiario;
            ObjBLL.ObjEF.data_pagamento = this.cDataPagamento.Value;
            ObjBLL.ObjEF.data_vencimento = this.cDataVencto.Value;
            ObjBLL.ObjEF.id_tipo_admto = ObjBLL.Tipo;
            ObjBLL.ObjEF.descricao_rd = cTextoDescRd.Text;
            ObjBLL.ObjEF.rp = cTextoRp.Text;
            ObjBLL.ObjEF.total_rd = cValorTotal.Value;
            ObjBLL.ObjEF.data_rd = cDataRd.Value;
        }

        public void GetDestAndCopias()
        {
            listaEmailpadrao.DataSource = ObjBLL.GetEmailsPadraoThisType();
            listaEmailpadrao.DataBind();
            if(listaEmailpadrao.Items.Count > 0)
                lista_SelectedIndexChanged(null, null);

            gridEmailCoord.DataSource = ObjBLL.ObjEF.Projeto.Coordenadores.Where(it => it.tipo == TipoCoordenador.coordenador);
            gridEmailCoord.DataBind();

            gridCopy.DataSource = ObjBLL.ObjEF.Projeto.Contatos.Where(it => it.Notificacoes.Where(it2 => it2.id_sol_de_proj == SolicitacaoDeProjetoBLL.adiantamento).Count() > 0);
            gridCopy.DataBind();
        }

        protected override void SetAdd()
        {
            base.SetAdd();
            lbMsg.Text = String.Empty;
            ControleHistoricoAdiantamento1.Visible = false;
            gridHistEmail.Visible = false;
            btRelatorio.Visible = false;
        }

        protected override void SetModify()
        {
            base.SetModify();
            lbMsg.Text = String.Empty;
            ControleHistoricoAdiantamento1.Visible = true;
            gridHistEmail.Visible = true;
        }

        protected override void SetView()
        {
            base.SetView();
            ControleHistoricoAdiantamento1.Visible = false;
            gridHistEmail.Visible = false;
            btRelatorio.Visible = false;
        }

        public void SetGridHistEmails()
        {
            gridHistEmail.DataSource = ObjBLL.ObjEF.HistoricoEmailAdmtos.OrderByDescending(it => it.data);
            gridHistEmail.DataBind();
        }

        protected void btSendEmail_Click(object sender, EventArgs e)
        {
            ObjBLL.Get((int)PkValue);
            string saida;
            if (ObjBLL.EnviarEmail(cListaPessoaEmailsCopy.Value.Select(it => it.email.value).ToArray(), cListaPessoaEmailsDest.Value.Select(it => it.email.value).ToArray(), cTextoAssunto.Text, cTextoEmail.Text, out saida))
            {
                lbSaida.BackColor = System.Drawing.Color.Green;
                Get();
            }
            else
                lbSaida.BackColor = System.Drawing.Color.Red;
            lbSaida.ForeColor = System.Drawing.Color.White;            
            lbSaida.Text = saida;
        }
        
        protected override void btInserir_Click(object sender, EventArgs e)
        {
            Set();
            ObjBLL.Add();
            if (ObjBLL.SaveChanges())
            {
                msg("inclusão efetuada");
                ObjBLL.Detach();
                PkValue = ObjBLL.ObjEF.id_adiantamento;
                Get();
            }
            else
                msgError("erro inclusão");
            ControleHistoricoAdiantamento1.DataBind();
            gridHistEmail.DataBind();
            SetModify();
        }

        protected override void btAlterar_Click(object sender, EventArgs e)
        {
            base.btAlterar_Click(sender, e);
            Get();
        }

        protected override void SetGrid()
        {
            var f = new Filter()
            {
                property = "id_tipo_admto",
                value = Convert.ToString(TiposAdiantamentoBLL.adiantamento),
                mode = "=="
            };
            filtros.Add(f);

            if (_ddlSize.SelectedValue == "0")
                _grid.PageSize = 50;
            else
                _grid.PageSize = Convert.ToInt32(_ddlSize.SelectedValue);

            int size = 10 * Convert.ToInt32(this._ddlSize.SelectedValue);
            _grid.DataKeyNames = new string[] { PRIMARY_KEY };
            _grid.DataSource = ObjBLL.Find(
                filtros,
                (string)ViewState["SortExpression"],
                (string)ViewState["SortDirection"], size);
            _grid.DataBind();
            filtros.Remove(f);
        }

        protected void btRelatorio_Click(object sender, EventArgs e)
        {
            Util.NovaJanela(String.Format("ReportAdiantamento.aspx?pk={0}", PkValue.ToString().Criptografar()));
        }

        protected void cDdlProjeto1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            var opa = new OpcaoAdiantamentoBLL();
            gridOpcao.DataSource = opa.GetOpcoesDoProjeto(cDdlProjeto1.Id_projeto);
            gridOpcao.DataBind();
        }

        protected void lista_SelectedIndexChanged(object sender, EventArgs e)
        {
            EmailPadraoBLL epBLL = new EmailPadraoBLL();
            epBLL.Get(Convert.ToInt32(listaEmailpadrao.SelectedValue));
            var lpe = new List<PessoaEmail>();

            foreach (var item in epBLL.ObjEF.EmailCopias)
            {
                var pe = new PessoaEmail() { email = item.email };
                lpe.Add(pe);
            }

            cListaPessoaEmailsCopy.Value = lpe;
            cListaPessoaEmailsCopy.DataBind();
            cTextoEmail.Text = epBLL.ObjEF.corpo;
            cTextoAssunto.Text = epBLL.ObjEF.assunto;
        }

        protected void btCalcular_Click(object sender, EventArgs e)
        {
            ObjBLL.Get(PkValue);
            cDataVencto.Value = cDataPagamento.Value.Value.AddDays(ObjBLL.ObjEF.TiposAdiantamento.num_dias);
        }

        protected void btEnviarEmail_Click(object sender, EventArgs e)
        {
            Util.ChamarScript("openDialog()", "");
            lbSaida.Text = String.Empty;
            cListaPessoaEmailsDest.DataBind();
        }
    }
}