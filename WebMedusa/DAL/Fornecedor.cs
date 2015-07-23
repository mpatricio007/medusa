using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Medusa.DAL
{
    public class Fornecedor
    {
        [Search("código", "id_fornecedor")]
        [Key]
        public int id_fornecedor { get; set; }

        [Invisible]
        public int id_usuario { get; set; }

        [Invisible]
        [ForeignKey("id_usuario")]
        public virtual Usuario Usuario { get; set; }

        [MaxLength(100)]
        public string nome { get; set; }

        [Search("CNPJ", "cnpj.Value")]
        public CNPJ cnpj { get; set; }

        [Invisible]
        public Endereco ender { get; set; }

        [Invisible]
        public int? id_categoria { get; set; }

        [Search("categoria", "Categoria.nome")]
        [ForeignKey("id_categoria")]
        public virtual Categoria Categoria { get; set; }

        [Invisible]
        public int? id_ultimo_status { get; set; }

        [Search("status", "StatusFornecedor.nome")]
        [ForeignKey("id_ultimo_status")]
        public virtual StatusFornecedor StatusFornecedor { get; set; }

        private ICollection<HistoricoFornecedor> historicos;

        [Invisible]
        public virtual ICollection<HistoricoFornecedor> Historicos
        {
            get
            {
                if (historicos == null)
                    historicos = new List<HistoricoFornecedor>();
                return historicos;
            }
            set { historicos = value; }
        }

        [Invisible]
        public int? id_ramo_atividade { get; set; }

        [Search("ramo de atividade", "RamoAtividade.nome")]
        [ForeignKey("id_ramo_atividade")]
        public virtual RamoAtividade RamoAtividade { get; set; }

        [Search("nome fantasia", "nome_fantasia")]
        public string nome_fantasia { get; set; }

        [Search("grupo economico", "grupo_economico")]
        public string grupo_economico { get; set; }

        [Search("incrição estadual", "inscr_estadual")]
        public string inscr_estadual { get; set; }

        [Search("inscrição municipal", "inscr_municipal")]
        public string inscr_municipal { get; set; }

        [Search("home page", "home_page")]
        public string home_page { get; set; }

        public DateTime? validade { get; set; }

        private Telefone tel;

        [Search("telefone", "telefone.valueToString")]
        public Telefone telefone
        {
            get
            {
                if (tel == null)
                    tel = new Telefone();
                return tel;
            }
            set { tel = value; }
        }

        private Email em;

        [Search("email", "email.value")]
        public Email email
        {
            get
            {
                if (em == null)
                    em = new Email();
                return em;
            }
            set { em = value; }
        }

        [Search("registro número", "registro_numero")]
        public string registro_numero { get; set; }

        [Search("data de alteração", "data_alteracao")]
        public DateTime? data_alteracao { get; set; }

        [Invisible]
        public string numero_alteracao { get; set; }

        [Search("capital social", "capital_social")]
        public Decimal? capital_social { get; set; }

        [Search("ano patrimonial", "ano_patrimonial")]
        public int? ano_patrimonial { get; set; }

        private ICollection<RepresentanteLegal> representanteLegal;
        [Search("representantes legais", "RepresentantesLegais.nome")]
        public virtual ICollection<RepresentanteLegal> RepresentantesLegais
        {
            get
            {
                if (representanteLegal == null)
                    representanteLegal = new List<RepresentanteLegal>();
                return representanteLegal;
            }

            set
            {
                representanteLegal = value;
            }
        }

        private ICollection<Socio> socio;
        [Search("sócios", "Socios.nome")]
        public virtual ICollection<Socio> Socios
        {
            get
            {
                if (socio == null)
                    socio = new List<Socio>();
                return socio;
            }

            set
            {
                socio = value;
            }
        }

        private ICollection<Diretor> diretor;
        [Search("diretores", "Diretores.nome")]
        public virtual ICollection<Diretor> Diretores
        {
            get
            {
                if (diretor == null)
                    diretor = new List<Diretor>();
                return diretor;
            }

            set
            {
                diretor = value;
            }
        }

        private ICollection<ReferenciaBancaria> refBanc;
        [Search("referências bancárias", "ReferenciasBancarias.contato")]
        public virtual ICollection<ReferenciaBancaria> ReferenciasBancarias
        {
            get
            {
                if (refBanc == null)
                    refBanc = new List<ReferenciaBancaria>();
                return refBanc;
            }

            set
            {
                refBanc = value;
            }
        }

        private ICollection<DocumentoFornecedor> documentos;
        [Search("documentos", "Documentos.nome")]
        public virtual ICollection<DocumentoFornecedor> Documentos
        {
            get
            {
                if (documentos == null)
                    documentos = new List<DocumentoFornecedor>();
                return documentos;
            }

            set
            {
                documentos = value;
            }
        }

        [NotMapped]
        public string StrSocios 
        {
            get 
            {
                StringBuilder s = new StringBuilder();
                foreach (var item in Socios)
                {
                    s.AppendFormat(" {0}", item.nome);
                }
                return s.ToString();
            }
            set { } 
        }
    }


    public class FornecedorConfiguration : EntityTypeConfiguration<Fornecedor>
    {
        public FornecedorConfiguration()
        {
            Property(p => p.cnpj.Value).HasColumnName("cnpj").IsRequired();            
            ToTable("Fornecedor");
        }
    }
}




