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
    public partial class Beneficiarios : PageCrud<BeneficiarioBLL>
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_beneficiario";
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
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_beneficiario);

            cDdlUnidades1.Id_unidade = Convert.ToInt32(ObjBLL.ObjEF.id_unidade);

            if (ObjBLL.ObjEF.PessoaFisica == null)
                ObjBLL.ObjEF.PessoaFisica = new PessoaFisica();

            getPessoaFisica(ObjBLL.ObjEF.PessoaFisica);

            this.cCPF1.Focus();
        }

        protected void getPessoaFisica(PessoaFisica pessoaFisica)
        {
            this.txtId_pessoa.Text = Convert.ToString(pessoaFisica.id_pessoa);
            this.cTextoNome.Text = pessoaFisica.nome;
            this.cDdlSexo1.Value = pessoaFisica.sexo;
            this.cCPF1.Value = pessoaFisica.cpf;
            this.cTextoRG.Text = pessoaFisica.rg;

            this.cControleContaPessoa1.Value = pessoaFisica.ContaPessoaFisica;

            this.cListaPessoaEmails1.Value = pessoaFisica.Emails.ToList();
            this.cListaPessoaEmails1.DataBind();

            this.ListaPessoaTelefones1.Value = pessoaFisica.Telefones.ToList();
            this.ListaPessoaTelefones1.DataBind();

            this.cEnder1.Value = pessoaFisica.Enderecos.Count > 0 ? ObjBLL.ObjEF.PessoaFisica.Enderecos.First().endereco
                : new DAL.Endereco();
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_beneficiario = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.id_unidade = cDdlUnidades1.Id_unidade != 0 ? cDdlUnidades1.Id_unidade : new Nullable<int>();
            ObjBLL.ObjEF.PessoaFisica = setPessoaFisica();
            
        }

        protected PessoaFisica setPessoaFisica()
        {
            var pessoaFisica = new PessoaFisica();
            pessoaFisica.id_pessoa = Convert.ToInt32(txtId_pessoa.Text);
            pessoaFisica.nome = cTextoNome.Text;
            pessoaFisica.sexo = cDdlSexo1.Value;
            pessoaFisica.cpf = cCPF1.Value;
            pessoaFisica.rg = cTextoRG.Text;

            pessoaFisica.SetConta = true;
            pessoaFisica.ContaPessoaFisica = cControleContaPessoa1.Value;

            pessoaFisica.SetEmails = true;
            pessoaFisica.Emails = this.cListaPessoaEmails1.Value;

            pessoaFisica.SetTelefones = true;
            pessoaFisica.Telefones = this.ListaPessoaTelefones1.Value;

            pessoaFisica.SetEnderecos = true;
            pessoaFisica.Enderecos.Add(new DAL.PessoaEndereco(this.cEnder1.Value));

            return pessoaFisica;
        }

        protected void cCPF1_OnTextChanged(object sender, EventArgs e)
        {
            if (cCPF1.IsValid)
            {
                var cpf = cCPF1.Value;
                ObjBLL.Get(cpf);
                getPessoaFisica(ObjBLL.ObjEF.PessoaFisica);
                this.cCPF1.Value = cpf;
                this.cTextoNome.Focus();
            }
        }
    }
}
