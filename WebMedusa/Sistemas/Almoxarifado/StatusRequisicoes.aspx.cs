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

namespace Medusa.Sistemas.Almoxarifado
{
    public partial class StatusRequisicoes : PageCrud<StatusRequisicaoBLL>
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_status_requisicao";
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
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_status_requisicao);
            this.cTextoNome.Text = ObjBLL.ObjEF.nome;
            this.cIntOrdem.Value = ObjBLL.ObjEF.ordem;
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_status_requisicao = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.nome = this.cTextoNome.Text;
            ObjBLL.ObjEF.ordem = this.cIntOrdem.Value;
        }

        protected override void btInserir_Click(object sender, EventArgs e)
        {
            Set();
            if (ObjBLL.DataIsValid())
            {
                ObjBLL.Add();
                if (ObjBLL.SaveChanges())
                {
                    msg("inclusão efetuada");
                    PkValue = 0;
                    ObjBLL.Detach();
                    Get();

                }
                else
                    msgError("erro alteração");
            }
            else
                msgError("erro inclusão");
        }

        protected override void btAlterar_Click(object sender, EventArgs e)
        {
            ObjBLL.Get(PkValue);
            Set();
            if (ObjBLL.DataIsValid())
            {
                ObjBLL.Update();
                if (ObjBLL.SaveChanges())
                    msg("alteração efetuada");
                else
                    msgError("erro alteração");
            }
            else
                msgError("erro alteração");
        }

        protected override void btExcluir_Click(object sender, EventArgs e)
        {
            ObjBLL.Get(PkValue);
            if (ObjBLL.DataIsValid())
            {
                ObjBLL.Delete();
                if (ObjBLL.SaveChanges())
                {
                    msg("exclusão efetuada");
                    Get();
                    SetAdd();
                }
                else
                    msgError("erro alteração");
            }
            else
                msgError("erro exclusão");
        }

        protected override void btCancelar_Click(object sender, EventArgs e)
        {
            SetGrid();
            SetView();
        }
    }
}