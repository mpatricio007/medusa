<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.Master" AutoEventWireup="true"
    CodeBehind="ProjetoSolicitacoes.aspx.cs" Inherits="Medusa.Sistemas.SCP.ProjetoSolicitacoes" %>



<%@ Register Src="../../Controles/DdlStatusSolicitacao.ascx" TagName="DdlStatusSolicitacao"
    TagPrefix="uc1" %>
<%@ Register Src="ControleHistorico.ascx" TagName="ControleHistorico" TagPrefix="uc2" %>
<%@ Register Src="../../Controles/DdlTipoSolicitacao.ascx" TagName="DdlTipoSolicitacao"
    TagPrefix="uc3" %>
<%@ Register Src="../../Controles/DdlProjetoSolicitacao.ascx" TagName="DdlProjetoSolicitacao"
    TagPrefix="uc4" %>

<%@ Register src="~/Sistemas/SCP/ControleTornarPA.ascx" tagname="ControleTornarPA" tagprefix="uc5" %>





<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
            $(function () {
                $(".txtCoordenador").autocomplete({
                    source: function (request, response) {
                        $.getJSON("../../MedusaService.svc/GetCoordenadores?name='" + request.term + "'", function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item,
                                    value: item
                                }
                            }));
                        })
                    },
                    minLength: 2,
                    open: function () {
                        $(this).removeClass("ui-corner-all").addClass("ui-corner-top");
                    },
                    close: function () {
                        $(this).removeClass("ui-corner-top").addClass("ui-corner-all");
                    }
                });
            });

            $(function () {
                $("#<%=txtFinanciador.ClientID %>").autocomplete({
                    source: function (request, response) {
                        $.getJSON("../../MedusaService.svc/GetFinanciadores?name='" + request.term + "'", function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item,
                                    value: item
                                }
                            }));
                        })//teste
                    },
                    minLength: 2,
                    open: function () {
                        $(this).removeClass("ui-corner-all").addClass("ui-corner-top");
                    },
                    close: function () {
                        $(this).removeClass("ui-corner-top").addClass("ui-corner-all");
                    }
                });
            });
        }

        function openDialog() {
            Page_ClientValidate('');
            if (Page_IsValid) {
                var dlg = $("#dCodigoA").dialog({
                    width: 400,
                    height: 230,
                    position: 'top',
                    modal: true,
                    resizable: false,
                    draggable: false,
                    title: 'Atenção'
                });

                dlg.parent().appendTo(jQuery("form:first"));
                $("#dCodigoA").dialog("open");
            }
        }

        function closeDialog() {
            $("#dCodigoA").dialog("close");
        }

      
    </script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="conteudo">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
               <div id="dCodigoA" style="display: none">
            <asp:UpdatePanel ID="upPa" runat="server">
                <ContentTemplate>
                    Ao tornar esta proposta projeto A
                    <br />
                    será impossível estorná-lo para proposta novamente!<br />
                    Se você tem realmente <b>certeza</b> de que deseja
                    <br />
                    torna-lo projeto A clique em "continuar". Ou clique em
                    <br />
                    "cancelar" para cancelar esta ação.<br /><br />
                    <table>
                    <tr>
                            <td>
                                projeto: <cint:cInteiro ID="cIntCodA" runat="server" ValidationGroup="1" MaxLength="8"/>
                            </td>
                            <td>
                                sub projeto: <ctx:cTexto ID="cTextoSubProjA" runat="server" MaxLength="2" 
                                         ValidationGroup="vgIdentificacao" Width="20"/>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btGerarProjA" runat="server" OnClick="btGerarProjA_Click" Text="continuar" />
                                <asp:Button ID="BtCancel" runat="server" Text="cancelar" OnClientClick="$('#dCodigoA').dialog('close');"
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
                    OnDeleteCommand="DataListFiltros_DeleteCommand" Width="800px">
                    <ItemTemplate>
                        <div class="FilterName">
                            <%# Eval("property_name") %>&nbsp
                            <%# Eval("mode_name")%>&nbsp
                            <%# Eval("value")%>
                            &nbsp
                            <asp:ImageButton ID="btExcluiFiltro" runat="server" ImageUrl="~/Styles/img/bt_delete.jpg"
                                Width="15px" Height="15px" CommandName="delete" />
                        </div>
                    </ItemTemplate>
                </asp:DataList>
                <div class="pesquisar">
                    <div id="updateProgressDiv" style="display: none; position: absolute;">
                        <div style="margin-left: 780px; float: left">
                            <img src="../../Styles/img/loading.gif" />
                            <span style="margin: 3px">Carregando ...</span></div>
                    </div>
                    <asp:Label ID="Label1" runat="server" Text="procurar"></asp:Label>
                    &nbsp;<asp:DropDownList ID="ddlOptions" runat="server" CausesValidation="True" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlOptions_SelectedIndexChanged">
                        <asp:ListItem Value="codigo">nº solicitação</asp:ListItem>
                        <asp:ListItem Value="titulo">título</asp:ListItem>
                        <asp:ListItem Value="strCoordenador">coordenador</asp:ListItem>
                        <asp:ListItem Value="strFinanciador">financiador</asp:ListItem>
                        <asp:ListItem Value="data_abertura">data da solicitação</asp:ListItem>
                        <asp:ListItem Value="StatusSolicitacao.descricao">status</asp:ListItem>
                        <asp:ListItem Value="Usuario.PessoaFisica.nome">solicitante</asp:ListItem>
                        <asp:ListItem Value="TipoSolicitacao.nome">tipo de solicitação</asp:ListItem>
                        <asp:ListItem Value="Projeto.codigo">projeto</asp:ListItem>
                        <asp:ListItem Value="Projeto.codigoa">projeto A</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtProcura" runat="server" Width="137px"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlMode" runat="server">
                    </asp:DropDownList>
                    <asp:Label ID="Label2" runat="server" Text="mostrar"></asp:Label>
                    &nbsp;
                    <asp:DropDownList ID="ddlSize" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSize_SelectedIndexChanged">
                        <asp:ListItem Selected="True">10</asp:ListItem>
                        <asp:ListItem>20</asp:ListItem>
                        <asp:ListItem>50</asp:ListItem>
                        <asp:ListItem Value="0">todos</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="btSearch" runat="server" CausesValidation="False" OnClick="btProcurar_Click"
                        Text="procurar" />
                    <asp:Button ID="btCriar" runat="server" CausesValidation="False" OnClick="btCriar_Click"
                        Text="novo" Width="80px" />
                </div>
                <asp:Panel ID="panelGrid" runat="server">
                    <asp:GridView ID="grid" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                        Caption="Lista de Solicitações de Projeto" CssClass="tableView" OnPageIndexChanging="grid_PageIndexChanging"
                        OnRowCreated="grid_RowCreated" OnRowEditing="grid_RowEditing" OnSorting="grid_Sorting"
                        Width="100%">
                        <Columns>
                            <asp:BoundField DataField="codigo" HeaderText="nº " SortExpression="codigo">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="titulo" HeaderText="titulo" SortExpression="titulo">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="strCoordenador" HeaderText="coordenador" SortExpression="strCoordenador">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="200px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="strFinanciador" HeaderText="financiador" SortExpression="strFinanciador">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="data_solicitacao" HeaderText="data" SortExpression="data_solicitacao"
                                DataFormatString="{0:d}">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="status" SortExpression="StatusSolicitacao.descricao">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("StatusSolicitacao.descricao") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("StatusSolicitacao.descricao") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="tipo" SortExpression="TipoSolicitacao.nome">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("TipoSolicitacao.nome") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("TipoSolicitacao.nome") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="solicitante" SortExpression="Usuario.PessoaFisica.nome">
                                
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("Usuario.PessoaFisica.nome") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="projeto" SortExpression="Projeto.codigo">
                               
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("Projeto.HtmlPaginaProjeto") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="projeto A" SortExpression="Projeto.codigoa">
                             
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("Projeto.HtmlPaginaProjetoA") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField EditText="selecionar" ShowEditButton="True">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:CommandField>
                        </Columns>
                        <HeaderStyle HorizontalAlign="Left" />
                        <SortedAscendingCellStyle CssClass="SortedAscendingCellStyle" />
                        <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle" />
                        <SortedDescendingCellStyle CssClass="SortedDescendingCellStyle" />
                        <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle " />
                    </asp:GridView>
                </asp:Panel>
                <asp:Panel ID="panelCadastro" runat="server">
                    <table class="cadastro">
                        <tr>
                            <th colspan="1">
                                cadastro de solicitação de projeto
                            </th>
                            <th colspan="1" >
                                <div id="dGravacao" runat="server">
                                    <asp:Button ID="btInserir" runat="server" OnClick="btInserir_Click" Text="inserir" />
                                    <asp:Button ID="btAlterar" runat="server" OnClick="btAlterar_Click" Text="salvar" />
                                    <asp:Button ID="btExcluir" runat="server" CausesValidation="False" OnClick="btExcluir_Click"
                                        Text="excluir" />
                                    <asp:Button ID="btCancelar" runat="server" CausesValidation="False" OnClick="btCancelar_Click"
                                        Text="cancelar" />
                                </div>
                            </th>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                tipo de solicitação:
                            </td>
                            <td class="direito">
                                 <asp:DropDownList ID="cDdlTipoSolicitacao" runat="server" DataTextField="nome" 
                    DataValueField="id_tipo_solicitacao" 
    AppendDataBoundItems="True" 
   onselectedindexchanged="cDdlTipoSolicitacao_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList>





<asp:CompareValidator ID="cvTipoSolicitacao" runat="server" 
    ErrorMessage="selecione um tipo de solicitação..." ForeColor="Red" Operator="NotEqual" 
    ValueToCompare="0" ControlToValidate="cDdlTipoSolicitacao"></asp:CompareValidator>

</td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                solicitante:
                            </td>
                            <td class="direito">
                                <asp:Label ID="txtSolicitante" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                numero da solicitação:
                            </td>
                            <td class="direito">
                                <asp:Label ID="txtPx" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                título:
                            </td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoTitulo" runat="server" TextMode="MultiLine"
                                    Height="80px" Width="500px" MaxLength="500"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                descrição:
                                <asp:Label ID="txtCodigo" runat="server" Text="0" Visible="False"></asp:Label>
                            </td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoDescricao" runat="server" EnableValidator="True" TextMode="MultiLine"
                                    Height="80px" Width="500px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                coordenador:
                            </td>
                            <td class="direito">
                                <asp:TextBox ID="txtCoordenador" runat="server" MaxLength="100" Width="500px" class="txtCoordenador"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvCoordenador" runat="server" ControlToValidate="txtCoordenador"
                                    ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                sub coordenador:
                            </td>
                            <td class="direito">
                                <asp:TextBox ID="txtSubCoordenador" runat="server" MaxLength="100" Width="500px"
                                    class="txtCoordenador" ValidationGroup="solicitacao"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCoordenador"
                                    ErrorMessage="RequiredFieldValidator" ValidationGroup="solicitacao"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                financiador:
                            </td>
                            <td class="direito">
                                <asp:TextBox ID="txtFinanciador" runat="server" MaxLength="100" Width="500px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rftFinanciador" runat="server" ControlToValidate="txtFinanciador"
                                    ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                data de abertura:
                            </td>
                            <td class="direito">
                                <asp:Label ID="txtDataAbertura" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                observação:
                            </td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoObs" runat="server" EnableValidator="False" Height="80px" MaxLength="100"
                                    Width="500px" TextMode="MultiLine" />
                            </td>
                        </tr>
                         <asp:Panel ID="pProposta" runat="server" >
                      
                        <tr>
                            <td class="esquerdo">
                                unidade:
                            </td>
                            <td class="direito">
                                <asp:DropDownList ID="listaUnidade" runat="server" DataTextField="nome" DataValueField="id_unidade"
                                    AppendDataBoundItems="True" Width="500px" AutoPostBack="True" OnSelectedIndexChanged="listaUnidade_SelectedIndexChanged">
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
                        <tr>
                            <td class="esquerdo">
                                valor global:
                            </td>
                            <td class="direito">
                                <cddlMoeda:cDdlMoeda ID="cDdlMoeda2" runat="server" />
                                <cvl:cValor ID="cValor" runat="server" MaxLength="20" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                inicio:
                            </td>
                            <td class="direito">
                                <cdt:cData ID="cDataInicio" runat="server" ValidationGroup="false" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                termino:
                            </td>
                            <td class="direito">
                                <cdt:cData ID="cDataTermino" runat="server" ValidationGroup="solicitacao" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                instrumento contratual:
                            </td>
                            <td class="direito">
                                <cddlInstrumentoContratual:cDdlInstrumentoContratual ID="cDdlInstrumentoContratual"
                                    runat="server" ValidationGroup="false" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                instrumento contratual nº:
                            </td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoContrPatr" runat="server" MaxLength="50" ValidationGroup="false"/>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                </asp:Panel>
                     
                <tr>
                    <td>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">
                        <div id="dGravacao1" runat="server">
                            <asp:Button ID="btInserir0" runat="server" OnClick="btInserir_Click" Text="inserir" />
                            <asp:Button ID="btAlterar0" runat="server" OnClick="btAlterar_Click" Text="salvar" />
                            <asp:Button ID="btExcluir0" runat="server" CausesValidation="False" OnClick="btExcluir_Click"
                                Text="excluir" />
                            <asp:Button ID="btCancelar0" runat="server" CausesValidation="False" OnClick="btCancelar_Click"
                                Text="cancelar" />
                            <uc5:ControleTornarPA ID="ControleTornarPA1" runat="server" Click="btTornarA_Click"/>
                            
                        </div>
                        <asp:Button ID="btImprimir" runat="server" CausesValidation="False" 
                                OnClick="btImprimir_Click" Text="imprimir" />
                    </th>
                        
                </tr>
                <tr>
                    <td colspan="2">
                        <uc2:ControleHistorico ID="ControleHistorico1" runat="server" />
                    </td>
                </tr>
                </table>
                    
                     
                 </asp:Panel>
                <asp:Panel ID="pImprimir" runat="server">
                    <rsweb:ReportViewer ID="ReportViewer2" runat="server"></rsweb:ReportViewer>
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt"
                        InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt"
                        Width="900px">
                        <LocalReport ReportPath="Relatorios\Projeto\RSolicitacao.rdlc">
                        </LocalReport>
                    </rsweb:ReportViewer>
                </asp:Panel>
            </ContentTemplate>
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
