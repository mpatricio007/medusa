<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControleMaterial.ascx.cs"
    Inherits="Medusa.Sistemas.Almoxarifado.ControleMaterial" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<style type="text/css">
    .style3
    {
        height: 114px;
    }
</style>
<table class="cadastro" id="tbCadastro" runat="server">
    <tr>
        <td colspan="2" class="style3">
            <cDdlMaterialConsumo:cDdlMaterialConsumo ID="cDdlMaterialConsumo1" 
                runat="server" ValidationGroup="material" />&nbsp
            quantidade:
            <cint:cInteiro ID="cInteiro1" runat="server" MaxLength="5" Width="50px" />&nbsp
            <asp:Button ID="btInserir" runat="server" OnClick="btInserir_Click" Text="adicionar"
                ValidationGroup="material" />
            <asp:Label ID="txtCodigo" runat="server" Font-Bold="False" Text="-1" Visible="False" />
            
               
        </td>
        </tr>
        <tr>
        <td colspan="2">
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </td>
        </tr>
</table>
        <%--<asp:CommandField DeleteText="excluir" ShowDeleteButton="True" />--%>

<asp:GridView ID="grid" runat="server" AutoGenerateColumns="False" 
    CssClass="tableView" OnDataBinding="grid_DataBinding" 
    OnRowDeleting="grid_RowDeleting">
    <Columns>
        <asp:BoundField DataField="StrMaterial" HeaderText="material" />
        <asp:BoundField DataField="quantidade" HeaderText="quantidade" SortExpression="quantidade" />
        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="../../Styles/img/delete_img.png" CausesValidation="false" CommandName="Delete" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
    </Columns>
    <HeaderStyle HorizontalAlign="Left" />
    <SortedAscendingCellStyle CssClass="SortedAscendingCellStyle" />
    <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle" />
    <SortedDescendingCellStyle CssClass="SortedDescendingCellStyle" />
    <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle " />
</asp:GridView>
