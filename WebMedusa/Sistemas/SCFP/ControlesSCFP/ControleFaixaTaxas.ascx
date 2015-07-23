<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControleFaixaTaxas.ascx.cs"
    Inherits="Medusa.Sistemas.SCFP.ControleFaixaTaxas" %>
<div class="conteudo">
    <asp:Panel ID="panelGrid" runat="server">

        <asp:GridView ID="grid" runat="server" CssClass="mGrid" AutoGenerateColumns="False" Width="100%" DataKeyNames="id_faixa_taxa"
            EmptyDataText="nenhuma faixa cadastrada para esta tabela" Caption="Faixas"
            OnRowEditing="grid_RowEditing">
            <Columns>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:ImageButton ID="ibEdit" runat="server" ImageUrl="~/Styles/img/Edit.png"
                            CausesValidation="false" CommandName="Edit" ToolTip="editar" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="10px" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:BoundField DataField="faixa_de" HeaderText="de" DataFormatString="{0:c}">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="faixa_ate" HeaderText="até" DataFormatString="{0:c}">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="valor_max" HeaderText="máximo" DataFormatString="{0:c}">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="vlr_minimo" HeaderText="mínimo" DataFormatString="{0:c}">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="deducao" HeaderText="dedução" DataFormatString="{0:c}">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="aliquota" HeaderText="alíquota" DataFormatString="{0:n}">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
            </Columns>
            <HeaderStyle HorizontalAlign="Left" />
        <SortedAscendingCellStyle CssClass="SortedAscendingCellStyle" />
        <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle" />
        <SortedDescendingCellStyle CssClass="SortedDescendingCellStyle" />
        <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle " />
        </asp:GridView>
    </asp:Panel>
</div>
<asp:Panel ID="panelCadastro" runat="server" Width="100%">
    <table width="100%" class="mCad">
        <tr>
            <td>
                faixa de:<asp:Label ID="txtCodigo" runat="server" Text="0" Visible="False"></asp:Label><br />
                <ctx:cTexto ID="cTextoFaixaDe" runat="server" ValidationGroup= '<%# ValidationGroup %>' Width="120px" />
            </td>
            <td>
                faixa até:<br />
                <ctx:cTexto ID="cTextoFaixaAte" runat="server" ValidationGroup= '<%# ValidationGroup %>' Width="120px" />
            </td>
            <td>
                valor máximo:<br />
                <ctx:cTexto ID="cTextoMax" runat="server" EnableValidator="false" Width="120px" />
            </td>
            <td>
                valor mínimo:<br />
                <ctx:cTexto ID="cTextoMin" runat="server" ValidationGroup= '<%# ValidationGroup %>' Width="120px" />
            </td>
            <td>
                dedução:<br />
                <ctx:cTexto ID="cTextoDeducao" runat="server" ValidationGroup= '<%# ValidationGroup %>' Width="120px" />
            </td>
            <td>
                aliquota:<br />
                <ctx:cTexto ID="cTextoAliquota" runat="server" ValidationGroup= '<%# ValidationGroup %>' Width="120px" />
            </td>
        </tr>
        <tr>
            <td colspan="6">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <th colspan="6">
                <div id="dGravacao1">
                    <asp:Button ID="btInserir0" runat="server" OnClick="btInserir_Click"
                        Text="inserir" ValidationGroup= '<%# ValidationGroup %>' />
                    <asp:Button ID="btAlterar0" runat="server" OnClick="btAlterar_Click"
                        Text="salvar" ValidationGroup= '<%# ValidationGroup %>' />
                    <asp:Button ID="btExcluir0" runat="server" CausesValidation="False" OnClick="btExcluir_Click"
                        Text="excluir" />
                    <asp:Button ID="btCancelar0" runat="server" CausesValidation="False" OnClick="btCancelar_Click"
                        Text="cancelar" />
                </div>
            </th>
        </tr>
    </table>

</asp:Panel>
<p>
    &nbsp;</p>

