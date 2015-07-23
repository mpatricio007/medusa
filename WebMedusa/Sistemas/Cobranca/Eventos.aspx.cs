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

namespace Medusa.Sistemas.Cobranca
{
    public partial class Eventos : PageCrud<EventoBLL>
    {


        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_evento";
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
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_evento);
            this.cTextoNome.Text = ObjBLL.ObjEF.nome;
            this.cTextoDescricao.Text = ObjBLL.ObjEF.descricao;
            this.cTextoInstrucao.Text = ObjBLL.ObjEF.instrucao;
            this.cTextoConta.Id_Conta = ObjBLL.ObjEF.id_conta;
            this.cDdlProjeto1.Id_projeto = ObjBLL.ObjEF.id_projeto;     
            setGridSacados();
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_evento = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.nome = this.cTextoNome.Text;
            ObjBLL.ObjEF.descricao = this.cTextoDescricao.Text;
            ObjBLL.ObjEF.instrucao = this.cTextoInstrucao.Text;
            ObjBLL.ObjEF.id_conta = this.cTextoConta.Id_Conta;
            ObjBLL.ObjEF.id_projeto = this.cDdlProjeto1.Id_projeto;
        }

        protected void btOk_Click(object sender, EventArgs e)
        {
            ObjBLL.Get(PkValue);
            ObjBLL.SalvarEventoSacados(cPesqSacado1.Retorno);
            setGridSacados();
        }

        public void setGridSacados()
        {
            var evsBLL = new EventoSacadoBLL();            
            gridSacadosEvento.DataKeyNames = new string[] { "id_evento_sacado" };            
            gridSacadosEvento.DataSource = evsBLL.Find(it => it.id_evento == (int)PkValue &
                (it.Sacado.PessoaFisica.nome.ToUpper().StartsWith(this.txtProcurarSacado.Text.ToUpper()) ||
                it.Sacado.PessoaFisica.cpf.Value.StartsWith(this.txtProcurarSacado.Text))
                ).OrderBy(k => k.Sacado.PessoaFisica.nome).ToList(); 
            gridSacadosEvento.DataBind();
        }

        protected void gridSacadosEvento_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridSacadosEvento.PageIndex = e.NewPageIndex;
            setGridSacados();
        }

        protected override void btInserir_Click(object sender, EventArgs e)
        {
            Set();
            ObjBLL.Add();            
            if (ObjBLL.SaveChanges())
            {
                msg("inclusão efetuada");
                PkValue = ObjBLL.ObjEF.id_evento;
                ObjBLL.Detach();  
                Get();
                SetModify();
            }
            else
                msgError("erro inclusão");
        }

        protected override void SetAdd()
        {
            base.SetAdd();
            pSacados.Visible = false;
        }
        protected override void SetModify()
        {            
            base.SetModify();
            pSacados.Visible = true;
        }

        protected override void SetView()
        {
            pSacados.Visible = false;
            base.SetView();
        }

        protected void btProcuraSacados_Click(object sender, EventArgs e)
        {
            setGridSacados();    
        }

        protected void gridSacadosEvento_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var evsBLL = new EventoSacadoBLL();
            evsBLL.Get(Convert.ToInt32(gridSacadosEvento.DataKeys[e.RowIndex]["id_evento_sacado"].ToString()));
            evsBLL.Delete();
            if (evsBLL.SaveChanges())
                Util.ShowMessage("Sacado excluído com sucesso!");
            else
                Util.ShowMessage("Impossível excluir sacado !");
            e.Cancel = true;
            setGridSacados();         
        }

        protected void gridSacadosEvento_RowEditing(object sender, GridViewEditEventArgs e)
        {
            ControleBoletos1.setEventoSacado(Convert.ToInt32(gridSacadosEvento.DataKeys[e.NewEditIndex]["id_evento_sacado"].ToString()));            
            e.Cancel = true;
        }

        protected void btPrint_Click(object sender, ImageClickEventArgs e)
        {
            Util.NovaJanela(String.Format("Boletos.aspx?ev={0}", this.txtCodigo.Text.Criptografar()));
        }

        protected void btEnviarEmail_Click(object sender, ImageClickEventArgs e)
        {
            ObjBLL.Get(PkValue);
            ObjBLL.EnviarBoletosEmail();
        }

        protected void btGerarParcela_Click(object sender, EventArgs e)
        {
            var evsBLL = new EventoSacadoBLL();   
            ObjBLL.Get(PkValue);
            var lstEventoSacado = evsBLL.Find(it => it.id_evento == (int)PkValue &
                (it.Sacado.PessoaFisica.nome.ToUpper().StartsWith(this.txtProcurarSacado.Text.ToUpper()) ||
                it.Sacado.PessoaFisica.cpf.Value.StartsWith(this.txtProcurarSacado.Text))
                ).OrderBy(k => k.Sacado.PessoaFisica.nome).ToList();

            if (ObjBLL.GerarParcelas(lstEventoSacado
                , this.cInteiroQtdade.Value.GetValueOrDefault(),
                this.cInteiroPrimeira.Value.GetValueOrDefault(),
                this.cDataVenctoInicial.Value.GetValueOrDefault(),
                this.cValorParcela.Value.GetValueOrDefault()))
            {
                Util.ShowMessage("Parcelas geradas com sucesso!");

            }
            else
                Util.ShowMessage("Erro!");
        }

    }
}