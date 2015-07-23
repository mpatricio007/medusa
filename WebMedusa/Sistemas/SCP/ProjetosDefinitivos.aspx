<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.Master" AutoEventWireup="true"
    CodeBehind="ProjetosDefinitivos.aspx.cs" Inherits="Medusa.Sistemas.SCP.ProjetosDefinitivos" %>

<%@ Register Src="../../Controles/DdlProjetoA.ascx" TagName="DdlProjetoA" TagPrefix="uc1" %>
<%@ Register Src="ProjetoCoordenadores.ascx" TagName="ProjetoCoordenadores" TagPrefix="uc2" %>
<%@ Register Src="ProjetoFinanciadores.ascx" TagName="ProjetoFinanciadores" TagPrefix="uc3" %>
<%@ Register Src="ControleEnderecoProjeto.ascx" TagName="ControleEnderecoProjeto"
    TagPrefix="uc4" %>
<%@ Register Src="ControleProjetoDocumento.ascx" TagName="ControleProjetoDocumento"
    TagPrefix="uc5" %>
<%@ Register Src="ControleTaxaProjeto.ascx" TagName="ControleTaxaProjeto" TagPrefix="uc6" %>
<%@ Register Src="ControleHistoricoProjeto.ascx" TagName="ControleHistoricoProjeto"
    TagPrefix="uc7" %>
<%@ Register Src="ControleContatoProjeto.ascx" TagName="ControleContatoProjeto" TagPrefix="uc8" %>
<%@ Register Src="ControleSetorResponsavel.ascx" TagName="ControleSetorResponsavel"
    TagPrefix="uc9" %>
<%@ Register Src="ControleAssinProjeto.ascx" TagName="ControleAssinProjeto" TagPrefix="uc10" %>
<%@ Register src="../../Controles/DdlClassificacaoFusp.ascx" tagname="DdlClassificacaoFusp" tagprefix="uc11" %>
<%@ Register src="ControleOpcaoAdiantamento.ascx" tagname="ControleOpcaoAdiantamento" tagprefix="uc12" %>
<%@ Register src="ControleRequisitosEncerramento.ascx" tagname="ControleRequisitosEncerramento" tagprefix="uc13" %>
<%@ Register Src="ControleArquivoAnexo.ascx" TagPrefix="uc14" TagName="ControleArquivoAnexo" %>



<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function pageLoad() {

            $('.date').datepicker({
                showOtherMonths: true,
                changeMonth: true,
                changeYear: true
            });

            $('.date').dateEntry({ spinnerImage: '' });

            $('.cep').mask("99999-999");
            $(".cpf").mask("999.999.999-99");


            var tab = $("#abas").tabs({
                selected: $('#hfSelectedTAB').val(),
                select: function (event, ui) {
                    $('#hfSelectedTAB').val(ui.index);                                       
                }
            });

           

            $("a[href=#aba-10]").click(function () {
                $("#<%= btImprimir0.ClientID %>").click();
            });
            //            $("#abas").tabs("option", "selected", [$('#hfSelectedTAB').val()]);
        }
        function callPrint() {
            var seleted = $('#hfSelectedTAB').val();
            if (seleted == '10')
                $("#<%= btImprimir0.ClientID %>").click();
        }
        function openDialog() {
            Page_ClientValidate('');
            if (Page_IsValid) {
                var dlg = $("#dCodigoDefinitivo").dialog({
                    width: 400,
                    height: 150,
                    position: 'top',
                    modal: true,
                    resizable: false,
                    draggable: false,
                    title: 'tornar projeto definitivo'
                });

                dlg.parent().appendTo(jQuery("form:first"));
                $("#dCodigoDefinitivo").dialog("open");
            }
        }

        function closeDialog() {
            $("#dCodigoDefinitivo").dialog("close");
        }


    </script><div class="conteudo">
        <input type="text" id="hfSelectedTAB" value="0" style="display: none;" />
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div id="dCodigoDefinitivo" style="display: none">
            <asp:UpdatePanel ID="upCodigoDefinitivo" runat="server">
                <ContentTemplate>
                    <table cellpadding="0">
                        <tr>
                            <td>
                                projeto:
                                <cint:cInteiro ID="cIntCod_def_projeto" runat="server" ValidationGroup="1" MaxLength="8" />
                            </td>
                            <td>
                                sub projeto:
                                <ctx:cTexto ID="cTextoSubProj" runat="server" MaxLength="2" ValidationGroup="vgIdentificacao"
                                    Width="20" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btDefinitivo" runat="server" OnClick="btDefinitivo_Click" Text="ok"
                                    ValidationGroup="1" />
                                <asp:Button ID="BtCancel" runat="server" Text="cancelar" OnClientClick="$('#dCodigoDefinitivo').dialog('close');"
                                    CausesValidation="false" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:CheckBox ID="ckFilter" runat="server" AutoPostBack="True" OnCheckedChanged="ckFilter_CheckedChanged"
                    Text="habilitar múltiplos filtros" />
                <asp:DataList ID="DataListFiltros" runat="server" RepeatColumns="12" OnDataBinding="DataListFiltros_DataBinding"
                    RepeatDirection="Horizontal" 
                    OnDeleteCommand="DataListFiltros_DeleteCommand">
                    <ItemTemplate>
                        <div class="FilterName">
                            <%# Eval("property_name") %>&nbsp
                            <%# Eval("mode_name")%>&nbsp
                            <%# Eval("displayValue")%>
                            &nbsp
                            <asp:ImageButton ID="btExcluiFiltro" runat="server" ImageUrl="~/Styles/img/excluir.png"
                                Width="15px" Height="15px" CommandName="delete" />
                        </div>
                    </ItemTemplate>
                </asp:DataList>
                <div class="pesquisar">
                    <asp:Label ID="Label1" runat="server" Text="procurar"></asp:Label>
                    &nbsp;<asp:DropDownList ID="ddlOptions" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlOptions_SelectedIndexChanged">
                        <asp:ListItem Value="codigo">código projeto</asp:ListItem>
                        <asp:ListItem Value="sigla">sigla</asp:ListItem>
                        <asp:ListItem Value="codigoa">projeto A</asp:ListItem>
                        <asp:ListItem Value="titulo">titulo</asp:ListItem>
                        <asp:ListItem>coordenador</asp:ListItem>
                        <asp:ListItem>financiador</asp:ListItem>
                        <asp:ListItem Value="ProjetoSolicitacao.codigo">nº solicitação</asp:ListItem>
                        <asp:ListItem Value="status">status</asp:ListItem>
                        <asp:ListItem Value="data_termino">data término</asp:ListItem>
                        <asp:ListItem Value="num_contrato">n° contrato/convênio</asp:ListItem>
                        <asp:ListItem>natureza</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="listaFinanciador" runat="server" DataTextField="strFinanciador"
                        DataValueField="id_financiador" AppendDataBoundItems="True" Style="margin-bottom: 0px"
                        Width="400px">
                    </asp:DropDownList>
                    <ajaxToolkit:ListSearchExtender ID="listaFinanciador_ListSearchExtender" runat="server"
                        Enabled="True" PromptCssClass="ListSearchExtenderPrompt" PromptText="digite para procurar"
                        QueryPattern="Contains" QueryTimeout="2000" TargetControlID="listaFinanciador">
                    </ajaxToolkit:ListSearchExtender>
                    <asp:DropDownList ID="listaCoordenador" runat="server" DataTextField="nome" DataValueField="id_coordenador"
                        AppendDataBoundItems="True" Width="400px">
                    </asp:DropDownList>
                    <ajaxToolkit:ListSearchExtender ID="listaCoordenador_ListSearchExtender" runat="server"
                        Enabled="True" PromptCssClass="ListSearchExtenderPrompt" PromptText="digite para procurar"
                        QueryPattern="Contains" QueryTimeout="2000" TargetControlID="listaCoordenador">
                    </ajaxToolkit:ListSearchExtender>
                    <asp:DropDownList ID="listaStatus" runat="server" AppendDataBoundItems="True" 
                        DataTextField="nome" DataValueField="id_status_projeto">
                    </asp:DropDownList>

                    <asp:DropDownList ID="listaNatProjeto" runat="server" DataTextField="nome"
                        DataValueField="id_nat_projeto" AppendDataBoundItems="True" Style="margin-bottom: 0px"
                        Width="400px">
                    </asp:DropDownList>
                    <ajaxToolkit:ListSearchExtender ID="listaNatProjeto_ListSearchExtender" runat="server"
                        Enabled="True" PromptCssClass="ListSearchExtenderPrompt" PromptText="digite para procurar"
                        QueryPattern="Contains" QueryTimeout="2000" TargetControlID="listaNatProjeto">
                    </ajaxToolkit:ListSearchExtender>

                    <asp:TextBox ID="txtProcura" runat="server" Width="137px"></asp:TextBox>
                    &nbsp;<asp:DropDownList ID="ddlMode" runat="server">
                    </asp:DropDownList>
                    <asp:Label ID="Label2" runat="server" Text="mostrar"></asp:Label>
                    &nbsp;
                    <asp:DropDownList ID="ddlSize" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSize_SelectedIndexChanged">
                        <asp:ListItem Selected="True">50</asp:ListItem>
                        <asp:ListItem>20</asp:ListItem>
                        <asp:ListItem Value="0">todos</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="btSearch" runat="server" CausesValidation="False" OnClick="btProcurar_Click"
                        Text="procurar" />
                    <asp:Button ID="btCodDefinitivo" runat="server" Text="tornar definitivo" OnClick="btCodDefinitivo_Click" />
                    <asp:Label ID="txtEtapa" runat="server" Text="0" Visible="False"></asp:Label>
                    <asp:Button ID="btExportToExcel" runat="server" OnClick="btExportToExcel_Click" Text="exportar para excel"
                        ToolTip="exportar para excel" />
                </div>
                <asp:Panel ID="panelGrid" runat="server">
                    <asp:GridView ID="grid" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                        Width="100%" Caption="lista de projetos" GridLines="None" OnPageIndexChanging="grid_PageIndexChanging"
                        OnRowCreated="grid_RowCreated" OnRowEditing="grid_RowEditing" OnSorting="grid_Sorting"
                        EnableTheming="False" CssClass="mGrid" PagerStyle-CssClass="pgr">
                        <RowStyle BackColor="#dedede" ForeColor="Black" />
                        <AlternatingRowStyle BackColor="White" ForeColor="Black" />
                        <Columns>
                            <asp:CommandField EditText="selecione" ShowEditButton="True">
                                <ItemStyle Width="40px" />
                            </asp:CommandField>
                            <asp:BoundField DataField="id_projeto" HeaderText="id" 
                                SortExpression="id_projeto">
                                <ItemStyle Width="50px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="n° sol" 
                                SortExpression="ProjetoSolicitacao.codigo">
                       
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" 
                                        Text='<%# Bind("ProjetoSolicitacao.HtmlPaginaProjetoSolicitacao") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="codigoa" HeaderText="proj.A" 
                                SortExpression="codigoa">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="50px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="codigo" HeaderText="código definitivo" 
                                SortExpression="codigo">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="50px" HorizontalAlign="Right" Font-Bold="True" 
                                ForeColor="Black" />
                            </asp:BoundField>
                            <asp:BoundField DataField="sigla" HeaderText="sigla" SortExpression="sigla">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="160px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="titulo" HeaderText="titulo" 
                                SortExpression="titulo" >
                            <HeaderStyle Width="350px" />
                            <ItemStyle Width="350px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="coordenador(es)">
                                <ItemTemplate>
                                    <asp:DataList runat="server" ID="dlCoordenadores" DataSource='<%# Bind("Coordenadores") %>'
                                        Style="border-bottom-style: none" BorderStyle="None" BorderWidth="0px">
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("tipo") %>'></asp:Label>
                                            &nbsp;<asp:Label ID="Label1" runat="server" Text='<%# Bind("HtmlPaginaCoordenador") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="financiador(es)">
                                <ItemTemplate>
                                    <asp:DataList runat="server" ID="dlFinanciadores" DataSource='<%# Bind("Financiadores") %>'
                                        BorderStyle="None">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("HtmlPaginaFinanciador") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="status" SortExpression="StatusProjeto.nome">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" 
                                        Text='<%# Bind("StatusProjeto.nome") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("StatusProjeto.nome") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle HorizontalAlign="Left" />
                        <PagerStyle CssClass="pgr" />
                    </asp:GridView>
                    <%--<asp:Panel ID="panelTipo" runat="server">
                    <table id="tableTipo" class="cadastro">
                        <tr>
                            <th colspan="2">
                                cadastro de projeto
                            </th>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                selecione o tipo de cadastro:
                            </td>
                            <td class="direito">
                            </td>
                        </tr>
                    </table>
                </asp:Panel>--%>
                </asp:Panel>
                <asp:Panel ID="panelCadastro" runat="server">
                    <table class="mCad" width="100%">
                        <tr>
                            <th class="esquerdo">
                                código projeto :
                                <asp:Label ID="txtCodigo" runat="server" Text="0" Visible="False"></asp:Label>
                            </th>
                            <td class="direito">
                                <asp:Label ID="lbCodDef" runat="server" Text="0"></asp:Label>
                                <asp:Label ID="lblCodSub" runat="server" Text=" sub projeto: "></asp:Label>
                                &nbsp;<asp:Label ID="lbSubProjeto" runat="server" Text="00"></asp:Label>
                            </td>
                            <th class="esquerdo">
                                sigla do projeto:
                            </th>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoSigla" runat="server" EnableValidator="True" MaxLength="50"
                                    Width="300" ValidationGroup="vgIdentificacao" />
                            </td>
                        </tr>
                        <tr>
                            <th class="esquerdo">
                                data abertura projeto:
                            </th>
                            <td class="direito">
                                <cdt:cData ID="cDataAberturaDef" runat="server" ValidationGroup="vgIdentificacao" />
                            </td>
                            <th class="esquerdo">
                                solicitação de abertura:
                            </th>
                            <td class="direito">
                                <cDdlProjetoSolicitacao:cDdlProjetoSolicitacao ID="cDdlProjetoSolicitacao1" runat="server"
                                    Enable="false" />
                            </td>
                        </tr>
                        <tr>
                            <th class="esquerdo">
                                código do projeto A:
                            </th>
                            <td class="direito">
                                <asp:Label ID="lblCodigoA" runat="server" Text="0"></asp:Label>
                                <asp:Label ID="lblSubA" runat="server" Text=" sub projeto A: "></asp:Label>
                                <asp:Label ID="lblSubProjA" runat="server" Text="00"></asp:Label>
                            </td>
                            <th class="esquerdo">
                                data abertura projeto A:
                            </th>
                            <td class="direito">
                                <cdt:cData ID="cDataAberturaA" runat="server" ValidationGroup="vgIdentificacao" Enabled="False" />
                            </td>
                        </tr>
                        <tr>
                            <th class="esquerdo">
                                data do início:
                            </th>
                            <td class="direito">
                                <cdt:cData ID="cDataInicio" runat="server" ValidationGroup="vgIdentificacao" />
                            </td>
                            <th class="esquerdo">
                                data do término:
                            </th>
                            <td>
                                <cdt:cData ID="cDataTermino" runat="server" ValidationGroup="vgIdentificacao" />
                            </td>
                        </tr>
                        <tr>
                            <th class="esquerdo">
                                moeda:
                            </th>
                            <td>
                                <cddlMoeda:cDdlMoeda ID="cDdlMoeda1" runat="server" ValidationGroup="vgIdentificacao" />
                                <br />
                            </td>
                            <th class="esquerdo">
                                valor:
                            </th>
                            <td class="direito">
                                <cvl:cValor ID="cValorProjeto" runat="server" ValidationGroup="vgIdentificacao" />
                            </td>
                        </tr>
                           <tr>
                            <th class="esquerdo">
                                instrumento contratual:
                            </th>
                            <td class="direito" colspan="3">
                               
                                
                                <cddlInstrumentoContratual:cDdlInstrumentoContratual ID="cDdlInstrumentoContratual1" 
                                    runat="server" ValidationGroup="vgIdentificacao" />
                                    <ctx:cTexto ID="cTextoInstrumentoContratual" runat="server" MaxLength="50" 
                                    EnableValidator="False" Width="200px"/>
                               
                            </td>
                        </tr>
                        <tr>
                            <th class="esquerdo">
                                status:
                            </th>
                            <td class="direito" colspan="3">
                                <asp:Label ID="lbStatus" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                           <div id="dGravacao" runat="server">
                                <asp:Button ID="btAlterar8" runat="server" Text="salvar" CausesValidation="False"
                                    OnClick="btAtualizarProjA_Click" />
                                    </div>
                            </td>
                        </tr>
                    </table>
                    <div id="dMsg" runat="server">
                    </div>
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" />
                    <br />
                    <div id="abas">
                        <ul>
                            <li><a href="#aba-1">identificação</a></li>
                            <li><a href="#aba-2">descritivo</a></li>
                            <li><a href="#aba-3">endereços/contatos</a></li>
                            <li><a href="#aba-4">documentos</a> </li>
                            <li><a href="#aba-5">taxas</a></li>
                            <li><a href="#aba-6">setores responsáveis</a></li>
                            <li><a href="#aba-7">cartão de assinatura</a></li>
                            <li><a href="#aba-8">Anexos</a></li>
                            <li><a href="#aba-9">status</a></li>
                            <li><a href="#aba-10">opção de adiantamento</a></li>                         
                            <li><a href="#aba-11">versão para impressão</a></li>
                        </ul>
                        <div id="aba-1">
                            <table>
                                <tr>
                                    <td class="esquerdo">
                                        unidade:
                                    </td>
                                    <td class="direito">
                                        <asp:DropDownList ID="listaUnidade" runat="server" DataTextField="nome" DataValueField="id_unidade"
                                            AppendDataBoundItems="True" Width="500px" AutoPostBack="True" 
                                            OnSelectedIndexChanged="listaUnidade_SelectedIndexChanged" 
                                            ValidationGroup="vgIdentificacao">
                                        </asp:DropDownList>
                                        <ajaxToolkit:ListSearchExtender ID="listaUnidade_ListSearchExtender" runat="server"
                                            Enabled="True" PromptCssClass="ListSearchExtenderPrompt" PromptText="digite para procurar"
                                            QueryPattern="Contains" QueryTimeout="2000" TargetControlID="listaUnidade">
                                        </ajaxToolkit:ListSearchExtender>
                                        <asp:CompareValidator ID="cvUnidade" runat="server" ErrorMessage="selecione uma unidade..."
                                            ForeColor="Red" Operator="NotEqual" ValueToCompare="0" ControlToValidate="listaUnidade"></asp:CompareValidator>
                                </tr>
                                <tr>
                                    <td class="esquerdo">
                                        departamento:
                                    </td>
                                    <td class="direito">
                                        <asp:DropDownList ID="listaDepto" runat="server" AppendDataBoundItems="True" DataTextField="nome"
                                            DataValueField="id_departamento" Width="500px" AutoPostBack="True" OnSelectedIndexChanged="listaDepto_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <ajaxToolkit:ListSearchExtender ID="listaDepto_ListSearchExtender" runat="server"
                                            Enabled="True" PromptCssClass="ListSearchExtenderPrompt" PromptText="digite para procurar"
                                            QueryPattern="Contains" QueryTimeout="2000" TargetControlID="listaDepto">
                                        </ajaxToolkit:ListSearchExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="esquerdo">
                                        laboratorio:
                                    </td>
                                    <td class="direito">
                                        <asp:DropDownList ID="listaLab" runat="server" AppendDataBoundItems="True" DataTextField="nome"
                                            DataValueField="id_laboratorio" Width="500px">
                                        </asp:DropDownList>
                                        <ajaxToolkit:ListSearchExtender ID="listaLab_ListSearchExtender" runat="server" Enabled="True"
                                            PromptCssClass="ListSearchExtenderPrompt" PromptText="digite para procurar" QueryPattern="Contains"
                                            QueryTimeout="2000" TargetControlID="listaLab">
                                        </ajaxToolkit:ListSearchExtender>
                                    </td>
                                </tr>
                            </table>
                            <uc2:ProjetoCoordenadores ID="ProjetoCoordenadores1" runat="server" />
                            <uc3:ProjetoFinanciadores ID="ProjetoFinanciadores1" runat="server" />
                            <%--<asp:Button ID="btAtualizar1" runat="server" Text="salvar" 
                                CausesValidation="False" onclick="btAtualizarProjA_Click" />--%>
                        </div>
                        <div id="aba-2">
                            <table class="mGrid" width="50%">
                                <tr>
                                    <th colspan="2">
                                        &nbsp
                                    </th>
                                </tr>
                                <tr>
                                    <td class="esquerdo">
                                        título:
                                    </td>
                                    <td class="direito">
                                        <ctx:cTexto ID="cTextoTitulo" runat="server" Height="100" Width="500" 
                                            TextMode="MultiLine" ValidationGroup="vgIdentificacao" 
                                            CausesValidation="False" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="esquerdo">
                                        objetivo:
                                    </td>
                                    <td class="direito">
                                        <ctx:cTexto ID="cTextoObjetivo" runat="server" EnableValidator="True" Width="500"
                                            Height="100" TextMode="MultiLine" ValidationGroup="vgIdentificacao" 
                                            CausesValidation="False" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="esquerdo">
                                        principal área de atuação:
                                    </td>
                                    <td class="direito">
                                        <cddlAtuacao:cDdlAtuacao ID="cDdlAtuacao1" runat="server"    EnableValidator="false" />
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="esquerdo">
                                        natureza do projeto:
                                    </td>
                                    <td class="direito">
                                        <cddlNaturezaProjeto:cDdlNaturezaProjeto ID="cDdlNaturezaProjeto" runat="server" EnableValidator="false" />
                                    </td>
                                </tr>
                             <%--   <tr style="display:none">
                                    <td class="esquerdo">
                                        classificação do projeto (recursos recebidos):
                                    </td>
                                    <td class="direito">
                                        <cddlClassificacao:cDdlClassificacao ID="cDdlClassificacao1" runat="server" />
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td class="esquerdo">
                                        nome do programa:
                                    </td>
                                    <td class="direito">
                                        <ctx:cTexto ID="cTextoPrograma" runat="server" Width="300" ErrorMsg="digite o programa"
                                            EnableValidator="False" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="esquerdo">
                                        classificação FUSP:</td>
                                    <td class="direito">
                                        <cDdlClassificacaoFusp:cDdlClassificacaoFusp ID="cDdlClassificacaoFusp1" 
                                            runat="server" ValidationGroup="classificacaoFusp" />
                                    </td>
                                </tr>
                            </table>
                            <%--<asp:Button ID="btAtualizar2" runat="server" Text="salvar" 
                                CausesValidation="False" onclick="btAtualizarProjA_Click" />--%>
                        </div>
                        <div id="aba-3">
                            <uc4:ControleEnderecoProjeto ID="ControleEnderecoProjeto1" runat="server" OnSelectedIndexChanged="rbTipoEnderecoCorrespondencia_SelectedIndexChanged" />
                            <uc8:ControleContatoProjeto ID="ControleContatoProjeto1" runat="server" />
                        </div>
                        <div id="aba-4">
                            <uc5:ControleProjetoDocumento ID="ControleProjetoDocumento1" runat="server" />
                        </div>
                        <div id="aba-5">
                            <table class="mGrid" width="50%">
                                <tr>
                                    <th colspan="2">
                                    financeiro </td>
                                </tr>
                                <tr>
                                    <td class="esquerdo">
                                        data primeiro recebimento:
                                    </td>
                                    <td class="direito">
                                        <cdt:cData ID="cData_1_recebto" runat="server" ValidationGroup="vgIdentificacao" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="esquerdo">
                                        valor do primeiro recebimento:
                                    </td>
                                    <td class="direito">
                                        <cvl:cValor ID="cValor_1_recebto" runat="server" ValidationGroup="vgIdentificacao" />
                                    </td>
                                </tr>
                            </table>
                            <uc6:ControleTaxaProjeto ID="ControleTaxaProjeto1" runat="server" />
                            <%--<asp:Button ID="btAtualizar5" runat="server" Text="salvar" 
                                onclick="btAtualizarProjA_Click" />--%>
                        </div>
                        <div id="aba-6">
                            <uc9:ControleSetorResponsavel ID="ControleSetorResponsavel1" runat="server" />
                        </div>
                        <div id="aba-7">
                            <uc10:ControleAssinProjeto ID="ControleAssinProjeto1" runat="server" />
                        </div>

                        <div id="aba-8">
                            
                            <uc14:ControleArquivoAnexo ID="ControleArquivoAnexo1" runat="server" />
                            
                        </div>

                        <div id="aba-9">
                            <uc7:ControleHistoricoProjeto ID="ControleHistoricoProjeto1" runat="server" />
                            <uc13:ControleRequisitosEncerramento ID="ControleRequisitosEncerramento1" runat="server" />
                        </div>
                        <div id="aba-10">
                            <uc12:ControleOpcaoAdiantamento ID="ControleOpcaoAdiantamento1" 
                                runat="server" />
                            
                        </div>
                        
                        <div id="aba-11">
                                                     
                    <div id="dRelatorio" runat="server"></div>
                       <asp:Button ID="btImprimir0" runat="server" CausesValidation="False" Width="0px" Height="0px" onclick="btImprimir_Click"  />      
                        </div>
                    </div>
                    <div id="dGravacao1" runat="server">
                    <asp:Button ID="btAtualizar5" runat="server" Text="salvar" OnClick="btAtualizarProjA_Click"
                        CausesValidation="false" />
                        </div>
                </asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btExportToExcel" />
            </Triggers>
        </asp:UpdatePanel>
        <ajaxToolkit:UpdatePanelAnimationExtender ID="upae" BehaviorID="animation" runat="server"
            TargetControlID="UpdatePanel1" Enabled="True">
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
        </ajaxToolkit:UpdatePanelAnimationExtender>
    </div>
</asp:Content>
