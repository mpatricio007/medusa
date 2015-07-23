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

namespace Medusa.Sistemas.Correspondencia
{
    public partial class Circulares : PageCrud<CircularBLL>
    {
        protected override void Page_Load(object sender, EventArgs e)
        {

            
            // chave primária da tabela
            PRIMARY_KEY = "id_correspondencia";
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
            this.lbNum.Text = Convert.ToString(ObjBLL.ObjEF.num);
            this.lbUsuario.Text = ObjBLL.ObjEF.id_usuario != 0 ? ObjBLL.ObjEF.Usuario.PessoaFisica.nome : SecurityBLL.GetCurrentUsuario().PessoaFisica.nome;
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_correspondencia);
            this.cData1.Value = ObjBLL.ObjEF.data;
            this.cTextoProjeto.Text = ObjBLL.ObjEF.projeto;
            this.cTextoDestinatario.Text = ObjBLL.ObjEF.destinatario;
            this.cTextoDescricao.Text = ObjBLL.ObjEF.descricao;
            this.cTextoPalavrasChave.Text = ObjBLL.ObjEF.tags;
            this.AsyncFileUpload1.Visible = false;
            this.spanUploading.Visible = false;
            this.lbArquivo.Text = ObjBLL.ObjEF.arquivo;
            this.chkAtiva.Checked = ObjBLL.ObjEF.ativa;
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_correspondencia = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.data = this.cData1.Value.GetValueOrDefault();
            ObjBLL.ObjEF.projeto = this.cTextoProjeto.Text;
            ObjBLL.ObjEF.destinatario = this.cTextoDestinatario.Text;
            ObjBLL.ObjEF.descricao = this.cTextoDescricao.Text;
            ObjBLL.ObjEF.tags = this.cTextoPalavrasChave.Text;
            ObjBLL.up = new BLL.Upload();
            ObjBLL.up.file = (HttpPostedFile)Session["file"];
            Session.Remove("file");
            ObjBLL.ObjEF.ativa = this.chkAtiva.Checked;
        }

        protected override void btInserir_Click(object sender, EventArgs e)
        {
            Set();
            ObjBLL.Add();
            //if (ObjBLL.MesmoAno())
            if (ObjBLL.SaveChanges())
            {
                msg("inclusão efetuada");
                Util.ShowMessage(String.Format("Circular de número {0} gerado com sucesso!", ObjBLL.ObjEF.num));
                PkValue = ObjBLL.ObjEF.id_correspondencia;
                ObjBLL.Detach();  // Usado para tirar o objeto do contexto pois não estava buscando os dados da classe usuário
                Get();
                SetModify();
            }
            else
                msgError("erro inclusão");
            //else
            //    msgError("Não é permitido inserir uma circular com data diferente do ano atual");

        }
        protected override void btAlterar_Click(object sender, EventArgs e)
        {
            ObjBLL.Get(PkValue);
            Set();

            //if (ObjBLL.MesmoUsuario())
            //{
            //if (ObjBLL.MesmoAno())
            //{
            ObjBLL.Update();
            if (ObjBLL.SaveChanges())
                msg("alteração efetuada");
            else
                msgError("erro alteração");
            Get();
            //}
            //else
            //    msgError("Não é permitido alterar uma circular com data diferente do ano atual");
            //}
            //else
            //    msgError("Somente quem criou a circular pode alterá-la");
        }
        protected override void btExcluir_Click(object sender, EventArgs e)
        {
            ObjBLL.Get(PkValue);
            //if (ObjBLL.MesmoUsuario())
            //    if (ObjBLL.MesmoAno())
            //    {
            ObjBLL.Delete();
            if (ObjBLL.SaveChanges())
                msg("exclusão efetuada");
            else
                msgError("erro exclusão");
            Get();
            SetAdd();
            //    }
            //    else
            //        msgError("Não é permitido excluir uma circular com data diferente do ano atual");
            //else
            //    msgError("Somente quem criou a circular pode excluí-la");
        }


        protected void ProcessUpload(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
        {
            Session["file"] = AsyncFileUpload1.PostedFile;
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
                property = "data.Year",
                value = this.ddlAno.SelectedValue,
                mode = "=="

            };
            filtros.Add(f);
            this.grid.Caption = String.Format("Lista de Circulares de {0}", f.value);
            base.SetGrid();
            filtros.Remove(f);
        }

        protected void lkAddFile_Click(object sender, EventArgs e)
        {
            this.AsyncFileUpload1.Visible = true;
            this.spanUploading.Visible = true;
        }

        protected override void SetView()
        {
            pGrid.Visible = true;
            pCadastro.Visible = false;
            PanelEmail.Visible = false;
        }


        protected override void SetAdd()
        {
            lbMsg.Text = String.Empty;
            pGrid.Visible = false;
            pCadastro.Visible = true;
            PanelEmail.Visible = false;
            _btInserir.Visible = true;
            _btInserir0.Visible = true;
            _btAlterar.Visible = false;
            _btAlterar0.Visible = false;
            _btExcluir.Visible = false;
            _btExcluir0.Visible = false;
        }

        protected override void SetModify()
        {
            lbMsg.Text = String.Empty;
            pCadastro.Visible = true;
            pGrid.Visible = false;
            PanelEmail.Visible = true;
            _btInserir.Visible = false;
            _btInserir0.Visible = false;
            _btAlterar.Visible = true;
            _btAlterar0.Visible = true;
            _btExcluir.Visible = true;
            _btExcluir0.Visible = true;
            SetAddEnvEmail();           
        }

        protected void SetAddEnvEmail()
        {
            GetEnvEmail(0);
            this.cDataCorrespEmail.Value = DateTime.Now;
            lbMsg.Text = String.Empty;
            PanelGridEmail.Visible = true;
            pCadastro.Visible = true;
            PanelEmail.Visible = true;
            btSalvarEmail.Visible = true;
            btExcluirEmail.Visible = false;
            btEnviarEmail.Visible = false;
            btReencaminharEmail.Visible = false;
            PanelDestinatariosEmail.Visible = false;
            gridEnvEmail.DataBind();
            lblLidos.Text = "";
        }

        protected void SetModifyEnvEmail()
        {
            lbMsg.Text = String.Empty;
            pCadastro.Visible = true;
            pGrid.Visible = false;
            PanelEmail.Visible = true;
            btSalvarEmail.Visible = false;
            btExcluirEmail.Visible = true;
            btEnviarEmail.Visible = true;
            btReencaminharEmail.Visible = true;
            PanelEscolherDestinatarios.Visible = (cDataEnviado.Text == "");
            PanelDestinatariosEmail.Visible = true;
            btEnviarEmail.Visible = (cDataEnviado.Text == "");
            btReencaminharEmail.Visible = !btEnviarEmail.Visible;
            txtProcuraDestinatario.Text = "";            
            setGridDestinatariosDoEmail();

        }



        protected void btSalvarEmail_Click(object sender, EventArgs e)
        {
            CorrespondenciaEmailBLL ceBLL = new CorrespondenciaEmailBLL();
            SetEnvEmail(ceBLL);
            ceBLL.Add();
            ceBLL.SaveChanges();
            gridEnvEmail.DataBind();
            GetEnvEmail(ceBLL.ObjEF.id_correspEmail);
            SetModifyEnvEmail();

        }

        protected void gridEnvEmail_DataBinding(object sender, EventArgs e)
        {
            if (Convert.ToInt32(PkValue) != 0)
            {
                gridEnvEmail.DataKeyNames = new string[] { "id_correspEmail" };
                ObjBLL.Get(PkValue);
                gridEnvEmail.DataSource = ObjBLL.ObjEF.CorrespondenciaEmails.OrderByDescending(it=>it.data);
            }
            else PanelEmail.Visible = false;

        }


        protected void GetEnvEmail(Int32 id)
        {
            ObjBLL.Get(PkValue);
            CorrespondenciaEmailBLL ceBLL = new CorrespondenciaEmailBLL();
            ceBLL.Get(id);
            this.txt_id_correspEmail.Text = Convert.ToString(ceBLL.ObjEF.id_correspEmail);
            this.cTextoAssunto.Text = id != 0 ? ceBLL.ObjEF.assunto : String.Format("Circular {0}/{1}", ObjBLL.ObjEF.num,ObjBLL.ObjEF.data.Year);
            this.cTextoCorpo.Text = id != 0 ? ceBLL.ObjEF.corpo : "Prezado(a) Senhor(a), \n\nSegue a " + this.cTextoAssunto.Text + " para o seu conhecimento.  \nPeço que responda este e-mail confirmando o seu recebimento e o entendimento da circular em questão.\nCaso queira, faça seus comentários e/ou críticas sobre o assunto. \n \nAtenciosamente, \nFundação de Apoio à Universidade de São Paulo";
            this.cDataCorrespEmail.Value = ceBLL.ObjEF.data;
            this.cDataEnviado.Value = ceBLL.ObjEF.enviadoEm;
            
        }

        protected void SetEnvEmail(CorrespondenciaEmailBLL cBLL)
        {
            cBLL.ObjEF.id_correspondencia = Convert.ToInt32(PkValue);
            cBLL.ObjEF.data = DateTime.Now;
            cBLL.ObjEF.assunto = this.cTextoAssunto.Text;
            cBLL.ObjEF.corpo = cTextoCorpo.Text;
        }

        protected void gridEnvEmail_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GetEnvEmail(Convert.ToInt32(gridEnvEmail.DataKeys[e.NewEditIndex]["id_correspEmail"].ToString()));
            SetModifyEnvEmail();
            e.Cancel = true;
        }

        protected void btExcluirEmail_Click(object sender, EventArgs e)
        {
            CorrespondenciaEmailBLL ceBLL = new CorrespondenciaEmailBLL();
            ceBLL.Get(Convert.ToInt32(this.txt_id_correspEmail.Text));
            ceBLL.Delete();
            if (ceBLL.SaveChanges())
                msg("exclusão efetuada");
            else
                msgError("erro exclusão");
            SetAddEnvEmail();
        }

        protected void btOk_Click(object sender, EventArgs e)
        {
            var ceBLL = new CorrespondenciaEmailBLL();
            ceBLL.Get(Convert.ToInt32(txt_id_correspEmail.Text));

            var ds = cPesqOrigemEmail1.Retorno.Select(it => new { nome = it.nome, email = it.email, tipo = it.tipo }).
                Except(ceBLL.ObjEF.DestinatarioEmails.Select(k => new { nome = k.nome_destinatario, email = k.email_value, tipo = k.tipo }));
            var deBLL = new DestinatarioEmailBLL();
            foreach (var c in ds)
            {
                deBLL.ObjEF = new DestinatarioEmail();
                deBLL.ObjEF.id_correspEmail = Convert.ToInt32(txt_id_correspEmail.Text);
                deBLL.ObjEF.nome_destinatario = c.nome;
                deBLL.ObjEF.email_value = c.email;
                deBLL.ObjEF.tipo = c.tipo;
                deBLL.Add();                
            }
            deBLL.SaveChanges();
            SetModifyEnvEmail();
            GetEnvEmail(ceBLL.ObjEF.id_correspEmail);
        
        }

      

        private void setGridDestinatariosDoEmail()
        {
            if (txt_id_correspEmail.Text != "")
            {
                CorrespondenciaEmailBLL ceBLL = new CorrespondenciaEmailBLL();
                ceBLL.Get(Convert.ToInt32(txt_id_correspEmail.Text));
                lblLidos.Text = string.Format("lidos: {0} não lidos: {1}", ceBLL.QtdeEmailsLidos(), ceBLL.QtdeEmailsNaoLidos());
                GridDestinatariosDoEmail.DataKeyNames = new string[] { "id_destinatario" };
                if (txtProcuraDestinatario.Text == "")
                    GridDestinatariosDoEmail.DataSource = ceBLL.ObjEF.DestinatarioEmails.OrderBy(t => t.nome_destinatario).ToList();
                else
                    GridDestinatariosDoEmail.DataSource = ceBLL.ObjEF.DestinatarioEmails.Where(it => 
                        it.nome_destinatario.ToUpper().StartsWith(txtProcuraDestinatario.Text.ToUpper())).OrderBy(t => t.nome_destinatario).ToList();
                GridDestinatariosDoEmail.DataBind();
            }
        }



        
        protected void GridDestinatariosDoEmail_RowEditing(object sender, GridViewEditEventArgs e)
        {
            DestinatarioEmailBLL deBLL = new DestinatarioEmailBLL();
            deBLL.Get(Convert.ToInt32(GridDestinatariosDoEmail.DataKeys[e.NewEditIndex]["id_destinatario"].ToString()));
            deBLL.Delete();
            deBLL.SaveChanges();
            SetModifyEnvEmail();
            e.Cancel = true;
        }

        protected void btEnviarEmail_Click(object sender, EventArgs e)
        {
            CorrespondenciaEmailBLL ceBLL = new CorrespondenciaEmailBLL();
            ceBLL.Get(Convert.ToInt32(txt_id_correspEmail.Text));
            ceBLL.EnviarEmail();
            setGridDestinatariosDoEmail();
            gridEnvEmail.DataBind();
            Util.ShowMessage(String.Format("Circular enviada com sucesso!", ObjBLL.ObjEF.num));
            cDataEnviado.Text = Util.DateToString(ceBLL.ObjEF.enviadoEm);
            SetModifyEnvEmail();
        }

        protected void btProcuraDestinatario_Click(object sender, EventArgs e)
        {
            setGridDestinatariosDoEmail();
        }

        protected void btReencaminharEmail_Click(object sender, EventArgs e)
        {
            var ceBLL = new CorrespondenciaEmailBLL();
            ceBLL.Get(Convert.ToInt32(txt_id_correspEmail.Text));
            ceBLL.CriarEmailParaOsQueNaoLeram();
            txt_id_correspEmail.Text = Convert.ToString(ceBLL.ObjEF.id_correspEmail);
            cDataEnviado.Text = String.Empty;
            gridEnvEmail.DataBind();            
            Util.ShowMessage(String.Format("Email da circular criado com sucesso!", ObjBLL.ObjEF.num));
            SetModifyEnvEmail();            
        }

        protected void GridDestinatariosDoEmail_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridDestinatariosDoEmail.PageIndex = e.NewPageIndex;
            setGridDestinatariosDoEmail();
        }
    }
}
