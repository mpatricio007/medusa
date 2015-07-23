using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.LIB;
using Medusa.BLL;
using Medusa.DAL;

namespace Medusa.Sistemas.SCFP
{
    public partial class ControleFaixaTaxas : ControlCrud<FaixaTaxasBLL>
    {
        public int Id_tabela
        {
            get
            {
                if (ViewState[ID] == null)
                    Id_tabela = 0;
                return (int)ViewState[ID];
            }
            set
            {
                ViewState[ID] = value;
            }
        }

        public string ValidationGroup
        {
            get
            {
                if (ViewState["ValidationGroup"] == null)
                    ViewState["ValidationGroup"] = "new";
                return (string)ViewState["ValidationGroup"];
            }
            set
            {
                ViewState["ValidationGroup"] = value;
            }
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_faixa_taxa";
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
            _btAlterar0 = btAlterar0;
            _btInserir = new Button();
            _btInserir0 = btInserir0;
            _btExcluir = new Button();
            _btExcluir0 = btExcluir0;
            _ddlSize = new DropDownList();
            _ddlMode = new DropDownList();
            _ddlOptions = new DropDownList();
            _txtProcura = new TextBox();
            _btProcurar = new Button();
            _ckFilter = new CheckBox();
            _dataListFiltros = new DataList();

            if (!IsPostBack)
            {
                btExcluir0.OnClientClick = "return confirm('confirma exclusão?')";
                Security();
            }
        }

        protected override void SetView()
        {
            SetAdd();
        }

        protected override void SetAdd()
        {
            lblMsg.Text = String.Empty;
            panelGrid.Visible = true;
            panelCadastro.Visible = true;
            btInserir0.Visible = true;
            btAlterar0.Visible = false;
            btExcluir0.Visible = false;
        }

        protected override void SetModify()
        {
            lblMsg.Text = String.Empty;
            panelCadastro.Visible = true;
            panelGrid.Visible = true;
            btInserir0.Visible = false;
            btAlterar0.Visible = true;
            btExcluir0.Visible = true;
        }

        protected override void Get()
        {
            ObjBLL.Get(PkValue);
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_faixa_taxa);
            this.cTextoFaixaDe.Text = Util.DecimalToString(ObjBLL.ObjEF.faixa_de);
            this.cTextoFaixaAte.Text = Util.DecimalToString(ObjBLL.ObjEF.faixa_ate);
            this.cTextoMax.Text = ObjBLL.ObjEF.valor_max.HasValue ? Util.DecimalToString(ObjBLL.ObjEF.valor_max) : String.Empty;
            this.cTextoMin.Text = Util.DecimalToString(ObjBLL.ObjEF.vlr_minimo);
            this.cTextoDeducao.Text = Util.DecimalToString(ObjBLL.ObjEF.deducao);
            this.cTextoAliquota.Text = Util.DecimalToString(ObjBLL.ObjEF.aliquota);
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_faixa_taxa = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.faixa_de = Util.StringToDecimal(this.cTextoFaixaDe.Text).GetValueOrDefault();
            ObjBLL.ObjEF.faixa_ate = Util.StringToDecimal(this.cTextoFaixaAte.Text).GetValueOrDefault();
            ObjBLL.ObjEF.vlr_minimo = Util.StringToDecimal(this.cTextoMin.Text).GetValueOrDefault();
            ObjBLL.ObjEF.valor_max = Util.StringToDecimal(this.cTextoMax.Text);
            ObjBLL.ObjEF.deducao = Util.StringToDecimal(this.cTextoDeducao.Text).GetValueOrDefault();
            ObjBLL.ObjEF.aliquota = Util.StringToDecimal(this.cTextoAliquota.Text).GetValueOrDefault();
            ObjBLL.ObjEF.id_tabela = Id_tabela;
        }

        protected override void SetGrid()
        {
            Filter f = new Filter()
            {
                property = "id_tabela",
                value = Util.InteiroToString(Id_tabela),
                mode = "=="
            };
            filtros.Add(f);
            grid.DataKeyNames = new string[] { "id_faixa_taxa" };
            grid.DataSource = ObjBLL.Find(
                filtros,
                "id_faixa_taxa",
                "ASC", 0);
            grid.DataBind();
            filtros.Remove(f);
        }

        public override void DataBind() 
        {
            base.DataBind();
            SetGrid();
            SetAdd();
        }

        protected override void btCancelar_Click(object sender, EventArgs e)
        {
            PkValue = 0;
            Get();
            SetAdd();
        }

        protected override void btInserir_Click(object sender, EventArgs e)
        {
            string strMsg = String.Empty;
            if (ObjBLL.DataIsValid(cTextoFaixaDe.Text, cTextoFaixaAte.Text, cTextoMax.Text, cTextoMin.Text, cTextoDeducao.Text, cTextoAliquota.Text, ref strMsg))
            {
                Set();
                ObjBLL.Add();
                if (ObjBLL.SaveChanges())
                {
                    msg("inclusão efetuada");
                    PkValue = 0;
                    ObjBLL.Detach();
                    Get();
                    SetGrid();
                }
                else
                    msgError("erro inclusão");
            }
            else
                msgError(strMsg);
        }

        protected override void btAlterar_Click(object sender, EventArgs e)
        {
            ObjBLL.Get(PkValue);
            Set();
            ObjBLL.Update();
            string strMsg = String.Empty;
            if (ObjBLL.DataIsValid(cTextoFaixaDe.Text, cTextoFaixaAte.Text, cTextoMax.Text, cTextoMin.Text, cTextoDeducao.Text, cTextoAliquota.Text, ref strMsg))
            {
                if (ObjBLL.SaveChanges())
                {
                    msg("alteração efetuada");
                    PkValue = ObjBLL.ObjEF.id_faixa_taxa;
                    ObjBLL.Detach();
                    Get();
                    SetGrid();
                }
                else
                    msgError("erro alteração");
            }
            else
                msgError(strMsg);
        }

        protected override void btExcluir_Click(object sender, EventArgs e)
        {
            ObjBLL.Get(PkValue);
            ObjBLL.Delete();
            if (ObjBLL.SaveChanges())
            {
                msg("exclusão efetuada");
                PkValue = 0;
                ObjBLL.Detach();
                Get();
                SetGrid();
            }
            else
                msgError("erro exclusão");
        }
    }
}