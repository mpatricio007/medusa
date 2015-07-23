<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProjetoCoordenadores.ascx.cs"
    Inherits="Medusa.Sistemas.SCP.ProjetoCoordenadores" %>
<%@ Register Src="../../Controles/DdlCoordenadores.ascx" TagName="DdlCoordenadores"
    TagPrefix="uc1" %>
<div class="conteudo" id="dConteudo" runat="server">
    <div>
   
        <asp:Button ID="btCriar" runat="server" CausesValidation="False" OnClick="btCriar_Click"
            Text="novo coordenador" />
    </div>
    <asp:Panel ID="panelGrid" runat="server">
        <asp:GridView ID="grid" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
            OnPageIndexChanging="grid_PageIndexChanging" OnRowCreated="grid_RowCreated" OnRowEditing="grid_RowEditing"
            OnSorting="grid_Sorting" Width="50%" EmptyDataText="não há coordenadores cadastrados"
            EnableTheming="False" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
            <AlternatingRowStyle CssClass="GridAltItem" />
            <Columns>
                <asp:CommandField EditText="selecionar" ShowEditButton="True" />
                <asp:BoundField DataField="tipo" HeaderText="tipo" SortExpression="tipo" />
                <asp:TemplateField HeaderText="nome" SortExpression="Coordenador.PessoaFisica.nome">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Coordenador.PessoaFisica.nome") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Coordenador.PessoaFisica.nome") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
               
                <asp:TemplateField HeaderText="inicio"    SortExpression="inicio" >
                  
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Medusa.LIB.Util.DateToString(Convert.ToDateTime(Eval("inicio"))) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="término"    SortExpression="termino" >
                  
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Medusa.LIB.Util.DateToString(Convert.ToDateTime(Eval("termino"))) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="observacao" HeaderText="observação" />
            </Columns>
            <PagerStyle CssClass="pgr" />
        </asp:GridView>
    </asp:Panel>
    <asp:Panel ID="panelCadastro" runat="server">
        <table class="mGrid" width="50%">
            <tr>
                <th colspan="2">
                    coordenador do projeto
                </th>
               
            </tr>
            <tr>
                <td class="esquerdo">
                    tipo:
                </td>
                <td class="direito">
                    <cddlTipoCoordenadores:cDdlTipoCoordenadores ID="cDdlTipoCoordenadores1" 
                        runat="server" ValidationGroup="coordenador" />
                </td>
            </tr>
            <tr>
                <td class="esquerdo">
                    nome:
                    <asp:Label ID="txtCodigo" runat="server" Text="0" Visible="False"></asp:Label>
                </td>
                <td class="direito">
                    <uc1:DdlCoordenadores ID="DdlCoordenadores1" runat="server" 
                        ValidationGroup="coordenador" />
                </td>
            </tr>
            <tr>
                <td class="esquerdo">
                    início:
                </td>
                <td class="direito">
                    <cdt:cData ID="cDataInicio" runat="server" ValidationGroup="coordenador" />
                </td>
            </tr>
            <tr>
                <td class="esquerdo">
                    término:
                </td>
                <td class="direito">
                    <cdt:cData ID="cDataTermino" runat="server" ValidationGroup="termino" />
                </td>
            </tr>
            <tr>
                <td class="esquerdo">
                    observação:
                </td>
                <td class="direito">
                    <ctx:cTexto ID="cTextoObs" runat="server" ValidationGroup="observacao" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <th colspan="2">
                    <div id="dGravacao" runat="server">
                        <asp:Button ID="btInserir0" runat="server" OnClick="btInserir_Click" 
                            Text="inserir" ValidationGroup="coordenador" />
                        <asp:Button ID="btAlterar0" runat="server" OnClick="btAlterar_Click" Text="salvar"
                            ValidationGroup="salvar" />
                        <asp:Button ID="btExcluir0" runat="server" CausesValidation="False" OnClick="btExcluir_Click"
                            Text="excluir" ValidationGroup="excluir" />
                        
                        </div>&nbsp;<asp:Button ID="btCancelar0" runat="server" CausesValidation="False" OnClick="btCancelar_Click"
                            Text="cancelar" ValidationGroup="cancelar" />
                </th>
            </tr>
        </table>
    </asp:Panel>
</div>
