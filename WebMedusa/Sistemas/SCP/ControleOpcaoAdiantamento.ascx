<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControleOpcaoAdiantamento.ascx.cs" Inherits="Medusa.Sistemas.SCP.ControleOpcaoAdiantamento" %>
                <%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
                <%@ Register src="../../Controles/DdlTiposAdiantamentos.ascx" tagname="DdlTiposAdiantamentos" tagprefix="uc1" %>
<div>
    <asp:Button ID="btCriar" runat="server" CausesValidation="False" OnClick="btCriar_Click"
        Text="nova opçao"  />
</div>
<asp:Panel ID="panelGrid" runat="server">
    <asp:GridView ID="grid" runat="server" AutoGenerateColumns="False" AllowPaging="True"
        AllowSorting="True" OnPageIndexChanging="grid_PageIndexChanging" OnSorting="grid_Sorting"
        Width="50%" EmptyDataText="não há opções de adiantamento cadastradas" OnRowCreated="grid_RowCreated"
        OnRowEditing="grid_RowEditing"
        EnableTheming="False" CssClass="mGrid" PagerStyle-CssClass="pgr" 
        AlternatingRowStyle-CssClass="alt">
        <AlternatingRowStyle />
        <Columns>
            <asp:TemplateField HeaderText="opção" SortExpression="TiposAdiantamento.nome">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" 
                        Text='<%# Bind("TiposAdiantamento.nome") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" 
                        Text='<%# Bind("TiposAdiantamento.nome") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="data" HeaderText="data" SortExpression="data" 
                DataFormatString="{0:d}" />
            <asp:BoundField DataField="obs" HeaderText="observação" SortExpression="obs" />
            <asp:TemplateField HeaderText="conferente" 
                SortExpression="Usuario.PessoaFisica.nome">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" 
                        Text='<%# Bind("Usuario.PessoaFisica.nome") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" 
                        Text='<%# Bind("Usuario.PessoaFisica.nome") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField EditText="selecionar" ShowEditButton="True">
                <HeaderStyle ></HeaderStyle>
                <ItemStyle HorizontalAlign="Left" />
            </asp:CommandField>
        </Columns>
        <HeaderStyle />
        <PagerStyle CssClass="pgr" />
        <SortedAscendingCellStyle CssClass="SortedAscendingCellStyle" />
        <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle" />
        <SortedDescendingCellStyle CssClass="SortedDescendingCellStyle" />
        <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle " />
    </asp:GridView>
</asp:Panel>
<asp:Panel ID="panelCadastro" runat="server" Visible="False">
    <table width="50%" class="mGrid">
        <tr>
            <th colspan="2">
                opção de adiantamento
            </th>
        </tr>
        <tr>
            <td class="esquerdo">
                opção:<asp:Label ID="txtCodigo" runat="server" Text="0" Visible="False"></asp:Label>
            </td>
            <td class="direito">
                <cDdlTiposAdiantamentos:cDdlTiposAdiantamentos ID="cDdlTiposAdiantamentos1" ValidationGroup="adiantamentos"
                    runat="server" />
            </td>
        </tr>
        <tr>
            <td class="esquerdo">
                data:</td>
            <td class="direito">
                <cdt:cData ID="cData1" runat="server" ValidationGroup="adiantamentos" />
            </td>
        </tr>
        <tr>
            <td class="esquerdo">
                observação:</td>
            <td class="direito">
                <ctx:cTexto ID="cTextoObs" runat="server" Text="100px" TextMode="MultiLine" 
                    Width="500px" MaxLength="150" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th colspan="2">
                <table>
                    <tr>
                        <td>
                            <div id="dGravacao" runat="server">
                                <asp:Button ID="btInserir0" runat="server" OnClick="btInserir_Click" Text="inserir" ValidationGroup="adiantamentos"
                                    />
                                </div>
                        </td>
                        <td>
                            <asp:Button ID="btCancelar0" runat="server" CausesValidation="False" OnClick="btCancelar_Click"  Text="cancelar" />
                        </td>
                    </tr>
                </table>
            </th>
        </tr>
    </table>
</asp:Panel>




