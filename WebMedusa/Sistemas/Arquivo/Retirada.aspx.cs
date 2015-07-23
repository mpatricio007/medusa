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

namespace Medusa.Sistemas.Arquivo
{
    public partial class Retirada : PageCrud<EmprestimoVolumeBLL>
    {
        public List<Medusa.DAL.Volume> listaVolumes
        {
            get
            {
                if (ViewState["volumes"] == null)
                    listaVolumes = new List<Medusa.DAL.Volume>();
                return (List<Medusa.DAL.Volume>)ViewState["volumes"];
            }
            set
            {
                ViewState["volumes"] = value;
            }
        }


        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_emprestimo";
            //valor da chave primária
            PkValue = 0;
            // painel do grid
            pGrid = panelGrid;
            // painel do formulário de alteração
            pCadastro = panelCadastro;
            // gridview
            _grid = grid;
            lbMsg = lblMsg;
            _btAlterar = new Button();
            _btAlterar0 = new Button();
            _btInserir = new Button();
            _btInserir0 = btInserir0;
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
        protected override void SetAdd()
        {            
            base.SetAdd();
            cTextoUsuRetirada.Focus();
        }

        protected override void Get()
        {
            this.cTextoUsuRetirada.Focus();
            this.cTextoUsuRetirada.Text = String.Empty;
            this.cTextoUsuRetirada_TextChanged(null, null);
            this.txtVolume.Text = String.Empty;
            this.listaVolumes = new List<DAL.Volume>();            
            gridVolumes.DataBind();
        }

        protected override void Set()
        {
            

        }     

        protected override void btCancelar_Click(object sender, EventArgs e)
        {
            cTextoUsuRetirada.Focus();
            this.cTextoUsuRetirada.Text = String.Empty;
            
            cTextoUsuRetirada_TextChanged(null, null);
            listaVolumes = new List<Medusa.DAL.Volume>();
            gridVolumes.DataBind();            
        }

        protected void cTextoUsuRetirada_TextChanged(object sender, EventArgs e)
        {   
            UsuarioFuspBLL usu = new UsuarioFuspBLL();
            usu.GetUsuarioFuspPorCpf(this.cTextoUsuRetirada.Text);
            if (usu.ObjEF.id_pessoa != 0)
            {
                this.txtNome.Text = usu.ObjEF.PessoaFisica.nome;
                this.txtSetor.Text = usu.ObjEF.Setor != null ? usu.ObjEF.Setor.nome : String.Empty;
                txtVolume.Focus();
            }
            else
            {
                this.cTextoUsuRetirada.Text = String.Empty;
                this.txtNome.Text = String.Empty;
                this.txtSetor.Text = String.Empty;
            }

        }

        protected void txtVolume_TextChanged(object sender, EventArgs e)
        {
            if (this.txtVolume.Text == EmprestimoVolumeBLL.FinalizarEmprestimo)
                this.btInserir_Click(sender, e);
            else
            {
                if (!ObjBLL.VolumeEstaEmprestado(this.txtVolume.Text))
                {
                    VolumeBLL volumeBLL = new VolumeBLL();
                    volumeBLL.Get(Convert.ToInt32(this.txtVolume.Text));
                    if ((listaVolumes.Where(it => it.id_volume == volumeBLL.ObjEF.id_volume).Count() == 0) & (volumeBLL.ObjEF.id_volume != 0))
                    {
                        listaVolumes.Add(volumeBLL.ObjEF);
                        gridVolumes.DataBind();
                    }
                }
                else                    
                    Util.ShowMessage("este volume já está emprestado!");
                    
                this.txtVolume.Text = String.Empty;
                txtVolume.Focus();
            }
        }

        protected void gridVolumes_DataBinding(object sender, EventArgs e)
        {
            listaVolumes = listaVolumes.OrderBy(it => it.id_volume).ToList();
            gridVolumes.DataSource = listaVolumes;
        }

        protected void gridVolumes_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            listaVolumes.RemoveAt(e.RowIndex);
            gridVolumes.DataBind();
            txtVolume.Focus();
        }

        protected override void btInserir_Click(object sender, EventArgs e)
        {  
            if (ObjBLL.RetiradaVolumes(listaVolumes, this.cTextoUsuRetirada.Text))
                msg(String.Format("empréstimo de {0} volume(s) para {1} efetuado com sucesso!",listaVolumes.Count(),this.txtNome.Text));
            else
                msgError("erro!");
            Get();
            this.cTextoUsuRetirada.Focus();
        }

        protected void grid_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}