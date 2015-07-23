using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.LIB;
using Medusa.BLL;
using Medusa.DAL;

namespace Medusa.Sistemas.Bolsa
{
    public partial class ControleContratoBolsa : ControlCrud<ContratoBolsaBLL>
    {
        public int Id_pessoa_fisica
        {
            get
            {
                if (ViewState[ID] == null)
                    Id_pessoa_fisica = 0;
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
            PRIMARY_KEY = "id_contrato";
            //valor da chave primária
            PkValue = Convert.ToInt32(this.txtCodigoContrato.Text);
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
            this.txtCodigoContrato.Text = Convert.ToString(ObjBLL.ObjEF.id_contrato);
            this.DdlTipoContratos.Id_tipo_contrato = Convert.ToInt32(ObjBLL.ObjEF.id_tipo_contrato);
            this.cDataInicio.Value = Convert.ToDateTime(ObjBLL.ObjEF.inicio);
            this.cDataTermino.Value = ObjBLL.ObjEF.termino;
            this.cValorContrato.Value = ObjBLL.ObjEF.valor;
            this.cInteiroParcela.Value = ObjBLL.ObjEF.qtde_parcelas;
            this.cDataRescisao.Value = ObjBLL.ObjEF.rescisao;
            this.cTextoDescricao.Text = ObjBLL.ObjEF.descricao;
            this.cTextoObs.Text = ObjBLL.ObjEF.observacao;
            this.cDdlProjeto.Id_projeto = Convert.ToInt32(ObjBLL.ObjEF.id_projeto);
            this.cDataRelatorio.Value = ObjBLL.ObjEF.data_relatorio;
            this.cDataInicioSeg.Value = ObjBLL.ObjEF.inicio_seguro;
            this.cDataTerminoSeg.Value = ObjBLL.ObjEF.termino_seguro;
            this.cDdlBolsas.Id_bolsa = Convert.ToInt32(ObjBLL.ObjEF.id_bolsa);
            this.CheckAtivo.Checked = Convert.ToBoolean(ObjBLL.ObjEF.ativo);
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_contrato = Convert.ToInt32(this.txtCodigoContrato.Text);
            ObjBLL.ObjEF.id_tipo_contrato = DdlTipoContratos.Id_tipo_contrato;
            ObjBLL.ObjEF.inicio = Convert.ToDateTime(this.cDataInicio.Value);
            ObjBLL.ObjEF.termino = this.cDataTermino.Value;
            ObjBLL.ObjEF.valor = Convert.ToDecimal(this.cValorContrato.Value);
            ObjBLL.ObjEF.qtde_parcelas = Convert.ToInt32(this.cInteiroParcela.Value);
            ObjBLL.ObjEF.rescisao = Convert.ToDateTime(this.cDataRescisao.Value);
            ObjBLL.ObjEF.descricao = Convert.ToString(this.cTextoDescricao.Text);
            ObjBLL.ObjEF.observacao = Convert.ToString(this.cTextoObs.Text);
            ObjBLL.ObjEF.id_projeto = this.cDdlProjeto.Id_projeto;
            ObjBLL.ObjEF.data_relatorio = Convert.ToDateTime(this.cDataRelatorio.Value);
            ObjBLL.ObjEF.inicio_seguro = Convert.ToDateTime(this.cDataInicioSeg.Value);
            ObjBLL.ObjEF.termino_seguro = Convert.ToDateTime(this.cDataTerminoSeg.Value);
            ObjBLL.ObjEF.id_bolsa = Convert.ToInt32(this.cDdlBolsas.Id_bolsa);
            ObjBLL.ObjEF.ativo = this.CheckAtivo.Checked;
            ObjBLL.ObjEF.id_pessoa = Id_pessoa_fisica;
        }

        protected override void SetGrid()
        {
            Filter f = new Filter()
            {
                property = "id_pessoa",
                value = Convert.ToString(Id_pessoa_fisica),
                mode = "=="

            };
            filtros.Add(f);            
            base.SetGrid();
            filtros.Remove(f);
        }

        public override void DataBind()
        {
            SetView();
            SetGrid();
            base.DataBind();
        }
    }
}