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

namespace Medusa.Sistemas.Almoxarifado
{
    public partial class Requisicoes : PageCrud<RequisicaoBLL>
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_requisicao";
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
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_requisicao);

            if (ObjBLL.ObjEF.RequisicaoMateriais == null)
                ObjBLL.ObjEF.RequisicaoMateriais = new List<RequisicaoMaterial>();

            this.txtUsuario.Text = ObjBLL.Exists() ? ObjBLL.ObjEF.Usuario.PessoaFisica.nome : SecurityBLL.GetCurrentUsuario().PessoaFisica.nome;
            this.cData.Text = Util.DateToString(DateTime.Now);


            btEnviar.Visible = ObjBLL.Exists();
            dGravacao.Visible = dGravacao1.Visible = ObjBLL.Exists() ? ObjBLL.ObjEF.id_status_requisicao == StatusRequisicaoBLL.NaoEnviado : true;

            btExcluir.Visible = btExcluir0.Visible = ObjBLL.Exists() ? ObjBLL.MesmoUsuario() : true;
            btAlterar.Visible = btAlterar0.Visible = btExcluir.Visible;

            ControleMaterial1.Id_requisicao = ObjBLL.ObjEF.id_requisicao;
            ControleMaterial1.Value = ObjBLL.ObjEF.RequisicaoMateriais.ToList();
            ControleMaterial1.DataBind();
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_requisicao = Convert.ToInt32(this.txtCodigo.Text);

            ObjBLL.oldMateriais = ObjBLL.ObjEF.RequisicaoMateriais.ToList();
            ObjBLL.oldMateriais.ForEach(it => ObjBLL.ObjEF.RequisicaoMateriais.Remove(it));

            ObjBLL.ObjEF.RequisicaoMateriais = ControleMaterial1.Value;
        }       

        protected override void SetAdd()
        {
            lbMsg.Visible = true;
            lbMsg.Text = String.Empty;
            btInserir0.Visible = true;
            btInserir.Visible = true;
            btExcluir0.Visible = false;
            btExcluir.Visible = false;
            btCancelar0.Visible = true;
            btCancelar.Visible = true;
            base.SetAdd();
        }

        protected override void SetModify()
        {
            lbMsg.Visible = true;
            lbMsg.Text = String.Empty;
            btInserir.Visible = false;
            btInserir0.Visible = false;
            btExcluir.Visible = true;
            btExcluir0.Visible = true;
            btCancelar.Visible = true;
            btCancelar0.Visible = true;
            base.SetModify();
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
                    PkValue = ObjBLL.ObjEF.id_requisicao;
                    ObjBLL.Detach();
                    Get();
                    SetModify();
                }
                else
                    msgError("erro inclusão");
            }
        }

        protected override void btExcluir_Click(object sender, EventArgs e)
        {
            ObjBLL.Get(PkValue);
            if (ObjBLL.MesmoUsuario())
            {
                ObjBLL.Delete();
                if (ObjBLL.SaveChanges())
                    msg("exclusão efetuada");
                else
                    msgError("erro exclusão");
                Get();
                SetAdd();
            }
            else
                msgError("Somente quem requisitou poderá excluí-la");
        }

        protected override void btAlterar_Click(object sender, EventArgs e)
        {
            ObjBLL.Get(PkValue);
            Set();

            if (ObjBLL.MesmoUsuario())
            {
                ObjBLL.Update();
                if (ObjBLL.SaveChanges())
                    msg("alteração efetuada");
                else
                    msgError("erro alteração");
                Get();
            }
            else
                msgError("Somente quem requisitou poderá altera-lá!");
        }

        protected void btEnviar_Click(object sender, EventArgs e)
        {
            ObjBLL.Get(PkValue);
            Set();
            if (ObjBLL.MesmoUsuario())
            {
                if (ObjBLL.Enviar())
                {
                    PkValue = ObjBLL.ObjEF.id_requisicao;
                    msg("enviado com sucesso");
                }
                else
                    msgError("erro ao enviar");
                Get();
            }
            else
                msgError("Somente quem requisitou poderá enviar-lá!");
        }
    }
}