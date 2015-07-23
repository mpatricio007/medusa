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

namespace Medusa.Sistemas.Cursos
{
    public partial class Cursos : PageCrud<CursoBLL>
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_curso";
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
            //
            _btProcurar = btSearch;
            _ckFilter = ckFilter;
            _dataListFiltros = DataListFiltros;
            //
            if (!IsPostBack)
            {

                base.Page_Load(sender, e);
            }
        }

        protected override void Get()
        {
            ObjBLL.Get(PkValue);
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_curso);
            this.cTextoNome.Text = ObjBLL.ObjEF.nome;
            this.cTextoInstrucao.Text = ObjBLL.ObjEF.instrucao;
            this.cTextoProjeto.Text = ObjBLL.ObjEF.id_projeto;
            this.DdlContas1.Id_Conta = ObjBLL.ObjEF.id_conta;
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_curso = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.nome = this.cTextoNome.Text;
            ObjBLL.ObjEF.instrucao = this.cTextoInstrucao.Text;
            ObjBLL.ObjEF.id_projeto = this.cTextoProjeto.Text;
            ObjBLL.ObjEF.id_conta = this.DdlContas1.Id_Conta;
        }




    }
}