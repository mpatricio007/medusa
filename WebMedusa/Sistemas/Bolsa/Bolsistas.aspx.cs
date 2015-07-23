using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.DAL;
using Medusa.BLL;
using Medusa.LIB;


namespace Medusa.Sistemas.Bolsa
{
    public partial class Bolsistas : PageCrud<BolsistaBLL>
    {
        private ContratoAutonomoBLL ContratoAutonomoBLL = new ContratoAutonomoBLL();
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_bolsista";
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
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_bolsista);

            if (ObjBLL.ObjEF.PessoaFisica == null)
                ObjBLL.ObjEF.PessoaFisica = new PessoaFisica();

            getPessoaFisica(ObjBLL.ObjEF.PessoaFisica);
          
            this.cCPFBolsista.Focus();
            //ControleContratoBolsa1.Id_pessoa_fisica = ObjBLL.ObjEF.id_pessoa;
            //ControleContratoBolsa1.DataBind();
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_bolsista = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.PessoaFisica = setPessoaFisica();
        }

        protected void getPessoaFisica(PessoaFisica PessoaFisica)
        {
            this.txtId_pessoa.Text = Convert.ToString(PessoaFisica.id_pessoa);
            this.cTextoNome.Text = PessoaFisica.nome;
            this.cDdlSexo.Value = PessoaFisica.sexo;
            this.cCPFBolsista.Value = PessoaFisica.cpf;
            this.cTextoRg.Text = PessoaFisica.rg;
            this.cDataNascimento.Value = PessoaFisica.dtNascto;

            this.cControleContaPessoa1.Value = PessoaFisica.ContaPessoaFisica;

            //this.textAgencia.Text = Convert.ToString(PessoaFisica.ContaPessoaFisica.agencia);
            //this.textDigitoAgencia.Text = Convert.ToString(PessoaFisica.ContaPessoaFisica.digitoAgencia);
            //this.textConta.Text = Convert.ToString(PessoaFisica.ContaPessoaFisica.conta);
            //this.textDigitoConta.Text = Convert.ToString(PessoaFisica.ContaPessoaFisica.digitoConta);
            //this.cDdlBancos.Id_banco = Convert.ToInt32(PessoaFisica.ContaPessoaFisica.id_banco);

            this.cListaPessoaEmails1.Value = PessoaFisica.Emails.ToList();
            this.cListaPessoaEmails1.DataBind();

            this.cListaPessoaTelefones1.Value = PessoaFisica.Telefones.ToList();
            this.cListaPessoaTelefones1.DataBind();

            this.cEndereco.Value = PessoaFisica.Enderecos.Count > 0 ? ObjBLL.ObjEF.PessoaFisica.Enderecos.First().endereco
                : new DAL.Endereco();
        }

        protected PessoaFisica setPessoaFisica()
        {
            var PessoaFisica = new PessoaFisica();
            PessoaFisica.id_pessoa = Convert.ToInt32(this.txtId_pessoa.Text);
            PessoaFisica.nome = this.cTextoNome.Text;
            PessoaFisica.sexo = this.cDdlSexo.Value;
            PessoaFisica.cpf = this.cCPFBolsista.Value;
            PessoaFisica.rg = this.cTextoRg.Text;
            PessoaFisica.dtNascto = Convert.ToDateTime(this.cDataNascimento.Value);

            PessoaFisica.SetConta = true;
            PessoaFisica.ContaPessoaFisica = this.cControleContaPessoa1.Value; 
            
            PessoaFisica.SetEmails = true;
            PessoaFisica.Emails = this.cListaPessoaEmails1.Value;

            PessoaFisica.SetTelefones = true;
            PessoaFisica.Telefones = this.cListaPessoaTelefones1.Value;

            PessoaFisica.SetEnderecos = true;
            PessoaFisica.Enderecos.Add(new DAL.PessoaEndereco(this.cEndereco.Value));

            return PessoaFisica;
        }

        //protected void getContratoBolsa(ContratoBolsa ContratoBolsa)
        //{
        //    this.txtCodigoContrato.Text = Convert.ToString(ContratoBolsa.id_contrato);
        //    this.DdlTipoContratos.Id_tipo_contrato = Convert.ToInt32(ContratoBolsa.id_tipo_contrato);
        //    this.cDataInicio.Value = Convert.ToDateTime(ContratoBolsa.inicio);
        //    this.cDataTermino.Value = ContratoBolsa.termino;
        //    this.cValorContrato.Value = ContratoBolsa.valor;
        //    this.cInteiroParcela.Value = ContratoBolsa.qtde_parcelas;
        //    this.cDataRescisao.Value = ContratoBolsa.rescisao;
        //    this.cTextoDescricao.Text = ContratoBolsa.descricao;
        //    this.cTextoObs.Text = ContratoBolsa.observacao;
        //    this.cDdlProjeto.Id_projeto = Convert.ToInt32(ContratoBolsa.id_projeto);
        //    this.cDataRelatorio.Value = ContratoBolsa.data_relatorio;
        //    this.cDataInicioSeg.Value = ContratoBolsa.inicio_seguro;
        //    this.cDataTerminoSeg.Value = ContratoBolsa.termino_seguro;
        //    this.cDdlBolsas.Id_bolsa = Convert.ToInt32(ContratoBolsa.id_bolsa);
        //    this.CheckSuspenso.Checked = Convert.ToBoolean(ContratoBolsa.suspenso);
        //}

        //protected ContratoBolsa setContratoBolsa()
        //{
        //    var ContratoBolsa = new ContratoBolsa();
        //    ContratoBolsa.id_contrato = Convert.ToInt32(this.txtCodigoContrato.Text);
        //    ContratoBolsa.id_tipo_contrato = DdlTipoContratos.Id_tipo_contrato;
        //    ContratoBolsa.inicio = Convert.ToDateTime(this.cDataInicio.Value);
        //    ContratoBolsa.termino = this.cDataTermino.Value;
        //    ContratoBolsa.valor = Convert.ToDecimal(this.cValorContrato.Value);
        //    ContratoBolsa.qtde_parcelas = Convert.ToInt32(this.cInteiroParcela.Value);
        //    ContratoBolsa.rescisao = Convert.ToDateTime(this.cDataRescisao.Value);
        //    ContratoBolsa.descricao = Convert.ToString(this.cTextoDescricao.Text);
        //    ContratoBolsa.observacao = Convert.ToString(this.cTextoObs.Text);
        //    ContratoBolsa.id_projeto = this.cDdlProjeto.Id_projeto;
        //    ContratoBolsa.data_relatorio = Convert.ToDateTime(this.cDataRelatorio.Value);
        //    ContratoBolsa.inicio_seguro = Convert.ToDateTime(this.cDataInicioSeg.Value);
        //    ContratoBolsa.termino_seguro = Convert.ToDateTime(this.cDataTerminoSeg.Value);
        //    ContratoBolsa.id_bolsa = Convert.ToInt32(this.cDdlBolsas.Id_bolsa);
        //    ContratoBolsa.suspenso = this.CheckSuspenso.Checked;

        //    return ContratoBolsa;
        //}

        //protected virtual void msgContrato(string msg)
        //{
        //    lblMsgContrato.BackColor = System.Drawing.Color.Green;
        //    lblMsgContrato.ForeColor = System.Drawing.Color.White;
        //    lblMsgContrato.Text = string.Format("* {0} !", msg);
        //}

        //protected virtual void msgErrorContrato(string msg)
        //{
        //    lblMsgContrato.BackColor = System.Drawing.Color.Red;
        //    lblMsgContrato.ForeColor = System.Drawing.Color.White;
        //    lblMsgContrato.Text = string.Format("* {0} !?", msg);
        //}

        protected void cCPFBolsista_OnTextChanged(object sender, EventArgs e)
        {
            if (cCPFBolsista.IsValid)
            {
                var cpf = cCPFBolsista.Value;
                ObjBLL.Get(cCPFBolsista.Value);
                getPessoaFisica(ObjBLL.ObjEF.PessoaFisica);
                this.cCPFBolsista.Value = cpf;
                this.cTextoNome.Focus();
            }
        }

        protected override void SetAdd()
        {
            pContratosBolsista.Visible = false;
            base.SetAdd();
        }

        protected override void SetModify()
        {
            pContratosBolsista.Visible = true;
            
            base.SetModify();
        }

        protected override void SetView()
        {
            pContratosBolsista.Visible = false;
            base.SetView();
        }


        protected void SetPrint()
        {
            pGrid.Visible = false;
            pCadastro.Visible = false;
        }

        //protected void btInserirContrato_Click(object sender, EventArgs e)
        //{
        //    Set();
        //    ObjBLL.Add();
        //    if (ObjBLL.SaveChanges())
        //    {
        //        msg("inclusão efetuada");
        //        PkValue = 0;
        //        Get();

        //    }
        //    else
        //        msgError("erro inclusão");
        //}

        //protected void btAlterarContrato_Click(object sender, EventArgs e)
        //{
        //    ObjBLL.Get(PkValue);
        //    Set();
        //    ObjBLL.Update();
        //    if (ObjBLL.SaveChanges())
        //        msgContrato("alteração efetuada");
        //        //this.txtCodigoContrato.Text = Convert.ToString(contratobolsaBLL.ObjEF.id_contrato);
        //    else
        //        msgErrorContrato("erro alteração");
        //}

        //protected void btExcluirContrato_Click(object sender, EventArgs e)
        //{
        //    ObjBLL.Get(PkValue);
        //    ObjBLL.Delete();
        //    if (ObjBLL.SaveChanges())
        //        msg("exclusão efetuada");
        //    else
        //        msgError("erro exclusão");
        //    Get();
        //    SetAdd();
        //}

        //protected void btCancelarContrato_Click(object sender, EventArgs e)
        //{
        //    SetGrid();
        //    SetView();
        //}
    }
}