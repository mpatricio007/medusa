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
using Medusa.Controles;

namespace Medusa.Sistemas.Admin
{
    public partial class UsuariosExternos : PageCrud<UsuarioBLL>
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_usuario";
            //valor da chave primária
            PkValue = Convert.ToInt32(this.txtCodigo.Text);
            // painel do grid
            pGrid = new Panel();
            // painel do formulário de alteração
            pCadastro = new Panel();
            lbMsg = lblMsg;
            _btAlterar = new Button();
            _btAlterar0 = new Button();
            _btInserir = btInserir0;
            _btInserir0 = btInserir0;
            _btExcluir = new Button();
            _btExcluir0 = new Button();

            if (!IsPostBack)
            {
                Seguranca = false;
                GetQueryString();
            }
        }

        protected override void GetQueryString()
        {
            PkValue = Convert.ToInt32(HttpContext.Current.Request.QueryString["pk"].DesCriptografar());
            if ((int)PkValue == 0)
                SetAdd();
            else
            {
                Get();
                SetModify();
            }
        }

        protected override void Get()
        {
            //ObjBLL.Get(PkValue);
            //this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_usuario);

            //if (ObjBLL.ObjEF.PessoaFisica == null)
            //    ObjBLL.ObjEF.PessoaFisica = new PessoaFisica();

            //getPessoaFisica(ObjBLL.ObjEF.PessoaFisica);
            //this.cTextoLogin.Text = ObjBLL.ObjEF.login;           
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_usuario = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.PessoaFisica = setPessoaFisica();
            ObjBLL.ObjEF.login = this.cTextoLogin.Text;
        }

        protected override void SetAdd()
        {
            base.SetAdd();
            pSenha.Visible = true;
        }

        protected override void SetModify()
        {
            base.SetModify();
            pSenha.Visible = false;

        }

        //protected void getPessoaFisica(PessoaFisica PessoaFisica)
        //{
        //    this.txtId_pessoa.Text = Convert.ToString(PessoaFisica.id_pessoa);
        //    this.cTextoNome.Text = PessoaFisica.nome;
        //    this.cDdlSexo1.Value = PessoaFisica.sexo;
        //    this.cCPF1.Value = PessoaFisica.cpf;
        //    this.cTextoRG.Text = PessoaFisica.rg;
        //    var em = PessoaFisica.Emails.FirstOrDefault();
        //    em = em ?? new PessoaEmail();
        //    this.cEmail1.Value = em.email;
        //}

        protected PessoaFisica setPessoaFisica()
        {
            var PessoaFisica = new PessoaFisica();
            PessoaFisica.id_pessoa = Convert.ToInt32(this.txtId_pessoa.Text);
            PessoaFisica.nome = this.cTextoNome.Text;
            PessoaFisica.sexo = this.cDdlSexo1.Value;
            PessoaFisica.cpf = this.cCPF1.Value;
            PessoaFisica.rg = this.cTextoRG.Text;

            PessoaFisica.Emails.Add(new PessoaEmail(this.cEmail1.Value));
            PessoaFisica.SetEmails = true;
            return PessoaFisica;

        }

        protected override void btInserir_Click(object sender, EventArgs e)
        {
            if (!ObjBLL.ExistUsuario(this.cCPF1.Value))
            {
                ObjBLL.Get(this.cCPF1.Value);
                this.txtId_pessoa.Text = Util.InteiroToString(ObjBLL.ObjEF.id_pessoa);

                ObjBLL.ObjEF.senha = SecurityBLL.GetSha1Hash(this.txtSenha.Text);
                ObjBLL.ObjEF.status = true;
                ObjBLL.ObjEF.UsuarioSistema = new List<UsuarioSistema>();

                ObjBLL.ObjEF.UsuarioSistema.Add(new UsuarioSistema()
                {
                    id_sistema = (int)SistemasDefault.Fornecedor
                });

                Set();
                ObjBLL.Add();
                if (ObjBLL.SaveChanges())
                {
                    Util.ResponseToPage("../../HomePJ.aspx", "Cadastro realizado com sucesso!");

                }
                else
                    Util.ShowMessage("Erro! Contate o administrador do sistema.");
            }
            else
                Util.ShowMessage("Erro! Usuário já cadastrado!");
        }
    }
}