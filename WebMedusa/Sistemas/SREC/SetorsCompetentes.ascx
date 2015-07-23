<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SetorsCompetentes.ascx.cs" Inherits="Medusa.Sistemas.SREC.SetorsCompetentes" %>

<%@ Register Src="../../Controles/DdlStatusAdiantamentos.ascx" TagName="DdlStatusAdiantamentos" TagPrefix="uc1" %>

<asp:Panel ID="panelCadastro" runat="server" Width="70%">
    <table>

        <tr>
            <td>
                <asp:Label ID="txtCodigo" runat="server" Text="-1" Visible="False" />
                <cddlSetor:cDdlSetor ID="cDdlSetor1" runat="server" EnabledValidator="false" Width="200" ValidationGroup="setoresCompetentes" />
                <asp:ImageButton ID="ImageButtonInserir" runat="server" ImageUrl="../../Styles/img/insert.png" OnClick="btInserir_Click" />
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="panelGrid" runat="server" Width="70%">
    <asp:GridView ID="grid" runat="server" AutoGenerateColumns="False" CssClass="tableView"
        OnDataBinding="grid_DataBinding" OnRowDeleting="grid_RowDeleting" Width="50%">
        <Columns>
            <asp:TemplateField HeaderText="setor competente" SortExpression="Setor.nome">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Setor.nome") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Setor.nome") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="../../Styles/img/delete_img.png"
                        CausesValidation="false" CommandName="Delete" />
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
</asp:Panel>
