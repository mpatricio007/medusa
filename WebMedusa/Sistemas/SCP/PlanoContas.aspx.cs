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

namespace Medusa.Sistemas.SCP
{
    public partial class PlanoContas : PageCrud<PlanoContaBLL>
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_plano_conta";
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
                rbClassificacao.DataSource = Enum.GetNames(typeof(ClassificacaoPC));
                rbClassificacao.DataBind();
            }
        }

        protected override void Get()
        {
            ObjBLL.Get(PkValue);
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_plano_conta);
            cTextoCodigo.Text = ObjBLL.ObjEF.codigo;
            this.cTextoClasse.Text = ObjBLL.ObjEF.classe;
            this.cTextoItem.Text = ObjBLL.ObjEF.item;
            cTextoSubItem.Text = ObjBLL.ObjEF.sub_item;
            cTextoContaContabil.Text = ObjBLL.ObjEF.conta_contabil;
            rbClassificacao.SelectedValue = ObjBLL.ObjEF.Classificacao == 0 ? Convert.ToString(ClassificacaoPC.outros) : Convert.ToString(ObjBLL.ObjEF.Classificacao);
            //if (ObjBLL.ObjEF.classificacao != 0)
            //    rbClassificacao.SelectedValue = Convert.ToString(ObjBLL.ObjEF.classificacao);
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_plano_conta = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.codigo = cTextoCodigo.Text;
            ObjBLL.ObjEF.classe = this.cTextoClasse.Text;
            ObjBLL.ObjEF.item = this.cTextoItem.Text;
            ObjBLL.ObjEF.sub_item = this.cTextoSubItem.Text;
            ObjBLL.ObjEF.conta_contabil = this.cTextoContaContabil.Text;
            ObjBLL.ObjEF.Classificacao = (ClassificacaoPC)Enum.Parse(typeof(ClassificacaoPC), rbClassificacao.SelectedValue);
        }

        protected override void SetAdd()
        {
            base.SetAdd();
            rbClassificacao.DataBind();
        }

        protected override void SetModify()
        {
            base.SetModify();
            rbClassificacao.DataBind();
        }
    }
}