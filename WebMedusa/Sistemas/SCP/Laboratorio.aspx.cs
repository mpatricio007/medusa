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
    public partial class Laboratorio : PageCrud<LaboratorioBLL>
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_laboratorio";
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
                UnidadeBLL u = new UnidadeBLL();
                listaUnidade.DataSource = u.GetAll("nome");
                listaUnidade.Items.Insert(0, new ListItem("selecione uma unidade...", "0"));
                listaUnidade.DataBind();
            }
        }

        protected override void Get()
        {
            ObjBLL.Get(PkValue);
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_laboratorio);
            this.cTextoNome.Text = ObjBLL.ObjEF.nome;
            this.cTextoSigla.Text = ObjBLL.ObjEF.sigla;
            this.listaUnidade.SelectedValue = ObjBLL.ObjEF.id_departamento != 0 ? Convert.ToString(ObjBLL.ObjEF.Departamento.id_unidade) : "0";
            listaUnidade_SelectedIndexChanged(null, null);
            this.listaDepto.SelectedValue = Convert.ToString(ObjBLL.ObjEF.id_departamento);
            this.cEnder1.Value = ObjBLL.ObjEF.ender; 
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_laboratorio = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.nome = this.cTextoNome.Text;
            ObjBLL.ObjEF.sigla = this.cTextoSigla.Text;
            ObjBLL.ObjEF.id_departamento = Convert.ToInt32(this.listaDepto.SelectedValue);
            ObjBLL.ObjEF.ender = this.cEnder1.Value;
        }

        protected void listaUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            UnidadeBLL unidadeBLL = new UnidadeBLL();
            unidadeBLL.Get(Convert.ToInt32(this.listaUnidade.SelectedValue));
            listaDepto.Items.Clear();
            listaDepto.DataSource = unidadeBLL.ObjEF.Departamentos.OrderBy(it => it.nome);
            listaDepto.Items.Insert(0, new ListItem("selecione um departamento...", "0"));
            listaDepto.DataBind();
        }




    }
}