<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.Master" AutoEventWireup="true" CodeBehind="PlanoContas.aspx.cs" Inherits="Medusa.Sistemas.SCFP.PlanoContas" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<%@ Register Src="../../Controles/DdlTipoDespesas.ascx" TagName="DdlTipoDespesas" TagPrefix="uc1" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
                    <div id="updateProgressDiv" style="display: none; position: absolute;">
                        <div style="margin-left: 780px; float: left">
                            <img src="../../Styles/img/loading.gif" />
                            <span style="margin: 3px">Carregando ...</span>
                        </div>
                    </div>
                    <asp:Label ID="Label1" runat="server" Text="procurar"></asp:Label>
                    &nbsp;<asp:DropDownList ID="ddlOptions" runat="server" CausesValidation="True"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlOptions_SelectedIndexChanged">
                        <asp:ListItem Value="codigo">código</asp:ListItem>
                        <asp:ListItem Value="classe">classe</asp:ListItem>
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
                </div>
                <asp:Panel ID="panelGrid" runat="server">


                    <asp:GridView ID="grid" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" Caption="Lista de Plano Contas" CellPadding="4"
                        CssClass="tableView" ForeColor="#333333" GridLines="None"
                        OnPageIndexChanging="grid_PageIndexChanging" OnRowCreated="grid_RowCreated"
                        OnRowEditing="grid_RowEditing" OnSorting="grid_Sorting" Width="100%">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:BoundField DataField="codigo" HeaderText="código"
                                SortExpression="codigo">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="classe" HeaderText="classe"
                                SortExpression="classe">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="item" HeaderText="item" SortExpression="item">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="sub_item" HeaderText="sub item"
                                SortExpression="sub_item">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="strClassificacao" HeaderText="classificação"
                                SortExpression="strClassificacao">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:CommandField EditText="selecionar" ShowEditButton="True">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:CommandField>
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2"
                            CssClass="SortedAscendingCellStyle" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C"
                            CssClass="SortedAscendingHeaderStyle" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8"
                            CssClass="SortedDescendingCellStyle" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE"
                            CssClass="SortedDescendingHeaderStyle " />
                    </asp:GridView>
                </asp:Panel>
                <asp:Panel ID="panelCadastro" runat="server">
                    <table class="cadastro">
                        <tr>
                            <th colspan="1">cadastro de planos de contas</th>
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
                            <td class="esquerdo">código:</td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoCodigo" runat="server" MaxLength="6" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">tipo de despesa:</td>
                            <td class="direito"></td>
                        </tr>
                        <tr>
                            <td class="esquerdo" style="height: 33px">classe:
                                <asp:Label ID="txtCodigo" runat="server" Text="0" Visible="False"></asp:Label>
                            </td>
                            <td class="direito" style="height: 33px">
                                <ctx:cTexto ID="cTextoClasse" runat="server" EnableValidator="True"
                                    MaxLength="30" Width="300" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">item:
                            </td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoItem" runat="server" EnableValidator="True" MaxLength="50"
                                    Width="300" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">subItem:</td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoSubItem" runat="server" Width="300" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">conta contabil:</td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoContaContabil" runat="server" Width="300" EnableValidator="false"
                                    MaxLength="25" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">classificação:</td>
                            <td class="direito">
                                <asp:RadioButtonList ID="rbClassificacao" runat="server"
                                    RepeatDirection="Horizontal">
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">crédito:</td>
                            <td class="direito">
                                <asp:RadioButtonList ID="rbCredito" runat="server"
                                    RepeatDirection="Horizontal" AppendDataBoundItems="true">
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">débito:</td>
                            <td class="direito">
                                <asp:RadioButtonList ID="rbDebito" runat="server"
                                    RepeatDirection="Horizontal" AppendDataBoundItems="true">
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">projeto destino:</td>
                            <td class="direito">
                                <cddlProjeto:cDdlProjeto ID="cDdlProjeto1" runat="server" EnableValidator="false" />
                            </td>
                        </tr>
                        <tr id="trLanctoTipos" runat="server">
                            <td class="esquerdo">tipos de lançamento:</td>
                            <td>
                                <table>
                                    <tr>
                                        <td colspan="2" align="left">
                                            <asp:GridView ID="gridLanctoTipos" runat="server" AutoGenerateColumns="False"
                                                OnRowDeleting="gridLanctoTipos_RowDeleting" DataKeyNames="id_lanTipo_Pc"
                                                EmptyDataText="os tipos de lançamento aparecerão aqui" CssClass="mGrid"
                                                PagerStyle-CssClass="pgr" EnableTheming="False" GridLines="None"
                                                AllowSorting="True">
                                                <RowStyle BackColor="#dedede" ForeColor="Black" />
                                                <AlternatingRowStyle BackColor="White" ForeColor="Black" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="tipo de lancto">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("tipo.nome") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("tipo.nome") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ShowHeader="False">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="../../Styles/img/delete_img.png"
                                                                CausesValidation="false" CommandName="Delete" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <cDdlLancamentoTipo:cDdlLancamentoTipo ID="cDdlLancamentoTipo1" runat="server" CausesValidation="true" ValidationGroup="LanctoTipo" />
                                        </td>
                                        <td align="left">

                                            <asp:Button ID="btAddLanctoTipo" runat="server" Text="adicionar" OnClick="btAddLanctoTipo_Click" ValidationGroup="LanctoTipo" />

                                        </td>
                                    </tr>
                                </table>

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
