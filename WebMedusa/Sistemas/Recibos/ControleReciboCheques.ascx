<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControleReciboCheques.ascx.cs" Inherits="Medusa.Sistemas.Recibos.ControleReciboCheques" %>
 <%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

 <asp:Panel ID="panelGrid" runat="server">
<asp:GridView ID="gridCheque" runat="server" 
                            AutoGenerateColumns="False" CssClass="tableView" Width="100%" 
    Caption="lista de cheques" onrowediting="grid_RowEditing1">
                                <Columns>
                                    <asp:BoundField DataField="num_cheque" HeaderText="cheque nº" 
                                        SortExpression="num_cheque" >
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="data" HeaderText="data" 
                                        SortExpression="data" DataFormatString="{0:d}" >
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="valor" HeaderText="valor" SortExpression="valor" 
                                        DataFormatString="{0:c}" >
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:CommandField ShowEditButton="True" EditText="selecionar">                                   
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:CommandField>
                                </Columns>
                                <HeaderStyle HorizontalAlign="Left" />
                                <SortedAscendingCellStyle CssClass="SortedAscendingCellStyle" />
                                <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle" />
                                <SortedDescendingCellStyle CssClass="SortedDescendingCellStyle" />
                                <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle " />
                            </asp:GridView>
</asp:Panel>
<asp:Panel ID="pCadastro" runat="server">
                            <table class="cadastro">
                            <tr>
                <th colspan="2">
                    cadastro de cheques&nbsp;</th>
                </tr>
                <tr>
                        <td class="esquerdo">cheque nº:<asp:Label ID="txtCodigo" runat="server" Text="0" Visible="False"></asp:Label>
                            </td>
                        <td class="direito">
                            <cint:cInteiro ID="cIntNumCheque" runat="server" ValidationGroup="cheque" 
                                MaxLength="50" />
                          </td>
                      </tr>
                <tr>
                        <td class="esquerdo">data:</td>
                        <td class="direito">
                            <cdt:cData ID="cData" runat="server" ValidationGroup="cheque" />
                          </td>
                      </tr>
                <tr>
                        <td class="esquerdo">valor:</td>
                        <td class="direito">
                            <cvl:cValor ID="cValor" runat="server" ValidationGroup="cheque" />
                          </td>
                      </tr>
                <tr>
                   <td colspan="2">
                            <asp:Label ID="lblMsg" runat="server" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                 <div id="dGravacao" runat="server">
                   <th colspan="2">
                     <asp:Button ID="btInserir" runat="server" onclick="btInserir_Click" 
                       Text="inserir" ValidationGroup="cheque" />
                       <asp:Button ID="btAlterar" runat="server" onclick="btAlterar_Click" 
                       Text="salvar" ValidationGroup="cheque" />
                       <asp:Button ID="btExcluir" runat="server" onclick="btExcluir_Click" 
                       Text="excluir" CausesValidation="False" ValidationGroup="cheque" />
                       <asp:Button ID="btCancelar" runat="server" onclick="btCancelar_Click" 
                       Text="cancelar" CausesValidation="False" ValidationGroup="cheque" />
                    </th>
                  </div>
                </tr>
                            </table>
</asp:Panel>