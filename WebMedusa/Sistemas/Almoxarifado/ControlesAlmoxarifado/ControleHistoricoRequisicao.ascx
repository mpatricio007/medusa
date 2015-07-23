<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControleHistoricoRequisicao.ascx.cs"
    Inherits="Medusa.Sistemas.Almoxarifado.ControleHistoricoRequisicao" %>
<%@ Register src="../../../Controles/DdlStatusRequisicaoMaterial.ascx" tagname="DdlStatusRequisicaoMaterial" tagprefix="uc1" %>
<asp:Panel ID="pAll" runat="server">
    <table class="cadastro">
        <tr>
            <td>
                <asp:Button ID="btDesfazer" runat="server" Text="desfazer atendimento" OnClick="btDesfazer_Click"
                    CausesValidation="false" />
            </td>
        </tr>
    </table>
    <asp:GridView ID="grid" runat="server" AutoGenerateColumns="False" CssClass="mGrid" ForeColor="White"
        Width="100%" Caption="Histórico">
        <Columns>
            <asp:BoundField DataField="data" HeaderText="data" SortExpression="data" />
            <asp:BoundField DataField="quantidade" HeaderText="quantidade atendida" SortExpression="quantidade" />
            <asp:BoundField DataField="observacao" HeaderText="observação" SortExpression="observacao" />
            <asp:TemplateField HeaderText="usuário" SortExpression="Usuario.PessoaFisica.nome">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Usuario.PessoaFisica.nome") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Usuario.PessoaFisica.nome") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="status" SortExpression="StatusRequisicaoMaterial.nome">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("StatusRequisicaoMaterial.nome") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("StatusRequisicaoMaterial.nome") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <HeaderStyle HorizontalAlign="Left" />
        <SortedAscendingCellStyle CssClass="SortedAscendingCellStyle" />
        <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle" />
        <SortedDescendingCellStyle CssClass="SortedDescendingCellStyle" />
        <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle " />
    </asp:GridView>
    <table class="mCad" id="tbMateriais" runat="server" width="100%">
        <tr>
            <td>
                status:
            </td>
            <td class="direito" style="margin-left: 40px">
                <uc1:DdlStatusRequisicaoMaterial ID="DdlStatusRequisicaoMaterial1" 
                    runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                quantidade:
            </td>
            <td class="direito" style="margin-left: 40px">
                <cint:cInteiro ID="cIntQtde" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                observação:<asp:Label ID="txtCodigo" runat="server" Text="0" Visible="False"></asp:Label>
            </td>
            <td class="direito">
                <ctx:cTexto ID="cTextoObs" runat="server" Width="450px" Height="80" TextMode="MultiLine"
                    ValidationGroup="cancel" MaxLength="200" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lbMsg" runat="server" Text="[lbMsg]"></asp:Label>
            </td>
        </tr>
        <tr>
            <th colspan="2">
                <div id="dGravação2" runat="server">
                    <asp:Button ID="btInserir" runat="server" OnClick="btInserir_Click" Text="inserir"
                        ValidationGroup="historicoRequisicao" Width="53px" />
                    <asp:Button ID="btCancelar" runat="server" OnClick="btCancelar_Click" Text="cancelar"
                        CausesValidation="False" />
                </div>
            </th>
        </tr>
    </table>
</asp:Panel>
