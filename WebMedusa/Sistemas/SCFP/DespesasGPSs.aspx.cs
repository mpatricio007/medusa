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

namespace Medusa.Sistemas.SCFP
{
    public partial class DespesasGPSs : PageCrud<DespesaGPSBLL>
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_lancto";
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
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_lancto);
            this.txtBoleto.Text = ObjBLL.ObjEF.Guia.RepresentacaoNumerica;
            this.cDdlProjeto1.Id_projeto = ObjBLL.ObjEF.id_projeto;
            this.cTextoRp.Text = ObjBLL.ObjEF.rp;
            this.cValorPagto.Value = ObjBLL.ObjEF.valor;
            this.cData1.Value = ObjBLL.ObjEF.dataVencto;
            this.cTextoCedente.Text = ObjBLL.ObjEF.cedente;
            this.cTextoDescricao.Text = ObjBLL.ObjEF.descricao;
            cDdlProjeto1_SelectedIndexChanged(null, null);
            PreencheGrid();

        }

        protected void PreencheGrid()
        {

            if (ObjBLL.Exists())
            {
                GridView1.DataSource = ObjBLL.LctosBeneficiario(ObjBLL.ObjEF.Itens.ToList());
                GridView2.DataSource = ObjBLL.LctosProjeto(ObjBLL.ObjEF.Itens.ToList());
                GridView3.DataSource = ObjBLL.LctosProvisaoImpostos(ObjBLL.ObjEF.Itens.ToList());
            }
            GridView1.DataBind();
            GridView2.DataBind();
            GridView3.DataBind();
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_lancto = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.Guia = new CodigoBarrasConsumo(this.txtBoleto.Text);
            ObjBLL.ObjEF.id_projeto = this.cDdlProjeto1.Id_projeto;
            ObjBLL.ObjEF.rp = this.cTextoRp.Text;
            ObjBLL.ObjEF.valor = this.cValorPagto.Value.GetValueOrDefault();
            ObjBLL.ObjEF.dataVencto = this.cData1.Value.GetValueOrDefault();
            ObjBLL.ObjEF.cedente = cTextoCedente.Text;
            ObjBLL.ObjEF.descricao = this.cTextoDescricao.Text;
        }

        protected override void SetAdd()
        {
            base.SetAdd();
            txtBoleto.Enabled = true;
        }

        protected override void SetModify()
        {
            base.SetModify();
            txtBoleto.Enabled = false;
        }

        protected void cDdlProjeto1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            var proj = new ProjetoBLL();
            proj.Get(cDdlProjeto1.Id_projeto);
            GridCoordenadores.DataSource = proj.ObjEF.Coordenadores;
            GridCoordenadores.DataBind();

            GridPatrocinadores.DataSource = proj.ObjEF.Financiadores;
            GridPatrocinadores.DataBind();

            lblTitulo.Text = proj.ObjEF.titulo;
            lblTitulo.DataBind();
        }

        protected override void btInserir_Click(object sender, EventArgs e)
        {
            string strMsg = "erro inclusão";
            Set();
            if (ObjBLL.DataIsValid(ref strMsg))
            {
                msg(ObjBLL.Agendar());
                PkValue = 0;
                ObjBLL.Detach();
                Get();
            }
            else
                msgError(strMsg);

        }

        protected void btCalcular_Click(object sender, EventArgs e)
        {
            lblMsg.Text = String.Empty;
            string strMsg = String.Empty;
            Set();
            if (ObjBLL.DataIsValid(ref strMsg))
            {
                List<LancamentoItem> l = ObjBLL.ProcessarPlanosContas();
                GridView1.DataSource = ObjBLL.LctosBeneficiario(l);
                GridView2.DataSource = ObjBLL.LctosProjeto(l);
                GridView3.DataSource = ObjBLL.LctosProvisaoImpostos(l);
            }
            else
                msgError(strMsg);
            GridView1.DataBind();
            GridView2.DataBind();
            GridView3.DataBind();

        }

        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            GridViewRow footer = GridView1.FooterRow;
            if (footer != null)
            {
                decimal cred = 0;
                decimal deb = 0;
                foreach (GridViewRow row in GridView1.Rows)
                {
                    cred += decimal.Parse(row.Cells[2].Text);
                    deb += decimal.Parse(row.Cells[3].Text);
                }
                footer.Cells[2].Text = string.Format("{0:n2}", cred);
                footer.Cells[3].Text = string.Format("{0:n2}", deb);
                footer.Cells[4].Text = string.Format("Líquido: {0:n2}", cred - deb);
            }
        }

        protected void GridView2_DataBound(object sender, EventArgs e)
        {
            GridViewRow footer = GridView2.FooterRow;
            if (footer != null)
            {
                decimal cred = 0;
                decimal deb = 0;
                foreach (GridViewRow row in GridView2.Rows)
                {
                    cred += decimal.Parse(row.Cells[2].Text);
                    deb += decimal.Parse(row.Cells[3].Text);
                }
                footer.Cells[2].Text = string.Format("{0:n2}", cred);
                footer.Cells[3].Text = string.Format("{0:n2}", deb);
            }
        }

        protected void GridView3_DataBound(object sender, EventArgs e)
        {
            GridViewRow footer = GridView3.FooterRow;
            if (footer != null)
            {
                decimal cred = 0;
                decimal deb = 0;
                foreach (GridViewRow row in GridView3.Rows)
                {
                    cred += decimal.Parse(row.Cells[2].Text);
                    deb += decimal.Parse(row.Cells[3].Text);
                }

                footer.Cells[2].Text = string.Format("{0:n2}", cred);
                footer.Cells[3].Text = string.Format("{0:n2}", deb);
            }
        }

        protected override void btAlterar_Click(object sender, EventArgs e)
        {
            string strMsg = "erro inclusão";
            ObjBLL.Get(PkValue);
            Set();
            if (ObjBLL.DataIsValid(ref strMsg))
            {
                msg(ObjBLL.ReAgendar());
                Get();
                ObjBLL.Detach();

            }
            else
                msgError(strMsg);
        }

        protected void txtBoleto_TextChanged(object sender, EventArgs e)
        {
            var txt = (TextBox)sender;
            cvBoleto_ServerValidate(null, null);
            //this.txtBoleto.Focus();
            if (cvBoleto.IsValid)
                return;
        }

        protected void cvBoleto_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string msg = cvBoleto.ErrorMessage;
            cvBoleto.IsValid = validaBoleto(out msg);
            cvBoleto.ErrorMessage = msg;
        }

        protected bool validaBoleto(out string msg)
        {
            var rt = false;
            msg = String.Empty;
            var guia = new CodigoBarrasConsumo(this.txtBoleto.Text);
            ObjBLL.ObjEF.Guia = guia;
            ObjBLL.ObjEF.id_lancto = Convert.ToInt32(PkValue);
            if (!ObjBLL.Exists())
            {
                if (ObjBLL.ObjEF.Guia.ValidaCodBarra())
                {
                    this.cValorPagto.Value = guia.Valor();
                    this.cTextoCedente.Focus();
                    //this.txtBoleto.Focus();
                    rt = true;
                }
                else
                {
                    msg = "Guia inválida!";
                    //this.txtBoleto.Text = String.Empty;
                    this.txtBoleto.Focus();
                }
            }
            else
                rt = true;
            return rt;
        }
    }
}