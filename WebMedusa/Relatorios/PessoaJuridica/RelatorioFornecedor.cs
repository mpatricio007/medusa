using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medusa.DAL;
using Medusa.BLL;
using System.Text;
using Medusa.LIB;

namespace Medusa.Relatorios.PessoaJuridica
{
    public class RelatorioFornecedor
    {
        public string cnpj { get; set; }

        public string nome { get; set; }

        public string ramoatividade { get; set; }

        public string categoria { get; set; }

        public string nome_fantasia { get; set; }

        public string data_alteracao { get; set; }

        public string numero_alteracao { get; set; }

        public string capital_social { get; set; }

        public string ano_patrimonial { get; set; }

        public string grupo_economico { get; set; }

        public string inscr_estadual { get; set; }

        public string inscr_municipal { get; set; }

        public string home_page { get; set; }

        public string email { get; set; }

        public string registro_numero { get; set; }

        public string endereco { get; set; }

        public string telefone { get; set; }

        public string documento { get; set; }

        public string validadeCadastro { get; set; }

        public string msgValidade { get; set; }

        public string text1 { get; set; }

        public string text2 { get; set; }

        public string text3 { get; set; }

        public string textRef { get; set; }

        public string socios { get; set; }

        public string representante { get; set; }

        private FornecedorBLL forBLL = new FornecedorBLL();
        public RelatorioFornecedor()
        {
        }
        
        public RelatorioFornecedor(Fornecedor f)
        {
            cnpj = f.cnpj.Value;
            nome = f.nome;
            ramoatividade = f.id_ramo_atividade.HasValue ? f.RamoAtividade.nome : String.Empty;
            categoria = f.id_categoria.HasValue ? f.Categoria.nome : String.Empty;
            nome_fantasia = f.nome_fantasia;
            data_alteracao = f.data_alteracao.HasValue ? String.Format("{0:d}", f.data_alteracao) : String.Empty;
            numero_alteracao = f.numero_alteracao.ToString();
            capital_social = f.capital_social.HasValue ? String.Format("{0:N2}", f.capital_social) : String.Empty;
            ano_patrimonial = f.ano_patrimonial.ToString();
            grupo_economico = f.grupo_economico;
            inscr_estadual = f.inscr_estadual;
            inscr_municipal = f.inscr_municipal;
            home_page = f.home_page.ToString();
            email = f.email.value;
            registro_numero = f.registro_numero.ToString();
            endereco = f.ender.ToString();
            telefone = f.telefone.ToString();
            
            validadeCadastro = DateTime.Now > f.validade ? String.Format("Vencido em {0:d}", f.validade) : String.Format("{0:d}", f.validade);
            
            var txtDocs = new StringBuilder();
            foreach (var item in f.Categoria.CategoriaDocumentos)
                txtDocs.AppendLine(item.Documento.nome);
            documento = txtDocs.ToString();


            var alteracao = f.Historicos.Where(it => it.id_status_fornecedor == 3).Count() > 0;
          

            var txt1 = new StringBuilder();
            txt1.AppendLine("À");
            txt1.AppendLine("Fundação de Apoio à Universidade de São Paulo – FUSP");
            txt1.AppendLine("Av. Afrânio Peixoto, 14 – Butantã");
            txt1.AppendLine("05507-000 – São Paulo – SP ");
            txt1.AppendLine("A/C: Setor de Licitação e Contratos");
            text1 = txt1.ToString();

            var txt2 = new StringBuilder();
            txt2.AppendFormat("A empresa {0}, inscrita no CNPJ sob o nº {1}, com sede na {2}, solicita a {4} no Cadastro de Pessoa Jurídica da FUSP, na categoria de {3}.",
                f.nome,f.cnpj.Value,f.ender.ToString(),f.Categoria.nome, alteracao ? "atualização" : "inscrição");
            txt2.AppendLine();
            txt2.AppendLine();
            txt2.Append("Para tal, anexamos à presente solicitação o formulário de Cadastro de Pessoa Jurídica, devidamente preenchido e assinado, juntamente com os documentos abaixo listados:");
            text2 = txt2.ToString();

            var txt3 = new StringBuilder();
            txt3.AppendLine("Declaramos, sob as penas de lei, que:");
            txt3.AppendLine();  
            txt3.AppendLine("   a)	As informações constantes desta solicitação são verdadeiras;");
            txt3.AppendLine("   b)	Atestamos a autenticidade de todos os documentos apresentados;");
            txt3.AppendLine("   c)	Autorizamos a realização de quaisquer diligências por parte da FUSP;");
            txt3.AppendLine("   d)	Temos pleno conhecimento das Orientações para Cadastro de Pessoa Jurídica constantes da opção “cadastro de pessoa jurídica” do site da FUSP.");
            txt3.AppendLine();            
            txt3.AppendLine("Declaramos, ainda, ser de nossa obrigação:");
            txt3.AppendLine();  
            txt3.AppendLine("   a)	Comunicar a FUSP de quaisquer alterações subsequentes que possam ocorrer;");
            txt3.AppendLine("   b)	Fornecer quaisquer informações ou documentos adicionais que nos forem exigidos;");
            text3 = txt3.ToString();

            var txt4 = new StringBuilder();
            txt4.AppendFormat("Ref.: {0} do Cadastro de Pessoa Jurídica da FUSP", alteracao ? "Atualização" : "Inscrição");
            textRef = txt4.ToString();
        }

        public IEnumerable<RelatorioFornecedor> GerarRelatorio(int id_fornecedor)
        {
            var lst = new List<RelatorioFornecedor>();
            forBLL.Get(id_fornecedor);
            lst.Add(new RelatorioFornecedor(forBLL.ObjEF));
            return lst;
        }

        public IEnumerable<RelatorioDocumento> GetDocumentos(int id_fornecedor)
        {
            var lst = new List<RelatorioDocumento>();
            forBLL.Get(id_fornecedor);
            foreach (var item in forBLL.ObjEF.Documentos.Where(it =>  it.StatusDocFornecedor.nome != "cancelado" ).ToList())
            {
                lst.Add(new RelatorioDocumento(item));
                
            }
            return lst;
        }

        public IEnumerable<RelatorioSocio> GetSocios(int id_fornecedor)
        {
            var lst = new List<RelatorioSocio>();
            forBLL.Get(id_fornecedor);
            foreach (var item in forBLL.ObjEF.Socios)
            {
                lst.Add(new RelatorioSocio(item));
            }
            return lst;
        }

        public IEnumerable<RelatorioRepresentanteLegal> GetRepresentanteLegal(int id_fornecedor)
        {
            var lst = new List<RelatorioRepresentanteLegal>();
            forBLL.Get(id_fornecedor);
            foreach (var item in forBLL.ObjEF.RepresentantesLegais)
            {
                lst.Add(new RelatorioRepresentanteLegal(item));
            }
            return lst;
        }

        public IEnumerable<RelatorioDiretor> GetDiretor(int id_fornecedor)
        {
            var lst = new List<RelatorioDiretor>();
            forBLL.Get(id_fornecedor);
            foreach (var item in forBLL.ObjEF.Diretores)
            {
                lst.Add(new RelatorioDiretor(item));
            }
            return lst;
        }

        public IEnumerable<RelatorioReferenciaBancaria> GetReferenciaBancaria(int id_fornecedor)
        {
            var lst = new List<RelatorioReferenciaBancaria>();
            forBLL.Get(id_fornecedor);
            foreach (var item in forBLL.ObjEF.ReferenciasBancarias)
            {
                lst.Add(new RelatorioReferenciaBancaria(item));
            }
            return lst;
        }
    }   
}

public class RelatorioDocumento
{
    public string nome { get; set; }

    public string numero { get; set; }

    public string validade { get; set; }


    public RelatorioDocumento(DocumentoFornecedor d)
    {
        nome = d.documentocategoria.nome;
        numero = d.numero;
        validade = d.validade.HasValue ? String.Format("{0:d}", d.validade) : String.Empty; 
    }
}

public class RelatorioSocio
{
    public string nome { get; set; }

    public string cpf { get; set; }

    public string cnpj { get; set; }

    public string tipoDocto { get; set; }

    public RelatorioSocio(Socio s)
    {
        nome = s.nome;
        cpf = s.cpf.Value;
        cnpj = s.cnpj.Value;
        tipoDocto = s.strDocumentos;
    }
}

public class RelatorioRepresentanteLegal
{
    public string nome { get; set; }

    public string cpf { get; set; }

    public RelatorioRepresentanteLegal(RepresentanteLegal rl)
    {
        nome = rl.nome;
        cpf = rl.cpf.Value;
    }
}

public class RelatorioDiretor
{
    public string nome { get; set; }

    public string cpf { get; set; }

    public RelatorioDiretor(Diretor rd)
    {
        nome = rd.nome;
        cpf = rd.cpf.Value;
    }
}

public class RelatorioReferenciaBancaria
{
    public string banco { get; set; }

    public string agencia { get; set; }

    public string contato { get; set; }

    public string telefone { get; set; }

    public RelatorioReferenciaBancaria(ReferenciaBancaria rf)
    {
        banco = rf.banco;
        agencia = rf.agencia;
        contato = rf.contato;
        telefone = rf.telefone.value;
    }
}
