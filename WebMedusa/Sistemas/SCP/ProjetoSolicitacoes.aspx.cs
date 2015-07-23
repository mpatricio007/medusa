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
using Microsoft.Reporting.WebForms;
using Medusa.Relatorios.Projeto;

namespace Medusa.Sistemas.SCP
{
    public partial class ProjetoSolicitacoes : PageCrud<ProjetoSolicitacaoBLL>
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_sol_proj";
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
            ControleTornarPA1.Click += btTornarA_Click;
            if (!IsPostBack)
            {
                base.Page_Load(sender, e);

                TipoSolicitacaoBLL ts = new TipoSolicitacaoBLL();
                cDdlTipoSolicitacao.DataSource = ts.GetAll("nome");
                cDdlTipoSolicitacao.DataBind();
                
                UnidadeBLL u = new UnidadeBLL();
                listaUnidade.DataSource = u.GetAll("nome");
                listaUnidade.Items.Insert(0, new ListItem("selecione uma unidade...", "0"));
                listaUnidade.DataBind();
            }
        }

        protected override void Get()
        {
            ObjBLL.Get(PkValue);
            this.ControleTornarPA1.Id_solicitacao = ObjBLL.ObjEF.id_sol_proj;
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_sol_proj);
            this.cTextoDescricao.Text = ObjBLL.ObjEF.descricao;
            this.txtCoordenador.Text = ObjBLL.ObjEF.strCoordenador;
            this.txtSubCoordenador.Text = ObjBLL.ObjEF.strSubCoordenador;
            this.txtFinanciador.Text = ObjBLL.ObjEF.strFinanciador;
            this.cTextoTitulo.Text = ObjBLL.ObjEF.titulo;
            this.txtDataAbertura.Text = ObjBLL.ObjEF.data_solicitacao != DateTime.MinValue ? Util.DateToString(ObjBLL.ObjEF.data_solicitacao) : Util.DateToString(DateTime.Now);
            this.txtSolicitante.Text = ObjBLL.ObjEF.id_usuario != 0 ? ObjBLL.ObjEF.Usuario.PessoaFisica.nome : SecurityBLL.GetCurrentUsuario().PessoaFisica.nome;
            this.txtPx.Text = Util.InteiroToString(ObjBLL.ObjEF.codigo);
            this.cTextoObs.Text = ObjBLL.ObjEF.observacao;

            ControleHistorico1.Id_sol_proj = ObjBLL.ObjEF.id_sol_proj;
            ControleHistorico1.DataBind();

            if(ObjBLL.ObjEF.id_tipo_solicitacao.HasValue)
                cDdlTipoSolicitacao.SelectedValue = Convert.ToString(ObjBLL.ObjEF.id_tipo_solicitacao.GetValueOrDefault());

            cDdlTipoSolicitacao_SelectedIndexChanged(null,null);
            GetProposta();
            cDdlTipoSolicitacao.Enabled = !ObjBLL.EhProposta;

            dGravacao1.Visible = dGravacao.Visible = !ObjBLL.EhPA;
            ControleTornarPA1.Visible = ObjBLL.Exists() & !ObjBLL.EhPA;
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_sol_proj = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.descricao = this.cTextoDescricao.Text;
            
            ObjBLL.ObjEF.strCoordenador = this.txtCoordenador.Text;
            ObjBLL.ObjEF.strSubCoordenador = txtSubCoordenador.Text;
            ObjBLL.ObjEF.strFinanciador = this.txtFinanciador.Text;            
            ObjBLL.ObjEF.titulo = this.cTextoTitulo.Text;
            ObjBLL.ObjEF.observacao = this.cTextoObs.Text;

            ObjBLL.ObjEF.id_tipo_solicitacao = Util.StringToInteiroOrNullable(this.cDdlTipoSolicitacao.SelectedValue);
            cDdlTipoSolicitacao.DataBind();

            if(ObjBLL.EhProposta)
                SetProposta();
        }

        protected void GetProposta()
        {
            this.cDdlMoeda2.Id_moeda = Convert.ToInt32(ObjBLL.ObjEF.id_moeda);
            this.listaUnidade.SelectedValue = Convert.ToString(ObjBLL.ObjEF.id_unidade.GetValueOrDefault());
            listaUnidade_SelectedIndexChanged(null, null);

            this.listaDepto.SelectedValue = Convert.ToString(ObjBLL.ObjEF.id_departamento.GetValueOrDefault());
            listaDepto_SelectedIndexChanged(null, null);

            this.listaLab.SelectedValue = Convert.ToString(ObjBLL.ObjEF.id_laboratorio.GetValueOrDefault());
            this.cValor.Value = ObjBLL.ObjEF.valor_global.GetValueOrDefault();
            this.cDataInicio.Value = ObjBLL.ObjEF.inicio.GetValueOrDefault();
            this.cDataTermino.Value = ObjBLL.ObjEF.termino.GetValueOrDefault();
            this.cDdlInstrumentoContratual.Id_instrumento_contratual = Convert.ToInt32(ObjBLL.ObjEF.id_instrumento_contratual.GetValueOrDefault());
            this.cTextoContrPatr.Text = ObjBLL.ObjEF.contrato_patrocinio;
        }

        protected void SetProposta()
        {
            ObjBLL.ObjEF.id_unidade = Util.StringToInteiroOrNullable(listaUnidade.SelectedValue);
            ObjBLL.ObjEF.id_departamento = Util.StringToInteiroOrNullable( listaDepto.SelectedValue);
            ObjBLL.ObjEF.id_laboratorio = Util.StringToInteiroOrNullable(listaLab.SelectedValue);
            ObjBLL.ObjEF.valor_global = cValor.Value;
            ObjBLL.ObjEF.inicio = cDataInicio.Value;
            ObjBLL.ObjEF.termino = cDataTermino.Value;
            ObjBLL.ObjEF.id_instrumento_contratual = this.cDdlInstrumentoContratual.Id_instrumento_contratual;            
            ObjBLL.ObjEF.contrato_patrocinio = cTextoContrPatr.Text;
            ObjBLL.ObjEF.id_moeda = cDdlMoeda2.Id_moeda;
        }

        protected override void btInserir_Click(object sender, EventArgs e)
        {
            Set();
            ObjBLL.Add();
            if (ObjBLL.SaveChanges())
            {                
                string mensagem = String.Format("Solicitação Nº {0} gerada com sucesso em {1:d}!",ObjBLL.ObjEF.codigo,ObjBLL.ObjEF.data_solicitacao);
                Util.ShowMessage(mensagem);
                msg("inclusão efetuada");
                PkValue = Convert.ToInt32(ObjBLL.ObjEF.id_sol_proj);
                ObjBLL.Detach();
                Get();
                SetModify();
            }
            else
                msgError("erro inclusão");
        }

        protected override void SetModify()
        {
            base.SetModify();
            pImprimir.Visible = false;
            btImprimir.Visible = true;
            ControleHistorico1.Visible = true;
        }

        protected override void SetAdd()
        {
            base.SetAdd();
            pImprimir.Visible = false;
            btImprimir.Visible = false;
            ControleHistorico1.Visible = false;            
        }

        protected override void SetView()
        {
            base.SetView();
            pImprimir.Visible = false;
            ControleHistorico1.Visible = false;
            btImprimir.Visible = false;
        }

        protected void SetPrint()
        {
            pGrid.Visible = false;
            pCadastro.Visible = false;
            pImprimir.Visible = true;
        }

        protected void btImprimir_Click(object sender, EventArgs e)
        {
            //SetPrint();
            //var r = new RelatorioAbertura();
            //ReportViewer1.LocalReport.DataSources.Clear();
            //ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dsSolicitacao", r.GerarRelatorio(Convert.ToInt32(PkValue))));
            //ReportViewer1.LocalReport.Refresh();
            Util.NovaJanela(String.Format("../../Relatorios/Projeto/ReportSolicitacao.aspx?pk={0}", PkValue.ToString().Criptografar()));            
        }

        protected void cDdlTipoSolicitacao_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (cDdlTipoSolicitacao.SelectedValue != null)
            {
                TipoSolicitacaoBLL t = new TipoSolicitacaoBLL();
                t.Get(Convert.ToInt32(cDdlTipoSolicitacao.SelectedValue));
                pProposta.Visible = t.EhProposta;                
            } 
        }

        protected void listaUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            UnidadeBLL unidadeBLL = new UnidadeBLL();
            unidadeBLL.Get(Convert.ToInt32(this.listaUnidade.SelectedValue));
            listaDepto.Items.Clear();
            listaDepto.DataSource = unidadeBLL.ObjEF.Departamentos.OrderBy(it => it.nome);
            listaDepto.Items.Insert(0, new ListItem("selecione um departamento...", "0"));
            listaDepto.DataBind();
            this.listaDepto.Focus();
        }

        protected void listaDepto_SelectedIndexChanged(object sender, EventArgs e)
        {
            var departamentoBLL = new DepartamentoBLL();
            departamentoBLL.Get(Convert.ToInt32(this.listaDepto.SelectedValue));
            listaLab.Items.Clear();

            listaLab.DataSource = departamentoBLL.ObjEF.Laboratorios.OrderBy(it => it.nome);
            listaLab.Items.Insert(0, new ListItem("selecione um laboratório...", "0"));
            listaLab.DataBind();
            this.listaLab.Focus();
        }

        protected void btGerarProjA_Click(object sender, EventArgs e)
        {
            ObjBLL.Get(PkValue);
            Util.ShowMessage(ObjBLL.GerarProjetoA((int)cIntCodA.Value, cTextoSubProjA.Text));
            ControleHistorico1.DataBind();
            Util.ChamarScript("closeDialog()", "");
            Get();
        }

        protected void btTornarA_Click(object sender, EventArgs e)
        {
            if (this.IsValid)
            {
                cTextoSubProjA.Text = String.Empty;
                cIntCodA.Value = new Nullable<int>();
                var p = new ProjetoBLL();
                cIntCodA.Value = p.UltimoCodigoA() + 1;
                Util.ChamarScript("openDialog()", "");
            }
        }

        protected override void btAlterar_Click(object sender, EventArgs e)
        {
            base.btAlterar_Click(sender, e);
                Get();
        }

     
    }
}