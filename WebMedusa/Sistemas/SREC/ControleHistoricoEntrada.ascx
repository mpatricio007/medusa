<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControleHistoricoEntrada.ascx.cs" Inherits="Medusa.Sistemas.SREC.ControleHistoricoEntrada" %>

<%@ Register src="../../Controles/DdlStatusAdiantamentos.ascx" tagname="DdlStatusAdiantamentos" tagprefix="uc1" %>

<%@ Register src="../../Controles/DdlStatusEntrada.ascx" tagname="DdlStatusEntrada" tagprefix="uc2" %>

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
                                    <asp:BoundField DataField="data" HeaderText="data" />
                                    <asp:TemplateField HeaderText="status">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox2" runat="server" 
                                                Text='<%# Bind("Usuario.PessoaFisica.nome") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" 
                                                Text='<%# Bind("Usuario.PessoaFisica.nome") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" 
                                                Text='<%# Bind("StatusEntrada.nome") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("StatusEntrada.nome") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="de">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox2" runat="server" 
                                                Text='<%# Bind("UsuarioDe.PessoaFisica.nome") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" 
                                                Text='<%# Bind("UsuarioDe.PessoaFisica.nome") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="para" DataField="StrDestinatarios" />
                                    <asp:BoundField DataField="obs" HeaderText="observação" />
                                </Columns>
                                <HeaderStyle HorizontalAlign="Left" />
                                <SortedAscendingCellStyle CssClass="SortedAscendingCellStyle" />
                                <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle" />
                                <SortedDescendingCellStyle CssClass="SortedDescendingCellStyle" />
                                <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle " />
                            </asp:GridView>
                        </asp:Panel>
<asp:Panel ID="panelCadastro" runat="server">
</asp:Panel> 
