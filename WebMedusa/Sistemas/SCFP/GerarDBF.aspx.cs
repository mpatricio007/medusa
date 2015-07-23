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
using System.Data.Entity;
using System.Data;


namespace Medusa.Sistemas.RemessaBancaria
{
    public partial class GerarDBF : Page
    {
        protected  void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               // base.Page_Load(sender, e);
            }
        }

        protected void btImportar_Click(object sender, EventArgs e)
        {
            string strMsg = String.Empty;
            try
            {


                CriaDbf c = new CriaDbf();
                c.dt = cData.Value.GetValueOrDefault();
                c.listaDBF = new List<DadosDBF>();
                vDespesasToDbfBLL l = new vDespesasToDbfBLL();
                var lst = l.Find(t => t.data_pagto == c.dt).ToList();
                foreach (var it in lst)
                {
                    DadosDBF d = new DadosDBF();
                    d.projeto = it.projeto;
                    d.rp_rd = "";
                    d.item = Convert.ToInt32(it.codigo);
                    d.iss = 0;
                    d.calcinss = "N";
                    d.pensao = 0;
                    d.valinss = 0;
                    d.inss11 = 0;
                    d.valir = 0;
                    d.data = it.data_pagto;
                    d.historico = it.nome;
                    d.valpago = it.debitoProjeto != 0 ? it.debitoProjeto : it.creditoProjeto * -1;
                    d.valcc = 0;
                    d.cpmf = "N";
                    c.listaDBF.Add(d);
                }
                c.CriarDBF();
                strMsg = "Criação do DBF realizada com sucesso!";
            }
            catch (Exception ex)
            {

                strMsg = ex.Message;
            }
            finally
            {
                Util.ChamarScript("hideProgressDialog();", "");
                Util.ShowMessage(strMsg);
            }
     
        }

        protected void btCancelar_Click(object sender, EventArgs e)
        {
            //cData.Value = null;
            //cDdlTipoFolhaPagto.id_tipo_folha_pagto = 0;
        }
    }
}