using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.BLL;
using Medusa.LIB;

namespace Medusa
{
    public partial class Principal : BasePage
    {
        protected List<ListaUsuario> lista
        {
            get
            {
                if (ViewState["listaUsuarios"] == null)
                    lista = new List<ListaUsuario>();
                return (List<ListaUsuario>)ViewState["listaUsuarios"];
            }
            set
            {
                ViewState["listaUsuarios"] = value;
            }
        }


        protected override void Page_Load(object sender, EventArgs e)
        {  
            if (!IsPostBack)
            {
                Seguranca = false;
                base.Page_Load(sender, e);
                dtTarefasPendentes.DataBind();
                dtTarefasEncerradas.DataBind();
            }

        }
       

        protected void Button1_Click(object sender, EventArgs e)
        {
            Button bt = (Button)sender;
            Session["id_sistema"] = Convert.ToInt32(bt.CommandArgument);
            SistemaBLL s = new SistemaBLL();
            Response.Redirect(s.ResponseUrl(Convert.ToInt32(bt.CommandArgument)));
        }

        protected void lbMinhasTarefas()
        {
            this.CollapsiblePanelExtender1.Collapsed = false;
            this.CollapsiblePanelExtender1.ClientState = "false";
            this.CollapsiblePanelExtender2.Collapsed = true;
            this.CollapsiblePanelExtender2.ClientState = "true";
            this.CollapsiblePanelExtender3.Collapsed = true;
            this.CollapsiblePanelExtender3.ClientState = "true";
        }

        protected void lbMinhasTarefasEncerradas()
        {
            this.CollapsiblePanelExtender1.Collapsed = true;
            this.CollapsiblePanelExtender1.ClientState = "true";
            this.CollapsiblePanelExtender2.Collapsed = false;
            this.CollapsiblePanelExtender2.ClientState = "false";
            this.CollapsiblePanelExtender3.Collapsed = true;
            this.CollapsiblePanelExtender3.ClientState = "true";
        }

        protected void btAlterar0_Click(object sender, EventArgs e)
        {
            if (cvUsuarios.IsValid)
            {
                TarefaBLL t = new TarefaBLL();
                t.CriarTarefa(this.cTextoTarefa.Text, lista, SecurityBLL.GetCurrentUsuario().id_usuario);
                dtTarefasPendentes.DataBind();
                lbMinhasTarefas();
                LimparTarefa();
            }
        }
        

        protected void btCancelar0_Click(object sender, EventArgs e)
        {
            LimparTarefa();
            lbMinhasTarefas();
        }

        protected void LimparTarefa()
        {
            this.cTextoTarefa.Text = String.Empty;
            lista = new List<ListaUsuario>();
            dlUsuarios.DataBind();
        }

        protected void lkEncerrar_Click(object sender, EventArgs e)
        {
            ImageButton bt = (ImageButton)sender;
            int id_tarefa = Convert.ToInt32(bt.CommandArgument);
            TarefaBLL tBLL = new TarefaBLL();
            tBLL.EncerrarTarefa(id_tarefa, SecurityBLL.GetCurrentUsuario().id_usuario);
            dtTarefasPendentes.DataBind();
            dtTarefasEncerradas.DataBind();
        }

        protected void lkProvidencia_Click(object sender, EventArgs e)
        {
            ImageButton bt = (ImageButton)sender;
            int id_tarefa = Convert.ToInt32(bt.CommandArgument);
            TarefaBLL tfBLL = new TarefaBLL();
            tfBLL.Get(id_tarefa);

            TextBox txt = (TextBox)bt.FindControl("txtProvidencia");
            TarefaProvidenciaBLL tpBLL = new TarefaProvidenciaBLL();
            tpBLL.PostarProvidencia(tfBLL.ObjEF.id_tarefa, txt.Text, SecurityBLL.GetCurrentUsuario().id_usuario);
            txt.Text = String.Empty;
            DataList dl = (DataList)bt.FindControl("dlProvidencias");
            dl.DataSource = tfBLL.ObjEF.Providencias;
            dl.DataBind();
        }
      

        protected void dtTarefasPendentes_DataBinding(object sender, EventArgs e)
        {
            TarefaBLL t = new TarefaBLL();
            dtTarefasPendentes.DataSource = t.MinhasTarefas(SecurityBLL.GetCurrentUsuario().id_usuario);            
        }

        protected void lkShowPosts_Click(object sender, EventArgs e)
        {
            ImageButton bt = (ImageButton)sender;
            int id_tarefa = Convert.ToInt32(bt.CommandArgument);
            TarefaBLL tfBLL = new TarefaBLL();
            tfBLL.Get(id_tarefa);

            Panel p = (Panel)bt.FindControl("pComentarios");
            p.Visible = !p.Visible;

            DataList dl = (DataList)bt.FindControl("dlProvidencias");
            dl.DataSource = tfBLL.ObjEF.Providencias;
            dl.DataBind();
           
        }

        protected void lkShowText_Click(object sender, EventArgs e)
        {
            Panel p = (Panel)((Control)sender).FindControl("pProvidencias");
            p.Visible = !p.Visible;
        }

        protected void lkPendente_Click(object sender, EventArgs e)
        {
            ImageButton bt = (ImageButton)sender;
            int id_tarefa = Convert.ToInt32(bt.CommandArgument);
            TarefaBLL tBLL = new TarefaBLL();
            tBLL.TornarPendente(id_tarefa, SecurityBLL.GetCurrentUsuario().id_usuario);
            dtTarefasEncerradas.DataBind();
            dtTarefasPendentes.DataBind();
        }

        protected void dtTarefasEncerradas_DataBinding(object sender, EventArgs e)
        {
            TarefaBLL t = new TarefaBLL();
            dtTarefasEncerradas.DataSource = t.MinhasTarefasEncerradas(SecurityBLL.GetCurrentUsuario().id_usuario);            
        }

        protected void dlUsuarios_DataBinding(object sender, EventArgs e)
        {
            dlUsuarios.DataSource = lista;
        }

        protected void dlUsuarios_DeleteCommand(object source, DataListCommandEventArgs e)
        {
            lista.RemoveAt(e.Item.ItemIndex);
            dlUsuarios.DataBind();
        }

        protected void btSelecionar_Click(object sender, EventArgs e)
        {
            UsuarioFuspBLL usuBLL = new UsuarioFuspBLL();
            usuBLL.Get(Convert.ToInt32(this.cDdlUsuarioFUSP1.Id_usuario));
            if (!lista.Exists(it => it.id_pessoa == usuBLL.ObjEF.id_pessoa))
            {
                lista.Add(new ListaUsuario(usuBLL.ObjEF));
                dlUsuarios.DataBind();
            }
        }

        protected void cvUsuarios_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = lista.Count() > 0;
        }

        protected void dlProvidencias_DeleteCommand(object source, DataListCommandEventArgs e)
        {
            
            int id_providencia = Convert.ToInt32(e.CommandArgument);

            TarefaProvidenciaBLL tpBLL = new TarefaProvidenciaBLL();
            tpBLL.Get(id_providencia);

            TarefaBLL tfBLL = new TarefaBLL();
            tfBLL.Get(tpBLL.ObjEF.id_tarefa);

            tpBLL.ExcluirProvidencia(id_providencia, SecurityBLL.GetCurrentUsuario().id_usuario);

            DataList dl = (DataList)source;
            dl.DataSource = tfBLL.ObjEF.Providencias;
            dl.DataBind();
        }

        protected void dtTarefasPendentes_DeleteCommand(object source, DataListCommandEventArgs e)
        {
            int id_tarefa = Convert.ToInt32(e.CommandArgument);
            TarefaBLL tfBLL = new TarefaBLL();
            tfBLL.ExcluirTarefa(id_tarefa, SecurityBLL.GetCurrentUsuario().id_usuario);
            dtTarefasPendentes.DataBind();
        }
    }
}