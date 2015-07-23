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
using System.Text;
using Medusa.Relatorios.Projeto;
using Microsoft.Reporting.WebForms;

namespace Medusa.Sistemas.SCP
{
    public partial class ProjetosDefinitivos : PageCrud<ProjetoBLL>
    {
        Dictionary<string, string> dicSearch = new Dictionary<string, string>()
        {
            {"coordenador(es)","Coordenadores.Coordenador.PessoaFisica.nome"},
            {"financiador(es)","Financiadores.Financiador.nome"},
            {"natureza","NaturezaProjeto.nome"},
            {"status","id_ultimo_status"}
        };

        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela 
            PRIMARY_KEY = "id_projeto";
            //valor da chave primária
            PkValue = Convert.ToInt32(this.txtCodigo.Text);
            // painel do grid
            pGrid = panelGrid;
            // painel do formulário de alteração
            pCadastro = panelCadastro;
            // gridview
            _grid = grid;
            lbMsg = lblMsg;
            _btAlterar = new Button();
            _btAlterar0 = new Button();
            _btInserir = new Button();
            _btInserir0 = new Button();
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

                UnidadeBLL u = new UnidadeBLL();
                listaUnidade.DataSource = u.GetAll("nome");
                listaUnidade.Items.Insert(0, new ListItem("selecione uma unidade...", "0"));
                listaUnidade.DataBind();

                var coorBLL = new CoordenadorBLL();
                listaCoordenador.DataSource = from c in coorBLL.GetAll("PessoaFisica.nome").OfType<Coordenador>()
                                   select new
                                   {
                                       c.PessoaFisica.nome,
                                       c.id_coordenador
                                   };

                listaCoordenador.Items.Insert(0, new ListItem("selecione um coordenador...", "0"));
                listaCoordenador.DataBind();

                FinanciadorBLL f = new FinanciadorBLL();
                listaFinanciador.DataSource = f.GetAll("nome");
                listaFinanciador.Items.Insert(0, new ListItem("selecione um financiador...", "0"));
                listaFinanciador.DataBind();

                StatusProjetoBLL s = new StatusProjetoBLL();
                listaStatus.DataSource = s.GetAll("nome");
                listaStatus.Items.Insert(0, new ListItem("selecione um status...", "0"));
                listaStatus.DataBind();

                var np = new NaturezaProjetoBLL();
                listaNatProjeto.DataSource = np.GetAll("nome");
                listaNatProjeto.Items.Insert(0, new ListItem("selecione uma natureza...", "0"));
                listaNatProjeto.DataBind();
            }
            else
                lblMsg.Text = String.Empty;
        }

        protected override void SetView()
        {
            btCodDefinitivo.Visible = false;
            btExportToExcel.Visible = true;
            base.SetView();
        }

        protected override void SetAdd()
        {
            btExportToExcel.Visible = false;
            base.SetAdd();
        }

        protected override void SetModify()
        {
            btExportToExcel.Visible = false;
            base.SetModify();
        }

        protected override void Get()
        {
            ObjBLL.Get(PkValue);
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_projeto);          
            ProjetoCoordenadores1.Id_projeto = ObjBLL.ObjEF.id_projeto;
            ProjetoCoordenadores1.DataBind();            
            lbStatus.Text = ObjBLL.ObjEF.id_ultimo_status.HasValue ? ObjBLL.ObjEF.StatusProjeto.nome : String.Empty;

            ProjetoFinanciadores1.Id_projeto = ObjBLL.ObjEF.id_projeto;
            ProjetoFinanciadores1.DataBind();
            ControleEnderecoProjeto1.Id_projeto = ObjBLL.ObjEF.id_projeto;
            ControleEnderecoProjeto1.DataBind();
            ControleProjetoDocumento1.Id_projeto = ObjBLL.ObjEF.id_projeto;
            ControleProjetoDocumento1.DataBind();

            ControleTaxaProjeto1.Id_projeto = ObjBLL.ObjEF.id_projeto;
            ControleTaxaProjeto1.DataBind();

            this.lblCodigoA.Text = Convert.ToString(ObjBLL.ObjEF.cod_a_projeto);
            this.cDataAberturaA.Value = ObjBLL.ObjEF.data_cod_a;
            this.cIntCod_def_projeto.Value = ObjBLL.ObjEF.cod_def_projeto;
            this.cDataAberturaDef.Value = ObjBLL.ObjEF.data_proj_def;
            if(ObjBLL.ObjEF.ProjetoSolicitacao != null)
                cDdlProjetoSolicitacao1.Id_proj_sol = ObjBLL.ObjEF.ProjetoSolicitacao.id_sol_proj;
            cTextoSubProj.Text = ObjBLL.ObjEF.sub_projeto;
            lbSubProjeto.Text = ObjBLL.ObjEF.sub_projeto;

            cTextoSigla.Text = ObjBLL.ObjEF.sigla;
            listaUnidade.SelectedValue = Convert.ToString(ObjBLL.ObjEF.id_unidade.GetValueOrDefault());
            listaUnidade_SelectedIndexChanged(null, null);
            listaDepto.SelectedValue = Convert.ToString(ObjBLL.ObjEF.id_departamento.GetValueOrDefault());
            listaDepto_SelectedIndexChanged(null, null);
            listaLab.SelectedValue = Convert.ToString(ObjBLL.ObjEF.id_laboratorio.GetValueOrDefault());
            cDataInicio.Value = ObjBLL.ObjEF.data_inicio;
            cDataTermino.Value = ObjBLL.ObjEF.data_termino;
            cValorProjeto.Value = ObjBLL.ObjEF.valor;
            cData_1_recebto.Value = ObjBLL.ObjEF.data_1_recebto;
            cValor_1_recebto.Value = ObjBLL.ObjEF.valor_1_recebto;
            cDdlMoeda1.Id_moeda = ObjBLL.ObjEF.id_moeda.GetValueOrDefault();
            cDdlNaturezaProjeto.Id_nat_projeto = ObjBLL.ObjEF.id_nat_projeto.GetValueOrDefault();
            cTextoTitulo.Text = ObjBLL.ObjEF.titulo;
            cTextoObjetivo.Text = ObjBLL.ObjEF.objetivo;
            cDdlAtuacao1.Id_atuacao = ObjBLL.ObjEF.id_atuacao.GetValueOrDefault();

            cDdlClassificacaoFusp1.Id_classificacao_fusp = ObjBLL.ObjEF.id_classificacao_fusp.GetValueOrDefault();

            ControleRequisitosEncerramento1.Id_projeto = ObjBLL.ObjEF.id_projeto;
            ControleRequisitosEncerramento1.DataBind();

            cDdlInstrumentoContratual1.Id_instrumento_contratual = ObjBLL.ObjEF.id_instrumento_contratual.GetValueOrDefault();
            cTextoInstrumentoContratual.Text = ObjBLL.ObjEF.num_contrato;

            //cDdlClassificacao1.Id_classificacao = Convert.ToInt32(ObjBLL.ObjEF.id_classificacao);
            cTextoPrograma.Text = ObjBLL.ObjEF.nome_programa;
            ControleHistoricoProjeto1.Id_projeto = ObjBLL.ObjEF.id_projeto;
            ControleHistoricoProjeto1.DataBind();
            ControleContatoProjeto1.Id_projeto = ObjBLL.ObjEF.id_projeto;
            ControleContatoProjeto1.DataBind();
            btCodDefinitivo.Visible = !ObjBLL.ObjEF.cod_def_projeto.HasValue;
            lbCodDef.Visible = ObjBLL.ObjEF.cod_def_projeto.HasValue;
            lbCodDef.Text = Util.InteiroToString(ObjBLL.ObjEF.cod_def_projeto);
            ControleSetorResponsavel1.Id_projeto = ObjBLL.ObjEF.id_projeto;
            ControleSetorResponsavel1.DataBind();
            ControleAssinProjeto1.Id_projeto = ObjBLL.ObjEF.id_projeto;
            ControleAssinProjeto1.DataBind();
            ControleArquivoAnexo1.Id_projeto = ObjBLL.ObjEF.id_projeto;
            ControleArquivoAnexo1.DataBind();
            ControleOpcaoAdiantamento1.Id_projeto = ObjBLL.ObjEF.id_projeto;
            ControleOpcaoAdiantamento1.DataBind();
            lblSubProjA.Text = ObjBLL.ObjEF.sub_projeto_a;
            
            lblCodSub.Visible = lbSubProjeto.Visible = lbSubProjeto.Text != "";
            lblSubA.Visible = lblSubProjA.Visible = lblSubProjA.Text != "";

           

            Util.ChamarScript("callPrint();", "");
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_projeto = Convert.ToInt32(txtCodigo.Text);
            ObjBLL.ObjEF.cod_def_projeto = cIntCod_def_projeto.Value;

            ObjBLL.ObjEF.id_unidade = Util.StringToInteiroOrNullable(listaUnidade.SelectedValue);
            ObjBLL.ObjEF.id_departamento = Util.StringToInteiroOrNullable(listaDepto.SelectedValue);
            ObjBLL.ObjEF.id_laboratorio = Util.StringToInteiroOrNullable(listaLab.SelectedValue);

            ObjBLL.ObjEF.data_proj_def = this.cDataAberturaDef.Value.GetValueOrDefault();
            ObjBLL.ObjEF.data_inicio = this.cDataInicio.Value.GetValueOrDefault();
            ObjBLL.ObjEF.data_termino = this.cDataTermino.Text != String.Empty ? this.cDataTermino.Value.GetValueOrDefault() : new Nullable<DateTime>();
            ObjBLL.ObjEF.data_1_recebto = cData_1_recebto.Value;
            ObjBLL.ObjEF.valor_1_recebto = cValor_1_recebto.Value;
            ObjBLL.ObjEF.titulo = this.cTextoTitulo.Text;
            ObjBLL.ObjEF.objetivo = this.cTextoObjetivo.Text;
            ObjBLL.ObjEF.id_atuacao = Util.IntToInteiroOrNullable(cDdlAtuacao1.Id_atuacao);
            //ObjBLL.ObjEF.id_classificacao = Util.IntToInteiroOrNullable(cDdlClassificacao1.Id_classificacao);
            ObjBLL.ObjEF.nome_programa = this.cTextoPrograma.Text;
            ObjBLL.ObjEF.id_nat_projeto = Util.IntToInteiroOrNullable(cDdlNaturezaProjeto.Id_nat_projeto);
            ObjBLL.ObjEF.data_proj_def = cDataAberturaDef.Value;
            ObjBLL.ObjEF.sigla = cTextoSigla.Text;
            ObjBLL.ObjEF.id_moeda = Util.IntToInteiroOrNullable(cDdlMoeda1.Id_moeda);
            ObjBLL.ObjEF.valor = Convert.ToDecimal(this.cValorProjeto.Value.GetValueOrDefault());
            ObjBLL.ObjEF.sub_projeto = cTextoSubProj.Text;
            ObjBLL.ObjEF.id_classificacao_fusp = cDdlClassificacaoFusp1.Id_classificacao_fusp;

            ObjBLL.ObjEF.id_instrumento_contratual = cDdlInstrumentoContratual1.Id_instrumento_contratual;
            ObjBLL.ObjEF.num_contrato = cTextoInstrumentoContratual.Text;
        }

        protected override void grid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            base.grid_RowEditing(sender, e);
        }

        protected void btDefinitivo_Click(object sender, EventArgs e)
        {
            ObjBLL.Get(Convert.ToInt32(this.txtCodigo.Text));
            try
            {
                Set();
                
                if (ObjBLL.GerarProjetoDefinitivo(cIntCod_def_projeto.Value.GetValueOrDefault(), cTextoSubProj.Text))
                {
                    Util.ChamarScript("closeDialog();", "close");
                    ObjBLL.Detach();
                    Get();
                    lblMsg.Text = String.Empty;
                }
            }
            catch (Exception ex)
            {
                Util.ShowMessage(ex.Message);
            }
        }                                                                                                               

        protected void rbTipoEnderecoCorrespondencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ender = new Endereco();
            var rbTipoEnderecoCorrespondencia = (RadioButtonList)sender;
            switch (rbTipoEnderecoCorrespondencia.SelectedValue)
            {
                case ("unidade"):
                    {
                        var uBLL = new UnidadeBLL();
                        uBLL.Get(Convert.ToInt32(this.listaUnidade.SelectedValue));
                        ender = uBLL.ObjEF.ender;
                        break;
                    }
                case ("departamento"):
                    {
                        var dBLL = new DepartamentoBLL();
                        dBLL.Get(Convert.ToInt32(this.listaDepto.SelectedValue));
                        ender = dBLL.ObjEF.ender;
                        break;
                    }
                case ("laboratorio"):
                    {
                        var lBLL = new LaboratorioBLL();
                        lBLL.Get(Convert.ToInt32(this.listaLab.SelectedValue));
                        ender = lBLL.ObjEF.ender;
                        break;
                    }
                case ("outros"):
                    {
                        break;
                    }
            }

            ControleEnderecoProjeto1.endereco = ender;
        }

        protected void btAtualizarProjA_Click(object sender, EventArgs e)
        {
            ObjBLL.Get(PkValue);
            Set();
            ObjBLL.Update();
            if (ObjBLL.SaveChanges())
                msg("alteração efetuada!");
            else
                msgError("erro alteração");
        }

        protected override void btProcurar_Click(object sender, EventArgs e)
        {

            int id_ultimo_status = 0;
            int id_coodernador = 0;
            int id_financiador = 0;
            int id_nat_projeto = 0;


            if (!_ckFilter.Checked)
                filtros.Clear();

            switch (ddlOptions.SelectedValue)
            {
                case ("financiador"):
                    {
                        int.TryParse(listaFinanciador.SelectedValue, out id_financiador);
                        break;
                    }
                case ("coordenador"):
                    {
                        int.TryParse(listaCoordenador.SelectedValue, out id_coodernador);
                        break;
                    }
                case ("status"):
                    {                        
                        int.TryParse(listaStatus.SelectedValue, out id_ultimo_status);
                        break;
                    }
                case ("natureza"):
                    {
                        int.TryParse(listaNatProjeto.SelectedValue, out id_nat_projeto);
                        break;
                    }
                default:
                    {
                        if (!String.IsNullOrEmpty(this._txtProcura.Text))
                            filtros.Add(setFilter());
                        break;
                    }
            }
            

            SetView();


            if (_ddlSize.SelectedValue == "0")
                _grid.PageSize = 50;
            else
                _grid.PageSize = Convert.ToInt32(_ddlSize.SelectedValue);         
            

            int size = 10 * Convert.ToInt32(this._ddlSize.SelectedValue);
            _grid.DataKeyNames = new string[] { PRIMARY_KEY };
            _grid.DataSource = ObjBLL.Find(
                filtros,
                (string)ViewState["SortExpression"],
                (string)ViewState["SortDirection"], size,
                id_coodernador, id_financiador, id_ultimo_status, id_nat_projeto);

            _grid.DataBind();            
            DataListFiltros.DataBind();
            this._txtProcura.Text = String.Empty;
        }

        protected void btCodDefinitivo_Click(object sender, EventArgs e)
        {
            ObjBLL.Get(Convert.ToInt32(this.txtCodigo.Text));
            string txtMsg;
            if (ObjBLL.ValidarCadastro(out txtMsg))
                Util.ChamarScript("openDialog()", "");
            else
                msgError(txtMsg);
        }

        protected override void msgError(string msg)
        {
            lbMsg.BackColor = System.Drawing.Color.Red;
            lbMsg.ForeColor = System.Drawing.Color.White;
            lbMsg.Text = msg;
        }

        protected void listaUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            UnidadeBLL unidadeBLL = new UnidadeBLL();
            unidadeBLL.Get(Convert.ToInt32(this.listaUnidade.SelectedValue));
            listaDepto.Items.Clear();
            listaDepto.DataSource = unidadeBLL.ObjEF.Departamentos.OrderBy(it => it.nome);
            listaDepto.Items.Insert(0, new ListItem("selecione um departamento...", "0"));
            listaDepto.DataBind();
            this.listaDepto.Focus();
        }

        protected void listaDepto_SelectedIndexChanged(object sender, EventArgs e)
        {
            var departamentoBLL = new DepartamentoBLL();
            departamentoBLL.Get(Convert.ToInt32(this.listaDepto.SelectedValue));
            listaLab.Items.Clear();

            listaLab.DataSource = departamentoBLL.ObjEF.Laboratorios.OrderBy(it => it.nome);
            listaLab.Items.Insert(0, new ListItem("selecione um laboratório...", "0"));
            listaLab.DataBind();
            this.listaLab.Focus();
        }

        protected override void ddlOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddlOptions.SelectedValue)
            {
                case ("coordenador"):
                    {
                        listaFinanciador.SelectedValue = "0";
                        GetModes(typeof(string));
                        listaCoordenador.Visible = true;
                        listaFinanciador.Visible = false;
                        listaStatus.Visible = false;
                        listaNatProjeto.Visible = false;
                        ddlMode.Visible = txtProcura.Visible = false;
                        break;
                    }

                case ("financiador"):
                    {
                        listaCoordenador.SelectedValue = "0";                        
                        GetModes(typeof(string));
                        listaCoordenador.Visible = false;
                        listaFinanciador.Visible = true;
                        listaStatus.Visible = false;
                        listaNatProjeto.Visible = false;
                        ddlMode.Visible = txtProcura.Visible = false;
                        break;
                    }
                case ("status"):
                    {
                        listaCoordenador.SelectedValue = "0";
                        GetModes(typeof(string));
                        listaCoordenador.Visible = false;
                        listaFinanciador.Visible = false;
                        listaStatus.Visible = true;
                        listaNatProjeto.Visible = false;
                        ddlMode.Visible = txtProcura.Visible = false;
                        break;
                    }
                case ("natureza"):
                    {
                        listaNatProjeto.SelectedValue = "0";
                        GetModes(typeof(string));
                        listaCoordenador.Visible = false;
                        listaFinanciador.Visible = false;
                        listaStatus.Visible = false;
                        listaNatProjeto.Visible = true;
                        ddlMode.Visible = txtProcura.Visible = false;
                        break;
                    }
                default:
                    {
                        listaCoordenador.SelectedValue = "0";
                        listaFinanciador.SelectedValue = "0";
                        ddlMode.Visible = txtProcura.Visible = true;
                        listaCoordenador.Visible = false;
                        listaFinanciador.Visible = false;
                        listaStatus.Visible = false;
                        listaNatProjeto.Visible = false;
                        base.ddlOptions_SelectedIndexChanged(sender, e);
                        break;
                    }
            }
            
        }       

        public override void VerifyRenderingInServerForm(Control control)
        {
            //return;
        }

        protected void btExportToExcel_Click(object sender, EventArgs e)
        {               
            var gridToExport = _grid;
            gridToExport.AllowPaging = false;
            gridToExport.AllowSorting = false;
            var lstColumns = gridToExport.Columns.OfType<CommandField>().ToList();
            foreach (var item in lstColumns)
                gridToExport.Columns.Remove(item);

            foreach (var item in gridToExport.Columns.OfType<TemplateField>().ToList())
            {   
                var t = (TemplateField)item;
                try
                {
                    t.ItemTemplate = new GridViewTemplate(dicSearch[item.ToString()]);
                }
                catch (Exception)
                {

                    t.ItemTemplate = new GridViewTemplate(item.SortExpression);
                }
                
            }

            gridToExport.DataSource = ObjBLL.Find(
                filtros,
                (string)ViewState["SortExpression"],
                (string)ViewState["SortDirection"], 0);
            gridToExport.DataBind();
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("content-disposition", String.Format("attachment;filename={0}.xls", gridToExport.Caption));
            Response.Charset = String.Empty;
            EnableViewState = false;


            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

            gridToExport.RenderControl(htw);

            Response.Write(sw.ToString());
            Response.End();
        }

        protected void btImprimir_Click(object sender, EventArgs e)
        {
            dRelatorio.InnerHtml = String.Format("<iframe src=\"../../Relatorios/Projeto/RProjeto.aspx?pk={0}\" width=\"100%\" height=\"1000px\"></iframe>", PkValue.ToString().Criptografar());
            dRelatorio.DataBind();
        }
    }
}
