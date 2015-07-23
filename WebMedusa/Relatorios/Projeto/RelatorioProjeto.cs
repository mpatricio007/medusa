using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using Medusa.BLL;
using Medusa.DAL;
using System.Text;
using Medusa.LIB;

namespace Medusa.Relatorios.Projeto
{
    public class RelatorioProjeto
    {
        public DateTime? dataCadastro { get; set; }

        public string projA { get; set; }

        public string projDefinitivo { get; set; }

        public string valor { get; set; }

        public DateTime? dataProjA { get; set; }

        public DateTime? dataProjDefinitivo { get; set; }

        public string titulo { get; set; }

        public string objetivo { get; set; }

        public string numeroContrato { get; set; }

        public string dataInicio { get; set; }

        public string dataTermino { get; set; }

        public string siglaProj { get; set; }

        public string tags { get; set; }

        public DateTime? dataRecebimento { get; set; }

        public string valorRecebimento { get; set; }

        public string projOrigem { get; set; }

        public string nomePrograma { get; set; }

        public string pendencias { get; set; }

        public string nomeNatureza { get; set; }

        public string nomeUnidade { get; set; }

        public string nomeDepartamento { get; set; }

        public string nomeLaboratorio { get; set; }

        public string nomeAtuacao { get; set; }

        public string nomeInstContratual { get; set; }

        public string moeda { get; set; }

        public string nomeClassificacao { get; set; }

        public string telefone { get; set; }

        public string email { get; set; }

        public string solicitante { get; set; }

        public string setorResp { get; set; }

        public string status { get; set; }


        private ProjetoBLL projBLL = new ProjetoBLL();
        public RelatorioProjeto()
        {
        }

        public RelatorioProjeto(Medusa.DAL.Projeto p)
        {
            projA = p.codigoa.ToString();
            projDefinitivo = p.codigo.ToString();
            valor = p.valor.ToString();
            dataProjA = p.data_cod_a;
            dataProjDefinitivo = p.data_proj_def;
            titulo = p.titulo;
            objetivo = p.objetivo;
            numeroContrato = p.num_contrato;
            dataInicio = Util.DateToString(p.data_inicio);
            dataTermino = Util.DateToString(p.data_termino);
            siglaProj = p.sigla;
            status = "Relatório de Cadastro de Projeto ";
            if (p.id_ultimo_status == StatusProjetoBLL.Preliminar)
                status = status + p.StatusProjeto.nome;
            dataRecebimento = p.data_1_recebto;
            valorRecebimento = p.valor_1_recebto.ToString();
            nomePrograma = p.nome_programa;
            if (!String.IsNullOrEmpty(p.pendencias))
                pendencias = p.pendencias.Replace("<br/>", "");
            nomeNatureza = p.id_nat_projeto.HasValue ? p.NaturezaProjeto.nome : String.Empty;
            nomeUnidade = p.id_unidade.HasValue ? p.Unidade.nome : String.Empty;
            nomeDepartamento = p.id_departamento.HasValue ? p.Departamento.nome : String.Empty;
            nomeLaboratorio = p.id_laboratorio.HasValue ? p.Laboratorio.nome : String.Empty;
            nomeAtuacao = p.id_atuacao.HasValue ? p.Atuacao.nome : String.Empty;
            nomeInstContratual = p.id_instrumento_contratual.HasValue ? p.InstrumentoContratual.nome : String.Empty;
            moeda = p.id_moeda.HasValue ? p.Moeda.sigla : String.Empty;
            nomeClassificacao = p.id_classificacao.HasValue ? p.Classificacao.nome : String.Empty;
            if (p.ProjetoSolicitacao != null)
                solicitante = p.ProjetoSolicitacao.Usuario.PessoaFisica.nome;

            var s = new StringBuilder();
            foreach (var item in p.SetorResponsavel)
            {
                s.AppendLine(String.Format("- {0}", item.Setor.nome));
            }
            setorResp = s.ToString();
        }

        public IEnumerable<RelatorioProjeto> GerarRelatorio(int id_projeto)
        {
            var lst = new List<RelatorioProjeto>();
            projBLL.Get(id_projeto);
            lst.Add(new RelatorioProjeto(projBLL.ObjEF));
            return lst;
        }

        public IEnumerable<RelatorioCoordenadores> GetCoordenadores(int id_projeto)
        {
            var lst = new List<RelatorioCoordenadores>();
            projBLL.Get(id_projeto);
            foreach (var item in projBLL.ObjEF.Coordenadores)
            {
                lst.Add(new RelatorioCoordenadores(item));
            }
            return lst;
        }

        public IEnumerable<RelatorioFinanciador> GetFinanciadores(int id_projeto)
        {
            var lst = new List<RelatorioFinanciador>();
            projBLL.Get(id_projeto);
            foreach (var item in projBLL.ObjEF.Financiadores)
            {
                lst.Add(new RelatorioFinanciador(item));
            }
            return lst;
        }

        public IEnumerable<RelatorioEndereco> GetEnderecos(int id_projeto)
        {
            var lst = new List<RelatorioEndereco>();
            projBLL.Get(id_projeto);
            foreach (var item in projBLL.ObjEF.Enderecos)
            {
                lst.Add(new RelatorioEndereco(item));
            }
            return lst;
        }

        public IEnumerable<RelatorioDocumentoProjeto> GetDoctos(int id_projeto)
        {
            var lst = new List<RelatorioDocumentoProjeto>();
            projBLL.Get(id_projeto);
            foreach (var item in projBLL.ObjEF.Documentos)
            {
                lst.Add(new RelatorioDocumentoProjeto(item));
            }
            return lst;
        }

        public IEnumerable<RelatorioTaxaProjeto> GetTaxas(int id_projeto)
        {
            var lst = new List<RelatorioTaxaProjeto>();
            projBLL.Get(id_projeto);
            foreach (var item in projBLL.ObjEF.Taxas)
            {
                lst.Add(new RelatorioTaxaProjeto(item));
            }
            return lst;
        }
    }
}

public class RelatorioCoordenadores
{
    public string nomeCoordenador { get; set; }

    public string inicioCoord { get; set; }

    public string terminoCoord { get; set; }

    public string tipoCoord { get; set; }

    public string telefones { get; set; }

    public string emails { get; set; }

    public RelatorioCoordenadores()
    {
    }

    public RelatorioCoordenadores(ProjetoCoordenadores pc)
    {
        nomeCoordenador = pc.Coordenador.PessoaFisica.nome;
        inicioCoord = Util.DateToString(pc.inicio);
        terminoCoord = Util.DateToString(pc.termino);
        tipoCoord = Convert.ToString(pc.tipo);

        var s = new StringBuilder();
        foreach (var item in pc.Coordenador.PessoaFisica.Telefones)
        {
            s.AppendFormat(String.Format("{0}, ", item.telefone.value));
        }
        telefones = s.ToString();

        var se = new StringBuilder();
        foreach (var item in pc.Coordenador.PessoaFisica.Emails)
        {
            se.AppendFormat(String.Format("{0}, ", item.email.value));
        }
        emails = se.ToString();
    }
}

public class RelatorioFinanciador
{
    public string financiador { get; set; }

    public string naturezaPJ { get; set; }

    public RelatorioFinanciador()
    {
    }

    public RelatorioFinanciador(ProjetoFinanciador pf)
    {
        financiador = pf.Financiador.nome;
        naturezaPJ = pf.Financiador.Natureza.nome;
    }
}

public class RelatorioEndereco
{
    public string endereco { get; set; }

    public RelatorioEndereco()
    {
    }

    public RelatorioEndereco(EnderecoProjeto ep)
    {
        endereco = ep.endereco.ToString();
    }
}

public class RelatorioDocumentoProjeto
{
    public string nomeDocumento { get; set; }

    public DateTime? dataDocumento { get; set; }

    public string obsDocumento { get; set; }

    public RelatorioDocumentoProjeto()
    {
    }

    public RelatorioDocumentoProjeto(ProjetoDocumento pd)
    {
        nomeDocumento = pd.Documento.nome;
        dataDocumento = pd.data;
        obsDocumento = pd.observacao;
    }
}

public class RelatorioTaxaProjeto
{
    public string nomeTaxa { get; set; }

    public string numeroTaxa { get; set; }

    public RelatorioTaxaProjeto()
    {
    }

    public RelatorioTaxaProjeto(ProjetoTaxa pt)
    {
        nomeTaxa = pt.TaxaProjeto.nome;
        numeroTaxa = Util.DecimalToString(pt.TaxaProjeto.taxa);
    }
}
