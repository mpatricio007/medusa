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
using System.IO;

namespace Medusa.Sistemas.RemessaBancaria
{
    public partial class ArquivosRetornos : PageCrud<ArquivosRetornoBLL>
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_arquivo";
            //valor da chave primária
            PkValue = Convert.ToInt32(this.txtCodigo.Text);
            // painel do grid
            pGrid = panelGrid;
            // painel do formulário de alteração
            pCadastro = panelCadastro;
            // gridview
            _grid = grid;
            lbMsg = new Label();
            _btAlterar = new Button();
            _btAlterar0 = new Button();
            _btInserir = new Button();
            _btInserir0 = new Button();
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
            }
        }

        protected override void Get()
        {
            ObjBLL.Get(PkValue);
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_arquivo);
            this.lblDataProcessado.Text = Util.DateToString(ObjBLL.ObjEF.data_processado);
            this.lblCodBanco.Text = Convert.ToString(ObjBLL.ObjEF.codigo_banco);
            this.lblDataCriacao.Text = Util.DateToString(ObjBLL.ObjEF.data_criacao);
            this.lblTipoConc.Text = ObjBLL.ObjEF.TipoConciliacao.nome;
            this.lblLog.Text = ObjBLL.ObjEF.log_importacao;
            this.lblTipoArquivo.Text = ObjBLL.ObjEF.TipoArquivo.nome;
            this.lblUsuario.Text = ObjBLL.ObjEF.Usuario.PessoaFisica.nome;
            this.cTextoObs.Text = ObjBLL.ObjEF.obs;
            btCancelProcessamento.Visible = ObjBLL.ObjEF.status.GetValueOrDefault();
        }

        protected override void Set()
        {
        }

        protected void btConciliar_Click(object sender, EventArgs e)
        {
            panelConciliacao.Visible = true;
            panelCadastro.Visible = panelGrid.Visible = false;
            SetGridNaoProcessados();
        }

        private void SetGridNaoProcessados()
        {
            gridNaoProcessados.DataSource = ObjBLL.GetArquivosNaoProcessados();
            gridNaoProcessados.DataBind();
        }

        protected override void SetView()
        {
            base.SetView();
            panelConciliacao.Visible = false;
        }

        protected override void SetAdd()
        {
            base.SetAdd();
            panelConciliacao.Visible = false;
        }

        protected override void SetModify()
        {
            base.SetModify();
            panelConciliacao.Visible = false;
        }

        protected void btProcessar_Click(object sender, EventArgs e)
        {
            ObjBLL.ProcessarArquivos();
            SetView();
            SetGrid();
            Util.ChamarScript("hideProgressDialog();","hide");
        }

        protected void btCancelProcessamento_Click(object sender, EventArgs e)
        {
            ObjBLL.Get(PkValue);
            ObjBLL.ObjEF.obs = this.cTextoObs.Text;
            ObjBLL.CancelarProcessamento();
            Get();
            Util.ChamarScript("hideProgressDialog();", "hide");
        }
    }

}