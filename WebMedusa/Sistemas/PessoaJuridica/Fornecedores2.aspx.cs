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
using Microsoft.Reporting.WebForms;
using Medusa.Relatorios.PessoaJuridica;
using System.Collections;

namespace Medusa.Sistemas.PessoaJuridica
{
    public partial class Fornecedores2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //// chave primária da tabela
            //PRIMARY_KEY = "id_fornecedor";
            ////valor da chave primária
            //PkValue = Convert.ToInt32(txtCodigo.Text);
            //pCadastro = panelCadastro;
            //lbMsg = lblMsg;
            //_btAlterar0 = btAlterar0;

            if (!IsPostBack)
            {
                var c = new CategoriaBLL();
                listaCategoria.DataSource = c.GetAll("nome");
                listaCategoria.Items.Insert(0, new ListItem("selecione uma categoria...", "0"));
                listaCategoria.DataBind();

                var r = new RamoAtividadeBLL();
                ListaRamoAtividade.DataSource = r.GetAll("nome");
                ListaRamoAtividade.Items.Insert(0, new ListItem("selecione um ramo de atividade...", "0"));
                ListaRamoAtividade.DataBind();

                //GetQueryString();

                cEnder1.Value = new DAL.Endereco();
            }
        }


        protected void listaCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            CategoriaBLL categoriaBLL = new CategoriaBLL();
            categoriaBLL.Get(Convert.ToInt32(this.listaCategoria.SelectedValue));
            listDocumentos.Items.Clear();
            listDocumentos.DataSource = categoriaBLL.ObjEF.CategoriaDocumentos.OrderBy(it => it.Documento.nome).Select(it => it.Documento);
            listDocumentos.DataBind();
        }

        protected void btImprimir_Click(object sender, EventArgs e)
        {
            //var r = new RelatorioFornecedor();
            //ReportDataSource rpd = new ReportDataSource("dsRelatorioFornecedor", r.GerarRelatorio(Convert.ToInt32(PkValue)));
            //ReportViewer1.LocalReport.DataSources.Clear();
            //ReportViewer1.LocalReport.DataSources.Add(rpd);
            //ReportViewer1.LocalReport.Refresh();
            //SetPrint();
        }
    }

}
