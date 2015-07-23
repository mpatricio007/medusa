<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControleReferenciaBancaria.ascx.cs" Inherits="Medusa.Sistemas.PessoaJuridica.ControleReferenciaBancaria" %>
 <%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
               

                            <asp:GridView ID="grid" runat="server" 
                            AutoGenerateColumns="False" CssClass="tableView" Width="100%" 
                                onrowediting="grid_RowEditing" Caption="lista de referências bancárias">
                                <Columns>
                                    <asp:BoundField DataField="banco" HeaderText="banco" SortExpression="banco" />
                                    <asp:BoundField DataField="agencia" HeaderText="agência" 
                                        SortExpression="agencia" />
                                    <asp:BoundField DataField="contato" HeaderText="contato" 
                                        SortExpression="contato" />
                                    <asp:BoundField DataField="telefone" HeaderText="telefone" />
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
               <table class="cadastro">
                      <tr>
                        <th colspan="2">
                            referências bancárias</th>
                        </tr>
                      <tr>
                        <td class="esquerdo">banco:<asp:Label ID="txtCodigo" runat="server" Text="0" Visible="False"></asp:Label>
                            </td>
                        <td class="direito">
                            <ctx:cTexto ID="cTextoBanco" runat="server" Width="350" 
                                ValidationGroup="referenciaBancaria" MaxLength="100" />
                          </td>
                      </tr>

                      <tr>
                        <td class="esquerdo">agência:</td>
                        <td class="direito">
                            <ctx:cTexto ID="cTextoAgencia" runat="server" 
                                ValidationGroup="referenciaBancaria" MaxLength="10" />
                          </td>
                      </tr>

                      <tr>
                        <td class="esquerdo">contato:</td>
                        <td class="direito">
                            <ctx:cTexto ID="cTextoContato" runat="server" MaxLength="50" Width="300px" 
                                ValidationGroup="referenciaBancaria" />
                          </td>
                      </tr>

                      <tr>
                        <td class="esquerdo">telefone:</td>
                        <td class="direito">
                            <ctel:cTelefone ID="cTelefone1" runat="server" 
                                ValidationGroup="referenciaBancaria" />
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
                                  Text="inserir" ValidationGroup="referenciaBancaria" />
                              <asp:Button ID="btAlterar" runat="server" onclick="btAlterar_Click" 
                                  Text="salvar" ValidationGroup="referenciaBancaria" />
                              <asp:Button ID="btExcluir" runat="server" onclick="btExcluir_Click" 
                                  Text="excluir" CausesValidation="False" />
                              <asp:Button ID="btCancelar" runat="server" onclick="btCancelar_Click" 
                                  Text="cancelar" CausesValidation="False" />
                          </th>
                          </div>
                       </tr>
</table>