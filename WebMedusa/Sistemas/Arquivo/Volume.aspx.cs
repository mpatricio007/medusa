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
    public partial class Volume : PageCrud<VolumeBLL>
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_volume";
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
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_volume);
            this.cTextoNome.Text = ObjBLL.ObjEF.nome;
            this.cTextoDescricao.Text = ObjBLL.ObjEF.descricao;
            this.txtProjeto.Text = ObjBLL.ObjEF.projeto;
            this.cDdlTipoVolume1.Id_tipo = ObjBLL.ObjEF.id_tipo;
            this.cDdlLocalizacaoVolume1.Id_localizacao = Convert.ToString(ObjBLL.ObjEF.id_localizacao);
            this.txtProjetoA.Text = ObjBLL.ObjEF.projetoA;
            this.txtCoordenador.Text = ObjBLL.ObjEF.coordenador;
            this.cInteiro1.Value = ObjBLL.ObjEF.num;
            this.btImprimir.PostBackUrl = String.Format("PrintVolume.aspx?id={0:0000000000000000000}", ObjBLL.ObjEF.id_volume);
            this.txtCodigoMetrofile.Text = ObjBLL.ObjEF.codigo_metrofile;
            
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_volume = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.nome = this.cTextoNome.Text;
            ObjBLL.ObjEF.descricao = this.cTextoDescricao.Text;
            ObjBLL.ObjEF.projeto = this.txtProjeto.Text;
            ObjBLL.ObjEF.id_tipo = this.cDdlTipoVolume1.Id_tipo;
            ObjBLL.ObjEF.id_localizacao = Convert.ToInt32(this.cDdlLocalizacaoVolume1.Id_localizacao);
            ObjBLL.ObjEF.projetoA = this.txtProjetoA.Text;
            ObjBLL.ObjEF.coordenador = this.txtCoordenador.Text;
            ObjBLL.ObjEF.num = this.cInteiro1.Value;
            ObjBLL.ObjEF.codigo_metrofile = this.txtCodigoMetrofile.Text;
        }

        protected override void btInserir_Click(object sender, EventArgs e)
        {
            Set();
            ObjBLL.Add();
            if (ObjBLL.SaveChanges())
            {                
                msg("inclusão efetuada");
                PkValue = ObjBLL.ObjEF.id_volume;
                SetModify();
                Get();
            }
            else
                msgError("erro inclusão");

        }

        protected override void SetAdd()
        {
            btImprimir.Visible = false;
            base.SetAdd();
        }

        protected override void SetModify()
        {
            btImprimir.Visible = true;
            base.SetModify();
        }

    



    }
}