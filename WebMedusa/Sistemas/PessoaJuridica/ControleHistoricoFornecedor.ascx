<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControleHistoricoFornecedor.ascx.cs" Inherits="Medusa.Sistemas.PessoaJuridica.ControleHistoricoFornecedor"%> 

<table class="cadastro">
                        <tr>
                            <th colspan="2">
                            histórico do fornecedor
                            </th>
                        </tr>
                      <tr>
                        <td colspan="2">
                            <asp:GridView ID="grid" runat="server" 
                            AutoGenerateColumns="False" CssClass="tableView" Width="100%" 
                                Caption="Histórico" onrowediting="grid_RowEditing">
                                <Columns>
                                    <asp:BoundField DataField="data" HeaderText="data" SortExpression="data" />
                                    <asp:BoundField DataField="observacao" HeaderText="observação" 
                                        SortExpression="observacao" />
                                    <asp:TemplateField HeaderText="status" SortExpression="StatusFornecedor.nome">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("StatusFornecedor.nome") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("StatusFornecedor.nome") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="usuário" 
                                        SortExpression="Usuario.PessoaFisica.nome">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" 
                                                Text='<%# Bind("Usuario.PessoaFisica.nome") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" 
                                                Text='<%# Bind("Usuario.PessoaFisica.nome") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
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
                        </td>
                      </tr>
                      <tr>
                        <td>observação:<asp:Label ID="txtCodigo" runat="server" Text="0" Visible="False"></asp:Label>
                            </td>
                        <td class="direito">
                            <ctx:cTexto ID="cTextoObs" runat="server" Width="400" Height="80" 
                                TextMode="MultiLine" />
                        </td>
                      </tr>

                      <tr>
                        <td>status:</td>
                        <td class="direito">
                            <cDdlStatusFornecedores:cddlStatusFornecedores ID="cddlStatusFornecedores1" 
                                runat="server" />
                        </td>
                      </tr>

                      <tr>
                        <td colspan="2">
                            <asp:Label ID="lbMsg" runat="server" Text="[lbMsg]" Visible="False"></asp:Label>
                        </td>
                      </tr>

                        <tr>
                          <th colspan="2">
                          <div id="dGravacao" runat="server">
                              <asp:Button ID="btInserir" runat="server" onclick="btInserir_Click" 
                                  Text="inserir" ValidationGroup="historicofornecedor" />
                              <asp:Button ID="btCancelar" runat="server" onclick="btCancelar_Click" 
                                  Text="cancelar" CausesValidation="False" />
                                  </div>
                          </th>
                       </tr>
</table>

 


                

                