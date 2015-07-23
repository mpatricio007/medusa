<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.Master" AutoEventWireup="true" CodeBehind="Devolucoes.aspx.cs" Inherits="Medusa.Sistemas.Devolucao.Devolucoes" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../Controles/DdlTipoDevolucao.ascx" TagName="DdlTipoDevolucao" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="conteudo">
        &nbsp;&nbsp;&nbsp;
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
                        <asp:ListItem Value="Projeto.codigo">projeto</asp:ListItem>
                        <asp:ListItem Value="numero">numero</asp:ListItem>
                        <asp:ListItem Value="protocolo">protocolo</asp:ListItem>
                        <asp:ListItem Value="beneficiario">beneficiário</asp:ListItem>
                        <asp:ListItem Value="valor_total">valor</asp:ListItem>
                        <asp:ListItem Value="TipoDevolucao.nome">tipo de devolução</asp:ListItem>
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
                <asp:Panel ID="panelGrid" runat="server" HorizontalAlign="Left">


                    <asp:GridView ID="grid" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" Caption="Lista de Devolucoes"
                        CssClass="tableView"
                        OnPageIndexChanging="grid_PageIndexChanging" OnRowCreated="grid_RowCreated"
                        OnRowEditing="grid_RowEditing" OnSorting="grid_Sorting" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="Projeto.codigo" HeaderText="projeto"
                                SortExpression="Projeto.codigo">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="numero" HeaderText="número" SortExpression="numero">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="protocolo" HeaderText="protocolo"
                                SortExpression="protocolo">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="beneficiario" HeaderText="beneficiário" SortExpression="beneficiario">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="valor_total" DataFormatString="{0:c}" HeaderText="valor total" SortExpression="valor_total">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="tipo de devolução" SortExpression="TipoDevolucao.nome">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("TipoDevolucao.nome") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("TipoDevolucao.nome") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:CommandField EditText="selecionar" ShowEditButton="True">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:CommandField>
                        </Columns>
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
                            <th colspan="1">cadastro de bolsas</th>
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
                            <td class="esquerdo">projeto:
                                <asp:Label ID="txtCodigo" runat="server" Text="0" Visible="False"></asp:Label>
                                <asp:Label ID="lblIdMotivo" runat="server" Text="-1" Visible="False"></asp:Label>
                            </td>
                            <td class="direito">
                                <cddlProjeto:cDdlProjeto ID="cDdlProjeto1" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">número:</td>
                            <td class="direito">
                                <cint:cInteiro ID="cIntNumero" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">protocolo:
                            </td>
                            <td class="direito">
                                <cint:cInteiro ID="cIntProtocolo" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">beneficiário:</td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoBeneficiario" runat="server" MaxLength="200" Width="300px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">valor total:</td>
                            <td class="direito">
                                <cvl:cValor ID="cValor" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">tipo de devolução:</td>
                            <td class="direito">
                                <uc1:DdlTipoDevolucao ID="DdlTipoDevolucao1" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:GridView ID="gridMotivos" runat="server" CssClass="mGrid" BorderStyle="Solid"
                                    AutoGenerateColumns="False" CellPadding="4" PagerStyle-CssClass="pgr"
                                    ForeColor="#333333" GridLines="None" Width="100%">
                                    <RowStyle BackColor="#dedede" ForeColor="Black" />
                                    <AlternatingRowStyle BackColor="White" ForeColor="Black" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ckMotivo" runat="server" AutoPostBack="True" OnCheckedChanged="ckMotivo_CheckedChanged" CssClass='<%# Container.DataItemIndex %>' />
                                                <asp:Label ID="lbMotivo" runat="server" Text='<%# Bind("id_motivo") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="20px" />
                                            <ItemStyle Width="20px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="codigo" HeaderText="código">
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="descricao_motivo" HeaderText="descrição"></asp:BoundField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Panel ID="pOutros" runat="server" Width="500px" Height="100px" Visible='<%# Bind("temOutros") %>'>
                                                    <tr>
                                                        <td></td>
                                                        <td colspan='999'>
                                                            <ctx:cTexto ID="cTextoOutros" runat="server" Width="500px" Height="100px" TextMode="MultiLine" />
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                                <asp:Panel ID="pSubmotivos" runat="server" Width="100%" Visible="false">

                                                    <tr>
                                                        <td></td>
                                                        <td colspan='999'>
                                                            <asp:GridView ID="gridMotivos" runat="server" AllowSorting="True" Width="100%"
                                                                AutoGenerateColumns="False" BorderStyle="Solid" CellPadding="4"
                                                                CssClass="mGrid" EnableTheming="False" GridLines="None" DataKeyNames="id_motivo">
                                                                <RowStyle BackColor="#dedede" ForeColor="Black" />
                                                                <AlternatingRowStyle BackColor="White" ForeColor="Black" />
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="ckMotivo" runat="server" AutoPostBack="true" OnCheckedChanged="ckSubMotivo_CheckedChanged" CssClass='<%# Container.DataItemIndex %>' />
                                                                            <headerstyle width="10px" />
                                                                            <itemstyle width="10px" />
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="30px" />
                                                                        <ItemStyle Width="30px" />
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="codigo" HeaderText="código">
                                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="70px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="descricao_motivo" HeaderText="descrição"></asp:BoundField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:Panel ID="pOutros" runat="server" Width="500px" Height="100px" Visible='<%# Bind("temOutros") %>'>
                                                                                <tr>
                                                                                    <td></td>
                                                                                    <td colspan='999'>
                                                                                        <ctx:cTexto ID="cTextoOutros" runat="server" Width="500px" Height="100px" TextMode="MultiLine"
                                                                                            EnableValidator='<%#Bind("temOutros") %>' />
                                                                                    </td>
                                                                                </tr>
                                                                            </asp:Panel>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                </asp:Panel>

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
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
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th colspan="2">
                                <div id="dGravacao1">
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
        <ajaxtoolkit:UpdatePanelAnimationExtender ID="upae" BehaviorID="animation" runat="server" TargetControlID="UpdatePanel1" Enabled="True">

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
        </ajaxtoolkit:UpdatePanelAnimationExtender>
    </div>
</asp:Content>
