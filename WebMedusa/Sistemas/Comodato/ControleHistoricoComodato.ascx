<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControleHistoricoComodato.ascx.cs" Inherits="Medusa.Sistemas.Comodato.ControleHistoricoComodato" %>
                
<%@ Register src="../../Controles/DdlStatusComodato.ascx" tagname="DdlStatusComodato" tagprefix="uc1" %>
                
<table class="cadastro">
                        <tr>
                            <th colspan="2">
                                histórico do comodato</th>
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
                                    <asp:TemplateField HeaderText="status" SortExpression="StatusComodatos.nome">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" 
                                                Text='<%# Bind("StatusComodatos.nome") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("StatusComodatos.nome") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
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
                        <td>status:</td>
                        <td class="direito" style="margin-left: 40px">
                            <uc1:DdlStatusComodato ID="DdlStatusComodato" runat="server" 
                                ValidationGroup="historicoComodato" />
                        </td>
                      </tr>
                             <tr>
                        <td>observação:<asp:Label ID="txtCodigo" runat="server" Text="0" Visible="False"></asp:Label>
                            </td>
                        <td class="direito">
                            <ctx:cTexto ID="cTextoObs" runat="server" Width="400px" Height="80" 
                                TextMode="MultiLine" EnableValidator="False" />
                        </td>
                      </tr>

                      <tr>
                        <td colspan="2">
                            <asp:Label ID="lbMsg" runat="server" Text="[lbMsg]" Visible="False"></asp:Label>
                        </td>
                      </tr>

                        <tr>
                          <th colspan="2">
                          <div id="dGravação2" runat="server">
                              <asp:Button ID="btInserir" runat="server" onclick="btInserir_Click" 
                                  Text="inserir" ValidationGroup="historicoComodato" />
                              <asp:Button ID="btCancelar" runat="server" onclick="btCancelar_Click" 
                                  Text="cancelar" CausesValidation="False" />
                                  </div>
                          </th>
                       </tr>
</table>


