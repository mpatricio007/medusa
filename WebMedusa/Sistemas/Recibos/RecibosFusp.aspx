<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.Master" AutoEventWireup="true"
    CodeBehind="RecibosFusp.aspx.cs" Inherits="Medusa.Sistemas.Recibos.RecibosFusp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="ControleReciboCheques.ascx" TagName="ControleReciboCheques" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="conteudo">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:CheckBox ID="ckFilter" runat="server" AutoPostBack="True" OnCheckedChanged="ckFilter_CheckedChanged"
                    Text="habilitar múltiplos filtros" />
                <asp:DataList ID="DataListFiltros" runat="server" RepeatColumns="6" OnDataBinding="DataListFiltros_DataBinding"
                    RepeatDirection="Horizontal" OnDeleteCommand="DataListFiltros_DeleteCommand">
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
                        <asp:ListItem Value="id_recibo_fusp">recibo nº</asp:ListItem>
                        <asp:ListItem Value="data">data</asp:ListItem>
                        <asp:ListItem Value="valor">valor</asp:ListItem>
                        <asp:ListItem Value="nome">recebemos de</asp:ListItem>
                        <asp:ListItem Value="descricao">referente a</asp:ListItem>
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
                        Caption="Lista de Recibos FUSP" CssClass="tableView" OnPageIndexChanging="grid_PageIndexChanging"
                        OnRowCreated="grid_RowCreated" OnRowEditing="grid_RowEditing" OnSorting="grid_Sorting"
                        Width="100%">
                        <Columns>
                            <asp:BoundField DataField="id_recibo_fusp" HeaderText="Nº do recibo" SortExpression="id_recibo_fusp">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="data" HeaderText="data" SortExpression="data" DataFormatString="{0:d}">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="valor" HeaderText="valor" SortExpression="valor" DataFormatString="{0:c}">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nome" HeaderText="recebemos de" SortExpression="nome">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="descricao" HeaderText="referente a" SortExpression="descricao">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tipo_pagamento" HeaderText="pagamento através de" SortExpression="tipo_pagamento">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="strDocumentos" HeaderText="cpf/cnpj">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="usuário">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Usuario.PessoaFisica.nome") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Usuario.PessoaFisica.nome") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:CheckBoxField DataField="status_recibo" HeaderText="status" SortExpression="status_recibo">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:CheckBoxField>
                            <asp:CommandField EditText="selecionar" ShowEditButton="True">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:CommandField>
                        </Columns>
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
                                cadastro de recibos Fusp
                            </th>
                            <th colspan="1">
                                <div id="dGravacao" runat="server">
                                    <asp:Button ID="btInserir" runat="server" OnClick="btInserir_Click" Text="inserir"
                                        ValidationGroup="recibo" />
                                    <asp:Button ID="btExcluir" runat="server" OnClick="btExcluir_Click"
                                        Text="cancelar recibo" ValidationGroup="cancelar" />
                                    <asp:Button ID="btCancelar" runat="server" CausesValidation="False" OnClick="btCancelar_Click"
                                        Text="limpar campos" ValidationGroup="recibo" Style="width: 129px" />
                                    <asp:ImageButton ID="btImprimirRecibo" runat="server" ImageUrl="~/Styles/img/print.gif"
                                        OnClick="btImprimirRecibo_Click" Style="width: 16px; height: 16px;" ToolTip="imprimir" />
                                </div>
                            </th>
                        </tr>
                        <asp:Panel ID="panelCampos" runat="server">
                            <tr>
                                <td class="esquerdo">
                                    Recibo Nº:
                                </td>
                                <td class="direito">
                                    <asp:TextBox ID="txtCodigo" runat="server" Enabled="False" ValidationGroup="recibo"
                                        Width="80px">0</asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="esquerdo">
                                    data:&nbsp;
                                </td>
                                <td class="direito">
                                    <cdt:cData ID="cData" runat="server" ValidationGroup="recibo" />
                                </td>
                            </tr>
                            <tr>
                                <td class="esquerdo">
                                    usuário:
                                </td>
                                <td class="direito">
                                    <asp:Label ID="txtUsuario" runat="server" Text="0"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="esquerdo">
                                    tipo inscrição:
                                </td>
                                <td class="direito">
                                    <asp:RadioButtonList ID="rbDocumento" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rbDocumento_SelectedIndexChanged"
                                        RepeatDirection="Horizontal" ValidationGroup="recibo">
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <asp:Panel ID="divCnpj" runat="server">
                                <tr>
                                    <td class="esquerdo">
                                        cnpj:
                                    </td>
                                    <td class="direito">
                                        <cCnpj:cCNPJ ID="cCNPJ2" runat="server" ValidationGroup="recibo" />
                                    </td>
                                </tr>
                            </asp:Panel>
                            <asp:Panel ID="divCpf" runat="server">
                                <tr>
                                    <td class="esquerdo">
                                        cpf:
                                    </td>
                                    <td class="direito">
                                        <cCpf:cCPF ID="cCPF" runat="server" ValidationGroup="recibo" />
                                    </td>
                                </tr>
                            </asp:Panel>
                            
                            <tr>
                                <td class="esquerdo">
                                    recebemos de:
                                </td>
                                <td class="direito">
                                    <ctx:cTexto ID="cTextoNome" runat="server" EnableValidator="True" MaxLength="100"
                                        ValidationGroup="recibo" Width="400" />
                                </td>
                            </tr>
                            <tr>
                                <td class="esquerdo">
                                    projeto:
                                </td>
                                <td class="direito">
                                    <cddlProjeto:cDdlProjeto ID="cDdlProjeto1" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="esquerdo">
                                    referente a:
                                </td>
                                <td class="direito">
                                    <ctx:cTexto ID="cTextoDescricao" runat="server" Height="100" MaxLength="200" TextMode="MultiLine"
                                        Width="400" ValidationGroup="recibo" />
                                </td>
                            </tr>
                            <tr>
                                <td class="esquerdo">
                                    pagamento através de:
                                </td>
                                <td class="direito">
                                    <ctx:cTexto ID="cTextoTipoPagto" runat="server" MaxLength="100" Visible="True" Width="400"
                                        ValidationGroup="recibo" />
                                </td>
                            </tr>
                            <tr>
                                <td class="esquerdo">
                                    a importância supra de:
                                </td>
                                <td class="direito">
                                    <cvl:cValor ID="cValor" runat="server" ValidationGroup="recibo" />
                                </td>
                            </tr>
                            <tr>
                                <td class="esquerdo">
                                    observação:
                                </td>
                                <td class="direito">
                                    <ctx:cTexto ID="cTextoObs" runat="server" Height="70" TextMode="MultiLine" Width="400"
                                        MaxLength="200" EnableValidator="False" />
                                </td>
                            </tr>
                        </asp:Panel>
                        <tr>
                            <td colspan="2">
                                <div id="dMotivo" runat="server">
                                    <table width="100%">
                                        <tr>
                                            <td class="esquerdo">
                                                motivo do cancelamento:
                                            </td>
                                            <td class="direito">
                                                <ctx:cTexto ID="cTextoMotivo" runat="server" Height="70" TextMode="MultiLine" ValidationGroup="cancelar"
                                                    Width="400" MaxLength="100" Enable="true" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
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
                                    <asp:Button ID="btInserir0" runat="server" OnClick="btInserir_Click" Text="inserir"
                                        ValidationGroup="recibo" />
                                    <asp:Button ID="btExcluir0" runat="server" CausesValidation="True" OnClick="btExcluir_Click"
                                        Text="cancelar recibo" ValidationGroup="cancelar" />
                                    <asp:Button ID="btCancelar0" runat="server" CausesValidation="False" OnClick="btCancelar_Click"
                                        Text="limpar campos" ValidationGroup="recibo" />
                                    &nbsp;<asp:ImageButton ID="btImprimirRecibo0" runat="server" ImageUrl="~/Styles/img/print.gif"
                                        OnClick="btImprimirRecibo_Click" Style="height: 16px; width: 16px;" ToolTip="imprimir" />
                                </div>
                            </th>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
