using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.BLL;
using Medusa.DAL;
using Medusa.LIB;

namespace Medusa.Controles.Pesquisa
{
    public partial class PesquisaUsuarios : System.Web.UI.UserControl
    {
        string cacheName = Util.InteiroToString(SecurityBLL.GetCurrentUsuario().id_usuario);

        public List<Usuario> Value
        {
            get
            {
                if (cDdlSetorEntrada1.Visible)
                {
                    SetorBLL s = new SetorBLL();
                    s.Get(cDdlSetorEntrada1.Id_Setor);
                    return s.ObjEF.UsuarioFusp.Select(it => (Usuario)it).DefaultIfEmpty(new Usuario()).Where(it => it.status).ToList();
                }
                else
                {
                    UsuarioBLL u = new UsuarioBLL();
                    u.Get(DdlUsuariosFuspEntradas1.Id_usuario);
                    return new List<Usuario>() { u.ObjEF };
                }
            }
            //get
            //{
            //    if (Cache[cacheName] == null)
            //        Cache[cacheName] = new List<Usuario>();
            //    return (List<Usuario>)Cache[cacheName];
            //}
            //set
            //{
            //    Cache.Remove(cacheName);
            //    Cache[cacheName] = value;
            //}
        }

        //private int index
        //{
        //    get
        //    {
        //        return Convert.ToInt32(txtCodigo.Text);
        //    }
        //    set
        //    {
        //        txtCodigo.Text = Convert.ToString(value);

        //    }
        //}

        //public bool EnableValidator
        //{
        //    get
        //    {
        //        return DdlUsuariosFuspEntradas1.EnableValidator;
        //    }
        //    set
        //    {
        //        cDdlSetorEntrada1.EnableValidator = DdlUsuariosFuspEntradas1.EnableValidator = value;
        //    }
        //}

        public string ValidationGroup
        {
            get
            {
                return DdlUsuariosFuspEntradas1.ValidationGroup;
            }
            set
            {
                cDdlSetorEntrada1.ValidationGroup = DdlUsuariosFuspEntradas1.ValidationGroup = value;
            }
        }

        private UsuarioBLL uBLL;

        public UsuarioBLL UBLL
        {
            get
            {
                if (uBLL == null)
                    uBLL = new UsuarioBLL();
                return uBLL;
            }
            set { uBLL = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarTipoBusca();
            }
        }

        public void CarregarTipoBusca()
        {
            rbTipoBusca.Items.Clear();
            Cache.Remove(cacheName);
            //UBLL.Get(SecurityBLL.GetCurrentUsuario().id_usuario);
            //Value = new List<Usuario>() { UBLL.ObjEF };
            rbTipoBusca.DataSource = Enum.GetValues(typeof(TipoBuscaUsu));
            rbTipoBusca.DataBind();
            rbTipoBusca.SelectedIndex = 0;
            rbTipoBusca_SelectedIndexChanged(null, null);
        }

        //protected void SetGrid()
        //{
        //    grid.DataSource = Value;
        //    grid.DataBind();
        //}

        //public override void DataBind()
        //{
        //    SetGrid();
        //    //panelCadastro.Visible = !ObjBLL.EhBloqueado(Id_adiantamento);
        //    base.DataBind();
        //}

        protected void rbTipoBusca_SelectedIndexChanged(object sender, EventArgs e)
        {
            cDdlSetorEntrada1.Visible = (TipoBuscaUsu)Enum.Parse(typeof(TipoBuscaUsu), rbTipoBusca.SelectedValue) == TipoBuscaUsu.setor;
            DdlUsuariosFuspEntradas1.Visible = !cDdlSetorEntrada1.Visible;
        }

        protected void DdlUsuariosFuspEntradas1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            //Cache.Remove(cacheName);
            //UsuarioBLL u = new UsuarioBLL();
            //u.Get(DdlUsuariosFuspEntradas1.Id_usuario);
            //Value = new List<Usuario>()
            //{
            //    u.ObjEF
            //};
            //cDdlSetorEntrada1.DataBind();
        }

        protected void cDdlSetorEntrada1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            //Cache.Remove(cacheName);
            //SetorBLL s = new SetorBLL();
            //s.Get(cDdlSetorEntrada1.Id_Setor);
            //Value = s.ObjEF.UsuarioFusp.Select(it => (Usuario)it).DefaultIfEmpty(new Usuario()).Where(it => it.status).ToList();
            //DdlUsuariosFuspEntradas1.DataBind();
        }

        public override void DataBind()
        {
            CarregarTipoBusca();
            cDdlSetorEntrada1.DataBind();
            DdlUsuariosFuspEntradas1.DataBind();
            rbTipoBusca_SelectedIndexChanged(null, null);
        }
    }

    public enum TipoBuscaUsu
    {
        setor,
        usuário
    }
}