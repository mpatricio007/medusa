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
using Medusa.Controles;

namespace Medusa.Sistemas.Devolucao
{
    public partial class Devolucoes : PageCrud<DevolucaoBLL>
    {
        public List<MotivoDevolucao> lstMotivoDevolucao
        {
            get
            {
                if (Cache["lstMotivoDevolucao"] == null)
                    Cache["lstMotivoDevolucao"] = new List<MotivoDevolucao>();
                return (List<MotivoDevolucao>)Cache["lstMotivoDevolucao"];
            }
            set { Cache["lstMotivoDevolucao"] = value; }
        }

      

        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_devolucao";
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

                var mBLL = new MotivoBLL();
                gridMotivos.DataKeyNames = new string[] { "id_motivo" };
                gridMotivos.DataSource = mBLL.Find(it => !it.id_motivo_pai.HasValue).OrderBy(it => it.id_motivo).ToList();
                gridMotivos.DataBind();
            }
        }

        protected override void Get()
        {
            Cache.Remove("lstMotivoDevolucao");
            ObjBLL.Get(PkValue);
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_devolucao);
            this.cDdlProjeto1.Id_projeto = ObjBLL.ObjEF.id_projeto;
            this.cIntNumero.Value = ObjBLL.ObjEF.numero;
            this.cIntProtocolo.Value = ObjBLL.ObjEF.protocolo;
            this.cTextoBeneficiario.Text = ObjBLL.ObjEF.beneficiario;
            this.cValor.Value = ObjBLL.ObjEF.valor_total;
            this.DdlTipoDevolucao1.Id_tipo_devolucao = ObjBLL.ObjEF.id_tipo_devolucao;
            lstMotivoDevolucao = ObjBLL.ObjEF.MotivoDevolucoes.ToList();
            UpdateSelectedRows();
            //UpdatelstMotivoDevolucao(gridMotivos);
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_devolucao = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.id_projeto = this.cDdlProjeto1.Id_projeto;
            ObjBLL.ObjEF.numero = Convert.ToInt32(this.cIntNumero.Value);
            ObjBLL.ObjEF.protocolo = Convert.ToInt32(this.cIntProtocolo.Value);
            ObjBLL.ObjEF.beneficiario = this.cTextoBeneficiario.Text;
            ObjBLL.ObjEF.valor_total = Convert.ToDecimal(this.cValor.Value);
            ObjBLL.ObjEF.id_tipo_devolucao = this.DdlTipoDevolucao1.Id_tipo_devolucao;
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
                    msgError("erro inclusão");
            }
            else
                msgError("erro inclusão");
        }

        protected void ckMotivo_CheckedChanged(object sender, EventArgs e)
        {
           
            var ck = (CheckBox)sender;
            var row = gridMotivos.Rows[Util.StringToInteiro(ck.CssClass).GetValueOrDefault()];

            UpdateLstMotivoDevolucao(row,ck.Checked);

        }

        public void UpdateLstMotivoDevolucao(GridViewRow row, bool ck)
        {
            var mBLL = new MotivoBLL();
            Label lbMotivo = (Label)row.FindControl("lbMotivo");
            Panel panel = (Panel)row.FindControl("pSubmotivos");
            panel.Visible = false;

            mBLL.Get(Util.StringToInteiro(lbMotivo.Text));

            if (ck)
            {
                var md = new MotivoDevolucao();
                md.id_motivo = mBLL.ObjEF.id_motivo;
                md.id_devolucao = (int)PkValue;
                if (mBLL.ObjEF.temOutros)
                {
                    Panel pOutros = (Panel)row.FindControl("pOutros");
                    Texto cTextoOutros = (Texto)pOutros.FindControl("cTextoOutros");
                    md.obs = cTextoOutros.Text;
                }

                lstMotivoDevolucao.Add(md);
                panel.Visible = mBLL.ObjEF.MotivosFilhos.Count() > 0;
                if (panel.Visible)
                {
                    var gridSubMotivos = (GridView)panel.FindControl("gridMotivos");
                    gridSubMotivos.DataSource = mBLL.ObjEF.MotivosFilhos.OrderBy(it => it.id_motivo).ToList();
                    gridSubMotivos.DataBind();
                }
            }
            else
                lstMotivoDevolucao.Remove(lstMotivoDevolucao.FirstOrDefault(it => it.id_motivo == mBLL.ObjEF.id_motivo));
        }

        protected void ckSubMotivo_CheckedChanged(object sender, EventArgs e)
        {
            var ck = (CheckBox)sender;
            var row = gridMotivos.Rows[Util.StringToInteiro(ck.CssClass).GetValueOrDefault()];

            UpdateLstMotivoDevolucao(row, ck.Checked);
            //foreach (GridViewRow row in gridMotivos.Rows)
            //{
            //    if (row.RowType == DataControlRowType.DataRow)
            //    {
            //        UpdateGridSubMotivos((Panel)row.FindControl("pSubmotivos"));
            //    }
            //}
        }

        #region old
        //public void UpdateGridSubMotivos(Panel panel) 
        //{
        //    GridView gSubMot = (GridView)panel.FindControl("gridMotivos");
        //    foreach (GridViewRow SubRow in gSubMot.Rows)
        //    {
        //        CheckBox cb = (CheckBox)SubRow.FindControl("ckMotivo");
        //        Panel SubPanel = (Panel)SubRow.FindControl("pOutros");
        //        Texto tOutros = (Texto)SubPanel.FindControl("cTextoOutros");
        //        if (cb != null)
        //            tOutros.EnableValidator = cb.Checked;
        //    }
        //    UpdatelstMotivoDevolucao(gSubMot);
        //}

        //public void UpdatelstMotivoDevolucao(GridView CurrentGrid)
        //{
        //    foreach (GridViewRow row in CurrentGrid.Rows)
        //    {
        //        var mBLL = new MotivoBLL();
        //        mBLL.Get(Convert.ToInt32(CurrentGrid.DataKeys[row.RowIndex]["id_motivo"]));
        //        var md = new MotivoDevolucao();
        //        md.id_motivo = mBLL.ObjEF.id_motivo;
        //        md.id_devolucao = (int)PkValue;

        //        CheckBox ckItem = (CheckBox)row.FindControl("ckMotivo");

        //        if (ckItem.Checked)
        //        {
        //            if (mBLL.ObjEF.temOutros)
        //            {
        //                Panel pOutros = (Panel)CurrentGrid.FindControl("pOutros");
        //                Texto cTextoOutros = (Texto)pOutros.FindControl("cTextoOutros");
        //                md.obs = cTextoOutros.Text;
        //            }

        //            if (index < 0)
        //            {
        //                if (!lstMotivoDevolucao.Select(it => it.id_motivo).Contains(mBLL.ObjEF.id_motivo))
        //                    lstMotivoDevolucao.Add(md);
        //            }
        //            else
        //                lstMotivoDevolucao[index] = md;
        //        }
        //        else
        //            if (lstMotivoDevolucao.Select(it => it.id_motivo).Contains(mBLL.ObjEF.id_motivo))
        //            {
        //                lstMotivoDevolucao.RemoveAt(row.RowIndex);
        //                index = -1;
        //            }
        //    }
        //}
        #endregion

        public void UpdateSelectedRows()
        {
            foreach (GridViewRow row in gridMotivos.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox cb = (CheckBox)row.FindControl("ckMotivo");
                    Panel panel = (Panel)row.FindControl("pSubmotivos");
                    cb.Checked = lstMotivoDevolucao.Select(it => it.id_motivo).Contains(Convert.ToInt32(gridMotivos.DataKeys[row.RowIndex]["id_motivo"]));
                    if (cb != null)
                        if (cb.Checked)
                        {
                            var gridSubMotivos = (GridView)panel.FindControl("gridMotivos");
                            foreach (GridViewRow rowgSub in gridSubMotivos.Rows)
                            {
                                CheckBox cbSub = (CheckBox)rowgSub.FindControl("ckMotivo");
                                cbSub.Checked = lstMotivoDevolucao.Select(it => it.id_motivo).Contains(Convert.ToInt32(gridSubMotivos.DataKeys[rowgSub.RowIndex]["id_motivo"]));                                
                            }
                        }
                }
            }
        }      
    }
}