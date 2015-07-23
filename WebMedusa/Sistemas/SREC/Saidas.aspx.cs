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
    public partial class Saidas : PageCrud<SaidaBLL>
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_saida";
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
            this.txtNprotent.Focus();
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_saida);
            this.cTextoProtSaida.Value = ObjBLL.ObjEF.nprotsai;
            this.cTextoDataSai.Value = ObjBLL.ObjEF.datasai;
            this.cTextoObsSaida.Text = ObjBLL.ObjEF.obssaida;
            this.cTextoUsuarioFUSPResp.Id_usuario = ObjBLL.ObjEF.id_usu_respdevol;
            this.cTextoDestinatario.Text = ObjBLL.ObjEF.destinatario;

            ObjBLL.ObjEF.Entrada = ObjBLL.ObjEF.Entrada ?? new Entrada();
            GetEntrada(ObjBLL.ObjEF.Entrada);
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_saida = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.nprotsai = this.cTextoProtSaida.Value.GetValueOrDefault();
            ObjBLL.ObjEF.datasai = this.cTextoDataSai.Value.GetValueOrDefault();
            ObjBLL.ObjEF.obssaida = this.cTextoObsSaida.Text;
            ObjBLL.ObjEF.id_usu_respdevol = this.cTextoUsuarioFUSPResp.Id_usuario;
            ObjBLL.ObjEF.destinatario = this.cTextoDestinatario.Text;
            //ObjBLL.ObjEF.Entrada.id_entrada = Convert.ToInt32(this.id_entrada.Text);
            ObjBLL.ObjEF.id_usu_saida = cTextoUsuarioFUSPResp.Id_usuario;
      }

        protected void txtNprotent_TextChanged(object sender, EventArgs e)
        {
            var entBLL = new EntradaBLL();
            entBLL.GetPorProtocolo(Util.StringToInteiro(ddlAno.SelectedValue).GetValueOrDefault(),
                Util.StringToInteiro(this.txtNprotent.Text).GetValueOrDefault());
            GetEntrada(entBLL.ObjEF);
            this.cTextoProtSaida.Focus();
        }

        protected void GetEntrada(Entrada ent)
        {
            id_entrada.Text = Convert.ToString(ent.id_entrada);
            txtNprotent.Text = Convert.ToString(ent.nprotent);
            txtProjeto.Text = Convert.ToString(ent.codproj);
            txtProjA.Text = Convert.ToString(ent.codproja);
            txtDocumento.Text = Convert.ToString(ent.tipodocent);
            txtNumero.Text = Convert.ToString(ent.numdocent);
            txtValor.Text = Convert.ToString(ent.valorent);
            txtEnviadoPor.Text = Convert.ToString(ent.enviadoent);
            txtDescricao.Text = Convert.ToString(ent.descrent);
            txtEncaminhadoPara.Text = ent.id_entrada != 0 ? Convert.ToString(ent.UsuarioPara.PessoaFisica.nome) : String.Empty;
            txtObs.Text = Convert.ToString(ent.obsent);
        }

        protected void ddlAno_SelectedIndexChanged(object sender, EventArgs e)
        {

            SetGrid();
            SetView();
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
    }
}

