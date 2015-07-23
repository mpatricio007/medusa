﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.LIB;
using Medusa.BLL;

namespace Medusa.Sistemas.Conciliacao
{
    public partial class IdentificadorDepositos : PageCrud<IdentificadorDepositoBLL>
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_identificador";
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
            }
        }

        protected override void Get()
        {
            ObjBLL.Get(PkValue);
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_identificador);
            this.DdlContas1.Id_Conta = ObjBLL.ObjEF.id_conta;
            this.cTextoNum.Text = ObjBLL.ObjEF.num_identificador;
            this.cInteiroProjeto.Value = ObjBLL.ObjEF.cod_def_projeto;
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_identificador = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.id_conta = this.DdlContas1.Id_Conta;
            ObjBLL.ObjEF.num_identificador = this.cTextoNum.Text;
            ObjBLL.ObjEF.cod_def_projeto = this.cInteiroProjeto.Value.GetValueOrDefault();
        }




    }
}