<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControleContasProjeto.ascx.cs"
    Inherits="Medusa.Sistemas.SCP.ControleContasProjeto" %>
<div class="conteudo">
    <asp:Button ID="btCriar" runat="server" CausesValidation="False" OnClick="btCriar_Click"
        Text="nova conta" />
    <asp:Panel ID="panelGrid" runat="server">
        <asp:GridView ID="grid" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" EmptyDataText="nenhuma conta cadastrada"
                        OnPageIndexChanging="grid_PageIndexChanging" OnRowCreated="grid_RowCreated" OnRowEditing="grid_RowEditing"
                        OnSorting="grid_Sorting" Width="50%" EnableTheming="False" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                        <AlternatingRowStyle CssClass="GridAltItem" />
                        <Columns>
                            <asp:TemplateField HeaderText="banco" 
                                SortExpression="Conta.BancoAgencia.Banco.codigo">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" 
                                        Text='<%# Bind("Conta.BancoAgencia.Banco.codigo") %>'></asp:Label>
                                </ItemTemplate>
                               
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="agência" 
                                SortExpression="Conta.BancoAgencia.nome">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" 
                                        Text='<%# Bind("Conta.BancoAgencia.StrAgenciaDigito") %>'></asp:Label>
                                </ItemTemplate>
                               
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="conta" SortExpression="Conta.numero">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" 
                                        Text='<%# Bind("Conta.StrContaDigito") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" 
                                        Text='<%# Bind("Conta.StrContaDigito") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:CheckBoxField DataField="conta_pagadora" HeaderText="conta_pagadora" 
                                SortExpression="conta_pagadora" >
                            <HeaderStyle HorizontalAlign="Left" />
                            </asp:CheckBoxField>
                            <asp:CommandField EditText="selecionar" ShowEditButton="True">
                            </asp:CommandField>
                        </Columns>
                        <PagerStyle CssClass="pgr" />
                    </asp:GridView>
    </asp:Panel>
    <asp:Panel ID="panelCadastro" runat="server">
     <table class="mGrid" width="50%">
                        <tr>
                            <th colspan="2">
                                cadastro de contratos
                            </th>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                conta:
                                <asp:Label ID="txtCodigo" runat="server" Text="0" Visible="False"></asp:Label>
                            </td>
                            <td class="direito">
                                <cDdlConta:cDdlConta ID="cDdlConta1" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                conta pagadora:
                            </td>
                            <td class="direito">
                                <asp:CheckBox ID="ckContaPagadora" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th colspan="2">
                                <div id="dGravacao1" runat="server">
                                    <asp:Button ID="btInserir0" runat="server" OnClick="btInserir_Click" Text="inserir"
                                        ValidationGroup="contratosProjeto" />
                                    <asp:Button ID="btAlterar0" runat="server" OnClick="btAlterar_Click" Text="salvar"
                                        ValidationGroup="contratosProjeto" />
                                    <asp:Button ID="btExcluir0" runat="server" OnClick="btExcluir_Click" Text="excluir" />
                                    <asp:Button ID="btCancelar0" runat="server" CausesValidation="False" OnClick="btCancelar_Click"
                                        Text="cancelar" />
                                </div>
                            </th>
                        </tr>
                    </table>
      <%--  <ajaxToolkit:TabContainer ID="tabs" runat="server" ActiveTabIndex="1" AutoPostBack="true"
            Width="100%" onactivetabchanged="tabs_ActiveTabChanged">
            <ajaxToolkit:TabPanel ID="tbCadastro" runat="server" HeaderText="cadastro">
                <ContentTemplate>
                   
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="tbLog" runat="server" HeaderText="historico" >
                <ContentTemplate>
                   <cLog:cLog ID="cLog1" runat="server" />
                    <table class="mGrid" width="100%">
                        <tr>
                            <th>
                                <asp:Button ID="btCalcelarLog" runat="server" Text="cancelar" CausesValidation="False" OnClick="btCancelar_Click" />
                            </th>
                        </tr>
                    </table>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
        </ajaxToolkit:TabContainer>--%>
    </asp:Panel>
</div>
