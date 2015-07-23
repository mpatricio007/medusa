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

namespace Medusa.Sistemas.Folha
{
    public partial class Autonomos : PageCrud<AutonomoBLL>
    {
        private ContratoAutonomoBLL ContratoAutonomoBLL = new ContratoAutonomoBLL();
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_autonomo";
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
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_autonomo);

            if (ObjBLL.ObjEF.PessoaFisica == null)
                ObjBLL.ObjEF.PessoaFisica = new PessoaFisica();

            getPessoaFisica(ObjBLL.ObjEF.PessoaFisica);

            //cTextoProfissao.Text = this.ObjBLL.ObjEF.profissao;
            //cTextoInscrInss.Text = this.ObjBLL.ObjEF.inscricao_inss;
            //cDataValidadeIss.Value = this.ObjBLL.ObjEF.validade_iss;
            //RbTipoIss.SelectedValue = this.ObjBLL.ObjEF.tipo_iss;
            //cTextoCbo.Text = this.ObjBLL.ObjEF.cbo;
            //cTextoCcm.Text = this.ObjBLL.ObjEF.ccm;
            //RbIsentoIr.SelectedValue = Convert.ToString(this.ObjBLL.ObjEF.isento_ir);
            //cTextoMotivoIsencao.Text = this.ObjBLL.ObjEF.motivo_isencao;
            //RbIsentoIr_SelectedIndexChanged(null, null);
            //RbClassificacao.SelectedValue = this.ObjBLL.ObjEF.classificacao;
            //cTextoRegimeUsp.Text = this.ObjBLL.ObjEF.regime_usp;
            //cTextoNumeroCert.Text = this.ObjBLL.ObjEF.numero_cert;
            //cDataValidade.Value = this.ObjBLL.ObjEF.validade;
            //cInteiroHoras.Value = this.ObjBLL.ObjEF.horas_semanais;

            this.cCPF.Focus();
            ControleContratoAutonomo.Id_pessoa_fisica = ObjBLL.ObjEF.id_pessoa;
            ControleContratoAutonomo.DataBind();

        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_autonomo = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.PessoaFisica = setPessoaFisica();
            //ObjBLL.ObjEF.profissao = cTextoProfissao.Text;
            //ObjBLL.ObjEF.inscricao_inss = cTextoInscrInss.Text;
            //ObjBLL.ObjEF.validade_iss = cDataValidadeIss.Value;
            //ObjBLL.ObjEF.tipo_iss = RbTipoIss.SelectedValue;
            //ObjBLL.ObjEF.cbo = cTextoCbo.Text;
            //ObjBLL.ObjEF.ccm = cTextoCcm.Text;            
            //ObjBLL.ObjEF.isento_ir = Convert.ToBoolean(RbIsentoIr.SelectedValue);
            //ObjBLL.ObjEF.motivo_isencao = cTextoMotivoIsencao.Text;
            //ObjBLL.ObjEF.classificacao = RbClassificacao.SelectedValue;
            //ObjBLL.ObjEF.regime_usp = cTextoRegimeUsp.Text;
            //ObjBLL.ObjEF.numero_cert = cTextoNumeroCert.Text;
            //ObjBLL.ObjEF.validade = Convert.ToDateTime(cDataValidade.Value);
            //ObjBLL.ObjEF.horas_semanais = cInteiroHoras.Value;

        }

        protected void getPessoaFisica(PessoaFisica PessoaFisica)
        {
            this.txtId_pessoa.Text = Convert.ToString(PessoaFisica.id_pessoa);
            this.cTextoNome.Text = PessoaFisica.nome;
            this.cCPF.Value = PessoaFisica.cpf;
            this.cTextoRg.Text = PessoaFisica.rg;
            this.cDataNascimento.Value = PessoaFisica.dtNascto;
            this.cDdlSexo.Value = PessoaFisica.sexo;

            this.cListaPessoaEmails.Value = PessoaFisica.Emails.ToList();
            this.cListaPessoaEmails.DataBind();

            this.cListaPessoaTelefones.Value = PessoaFisica.Telefones.ToList();
            this.cListaPessoaTelefones.DataBind();

            this.cEndereco.Value = PessoaFisica.Enderecos.Count > 0 ? ObjBLL.ObjEF.PessoaFisica.Enderecos.First().endereco
                : new DAL.Endereco();

            this.cControleContaPessoa1.Value = PessoaFisica.ContaPessoaFisica;
        }

        protected PessoaFisica setPessoaFisica()
        {
            var PessoaFisica = new PessoaFisica();
            PessoaFisica.id_pessoa = Convert.ToInt32(this.txtId_pessoa.Text);
            PessoaFisica.nome = this.cTextoNome.Text;
            PessoaFisica.cpf = this.cCPF.Value;
            PessoaFisica.rg = this.cTextoRg.Text;
            PessoaFisica.dtNascto = Convert.ToDateTime(this.cDataNascimento.Value);
            PessoaFisica.sexo = this.cDdlSexo.Value;

            PessoaFisica.SetEmails = true;
            PessoaFisica.Emails = this.cListaPessoaEmails.Value;

            PessoaFisica.SetTelefones = true;
            PessoaFisica.Telefones = this.cListaPessoaTelefones.Value;

            PessoaFisica.SetEnderecos = true;
            PessoaFisica.Enderecos.Add(new DAL.PessoaEndereco(this.cEndereco.Value));

            PessoaFisica.ContaPessoaFisica = this.cControleContaPessoa1.Value; 

            return PessoaFisica;
        }

        protected void cCPF_OnTextChanged(object sender, EventArgs e)
        {
            if (cCPF.IsValid)
            {
                var cpf = cCPF.Value;
                ObjBLL.Get(cCPF.Value);
                getPessoaFisica(ObjBLL.ObjEF.PessoaFisica);
                this.cCPF.Value = cpf;
                this.cTextoNome.Focus();
            }
        }

        protected void RbIsentoIr_SelectedIndexChanged(object sender, EventArgs e)
        {
            cTextoMotivoIsencao.EnableValidator = Convert.ToBoolean(RbIsentoIr.SelectedValue);
        }
        protected override void SetAdd()
        {
            pContratos.Visible = false;
            base.SetAdd();
        }

        protected override void SetModify()
        {
            pContratos.Visible = true;
            base.SetModify();
        }

        protected override void SetView()
        {
            pContratos.Visible = false;
            base.SetView();
        }


        protected void SetPrint()
        {
            pGrid.Visible = false;
            pCadastro.Visible = false;
        }
    }
}