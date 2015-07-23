<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControleProjetoDocumento.ascx.cs"
    Inherits="Medusa.Sistemas.SCP.ControleProjetoDocumento" %>
<%@ Register Src="../../Controles/DdlCoordenadores.ascx" TagName="ddlcoordenadores"
    TagPrefix="uc1" %>
<%@ Register Src="../../Controles/DdlDocumentoProjeto.ascx" TagName="DdlDocumentoProjeto"
    TagPrefix="uc2" %>
<div id="dConteudo" runat="server">
<div>
    &nbsp;<asp:Button ID="btCriar" runat="server" CausesValidation="False" OnClick="btCriar_Click"
        Text="novo documento"  />
</div>
<asp:Panel ID="panelGrid" runat="server">
    <asp:GridView ID="grid" runat="server" AutoGenerateColumns="False" AllowPaging="True"
        AllowSorting="True" OnPageIndexChanging="grid_PageIndexChanging" OnSorting="grid_Sorting"
        Width="50%" EmptyDataText="não há documentos cadastrados" OnRowCreated="grid_RowCreated"
        OnRowEditing="grid_RowEditing"
        EnableTheming="False" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                        
        <AlternatingRowStyle CssClass="alt" />
        <Columns>
            <asp:CommandField EditText="selecionar" ShowEditButton="True">
            </asp:CommandField>
            <asp:TemplateField HeaderText="documento" SortExpression="Documento.nome">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Documento.nome") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Documento.nome") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="data" SortExpression="data">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Medusa.LIB.Util.DateToString(Convert.ToDateTime(Eval("data"))) %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:BoundField DataField="observacao" HeaderText="observação">
            <HeaderStyle HorizontalAlign="Left" />
            </asp:BoundField>
        </Columns>
        <HeaderStyle CssClass="GridHeader" />
        <PagerStyle CssClass="pgr" />
        <SortedAscendingCellStyle CssClass="SortedAscendingCellStyle" />
        <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle" />
        <SortedDescendingCellStyle CssClass="SortedDescendingCellStyle" />
        <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle " />
    </asp:GridView>
</asp:Panel>
<asp:Panel ID="panelCadastro" runat="server" Visible="False">
    <table class="mGrid" width="50%">
        <tr>
            <th colspan="2">
                documento do projeto
            </th>
           
        </tr>
        <tr>
            <td class="esquerdo">
                documento:<asp:Label ID="txtCodigo" runat="server" Text="0" Visible="False"></asp:Label>
            </td>
            <td class="direito">
                <uc2:DdlDocumentoProjeto ID="DdlDocumentoProjeto1" runat="server" 
                    ValidationGroup="dependente" />
            </td>
        </tr>
        <tr>
            <td class="esquerdo">
                data recebimento:
            </td>
            <td class="direito">
                <cdt:cData ID="cData1" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="esquerdo">
                observação:
            </td>
            <td class="direito">
                <ctx:cTexto ID="cTextoObs" runat="server" MaxLength="100" TextMode="MultiLine" Width="350px" />
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
                                <asp:Button ID="btInserir0" runat="server" OnClick="btInserir_Click" Text="inserir"
                                    ValidationGroup="dependente" />
                                <asp:Button ID="btAlterar0" runat="server" OnClick="btAlterar_Click" Text="salvar"
                                    ValidationGroup="dependente" />
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