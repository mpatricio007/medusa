<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControleHistoricoProjeto.ascx.cs"
    Inherits="Medusa.Sistemas.SCP.ControleHistoricoProjeto" %>

<div id="dConteudo" runat="server">

<table class="mGrid" width="80%">
    <tr>
        <td colspan="2">
         
   
 <asp:Panel ID="panelGrid" runat="server">
     <asp:GridView ID="grid" runat="server" AutoGenerateColumns="False" Caption="Histórico das mudanças do status do projeto"
                OnRowEditing="grid_RowEditing" Width="100%" EnableTheming="False" CssClass="mGrid"
                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
<AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                <Columns>
                    <asp:TemplateField HeaderText="data">
                        <ItemTemplate>
                             <asp:Label ID="Label3" runat="server" Text='<%# Medusa.LIB.Util.DateToString(Convert.ToDateTime(Eval("data"))) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="observacao" HeaderText="observação" />
                    <asp:TemplateField HeaderText="status" SortExpression="StatusProjeto.nome">
                       
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("StatusProjeto.nome") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="usuário">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Usiario.PessoaFisica.nome") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Usuario.PessoaFisica.nome") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>

                <HeaderStyle HorizontalAlign="Left" />

<PagerStyle CssClass="pgr"></PagerStyle>
            </asp:GridView>
          </asp:Panel>
  

        </td>
    </tr>
        <tr>
            <td class="esquerdo">
                observação:
                <asp:Label ID="txtCodigo" runat="server" Text="0" Visible="False"></asp:Label>
            </td>
            <td class="direito">
                <ctx:cTexto ID="cTextoObs" runat="server" EnableValidator="True" Height="50px" TextMode="MultiLine"
                    Width="300px" ValidationGroup="historico" MaxLength="100" />
            </td>
        </tr>
        <tr>
            <td class="esquerdo">
                status:
            </td>
            <td class="direito">
                <cDdlStatusProjeto:cDdlStatusProjeto ID="cDdlStatusProjeto1" runat="server" ValidationGroup="historico" />
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
                                <asp:Button ID="btInserir" runat="server" OnClick="btInserir_Click" Text="inserir"
                                    ValidationGroup="historico" />
                                <%--<asp:Button ID="btAlterar" runat="server" onclick="btAlterar_Click" 
                                    Text="salvar" ValidationGroup="historico" />
                                <asp:Button ID="btExcluir" runat="server" CausesValidation="False" 
                                    onclick="btExcluir_Click" Text="excluir" />--%>
                            </div>
                        </td>
                        <td>
                            <asp:Button ID="btCancelar" runat="server" CausesValidation="False" OnClick="btCancelar_Click"
                                Text="cancelar" />
                        </td>
                    </tr>
                </table>
            </th>
        </tr>
</table>

</div>

