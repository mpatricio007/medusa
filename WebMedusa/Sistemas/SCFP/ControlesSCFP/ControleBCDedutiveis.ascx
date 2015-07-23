<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControleBCDedutiveis.ascx.cs" Inherits="Medusa.Sistemas.SCFP.ControlesSCFP.ControleBCDedutiveis" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Panel ID="panelGrid" runat="server">

    <asp:GridView ID="grid" runat="server" CssClass="mGrid" AutoGenerateColumns="False" Width="100%" DataKeyNames="id_taxa"
        EmptyDataText="nenhum dedutível cadastrado para esta tabela" Caption="Dedutíveis" OnRowDeleting="grid_RowDeleting">
        <Columns>
            <asp:BoundField DataField="nome" HeaderText="taxa" />
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Styles/img/delete_img.png"
                        CausesValidation="false" CommandName="Delete" />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" Width="10px" />
                <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>
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
            <td>dedutivel:<asp:Label ID="txtCodigo" runat="server" Text="0" Visible="False"></asp:Label>
            </td>
            <td class="direito">
                <asp:DropDownList ID="lista" runat="server" DataTextField="nome"
                    DataValueField="id_taxa" AppendDataBoundItems="True">
                </asp:DropDownList>
                <asp:ListSearchExtender ID="lista_ListSearchExtender" runat="server"
                    Enabled="True" PromptCssClass="ListSearchExtenderPrompt"
                    PromptText="digite para procurar" QueryPattern="Contains" QueryTimeout="2000"
                    TargetControlID="lista">
                </asp:ListSearchExtender>
                <asp:CompareValidator ID="cv" runat="server" ValidationGroup="dedutivel"
                    ErrorMessage="selecione uma taxa" ForeColor="Red" Operator="NotEqual"
                    ValueToCompare="0" ControlToValidate="lista"></asp:CompareValidator>
                </td><td>
                <asp:Button ID="btInserir" runat="server" Text="inserir" ValidationGroup="dedutivel"
                    OnClick="btInserir_Click" />
            </td>
        </tr>
    </table>
</asp:Panel>
