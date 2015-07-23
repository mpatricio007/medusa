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
using Medusa.Relatorios.Conciliacao;
using Microsoft.Reporting.WebForms;

namespace Medusa.Sistemas.Conciliacao
{
    public partial class ImportacaoDeDados : PageCrud<ImportaArquivoBLL>
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_imparq";
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
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_imparq);
            this.cDdlTipoImportacao1.Id_tipo = ObjBLL.ObjEF.id_tipoimp;           
            this.cData1.Value = ObjBLL.ObjEF.data;
            this.cData2.Value = ObjBLL.ObjEF.data_importacao;          
        }

        protected override void Set()
        {
            
        }

        protected override void SetAdd()
        {
            lbMsg.Text = String.Empty;
            pGrid.Visible = false;
            pCadastro.Visible = true;
            _btInserir.Visible = false;
            _btInserir0.Visible = false;
            _btAlterar.Visible = false;
            _btAlterar0.Visible = false;
            _btExcluir.Visible = true;
            _btExcluir0.Visible = true;
        }

        protected override void SetModify()
        {
            lbMsg.Text = String.Empty;
            pCadastro.Visible = true;
            pGrid.Visible = false;
            _btInserir.Visible = false;
            _btInserir0.Visible = false;
            _btAlterar.Visible = false;
            _btAlterar0.Visible = false;
            _btExcluir.Visible = true;
            _btExcluir0.Visible = true;
        }

        protected override void btExcluir_Click(object sender, EventArgs e)
        {
            ObjBLL.Get(PkValue);
            ObjBLL.Delete();
            if (ObjBLL.SaveChanges())
                msg("exclusão efetuada");
            else
                msgError("erro exclusão");
            SetGrid();
            SetView();
        }

        protected void btImprimir_Click(object sender, EventArgs e)
        {
            Session["id_imparq"] = this.txtCodigo.Text;
            Response.Redirect("../../Relatorios/Conciliacao/RelLanctoImportado.aspx");//?id={0}&nome={1}",this.txtCodigo.Text,string.Format("tipo: {0} data: {1:dd/MM/yyyy}",this.cDdlTipoImportacao1.Texto,this.cData1.Value)));
         
            
        }
    }
}