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

namespace Medusa.Sistemas.Conciliacao
{
    public partial class TiposLcto : PageCrud<TipoLctoBLL>
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_tipo_lcto";
            //valor da chave primária
            PkValue = Convert.ToInt32(this.txtId_tipolcto.Text);
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
                UsuarioFuspBLL uf = new UsuarioFuspBLL();
                chkPertenceAdmin.Visible = (uf.IsAdministrador(SecurityBLL.GetCurrentUsuario().id_usuario));
                base.Page_Load(sender, e);
            }
        }

        protected override void Get()
        {
            ObjBLL.Get(PkValue);
            this.txtId_tipolcto.Text = Convert.ToString(ObjBLL.ObjEF.id_tipo_lcto);
            this.cTextoCodigo.Text = ObjBLL.ObjEF.codigo;
            this.cTextoDescricao.Text = ObjBLL.ObjEF.descricao;
            this.rbImportar.SelectedValue = Convert.ToString(ObjBLL.ObjEF.importar);
            this.cDdlBancos1.Id_banco = ObjBLL.ObjEF.id_banco;
            if ((Convert.ToInt32(PkValue) != 0))
            {
                this.rblDC.SelectedValue = ObjBLL.ObjEF.dc;
                this.chkPertenceAdmin.Checked = ObjBLL.ObjEF.pertenceAdmin;
                
            }
            else
                this.chkPertenceAdmin.Checked = false;
            panelUsuarios.Visible = true;
            gridUsuariosTipoLcto.DataSource = ObjBLL.ObjEF.UsuarioTipoLcto.OrderBy(it => it.Usuario.PessoaFisica.nome);
            gridUsuariosTipoLcto.DataBind();
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_tipo_lcto = Convert.ToInt32(this.txtId_tipolcto.Text);
            ObjBLL.ObjEF.codigo = this.cTextoCodigo.Text;
            ObjBLL.ObjEF.descricao = this.cTextoDescricao.Text;
            ObjBLL.ObjEF.dc = this.rblDC.SelectedValue;
            ObjBLL.ObjEF.pertenceAdmin = this.chkPertenceAdmin.Checked;
            ObjBLL.ObjEF.importar = Convert.ToBoolean(this.rbImportar.SelectedValue);
            ObjBLL.ObjEF.id_banco = this.cDdlBancos1.Id_banco;
        }

        protected void gridUsuariosTipoLcto_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridUsuariosTipoLcto.PageIndex = e.NewPageIndex;
            gridUsuariosTipoLcto.DataBind();
        }

        protected void gridUsuariosTipoLcto_DataBinding(object sender, EventArgs e)
        {
            if (Convert.ToInt32(PkValue) != 0)
            {
                panelUsuarios.Visible = true;
                gridUsuariosTipoLcto.DataKeyNames = new string[] { "id_usuario_tipolcto" };
                ObjBLL.Get(PkValue);
                gridUsuariosTipoLcto.DataSource = ObjBLL.ObjEF.UsuarioTipoLcto.ToList();
                gridUsuarios.DataBind();
            }
            else panelUsuarios.Visible = false;

        }

        protected override void SetAdd()
        {
            base.SetAdd();
            panelUsuarios.Visible = false;
        }

        protected override void SetModify()
        {
            base.SetModify();
            panelUsuarios.Visible = true;
        }

        protected override void btProcurar_Click(object sender, EventArgs e)
        {
            base.btProcurar_Click(sender, e);
            panelUsuarios.Visible = false;
        }

        protected void gridUsuarios_DataBinding(object sender, EventArgs e)
        {
            UsuarioFuspBLL ufBLL = new UsuarioFuspBLL();
            gridUsuarios.DataKeyNames = new string[] { "id_pessoa" };
            gridUsuarios.DataSource = ufBLL.GetUsuariosDisponiveis(Convert.ToInt32(this.txtId_tipolcto.Text));
        }      

        protected void gridUsuarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridUsuarios.PageIndex = e.NewPageIndex;
            gridUsuarios.DataBind();
        }

        protected void gridUsuarios_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {            
            UsuarioTipoLctoBLL ut = new UsuarioTipoLctoBLL();
            ut.ObjEF.id_tipo_lcto = Convert.ToInt32(this.txtId_tipolcto.Text);
            ut.ObjEF.id_usuario = Convert.ToInt32(gridUsuarios.DataKeys[e.NewSelectedIndex].Value);
            ut.Add();
            ut.SaveChanges();
            gridUsuariosTipoLcto.DataBind();
            e.Cancel = true;
        }

        protected void gridUsuariosTipoLcto_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {            
            UsuarioTipoLctoBLL ut = new UsuarioTipoLctoBLL();
            ut.Get(Convert.ToInt32(gridUsuariosTipoLcto.DataKeys[e.NewSelectedIndex].Value));
            ut.Delete();
            ut.SaveChanges();
            gridUsuariosTipoLcto.DataBind();
            e.Cancel = true;
        }
    }
}