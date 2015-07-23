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
    public partial class Fornecedores : PageCrud<FornecedorBLL>
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_fornecedor";
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
            _btExcluir = new Button();
            _btExcluir0 = new Button();
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

                var c = new CategoriaBLL();
                listaCategoria.DataSource = c.GetAll("nome");
                listaCategoria.Items.Insert(0, new ListItem("selecione uma categoria...", "0"));
                listaCategoria.DataBind();

                var r = new RamoAtividadeBLL();
                ListaRamoAtividade.DataSource = r.GetAll("nome");
                ListaRamoAtividade.Items.Insert(0, new ListItem("selecione um ramo de atividade...", "0"));
                ListaRamoAtividade.DataBind();

                SetView();
            }
        }

        protected override void Get()
        {
            ObjBLL.Get(PkValue);
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_fornecedor);
            this.cTextoNome.Text = ObjBLL.ObjEF.nome;
            this.cCNPJ.Value = ObjBLL.ObjEF.cnpj;
            this.cEnder1.Value = ObjBLL.ObjEF.ender;
            this.ListaRamoAtividade.SelectedValue = Convert.ToString(ObjBLL.ObjEF.id_ramo_atividade.GetValueOrDefault());
            this.listaCategoria.SelectedValue = Convert.ToString(ObjBLL.ObjEF.id_categoria.GetValueOrDefault());
            listaCategoria_SelectedIndexChanged(null, null);

            gridHistorico.DataSource = ObjBLL.ObjEF.Historicos.ToList();
            gridHistorico.DataBind();

            gridDocumentos.DataSource = ObjBLL.ObjEF.Documentos.Where(it => it.id_status_docFornecedor != StatusDocFornecedorBLL.Cancelado).ToList();
            gridDocumentos.DataBind();
             
            this.cTextoFantasia.Text = ObjBLL.ObjEF.nome_fantasia;
            this.cTextoGrupoEconomico.Text = ObjBLL.ObjEF.grupo_economico;
            this.cTextoEstadual.Text = ObjBLL.ObjEF.inscr_estadual;
            this.cTextoMunicipal.Text = ObjBLL.ObjEF.inscr_municipal;
            this.cTextoHomePage.Text = ObjBLL.ObjEF.home_page;
            this.cEmail1.Value = ObjBLL.ObjEF.email;
            this.cTelefone1.Value = ObjBLL.ObjEF.telefone;
            this.cTextoRegistro.Text = ObjBLL.ObjEF.registro_numero;
            this.cDataAlteracao.Value = ObjBLL.ObjEF.data_alteracao;
            this.cTextoNumero.Text = ObjBLL.ObjEF.numero_alteracao;
            this.txtValorCapitalSocial.Text = Util.DecimalToString(ObjBLL.ObjEF.capital_social);
            this.cDdlAno1.Value = Convert.ToInt32(ObjBLL.ObjEF.ano_patrimonial);
            this.lbValidade.Text = Util.DateToString(ObjBLL.ObjEF.validade);

            panelCadastro2.Visible = ObjBLL.Exists();

            if (ObjBLL.Exists())
            {
                btEnviar.Text = "salvar alterações e enviar para FUSP";

                ObjBLL.ObjEF.StatusFornecedor = ObjBLL.ObjEF.StatusFornecedor ?? new StatusFornecedor();

                ControleRepresentanteLegal1.Id_fornecedor = ObjBLL.ObjEF.id_fornecedor;
                ControleRepresentanteLegal1.EnableGravacao = ObjBLL.ObjEF.StatusFornecedor.edicao;
                ControleRepresentanteLegal1.DataBind();

                ControleSocios1.Id_fornecedor = ObjBLL.ObjEF.id_fornecedor;
                ControleSocios1.EnableGravacao = ObjBLL.ObjEF.StatusFornecedor.edicao;
                ControleSocios1.DataBind();

                ControleDiretor1.Id_fornecedor = ObjBLL.ObjEF.id_fornecedor;
                ControleDiretor1.EnableGravacao = ObjBLL.ObjEF.StatusFornecedor.edicao;
                ControleDiretor1.DataBind();

                ControleReferenciaBancaria2.Id_fornecedor = ObjBLL.ObjEF.id_fornecedor;
                ControleReferenciaBancaria2.EnableGravacao = ObjBLL.ObjEF.StatusFornecedor.edicao;
                ControleReferenciaBancaria2.DataBind();

                dGravacao.Visible = ObjBLL.ObjEF.StatusFornecedor.edicao;
                dGravacao1.Visible = ObjBLL.ObjEF.StatusFornecedor.edicao;

                btImprimir.Visible = !ObjBLL.ObjEF.StatusFornecedor.enviar;
                btImprimirCRC.Visible = ObjBLL.EmitirCRC();
                //btImprimirCadastro.Visible = !ObjBLL.ObjEF.StatusFornecedor.enviar;
                btEnviar.Visible = ObjBLL.ObjEF.StatusFornecedor.enviar;
            }
            else //if(ObjBLL.ObjEF.StatusFornecedor.edicao)
                btEnviar.Text = "enviar para FUSP";
        }

        protected override void SetAdd()
        {
            dGravacao.Visible = dGravacao1.Visible = true;
            btEnviar.Visible = false;
            base.SetAdd();
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_fornecedor = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.nome = this.cTextoNome.Text;
            ObjBLL.ObjEF.cnpj = this.cCNPJ.Value;
            ObjBLL.ObjEF.ender = this.cEnder1.Value;
            if (this.listaCategoria.SelectedValue != "0")
                ObjBLL.ObjEF.id_categoria = Convert.ToInt32(this.listaCategoria.SelectedValue);
            if (this.ListaRamoAtividade.SelectedValue != "0")
                ObjBLL.ObjEF.id_ramo_atividade = Convert.ToInt32(this.ListaRamoAtividade.SelectedValue);
            ObjBLL.ObjEF.nome_fantasia = this.cTextoFantasia.Text;
            ObjBLL.ObjEF.grupo_economico = this.cTextoGrupoEconomico.Text;
            ObjBLL.ObjEF.inscr_estadual = this.cTextoEstadual.Text;
            ObjBLL.ObjEF.inscr_municipal = this.cTextoMunicipal.Text;
            ObjBLL.ObjEF.home_page = this.cTextoHomePage.Text;
            ObjBLL.ObjEF.telefone = this.cTelefone1.Value;
            ObjBLL.ObjEF.email = this.cEmail1.Value;
            ObjBLL.ObjEF.registro_numero = this.cTextoRegistro.Text;
            ObjBLL.ObjEF.data_alteracao = this.cDataAlteracao.Value;
            ObjBLL.ObjEF.numero_alteracao = this.cTextoNumero.Text;
            ObjBLL.ObjEF.capital_social = Util.StringToDecimal(this.txtValorCapitalSocial.Text);
            ObjBLL.ObjEF.ano_patrimonial = this.cDdlAno1.Value;
            ObjBLL.ObjEF.id_usuario = SecurityBLL.GetCurrentUsuario().id_usuario;

        } 

        protected override void btAlterar_Click(object sender, EventArgs e)
        {
            ObjBLL.Get(PkValue);
            Set();
            if (ObjBLL.ObjEF.id_fornecedor != 0)
            {
                ObjBLL.Update();
                if (ObjBLL.SaveChanges())
                {
                    msg("alteração efetuada");
                }
            }
            else
                msgError("erro alteração");
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_fornecedor);
        }

        protected void btEnviar_Click(object sender, EventArgs e)
        {
            Page.Validate("");
            if (!Page.IsValid)
                ScriptManager.GetCurrent(Page).SetFocus(vsErros);
            else
            {
                try
                {
                    string txtMsg;
                    ObjBLL.Get(PkValue);
                    Set();
                    if (ObjBLL.ValidarCadastro(out txtMsg))
                    {
                        if (ObjBLL.EnviarParaAnalise())
                            msg("Informamos que o cadastramento eletrônico no Cadastro de Pessoa Jurídica da FUSP foi efetuado com sucesso. Aguardando encaminhamento dos documentos para análise.");
                        ObjBLL.Detach();
                        Get();
                    }
                    else
                        msgError(txtMsg);
                }
                catch (Exception ex)
                {
                    msgError("Erro! " + ex.Message);
                }
            }
            
        }

        protected override void btInserir_Click(object sender, EventArgs e)
        {
            ObjBLL.Get(this.cCNPJ.Value);
            if (ObjBLL.ObjEF.id_fornecedor == 0)
            {
                Set();
                ObjBLL.Add();
                if (ObjBLL.SaveChanges())
                {
                    this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_fornecedor);
                    PkValue = ObjBLL.ObjEF.id_fornecedor;
                    Get();
                    msg("inclusão efetuada");
                    SetModify();
                }
                else
                {
                    msgError("erro na inclusão");
                }
            }
            else
            {
                Util.ShowMessage("CNPJ já cadastrado!");
                return;
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
            //Util.NovaJanela(String.Format("ReportFornecedor.aspx?pk={0}", PkValue.ToString().Criptografar()));
            Util.NovaJanela(String.Format("ReportCadastro.aspx?pk={0}", PkValue.ToString().Criptografar()));
        }

        protected void btImprimirCRC_Click(object sender, EventArgs e)
        {
            Util.NovaJanela(String.Format("ReportCRC.aspx?pk={0}", PkValue.ToString().Criptografar()));
        }

        //protected override void SetView()
        //{
        //    base.SetView();
        //    btImprimir.Visible = false;
        //    btImprimirCRC.Visible = false;
        //}

        protected override void SetGrid()
        {

            Filter f = new Filter()
            {
                property = "id_usuario",
                value = Convert.ToString(SecurityBLL.GetCurrentUsuario().id_usuario),
                mode = "=="
            };
            filtros.Add(f);
            base.SetGrid();
            filtros.Remove(f);
        }

        //protected void btImprimirCadastro_Click(object sender, EventArgs e)
        //{
        //    Util.NovaJanela(String.Format("ReportCadastro.aspx?pk={0}", PkValue.ToString().Criptografar()));
        //}
    }
}



 