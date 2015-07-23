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
    public partial class RecibosFusp : PageCrud<ReciboFuspBLL>
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_recibo_fusp";
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
                base.Page_Load(sender, e);
                _btExcluir.OnClientClick = String.Empty;
                _btExcluir0.OnClientClick = String.Empty;

                rbDocumento.DataSource = Enum.GetNames(typeof(TipoInscricao));
                rbDocumento.DataBind();

                base.Page_Load(sender, e);
                _btExcluir.OnClientClick = String.Empty;
                _btExcluir0.OnClientClick = String.Empty;
            }
        }

        protected override void Get()
        {
            ObjBLL.Get(PkValue);
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_recibo_fusp);
            this.txtUsuario.Text = ObjBLL.ObjEF.id_usuario != 0 ? ObjBLL.ObjEF.Usuario.PessoaFisica.nome : SecurityBLL.GetCurrentUsuario().PessoaFisica.nome;
            this.cData.Value = ObjBLL.ObjEF.data;
            this.cTextoNome.Text = ObjBLL.ObjEF.nome;
            this.cTextoDescricao.Text = ObjBLL.ObjEF.descricao;
            this.cTextoTipoPagto.Text = ObjBLL.ObjEF.tipo_pagamento;
            this.cValor.Value = ObjBLL.ObjEF.valor;
            this.cTextoMotivo.Text = ObjBLL.ObjEF.motivo_cancel;
            this.cTextoObs.Text = ObjBLL.ObjEF.observacao;
            this.cDdlProjeto1.Id_projeto = Convert.ToInt32(ObjBLL.ObjEF.id_projeto);
            cCPF.Value = ObjBLL.ObjEF.cpf;
            cCNPJ2.Value = ObjBLL.ObjEF.cnpj;
            btExcluir.Visible = btExcluir0.Visible = ObjBLL.Exists() ? ObjBLL.ObjEF.status_recibo : false;
            dMotivo.Visible = ObjBLL.Exists();

            rbDocumento.SelectedValue = Convert.ToString(ObjBLL.ObjEF.tipo);
            rbDocumento_SelectedIndexChanged(null, null);
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_recibo_fusp = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.data = Convert.ToDateTime(this.cData.Value);
            ObjBLL.ObjEF.nome = this.cTextoNome.Text;
            ObjBLL.ObjEF.descricao = this.cTextoDescricao.Text;
            ObjBLL.ObjEF.tipo_pagamento = this.cTextoTipoPagto.Text;
            ObjBLL.ObjEF.valor = this.cValor.Value.GetValueOrDefault();
            ObjBLL.ObjEF.motivo_cancel = this.cTextoMotivo.Text;
            ObjBLL.ObjEF.observacao = this.cTextoObs.Text;
            ObjBLL.ObjEF.id_projeto = this.cDdlProjeto1.Id_projeto;
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
            btImprimirRecibo.Visible = true;
            btImprimirRecibo0.Visible = true;
            rbDocumento.Enabled = false;
            panelCampos.Enabled = false;
            base.SetModify();
        }

        protected override void SetView()
        {
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
                    PkValue = ObjBLL.ObjEF.id_recibo_fusp;
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
            Util.NovaJanela(String.Format("ReportFusp.aspx?pk={0}", PkValue.ToString().Criptografar()));
        }
    }
}