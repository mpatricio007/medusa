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
    public partial class Usuarios : PageCrud<UsuarioFuspBLL>
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_usuario";
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

            //
            _btProcurar = btSearch;
            _ckFilter = ckFilter;
            _dataListFiltros = DataListFiltros;
            //
            if (!IsPostBack)
            {                
                base.Page_Load(sender, e);               
            }
        } 

        

        protected override void Get()
        {
            ObjBLL.Get(PkValue);
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_usuario);

            if (ObjBLL.ObjEF.PessoaFisica == null)
                ObjBLL.ObjEF.PessoaFisica = new PessoaFisica();


            getPessoaFisica(ObjBLL.ObjEF.PessoaFisica);

            this.cTextoLogin.Text = ObjBLL.ObjEF.login;
            this.cTextoRamal.Text = ObjBLL.ObjEF.ramal;
            this.cDdlSetor1.Id_setor = ObjBLL.ObjEF.id_setor;
            this.ddlNivel.SelectedValue = Convert.ToString(ObjBLL.ObjEF.nivel);
            this.cTextoChapa.Text = ObjBLL.ObjEF.chapa;
            this.ckStatus.Checked = ObjBLL.ObjEF.status;

            panelSistemas.Visible = true;
            gridSistemasUsuario.DataSource = ObjBLL.ObjEF.UsuarioSistema;
            gridSistemasUsuario.DataBind();
         }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_usuario = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.PessoaFisica = setPessoaFisica();          


            ObjBLL.ObjEF.login = this.cTextoLogin.Text;
            ObjBLL.ObjEF.ramal = this.cTextoRamal.Text;
            ObjBLL.ObjEF.id_setor = this.cDdlSetor1.Id_setor;
            
            ObjBLL.ObjEF.nivel = Convert.ToInt32(this.ddlNivel.SelectedValue);
            ObjBLL.ObjEF.chapa = this.cTextoChapa.Text;
            ObjBLL.ObjEF.status = this.ckStatus.Checked;
            
        }

        protected void gridSistemas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int linhaCorrente = Convert.ToInt32(e.CommandArgument); 
            UsuarioSistemaBLL us = new UsuarioSistemaBLL();
            us.ObjEF.id_usuario = Convert.ToInt32(this.txtCodigo.Text);
            us.ObjEF.id_sistema = Convert.ToInt32(gridSistemas.DataKeys[linhaCorrente].Value);
            us.Add(); 
            us.SaveChanges();
            gridSistemasUsuario.DataBind();
        }

        protected void gridSistemasUsuario_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int linhaCorrente = Convert.ToInt32(e.CommandArgument);
            UsuarioSistemaBLL us = new UsuarioSistemaBLL();
            us.Get(Convert.ToInt32(gridSistemasUsuario.DataKeys[linhaCorrente].Value));
            us.Delete();
            us.SaveChanges();
            gridSistemasUsuario.DataBind();

        }

        protected void gridSistemasUsuario_DataBinding(object sender, EventArgs e)
        {
            if (Convert.ToInt32(PkValue) != 0)
            {
                panelSistemas.Visible = true;
                gridSistemasUsuario.DataKeyNames = new string[] { "id_usuario_sistema" };
                ObjBLL.Get(PkValue);
                gridSistemasUsuario.DataSource = ObjBLL.ObjEF.UsuarioSistema.ToList();
                gridSistemas.DataBind();
            }
            else panelSistemas.Visible = false;

        }

        protected override void SetAdd()
        {
            base.SetAdd();
            panelSistemas.Visible = false;
            pSenha.Visible = true;
        }

        protected override void SetModify()
        {
            base.SetModify();
            panelSistemas.Visible = true;
            pSenha.Visible = false;

        }

        protected override void btProcurar_Click(object sender, EventArgs e)
        {
            base.btProcurar_Click(sender, e);
            panelSistemas.Visible = false;
        }

        protected void gridSistemas_DataBinding(object sender, EventArgs e)
        {
            SistemaBLL sistema = new SistemaBLL();
            gridSistemas.DataSource = sistema.GetSistemasDisponiveis(Convert.ToInt32(this.txtCodigo.Text));
        }

        protected void getPessoaFisica(PessoaFisica PessoaFisica)
        {
            this.txtId_pessoa.Text = Convert.ToString(PessoaFisica.id_pessoa);
            this.cTextoNome.Text = PessoaFisica.nome;
            this.cDataNascto.Value = PessoaFisica.dtNascto;
            this.cDdlSexo1.Value = PessoaFisica.sexo;
            this.cCPF1.Value = PessoaFisica.cpf;
            this.cTextoRG.Text = PessoaFisica.rg;

            this.cListaPessoaEmails1.Value = PessoaFisica.Emails.ToList();
            this.cListaPessoaEmails1.DataBind();    
                       
        }

        protected PessoaFisica setPessoaFisica()
        {
            var PessoaFisica = new PessoaFisica();
            PessoaFisica.id_pessoa = Convert.ToInt32(this.txtId_pessoa.Text);
            PessoaFisica.nome = this.cTextoNome.Text;
            PessoaFisica.sexo = this.cDdlSexo1.Value;
            PessoaFisica.dtNascto = this.cDataNascto.Value.GetValueOrDefault();
            PessoaFisica.cpf = this.cCPF1.Value;
            PessoaFisica.rg = this.cTextoRG.Text;

            PessoaFisica.Emails = this.cListaPessoaEmails1.Value;
            PessoaFisica.SetEmails = true;
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

        protected override void btInserir_Click(object sender, EventArgs e)
        {
            ObjBLL.ObjEF.senha = SecurityBLL.GetSha1Hash(this.txtSenha.Text);
            base.btInserir_Click(sender, e);
        }

    }
}