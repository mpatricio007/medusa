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

namespace Medusa.Sistemas.SREC
{
    public partial class StatusEntradas : PageCrud<StatusEntradaBLL>
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_status_entrada";
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
            txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_status_entrada);
            cTextoNome.Text = ObjBLL.ObjEF.nome;
            ckDestinatario.Checked = ObjBLL.ObjEF.escolhe_destinatarios;
            PossiveisProvidencias1.Id_status_atual = ObjBLL.ObjEF.id_status_entrada;
            PossiveisProvidencias1.Value = ObjBLL.ObjEF.PossiveisProvidencias.ToList();
            PossiveisProvidencias1.DataBind();
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_status_entrada = Convert.ToInt32(txtCodigo.Text);
            ObjBLL.ObjEF.nome = cTextoNome.Text;
            ObjBLL.ObjEF.escolhe_destinatarios = ckDestinatario.Checked;

            ObjBLL.oldPossiveisProvidencias = ObjBLL.ObjEF.PossiveisProvidencias.ToList();
            ObjBLL.oldPossiveisProvidencias.ForEach(it => ObjBLL.ObjEF.PossiveisProvidencias.Remove(it));

            ObjBLL.ObjEF.PossiveisProvidencias = PossiveisProvidencias1.Value;
        }

        protected override void btInserir_Click(object sender, EventArgs e)
        {
            Set();
            ObjBLL.Add();
            if (ObjBLL.SaveChanges())
            {
                msg("inclusão efetuada");
                PkValue = ObjBLL.ObjEF.id_status_entrada;
                ObjBLL.Detach();
                Get();
                SetModify();
            }
            else
                msgError("erro inclusão");
        }

        protected override void btAlterar_Click(object sender, EventArgs e)
        {
            ObjBLL.Get(PkValue);
            Set();
            ObjBLL.Update();
            if (ObjBLL.SaveChanges())
                msg("alteração efetuada");
            else
                msgError("erro alteração");
            Get();
        }
    }
}
