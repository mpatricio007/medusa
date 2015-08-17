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
using Medusa.Relatorios.Recibos;
using Microsoft.Reporting.WebForms;

namespace Medusa.Sistemas.Recibos
{
    public partial class Recibos : PageCrud<ReciboBLL>
    {
        Dictionary<string, string> dicSearch = new Dictionary<string, string>()
        {
            {"curso","id_recibo_curso"},
        };

        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_recibo";
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

                Seguranca = false;

                string um = Request.QueryString["um"];
                string pm = Request.QueryString["pm"];

                var usuMySQL = new vUsuariosDemonstrativoBLL();
                if (!usuMySQL.Login(um, pm) || um == null || pm == null)
                    Response.Redirect("http://demonstrativo.fusp.org.br");
                else
                {
                    listaCurso.DataSource = lista.DataSource = usuMySQL.GetRecibosCursos();
                    lista.Items.Insert(0, new ListItem("selecione um curso...", "0"));
                    lista.DataBind();

                    listaCurso.Items.Insert(0, new ListItem("selecione um curso...", "0"));
                    listaCurso.DataBind();

                    rbDocumento.DataSource = Enum.GetNames(typeof(TipoInscricao));
                    rbDocumento.DataBind();

                    base.Page_Load(sender, e);
                    _btExcluir.OnClientClick = String.Empty;
                    _btExcluir0.OnClientClick = String.Empty;

                }
            }
        }

        protected override void Get()
        {
            var vUsuDemonstrativo = new vUsuariosDemonstrativoBLL();

            ObjBLL.Get(PkValue);
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_recibo);
            this.txtUsuario.Text = ObjBLL.Exists() ?
               ObjBLL.ObjEF.Usuario.nome : vUsuDemonstrativo.GetCurrentvUsuariosDemonstrativo().nome;
            this.cData.Value = ObjBLL.ObjEF.data;
            this.cTextoNome.Text = ObjBLL.ObjEF.nome;
            this.cTextoDescricao.Text = ObjBLL.ObjEF.descricao;
            this.cTextoTipoPagto.Text = ObjBLL.ObjEF.tipo_pagamento;
            this.lista.SelectedValue = Util.InteiroToString(ObjBLL.ObjEF.id_recibo_curso);
            this.cValor.Value = ObjBLL.ObjEF.valor;
            this.cTextoMotivo.Text = ObjBLL.ObjEF.motivo_cancel;
            this.cTextoObs.Text = ObjBLL.ObjEF.observacao;
            cCPF.Value = ObjBLL.ObjEF.cpf;
            cCNPJ2.Value = ObjBLL.ObjEF.cnpj;
            ControleReciboCheques1.Id_recibo = ObjBLL.ObjEF.id_recibo;
            ControleReciboCheques1.DataBind();
            btExcluir.Visible = btExcluir0.Visible = ObjBLL.Exists() ? ObjBLL.ObjEF.status_recibo.GetValueOrDefault() : false;
            //btInserir.Visible = btInserir0.Visible = ObjBLL.Exists() ? ObjBLL.ObjEF.status_recibo.GetValueOrDefault() : false;
            //btCancelar.Visible = btCancelar0.Visible = ObjBLL.Exists() ? ObjBLL.ObjEF.status_recibo.GetValueOrDefault() : false;
            dMotivo.Visible = ObjBLL.Exists();

            rbDocumento.SelectedValue = Convert.ToString(ObjBLL.ObjEF.tipo);
            rbDocumento_SelectedIndexChanged(null, null);
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_recibo = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.data = Convert.ToDateTime(this.cData.Value);
            ObjBLL.ObjEF.nome = this.cTextoNome.Text;
            ObjBLL.ObjEF.descricao = this.cTextoDescricao.Text;
            ObjBLL.ObjEF.tipo_pagamento = this.cTextoTipoPagto.Text;
            ObjBLL.ObjEF.id_recibo_curso = Util.StringToInteiro(lista.SelectedValue).GetValueOrDefault();
            ObjBLL.ObjEF.valor = this.cValor.Value.GetValueOrDefault();
            ObjBLL.ObjEF.motivo_cancel = this.cTextoMotivo.Text;
            ObjBLL.ObjEF.observacao = this.cTextoObs.Text;
            ObjBLL.ObjEF.tipo = (TipoInscricao)Enum.Parse(typeof(TipoInscricao), rbDocumento.SelectedValue);
            ObjBLL.ObjEF.cnpj = cCNPJ2.Value = ObjBLL.ObjEF.tipo == TipoInscricao.CNPJ ? cCNPJ2.Value : new CNPJ();
            ObjBLL.ObjEF.cpf = cCPF.Value = ObjBLL.ObjEF.tipo == TipoInscricao.CPF ? cCPF.Value : new CPF();
        }

        protected override void SetAdd()
        {
            lbMsg.Visible = true;
            lbMsg.Text = String.Empty;
            btInserir0.Visible = true;
            btInserir.Visible = true;
            btExcluir0.Visible = false;
            btExcluir.Visible = false;
            btCancelar0.Visible = true;
            btCancelar.Visible = true;
            panelCheques.Visible = false;
            btImprimirRecibo.Visible = false;
            btImprimirRecibo0.Visible = false;
            rbDocumento.Enabled = true;
            panelCampos.Enabled = true;
            base.SetAdd();
        }

        protected override void SetModify()
        {
            lbMsg.Visible = true;
            lbMsg.Text = String.Empty;
            btInserir.Visible = false;
            btInserir0.Visible = false;
            btExcluir.Visible = true;
            btExcluir0.Visible = true;
            btCancelar.Visible = false;
            btCancelar0.Visible = false;
            panelCheques.Visible = !ObjBLL.Exists() || ObjBLL.ObjEF.status_recibo.GetValueOrDefault();
            btImprimirRecibo.Visible = true;
            btImprimirRecibo0.Visible = true;
            rbDocumento.Enabled = false;
            panelCampos.Enabled = false;
            base.SetModify();
        }

        protected override void SetView()
        {
            panelCheques.Visible = false;
            btImprimirRecibo.Visible = false;
            base.SetView();
        }

        protected override void btInserir_Click(object sender, EventArgs e)
        {
            Set();
            if (ObjBLL.DataIsValid())
            {
                ObjBLL.Add();
                if (ObjBLL.SaveChanges())
                {
                    msg("inclusão efetuada");
                    PkValue = ObjBLL.ObjEF.id_recibo;
                    ObjBLL.Detach();
                    Get();
                    SetModify();
                }
                else
                    msgError("erro inclusão");
            }
        }

        protected override void btExcluir_Click(object sender, EventArgs e)
        {
            ObjBLL.Get(PkValue);
            ObjBLL.ObjEF.motivo_cancel = cTextoMotivo.Text;
            if (ObjBLL.DataIsValid())
            {
                ObjBLL.Delete();
                if (ObjBLL.SaveChanges())
                {
                    msg("exclusão efetuada");
                    PkValue = 0;
                    Get();
                    SetAdd();
                }
                else
                    msgError("erro alteração");
            }
            else
                msgError("erro exclusão");
        }

        protected override void btCancelar_Click(object sender, EventArgs e)
        {
            PkValue = 0;
            Get();
        }


        protected void rbDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tipo = (TipoInscricao)Enum.Parse(typeof(TipoInscricao), rbDocumento.SelectedValue);
            divCnpj.Visible = tipo == TipoInscricao.CNPJ;
            divCnpj.DataBind();
            divCpf.Visible = !divCnpj.Visible;
            divCpf.DataBind();
        }

        protected void btImprimirRecibo_Click(object sender, ImageClickEventArgs e)
        {
            Util.NovaJanela(String.Format("../../Sistemas/Recibos/Report.aspx?pk={0}", PkValue.ToString().Criptografar()));
        }

        protected override void SetGrid()
        {
            int id_recibo_curso = 0;

            if (!_ckFilter.Checked)
                filtros.Clear();

            switch (ddlOptions.SelectedValue)
            {
                case ("curso"):
                    {
                        int.TryParse(listaCurso.SelectedValue, out id_recibo_curso);
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
                id_recibo_curso);

            _grid.DataBind();
            DataListFiltros.DataBind();
            this._txtProcura.Text = String.Empty;
        }

        protected override void ddlOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddlOptions.SelectedValue)
            {
                case ("curso"):
                    {
                        listaCurso.SelectedValue = "0";
                        GetModes(typeof(string));
                        listaCurso.Visible = true;
                        ddlMode.Visible = txtProcura.Visible = false;
                        break;
                    }
                default:
                    {
                        listaCurso.SelectedValue = "0";
                        ddlMode.Visible = txtProcura.Visible = true;
                        listaCurso.Visible = false;
                        base.ddlOptions_SelectedIndexChanged(sender, e);
                        break;
                    }
            }
        }

        protected void lista_SelectedIndexChanged(object sender, EventArgs e)
        {
            var rcBLL = new ReciboCursoBLL();
            rcBLL.Get(Convert.ToInt32(lista.SelectedValue));
            cValor.Value = rcBLL.ObjEF.valor;
        }
    }
}