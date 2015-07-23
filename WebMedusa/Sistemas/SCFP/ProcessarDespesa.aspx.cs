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

namespace Medusa.Sistemas.SCFP
{
    public partial class ProcessarDespesa : PageCrud<ProcessamentoDespesaBLL>
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_precessamento_despesa";
            //valor da chave primária
            PkValue = Convert.ToInt32(this.txtCodigo.Text);
            // painel do grid
            pGrid = panelGrid;
            // painel do formulário de alteração
            pCadastro = panelCadastro;
            // gridview
            _grid = grid;
            lbMsg = lblMsg;
            _btAlterar = new Button();
            _btAlterar0 = new Button();
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
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_precessamento_despesa);
            this.cData1.Value = ObjBLL.ObjEF.data_despesa;
            this.cDdlLancamentoTipo1.Id_lancto_tipo = ObjBLL.ObjEF.id_lancto_tipo;
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_precessamento_despesa = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.data_despesa = this.cData1.Value.GetValueOrDefault();
            ObjBLL.ObjEF.id_lancto_tipo = this.cDdlLancamentoTipo1.Id_lancto_tipo;
        }

        protected override void btInserir_Click(object sender, EventArgs e)
        {
            string erro = "";
            Set();
            if (ObjBLL.ProcessarDespesas(ref erro))
            {
                PkValue = ObjBLL.ObjEF.id_lancto_tipo;
                ObjBLL.Detach();
                SetModify();
                msg(erro);
            }
            else
                msgError(erro);
        }
        
        protected override void SetModify()
        {
            base.SetModify();
            cData1.Enabled = false;
            cDdlLancamentoTipo1.Enabled = false;
        }

        protected override void SetAdd()
        {
            base.SetAdd();
            cData1.Enabled = true;
            cDdlLancamentoTipo1.Enabled = true;
        }

    }
}