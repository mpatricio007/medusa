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

namespace Medusa.Sistemas.SISPS
{
    public partial class Vagas : PageCrud<VagaBLL>
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_vaga";
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
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_vaga);
            this.cTextoDescricao.Text = ObjBLL.ObjEF.descricao_vaga;
            this.cDdlProjeto1.Id_projeto = ObjBLL.ObjEF.id_projeto;
            this.cValor1.Value = ObjBLL.ObjEF.valor_inscricao_vaga;
            this.cDataInicio.Value = ObjBLL.ObjEF.inicio;
            this.cDataTermino.Value = ObjBLL.ObjEF.termino;
            this.cDataVencimento.Value = ObjBLL.ObjEF.data_vencto;
            this.CheckStatus.Checked = ObjBLL.ObjEF.status;
            this.cTextoCodigo.Text = ObjBLL.ObjEF.codigo;
            this.cDdlClassificacaoVaga1.Id_classificacao_vaga = ObjBLL.ObjEF.id_classificacao_vaga;
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_vaga = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.descricao_vaga = this.cTextoDescricao.Text;
            ObjBLL.ObjEF.id_projeto = this.cDdlProjeto1.Id_projeto;
            ObjBLL.ObjEF.valor_inscricao_vaga = Convert.ToDecimal(this.cValor1.Value);
            ObjBLL.ObjEF.inicio = Convert.ToDateTime(this.cDataInicio.Value);
            ObjBLL.ObjEF.termino = Convert.ToDateTime(this.cDataTermino.Value);
            ObjBLL.ObjEF.data_vencto = Convert.ToDateTime(this.cDataVencimento.Value);
            ObjBLL.ObjEF.status = this.CheckStatus.Checked;
            ObjBLL.ObjEF.codigo = this.cTextoCodigo.Text;
            ObjBLL.ObjEF.id_classificacao_vaga = this.cDdlClassificacaoVaga1.Id_classificacao_vaga;
        }




    }
}