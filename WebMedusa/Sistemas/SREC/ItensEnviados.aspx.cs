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
    public partial class ItensEnviados : PageCrud<HistoricoEntradaBLL>
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_historico_entrada";
            //valor da chave primária
            //PkValue = Convert.ToInt32(this.txtCodigo.Text);
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
                rbFilter.DataSource = Enum.GetValues(typeof(MeusDocumentos));
                rbFilter.SelectedIndex = 0;
                rbFilter.DataBind();
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
            string f1_Propety;
            string f2_Value;
            if (((MeusDocumentos)Enum.Parse(typeof(MeusDocumentos), rbFilter.SelectedValue)) == MeusDocumentos.enviados)
            {
                f1_Propety = "UsuarioDe.id_setor";
                f2_Value = Util.InteiroToString(StatusEntradaBLL.Encaminhado);
            }
            else
            {
                f1_Propety = "UsuarioPara.id_setor";
                f2_Value = Util.InteiroToString(StatusEntradaBLL.Recebido);
            }


            Filter f = new Filter()
            {
                property = f1_Propety,
                property_name = f1_Propety,
                value = Util.InteiroToString(SecurityBLL.GetCurrentSetor(SecurityBLL.GetCurrentUsuario().id_usuario).id_setor),
                mode = "=="
            };

            grid.Caption = String.Format("itens {0}", rbFilter.SelectedItem.Text);

            Filter f2 = new Filter()
            {
                property = "id_status_entrada",
                property_name = "id_status_entrada",
                value = f2_Value,
                mode = "=="
            };

            filtros.Add(f);
            //filtros.Add(f2);
            
            if (ddlSize.SelectedValue == "0")
                grid.PageSize = 50;
            else
                grid.PageSize = Convert.ToInt32(ddlSize.SelectedValue);

            int size = 10 * Convert.ToInt32(this.ddlSize.SelectedValue);
            grid.DataKeyNames = new string[] { PRIMARY_KEY };
            grid.DataSource = ObjBLL.Find(
                filtros,
                "data",
                "DESC", size);//.OfType<HistoricoEntrada>().Where(it=>it.Entrada.Historicos.OrderByDescending(h=>h.data).First().id_historico_entrada == it.id_historico_entrada).ToList();
            grid.DataBind();

            filtros.Remove(f);
            //filtros.Remove(f2);
        }
    }
}
