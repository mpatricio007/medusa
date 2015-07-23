using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
//using System.Data.Objects.DataClasses;
//using System.Data.Objects;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Text;
using System.Data;
using System.Collections;

namespace Medusa.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Arquivo> Arquivos { get; set; }

        public DbSet<vContratosPessoa> vContratosPessoas { get; set; }

        public DbSet<px> pxs { get; set; }


        #region Admin
        public DbSet<Contato_fake> Contato_fakes { get; set; }
        public DbSet<LogSistema> LogSistemas { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuPagina> MenuPaginas { get; set; }
        public DbSet<Pagina> Paginas { get; set; }
        public DbSet<Sistema> Sistemas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UsuarioSistema> UsuarioSistemas { get; set; }
        public DbSet<Recesso> Recessos { get; set; }
        #endregion admin

        #region Arquivo
        public DbSet<TipoVolume> TipoVolumes { get; set; }
        public DbSet<LocalizacaoVolume> LocalizacaoVolumes { get; set; }
        public DbSet<Volume> Volumes { get; set; }
        public DbSet<EmprestimoVolume> EmprestimoVolumes { get; set; }
        #endregion

        #region Almoxarifado
        public DbSet<UnidadeMedida> UnidadeMedidas { get; set; }
        public DbSet<MaterialConsumo> MaterialConsumos { get; set; }
        public DbSet<Requisicao> Requisicoes { get; set; }
        public DbSet<RequisicaoMaterial> RequisicaoMateriais { get; set; }
        public DbSet<StatusRequisicaoMaterial> StatusRequisicaoMateriais { get; set; }
        public DbSet<HistoricoRequisicao> HistoricoRequisicoes { get; set; }
        public DbSet<TipoMaterial> TipoMateriais { get; set; }
        public DbSet<StatusRequisicao> StatusRequisicoes { get; set; }
        public DbSet<NfMaterial> NfMateriais { get; set; }
        public DbSet<MaterialNotaFiscal> MaterialNotaFiscais { get; set; }
        #endregion

        #region Bolsa
        public DbSet<Bolsista> Bolsistas { get; set; }
        public DbSet<Bolsa> Bolsas { get; set; }
        public DbSet<BolsaVigencia> BolsaVigencias { get; set; }
        #endregion

        #region Cobrança
        public DbSet<Sacado> Sacados { get; set; }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<EventoSacado> EventoSacados { get; set; }
        public DbSet<BoletoCobranca> BoletosCobrancas { get; set; }
        #endregion

        #region Comum
        public DbSet<Banco> Bancos { get; set; }
        public DbSet<BancoAgencia> BancoAgencias { get; set; }
        public DbSet<Conta> Contas { get; set; }
        public DbSet<Setor> Setores { get; set; }
        public DbSet<Moeda> Moedas { get; set; }
        public DbSet<FormaPagto> FormaPagtos { get; set; }
        public DbSet<Cep> Ceps { get; set; }
        public DbSet<TipoContrato> TipoContratos { get; set; }
        #endregion

        #region Conciliação
        public DbSet<TipoImpArquivo> TipoImpArquivos { get; set; }
        public DbSet<TipoLcto> TipoLctos { get; set; }
        public DbSet<ContaTransf> ContaTransferencias { get; set; }
        public DbSet<ContaTipo> ContaTipos { get; set; }
        public DbSet<ContaAplicacao> ContaAplicacoes { get; set; }
        public DbSet<ContaLancto> ContaLanctos { get; set; }
        public DbSet<UsuarioTipoLcto> UsuarioTipoLctos { get; set; }
        public DbSet<ContaSaldoDiferenca> ContaSaldoDiferencas { get; set; }
        public DbSet<IdentificadorDeposito> IdentificadorDepositos { get; set; }
        public DbSet<ImportaArquivo> ImportaArquivos { get; set; }
        #endregion

        #region Correspondencia
        public DbSet<Carta> Cartas { get; set; }
        public DbSet<Circular> Circulares { get; set; }
        public DbSet<Cobranca> Cobranças { get; set; }
        public DbSet<CircularInterna> CircularesInternas { get; set; }
        public DbSet<CartaBancaria> CartasBancarias { get; set; }


        #endregion

        #region Cursos
        public DbSet<Curso> Cursos { get; set; }
        #endregion

        #region Folha
        public DbSet<Autonomo> Autonomos { get; set; }
        public DbSet<ContratoAutonomo> ContratoAutonomos { get; set; }
        #endregion

        #region PAC
        public DbSet<PACDestino> PACDestinos { get; set; }
        public DbSet<PacStatus> PacStatus { get; set; }
        public DbSet<TipoAquisicao> TipoAquisicoes { get; set; }
        public DbSet<ModalidadeCompra> ModalidadeCompras { get; set; }
        #endregion

        #region PessoaJuridica
        public DbSet<CategoriaDocumento> CategoriaDocumentos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<RamoAtividade> RamoAtividades { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<StatusFornecedor> StatusFornecedores { get; set; }
        public DbSet<HistoricoFornecedor> HistoricoFornecedores { get; set; }
        public DbSet<DocumentoFornecedor> DocumentoFornecedores { get; set; }
        public DbSet<RepresentanteLegal> RepresentanteLegal { get; set; }
        public DbSet<Socio> Socios { get; set; }
        public DbSet<Diretor> Diretores { get; set; }
        public DbSet<ReferenciaBancaria> ReferenciasBancarias { get; set; }
        public DbSet<StatusDocFornecedor> StatusDocsFornecedores { get; set; }
        #endregion

        #region Remessa Bancaria
        public DbSet<TipoLote> TipoLote { get; set; }
        public DbSet<LotePagBB> LotePagBBs { get; set; }
        public DbSet<LoteBoleto> LoteBoletos { get; set; }
        public DbSet<LoteCons> LoteContas { get; set; }
        public DbSet<LoteGPS> LoteGPSs { get; set; }
        public DbSet<LoteGRU> LoteGRUs { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<FinalidadePagto> FinalidadePagtos { get; set; }
        public DbSet<TipoRetorno> TipoRetornos { get; set; }
        public DbSet<TipoPagto> TipoPagtos { get; set; }
        public DbSet<TipoConciliacao> TiposConciliacoes { get; set; }
        public DbSet<ArquivosRetorno> ArquivosRetornos { get; set; }
        public DbSet<TipoArquivo> TiposArquivos { get; set; }
        public DbSet<TipoFolhaPagto> TiposFolhasPagtos { get; set; }
        public DbSet<Remessa> Remessas { get; set; }
        public DbSet<RemessaTit> RemessaTits { get; set; }
        public DbSet<RemessaPAG> RemessaPAGs { get; set; }
        public DbSet<RemessaCons> RemessaCons { get; set; }
        public DbSet<RemessaGpsSemCodBarra> RemessaGpsSemCodBarra { get; set; }
        public DbSet<RemessaGru> RemessaGRUs { get; set; }

        #endregion

        #region SCP
        public DbSet<Atuacao> Atuacoes { get; set; }
        public DbSet<Classificacao> Classificacoes { get; set; }
        public DbSet<InstrumentoContratual> InstrumentoContratuais { get; set; }
        public DbSet<Natureza> Naturezas { get; set; }
        public DbSet<Documento> Documentos { get; set; }
        public DbSet<NaturezaProjeto> NaturezaProjetos { get; set; }
        public DbSet<Contato> Contatos { get; set; }
        public DbSet<Coordenador> Coordenadores { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<TaxaProjeto> TaxasProjetos { get; set; }
        public DbSet<Financiador> Financiadores { get; set; }
        public DbSet<Laboratorio> Laboratorios { get; set; }
        public DbSet<StatusSolicitacao> StatusSolicitacoes { get; set; }
        public DbSet<ProjetoSolicitacao> ProjetoSolicitacoes { get; set; }
        public DbSet<OrigemProjeto> OrigemProjetos { get; set; }
        public DbSet<Projeto> Projetos { get; set; }
        public DbSet<Unidade> Unidades { get; set; }
        public DbSet<ProjetoA> ProjetosAs { get; set; }
        public DbSet<HistoricoPx> HistoricoPxs { get; set; }
        //public DbSet<TipoSolicitacao> TipoSolicitacoes { get; set; }
        public DbSet<ProjetoCoordenadores> ProjetoCoordenadores { get; set; }
        public DbSet<ProjetoFinanciador> ProjetoFinanciadores { get; set; }
        //public DbSet<ProjetoEndereco> ProjetoEnderecos { get; set; }
        public DbSet<EnderecoProjeto> EnderecoProjetos { get; set; }
        public DbSet<TipoEndereco> TipoEndereco { get; set; }
        public DbSet<ProjetoDocumento> ProjetoDocumento { get; set; }
        public DbSet<PlanoConta> PlanoConta { get; set; }
        public DbSet<ProjetoTaxa> ProjetoTaxa { get; set; }
        public DbSet<StatusProjeto> StatusProjeto { get; set; }
        public DbSet<HistoricoProjeto> HistoricoProjeto { get; set; }
        public DbSet<ContatoProjeto> ContatoProjeto { get; set; }
        public DbSet<SetorResponsavel> SetorResponsavel { get; set; }
        public DbSet<Assin> Assin { get; set; }
        public DbSet<ClassificacaoFusp> ClassificacoesFusp { get; set; }
        public DbSet<OpcaoAdiantamento> OpcoesAdiantamento { get; set; }
        public DbSet<RequisitoEncerramento> RequisitoEncerramentos { get; set; }
        public DbSet<ArquivoAnexoProjeto> ArquivoAnexoProjetos { get; set; }
        public DbSet<SolicitacaoDeProjeto> SolicitacaoDeProjetos { get; set; }
        public DbSet<Notificacao> Notificacoes { get; set; }
        public DbSet<ContasProjeto> ContasProjetos { get; set; }
        #endregion

        #region SISPS
        public DbSet<ClassificacaoVaga> ClassificacaoVagas { get; set; }
        public DbSet<Edital> Editais { get; set; }
        public DbSet<Inscricao> Inscricoes { get; set; }
        public DbSet<Vaga> Vagas { get; set; }
        #endregion

        #region SREC
        public DbSet<Saida> Saidas { get; set; }
        public DbSet<Entrada> Entradas { get; set; }
        public DbSet<StatusEntrada> StatusEntradas { get; set; }
        public DbSet<HistoricoEntrada> HistoricoEntradas { get; set; }
        public DbSet<Providencia> Providencias { get; set; }
        public DbSet<PossivelProvidencia> PossiveisProvidencias { get; set; }
        public DbSet<SetorCompetente> SetoresCompetentes { get; set; }
        public DbSet<DestinatarioEntrada> DestinatariosEntradas { get; set; }
        #endregion

        #region Views Sisch
        public DbSet<CarregarPagto> CarregarPagtos { get; set; }
        public DbSet<CarregarBoletos> CarregarBoletos { get; set; }
        public DbSet<TarefaProvidencia> TarefaProvidencias { get; set; }
        public DbSet<TarefaDestinatario> TarefaDestinatarios { get; set; }
        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<CorrespondenciaEmail> CorrespondenciaEmails { get; set; }
        public DbSet<OrigemDestinatariosEmail> OrigemDestinatariosEmails { get; set; }
        public DbSet<DestinatarioEmail> DestinatarioEmails { get; set; }
        #endregion

        #region Comodato
        public DbSet<StatusComodato> StatusComodatos { get; set; }
        public DbSet<Patrimonio> Patrimonios { get; set; }
        public DbSet<HistoricoComodato> HistoricoComodatos { get; set; }
        public DbSet<Comodato> Comodatos { get; set; }
        #endregion

        #region Adiantamentos

        public DbSet<Beneficiario> Beneficiarios { get; set; }
        public DbSet<Adiantamento> Adiantamentos { get; set; }
        public DbSet<AdiantamentoViagem> AdiantamentosViagens { get; set; }
        public DbSet<TiposAdiantamento> TiposAdiantamentos { get; set; }
        public DbSet<StatusAdiantamento> StatusAdiantamentos { get; set; }
        public DbSet<HistoricoAdiantamento> HistoricoAdiantamentos { get; set; }
        public DbSet<EmailPadrao> EmailPadroes { get; set; }
        public DbSet<EmailCopia> EmailCopias { get; set; }
        public DbSet<HistoricoEmailAdmto> HistoricoEmailAdmtos { get; set; }

        #endregion

        #region demonstrativo
        public DbSet<vUsuariosDemonstrativo> vUsuariosDemonstrativos { get; set; }
        public DbSet<vUsuariosProjetosDemonstrativo> vUsuariosProjetosDemonstrativos { get; set; }
        #endregion

        #region recibos
        public DbSet<Recibo> Recibos { get; set; }
        public DbSet<ReciboCheque> ReciboCheques { get; set; }
        public DbSet<ReciboCurso> ReciboCursos { set; get; }
        public DbSet<ReciboFusp> RecibosFusp { set; get; }
        #endregion

        #region scfp
        //public DbSet<BaseCalculo> BaseCalculos { get; set; }
        //public DbSet<BaseCalculoDedutivel> BaseCalculoDedutiveis { get; set; }
        public DbSet<Taxa> Taxas { get; set; }
        public DbSet<TabelaTaxas> TabelaTaxas { get; set; }
        public DbSet<FaixaTaxas> FaixaTaxas { get; set; }
        public DbSet<Despesa> Despesas { get; set; }
        public DbSet<DespesaAutonomo> DespesasAutonomos { get; set; }
        public DbSet<Lancamento> Lancamentos { get; set; }
        public DbSet<LancamentoItem> LancamentosItens { get; set; }
        public DbSet<LancamentoTipo> LancamentoTipos { get; set; }
        public DbSet<Importacao> Importacoes { get; set; }
        public DbSet<CarregarFolha> carregarFolha { get; set; }
        public DbSet<ProcessamentoDespesa> ProcessamentosDespesas { get; set; }
        public DbSet<DespesaConsumo> DespesaConsumos { get; set; }
        public DbSet<DespesaBoleto> DespesaBoletos { get; set; }
        public DbSet<IdentificacaoSegmento> IdenticacaoSegmentos { get; set; }
        public DbSet<IdentficacaoSegPlanConta> IdentficacaoSegPlanContas { get; set; }
        public DbSet<DespesaImpostoConsumo> DespesaImpostosConsumos { get; set; }
        public DbSet<DespesaGRF> DespesaGRFs { get; set; }
        public DbSet<DespesaGPS> DespesaGPSs { get; set; }
        public DbSet<DespesaDARF> DespesaDARFs { get; set; }
        public DbSet<DespesaGRFR> DespesaGRFRs { get; set; }
        public DbSet<DespesaDAMSP> DespesaDAMSPs { get; set; }
        public DbSet<DespesaProjeto> DespesasProjetos { get; set; }
        public DbSet<TransferenciaProjeto> TransferenciaProjetos { get; set; }
        public DbSet<LancamentosSCPR> LancamentosSCPR { get; set; }
    
        #endregion


        #region EditaisLic
        public DbSet<EditalLic> EditaisLics { get; set; }
        public DbSet<EditalLicAnexo> EditaisLicAnexos { get; set; }
        public DbSet<StatusEditaisLic> StatusEditaisLics { get; set; }
        public DbSet<InscricaoPregao> InscricaoPregoes { get; set; }
        public DbSet<LogEdital> LogEditais { get; set; }
        #endregion


        public DbSet<old_empresa> old_empresa { get; set; }
        
        public DbSet<CarregarImpostoConsumo> CarregarImpostoConsumo { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<PessoaEmail> PessoaEmails { get; set; }
        public DbSet<PessoaEndereco> PessoaEnderecos { get; set; }
        public DbSet<PessoaTelefone> PessoaTelefones { get; set; }
        public DbSet<Correspondencia> TipoCorrespondencias { get; set; }
        public DbSet<UF> UFs { get; set; }
        public DbSet<Cidade> Cidades { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<ContaSaldoFinal> ContaSaldoFinais { get; set; }
        public DbSet<ContratoPessoaFisica> ContratoPessoaFisicas { get; set; }
        public DbSet<ContratoPagamento> ContratoPagamentos { get; set; }
        public DbSet<CarregarFolhaAntiga> CarregarFolhaAntiga { get; set; }


        public static Type GetPropertyType<T>(string propertyName)
        {
            Type tipoCampo = typeof(T);
            string[] Properties = propertyName.Split('.');
            foreach (var Property in Properties)
                tipoCampo = tipoCampo.GetProperty(Property).PropertyType;
            return tipoCampo;
        }

        public Contexto()
            : base("name=Contexto")
        {

        }


        public ConnectionState returnState()
        {
            return base.Database.Connection.State;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new Site_pag_inicialConfiguration());
            modelBuilder.Configurations.Add(new Site_bndesConfiguration());

            modelBuilder.Configurations.Add(new ArquivoConfiguration());
            modelBuilder.Configurations.Add(new ExtensaoConfiguration());

            modelBuilder.Configurations.Add(new AutonomoConfiguration());

            modelBuilder.Configurations.Add(new pxConfiguration());

            modelBuilder.Configurations.Add(new RecessoConfiguration());

            modelBuilder.Configurations.Add(new TarefaConfiguration());
            modelBuilder.Configurations.Add(new CorrespondenciaConfiguration());
            modelBuilder.Configurations.Add(new UFConfiguration());
            modelBuilder.Configurations.Add(new CidadeConfiguration());
            modelBuilder.Configurations.Add(new PaisConfiguration());
            modelBuilder.Configurations.Add(new ContaSaldoFinalConfiguration());
            modelBuilder.Configurations.Add(new RemessaConfiguration());
            modelBuilder.Configurations.Add(new RemessaConsConfiguration());
            modelBuilder.Configurations.Add(new RemessaPAGConfiguration());
            modelBuilder.Configurations.Add(new RemessaTitConfiguration());
            modelBuilder.Configurations.Add(new RemessaGpsSemCodBarraConfiguration());
            modelBuilder.Configurations.Add(new RemessaGruConfiguration());
            modelBuilder.Configurations.Add(new ContatoConfiguration());
            modelBuilder.Configurations.Add(new CoordenadorConfiguration());
            modelBuilder.Configurations.Add(new PessoaEmailConfiguration());
            modelBuilder.Configurations.Add(new old_empresaConfiguration());

            #region Almoxarifado
            modelBuilder.Configurations.Add(new UnidadeMedidaConfiguration());
            modelBuilder.Configurations.Add(new MaterialConsumoConfiguration());
            modelBuilder.Configurations.Add(new RequisicaoConfiguration());
            modelBuilder.Configurations.Add(new RequisicaoMaterialConfiguration());
            modelBuilder.Configurations.Add(new StatusRequisicaoMaterialConfiguration());
            modelBuilder.Configurations.Add(new HistoricoRequisicaoConfiguration());
            modelBuilder.Configurations.Add(new TipoMaterialConfiguration());
            modelBuilder.Configurations.Add(new StatusRequisicaoConfiguration());
            modelBuilder.Configurations.Add(new MaterialNotaFiscalConfiguration());
            modelBuilder.Configurations.Add(new NfMaterialConfiguration());
            #endregion

            #region Projeto
            modelBuilder.Configurations.Add(new ProjetoCoordenadoresConfiguration());
            modelBuilder.Configurations.Add(new ProjetoFinanciadorConfiguration());
            modelBuilder.Configurations.Add(new ProjetoConfiguration());
            modelBuilder.Configurations.Add(new ProjetoAConfiguration());
            modelBuilder.Configurations.Add(new NaturezaProjetoConfiguration());
            modelBuilder.Configurations.Add(new StatusSolicitacaoConfiguration());
            modelBuilder.Configurations.Add(new ProjetoSolicitacaoConfiguration());
            modelBuilder.Configurations.Add(new OrigemProjetoConfiguration());
            modelBuilder.Configurations.Add(new TipoSolicitacaoConfiguration());
            modelBuilder.Configurations.Add(new HistoricoPxConfiguration());
            modelBuilder.Configurations.Add(new EnderecoProjetoConfiguration());
            modelBuilder.Configurations.Add(new TipoEnderecoConfiguration());
            modelBuilder.Configurations.Add(new StatusProjetoConfiguration());
            modelBuilder.Configurations.Add(new HistoricoProjetoConfiguration());
            modelBuilder.Configurations.Add(new ContatoProjetoConfiguration());
            modelBuilder.Configurations.Add(new SetorResponsavelConfiguration());
            modelBuilder.Configurations.Add(new AssinConfiguration());
            #endregion

            #region Views Sisch
            modelBuilder.Configurations.Add(new CarregarPagtoConfiguration());
            modelBuilder.Configurations.Add(new CarregarBoletosConfiguration());
            modelBuilder.Configurations.Add(new CarregarImpostoConsumoConfiguration());
            modelBuilder.Configurations.Add(new TarefaDestinatarioConfiguration());
            modelBuilder.Configurations.Add(new TarefaProvidenciaConfiguration());
            modelBuilder.Configurations.Add(new TarefaLctoConfiguration());
            modelBuilder.Configurations.Add(new IdentificadorDepositoConfiguration());
            modelBuilder.Configurations.Add(new CorrespondenciaEmailConfiguration());
            modelBuilder.Configurations.Add(new OrigemDestinatariosEmailConfiguration());
            modelBuilder.Configurations.Add(new DestinatarioEmailConfiguration());
            #endregion

            #region Cobrança
            modelBuilder.Configurations.Add(new SacadoConfiguration());
            modelBuilder.Configurations.Add(new EventoConfiguration());
            modelBuilder.Configurations.Add(new EventoSacadoConfiguration());
            modelBuilder.Configurations.Add(new BoletoCobrancaConfiguration());
            #endregion


            #region SREC
            modelBuilder.Configurations.Add(new SaidaConfiguration());
            modelBuilder.Configurations.Add(new EntradaConfiguration());
            modelBuilder.Configurations.Add(new StatusEntradaConfiguration());
            modelBuilder.Configurations.Add(new HistoricoEntradaConfiguration());
            modelBuilder.Configurations.Add(new ProvidenciaConfiguration());
            modelBuilder.Configurations.Add(new PossivelProvidenciaConfiguration());
            modelBuilder.Configurations.Add(new SetorCompetenteConfiguration());
            modelBuilder.Configurations.Add(new DestinatarioEntradaConfiguration());
            #endregion

            #region SISPS
            modelBuilder.Configurations.Add(new ClassificacaoVagaConfiguration());
            modelBuilder.Configurations.Add(new EditalConfiguration());
            //modelBuilder.Configurations.Add(new InscricaoConfiguration());
            modelBuilder.Configurations.Add(new VagaConfiguration());
            #endregion

            #region PAC
            modelBuilder.Configurations.Add(new ModalidadeCompraConfiguration());
            modelBuilder.Configurations.Add(new PACDestinoConfiguration());
            modelBuilder.Configurations.Add(new TipoAquisicaoConfiguration());
            modelBuilder.Configurations.Add(new PacStatusConfiguration());
            #endregion

            #region Bolsas
            modelBuilder.Configurations.Add(new BolsistaConfiguration());
            modelBuilder.Configurations.Add(new BolsaConfiguration());
            modelBuilder.Configurations.Add(new BolsaVigenciaConfiguration());
            #endregion

            #region Admin
            modelBuilder.Configurations.Add(new Contato_fakeConfiguration());
            modelBuilder.Configurations.Add(new MenuConfiguration());
            modelBuilder.Configurations.Add(new MenuPaginaConfiguration());
            modelBuilder.Configurations.Add(new PaginaConfiguration());
            modelBuilder.Configurations.Add(new SistemaConfiguration());
            modelBuilder.Configurations.Add(new UsuarioSistemaConfiguration());
            modelBuilder.Configurations.Add(new PessoaConfiguration());
            modelBuilder.Configurations.Add(new PessoaFisicaConfiguration());
            modelBuilder.Configurations.Add(new PessoaJuridicaConfiguration());
            modelBuilder.Configurations.Add(new UsuarioConfiguration());
            modelBuilder.Configurations.Add(new UsuarioFuspConfiguration());
            modelBuilder.Configurations.Add(new LogSistemaConfiguration());
            #endregion

            #region Folha
            modelBuilder.Configurations.Add(new ContratoAutonomoConfiguration());
            #endregion

            #region Contratos
            modelBuilder.Configurations.Add(new ContratoPessoaFisicaConfiguration());
            modelBuilder.Configurations.Add(new ContratoPagamentoConfiguration());
            modelBuilder.Configurations.Add(new ContratoBolsaConfiguration());
            #endregion

            #region PessoaJuridica
            modelBuilder.Configurations.Add(new CategoriaConfiguration());
            modelBuilder.Configurations.Add(new RamoAtividadeConfiguration());
            modelBuilder.Configurations.Add(new CategoriaDocumentoConfiguration());
            modelBuilder.Configurations.Add(new FornecedorConfiguration());
            modelBuilder.Configurations.Add(new StatusFornecedorConfiguration());
            modelBuilder.Configurations.Add(new HistoricoFornecedorConfiguration());
            modelBuilder.Configurations.Add(new DocumentoFornecedorConfiguration());
            modelBuilder.Configurations.Add(new RepresentanteLegalConfiguration());
            modelBuilder.Configurations.Add(new SocioConfiguration());
            modelBuilder.Configurations.Add(new DiretorConfiguration());
            modelBuilder.Configurations.Add(new ReferenciaBancariaConfiguration());
            modelBuilder.Configurations.Add(new StatusDocFornecedorConfiguration());
            #endregion

            #region Arquivo
            modelBuilder.Configurations.Add(new TipoVolumeConfiguration());
            modelBuilder.Configurations.Add(new LocalizacaoVolumeConfiguration());
            modelBuilder.Configurations.Add(new VolumeConfiguration());
            modelBuilder.Configurations.Add(new EmprestimoVolumeConfiguration());
            #endregion

            #region Comum
            modelBuilder.Configurations.Add(new BancoConfiguration());
            modelBuilder.Configurations.Add(new BancoAgenciaConfiguration());
            modelBuilder.Configurations.Add(new ContaConfiguration());
            modelBuilder.Configurations.Add(new MoedaConfiguration());
            modelBuilder.Configurations.Add(new SetorConfiguration());
            modelBuilder.Configurations.Add(new FormaPagtoConfiguration());
            //modelBuilder.Configurations.Add(new IrrfConfiguration());
            //modelBuilder.Configurations.Add(new IrrfFaixasConfiguration());
            modelBuilder.Configurations.Add(new CepConfiguration());
            modelBuilder.Configurations.Add(new TipoContratoConfiguration());
            #endregion

            #region Conciliação
            modelBuilder.Configurations.Add(new ContaTipoConfiguration());
            modelBuilder.Configurations.Add(new ContaLanctoConfiguration());
            modelBuilder.Configurations.Add(new TipoLctoConfiguration());
            modelBuilder.Configurations.Add(new ContaTransfConfiguration());
            modelBuilder.Configurations.Add(new ContaAplicacaoConfiguration());
            modelBuilder.Configurations.Add(new TipoImpArquivoConfiguration());
            modelBuilder.Configurations.Add(new ImportaArquivoConfiguration());
            modelBuilder.Configurations.Add(new UsuarioTipoLctoConfiguration());
            modelBuilder.Configurations.Add(new ContaSaldoDiferencaConfiguration());
            #endregion

            #region Cursos
            modelBuilder.Configurations.Add(new CursoConfiguration());
            #endregion

            #region Remessa Bancaria
            modelBuilder.Configurations.Add(new TipoLoteConfiguration());
            modelBuilder.Configurations.Add(new LoteConfiguration());
            modelBuilder.Configurations.Add(new LotePagBBConfiguration());
            modelBuilder.Configurations.Add(new TipoRetornoConfiguration());
            modelBuilder.Configurations.Add(new FinalidadePagtoConfiguration());
            modelBuilder.Configurations.Add(new TipoPagtoConfiguration());
            modelBuilder.Configurations.Add(new TipoConciliacaoConfiguration());
            modelBuilder.Configurations.Add(new ArquivosRetornoConfiguration());
            modelBuilder.Configurations.Add(new TipoArquivoConfiguration());
            modelBuilder.Configurations.Add(new CarregarFolhaAntigaConfiguration());
            modelBuilder.Configurations.Add(new TipoFolhaPagtoConfiguration());


            #endregion

            #region SCP
            modelBuilder.Configurations.Add(new AtuacaoConfiguration());
            modelBuilder.Configurations.Add(new ClassificacaoConfiguration());
            modelBuilder.Configurations.Add(new InstrumentoContratualConfiguration());
            modelBuilder.Configurations.Add(new NaturezaConfiguration());
            modelBuilder.Configurations.Add(new DocumentoConfiguration());
            modelBuilder.Configurations.Add(new UnidadeConfiguration());
            modelBuilder.Configurations.Add(new DepartamentoConfiguration());
            modelBuilder.Configurations.Add(new LaboratorioConfiguration());
            modelBuilder.Configurations.Add(new TaxaProjetoConfiguration());
            modelBuilder.Configurations.Add(new FinanciadorConfiguration());
            modelBuilder.Configurations.Add(new ProjetoDocumentoConfiguration());
            modelBuilder.Configurations.Add(new PlanoContaConfiguration());
            modelBuilder.Configurations.Add(new ProjetoTaxaConfiguration());
            modelBuilder.Configurations.Add(new ClassificacaoFuspConfiguration());
            modelBuilder.Configurations.Add(new OpcaoAdiantamentoConfiguration());
            modelBuilder.Configurations.Add(new RequisitoEncerramentoConfiguration());
            modelBuilder.Configurations.Add(new SolicitacaoDeProjetoConfiguration());
            modelBuilder.Configurations.Add(new NotificacaoConfiguration());
            modelBuilder.Configurations.Add(new ContasProjetoConfiguration());
            modelBuilder.Configurations.Add(new ArquivoAnexoProjetoConfiguration());

            #endregion

            #region Comodato
            modelBuilder.Configurations.Add(new StatusComodatoConfiguration());
            modelBuilder.Configurations.Add(new PatrimonioConfiguration());
            modelBuilder.Configurations.Add(new HistoricoComodatoConfiguration());
            modelBuilder.Configurations.Add(new ComodatoConfiguration());
            #endregion

            #region Adiantamento
            modelBuilder.Configurations.Add(new AdiantamentoConfiguration());
            modelBuilder.Configurations.Add(new AdiantamentoViagemConfiguration());
            modelBuilder.Configurations.Add(new BeneficiarioConfiguration());
            modelBuilder.Configurations.Add(new HistoricoAdiantamentoConfiguration());
            modelBuilder.Configurations.Add(new StatusAdiantamentoConfiguration());
            modelBuilder.Configurations.Add(new TiposAdiantamentoConfiguration());
            modelBuilder.Configurations.Add(new EmailPadraoConfiguration());
            modelBuilder.Configurations.Add(new EmailCopiaConfiguration());
            modelBuilder.Configurations.Add(new HistoricoEmailAdmtoConfiguration());
            #endregion

            #region demosntrativo
            modelBuilder.Configurations.Add(new vUsuariosDemonstrativoConfiguration());
            modelBuilder.Configurations.Add(new vUsuariosProjetosDemonstrativoConfiguration());
            #endregion

            #region recibos
            modelBuilder.Configurations.Add(new ReciboConfiguration());
            modelBuilder.Configurations.Add(new ReciboChequeConfiguration());
            modelBuilder.Configurations.Add(new ReciboCursoConfiguration());
            modelBuilder.Configurations.Add(new ReciboFuspConfiguration());
            #endregion

            modelBuilder.Configurations.Add(new vContratosPessoaConfiguration());



            #region scfp
            //modelBuilder.Configurations.Add(new BaseCalculoConfiguration());
            modelBuilder.Configurations.Add(new BaseCalculoDedutivelConfiguration());
            modelBuilder.Configurations.Add(new TaxaConfiguration());
            modelBuilder.Configurations.Add(new TabelaTaxasConfiguration());
            modelBuilder.Configurations.Add(new FaixaTaxasConfiguration());
            modelBuilder.Configurations.Add(new LancamentoConfiguration());
            modelBuilder.Configurations.Add(new LancamentoItemConfiguration());
            modelBuilder.Configurations.Add(new ImportacaoConfiguration());
            modelBuilder.Configurations.Add(new LancamentoTipoConfiguration());
            modelBuilder.Configurations.Add(new PlanoContaTipoLancamentoConfiguration());

            modelBuilder.Configurations.Add(new IdentificacaoSegmentoConfiguration());
            modelBuilder.Configurations.Add(new IdentficacaoSegPlanContaConfiguration());
            modelBuilder.Configurations.Add(new ProcessamentoDespesaConfiguration());
            modelBuilder.Configurations.Add(new DespesaConfiguration());
            modelBuilder.Configurations.Add(new DespesaPessoaFisicaConfiguration());
            modelBuilder.Configurations.Add(new DespesaAutonomoConfiguration());
            modelBuilder.Configurations.Add(new DespesaConsumoConfiguration());
            modelBuilder.Configurations.Add(new DespesaInovacaoConfiguration());
            modelBuilder.Configurations.Add(new DespesaBolsaConfiguration());
            modelBuilder.Configurations.Add(new DespesaBoletoConfiguration());
            modelBuilder.Configurations.Add(new DespesaImpostoConsumoConfiguration());
            modelBuilder.Configurations.Add(new DespesaGRFConfiguration());
            modelBuilder.Configurations.Add(new DespesaGPSConfiguration());
            modelBuilder.Configurations.Add(new DespesaDARFConfiguration());
            modelBuilder.Configurations.Add(new DespesaGRFRConfiguration());
            modelBuilder.Configurations.Add(new DespesaDAMSPConfiguration());
            modelBuilder.Configurations.Add(new DespesaProjetoConfiguration());
            modelBuilder.Configurations.Add(new TransferenciaProjetoConfiguration());
            modelBuilder.Configurations.Add(new DespesaAluguelConfiguration());
            modelBuilder.Configurations.Add(new DespesaPremioConfiguration());
            modelBuilder.Configurations.Add(new DespesaServTercPJConfiguration());
            modelBuilder.Configurations.Add(new DespesaPessoaJuridicaConfiguration());
            modelBuilder.Configurations.Add(new LancamentosSCPRConfiguration());
            modelBuilder.Configurations.Add(new vDespesasToDbfConfiguration());
            //modelBuilder.Configurations.Add(new TipoDespesaConfiguration());
            #endregion


            #region EditaisLic
            modelBuilder.Configurations.Add(new EditalLicConfiguration());
            modelBuilder.Configurations.Add(new EditalLicAnexoConfiguration());
            modelBuilder.Configurations.Add(new StatusEditaisLicConfiguration());
            modelBuilder.Configurations.Add(new InscricaoPregaoConfiguration());
            modelBuilder.Configurations.Add(new LogEditalConfiguration());

            #endregion
        }


        public List<vResumoAutorizacaoPagtos> Lista_ResumoAutorizacaoPagtos(DateTime data)
        {
            try
            {
                StringBuilder commandSQL = new StringBuilder();
                commandSQL.Append("SELECT id_lote, valor_credito_cc, valor_doc, valor_ted, valor_credito_cp, valor_tit_BB, valor_tit_outros, valor_imp_cons,valor_gps,valor_gru, rejeitados,");
                commandSQL.Append(" total, '' as decricao_rejeitado FROM [dbo].[ResumoAutorizacaoPagtos] (@dt)");
                return this.Database.SqlQuery<vResumoAutorizacaoPagtos>(commandSQL.ToString(), new SqlParameter("dt", data)).ToList();
            }
            catch (Exception ex)
            {
                ex.ToString();
                return null;
            }
        }

        public IQueryable<SaldoConta> Lista_Saldocontas(DateTime data)
        {
            try
            {
                StringBuilder commandSQL = new StringBuilder();
                commandSQL.Append("SELECT id_conta,numero, descricao,digito,cod_def_projeto,UPPER(agencia) as agencia,UPPER(banco) as banco,");
                commandSQL.Append("coalesce(creditos,0) as creditos,coalesce(debitos,0) as debitos,coalesce(saldo_final,0) as saldo_final,");
                commandSQL.Append("coalesce(saldo_anterior,0) as saldo_anterior ");
                commandSQL.Append("FROM lista_saldocontas (@dt)");
                return this.Database.SqlQuery<SaldoConta>(commandSQL.ToString(), new SqlParameter("dt", data)).AsQueryable();
            }
            catch (Exception ex)
            {
                ex.ToString();
                return null;
            }
        }

        public IQueryable<ExtratoConta> ExtratoConta_PorPeriodo(int intId_conta, DateTime dtDe, DateTime dtAte)
        {
            try
            {
                StringBuilder commandSQL = new StringBuilder();
                commandSQL.Append("SELECT id_lcto_conta,id_conta,data,descricao,dc,valor,saldo,num_documento,proj_num ");
                commandSQL.Append("FROM dbo.extratoconta_porperiodo(@id_conta,@de,@ate)");
                return this.Database.SqlQuery<ExtratoConta>(commandSQL.ToString(), new SqlParameter("id_conta", intId_conta),
                    new SqlParameter("de", dtDe),
                    new SqlParameter("ate", dtAte)).AsQueryable();
            }
            catch (Exception ex)
            {
                ex.ToString();
                return null;
            }
        }

        public IQueryable<vTaxasReceitasRm> ExtratoTaxasReceitasRm_PorPeriodo(DateTime dtDe, DateTime dtAte)
        {
            try
            {
                StringBuilder commandSQL = new StringBuilder();
                commandSQL.Append("SELECT codgerencial,Projeto,nome_projeto,recebimentos,despesas_indiretas,percentual ");
                commandSQL.Append("FROM dbo.taxasreceitasrm(@de,@ate)");
                return this.Database.SqlQuery<vTaxasReceitasRm>(commandSQL.ToString(),
                    new SqlParameter("de", dtDe),
                    new SqlParameter("ate", dtAte)).AsQueryable();
            }
            catch (Exception ex)
            {
                ex.ToString();
                return null;
            }
        }

        public decimal SaldoConta_PorData(DateTime data, int intId_conta)
        {
            try
            {
                SqlParameter spSaldo = new SqlParameter("saldo", 0);
                spSaldo.DbType = DbType.Decimal;
                spSaldo.Direction = ParameterDirection.Output;
                StringBuilder commandSQL = new StringBuilder();
                commandSQL.Append("SET @saldo = (SELECT coalesce(dbo.saldoconta_pordata(@id_conta,@data),0))");
                this.Database.ExecuteSqlCommand(commandSQL.ToString(), new SqlParameter("data", data), new SqlParameter("id_conta", intId_conta), spSaldo);
                return Convert.ToDecimal(spSaldo.Value);
            }
            catch (Exception ex)
            {
                ex.ToString();
                return 0;
            }
        }

        //public IQueryable<CarregarFolhaBolsaNova> CarregarFolhaBolsaNova(DateTime data)
        //{
        //    try
        //    {
        //        StringBuilder commandSQL = new StringBuilder();
        //        commandSQL.Append("SELECT id,projeto,nome_bolsista,cpf_bolsista,bolsa,banco,agencia,digagencia,conta,digcta,valor ");
        //        commandSQL.Append("FROM CarregarFolhaBolsaNova (@dt)");
        //        return this.Database.SqlQuery<CarregarFolhaBolsaNova>(commandSQL.ToString(), new SqlParameter("dt", data)).AsQueryable();
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ToString();
        //        return null;
        //    }
        //}

        public IQueryable<CarregarFolha> CarregarFolha(DateTime data, int id_tipo_folha_pagto)
        {
            try
            {
                StringBuilder commandSQL = new StringBuilder();
                commandSQL.Append("SELECT  id,id_tipo_folha_pagto,id_projeto,proj,valorbruto,data_pgto,deducaoINSS,iss,cpf_cnpj,nome,id_banco,bank,agencia,conta,ndeppes,codpf,codbarra,rdpf ");
                commandSQL.Append(" FROM [dbo].[CarregarFolhaNova] (@dt, @id)");
                return carregarFolha.SqlQuery(commandSQL.ToString(), new object[] { new SqlParameter("dt", data), new SqlParameter("id", id_tipo_folha_pagto) }).AsQueryable();
            }
            catch (Exception ex)
            {
                ex.ToString();
                return null;
            }
        }

        public IQueryable<CarregarFolhaBolsaNova> CarregarFolhaBolsaNova(DateTime data)
        {
            try
            {
                StringBuilder commandSQL = new StringBuilder();
                commandSQL.Append("SELECT id,projeto,nome_bolsista,cpf_bolsista,bolsa,banco,agencia,digagencia,conta,digcta,valor ");
                commandSQL.Append("FROM CarregarFolhaBolsaNova (@dt)");
                return this.Database.SqlQuery<CarregarFolhaBolsaNova>(commandSQL.ToString(), new SqlParameter("dt", data)).AsQueryable();
            }
            catch (Exception ex)
            {
                ex.ToString();
                return null;
            }
        }

        public IQueryable<Entrada> CarregarCaixaEntrada(int id_usuario, int id_status_entrada, int ano)
        {
            try
            {
                StringBuilder commandSQL = new StringBuilder();
                commandSQL.Append("SELECT [id_entrada],[nprotent],[dataprot],[dataent],[codproj],[tipodocent],[numdocent],[valorent]");
                commandSQL.Append(",[descrent],[enviadoent],[obsent],[id_usu_para],[codproja],[ano],[id_valor_moeda],[id_usu_de],[id_usu_entrada]");
                commandSQL.Append(",[id_ultimo_status],[id_ultimo_para],[id_projeto] FROM [dbo].[PesquisaCaixaEntrada] (@id_usuario,@id_status_doc, @ano) ");

                return this.Database.SqlQuery<Entrada>(commandSQL.ToString(),
                      new object[]{ 
                        new SqlParameter("id_usuario", id_usuario),
                        new SqlParameter("id_status_doc", id_status_entrada),
                        new SqlParameter("ano", ano)
                      }).AsQueryable();
            }
            catch (Exception ex)
            {
                ex.ToString();
                return null;
            }
        }

        public IQueryable<Entrada> CarregarCaixaSaida(int id_usuario, int id_status_entrada, int ano)
        {
            try
            {
                StringBuilder commandSQL = new StringBuilder();
                commandSQL.Append("SELECT [id_entrada],[nprotent],[dataprot],[dataent],[codproj],[tipodocent],[numdocent],[valorent]");
                commandSQL.Append(",[descrent],[enviadoent],[obsent],[id_usu_para],[codproja],[ano],[id_valor_moeda],[id_usu_de],[id_usu_entrada]");
                commandSQL.Append(",[id_ultimo_status],[id_ultimo_para],[id_projeto] FROM [dbo].[PesquisaCaixaSaida] (@id_usuario,@id_status_doc, @ano) ");

                return this.Database.SqlQuery<Entrada>(commandSQL.ToString(),
                      new object[]{ 
                        new SqlParameter("id_usuario", id_usuario),
                        new SqlParameter("id_status_doc", id_status_entrada),
                        new SqlParameter("ano", ano)
                      }).AsQueryable();
            }
            catch (Exception ex)
            {
                ex.ToString();
                return null;
            }
        }

    }
}


