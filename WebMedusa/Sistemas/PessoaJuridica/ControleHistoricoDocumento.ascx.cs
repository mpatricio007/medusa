using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.BLL;
using Medusa.DAL;
using Medusa.LIB;

namespace Medusa.Sistemas.PessoaJuridica
{
    public partial class ControleHistoricoDocumento : System.Web.UI.UserControl
    {
        public int Id_fornecedor
        {
            get
            {
                if (ViewState[ID] == null)
                    Id_fornecedor = 0;
                return (int)ViewState[ID];
            }
            set
            {
                ViewState[ID] = value;
            }
        }

        public int Id_categoria
        {
            get
            {
                if (ViewState["Id_categoria"] == null)
                    Id_categoria = 0;
                return (int)ViewState["Id_categoria"];
            }
            set
            {
                ViewState["Id_categoria"] = value;
            }
        }

        private DocumentoFornecedorBLL objBLL = new DocumentoFornecedorBLL();
        private int id_doc_fornecedor = 0;
        private const string PRIMARY_KEY = "id_doc_fornecedor";
        protected void Page_Load(object sender, EventArgs e)
        {
            id_doc_fornecedor = Convert.ToInt32(this.txtCodigo.Text);
            if (!Page.IsPostBack)
            {
                SetGrid();
            }
        }

        protected void SetAdd()
        {
            lbMsg.Text = String.Empty;
            TabelaCancelar.Visible = false;
            TabelaCadastro.Visible = true;
            btInserir.Visible = true;
            btAlterar.Visible = false;
            SetGrid();
        }

        protected virtual void SetModify()
        {
            lbMsg.Text = String.Empty;
            TabelaCadastro.Visible = false;
            TabelaCancelar.Visible = true;
            btInserir.Visible = false;
            btAlterar.Visible = true;
            //btExcluir.Visible = true;
            SetGrid();
        }

        protected void Get()
        {
            objBLL.Get(id_doc_fornecedor);
            this.txtCodigo.Text = Convert.ToString(objBLL.ObjEF.id_doc_fornecedor);
            this.ddlDocumentosCategorias.Id_documento = Convert.ToInt32(objBLL.ObjEF.id_documento);
            this.ddlDocumentosCategorias.Id_categoria = Id_categoria;
            this.cIntNumero.Text = objBLL.ObjEF.numero;
            this.cValidade.Value = objBLL.ObjEF.validade;
            this.cTextoObs.Text = objBLL.ObjEF.obs;
        }

        protected void Set()
        {
            objBLL.ObjEF.id_doc_fornecedor = Convert.ToInt32(this.txtCodigo.Text);
            objBLL.ObjEF.id_documento = this.ddlDocumentosCategorias.Id_documento;
            objBLL.ObjEF.numero = this.cIntNumero.Text;
            objBLL.ObjEF.validade = this.cValidade.Value.GetValueOrDefault();
            objBLL.ObjEF.id_fornecedor = Id_fornecedor;
            objBLL.ObjEF.obs = this.cTextoObs.Text;
        }

        protected virtual void msg(string msg)
        {
            lbMsg.BackColor = System.Drawing.Color.Green;
            lbMsg.ForeColor = System.Drawing.Color.White;
            lbMsg.Text = string.Format("* {0} !", msg);
        }

        protected virtual void msgError(string msg)
        {
            lbMsg.BackColor = System.Drawing.Color.Red;
            lbMsg.ForeColor = System.Drawing.Color.White;
            lbMsg.Text = string.Format("* {0} !", msg);
        }

        protected void btInserir_Click(object sender, EventArgs e)
        {
            Set();
            objBLL.Add();
            if (objBLL.SaveChanges())
            {
                msg("inclusão efetuada");
                objBLL.Detach();
                id_doc_fornecedor = 0;
                Get();
                SetAdd();
            }
            else
                msgError("erro inclusão");
        }

        protected void btAlterar_Click(object sender, EventArgs e)
        {
            objBLL.Get(id_doc_fornecedor);
            //Set();
            if (objBLL.ObjEF.id_status_docFornecedor == StatusDocFornecedorBLL.Cancelado)
            {
                msgError("Documento já se encontra cancelado!");
                return;
            }
            objBLL.Update(cTextoObs.Text);

            if (objBLL.SaveChanges())
            {
                msg("Alteração efetuada");
                objBLL.Detach();
                id_doc_fornecedor = 0;
                Get();
                SetAdd();
            }
            else
                msgError("erro alteração");
        }
    

        protected void btExcluir_Click(object sender, EventArgs e)
        {
            objBLL.Get(id_doc_fornecedor);
            objBLL.Delete();
            if (objBLL.SaveChanges())
                msg("exclusão efetuada");
            else
                msgError("erro exclusão");
            Get();
            SetAdd();
        }

        protected void btCancelar_Click(object sender, EventArgs e)
        {
            id_doc_fornecedor = 0;
            Get();
            SetAdd();
        }

        protected void SetGrid()
        {
            var filtros = new List<Filter>();
            filtros.Add(new Filter()
            {
                property = "id_fornecedor",
                value = Convert.ToString(Id_fornecedor),
                mode = "=="
            });
            grid.DataKeyNames = new string[] { PRIMARY_KEY };
            grid.DataSource = objBLL.Find(filtros,
               "data",
               "DESC", 0);
            grid.DataBind();
        }

        public override void DataBind()
        {
            Get();
            SetAdd();
            SetGrid();
            base.DataBind();
        }

        protected void grid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //id_doc_fornecedor = Convert.ToInt32(grid.DataKeys[e.NewEditIndex][PRIMARY_KEY]);
            txtCodigo.Text = Convert.ToString(grid.DataKeys[e.NewEditIndex][PRIMARY_KEY]);
            //Get();
            grid.DataBind();
            SetModify();
            e.Cancel = true;
        }
    }
}
