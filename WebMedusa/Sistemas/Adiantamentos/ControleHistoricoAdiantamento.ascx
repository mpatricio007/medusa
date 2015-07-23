<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControleHistoricoAdiantamento.ascx.cs" Inherits="Medusa.Sistemas.Adiantamentos.ControleHistoricoAdiantamento" %>

<%@ Register src="../../Controles/DdlStatusAdiantamentos.ascx" tagname="DdlStatusAdiantamentos" tagprefix="uc1" %>

<%--<table class="cadastro">
                        <tr>
                            <th colspan="2">
                                histórico do adiantamento</th>
                        </tr>
</table>--%>
                        <asp:Panel ID="panelGrid" runat="server">

                            <asp:GridView ID="grid" runat="server" 
                            AutoGenerateColumns="False" CssClass="tableView" Width="100%" 
                                Caption="Histórico" onrowediting="grid_RowEditing">
                                <Columns>
                                    <asp:BoundField DataField="data" HeaderText="data" SortExpression="data" 
                                        DataFormatString="{0:d}" />
                                    <asp:BoundField DataField="observacao" HeaderText="observação" 
                                        SortExpression="observacao" />
                                    <asp:TemplateField HeaderText="status" SortExpression="statusAdiantamento.nome">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" 
                                                Text='<%# Bind("statusAdiantamento.nome") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" 
                                                Text='<%# Bind("statusAdiantamento.nome") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="conferente" 
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
                                    <asp:TemplateField HeaderText="setor responsável" SortExpression="Setor.sigla">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Setor.sigla") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("Setor.sigla") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle HorizontalAlign="Left" />
                                <SortedAscendingCellStyle CssClass="SortedAscendingCellStyle" />
                                <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle" />
                                <SortedDescendingCellStyle CssClass="SortedDescendingCellStyle" />
                                <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle " />
                            </asp:GridView>
                        </asp:Panel>
<asp:Panel ID="panelCadastro" runat="server">
<table class="cadastro">

                      <tr>
                        <td>observação:<asp:Label ID="txtCodigo" runat="server" Text="0" Visible="False"></asp:Label>
                            </td>
                        <td class="direito">
                            <ctx:cTexto ID="cTextoObs" runat="server" Width="400" Height="80" 
                                TextMode="MultiLine" MaxLength="200" EnableValidator="False" />
                        </td>
                      </tr>

                      <tr>
                        <td>status:</td>
                        <td class="direito">
                            <uc1:DdlStatusAdiantamentos ID="DdlStatusAdiantamentos" runat="server" 
                                ValidationGroup="historicoadiantamento" OnSelectedIndexChanged="DdlStatusAdiantamentos_SelectedIndexChanged" AutoPostBack="true" Width="200" />
                            <cddlSetor:cDdlSetor ID="cDdlSetor1" runat="server" EnabledValidator="false"
                                ValidationGroup="historicoadiantamento" Width="200" /><cdt:cData ID="cDataProrrogacao" runat="server" ValidationGroup="historicoadiantamento" />
                        </td>
                      </tr>

                      <tr>
                        <td colspan="2">
                            <asp:Label ID="lblMsg" runat="server" Text="[lbMsg]" Visible="False"></asp:Label>
                        </td>
                      </tr>

                      <tr>
                          <th colspan="2">
                              <div id="dGravacao" runat="server">
                                  <asp:Button ID="btInserir" runat="server" Text="inserir" ValidationGroup="historicoadiantamento"
                                      OnClick="btInserir_Click" />
                                  <asp:Button ID="btCancelar" runat="server" OnClick="btCancelar_Click" Text="cancelar"
                                      CausesValidation="False" />
                              </div>
                          </th>
                      </tr>
</table>
</asp:Panel> 
