using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.BLL;
using Medusa.DAL;
using Medusa.LIB;

namespace Medusa.Sistemas.Adiantamentos
{
    public partial class ControleHistoricoAdiantamento : ControlCrud<HistoricoAdiantamentoBLL>
    {
        public int Id_adiantamento
        {
            get
            {
                if (ViewState[ID] == null)
                    Id_adiantamento = 0;
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
            PRIMARY_KEY = "id_hist_admto";
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
                grid.DataBind();
                SetGrid();
            }
        }

        protected override void SetAdd()
        {
            lbMsg.Text = String.Empty;
            btInserir.Visible = true;
            SetGrid();
        }

        protected override void Get()
        {
            ObjBLL.Get(PkValue);
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_hist_admto);
            this.cTextoObs.Text = ObjBLL.ObjEF.observacao;
            this.DdlStatusAdiantamentos.Id_status_admto = ObjBLL.ObjEF.id_status_admto;
            cDdlSetor1.DataBind();
            cDataProrrogacao.Visible = cDdlSetor1.Visible = false;
            
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_hist_admto = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.data = DateTime.Now;
            ObjBLL.ObjEF.observacao = this.cTextoObs.Text;
            ObjBLL.ObjEF.id_status_admto = this.DdlStatusAdiantamentos.Id_status_admto;
            ObjBLL.ObjEF.id_adiantamento = Id_adiantamento;
            ObjBLL.ObjEF.id_setor = cDdlSetor1.Id_setor != 0 ? cDdlSetor1.Id_setor : new Nullable<int>();
            ObjBLL.ObjEF.data_prorrogacao = cDataProrrogacao.Value;
        }      

        protected override void btInserir_Click(object sender, EventArgs e)
        {
            Set();
            ObjBLL.Add();
            if (ObjBLL.SaveChanges())
            {
                msg("inclusão efetuada");
                ObjBLL.Detach();
                PkValue = 0;
                Get();
                SetAdd();
                //var p = (IPageCrud)base.Page;
                //p.GetExternal();
            }
            else
                msgError("erro inclusão");
        }

        protected override void btCancelar_Click(object sender, EventArgs e)
        {
            PkValue = 0;
            Get();
            SetAdd();
        }
        
        protected override void SetGrid()
        {
            ObjBLL.Detach();
            var filtros = new List<Filter>();
            filtros.Add(new Filter()
            {
                property = "id_adiantamento",
                value = Convert.ToString(Id_adiantamento),
                mode = "=="
            });
            grid.DataKeyNames = new string[] { PRIMARY_KEY };
            grid.DataSource = ObjBLL.Find(filtros,
               "id_hist_admto",
               "DESC", 0);
            grid.DataBind();
        }

        public override void DataBind()
        {
            Get();
            SetAdd();
            SetGrid();
            //panelCadastro.Visible = !ObjBLL.EhBloqueado(Id_adiantamento);
            base.DataBind();
        }

        protected void DdlStatusAdiantamentos_SelectedIndexChanged(object sender, EventArgs e)
        {
            var saBLL = new StatusAdiantamentoBLL();
            saBLL.Get(DdlStatusAdiantamentos.Id_status_admto);
            cDdlSetor1.Visible = saBLL.ObjEF.exige_setor.GetValueOrDefault();
            cDataProrrogacao.Visible = saBLL.ObjEF.exige_data.GetValueOrDefault();
        }
    }
}