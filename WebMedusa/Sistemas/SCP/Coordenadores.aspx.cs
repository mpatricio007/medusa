using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.LIB;
using Medusa.BLL;
using Medusa.DAL;

namespace Medusa.Sistemas.SCP
{
    public partial class Coordenadores : PageCrud<CoordenadorBLL>
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_coordenador";
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
                UnidadeBLL u = new UnidadeBLL();
                listaUnidade.DataSource = u.GetAll("nome");
                listaUnidade.Items.Insert(0, new ListItem("selecione uma unidade...", "0"));
                listaUnidade.DataBind();
                base.Page_Load(sender, e);
            }
        }

        protected override void Get()
        {
            ObjBLL.Get(PkValue);
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_coordenador);

            if (ObjBLL.ObjEF.PessoaFisica == null)
                ObjBLL.ObjEF.PessoaFisica = new PessoaFisica();

            this.listaUnidade.SelectedValue = Convert.ToString(ObjBLL.ObjEF.id_unidade);
            listaUnidade_SelectedIndexChanged(null, null);

            this.listaDepto.SelectedValue = Convert.ToString(ObjBLL.ObjEF.id_departamento.GetValueOrDefault());
            listaDepto_SelectedIndexChanged(null, null);

            getPessoaFisica(ObjBLL.ObjEF.PessoaFisica);


            this.listaLab.SelectedValue = Convert.ToString(ObjBLL.ObjEF.id_laboratorio.GetValueOrDefault());

            this.cCPF1.Focus();
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_coordenador = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.PessoaFisica = setPessoaFisica();

            ObjBLL.ObjEF.id_unidade = Convert.ToInt32(this.listaUnidade.SelectedValue);
            var id_depto = 0;
            int.TryParse(this.listaDepto.SelectedValue, out id_depto);
            ObjBLL.ObjEF.id_departamento = id_depto != 0 ? id_depto : new Nullable<int>();

            var id_lab = 0;
            int.TryParse(this.listaLab.SelectedValue, out id_lab);
            ObjBLL.ObjEF.id_laboratorio = id_lab != 0 ? id_lab : new Nullable<int>();
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

        protected void getPessoaFisica(PessoaFisica PessoaFisica)
        {
            this.txtId_pessoa.Text = Convert.ToString(PessoaFisica.id_pessoa);
            this.cTextoNome.Text = PessoaFisica.nome;
            this.cDataNascto.Value = PessoaFisica.dtNascto;
            this.cDdlSexo1.Value = PessoaFisica.sexo;
            this.cCPF1.Value = PessoaFisica.cpf;
            this.cTextoRG.Text = PessoaFisica.rg;
            
            this.cListaPessoaEmails1.Value = PessoaFisica.Emails.ToList();
            this.cListaPessoaEmails1.DataBind();
            
            this.ListaPessoaTelefones1.Value = PessoaFisica.Telefones.ToList();
            this.ListaPessoaTelefones1.DataBind();
            
            this.cEnder1.Value = PessoaFisica.Enderecos.Count > 0 ? ObjBLL.ObjEF.PessoaFisica.Enderecos.First().endereco
                : new DAL.Endereco();            
        }

        protected PessoaFisica setPessoaFisica()
        {
            var PessoaFisica = new PessoaFisica();
            PessoaFisica.id_pessoa = Convert.ToInt32(this.txtId_pessoa.Text);
            PessoaFisica.nome = this.cTextoNome.Text;
            PessoaFisica.sexo = this.cDdlSexo1.Value;
            PessoaFisica.dtNascto = this.cDataNascto.Value.GetValueOrDefault();
            PessoaFisica.cpf = this.cCPF1.Value;
            PessoaFisica.rg = this.cTextoRG.Text;

            PessoaFisica.SetEmails = true;
            PessoaFisica.Emails = this.cListaPessoaEmails1.Value;

            PessoaFisica.SetTelefones = true;
            PessoaFisica.Telefones = this.ListaPessoaTelefones1.Value;

            PessoaFisica.SetEnderecos = true;
            PessoaFisica.Enderecos.Add(new DAL.PessoaEndereco(this.cEnder1.Value));

            return PessoaFisica;
            
        }

        protected void cCPF1_OnTextChanged(object sender, EventArgs e)
        {
            if (cCPF1.IsValid)
            {
                var cpf = cCPF1.Value;
                ObjBLL.Get(cCPF1.Value);
                getPessoaFisica(ObjBLL.ObjEF.PessoaFisica);
                this.cCPF1.Value = cpf;                
                this.cTextoNome.Focus();
            }
        }

    }
}
