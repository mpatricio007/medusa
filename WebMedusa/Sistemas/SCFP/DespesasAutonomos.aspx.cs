﻿using System;
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

namespace Medusa.Sistemas.SCFP
{
    public partial class DespesasAutonomos : PageCrud<DespesaAutonomoBLL>
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_lancto";
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
                base.Page_Load(sender, e);
                BancoBLL b = new BancoBLL();
                listaBanco.DataSource = b.GetAll("codigo");
                listaBanco.Items.Insert(0, new ListItem("selecione um banco...", "0"));
                listaBanco.DataBind();
            }
        }

        protected override void Get()
        {
            ObjBLL.Get(PkValue);
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_lancto);
            this.cDdlProjeto1.Id_projeto = ObjBLL.ObjEF.id_projeto;
            this.cValorPagto.Value = ObjBLL.ObjEF.valor;
            this.cData1.Value = ObjBLL.ObjEF.data_pagto;
            this.cTextoRp.Text = ObjBLL.ObjEF.rp;
            cValorDeducaoInss.Value = ObjBLL.ObjEF.deducaoINSS;
            rbIss.SelectedValue = Convert.ToString(ObjBLL.ObjEF.iss);
            CPF c = new CPF();
            c.Value = ObjBLL.ObjEF.cpf;
            cCPF1.Value = c;
            cTextoNome.Text = ObjBLL.ObjEF.nome;
            this.cTextoAgencia.Text = ObjBLL.ObjEF.agencia;
            this.cTextoDigitoAgencia.Text = ObjBLL.ObjEF.digito_agencia;
            this.cTextoConta.Text = ObjBLL.ObjEF.conta;
            this.cTextoDigitoConta.Text = ObjBLL.ObjEF.digito_conta;
            this.listaBanco.SelectedValue = Util.InteiroToString(ObjBLL.ObjEF.id_banco);
            cDdlProjeto1_SelectedIndexChanged(null, null);
            cIntNdep.Value = ObjBLL.ObjEF.num_dependentes;
            PreencheGrid();

        }

        protected void PreencheGrid()
        {

            if (ObjBLL.Exists())
            {
                GridView1.DataSource = ObjBLL.LctosBeneficiario(ObjBLL.ObjEF.Itens.ToList());
                GridView2.DataSource = ObjBLL.LctosProjeto(ObjBLL.ObjEF.Itens.ToList());
                GridView3.DataSource = ObjBLL.LctosProvisaoImpostos(ObjBLL.ObjEF.Itens.ToList());
            }
            GridView1.DataBind();
            GridView2.DataBind();
            GridView3.DataBind();
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_lancto = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.id_projeto = this.cDdlProjeto1.Id_projeto;
            ObjBLL.ObjEF.valor = this.cValorPagto.Value.GetValueOrDefault();
            ObjBLL.ObjEF.data_pagto = this.cData1.Value.GetValueOrDefault();
            ObjBLL.ObjEF.deducaoINSS = cValorDeducaoInss.Value.GetValueOrDefault();
            ObjBLL.ObjEF.iss = Convert.ToBoolean(rbIss.SelectedValue);
            ObjBLL.ObjEF.rp = this.cTextoRp.Text;

            CPF c = new CPF();
            c = cCPF1.Value;
            ObjBLL.ObjEF.cpf = c.Value;
            ObjBLL.ObjEF.nome = cTextoNome.Text;
            ObjBLL.ObjEF.agencia = this.cTextoAgencia.Text;
            ObjBLL.ObjEF.digito_agencia = this.cTextoDigitoAgencia.Text;
            ObjBLL.ObjEF.conta = this.cTextoConta.Text;
            ObjBLL.ObjEF.digito_conta = this.cTextoDigitoConta.Text;
            ObjBLL.ObjEF.id_banco = Util.StringToInteiro(listaBanco.SelectedValue).GetValueOrDefault();
            ObjBLL.ObjEF.num_dependentes = cIntNdep.Value.GetValueOrDefault(0);
            

        }   

        protected void cDdlProjeto1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            var proj = new ProjetoBLL();
            proj.Get(cDdlProjeto1.Id_projeto);
            GridCoordenadores.DataSource = proj.ObjEF.Coordenadores;
            GridCoordenadores.DataBind();

            GridPatrocinadores.DataSource = proj.ObjEF.Financiadores;
            GridPatrocinadores.DataBind();

            lblTitulo.Text = proj.ObjEF.titulo;
            lblTitulo.DataBind();
        }

        protected override void btInserir_Click(object sender, EventArgs e)
        {
            string strMsg = "erro inclusão";
            Set();
            if (ObjBLL.DataIsValid(ref strMsg))
            {                   
                    msg(ObjBLL.Agendar());
                    PkValue = 0;
                    ObjBLL.Detach();
                    Get();
            }
            else
                msgError(strMsg);
            
        }

        protected void btCalcular_Click(object sender, EventArgs e)
        {
            lblMsg.Text = String.Empty;
            string strMsg = String.Empty;
            Set();
            if (ObjBLL.DataIsValid(ref strMsg))
            {
                List<LancamentoItem> l = ObjBLL.ProcessarPlanosContas();
                GridView1.DataSource = ObjBLL.LctosBeneficiario(l);
                GridView2.DataSource = ObjBLL.LctosProjeto(l);
                GridView3.DataSource = ObjBLL.LctosProvisaoImpostos(l);
            }
            else
                msgError(strMsg);
            GridView1.DataBind();
            GridView2.DataBind();
            GridView3.DataBind();
            
        }

        protected void btImportar_Click(object sender, EventArgs e)
        {
            
        }

        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            GridViewRow footer = GridView1.FooterRow;
            if (footer != null)
            {
                decimal cred = 0;
                decimal deb = 0;
                foreach (GridViewRow row in GridView1.Rows)
                {                  
                    cred += decimal.Parse(row.Cells[2].Text);
                    deb += decimal.Parse(row.Cells[3].Text);
                }
                footer.Cells[2].Text = string.Format("{0:n2}", cred);
                footer.Cells[3].Text = string.Format("{0:n2}", deb);
                footer.Cells[4].Text = string.Format("Líquido: {0:n2}", cred - deb);
            }
        }

        protected void GridView2_DataBound(object sender, EventArgs e)
        {
            GridViewRow footer = GridView2.FooterRow;
            if (footer != null)
            {
                decimal cred = 0;
                decimal deb = 0;
                foreach (GridViewRow row in GridView2.Rows)
                {
                    cred += decimal.Parse(row.Cells[2].Text);
                    deb += decimal.Parse(row.Cells[3].Text);
                }
                footer.Cells[2].Text = string.Format("{0:n2}", cred);
                footer.Cells[3].Text = string.Format("{0:n2}", deb);
            }
        }

        protected void GridView3_DataBound(object sender, EventArgs e)
        {
            GridViewRow footer = GridView3.FooterRow;
            if (footer != null)
            {
                decimal cred = 0;
                decimal deb = 0;
                foreach (GridViewRow row in GridView3.Rows)
                {
                    cred += decimal.Parse(row.Cells[2].Text);
                    deb += decimal.Parse(row.Cells[3].Text);
                }

                footer.Cells[2].Text = string.Format("{0:n2}", cred);
                footer.Cells[3].Text = string.Format("{0:n2}", deb);
            }
        }

        protected override void btAlterar_Click(object sender, EventArgs e)
        {

            string strMsg = "erro inclusão";
            ObjBLL.Get(PkValue);
            Set();
            if (ObjBLL.DataIsValid(ref strMsg))
            {
                msg(ObjBLL.ReAgendar());
                Get();
                ObjBLL.Detach();

            }
            else
                msgError(strMsg);
        }

      
        

      
    }
}