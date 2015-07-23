<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControleHistorico.ascx.cs" Inherits="Medusa.Sistemas.SCP.ControleHistorico" %>


<%@ Register src="../../Controles/DdlStatusSolicitacao.ascx" tagname="DdlStatusSolicitacao" tagprefix="uc1" %>


<table class="cadastro">
  <tr>
                            <th colspan="2">
                                histórico de solicitação do projeto</th>
                        </tr>
                        <tr>
                        <td colspan="2">
                            <asp:GridView ID="grid" runat="server" 
                                AutoGenerateColumns="False" Caption="Histórico" CssClass="tableView" 
                                onrowediting="grid_RowEditing" Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="data" DataFormatString="{0:d}" HeaderText="data" />
                                    <asp:BoundField DataField="observacao" HeaderText="observação" />
                                    <asp:TemplateField HeaderText="status">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" 
                                                Text='<%# Bind("StatusSolicitacao.descricao") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" 
                                                Text='<%# Bind("StatusSolicitacao.descricao") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="usuário">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox2" runat="server" 
                                                Text='<%# Bind("Usiario.PessoaFisica.nome") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" 
                                                Text='<%# Bind("Usuario.PessoaFisica.nome") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField EditText="selecionar" ShowEditButton="True">
                                    <HeaderStyle HorizontalAlign="Right" />
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
                        <asp:Panel ID="pCadastro" runat="server">
                        <tr>
                            <td class="esquerdo">
                                observação:
                                <asp:Label ID="txtCodigo" runat="server" Text="0" Visible="False"></asp:Label>
                            </td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoObs" runat="server" EnableValidator="True" Height="50px" 
                                    TextMode="MultiLine" Width="300px" ValidationGroup="historico" 
                                    MaxLength="100" />
                            </td>
                        </tr>
                           <tr>
                            <td class="esquerdo">
                                status:</td>
                            <td class="direito">
                                <uc1:DdlStatusSolicitacao ID="cDdlStatusSolicitacao1" runat="server" 
                                    ValidationGroup="historico" />
                               </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lbMsg" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th colspan="2">
                            <div ID="dGravacao2" runat="server">
                                <asp:Button ID="btInserir" runat="server" onclick="btInserir_Click" 
                                     Text="inserir" ValidationGroup="historico" />
                                <asp:Button ID="btAlterar" runat="server" onclick="btAlterar_Click" 
                                    Text="salvar" ValidationGroup="historico" />
                                <asp:Button ID="btExcluir" runat="server" CausesValidation="False" 
                                    onclick="btExcluir_Click" Text="excluir" />
                                <asp:Button ID="btCancelar" runat="server" CausesValidation="False" 
                                    onclick="btCancelar_Click" Text="cancelar" Height="26px" />
                                    </div>
                            </th>
                        </tr>
                    </asp:Panel>
                    </table>