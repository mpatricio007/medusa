<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControleEnderecoProjeto.ascx.cs" Inherits="Medusa.Sistemas.SCP.ControleEnderecoProjeto" %>
<div id="dConteudo" runat="server">
<div>
                    <asp:Button ID="btCriar" runat="server" CausesValidation="False" 
                        onclick="btCriar_Click"  Text="novo endereço"  />
</div>
<asp:Panel ID="panelGrid" runat="server">
    <asp:GridView ID="grid" runat="server" AutoGenerateColumns="False" 
        AllowPaging="True" AllowSorting="True" 
        onpageindexchanging="grid_PageIndexChanging" onsorting="grid_Sorting" 
        Width="50%" EmptyDataText="não há endereço cadastrados" 
        onrowcreated="grid_RowCreated" onrowediting="grid_RowEditing"
         EnableTheming="False" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
        <AlternatingRowStyle CssClass="alt" />
        <Columns>
            <asp:CommandField EditText="selecionar" ShowEditButton="True" />
            <asp:TemplateField HeaderText="tipo" SortExpression="TipoEndereco.descricao">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox7" runat="server" 
                        Text='<%# Bind("TipoEndereco.descricao") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label7" runat="server" 
                        Text='<%# Bind("TipoEndereco.descricao") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:BoundField DataField="endereco" HeaderText="endereço">
            <HeaderStyle HorizontalAlign="Left" />
            </asp:BoundField>
        </Columns>
        <HeaderStyle HorizontalAlign="Left" />
        <PagerStyle CssClass="pgr" />
    </asp:GridView>
</asp:Panel>
<asp:Panel ID="panelCadastro" runat="server">
   
                tipo:<asp:Label ID="txtCodigo" runat="server" Text="0" Visible="False"></asp:Label>
                
                <cDdlTipoEndereco:cDdlTipoEndereco ID="cDdlTipoEndereco1" runat="server" 
                    ValidationGroup="dependente" />
                <br />
                endereço é o mesmo que:<asp:RadioButtonList ID="rbTipoEnderecoCorrespondencia" runat="server" 
                    AutoPostBack="True" 
                    onselectedindexchanged="rbTipoEnderecoCorrespondencia_SelectedIndexChanged" 
                    RepeatColumns="4" RepeatLayout="Flow">
                    <asp:ListItem>unidade</asp:ListItem>
                    <asp:ListItem>departamento</asp:ListItem>
                    <asp:ListItem Value="laboratorio">laboratório</asp:ListItem>
                    <asp:ListItem Selected="True">outros</asp:ListItem>
                </asp:RadioButtonList>
                
        <table class="mGrid" width="50%">
        <tr>
            <td>
                <cender:cEnder ID="cEnder1" runat="server" ValidationGroup="dependente" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <th>
                <table>
                    <tr>
                        <td>
                            <div ID="dGravacao" runat="server">
                                <asp:Button ID="btInserir0" runat="server" onclick="btInserir_Click" 
                                    Text="inserir" ValidationGroup="dependente" />
                                <asp:Button ID="btAlterar0" runat="server" onclick="btAlterar_Click" 
                                    Text="salvar" ValidationGroup="dependente" />
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