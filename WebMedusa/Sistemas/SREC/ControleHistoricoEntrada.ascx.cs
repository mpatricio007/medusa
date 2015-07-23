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
    public partial class ControleHistoricoEntrada : ControlCrud<HistoricoEntradaBLL>
    {
        public int? Id_entrada
        {
            get
            {
                if (ViewState[ID] == null)
                    ViewState[ID] = 0;
                return (int)ViewState[ID];
            }
            set
            {
                ViewState[ID] = value;
            }
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_historico_entrada";
            //valor da chave primária
            //PkValue = Convert.ToInt32(this.txtCodigo.Text);
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
            _ddlSize = new DropDownList();
            _ddlMode = new DropDownList();
            _ddlOptions = new DropDownList();
            _txtProcura = new TextBox();
            _btProcurar = new Button();
            _ckFilter = new CheckBox();
            _dataListFiltros = new DataList();

            //id_hist_admto = Convert.ToInt32(this.txtCodigo.Text);
            if (!IsPostBack)
            {
                SetGrid();
            }
        }

        protected override void Get()
        {
        }

        protected override void Set()
        {
        }

        protected override void SetGrid()
        {
            
            ObjBLL.Detach();
            var f = new Filter()
            {
                property = "id_entrada",
                value = Convert.ToString(Id_entrada.GetValueOrDefault()),
                mode = "=="
            };
            filtros.Add(f);
            grid.DataKeyNames = new string[] { PRIMARY_KEY };
            grid.DataSource = ObjBLL.Find(filtros,
               PRIMARY_KEY,
               "DESC", 0);
            grid.DataBind();
            filtros.Remove(f);
        }

        public override void DataBind()
        {
            SetGrid();
            base.DataBind();
        }
    }
}