<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControleTabelaTaxas.ascx.cs" Inherits="Medusa.Sistemas.SCFP.ControlesSCFP.ControleTabelaTaxas" %>


<%--<table class="cadastro">
                        <tr>
                            <th colspan="2">
                                histórico do adiantamento</th>
                        </tr>
</table>--%>
<asp:Panel ID="panelCadastro" runat="server">
    <table class="cadastro" width="100%">

        <tr>
            <td class="esquerdo">início:<asp:Label ID="txtCodigo" runat="server" Text="0" Visible="False"></asp:Label>
            </td>
            <td class="direito">
                <cdt:cData ID="cDataInicio" runat="server" />
            </td>
        </tr>

        <tr>
            <td class="esquerdo">término:</td>
            <td class="direito">
                <cdt:cData ID="cDataFim" runat="server" />
            </td>
        </tr>

        <tr>
            <td class="esquerdo">cumulativo mensal:</td>
            <td class="direito">
                <asp:CheckBox ID="ckCumulativoMensal" runat="server" />
            </td>
        </tr>

        <tr>
            <td class="esquerdo">faixas:</td>
            <td class="direito">
                <table Width="100%">
                    <tr>
                        <td>de:</td><td><cvl:cValor ID="cValorDe" runat="server" />
                        </td>
                        <td>até:</td><td><cvl:cValor ID="cValorAte" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>valor máximo:</td><td><cvl:cValor ID="cValorMax" runat="server" />
                        </td>
                        <td>valor minímo:</td><td><cvl:cValor ID="cValorMin" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>alíquota:</td><td><cvl:cValor ID="cValorAliquota" runat="server" />
                        </td>
                        <td>dedução:</td><td><cvl:cValor ID="cValorDeducao" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btAdd" runat="server" Text="adicionar" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">

                            <asp:GridView ID="gridFaixas" runat="server" CssClass="mGrid" AutoGenerateColumns="False"
                                Width="100%" OnRowDeleting="gridFaixas_RowDeleting">
                                <Columns>
                                    <asp:BoundField DataField="faixa_de" HeaderText="de">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="faixa_ate" HeaderText="até">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="valor_max" HeaderText="máximo">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="vlr_minimo" HeaderText="mínimo">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="deducao" HeaderText="dedução">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="aliquota" HeaderText="alíquota">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibDelete" runat="server" ImageUrl="~/Styles/img/delete_img.png"
                                                CausesValidation="false" CommandName="Delete" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                        </td>
                    </tr>
                </table>
            </td>
        </tr>

        <tr>
            <td colspan="2">
                <asp:Label ID="lblMsg" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>

        <tr>
            <th colspan="2">
                <div id="dGravacao" runat="server">
                    <asp:Button ID="btInserir" runat="server" Text="próximo" ValidationGroup="historicoadiantamento"
                        OnClick="btInserir_Click" />
                    <asp:Button ID="btCloseDialog" runat="server" OnClientClick="closeDialog();" Text="concluído" />
                </div>
            </th>
        </tr>
    </table>
</asp:Panel>
