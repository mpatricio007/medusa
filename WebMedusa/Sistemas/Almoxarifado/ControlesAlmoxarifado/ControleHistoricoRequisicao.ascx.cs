using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.BLL;
using Medusa.DAL;
using Medusa.LIB;

namespace Medusa.Sistemas.Almoxarifado
{
    public partial class ControleHistoricoRequisicao : System.Web.UI.UserControl
    {
        public int Id_requisicao_material
        {
            get
            {
                if (ViewState[ID] == null)
                    Id_requisicao_material = 0;
                return (int)ViewState[ID];
            }

            set
            {
                ViewState[ID] = value;
            }
        }

        private HistoricoRequisicaoBLL objBLL = new HistoricoRequisicaoBLL();
        private int id_historico_requisicao = 0;
        private const string PRIMARY_KEY = "id_historico_requisicao";
        protected void Page_Load(object sender, EventArgs e)
        {
            id_historico_requisicao = Convert.ToInt32(this.txtCodigo.Text);
            if (!IsPostBack)
            {
                SetGrid();
            }
        }

        protected void SetAdd()
        {
            lbMsg.Text = String.Empty;
            btInserir.Visible = true;
            pAll.Visible = true;
            SetGrid();
        }

        protected virtual void SetModify()
        {
            lbMsg.Text = String.Empty;
            btInserir.Visible = false;
            pAll.Visible = true;
            SetGrid();
        }

        protected virtual void SetView()
        {
            pAll.Visible = false;
        }

        protected void Get()
        {
            objBLL.Get(id_historico_requisicao);
            this.txtCodigo.Text = Convert.ToString(objBLL.ObjEF.id_historico_requisicao);
            this.cTextoObs.Text = objBLL.ObjEF.observacao;
            this.cIntQtde.Value = objBLL.ObjEF.quantidade;

            var rmBLL = new RequisicaoMaterialBLL();
            rmBLL.Get(Id_requisicao_material);
            btDesfazer.Visible = rmBLL.ObjEF.id_ultimo_status == StatusRequisicaoMaterialBLL.Atendido;
            tbMateriais.Visible = !btDesfazer.Visible;            
        }

        protected void Set()
        {
            objBLL.ObjEF.id_historico_requisicao = Convert.ToInt32(this.txtCodigo.Text);
            objBLL.ObjEF.observacao = this.cTextoObs.Text;
            objBLL.ObjEF.id_status_requisicao_material = this.DdlStatusRequisicaoMaterial1.Id_status_requisicao_Material;
            objBLL.ObjEF.id_requisicao_material = Id_requisicao_material;
            objBLL.ObjEF.quantidade = this.cIntQtde.Value.GetValueOrDefault();
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
            string strMSg = String.Empty;
            Set();
            if (objBLL.DataIsValid(ref strMSg))
            {
                objBLL.Add();
                if (objBLL.SaveChanges())
                {
                    var pr = (PesquisaRequisicoes)base.Page;
                    pr.PkValue = objBLL.ObjEF.RequisicaoMaterial.id_requisicao;
                    pr.GetExternal();
                    msg("inclusão efetuada");
                    objBLL.Detach();
                    id_historico_requisicao = 0;
                    Get();
                    SetAdd();
                }
                else
                    msgError("erro inclusão");
            }
            else
                msgError(strMSg);
        }
        
        protected void btCancelar_Click(object sender, EventArgs e)
        {
            id_historico_requisicao = 0;
            Get();
            SetAdd();
        }

        protected void SetGrid()
        {
            var filtros = new List<Filter>();
            filtros.Add(new Filter()
            {
                property = "id_requisicao_material",
                value = Convert.ToString(Id_requisicao_material),
                mode = "=="
            });
            grid.DataKeyNames = new string[] { PRIMARY_KEY };
            grid.DataSource = objBLL.Find(filtros,
               PRIMARY_KEY,
               "DESC", 0);
            grid.DataBind();
        }

        public override void DataBind()
        {
            if (Id_requisicao_material != 0)
            {
                Get();
                if (Id_requisicao_material != 0)
                    SetAdd();
                else
                    SetView();
                SetGrid();
            }
            base.DataBind();
        }

        protected void btDesfazer_Click(object sender, EventArgs e)
        {
            var rmBLL = new RequisicaoMaterialBLL();
            rmBLL.Get(Id_requisicao_material);
            rmBLL.DesfazerAtendimento();
            if (rmBLL.SaveChanges())
            {
                var pr = (PesquisaRequisicoes)base.Page;
                pr.PkValue = rmBLL.ObjEF.id_requisicao;
                pr.GetExternal();
                msg("atendimento desfeito");
                objBLL.Detach();
                id_historico_requisicao = 0;
                Get();
                SetAdd();
            }
            else
                msgError("erro ao tentar desfazer atendimento");
        } 
    }
}