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

namespace Medusa.Sistemas.Correspondencia
{
    public partial class Cobrancas : PageCrud<CobrancaBLL>
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_correspondencia";
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
                this.ddlAno.DataSource = ObjBLL.GetAnos();
                this.ddlAno.DataBind();
                base.Page_Load(sender, e);
            }
        }

        protected override void Get()
        {
            ObjBLL.Get(PkValue);
            this.lbNum.Text = Convert.ToString(ObjBLL.ObjEF.num);
            this.lbUsuario.Text = ObjBLL.ObjEF.id_usuario != 0 ? ObjBLL.ObjEF.Usuario.PessoaFisica.nome : SecurityBLL.GetCurrentUsuario().PessoaFisica.nome;
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_correspondencia);
            this.cData1.Value = ObjBLL.ObjEF.data;
            this.cTextoProjeto.Text = ObjBLL.ObjEF.projeto;
            this.cTextoDestinatario.Text = ObjBLL.ObjEF.destinatario;
            this.cTextoDescricao.Text = ObjBLL.ObjEF.descricao;
            this.AsyncFileUpload1.Visible = false;
            this.spanUploading.Visible = false;
            this.lbArquivo.Text = ObjBLL.ObjEF.arquivo;
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_correspondencia = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.data = this.cData1.Value.GetValueOrDefault();
            ObjBLL.ObjEF.projeto = this.cTextoProjeto.Text;
            ObjBLL.ObjEF.destinatario = this.cTextoDestinatario.Text;
            ObjBLL.ObjEF.descricao = this.cTextoDescricao.Text;

            ObjBLL.up = new BLL.Upload();
            ObjBLL.up.file = (HttpPostedFile)Session["file"];
            Session.Remove("file");
        }
        protected override void btInserir_Click(object sender, EventArgs e)
        {
            Set();
            ObjBLL.Add();
            if (ObjBLL.ObjEF.data.Year == DateTime.Now.Year)
                if (ObjBLL.SaveChanges())
                {
                    msg("inclusão efetuada");
                    Util.ShowMessage(String.Format("Cobrança de número {0} gerado com sucesso!", ObjBLL.ObjEF.num));
                    PkValue = 0;
                    Get();
                }
                else
                    msgError("erro inclusão");
            else
                msgError("Não é permitido inserir uma cobrança com data diferente do ano atual");

        }
        protected override void btAlterar_Click(object sender, EventArgs e)
        {
            ObjBLL.Get(PkValue);
            Set();

            if (ObjBLL.MesmoUsuario())
                if (ObjBLL.MesmoAno())
                {
                    ObjBLL.Update();
                    if (ObjBLL.SaveChanges())
                        msg("alteração efetuada");
                    else
                        msgError("erro alteração");
                    Get();
                }
                else
                    msgError("Não é permitido alterar uma cobrança com data diferente do ano atual");
            else
                msgError("Somente quem criou a cobrança pode alterá-la");
        }
        protected override void btExcluir_Click(object sender, EventArgs e)
        {
            ObjBLL.Get(PkValue);
            if (ObjBLL.MesmoUsuario())
                if (ObjBLL.MesmoAno())
                {
                    ObjBLL.Delete();
                    if (ObjBLL.SaveChanges())
                        msg("exclusão efetuada");
                    else
                        msgError("erro exclusão");
                    Get();
                    SetAdd();
                }
                else
                    msgError("Não é permitido excluir uma cobrança com data diferente do ano atual");
            else
                msgError("Somente quem criou a cobrança pode excluí-la");
        }


        protected void ProcessUpload(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
        {
            Session["file"] = AsyncFileUpload1.PostedFile;
        }

        protected void ddlAno_SelectedIndexChanged(object sender, EventArgs e)
        {

            SetGrid();
            SetView();
        }

        protected override void SetGrid()
        {
            Filter f = new Filter()
            {
                property = "data.Year",
                value = this.ddlAno.SelectedValue,
                mode = "=="

            };
            filtros.Add(f);
            this.grid.Caption = String.Format("Lista de Cobranças de {0}", f.value);
            base.SetGrid();
            filtros.Remove(f);
        }

        protected void lkAddFile_Click(object sender, EventArgs e)
        {
            this.AsyncFileUpload1.Visible = true;
            this.spanUploading.Visible = true;
        }
    }
}