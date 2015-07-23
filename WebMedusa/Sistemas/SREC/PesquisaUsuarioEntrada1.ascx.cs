using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.BLL;
using Medusa.DAL;
using Medusa.LIB;

namespace Medusa.Sistemas.SREC
{
    public partial class PesquisaUsuarioEntrada1 : PagePesquisa<UsuarioFuspBLL>
    {
        public IQueryable<UsuarioFusp> Retorno
        {
            get
            {
                return ObjBLL.Find(
                filtros,
                (string)ViewState["SortExpression"],
                (string)ViewState["SortDirection"], 0).OfType<UsuarioFusp>().AsQueryable();
            }
        }

        public string ValidationGroup
        {
            get
            {
                return DdlUsuariosFuspEntradas1.ValidationGroup;
            }
            set
            {
                cDdlSetorEntrada1.ValidationGroup = DdlUsuariosFuspEntradas1.ValidationGroup = value;
            }
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_usuario";
            // painel do grid
            pGrid = panelCadastro;
            // gridview
            _grid = new GridView();
            _ddlSize = ddlSize;
            _ddlMode = ddlMode;
            _ddlOptions = ddlOptions;
            _txtProcura = txtProcura;
            _btProcurar = btSearch;
            _ckFilter = ckFilter;
            _dataListFiltros = DataListFiltros;

            if (!IsPostBack)
            {
                filtros.Clear();
                base.Page_Load(sender, e);
                CarregarTipoBusca();
            }
        }

        public void CarregarTipoBusca()
        {
            rbTipoBusca.Items.Clear();
            filtros.Clear();
            rbTipoBusca.DataSource = Enum.GetValues(typeof(TipoBuscaUsu));
            rbTipoBusca.DataBind();
            rbTipoBusca.SelectedIndex = 0;
            rbTipoBusca_SelectedIndexChanged(null, null);
        }

        protected override void SetGrid()
        {
        }

        protected void rbTipoBusca_SelectedIndexChanged(object sender, EventArgs e)
        {
            cDdlSetorEntrada1.Visible = (TipoBuscaUsu)Enum.Parse(typeof(TipoBuscaUsu), rbTipoBusca.SelectedValue) == TipoBuscaUsu.setor;
            DdlUsuariosFuspEntradas1.Visible = !cDdlSetorEntrada1.Visible;
        }

        protected void DdlUsuariosFuspEntradas1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            filtros.Clear();
            var f = new Filter()
            {
                property = "id_usuario",
                value = Util.InteiroToString(DdlUsuariosFuspEntradas1.Id_usuario),
                mode = "=="
            };
            filtros.Add(f);
            cDdlSetorEntrada1.DataBind();
        }

        protected void cDdlSetorEntrada1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            filtros.Clear();
            var f = new Filter()
            {
                property = "id_setor",
                value = Util.InteiroToString(cDdlSetorEntrada1.Id_Setor),
                mode = "=="
            };
            filtros.Add(f);
            DdlUsuariosFuspEntradas1.DataBind();
        }

        public override void DataBind()
        {
            CarregarTipoBusca();
            cDdlSetorEntrada1.DataBind();
            DdlUsuariosFuspEntradas1.DataBind();
            rbTipoBusca_SelectedIndexChanged(null, null);
        }
    }

    public enum TipoBuscaUsu
    {
        setor,
        usuário
    }
}