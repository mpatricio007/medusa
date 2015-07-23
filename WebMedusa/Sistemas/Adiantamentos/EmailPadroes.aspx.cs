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
    public partial class EmailPadroes : PageCrud<EmailPadraoBLL>
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_email_padrao";
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
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_email_padrao);
            this.cTextoNome.Text = ObjBLL.ObjEF.nome;
            this.cTextoAssunto.Text = ObjBLL.ObjEF.assunto;
            this.cTextoCorpo.Text = ObjBLL.ObjEF.corpo;
            this.DdlStatusAdiantamentos1.Id_status_admto = ObjBLL.ObjEF.id_status_admto.GetValueOrDefault();
            this.cDdlTiposAdiantamentos1.Id_tipo_admto = ObjBLL.ObjEF.id_tipo_admto.GetValueOrDefault();
            CcontroleEmailCopia1.Id_email_padrao = ObjBLL.ObjEF.id_email_padrao;
            CcontroleEmailCopia1.DataBind();
            dEmails.Visible = ObjBLL.Existes();
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_email_padrao = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.nome = this.cTextoNome.Text;
            ObjBLL.ObjEF.assunto = this.cTextoAssunto.Text;
            ObjBLL.ObjEF.corpo = this.cTextoCorpo.Text;
            ObjBLL.ObjEF.id_status_admto = this.DdlStatusAdiantamentos1.Id_status_admto;
            ObjBLL.ObjEF.id_tipo_admto = this.cDdlTiposAdiantamentos1.Id_tipo_admto;
        }

        protected override void btInserir_Click(object sender, EventArgs e)
        {
            Set();
            ObjBLL.Add();
            if (ObjBLL.SaveChanges())
            {
                msg("inclusão efetuada");
                PkValue = ObjBLL.ObjEF.id_email_padrao;
                Get();
            }
            else
                msgError("erro inclusão");
        }
    }
}