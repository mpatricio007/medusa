using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public enum TipoCadastro
    {
        projMae = 1,
        subProj = 2,
        projeto = 3
    }

    public class Projeto
    {
        [Key]
        public int id_projeto { get; set; }


        public int? cod_a_projeto { get; set; }

        public string sub_projeto_a { get; set; }

        public DateTime? data_cod_a { get; set; }

        public int? id_nat_projeto { get; set; }

        [ForeignKey("id_nat_projeto")]
        public virtual NaturezaProjeto NaturezaProjeto { get; set; }

	    public int? cod_def_projeto { get; set; }

        public int? id_unidade { get; set; }        
        
        [ForeignKey("id_unidade")]
        public virtual Unidade Unidade { get; set; }

        public int? id_departamento { get; set; }

        [ForeignKey("id_departamento")]
        public virtual Departamento Departamento { get; set; }

        public int? id_laboratorio { get; set; }

        [ForeignKey("id_laboratorio")]
        public virtual Laboratorio Laboratorio { get; set; }

        public int? id_atuacao { get; set; }
        [ForeignKey("id_atuacao")]
        public virtual Atuacao Atuacao { get; set; }

        public int? id_instrumento_contratual { get; set; }
        [ForeignKey("id_instrumento_contratual")]
        public virtual InstrumentoContratual InstrumentoContratual { get; set; }

        public int? id_classificacao_fusp { get; set; }
        [ForeignKey("id_classificacao_fusp")]
        public virtual ClassificacaoFusp ClassificacaoFusp { get; set; }
        
        public string num_contrato { get; set; }

        public DateTime? data_inicio { get; set; }

        public DateTime? data_termino { get; set; }

        public int? id_moeda { get; set; }
        [ForeignKey("id_moeda")]
        public virtual Moeda Moeda { get; set; }

        public Decimal? valor { get; set; }

        public string sigla { get; set; }

        public string titulo { get; set; }

        public string objetivo { get; set; }

        public int? id_classificacao { get; set; }
        [ForeignKey("id_classificacao")]
        public virtual Classificacao Classificacao { get; set; }

        public DateTime? data_1_recebto { get; set; }

        public Decimal? valor_1_recebto { get; set; }
        
        public DateTime? data_proj_def { get; set; }
       
        public string nome_programa { get; set; }        

        public string pendencias { get; set; }

        public string sub_projeto { get; set; }

        [NotMapped]
        public int? id_sol_pr { get; set; }

        //[ForeignKey("id_sol_proj")]
        public virtual ProjetoSolicitacao ProjetoSolicitacao { get; set; }

        public int? id_ultimo_status { get; set; }

        [ForeignKey("id_ultimo_status")]
        public virtual StatusProjeto StatusProjeto { get; set; }

        public virtual ICollection<HistoricoProjeto> HistoricoProjetos { get; set; }

        private ICollection<ProjetoCoordenadores> coordenadores;

        [Search("coordenadores", "Coordenadores.Coordenador.PessoaFisica.nome")]
        public virtual ICollection<ProjetoCoordenadores> Coordenadores
        {
            get
            {
                if (coordenadores == null)
                    coordenadores = new List<ProjetoCoordenadores>();
                return coordenadores;
            }
            set { coordenadores = value; }
        }

        private ICollection<ProjetoFinanciador> financiadores;
        [Search("financiadores", "Financiadores.Financiador.nome")]
        public virtual ICollection<ProjetoFinanciador> Financiadores
        {
            get
            {
                if (financiadores == null)
                    financiadores = new List<ProjetoFinanciador>();
                return financiadores;
            }
            set { financiadores = value; }
        }

        private ICollection<EnderecoProjeto> enderecos;
        public virtual ICollection<EnderecoProjeto> Enderecos
        {
            get
            {
                if (enderecos == null)
                    enderecos = new List<EnderecoProjeto>();
                return enderecos;
            }
            set { enderecos = value; }
        }

        private ICollection<ContatoProjeto> contatos;
        public virtual ICollection<ContatoProjeto> Contatos
        {
            get
            {
                if(contatos == null)
                    contatos = new List<ContatoProjeto>();
                return contatos;
            }
            set { contatos = value; }
        }

        private ICollection<ProjetoDocumento> documentos;
        public virtual ICollection<ProjetoDocumento> Documentos
        {
            get
            {
                if (documentos == null)
                    documentos = new List<ProjetoDocumento>();
                return documentos;
            }
            set { documentos = value; }
        }

        private ICollection<ProjetoTaxa> taxas;
        public virtual ICollection<ProjetoTaxa> Taxas 
        {
            get
            {
                if (taxas == null)
                    taxas = new List<ProjetoTaxa>();
                return taxas;
            }
            set { taxas = value; }
        }

        private ICollection<SetorResponsavel> setorResponsavel;
        public virtual ICollection<SetorResponsavel> SetorResponsavel
        {
            get
            {
                if (setorResponsavel == null)
                    setorResponsavel = new List<SetorResponsavel>();
                return setorResponsavel;
            }
            set { setorResponsavel = value; }
        }

        private ICollection<Assin> assin;
        public virtual ICollection<Assin> Assin
        {
            get
            {
                if (assin == null)
                    assin = new List<Assin>();
                return assin;
            }
            set { assin = value; }
        }

        private ICollection<RequisitoEncerramento> requisitoEncerramento;
        public virtual ICollection<RequisitoEncerramento> RequisitoEncerramento
        {
            get
            {
                if (requisitoEncerramento == null)
                    requisitoEncerramento = new List<RequisitoEncerramento>();
                return requisitoEncerramento;
            }
            set { requisitoEncerramento = value; }
        }

        public int? codigo { get; set; }

        public int? codigoa { get; set; }

        public virtual ICollection<Conta> Contas { get; set; }

        public virtual ICollection<ReciboCurso> ReciboCursos { get; set; }

        [NotMapped]
        public string HtmlPaginaProjeto
        {
            get
            {
                return String.Format(@"<a href='../scp/projetosdefinitivos.aspx?pk={0}'>{1}</a>",
                   id_projeto.ToString().Criptografar(), codigo);
            }
        }

        [NotMapped]
        public string HtmlPaginaProjetoA
        {
            get
            {
                return String.Format(@"<a href='../scp/projetosdefinitivos.aspx?pk={0}'>{1}</a>",
                   id_projeto.ToString().Criptografar(), codigoa);
            }
        }

        private ICollection<ArquivoAnexoProjeto> arquivoAnexoProjetos;
        public virtual ICollection<ArquivoAnexoProjeto> ArquivoAnexoProjetos
        {
            get
            {
                if (arquivoAnexoProjetos == null)
                    arquivoAnexoProjetos = new List<ArquivoAnexoProjeto>();
                return arquivoAnexoProjetos;
            }

            set
            {
                arquivoAnexoProjetos = value;
            }
        }
    }

    public class ProjetoConfiguration : EntityTypeConfiguration<Projeto>
    {
        public ProjetoConfiguration()
        {
            HasOptional<ProjetoSolicitacao>(u => u.ProjetoSolicitacao)
            .WithOptionalDependent(c => c.Projeto).Map(p => p.MapKey("id_sol_proj"));
            Property(it => it.codigo).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(it => it.codigoa).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            ToTable("Projeto");
        }
    }

    public class Class1
    {
    }
}



