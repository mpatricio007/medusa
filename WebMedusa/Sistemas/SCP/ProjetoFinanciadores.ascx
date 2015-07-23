<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProjetoFinanciadores.ascx.cs" Inherits="Medusa.Sistemas.SCP.ProjetoFinanciadores" %>
<div id="dConteudo" runat="server">
<div>
                    <asp:Button ID="btCriar" runat="server" CausesValidation="False" 
                        onclick="btCriar_Click"  Text="novo financiador" />
</div>
<asp:Panel ID="panelGrid" runat="server">
    <asp:GridView ID="grid" runat="server" AutoGenerateColumns="False" 
        AllowPaging="True" AllowSorting="True" 
        onpageindexchanging="grid_PageIndexChanging" onsorting="grid_Sorting" 
        Width="50%" EmptyDataText="não há financiadores cadastrados" 
        onrowcreated="grid_RowCreated" onrowediting="grid_RowEditing"
        EnableTheming="False" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
        <AlternatingRowStyle CssClass="alt" />
        <Columns>
            <asp:CommandField EditText="selecionar" ShowEditButton="True" />
            <asp:TemplateField HeaderText="nome do financiador" 
                SortExpression="Financiador.nome">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" 
                        Text='<%# Bind("Financiador.nome") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Financiador.nome") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:BoundField DataField="observacao" HeaderText="observação" />
        </Columns>
        <PagerStyle CssClass="pgr" />
    </asp:GridView>
</asp:Panel>
<asp:Panel ID="panelCadastro" runat="server">
    <table class="mGrid" width="50%">
    <tr>
            <th colspan="2">
                Financiador do projeto
            </th>
            
        </tr>
        <tr>
            <td class="esquerdo">
            
                nome:<asp:Label ID="txtCodigo" runat="server" Text="0" Visible="False"></asp:Label>
            
            </td>
            <td class="direito">
            
                <cDdlFinanciador:cDdlFinanciador ID="cDdlFinanciador1" runat="server" 
                    ValidationGroup="financiador"  />
            
            </td>
        </tr>
        <tr>
            <td class="esquerdo">
                observação:</td>
            <td class="direito">
                <ctx:cTexto ID="cTextoObs" runat="server" ValidationGroup="financiador" Height="100px" Width="500px" TextMode="MultiLine" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <th colspan="2">
                <table>
                    <tr>
                        <td>
                            <div ID="dGravacao" runat="server">
                                <asp:Button ID="btInserir0" runat="server" onclick="btInserir_Click" 
                                    Text="inserir" ValidationGroup="financiador" />
                                <asp:Button ID="btAlterar0" runat="server" onclick="btAlterar_Click" 
                                    Text="salvar" ValidationGroup="financiador" />
                                <asp:Button ID="btExcluir0" runat="server" CausesValidation="False" 
                                    onclick="btExcluir_Click" Text="excluir" />
                            </div>
                        </td>
                        <td>
                            <asp:Button ID="btCancelar0" runat="server" CausesValidation="False" 
                                onclick="btCancelar_Click" Text="cancelar" />
                        </td>
                    </tr>
                </table>
            </th>
        </tr>
    </table>
</asp:Panel>
</div>