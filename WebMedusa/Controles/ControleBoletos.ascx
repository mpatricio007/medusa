<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControleBoletos.ascx.cs" Inherits="Medusa.Controles.ControleBoletos" %>
 <div class="conteudo">

               
                <asp:Panel ID="panelGrid" runat="server">
                          
                            
                    <asp:GridView ID="grid" runat="server" AllowPaging="True" AllowSorting="True" 
                        AutoGenerateColumns="False" Caption="Lista de Boletos de Cobrança" 
                        CssClass="tableView" 
                        onpageindexchanging="grid_PageIndexChanging" onrowcreated="grid_RowCreated" 
                        onrowediting="grid_RowEditing" onsorting="grid_Sorting" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="codigo" HeaderText="código" 
                                SortExpression="codigo">
                            </asp:BoundField>
                            <asp:BoundField DataField="valor" HeaderText="valor" 
                                SortExpression="valor" DataFormatString="{0:N2}">
                            </asp:BoundField>
                            <asp:BoundField DataField="data_vencto" DataFormatString="{0:d}" 
                                HeaderText="data de vencto." SortExpression="data_vencto" />
                            <asp:BoundField DataField="valor_pgto" DataFormatString="{0:N2}" 
                                HeaderText="valor pago" SortExpression="valor_pgto" />
                            <asp:BoundField DataField="data_pgto" DataFormatString="{0:d}" 
                                HeaderText="data pagto." SortExpression="data_pgto" />
                            <asp:CommandField EditText="selecionar" ShowEditButton="True">
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:CommandField>
                        </Columns>
                        <HeaderStyle HorizontalAlign="Left" />
                        <SortedAscendingCellStyle 
                            CssClass="SortedAscendingCellStyle" />
                        <SortedAscendingHeaderStyle 
                            CssClass="SortedAscendingHeaderStyle" />
                        <SortedDescendingCellStyle 
                            CssClass="SortedDescendingCellStyle" />
                        <SortedDescendingHeaderStyle 
                            CssClass="SortedDescendingHeaderStyle " />
                    </asp:GridView>
                </asp:Panel>
                <asp:Panel ID="panelCadastro" runat="server">
                    <table class="cadastro">
                        <tr>
                            <th colspan="1">
                                cadastro de boletos</th>
                            <th colspan="1">
                            <div id="dGravacao" runat="server">
                                <asp:Button ID="btInserir" runat="server" onclick="btInserir_Click" 
                                    Text="inserir" ValidationGroup="atuacao" />
                                <asp:Button ID="btAlterar" runat="server" onclick="btAlterar_Click" 
                                    Text="salvar" ValidationGroup="atuacao" />
                                <asp:Button ID="btExcluir" runat="server" CausesValidation="False" 
                                    onclick="btExcluir_Click" Text="excluir" />
                                <asp:Button ID="btCancelar" runat="server" CausesValidation="False" 
                                    onclick="btCancelar_Click" Text="cancelar" />
                                    </div>
                            </th>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                evento:
                                <asp:Label ID="txtCodigo" runat="server" Text="0" Visible="False"></asp:Label>
                                </td>
                            <td class="direito">
                                <asp:Label ID="lbEvento" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                valor:</td>
                            <td class="direito">
                                <cvl:cValor ID="cValor" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                data de vencimento:</td>
                            <td class="direito">
                                <cdt:cData ID="cDataVencto" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                data de pagamento:</td>
                            <td class="direito">
                                <cdt:cData ID="cDataPagto" runat="server" EnableValidator="False" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                valor pago:</td>
                            <td class="direito">
                                <cvl:cValor ID="cValorPagto" runat="server" EnableValidator="False" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                código:</td>
                            <td class="direito">
                                <asp:Label ID="lbCodigo" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                observação:</td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoObs" runat="server" EnableValidator="False" Height="50px" 
                                    TextMode="MultiLine" Width="300px" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th colspan="2">
                            <div id="dGravacao1" runat="server">
                                <asp:Button ID="btInserir0" runat="server" onclick="btInserir_Click" 
                                     Text="inserir" ValidationGroup="atuacao" />
                                <asp:Button ID="btAlterar0" runat="server" onclick="btAlterar_Click" 
                                    Text="salvar" ValidationGroup="atuacao" />
                                <asp:Button ID="btExcluir0" runat="server" CausesValidation="False" 
                                    onclick="btExcluir_Click" Text="excluir" />
                                <asp:Button ID="btCancelar0" runat="server" CausesValidation="False" 
                                    onclick="btCancelar_Click" Text="cancelar" />
                                    </div>
                            </th>
                        </tr>
                    </table>
                           </asp:Panel>
    </div>