<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.Master" AutoEventWireup="true" CodeBehind="Entradas.aspx.cs" Inherits="Medusa.Sistemas.SREC.Entradas" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../Controles/ListaPessoaTelefones.ascx" TagName="ListaPessoaTelefones" TagPrefix="uc1" %>
<%@ Register Src="../../Controles/DdlProjetoA.ascx" TagName="DdlProjetoA" TagPrefix="uc2" %>
<%@ Register Src="ControleHistoricoEntrada.ascx" TagName="ControleHistoricoEntrada" TagPrefix="uc4" %>
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
                $("#<%=txtDocumento.ClientID %>").autocomplete({
                    source: function (request, response) { return GetTipoDocEntradaData(request, response) },
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

        function GetTipoDocEntradaData(request, response) {
            var ajaxRequest = $.ajax("../../MedusaService.svc/GetTipoDocEntrada?name='" + request.term + "'", {
                dataType: "json", // this tells jquery that you are expcting a json object and so it will parse the data for you.
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Accept", "application/json;odata=verbose");
                    xhr.setRequestHeader("MaxDataServiceVersion", "3.0");
                },
                success: function (data) {
                    response($.map(data.d, function (item) {
                        return {
                            label: item.toString(),
                            value: item.toString()
                        }
                    }))
                }
            });
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="conteudo">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:CheckBox ID="ckFilter" runat="server" AutoPostBack="True"
                    OnCheckedChanged="ckFilter_CheckedChanged"
                    Text="habilitar múltiplos filtros" />
                <asp:DataList ID="DataListFiltros" runat="server" RepeatColumns="6"
                    OnDataBinding="DataListFiltros_DataBinding" RepeatDirection="Horizontal"
                    OnDeleteCommand="DataListFiltros_DeleteCommand">
                    <ItemTemplate>

                        <div class="FilterName">

                            <%# Eval("property_name") %>&nbsp
                <%# Eval("mode_name")%>&nbsp
                <%# Eval("value")%> &nbsp 
                   <asp:ImageButton ID="btExcluiFiltro" runat="server"
                       ImageUrl="~/Styles/img/bt_delete.jpg" Width="15px" Height="15px" CommandName="delete" />
                        </div>
                    </ItemTemplate>
                </asp:DataList>
                <div class="pesquisar">
                    ano
                    <asp:DropDownList ID="ddlAno" runat="server" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlAno_SelectedIndexChanged">
                    </asp:DropDownList>

                    &nbsp;<asp:Label ID="Label1" runat="server" Text="procurar"></asp:Label>
                    &nbsp;<asp:DropDownList ID="ddlOptions" runat="server" CausesValidation="True"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlOptions_SelectedIndexChanged">
                        <asp:ListItem Value="nprotent">protocolo</asp:ListItem>
                        <asp:ListItem Value="dataprot">data</asp:ListItem>
                        <asp:ListItem Value="codproj">projeto</asp:ListItem>
                        <asp:ListItem Value="codproja">projeto A</asp:ListItem>
                        <asp:ListItem Value="tipodocent">tipo do documento</asp:ListItem>
                        <asp:ListItem Value="numdocent">numero</asp:ListItem>
                        <asp:ListItem Value="valorent">valor</asp:ListItem>
                        <asp:ListItem Value="enviadoent">enviado por</asp:ListItem>
                        <asp:ListItem Value="descrent">descrição</asp:ListItem>
                        <asp:ListItem Value="UsuarioPara.PessoaFisica.nome">encaminhado para</asp:ListItem>
                        <asp:ListItem Value="UsuarioEntrada.PessoaFisica.nome">usuario</asp:ListItem>
                        <asp:ListItem Value="saida.nprotsai">protocolo de saida</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtProcura" runat="server" Width="137px"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlMode" runat="server">
                    </asp:DropDownList>
                    <asp:Label ID="Label2" runat="server" Text="mostrar"></asp:Label>
                    &nbsp;
                    <asp:DropDownList ID="ddlSize" runat="server" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlSize_SelectedIndexChanged">
                        <asp:ListItem Selected="True">10</asp:ListItem>
                        <asp:ListItem>20</asp:ListItem>
                        <asp:ListItem>50</asp:ListItem>
                        <asp:ListItem Value="0">todos</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="btSearch" runat="server" CausesValidation="False"
                        OnClick="btProcurar_Click" Text="procurar" />
                    <asp:Button ID="btCriar" runat="server" CausesValidation="False"
                        OnClick="btCriar_Click" Text="novo" Width="80px" />
                    <asp:ImageButton ID="btImprimirSelecionados" runat="server"
                        ImageUrl="~/Styles/img/print.gif" OnClick="btImprimirSelecionados_Click"
                        ToolTip="imprimir" />
                </div>
                <asp:Panel ID="panelGrid" runat="server">


                    <asp:GridView ID="grid" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" Caption="Entrada de Documentos"
                        CssClass="tableView"
                        OnPageIndexChanging="grid_PageIndexChanging" OnRowCreated="grid_RowCreated"
                        OnRowEditing="grid_RowEditing" OnSorting="grid_Sorting" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="nprotent" HeaderText="protocolo"
                                SortExpression="nprotent">
                                <HeaderStyle />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="dataprot" HeaderText="data"
                                SortExpression="dataprot" DataFormatString="{0:d}">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="codproj" HeaderText="projeto"
                                SortExpression="codproj">
                                <HeaderStyle />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="codproja" HeaderText="projeto A"
                                SortExpression="codproja">
                                <HeaderStyle />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="tipodocent" HeaderText="tipo do documento"
                                SortExpression="tipodocent" />
                            <asp:BoundField DataField="numdocent" HeaderText="número"
                                SortExpression="numdocent" />
                            <asp:BoundField DataField="valorent" HeaderText="valor"
                                SortExpression="valorent" DataFormatString="{0:N2}" />
                            <asp:BoundField DataField="enviadoent" HeaderText="enviado por"
                                SortExpression="enviadoent" />
                            <asp:BoundField DataField="descrent" HeaderText="descrição"
                                SortExpression="descrent" />
                            <asp:TemplateField HeaderText="encaminhado para"
                                SortExpression="UsuarioPara.login">

                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server"
                                        Text='<%# Bind("UsuarioPara.login") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="usuario"
                                SortExpression="UsuarioEntrada.PessoaFisica.nome">

                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server"
                                        Text='<%# Bind("UsuarioEntrada.login") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField EditText="selecionar" ShowEditButton="True">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:CommandField>
                        </Columns>
                        <HeaderStyle HorizontalAlign="Left" />
                        <SortedAscendingCellStyle
                            CssClass="SortedAscendingCellStyle" />
                        <SortedAscendingHeaderStyle
                            CssClass="SortedAscendingHeaderStyle" />
                        <SortedDescendingCellStyle
                            CssClass="SortedDescendingCellStyle" />
                        <SortedDescendingHeaderStyle
                            CssClass="SortedDescendingHeaderStyle " />
                    </asp:GridView>
                </asp:Panel>
                <asp:Panel ID="panelCadastro" runat="server">
                    <table class="cadastro">
                        <tr>
                            <th colspan="1">cadastro de entradas</th>
                            <th colspan="1">
                                <div id="dGravacao" runat="server">
                                    <asp:Button ID="btInserir" runat="server" OnClick="btInserir_Click"
                                        Text="inserir" />
                                    <asp:Button ID="btAlterar" runat="server" OnClick="btAlterar_Click"
                                        Text="salvar" />
                                    <asp:Button ID="btExcluir" runat="server" CausesValidation="False"
                                        OnClick="btExcluir_Click" Text="excluir" />
                                    <asp:Button ID="btCancelar" runat="server" CausesValidation="False"
                                        OnClick="btCancelar_Click" Text="cancelar" />
                                </div>
                            </th>
                        </tr>
                        <tr>
                            <td class="esquerdo">protocolo:<asp:Label ID="txtCodigo" runat="server" Text="0" Visible="False"></asp:Label>
                            </td>
                            <td class="direito">
                                <cint:cInteiro ID="cInteiroProtocolo" runat="server" EnableValidator="True"
                                    Visible="True" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">data do protocolo de entrada:</td>
                            <td class="direito">
                                <cdt:cData ID="cDataProtocoloEnt" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo" style="height: 24px">projeto:
                            </td>
                            <td class="direito" style="height: 24px">
                                <cint:cInteiro ID="cInteiroProjeto" runat="server" EnableValidator="False" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo" style="height: 25px">projeto A:</td>
                            <td class="direito" style="height: 25px">
                                <cint:cInteiro ID="cInteiroProjA" runat="server" EnableValidator="False" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">tipo documento:</td>
                            <td class="direito">
                                <asp:TextBox ID="txtDocumento" runat="server" MaxLength="50" Width="300px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvTextoDocumento" runat="server"
                                    ControlToValidate="txtDocumento" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">número:</td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoNumero" runat="server" MaxLength="30"
                                    EnableValidator="False" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo" style="height: 105px">valor:</td>
                            <td class="direito" style="height: 105px">
                                <cddlMoeda:cDdlMoeda ID="cDdlMoeda1" runat="server" />
                                <cvl:cValor ID="cValor1" runat="server" EnableValidator="False" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">enviado por:</td>
                            <td class="direito">
                                <asp:TextBox ID="txtEnviadoPor" runat="server" Width="500px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">descrição:</td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoDescricao" runat="server" Height="50px"
                                    Width="500px" TextMode="MultiLine" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo" style="height: 33px">encaminhado para:</td>
                            <td class="direito" style="height: 33px">
                                <cDdlUsuarioFUSP:cDdlUsuarioFUSP ID="cDdlUsuarioFUSP" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">obs:</td>
                            <td class="direito">
                                <asp:TextBox ID="txtObs" runat="server" Height="50px" TextMode="MultiLine"
                                    Width="500px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th colspan="2">
                                <div id="dGravacao1" runat="server">
                                    <asp:Button ID="btInserir0" runat="server" OnClick="btInserir_Click"
                                        Text="inserir" />
                                    <asp:Button ID="btAlterar0" runat="server" OnClick="btAlterar_Click"
                                        Text="salvar" />
                                    <asp:Button ID="btExcluir0" runat="server" CausesValidation="False"
                                        OnClick="btExcluir_Click" Text="excluir" />
                                    <asp:Button ID="btCancelar0" runat="server" CausesValidation="False"
                                        OnClick="btCancelar_Click" Text="cancelar" />
                                </div>
                            </th>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pSaida" runat="server">
                    <table class="cadastro">
                        <tr>
                            <th colspan="1">cadastro de saidas</th>
                            <th colspan="1"></th>
                        </tr>
                        <tr>
                            <td class="esquerdo">nº protocolo de saída:<asp:Label ID="txtCodigoSaida" runat="server" Text="0"
                                Visible="False"></asp:Label>
                            </td>
                            <td class="direito" style="height: 33px">
                                <cint:cInteiro ID="cTextoProtSaida" runat="server" ValidationGroup="saida" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">data da saída:</td>
                            <td class="direito">
                                <cdt:cData ID="cTextoDataSai" runat="server" ValidationGroup="saida" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">observação de saída:</td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoObsSaida" runat="server" Height="50px"
                                    TextMode="MultiLine" ValidationGroup="saida" Width="500px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo" style="height: 34px">destinatário:</td>
                            <td class="direito" style="height: 34px">
                                <asp:TextBox ID="cTextoDestinatario" runat="server" Height="26px" Width="278px"
                                    ValidationGroup="saida"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">responsável pela devolução:</td>
                            <td class="direito">
                                <cDdlUsuarioFUSP:cDdlUsuarioFUSP ID="cTextoUsuarioResp" runat="server"
                                    ValidationGroup="saida" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblMsgSaida" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th colspan="2">
                                <div id="dGravacao2" runat="server">
                                    <asp:Button ID="btAlterarSaida" runat="server"
                                        Text="salvar saida" ValidationGroup="saida"
                                        OnClick="btAlterarSaida_Click" />
                                    <asp:Button ID="btCancelSaida" runat="server" CausesValidation="False"
                                        Text="cancelar saida" ValidationGroup="saida"
                                        OnClick="btCancelSaida_Click" Visible="False" />
                                </div>
                            </th>
                        </tr>
                    </table>
                    <table class="cadastro" id="tbHistorico" runat="server">
                    <tr>
                        <td colspan="2">
                            <uc4:ControleHistoricoEntrada ID="ControleHistoricoEntrada1" runat="server" />
                        </td>
                    </tr>
                </table>
                </asp:Panel>
                <div id="dImprimir" runat="server">
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <ajaxToolkit:UpdatePanelAnimationExtender ID="upae" BehaviorID="animation" runat="server" TargetControlID="UpdatePanel1" Enabled="True">
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
