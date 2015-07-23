using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.BLL;
using Medusa.LIB;
using Medusa.DAL;

namespace Medusa.Sistemas.Comodato
{
    public partial class ControlePatrimonio : ControlCrud<PatrimonioBLL>
    {
        public int Id_comodato
        {
            get
            {
                if (ViewState[ID] == null)
                    Id_comodato = 0;
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
            PRIMARY_KEY = "id_patrimonio";
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
             this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_patrimonio);
             this.txtCodigoComodato.Text = Convert.ToString(ObjBLL.ObjEF.id_comodato);
             this.cTextoDescricao.Text = ObjBLL.ObjEF.descricao;
             this.cTextoNumPatrimonio.Text = ObjBLL.ObjEF.num_patrimonio;
             this.cTextoObs.Text = ObjBLL.ObjEF.observacao;
             this.lbQuantidade.Text = String.Empty;
             this.cDataInicio.Value = ObjBLL.ObjEF.inicio;
             this.cDataTermino.Value = ObjBLL.ObjEF.termino;
             this.cTextoNF.Text = ObjBLL.ObjEF.nf;
             this.cDataNf.Value = ObjBLL.ObjEF.data_nf;
             this.cValor.Value = ObjBLL.ObjEF.valor;
             this.cDdlUnidades.Id_unidade = ObjBLL.ObjEF.id_unidade;
         }

         protected override void Set()
         {
             ObjBLL.ObjEF.id_patrimonio = Convert.ToInt32(this.txtCodigo.Text);
             ObjBLL.ObjEF.id_comodato = Convert.ToInt32(this.txtCodigoComodato.Text);
             ObjBLL.ObjEF.descricao = this.cTextoDescricao.Text;
             ObjBLL.ObjEF.num_patrimonio = this.cTextoNumPatrimonio.Text;
             ObjBLL.ObjEF.observacao = this.cTextoObs.Text;
             ObjBLL.ObjEF.id_comodato = Id_comodato;
             ObjBLL.ObjEF.inicio = this.cDataInicio.Value.GetValueOrDefault();
             ObjBLL.ObjEF.termino = this.cDataTermino.Value;
             ObjBLL.ObjEF.nf = this.cTextoNF.Text;
             ObjBLL.ObjEF.data_nf = this.cDataNf.Value;
             ObjBLL.ObjEF.valor = this.cValor.Value;
             ObjBLL.ObjEF.id_unidade = this.cDdlUnidades.Id_unidade;
             ObjBLL.ObjEF.quantidade = 1;
         }

         protected override void SetGrid()
         {
             Filter p = new Filter()
             {
                 property = "id_comodato",
                 value = Convert.ToString(Id_comodato),
                 mode = "=="

             };
             filtros.Add(p);
             base.SetGrid();
             filtros.Remove(p);
         }

         public override void DataBind()
         {
             SetView();
             SetGrid();
             base.DataBind();
         }

         protected override void SetAdd()
         {
             //pQtdade.Visible = true;
             base.SetAdd();
         }

         protected override void SetModify()
         {
             //pQtdade.Visible = false;
             base.SetModify();
         }

         protected override void SetView()
         {
             SetGrid();
             base.SetView();
         }

         protected override void btInserir_Click(object sender, EventArgs e)
         {
             //try
             //{
             //    for (int i = 0; i < cIntQuantidade.Value; i++)
             //    {
                     Set();
                     ObjBLL.Add();
                     ObjBLL.SaveChanges();
                     ObjBLL.Detach();
                 //}
                 msg("inclusão efetuada");
                 PkValue = 0;
                 Get();
                 SetView();
             //}
             //catch (Exception)
             //{
                 msgError("erro inclusão");
             //}
         }

                  //protected void btInserir_Click(object sender, EventArgs e)
         //{
         //    Set();
         //    ObjBLL.Add();
         //    if (ObjBLL.SaveChanges())
         //    {
         //        msg("inclusão efetuada");
         //        PkValue = 0;
         //        Get();

         //    }
         //    else
         //        msgError("erro inclusão");
         //}

         //protected void btAlterar_Click(object sender, EventArgs e)
         //{
         //    ObjBLL.Get(PkValue);
         //    Set();
         //    ObjBLL.Update();
         //    if (ObjBLL.SaveChanges())
         //        msg("alteração efetuada");
         //    else
         //        msgError("erro alteração");
         //}

         //protected void btExcluir_Click(object sender, EventArgs e)
         //{
         //    ObjBLL.Get(PkValue);
         //    ObjBLL.Delete();
         //    if (ObjBLL.SaveChanges())
         //        msg("exclusão efetuada");
         //    else
         //        msgError("erro exclusão");
         //    Get();
         //    SetAdd();
         //}

         //protected void btCancelar_Click(object sender, EventArgs e)
         //{
         //    SetGrid();
         //    SetView();
         //}

    }
}