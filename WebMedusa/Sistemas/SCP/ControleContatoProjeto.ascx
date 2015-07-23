<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControleContatoProjeto.ascx.cs" Inherits="Medusa.Sistemas.SCP.ControleContatoProjeto" %>
<div id="dConteudo" runat="server">
<div>
    &nbsp;<asp:Button ID="btCriar" runat="server" CausesValidation="False" OnClick="btCriar_Click"
        Text="novo contato"  />
</div>
<asp:Panel ID="panelGrid" runat="server">
    <asp:GridView ID="grid" runat="server" AutoGenerateColumns="False" AllowPaging="True"
        AllowSorting="True" OnPageIndexChanging="grid_PageIndexChanging" OnSorting="grid_Sorting"
        Width="50%" EmptyDataText="não há contatos cadastrados" OnRowCreated="grid_RowCreated"
        OnRowEditing="grid_RowEditing" EnableTheming="False" CssClass="mGrid" PagerStyle-CssClass="pgr" 
        AlternatingRowStyle-CssClass="alt">
        <AlternatingRowStyle />
        <Columns>
            <asp:CommandField EditText="selecionar" ShowEditButton="True">
                <ItemStyle HorizontalAlign="Left" />
            </asp:CommandField>
            <asp:TemplateField HeaderText="nome">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" 
                        Text='<%# Bind("Contato.PessoaFisica.nome") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" 
                        Text='<%# Bind("Contato.PessoaFisica.nome") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="email">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Contato.email") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Contato.email") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="telefone">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Contato.telefone") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("Contato.telefone") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerStyle CssClass="pgr" />
        <SortedAscendingCellStyle CssClass="SortedAscendingCellStyle" />
        <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle" />
        <SortedDescendingCellStyle CssClass="SortedDescendingCellStyle" />
        <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle " />
    </asp:GridView>
</asp:Panel>
<asp:Panel ID="panelCadastro" runat="server" Visible="False">
    <table width="50%" class="mGrid" cellspacing="0">
        <tr>
            <th colspan="2">
                contato do projeto
            </th>
        </tr>
        <tr>
            <td class="esquerdo">
                contato:<asp:Label ID="txtCodigo" runat="server" Text="0" Visible="False"></asp:Label>
            </td>
            <td class="direito">
                <cDdlContato:cDdlContato ID="cDdlContato1" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div id="dNotificacoes" runat="server">
                    <table class="gridv" width="100%" cellspacing="0" border="0" cellpadding="0">
                        <tr>
                            <th colspan="2">
                                notificações
                            </th>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:GridView ID="GridSolDeProj" runat="server" AutoGenerateColumns="False" Width="300px"
                                    EmptyDataText="nenhuma solicitação disponivel" CssClass="gridv"
                                    EnableTheming="False" CellPadding="3" BackColor="White" 
                                    BorderColor="#666666" BorderStyle="Solid" BorderWidth="1px" 
                                    onrowediting="GridSolDeProj_RowEditing">
                                    <Columns>
                                        <asp:BoundField DataField="nome" HeaderText="solicitação" SortExpression="nome"/>
                                        <asp:CommandField EditText="incluir" ShowEditButton="True" >
                                        <ItemStyle ForeColor="Black" />
                                        </asp:CommandField>
                                    </Columns>
                                    <EditRowStyle BorderColor="#333333" BorderStyle="Solid" />
                                    <FooterStyle BackColor="White" ForeColor="#333333" />
                                    <HeaderStyle BackColor="Red" Font-Bold="True" ForeColor="White" 
                                        HorizontalAlign="Left" />
                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                    <RowStyle ForeColor="#000066" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" BorderColor="#333333" />
                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                                </asp:GridView>
                            </td>
                            <td align="right">
                                <asp:GridView ID="GridSolDeProjThis" runat="server" AutoGenerateColumns="False" Width="300px"
                                    CssClass="gridv" OnRowEditing="GridSolDeProjThis_RowEditing" EnableTheming="False" 
                                    EmptyDataText="este contato não recebe nenhuma notificação" 
                                    CellPadding="3" BackColor="White" BorderColor="#666666" BorderStyle="None" 
                                    BorderWidth="1px">
                                    <Columns>
                                        <asp:TemplateField HeaderText="solicitação" SortExpression="SolicitacaoDeProjeto.nome">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("SolicitacaoDeProjeto.nome") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("SolicitacaoDeProjeto.nome") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:CommandField EditText="excluir" ShowEditButton="True" >
                                        <ItemStyle HorizontalAlign="Left" ForeColor="Black" />
                                        </asp:CommandField>
                                    </Columns>
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <HeaderStyle BackColor="#00CC00" Font-Bold="True" ForeColor="White" 
                                        HorizontalAlign="Left" />
                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                    <RowStyle ForeColor="#000066" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th colspan="2">
                <table>
                    <tr>
                        <td>
                            <div id="dGravacao" runat="server">
                                <asp:Button ID="btInserir0" runat="server" OnClick="btInserir_Click" Text="inserir"
                                    ValidationGroup="contato" />
                                <asp:Button ID="btAlterar0" runat="server" OnClick="btAlterar_Click" Text="salvar"
                                    ValidationGroup="contato" />
                                <asp:Button ID="btExcluir0" runat="server" CausesValidation="False" OnClick="btExcluir_Click"
                                    Text="excluir" />
                                </div>
                        </td>
                        <td>
                            <asp:Button ID="btCancelar0" runat="server" CausesValidation="False" OnClick="btCancelar_Click"
                                    Text="cancelar" />
                        </td>
                    </tr>
                </table>
            </th>
        </tr>
    </table>
</asp:Panel>
</div>