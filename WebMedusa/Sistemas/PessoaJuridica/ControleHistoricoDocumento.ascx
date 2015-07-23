<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControleHistoricoDocumento.ascx.cs" Inherits="Medusa.Sistemas.PessoaJuridica.ControleHistoricoDocumento" %>


<style type="text/css">
    .style1
    {
        width: 341px;
    }
</style>


<table id="TabelaCadastro" class="cadastro" runat="server">
                      <tr>
                        <td colspan="2">
                            <asp:GridView ID="grid" runat="server" 
                            AutoGenerateColumns="False" CssClass="tableView" Width="100%" 
                                Caption="Documentos" onrowediting="grid_RowEditing">
                                <Columns>
                                    <asp:BoundField DataField="data" HeaderText="data" SortExpression="data" />
                                    <asp:TemplateField HeaderText="documento" 
                                        SortExpression="documentocategoria.nome">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" 
                                                Text='<%# Bind("documentocategoria.nome") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" 
                                                Text='<%# Bind("documentocategoria.nome") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="numero" HeaderText="número" 
                                        SortExpression="numero" />
                                    <asp:BoundField DataField="validade" DataFormatString="{0:d}" 
                                        HeaderText="validade" SortExpression="validade" />
                                    <asp:BoundField DataField="obs" HeaderText="observação" SortExpression="obs" />
                                    <asp:TemplateField HeaderText="status" 
                                        SortExpression="StatusDocFornecedor.nome">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox3" runat="server" 
                                                Text='<%# Bind("StatusDocFornecedor.nome") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" 
                                                Text='<%# Bind("StatusDocFornecedor.nome") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="usuário" 
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
                                    <asp:CommandField ShowEditButton="True" EditText="cancelar">                                   
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:CommandField>
                                </Columns>
                                <HeaderStyle HorizontalAlign="Left" />
                                <SortedAscendingCellStyle CssClass="SortedAscendingCellStyle" />
                                <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle" />
                                <SortedDescendingCellStyle CssClass="SortedDescendingCellStyle" />
                                <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle " />
                            </asp:GridView>
            </td>
            </tr>
    <tr>
        <td class="esquerdo">
            documento:<asp:Label ID="txtCodigo" runat="server" Text="0" Visible="False"></asp:Label>
        </td>

        <td class="direito">
            <DdlDocumentosCategorias:ddlDocumentosCategorias ID="ddlDocumentosCategorias" runat="server" />
        </td>
    </tr>

    <tr>
        <td class="esquerdo">
            número:
        </td>

        <td class="direito">
            <ctx:cTexto ID="cIntNumero" runat="server" />
        </td>
    </tr>

    <tr>
        <td class="esquerdo">
            validade:
        </td>

        <td class="direito">
            <cdt:cData ID="cValidade" runat="server" />
        </td>
    </tr>
</table>
<table id="TabelaCancelar" class="cadastro" runat="server">
    <tr>
        <td class="esquerdo">
            observação:
        </td>
        <td class="style1">
            <ctx:cTexto ID="cTextoObs" runat="server" EnableValidator="True" 
                ValidationGroup="cancelar" Height="80px" TextMode="MultiLine" 
                Width="400px" />
        </td>
    </tr>
    </table>






    <table class="cadastro">
    <tr>
      <td colspan="2">
          <asp:Label ID="lbMsg" runat="server" Text="[lbMsg]"></asp:Label>
      </td>
    </tr>
    <tr>
        <th colspan="2">
            <div id="dGravacao" runat="server">
            <asp:Button ID="btInserir" runat="server" 
                Text="inserir" ValidationGroup="historicofornecedor" 
                    onclick="btInserir_Click" />
            <asp:Button ID="btAlterar" runat="server" onclick="btAlterar_Click" 
                Text="ok" ValidationGroup="cancelar" />
            <asp:Button ID="btCancelar" runat="server" 
                Text="cancelar" CausesValidation="False" onclick="btCancelar_Click" />
            </div>
        </th>
    </tr>
</table>