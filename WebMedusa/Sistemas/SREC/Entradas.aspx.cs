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
using Medusa.Relatorios.Recepcao;
using Medusa.Relatorios;

namespace Medusa.Sistemas.SREC
{
    public partial class Entradas : PageCrud<EntradaBLL>
    {
        private SaidaBLL saidaBLL = new SaidaBLL();
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_entrada";
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
                this.ddlAno.DataSource = ObjBLL.GetAnos();
                this.ddlAno.DataBind();

                base.Page_Load(sender, e);
            }
        }

        protected override void Get()
        {
            ObjBLL.Get(PkValue);
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_entrada);
            this.cInteiroProtocolo.Value = ObjBLL.Exists() ? ObjBLL.ObjEF.nprotent : ObjBLL.GetNextProtocolo();
            this.cDataProtocoloEnt.Value = ObjBLL.ObjEF.dataprot;
            this.cInteiroProjeto.Value = ObjBLL.ObjEF.codproj;
            this.cInteiroProjA.Value = ObjBLL.ObjEF.codproja;
            this.txtDocumento.Text = ObjBLL.ObjEF.tipodocent;
            this.cTextoNumero.Text = ObjBLL.ObjEF.numdocent;
            this.cDdlMoeda1.Id_moeda = ObjBLL.ObjEF.id_valor_moeda.GetValueOrDefault();
            this.cValor1.Value = ObjBLL.ObjEF.valorent;
            this.txtEnviadoPor.Text = ObjBLL.ObjEF.enviadoent;
            this.cTextoDescricao.Text = ObjBLL.ObjEF.descrent;
            this.cDdlUsuarioFUSP.Id_usuario = ObjBLL.ObjEF.id_usu_para;
            this.txtObs.Text = ObjBLL.ObjEF.obsent;

            this.ControleHistoricoEntrada1.Id_entrada = ObjBLL.ObjEF.id_entrada;
            this.ControleHistoricoEntrada1.DataBind();

            GetSaida();
            //dGravacao.Visible = dGravacao1.Visible = ObjBLL.Exists() ? pSaida.Visible : true;
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_entrada = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.nprotent = this.cInteiroProtocolo.Value.GetValueOrDefault();
            ObjBLL.ObjEF.dataprot = this.cDataProtocoloEnt.Value.GetValueOrDefault();
            ObjBLL.ObjEF.codproj = this.cInteiroProjeto.Value;
            ObjBLL.ObjEF.codproja = this.cInteiroProjA.Value;
            ObjBLL.ObjEF.tipodocent = this.txtDocumento.Text;
            ObjBLL.ObjEF.numdocent = this.cTextoNumero.Text;
            ObjBLL.ObjEF.id_valor_moeda = this.cDdlMoeda1.Id_moeda;
            ObjBLL.ObjEF.valorent = this.cValor1.Value;
            ObjBLL.ObjEF.enviadoent = this.txtEnviadoPor.Text;
            ObjBLL.ObjEF.descrent = this.cTextoDescricao.Text;
            ObjBLL.ObjEF.id_usu_para = this.cDdlUsuarioFUSP.Id_usuario;
            ObjBLL.ObjEF.obsent = this.txtObs.Text;
        }

        protected override void SetAdd()
        {
            tbHistorico.Visible = false;
            dImprimir.Visible = false;
            pSaida.Visible = false;
            lblMsgSaida.Text = String.Empty;
            cDdlUsuarioFUSP.Enabled = true;
            btCancelSaida.Visible = false;

            base.SetAdd();
        }

        protected override void SetModify()
        {
            tbHistorico.Visible = true;
            dImprimir.Visible = false;
            pSaida.Visible = true;
            lblMsgSaida.Text = String.Empty;
            cDdlUsuarioFUSP.Enabled = false;
            base.SetModify();
        }

        protected override void SetView()
        {
            dImprimir.Visible = false;
            pSaida.Visible = false;
            btCancelSaida.Visible = false;
            tbHistorico.Visible = false;
            base.SetView();
        }

        protected void SetPrint()
        {
            pGrid.Visible = false;
            pCadastro.Visible = false;
            pSaida.Visible = false;
            dImprimir.Visible = true;
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
                msgError("saída de documento já registrada");
        }

        protected override void SetGrid()
        {
            Filter f = new Filter()
            {
                property = "ano",
                value = this.ddlAno.SelectedValue,
                mode = "=="

            };
            filtros.Add(f);
            this.grid.Caption = String.Format("Lista de Entradas de {0}", f.value);
            base.SetGrid();
            filtros.Remove(f);
        }

        protected void ddlAno_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetGrid();
            SetView();
        }

        protected void btImprimirSelecionados_Click(object sender, ImageClickEventArgs e)
        {
            SetPrint();
            Filter f = new Filter()
            {
                property = "ano",
                value = this.ddlAno.SelectedValue,
                mode = "=="

            };
            filtros.Add(f);
            var ReportViewer1 = new ReportViewer();
            ReportViewer1.Visible = true;
            ReportViewer1.LocalReport.ReportPath = @"Relatorios\Recepcao\RelatorioEntrada.rdlc";
            var r = new RelatorioEntrada();
            ReportDataSource rpd = new ReportDataSource("dsEntradas", r.GerarRelatorio(ObjBLL.Find(
                filtros,
                (string)ViewState["SortExpression"],
                (string)ViewState["SortDirection"], 0).OfType<Entrada>()));
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rpd);
            ReportViewer1.LocalReport.Refresh();
            filtros.Remove(f);
            Session[PageOfReport.SessionName] = ReportViewer1;
            dImprimir.InnerHtml = PageOfReport.iframe;
            dImprimir.DataBind();
        }

        protected void btAlterarSaida_Click(object sender, EventArgs e)
        {
            string msg = "";
            saidaBLL.Get(Convert.ToInt32(this.txtCodigoSaida.Text));
            SetSaida();
            ObjBLL.Get(Util.StringToInteiro(txtCodigo.Text));
            saidaBLL.ObjEF.Entrada = ObjBLL.ObjEF;
            if (saidaBLL.DataIsValid(ref msg))
            {
                if (saidaBLL.ObjEF.id_saida != 0)
                    saidaBLL.Update();
                else
                    saidaBLL.Add();
                if (saidaBLL.SaveChanges())
                {
                    saidaBLL.SalvarStausSaida();
                    msgSaida("saida efetuada");
                    this.txtCodigoSaida.Text = Convert.ToString(saidaBLL.ObjEF.id_saida);
                    Get();
                }
                else
                    msgErrorSaida("erro inclusão");
            }
            else
                msgErrorSaida(msg);
        }

        protected void btCancelSaida_Click(object sender, EventArgs e)
        {
            saidaBLL.Get(Convert.ToInt32(this.txtCodigoSaida.Text));
            //SetSaida();
            saidaBLL.Delete();
            ObjBLL.Get(Util.StringToInteiro(txtCodigo.Text));
            if (saidaBLL.SaveChanges())
            {
                ObjBLL.CancelarSaida();
                msgSaida("saida cancelada");
                saidaBLL.ObjEF = new Saida();
                Get();
            }
            else
                msgErrorSaida("erro cancelamento");
        }

        protected void GetSaida()
        {
            if (ObjBLL.ExistsSaida())
                saidaBLL.Get(ObjBLL.ObjEF.saida.id_saida);
            this.txtCodigoSaida.Text = Convert.ToString(saidaBLL.ObjEF.id_saida);
            this.cTextoProtSaida.Value = saidaBLL.ObjEF.nprotsai;
            this.cTextoDataSai.Value = saidaBLL.ObjEF.datasai;
            this.cTextoObsSaida.Text = saidaBLL.ObjEF.obssaida;
            this.cTextoDestinatario.Text = saidaBLL.ObjEF.destinatario;
            this.cTextoUsuarioResp.Id_usuario = saidaBLL.ObjEF.id_usu_respdevol;
            btCancelSaida.Visible = ObjBLL.ExistsSaida();
        }

        protected void SetSaida()
        {
            saidaBLL.ObjEF.id_saida = Convert.ToInt32(this.txtCodigoSaida.Text);
            saidaBLL.ObjEF.id_entrada = Convert.ToInt32(PkValue);

            saidaBLL.ObjEF.nprotsai = this.cTextoProtSaida.Value.GetValueOrDefault();
            saidaBLL.ObjEF.datasai = this.cTextoDataSai.Value.GetValueOrDefault();
            saidaBLL.ObjEF.obssaida = this.cTextoObsSaida.Text;
            saidaBLL.ObjEF.destinatario = this.cTextoDestinatario.Text;
            saidaBLL.ObjEF.id_usu_respdevol = this.cTextoUsuarioResp.Id_usuario;
            saidaBLL.ObjEF.id_usu_saida = SecurityBLL.GetCurrentUsuario().id_usuario;
        }

        protected virtual void msgSaida(string msg)
        {
            lblMsgSaida.BackColor = System.Drawing.Color.Green;
            lblMsgSaida.ForeColor = System.Drawing.Color.White;
            lblMsgSaida.Text = string.Format("* {0} !", msg);
        }

        protected virtual void msgErrorSaida(string msg)
        {
            lblMsgSaida.BackColor = System.Drawing.Color.Red;
            lblMsgSaida.ForeColor = System.Drawing.Color.White;
            lblMsgSaida.Text = string.Format("* {0} !?", msg);
        }
    }
}


