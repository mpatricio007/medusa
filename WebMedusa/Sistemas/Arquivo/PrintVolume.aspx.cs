using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.BLL;
using Medusa.LIB;

namespace Medusa.Sistemas.Arquivo
{
    public partial class PrintVolume : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string id_volume = Request.QueryString["id"];
            if (!String.IsNullOrEmpty(id_volume))
            {
                VolumeBLL vol = new VolumeBLL();
                int id = 0;
                int.TryParse(id_volume, out id);
                vol.Get(id);
                Image1.ImageUrl = String.Format("~/Sistemas/Comum/BarCode.aspx?code={0}", id);
                Image1.DataBind();

                if (vol.ObjEF.id_volume != 0)
                {

                    this.txtNome.Text = vol.ObjEF.nome;
                    this.txtDescricao.Text =  vol.ObjEF.descricao;
                    this.txtCoordenador.Text = String.Format("{0} - {1}", vol.ObjEF.coordenador, vol.ObjEF.num);
                    this.txtProjeto.Text = String.Format("FUSP - {0}", vol.ObjEF.projeto);
                    this.txtProjetoA.Text = String.Format("Projeto A: {0}", vol.ObjEF.projetoA);
                    this.Id_volume.Text = Convert.ToString(vol.ObjEF.id_volume);
                }
            }
        }
    }
}