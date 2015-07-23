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

namespace Medusa.Sistemas.SREC
{
    public partial class Providencias : PageCrud<ProvidenciaBLL>
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_providencia";
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
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_providencia);
            this.cTextoNome.Text = ObjBLL.ObjEF.nome;
            this.cDdlStatusEntrada1.Id_status_entrada = ObjBLL.ObjEF.id_status_final;
            SetorsCompetentes1.Id_providencia = ObjBLL.ObjEF.id_providencia;
            SetorsCompetentes1.Value = ObjBLL.ObjEF.SetoresCompetentes.ToList();
            SetorsCompetentes1.DataBind();
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_providencia = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.nome = this.cTextoNome.Text;
            ObjBLL.ObjEF.id_status_final = this.cDdlStatusEntrada1.Id_status_entrada.GetValueOrDefault();

            ObjBLL.oldSetoresCompetentes = ObjBLL.ObjEF.SetoresCompetentes.ToList();
            ObjBLL.oldSetoresCompetentes.ForEach(it => ObjBLL.ObjEF.SetoresCompetentes.Remove(it));

            ObjBLL.ObjEF.SetoresCompetentes = SetorsCompetentes1.Value;
        }

        protected override void btInserir_Click(object sender, EventArgs e)
        {
            Set();
            ObjBLL.Add();
            if (ObjBLL.SaveChanges())
            {
                msg("inclusão efetuada");
                PkValue = ObjBLL.ObjEF.id_providencia;
                ObjBLL.Detach();
                Get();
                SetModify();
            }
            else
                msgError("erro inclusão");
        }

        protected override void btAlterar_Click(object sender, EventArgs e)
        {
            ObjBLL.Get(PkValue);
            Set();
            ObjBLL.Update();
            if (ObjBLL.SaveChanges())
                msg("alteração efetuada");
            else
                msgError("erro alteração");
            Get();
        }
    }
}