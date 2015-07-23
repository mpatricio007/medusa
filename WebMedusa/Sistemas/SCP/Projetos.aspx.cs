using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.LIB;
using Medusa.BLL;

namespace Medusa.Sistemas.SCP
{
    public partial class Projetos : PageCrud<ProjetoaBLL>
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_projetoa";
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
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_projetoa);
            this.cInteiroCodigo.Value = ObjBLL.ObjEF.codigo;
            this.cTextoReferencia.Text = ObjBLL.ObjEF.referencia;
            this.cTextoCoordenador.Text = ObjBLL.ObjEF.coordenador;
            this.cTextoPatrocinador.Text = ObjBLL.ObjEF.patrocinador;
            this.cDataAbertura.Value = ObjBLL.ObjEF.data_abertura;
            this.cTextoEndereco.Text = ObjBLL.ObjEF.endereco;
            this.cTextoTitulo.Text = ObjBLL.ObjEF.titulo;
            this.cTextoObservacao.Text = ObjBLL.ObjEF.observacao;
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_projetoa = Convert.ToInt32(this.txtCodigo.Text);

            ObjBLL.ObjEF.codigo = this.cInteiroCodigo.Value.GetValueOrDefault();
            ObjBLL.ObjEF.referencia = this.cTextoReferencia.Text;
            ObjBLL.ObjEF.coordenador = this.cTextoCoordenador.Text;
            ObjBLL.ObjEF.patrocinador = this.cTextoPatrocinador.Text;
            ObjBLL.ObjEF.data_abertura = this.cDataAbertura.Value.GetValueOrDefault();
            ObjBLL.ObjEF.endereco = this.cTextoEndereco.Text;
            ObjBLL.ObjEF.titulo = this.cTextoTitulo.Text;
            ObjBLL.ObjEF.observacao = this.cTextoObservacao.Text;            
        }
    }
}