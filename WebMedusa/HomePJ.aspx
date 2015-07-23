<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/SiteBasePJ.Master" AutoEventWireup="true"
    CodeBehind="HomePJ.aspx.cs" Inherits="Medusa.HomePJ" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="aspCt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadBaseMaster" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBaseMaster" runat="server">
   

    <aspCt:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </aspCt:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="loginFields">
                Nome de Usuário
                <asp:TextBox ID="txtLogin" runat="server" Width="125px" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvUsuario" runat="server" ErrorMessage="? " ControlToValidate="txtLogin"
                    SetFocusOnError="True" SkinID="naotem" ForeColor="Red"></asp:RequiredFieldValidator>
                Senha
                <asp:TextBox ID="txtSenha" runat="server" Width="125px" MaxLength="20" TextMode="Password"
                    SkinID="naotem"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvSenha" runat="server" ControlToValidate="txtSenha"
                    ErrorMessage="? " SetFocusOnError="True" SkinID="reqfield_sem_msg" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:Button ID="btEntrar" runat="server" Text="entrar" BackColor="#404040" BorderStyle="Double"
                    ForeColor="White" OnClick="btEntrar_Click" />
                <asp:HyperLink ID="hlSenha" runat="server" Font-Underline="True" NavigateUrl="EsqueceuSenha.aspx"
                    ForeColor="White">esqueceu sua senha?</asp:HyperLink>&nbsp;&nbsp;
                <asp:LinkButton ID="lkNew" runat="server" PostBackUrl="~/Sistemas/Admin/UsuariosExternos.aspx"
                    CausesValidation="False" ForeColor="White">Não possui cadastro? Cadastre-se!</asp:LinkButton>
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentBaseMaster" runat="server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div id="master_page">
                <div id="master_menu">
                    <div class="sidebar">
                        <div class="sidebarheader">
                            Links</div>
                        <div id="master_sidebarLinks" class="sidebarcontent">
                            <asp:HyperLink ID="ConhecaFUSP" runat="server" Target="_blank" Text="Site da FUSP"
                                NavigateUrl="http://www.fusp.org.br" EnableViewState="false" /><br />
                                <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" Text="Manual de Funcionamento"
                                NavigateUrl="~/Sistemas/PessoaJuridica/manual.pdf" EnableViewState="false" /><br />
                        </div>
                        <br />
                        <div class="sidebarheader">
                            AVISO</div>
                        <div class="sidebarcontent">
                                                     

                                <p>Cadastros válidos até 30/11/2012:</p>
                                <p>Empresas cujo cadastro estavam válidos na data acima tiveram suas informações 
                                    migradas para o novo sistema. Para acessá-lo, basta inserir o CNPJ da empresa no 
                                    campo “Nome de Usuário” e a senha cadastrada no sistema anterior.</p>

                                <p>Cadastros com validade anterior à 30/11/2012:</p>
                                <p>Empresas cujo cadastro estavam vencidos na data acima <u>não</u> tiveram suas informações migradas para o novo sistema.
                                Caso queiram, deverão solicitar nova inscrição no cadastro de pessoa jurídica da fusp.</p>
                        </div>
                    </div>
                </div>
                <div id="master_content">
                    <%--<div id="master_contentheader">--%>
                    <asp:Panel ID="Panel2" runat="server" CssClass="collapsePanelHeader" Height="30px">
                        <div style="padding: 5px; cursor: pointer; vertical-align: middle;">
                            <div style="float: left;">
                                ORIENTAÇÕES PARA INSCRIÇÃO NO CADASTRO DE PESSOA JURÍDICA</div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="Panel1" runat="server" BorderWidth="1px" BorderStyle="Solid" Style="padding-left: 10px">

                    
                        <p>
                            <em>A inscrição no </em><em><u>Cadastro de Pessoa Jurídica</u></em><em> destina-se a
                                todas as empresas que possuem interesse em fornecer bens e serviços a projetos gerenciados
                                pela FUSP.
                                <p>
                                    <em>O Cadastro de </em><em><u>Cadastro de Pessoa Jurídica</u></em><em> possui <strong>
                                        2 (duas) categorias</strong>:
                                        <ul>
                                            <li><em><strong>Cadastro Simples</strong></em><em>: destinado a empresas interessadas
                                                em fornecer bens e serviços de pequeno vulto, limitado ao valor máximo de R$ 35.000,00
                                                (trinta e cinco mil reais) por ano, com ou sem contrato, ou interessados em participar
                                                de processos de compra direta, compra simples ou fornecimento de bens de pronta
                                                entrega..</em></li>
                                            <li><em><strong>Cadastro Completo</strong></em><em>: destinado a empresas interessadas
                                                em fornecer bens e serviços que ultrapassarem o limite de R$ 35.000,00 (trinta e
                                                cinco mil reais) por ano, com ou sem contrato, ou interessados em participar de
                                                processos de compra completa ou procedimentos licitatórios da FUSP de cotação prévia
                                                de preços ou nas modalidades convite, tomada de preços, concorrência ou pregão.</em></li>
                                        </ul>
                                        <p>
                                            <em>A classificação da categoria de cada fornecimento de bem ou serviço será estipulado
                                                em conformidade com o tipo de processo de compra que a originou. Na dúvida, a empresa
                                                deverá questionar a Coordenação do Projeto solicitante do fornecimento ou contratação.
                                                <div>
                                                    <p>
                                                        <em><strong>OBSERVAÇÃO IMPORTANTE:</strong></em><br />
                                                        <em>Se a qualquer momento se vislumbrar a possibilidade de ultrapassar o limite de valor
                                                            no caso de Cadastro Simples, a empresa deverá providenciar <strong>sua alteração cadastral</strong>
                                                            da categoria <strong>Cadastro Simples</strong> para a categoria <strong>Cadastro Completo</strong>
                                                    .
                                                </div>
                                                <p>
                                                    <em>Para requerer o cadastramento no </em><em><u>Cadastro de Pessoa Jurídica</u></em><em>
                                                        o interessado deverá efetuar os seguintes procedimentos:
                                                        <p>
                                                            <em><strong>1 Cadastramento eletrônico</strong>
                                                                <p>
                                                                    <em>Para inscrever-se, a empresa deverá entrar no site </em><em><a href="http://www.fusp.org.br">
                                                                        www.fusp.org.br</a>, acessar o menu </em><strong>Cadastro de Pessoa Jurídica</strong>,
                                                                    ler as <strong>Orientações para Cadastro de Pessoa Jurídica</strong>, clicar em
                                                                    <strong>Não possui cadastro? Cadastre-se!</strong>
                                                                </p>
                                                                <p>
                                                                    <em>Somente após a efetivação do cadastramento eletrônico que a empresa interessada
                                                                        poderá encaminhar sua solicitação de inscrição no Cadastro de Pessoa Jurídica da
                                                                        FUSP. </em>
                                                                </p>
                                                                <p>
                                                                    <strong>2 Encaminhamento da solicitação da inscrição</strong>
                                                                </p>
                                                                <p>
                                                                    Depois do cadastramento eletrônico, a empresa interessada deverá <strong>imprimir</strong>
                                                                    o <u>Formulário de Cadastro</u>, que deverá ser assinado por seu Representante Legal.
                                                                    O Formulário de Cadastro devidamente assinado pelo representante legal da empresa
                                                                    interessa, juntamente com a documentação pertinente a sua categoria, conforme relação
                                                                    contida no item 3, deverá ser encaminhada à Comissão de Análise Cadastral da FUSP
                                                                    por uma das seguintes formas:</p>
                                                                <br />
                                                                <ul>
                                                                    a. Pessoalmente, na recepção da sede da FUSP, na Av. Afrânio Peixoto nº 14, Butantã,
                                                                    CEP 05507-000, São Paulo, SP, no horário de atendimento, de segunda a sexta-feira,
                                                                    das 08h às 11h 30 min e 13h às 16h 30min.
                                                                </ul>
                                                                <br />
                                                                <ul>
                                                                    b. Por correio, no mesmo endereço do item “a”.
                                                                </ul>
                                                                <p>
                                                                    <strong>3 Documentos necessários à validação do cadastro</strong></p>
                                                                <p>
                                                                    3.1 Categoria: <strong>CADASTRO SIMPLES</strong></p>
                                                                <ul>
                                                                    1. Registro Comercial, Ato Constitutivo, Estatuto ou Contrato Social em vigor;</ul>
                                                                <ul>
                                                                    2. Prova de Inscrição no Cadastro Nacional de Pessoa Jurídica – CNPJ;</ul>
                                                                <ul>
                                                                    3. Prova de Inscrição no Cadastro de Contribuinte Estadual – DECA (se houver);</ul>
                                                                <ul>
                                                                    4. Prova de Inscrição no Cadastro de Contribuinte Municipal – CCM;</ul>
                                                                <ul>
                                                                    5. Certidão Negativa de Débitos Relativos às Contribuições e às de Terceiros – INSS;</ul>
                                                                <ul>
                                                                    6. Certificado de Regularidade do FGTS–CRF;</ul>
                                                                <ul>
                                                                    7. Certidão Negativa de Débitos Trabalhistas</ul>
                                                                <br />
                                                                <p>
                                                                    3.2 Categoria: <strong>CADASTRO COMPLETO</strong></p>
                                                                <ul>
                                                                    1. Registro Comercial, Ato Constitutivo, Estatuto ou Contrato Social em vigor;</ul>
                                                                <ul>
                                                                    2. Prova de Inscrição no Cadastro Nacional de Pessoa Jurídica – CNPJ;</ul>
                                                                <ul>
                                                                    3.Prova de Inscrição no Cadastro de Contribuinte Estadual – DECA (se houver);</ul>
                                                                <ul>
                                                                    4.Prova de Inscrição no Cadastro de Contribuinte Municipal – CCM;</ul>
                                                                <ul>
                                                                    5. Certidão Negativa de Débitos Relativos às Contribuições Previdenciárias e às
                                                                    de Terceiros – INSS;</ul>
                                                                <ul>
                                                                    6. Certificado de Regularidade do FGTS–CRF;</ul>
                                                                <ul>
                                                                    7. Certidão Negativa de Débitos Trabalhistas;</ul>
                                                                <ul>
                                                                    8. Certidão Negativa de Tributos Federais;</ul>
                                                                <ul>
                                                                    9. Certidão Negativa de Tributos Estaduais;</ul>
                                                                <ul>
                                                                    10. Certidão Negativa de Tributos Municipais;</ul>
                                                                <ul>
                                                             11. Certidão de Falências, Concordatas, Recuperações Judiciais e Extrajudiciais.</ul>
                                                                <br />
                                                                <p>
                                                                    Os documentos relacionados nos itens <strong>2 a 7</strong> do <strong>Cadastros Simples</strong>
                                                                    e <strong>2 a 10 Cadastros Completo</strong> poderão ser emitidos via internet,
                                                                    no site dos órgãos competentes.</p>
                                                                <p>
                                                                    Os documentos relacionados no item <strong>1</strong> do Cadastros Simples e <strong>
                                                                        1 e 11</strong> do </strong>Cadastro Completo</strong> deverão ser apresentados
                                                                    por cópia autenticada em cartório competente ou por funcionário da FUSP.</p>
                                                                <p>
                                                                    A <strong>Certidão Negativa de Tributos Estaduais</strong> é exigida como prova
                                                                    de regularidade para com a Fazenda Estadual inclusive para empresas que não possuem
                                                                    Inscrição Estadual (ISENTA).</p>
                                                                <p>
                                                                    As Certidões Negativas aqui exigidas podem ser substituídas por Certidões Positivas
                                                                    com Efeito de Negativas.</p>
                                                                <p>
                                                                    Não serão aceitos: protocolo de pedido de certidão nem qualquer substituição de
                                                                    documento exigido acima e também documentos enviados por e-mail.
                                                                </p>
                                                                <p>
                                                                    <strong>4 Relação de sites para emissão de certidões</strong></p>
                                                                <p>
                                                                    Lista de links para emissão de certidões acima relacionadas (sujeitos a alterações
                                                                    sem aviso prévio):</p>
                                                                    <p>
                                                                CNPJ:<br />
                                                                <a href="http://www.receita.fazenda.gov.br/PessoaJuridica/CNPJ/cnpjreva/Cnpjreva_Solicitacao.asp">http://www.receita.fazenda.gov.br/PessoaJuridica/CNPJ/cnpjreva/Cnpjreva_Solicitacao.asp</a>
                                                                </p>

<p>DECA – Estado SP:<br />
<a href="http://www.sintegra.gov.br/"> http://www.sintegra.gov.br/ </a>
</p>
<p>
CCM – Cidade SP:<br />
<a href="http://www.prefeitura.sp.gov.br/cidade/secretarias/financas/servicos/ccm/index.php?p=2373"> http://www.prefeitura.sp.gov.br/cidade/secretarias/financas/servicos/ccm/index.php?p=2373 </a>
</p>
<p>
CCM – Portal de Inf. e Solic. Fiscal de ISSQN:<br />
<a href="ahttp://www.informe.issqn.com.br/sitCadSch.cfm"> http://www.informe.issqn.com.br/sitCadSch.cfm </a>
</p>
<p>
INSS:<br />
<a href="http://www010.dataprev.gov.br/cws/contexto/cnd/cnd.html">http://www010.dataprev.gov.br/cws/contexto/cnd/cnd.html </a>
</p>
<p>
FGTS:<br />
<a href="https://webp.caixa.gov.br/cidadao/Crf/FgeCfSCriteriosPesquisa.asp">https://webp.caixa.gov.br/cidadao/Crf/FgeCfSCriteriosPesquisa.asp </a>
</p>
<p>
Débitos Trabalhistas:<br />
<a href="http://www.tst.jus.br/certidao">http://www.tst.jus.br/certidao </a>
</p>
<p>
Tributos Federais:<br />
<a href="http://www.receita.fazenda.gov.br/Aplicacoes/ATSPO/Certidao/CndConjuntaInter/InformaNiCertidao.asp?tipo=1">http://www.receita.fazenda.gov.br/Aplicacoes/ATSPO/Certidao/CndConjuntaInter/InformaNiCertidao.asp?tipo=1 </a>
</p>
<p>
Tributos Estaduais – SP:<br />
<a href="http://www.dividaativa.pge.sp.gov.br/da-ic-web/inicio.do">http://www.dividaativa.pge.sp.gov.br/da-ic-web/inicio.do </a>
</p>
<p>
Tributos Municipais – SP:<br />
<a href="http://www3.prefeitura.sp.gov.br/certidao/ctm_imp01.asp">http://www3.prefeitura.sp.gov.br/certidao/ctm_imp01.asp</a>
</p>


                                                                <p>
                                                                    <strong>5 Prazo de Análise</strong></p>
                                                                <p>
                                                                    Caberá a Comissão de Análise Cadastral avaliar a solicitação de inscrição no Cadastro
                                                                    de Pessoa Jurídica da FUSP no prazo de <strong>4 (quatro) dias úteis</strong>, contados
                                                                    a partir da data do protocolo de recebimento da solicitação de cadastro, juntamente
                                                                    com o formulário de cadastro e documentos pertinentes a categoria, na recepção da
                                                                    FUSP, concluindo aprovação ou reprovação da inscrição.
                                                                </p>
                                                                <p>
                                                                    Solicitações ou documentos recebidos por e-mail não serão considerados, salvo documentos
                                                                    obtidos através da internet que permitam a conferência de sua autenticidade também
                                                                    pela internet.
                                                                </p>
                                                                <p>
                                                                    A empresa solicitante da inscrição deverá aguardar o período de análise e acompanhar
                                                                    a aprovação ou reprovação do cadastro no site da FUSP, através de seu <strong>login</strong>
                                                                    e <strong>senha</strong>.
                                                                </p>
                                                                <p>
                                                                    Na avaliação dos documentos apresentados para efetivação do cadastro, sendo constatada
                                                                    alguma divergência ou questão que necessite de esclarecimento, o interessado será
                                                                    notificado por e-mail.
                                                                </p>
                                                                <p>
                                                                    O prazo para regularização da divergência ou questão mencionada acima é de <strong>4
                                                                        (quatro) dias úteis</strong>, contados a partir da data do recebimento da notificação.
                                                                </p>
                                                                <p>
                                                                    Caso seja apresentada justificativa plausível, o prazo para regularização citado
                                                                    acima poderá ser prorrogado, uma única vez, pelo mesmo período, mediante autorização
                                                                    por escrito da Comissão de Análise de Cadastro.
                                                                </p>
                                                                <p>
                                                                    O não atendimento da solicitação de regularidade ou dos prazos estabelecidos acarretará
                                                                    na reprovação da solicitação de cadastramento.
                                                                </p>
                                                                <p>
                                                                    <strong>6 Validade do Cadastro</strong>
                                                                </p>
                                                                <p>
                                                                    O Cadastro de Pessoa Jurídica da FUSP terá <strong>validade de 1 (um) ano</strong>.
                                                                </p>
                                                                <p>
                                                                    Caberá à empresa cadastrada a responsabilidade pela manutenção e atualização de
                                                                    certidões durante a vigência do cadastro e também de providenciar sua atualização
                                                                    pelo menos <u>30 dias corridos antes de sua expiração</u>.
                                                                </p>
                                                                <p>
                                                                    <strong>7 Atualização do Cadastro</strong>
                                                                </p>
                                                                <p>
                                                                    Para realizar a atualização cadastral, a empresa já cadastrada deverá tomar as mesmas
                                                                    providências descritas no item 1 e seguintes desta Orientação, isto é, acessar o
                                                                    site <a href="http://www.fusp.org.br">www.fusp.org.br</a>, menu <strong>Cadastro de
                                                                        Pessoa Jurídica</strong> ler as <strong>Orientações para Cadastro de Pessoa Jurídica</strong>,
                                                                    clicar em <strong>Clique aqui para fazer o login</strong> e preencher o campo <strong>
                                                                        Login Cadastro Fornecedor</strong>(CNPJ e Senha).
                                                                </p>
                                                                <p>
                                                                    Após a atualização do cadastro eletrônico, a empresa interessada encaminhará a solicitação
                                                                    de atualização do Cadastro de Pessoa Jurídica da FUSP, juntamente com o Formulário
                                                                    de Cadastro devidamente assinado pelo representante legal da empresa e documentação
                                                                    pertinente a atualização (documentos alterados, certidões vencidas, etc), por uma
                                                                    das formas relacionas no item 2 desta Orientação.
                                                                </p>
                                                                <p>
                                                                    <strong>8 Uso do Certificado de Registro Cadastral em Processos Licitatórios</strong>
                                                                </p>
                                                                <p>
                                                                    As empresas inscritas no Cadastro de Pessoa Jurídica da FUSP que quiserem participar
                                                                    dos procedimentos licitatórios nas modalidades convite, tomada de preços e concorrência
                                                                    poderão utilizar o Certificado de Registro Cadastral – CRC/FUSP para sua habilitação.
                                                                </p>
                                                                <p>
                                                                    As condições de aceite do CRC/FUSP nessas licitações estão estipuladas no edital
                                                                    do procedimento licitatório.
                                                                </p>
                                                                <p>
                                                                    <strong>9 Esclarecimentos</strong>
                                                                </p>
                                                                <p>
                                                                    Esclarecimentos somente poderão ser solicitados por escrito para o endereço eletrônico
                                                                    <a href="mailto:licitacao@fusp.org.br">licitacao@fusp.org.br</a>. A resposta também
                                                                    somente será encaminhada por e-mail.
                                                                </p>
                                                                <p>
                                                                    <u>Nenhum esclarecimento será fornecido por telefone ou pessoalmente</u>.
                                                                </p>
                                                                    </p>
                                                                    <p>
                                                                    </p>
                                                                    <p>
                                                                    </p>
                                                                    <p>
                                                                    </p>
                                                                </p>
                                                            </em>
                                                            
                    </asp:Panel>
                    <%--</div>--%>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <aspCt:UpdatePanelAnimationExtender ID="upae" BehaviorID="animation" runat="server"
        TargetControlID="UpdatePanel2" Enabled="True">
        <Animations>
            <OnUpdating>
                <Parallel duration="0">
                    <FadeOut minimumOpacity=".5" />
                    <ScriptAction Script="onUpdating();" />  
                 </Parallel>
            </OnUpdating>
            <OnUpdated>
                <Parallel duration="0">
                   <FadeIn minimumOpacity=".5" />  
                    <ScriptAction Script="onUpdated();" /> 
                </Parallel> 
            </OnUpdated>
        </Animations>
    </aspCt:UpdatePanelAnimationExtender>
</asp:Content>
