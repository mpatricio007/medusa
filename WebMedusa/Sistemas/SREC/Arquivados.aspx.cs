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
    //public partial class Arquivados : PageCrud<HistoricoEntradaBLL>
    //{
    //    protected override void Page_Load(object sender, EventArgs e)
    //    {
    //        // chave primária da tabela
    //        PRIMARY_KEY = "id_historico_entrada";
    //        //valor da chave primária
    //        PkValue = Convert.ToInt32(this.txtCodigo.Text);
    //        // painel do grid
    //        pGrid = panelGrid;
    //        // painel do formulário de alteração
    //        pCadastro = new Panel();
    //        // gridview
    //        _grid = grid;
    //        lbMsg = new Label();
    //        _btAlterar = new Button();
    //        _btAlterar0 = new Button();
    //        _btInserir = new Button();
    //        _btInserir0 = new Button();
    //        _btExcluir = new Button();
    //        _btExcluir0 = new Button();
    //        _ddlSize = ddlSize;
    //        _ddlMode = ddlMode;
    //        _ddlOptions = ddlOptions;
    //        _txtProcura = txtProcura;
    //        _btProcurar = btSearch;
    //        _ckFilter = ckFilter;
    //        _dataListFiltros = DataListFiltros;

    //        if (!IsPostBack)
    //        {
    //            base.Page_Load(sender, e);
    //        }
    //    }

    //    protected override void Get()
    //    {
    //        ObjBLL.Get(PkValue);
    //        this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_historico_entrada);
    //    }

    //    protected override void Set()
    //    {
    //        ObjBLL.Get(PkValue);
    //        ObjBLL.ObjEF.data = DateTime.Now;
    //        ObjBLL.ObjEF.id_status_entrada = StatusEntradaBLL.EmTransito;
    //        ObjBLL.ObjEF.id_usuario_de = SecurityBLL.GetCurrentUsuario().id_usuario;
    //        ObjBLL.ObjEF.id_usuario_para = cDdlUsuarioFUSP1.Id_usuario;
    //        ObjBLL.ObjEF.obs = cTextoUltimaObs.Text;
    //        ObjBLL.ObjEF.id_entrada = ObjBLL.ObjEF.id_entrada;
    //        ObjBLL.ObjEF.Entrada.id_ultimo_status = ObjBLL.ObjEF.id_status_entrada;
    //    }

    //    protected override void grid_RowEditing(object sender, GridViewEditEventArgs e)
    //    {
    //        PkValue = grid.DataKeys[e.NewEditIndex][PRIMARY_KEY];
    //        Get();
    //        grid.DataBind();
    //        SetModify();
    //        e.Cancel = true;
    //        Util.ChamarScript("openDialog();", "");
    //    }

    //    protected override void btInserir_Click(object sender, EventArgs e)
    //    {
    //        base.btInserir_Click(sender, e);
    //        Util.ChamarScript("closeDialog();", "");
    //        ObjBLL.Detach();
    //    }

    //    protected override void SetGrid()
        
    //    {

    //        Filter f = new Filter()
    //        {
    //            property = "id_status_entrada",
    //            property_name = "id_status_entrada",
    //            value = Util.InteiroToString(StatusEntradaBLL.Arquivado),
    //            mode = "=="
    //        };
            
    //        Filter f2 = new Filter()
    //        {
    //            property = "UsuarioDe.id_setor",
    //            property_name = "UsuarioDe.id_setor",
    //            value = Util.InteiroToString(SecurityBLL.GetCurrentSetor(SecurityBLL.GetCurrentUsuario().id_usuario).id_setor),
    //            mode = "=="
    //        };

    //        filtros.Add(f);
    //        filtros.Add(f2);

    //        if (ddlSize.SelectedValue == "0")
    //            grid.PageSize = 50;
    //        else
    //            grid.PageSize = Convert.ToInt32(ddlSize.SelectedValue);

    //        int size = 10 * Convert.ToInt32(this.ddlSize.SelectedValue);
    //        grid.DataKeyNames = new string[] { PRIMARY_KEY };
    //        grid.DataSource = ObjBLL.Find(filtros,"data", "DESC", size).OfType<HistoricoEntrada>()
    //                .Where(it=>it.id_historico_entrada == it.Entrada.Historicos.OrderByDescending(h=>h.data).FirstOrDefault().id_historico_entrada).ToList();
    //        grid.DataBind();

    //        filtros.Remove(f);
    //        filtros.Remove(f2);
    //    }
    //}
    public partial class Arquivados : PageCrud<EntradaBLL>
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_entrada";
            //valor da chave primária
            PkValue = Convert.ToInt32(this.txtCodigo.Text);
            // painel do grid
            pGrid = panelGrid;
            // painel do formulário de alteração
            pCadastro = new Panel();
            // gridview
            _grid = grid;
            lbMsg = new Label();
            _btAlterar = new Button();
            _btAlterar0 = new Button();
            _btInserir = new Button();
            _btInserir0 = new Button();
            _btExcluir = new Button();
            _btExcluir0 = new Button();
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
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_entrada);
        }

        protected override void Set()
        {
        }

        protected override void SetAdd()
        {
            SetView();
        }
        protected override void SetModify()
        {
            SetView();
        }

        protected override void grid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            PkValue = grid.DataKeys[e.NewEditIndex][PRIMARY_KEY];
            Get();
            Util.ChamarScript("openDialog();", "");
            e.Cancel = true;
        }

        protected override void btInserir_Click(object sender, EventArgs e)
        {
            base.btInserir_Click(sender, e);
            Util.ChamarScript("closeDialog();", "");
            ObjBLL.Detach();
        }

        protected override void SetGrid()
        {
            Filter f = new Filter()
            {
                property = "id_ultimo_status",
                property_name = "id_ultimo_status",
                value = Util.InteiroToString(StatusEntradaBLL.Arquivado),
                mode = "=="
            };
            Filter f2 = new Filter()
            {
                property = "UltimoPara.id_setor",
                property_name = "UltimoPara.id_setor",
                value = Util.InteiroToString(SecurityBLL.GetCurrentSetor(SecurityBLL.GetCurrentUsuario().id_usuario).id_setor),
                mode = "=="
            };

            filtros.Add(f);
            filtros.Add(f2);

            base.SetGrid();
            grid.DataBind();

            filtros.Remove(f);
            filtros.Remove(f2);
        }

        protected void btOk_Click(object sender, EventArgs e)
        {
            ObjBLL.Get(PkValue);
            var histEntradaBLL = new HistoricoEntradaBLL();
            histEntradaBLL.ObjEF.data = DateTime.Now;
            histEntradaBLL.ObjEF.id_status_entrada = StatusEntradaBLL.Encaminhado;
            histEntradaBLL.ObjEF.id_usuario_de = SecurityBLL.GetCurrentUsuario().id_usuario;
            histEntradaBLL.ObjEF.id_usuario_para = cDdlUsuarioFUSP1.Id_usuario;
            histEntradaBLL.ObjEF.obs = cTextoUltimaObs.Text;
            histEntradaBLL.ObjEF.id_entrada = ObjBLL.ObjEF.id_entrada;
            //ObjBLL.ObjEF.id_ultimo_status = histEntradaBLL.ObjEF.id_status_entrada;
            histEntradaBLL.Add();
            histEntradaBLL.SaveChanges();
            ObjBLL.Detach();
            histEntradaBLL.Detach();
            Util.ChamarScript("closeDialog();", "");
        }
    }
}
