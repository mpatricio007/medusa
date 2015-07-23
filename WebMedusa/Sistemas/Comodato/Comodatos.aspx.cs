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
using Medusa.Relatorios.REComodatos;
using Microsoft.Reporting.WebForms;

namespace Medusa.Sistemas.Comodato
{
    public partial class Comodatos : PageCrud<ComodatoBLL>
    {

        Dictionary<string, string> dicSearch = new Dictionary<string, string>()
        {
            {"financiador(es)","Financiadores.Financiador.nome"}
        };

        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_comodato";
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

                FinanciadorBLL f = new FinanciadorBLL();
                listaFinanciador.DataSource = f.GetAll("nome");
                listaFinanciador.Items.Insert(0, new ListItem("selecione um financiador...", "0"));
                listaFinanciador.DataBind();
            }
        }

        protected override void Get()
        {
            ObjBLL.Get(PkValue);
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_comodato);
            this.cTextoNumComodato.Text = ObjBLL.ObjEF.num_comodato;
            this.cDdlProjeto1.Id_projeto = ObjBLL.ObjEF.id_projeto;
            this.AsyncFileUpload1.Visible = false;
            this.spanUploading.Visible = false;
            this.lbArquivo.Text = ObjBLL.ObjEF.arquivo;

            cDdlProjeto1_SelectedIndexChanged(null, null);

            ControlePatrimonio1.Id_comodato = ObjBLL.ObjEF.id_comodato;
            ControlePatrimonio1.DataBind();

            ControleHistoricoComodato1.Id_comodato = ObjBLL.ObjEF.id_comodato;
            ControleHistoricoComodato1.DataBind();

            btImprimir.Visible = !panelGrid.Visible;

            btImprimir.Visible = ObjBLL.ObjEF.id_comodato != 0;
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_comodato = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.id_projeto = this.cDdlProjeto1.Id_projeto;
            ObjBLL.ObjEF.num_comodato = this.cTextoNumComodato.Text;
            ObjBLL.Up.file = (HttpPostedFile)Session["file"];
            Session.Remove("file");
           

            //ObjBLL.ObjEF.projetos.cod_def_projeto

            //ObjBLL.ObjEF.tiposAdiantamento = this.DdlTiposAdiantamentos1.Id_tipo_admto;
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
   
        protected void lkAddFile_Click(object sender, EventArgs e)
        {
            this.AsyncFileUpload1.Visible = true;
            this.spanUploading.Visible = true;
        }

        protected void SetPrint()
        {
            pGrid.Visible = false;
            pCadastro.Visible = false;
        }

        protected override void SetAdd()
        {
            panelPatrimonio.Visible = false;
            //ControlePatrimonio1.Visible = false;
            ControleHistoricoComodato1.Visible = false;
            btImprimir.Visible = false;
            base.SetAdd();
        }

        protected override void SetModify()
        {
            //ControlePatrimonio1.Visible = true;
            panelPatrimonio.Visible = true;
            btImprimir.Visible = true;
            ControleHistoricoComodato1.Visible = true;
            base.SetModify();
        }

        protected override void SetView()
        {
            base.SetView();
            panelPatrimonio.Visible = false;
            ControleHistoricoComodato1.Visible = false;
            btImprimir.Visible = false;
        }

        protected override void btInserir_Click(object sender, EventArgs e)
        {
            Set();
            ObjBLL.Add();
                if (ObjBLL.SaveChanges())
                {
                    msg("inclusão efetuada");
                    PkValue = ObjBLL.ObjEF.id_comodato;
                    ObjBLL.Detach();
                    Get();
                    SetModify();
                }
                else
                    msgError("erro inclusão");
      
        }
        protected void cDdlProjeto1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            var proj = new ProjetoBLL();
            proj.Get(cDdlProjeto1.Id_projeto);
            //lbUndiade.Text = proj.ObjEF.id_unidade.HasValue ? proj.ObjEF.Unidade.nome : String.Empty;
            GridPatrocinadores.DataSource = proj.ObjEF.Financiadores;
            GridPatrocinadores.DataBind();

        }

        protected void btImprimir_Click(object sender, ImageClickEventArgs e)
        {
            Util.NovaJanela(String.Format("ReportPatrimonio.aspx?pk={0}", PkValue.ToString().Criptografar()));
        }

        protected override void btProcurar_Click(object sender, EventArgs e)
        {
            int id_financiador = 0;


            if (!_ckFilter.Checked)
                filtros.Clear();

            switch (ddlOptions.SelectedValue)
            {
                case ("financiador"):
                    {
                        int.TryParse(listaFinanciador.SelectedValue, out id_financiador);
                        break;
                    }

                default:
                    {
                        if (!String.IsNullOrEmpty(this._txtProcura.Text))
                            filtros.Add(setFilter());
                        break;
                    }
            }


            SetView();


            if (_ddlSize.SelectedValue == "0")
                _grid.PageSize = 50;
            else
                _grid.PageSize = Convert.ToInt32(_ddlSize.SelectedValue);


            int size = 10 * Convert.ToInt32(this._ddlSize.SelectedValue);
            _grid.DataKeyNames = new string[] { PRIMARY_KEY };
            _grid.DataSource = ObjBLL.Find(
                filtros,
                (string)ViewState["SortExpression"],
                (string)ViewState["SortDirection"], size,
                id_financiador);

            _grid.DataBind();
            DataListFiltros.DataBind();
            this._txtProcura.Text = String.Empty;
        }

        protected override void ddlOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddlOptions.SelectedValue)
            {
                case ("financiador"):
                    {
                        GetModes(typeof(string));
                        listaFinanciador.Visible = true;
                        ddlMode.Visible = txtProcura.Visible = false;
                        break;
                    }
                default:
                    {
                        listaFinanciador.SelectedValue = "0";
                        ddlMode.Visible = txtProcura.Visible = true;
                        listaFinanciador.Visible = false;
                        base.ddlOptions_SelectedIndexChanged(sender, e);
                        break;
                    }
            }
        }
            }
}