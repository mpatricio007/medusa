using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.LIB;
using Medusa.BLL;
using Medusa.DAL;

namespace Medusa.Sistemas.Cobranca
{
    public partial class Sacados : PageCrud<SacadoBLL>
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_sacado";
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
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_sacado);

            if (ObjBLL.ObjEF.PessoaFisica == null)
                ObjBLL.ObjEF.PessoaFisica = new PessoaFisica();

            getPessoaFisica(ObjBLL.ObjEF.PessoaFisica);          

            this.cCPF1.Focus();
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_sacado = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.PessoaFisica = setPessoaFisica();          

           
        }       

        protected void getPessoaFisica(PessoaFisica PessoaFisica)
        {
            this.txtId_pessoa.Text = Convert.ToString(PessoaFisica.id_pessoa);
            this.cTextoNome.Text = PessoaFisica.nome;            
            this.cDdlSexo1.Value = PessoaFisica.sexo;
            this.cCPF1.Value = PessoaFisica.cpf;
            this.cTextoRG.Text = PessoaFisica.rg;
            
            this.cListaPessoaEmails1.Value = PessoaFisica.Emails.ToList();
            this.cListaPessoaEmails1.DataBind();
            
            this.ListaPessoaTelefones1.Value = PessoaFisica.Telefones.ToList();
            this.ListaPessoaTelefones1.DataBind();
            
            this.cEnder1.Value = PessoaFisica.Enderecos.Count > 0 ? ObjBLL.ObjEF.PessoaFisica.Enderecos.First().endereco
                : new DAL.Endereco();            
        }

        protected PessoaFisica setPessoaFisica()
        {
            var PessoaFisica = new PessoaFisica();
            PessoaFisica.id_pessoa = Convert.ToInt32(this.txtId_pessoa.Text);
            PessoaFisica.nome = this.cTextoNome.Text;
            PessoaFisica.sexo = this.cDdlSexo1.Value;            
            PessoaFisica.cpf = this.cCPF1.Value;
            PessoaFisica.rg = this.cTextoRG.Text;

            PessoaFisica.SetEmails = true;
            PessoaFisica.Emails = this.cListaPessoaEmails1.Value;

            PessoaFisica.SetTelefones = true;
            PessoaFisica.Telefones = this.ListaPessoaTelefones1.Value;

            PessoaFisica.SetEnderecos = true;
            PessoaFisica.Enderecos.Add(new DAL.PessoaEndereco(this.cEnder1.Value));

            return PessoaFisica;
            
        }

        protected void cCPF1_OnTextChanged(object sender, EventArgs e)
        {
            if (cCPF1.IsValid)
            {
                var cpf = cCPF1.Value;
                ObjBLL.Get(cCPF1.Value);
                getPessoaFisica(ObjBLL.ObjEF.PessoaFisica);
                this.cCPF1.Value = cpf;                
                this.cTextoNome.Focus();
            }
        }

    }
}