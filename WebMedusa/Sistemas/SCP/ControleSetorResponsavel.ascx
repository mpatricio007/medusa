<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControleSetorResponsavel.ascx.cs" Inherits="Medusa.Sistemas.SCP.ControleSetorResponsavel" %>

<div id="dConteudo" runat="server">
<div>
                    <asp:Button ID="btCriar" runat="server" CausesValidation="False" 
                        onclick="btCriar_Click"  Text="adicionar setor"  />
</div>
<asp:Panel ID="panelGrid" runat="server">
    <asp:GridView ID="grid" runat="server" AutoGenerateColumns="False" 
        AllowPaging="True" AllowSorting="True" 
        onpageindexchanging="grid_PageIndexChanging" onsorting="grid_Sorting" 
        Width="50%" EmptyDataText="não há setores cadastrados" 
        onrowcreated="grid_RowCreated" onrowediting="grid_RowEditing"
         EnableTheming="False" CssClass="mGrid" PagerStyle-CssClass="pgr" 
        AlternatingRowStyle-CssClass="alt">
        <AlternatingRowStyle CssClass="alt" />
        <Columns>
            <asp:CommandField EditText="selecionar" ShowEditButton="True" />
            <asp:TemplateField HeaderText="sigla" SortExpression="Setor.sigla">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" 
                        Text='<%# Bind("Setor.sigla") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" 
                        Text='<%# Bind("Setor.sigla") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="setor" SortExpression="Setor.nome">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Setor.nome") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Setor.nome") %>'></asp:Label>
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
            <td class="esquerdo">
                setor:
                <asp:Label ID="txtCodigo" runat="server" Text="0" Visible="False"></asp:Label>
            </td>
            <td class="direito">
                <cddlSetor:cDdlSetor ID="cDdlSetor1" runat="server" 
                    ValidationGroup="dependente" />
            </td>
        </tr>
        <tr>
            <td  class="direito" colspan="2">
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
                                    Text="inserir" ValidationGroup="setor" />
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