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

namespace Medusa.Sistemas.Devolucao
{
    public partial class Motivos : PageCrud<MotivoBLL>
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_motivo";
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


                ddlMotivos.DataSource = ObjBLL.Find(it =>it.id_motivo != (int)PkValue);

                ddlMotivos.Items.Insert(0, new ListItem("selecione um motivo...", "0"));
                ddlMotivos.DataBind();
            }
        }

        protected override void Get()
        {
            ObjBLL.Get(PkValue);
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_motivo);
            this.cTextoCodigo.Text = ObjBLL.ObjEF.codigo;
            this.cTextoDescricaoMotivo.Text = ObjBLL.ObjEF.descricao_motivo;
            this.rdSimNao.SelectedValue = Convert.ToString(ObjBLL.ObjEF.temOutros);
            this.ddlMotivos.SelectedValue = Util.InteiroToString(ObjBLL.ObjEF.id_motivo_pai.GetValueOrDefault(0));
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_motivo = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.codigo = this.cTextoCodigo.Text;
            ObjBLL.ObjEF.descricao_motivo = this.cTextoDescricaoMotivo.Text;
            ObjBLL.ObjEF.temOutros = Convert.ToBoolean(this.rdSimNao.SelectedValue);
            ObjBLL.ObjEF.id_motivo_pai = Util.StringToInteiroOrNullable(this.ddlMotivos.SelectedValue.ToString());

        }

       
    }
}