<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControleRequisitosEncerramento.ascx.cs" Inherits="Medusa.Sistemas.SCP.ControleRequisitosEncerramento" %>
                <%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
                <style type="text/css">
                    .style1
                    {
                        width: 106px;
                    }
                    .style3
                    {
                        width: 274px;
                    }
                    .style4
                    {
                        width: 107px;
                    }
                </style>
                <div id="dConteudo" runat="server">
                    <div>
                        &nbsp;<asp:Button ID="btCriar" runat="server" CausesValidation="False" 
                            OnClick="btCriar_Click" Text="novo requisito" />
                    </div>
                    <asp:Panel ID="panelGrid" runat="server">
                        <asp:GridView ID="grid" runat="server" AllowPaging="True" AllowSorting="True" 
                            AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" CssClass="mGrid" 
                            EmptyDataText="não há requisitos cadastrados" EnableTheming="False" 
                            OnPageIndexChanging="grid_PageIndexChanging" OnRowCreated="grid_RowCreated" 
                            OnRowEditing="grid_RowEditing" OnSorting="grid_Sorting" 
                            PagerStyle-CssClass="pgr" Width="80%" 
                            Caption="lista de requisitos para encerramento do projeto">
                            <AlternatingRowStyle CssClass="alt" />
                            <Columns>
                                <asp:CommandField EditText="selecionar" ShowEditButton="True" />
                                <asp:BoundField DataField="descricao" HeaderText="descrição">
                                <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="data_solucao" DataFormatString="{0:d}" 
                                    HeaderText="data solução" SortExpression="data_solucao" />
                                <asp:BoundField DataField="solucao" HeaderText="solução" />
                                <asp:TemplateField HeaderText="usuário" 
                                    SortExpression="Usuario.PessoaFisica.nome">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" 
                                            Text='<%# Bind("Usuario.PessoaFisica.nome") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" 
                                            Text='<%# Bind("Usuario.PessoaFisica.nome") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:CheckBoxField DataField="status" HeaderText="status" 
                                    SortExpression="status" />
                            </Columns>
                            <HeaderStyle CssClass="GridHeader" />
                            <PagerStyle CssClass="pgr" />
                            <SortedAscendingCellStyle CssClass="SortedAscendingCellStyle" />
                            <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle" />
                            <SortedDescendingCellStyle CssClass="SortedDescendingCellStyle" />
                            <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle " />
                        </asp:GridView>
                    </asp:Panel>
                    <asp:Panel ID="panelCadastro" runat="server" Visible="False">
                        <table class="mGrid" width="80%">
                            <tr>
                                <th colspan="2">
                                    cadastro de requisitos para encerramento do projeto
                                </th>
                            </tr>
                            <tr>
                                <td class="style1">
                                    descrição:<asp:Label ID="txtCodigo" runat="server" Text="0" Visible="False"></asp:Label>
                                </td>
                                <td class="direito">
                                    
                                    <ctx:cTexto ID="cTextoDescricao" runat="server" MaxLength="250" 
                                        TextMode="MultiLine" ValidationGroup="requisito" Height="100px" 
                                        Width="500px" />
                                    
                                </td>
                            </tr>
                            
                            <tr>
                                <td colspan="2">
                                    <div id="dSolucao" runat="server">
                                        <table>
                                            <tr>
                                                <td class="style4">
                                                    data solução:</td>
                                                <td class="style3">
                                                <cdt:cData ID="cDataSolucao" runat="server"
                                                    ValidationGroup="resolver" />
                                                </td>
                                            </tr>
                                            <tr>
                                <td class="style4">
                                    solução:</td>
                                <td class="style3">
                                    <ctx:cTexto ID="cTextoSolucao" runat="server" Height="100px" 
                                        ValidationGroup="resolver" Width="500px" TextMode="MultiLine" />
                                </td>
                            </tr>
                            
                            <tr>
                                <td class="style4">
                                    status:</td>
                                <td class="style3">
                                    <asp:CheckBox ID="ckStatus" runat="server" Enabled="False" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                </td>
                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            
                            <tr>
                                <th colspan="2">
                                    <table>
                                        <tr>
                                            <td class="style1">
                                                <div ID="dGravacao" runat="server">
                                                    <asp:Button ID="btInserir0" runat="server" OnClick="btInserir_Click" 
                                                        Text="inserir" ValidationGroup="requisito" />
                                                </div>
                                            </td>
                                            <td>
                                                            <asp:Button ID="btResolver" runat="server" onclick="btResolver_Click" 
                                                        Text="desativar" ValidationGroup="resolver" />
                                                            <asp:Button ID="btAtivar" runat="server" onclick="btAtivar_Click" 
                                                                Text="ativar" ValidationGroup="ativar" />
                                                <asp:Button ID="btCancelar0" runat="server" CausesValidation="False" 
                                                        OnClick="btCancelar_Click" Text="cancelar" />
                                            </td>
                                        </tr>
                                    </table>
                        </table>    
                    </asp:Panel>
</div>




