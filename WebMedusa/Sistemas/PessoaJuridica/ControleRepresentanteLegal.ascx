<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControleRepresentanteLegal.ascx.cs" Inherits="Medusa.Sistemas.PessoaJuridica.ControleRepresentanteLegal" %>
                <%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
                


                            <asp:GridView ID="grid" runat="server" 
                            AutoGenerateColumns="False" CssClass="tableView" Width="100%" 
                                onrowediting="grid_RowEditing" Caption="lista de representantes legais">
                                <Columns>
                                    <asp:BoundField DataField="nome" HeaderText="nome" SortExpression="nome" />
                                    <asp:BoundField DataField="cpf" HeaderText="cpf" 
                                        SortExpression="cpf" />
                                    <asp:BoundField DataField="rg" HeaderText="rg" SortExpression="rg" />
                                    <asp:TemplateField HeaderText="e-mail" SortExpression="email.value">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("email.value") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("email.value") %>'></asp:Label>
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
                <table class="cadastro">
                       <tr>
                <th colspan="2">
                representantes legais
                </th>
                </tr>
                      <tr>
                        <td class="esquerdo">nome:<asp:Label ID="txtCodigo" runat="server" Text="0" Visible="False"></asp:Label>
                            </td>
                        <td class="direito">
                            <ctx:cTexto ID="cTextoNome" runat="server" Width="350" 
                                ValidationGroup="representanteLegal" MaxLength="100" />
                          </td>
                      </tr>

                      <tr>
                        <td class="esquerdo">cpf:</td>
                        <td class="direito">
                            <cCpf:cCPF ID="cCPF" runat="server" ValidationGroup="representanteLegal" />
                          </td>
                      </tr>

                      <tr>
                        <td class="esquerdo">rg:</td>
                        <td class="direito">
                            <ctx:cTexto ID="cTextoRg" runat="server" MaxLength="15" Width="160px" 
                                ValidationGroup="representanteLegal" />
                          </td>
                      </tr>

                      <tr>
                        <td class="esquerdo">e-mail:</td>
                        <td class="direito">
                            <cem:cEmail ID="cEmail" runat="server" ValidationGroup="representanteLegal" />
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
                                  Text="inserir" ValidationGroup="representanteLegal" />
                              <asp:Button ID="btAlterar" runat="server" onclick="btAlterar_Click" 
                                  Text="salvar" ValidationGroup="representanteLegal" />
                              <asp:Button ID="btExcluir" runat="server" onclick="btExcluir_Click" 
                                  Text="excluir" CausesValidation="False" />
                              <asp:Button ID="btCancelar" runat="server" onclick="btCancelar_Click" 
                                  Text="cancelar" CausesValidation="False" />
                          </th>

                          </div>

                      </tr>
</table>

 




