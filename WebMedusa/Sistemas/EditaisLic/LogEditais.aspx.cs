using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.LIB;
using Medusa.BLL;
using Medusa.DAL;

namespace Medusa.Sistemas.EditaisLic
{
    public partial class LogEditais : PageCrud<LogEditalBLL>
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_log_edital";
            //valor da chave primária
            PkValue = Convert.ToInt32(this.lbId_log_edital.Text);
            // painel do grid
            pGrid = panelGrid;
            // painel do formulário de alteração
            pCadastro = panelCadastro;
            // gridview
            _grid = grid;
            lbMsg = new Label();
            _btAlterar = new Button();
            _btAlterar0 = new Button();
            _btInserir = new Button();
            _btInserir0 = new Button();
            _btExcluir = new Button();
            _btExcluir0 = new Button();
            _ddlSize = ddlSize;
            _ddlMode = ddlMode;
            _ddlOptions = ddlOptions;
            _txtProcura = txtProcura;
            //
            _btProcurar = btSearch;
            _ckFilter = ckFilter;
            _dataListFiltros = DataListFiltros;
            //

            if (!IsPostBack)
            {
                base.Page_Load(sender, e);
            }
        }

        protected override void Get()
        {
            ObjBLL.Get(PkValue);
            rbTipo.SelectedValue = Convert.ToString(ObjBLL.ObjEF.InscricaoPregao.tipo);
            rbTipo_SelectedIndexChanged(null, null);
            this.cTextoNome.Text = ObjBLL.ObjEF.InscricaoPregao.nome;
            this.cTextoRazaoSocial.Text = ObjBLL.ObjEF.InscricaoPregao.razao_social;
            this.cCPF.Value = ObjBLL.ObjEF.InscricaoPregao.cpf;
            this.cCNPJ2.Value = ObjBLL.ObjEF.InscricaoPregao.cnpj;
            this.lbArquivo.Text = ObjBLL.ObjEF.EditalLicAnexo.arquivo;
            this.lbEdital.Text = ObjBLL.ObjEF.EditalLicAnexo.EditalLic.titulo;
            this.lbId_log_edital.Text = Convert.ToString(ObjBLL.ObjEF.id_log_edital);
            this.lbAcao.Text = ObjBLL.ObjEF.acao;
            this.lbIp.Text = ObjBLL.ObjEF.ip;
            this.lbData.Text = Util.DateToString(ObjBLL.ObjEF.data, "dd/MM/yyyy HH:mm:ss");
        }

        protected override void Set()
        {

        }

        protected void rbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tipo = (TipoInscricao)Enum.Parse(typeof(TipoInscricao), rbTipo.SelectedValue);
            divPessoaJuridica.Visible = tipo == TipoInscricao.CNPJ;
            divPessoaJuridica.DataBind();
            divPessoaFisica.Visible = !divPessoaJuridica.Visible;
            divPessoaFisica.DataBind();
        }
    }
}