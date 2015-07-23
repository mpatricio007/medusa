<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControleTaxaProjeto.ascx.cs"
    Inherits="Medusa.Sistemas.SCP.ControleTaxaProjeto" %>
<%@ Register Src="../../Controles/DdlDocumentoProjeto.ascx" TagName="ddldocumentoprojeto"
    TagPrefix="uc2" %>
<%@ Register Src="../../Controles/DdlTaxaProjeto.ascx" TagName="DdlTaxaProjeto" TagPrefix="uc1" %>
<div id="dConteudo" runat="server">
<div>
    &nbsp;<asp:Button ID="btCriar" runat="server" CausesValidation="False" OnClick="btCriar_Click"
        Text="nova taxa" />
</div>
<asp:Panel ID="panelGrid" runat="server">
    <asp:GridView ID="grid" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
        OnPageIndexChanging="grid_PageIndexChanging" OnRowCreated="grid_RowCreated" OnRowEditing="grid_RowEditing"
        OnSorting="grid_Sorting" Width="50%" EmptyDataText="não há taxas cadastradas"
        EnableTheming="False" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
        <AlternatingRowStyle CssClass="alt" />
        <Columns>
            <asp:CommandField EditText="selecionar" ShowEditButton="True">
            </asp:CommandField>
            <asp:BoundField HeaderText="código" DataField="id_taxa" SortExpression="id_taxa">
            </asp:BoundField>
            <asp:TemplateField HeaderText="nome" SortExpression="TaxaProjeto.nome">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("TaxaProjeto.nome") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("TaxaProjeto.nome") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="taxa" SortExpression="TaxaProjeto.taxa">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("TaxaProjeto.taxa") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("TaxaProjeto.taxa") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="inicio" SortExpression="TaxaProjeto.data_inicio">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Medusa.LIB.Util.DateToString(Convert.ToDateTime(Eval("TaxaProjeto.data_inicio"))) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <HeaderStyle HorizontalAlign="Left" />
        <PagerStyle CssClass="pgr" />
    </asp:GridView>
</asp:Panel>
<asp:Panel ID="panelCadastro" runat="server">
    <table class="mGrid" width="50%">
        <tr>
            <th>
                taxa do projeto
            </th>
        </tr>
        <tr>
            <td class="direito">
                <cDdlTaxaProjeto:cDdlTaxaProjeto ID="cDdlTaxaProjeto1" runat="server" 
                    ValidationGroup="taxa" />
                <asp:Label ID="txtCodigo" runat="server" Text="0" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>
                  <table>
                    <tr>
                        <td>
                            <div id="dGravacao" runat="server">
                                <asp:Button ID="btInserir0" runat="server" OnClick="btInserir_Click" Text="inserir"
                                    ValidationGroup="taxa" />
                                <asp:Button ID="btExcluir0" runat="server" CausesValidation="False" OnClick="btExcluir_Click"
                                    Text="excluir" />
                                </div>
                        </td>
                        <td>
                            <asp:Button ID="btCancelar0" runat="server" CausesValidation="False" OnClick="btCancelar_Click"
                                    Text="cancelar" />
                        </td>
                    </tr>
                </table>
            </th>
        </tr>
    </table>
</asp:Panel>
</div>