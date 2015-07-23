<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControlePatrimonio.ascx.cs" Inherits="Medusa.Sistemas.Comodato.ControlePatrimonio" %>




    
<style type="text/css">
    .style1
    {
        width: 549px;
    }
    .style2
    {
        width: 937px;
    }
    .style4
    {
        width: 153px;
    }
    .style5
    {
        height: 30px;
    }
    .style6
    {
        width: 549px;
        height: 30px;
    }
</style>




    
<div class="conteudo">

               <asp:CheckBox ID="ckFilter" runat="server" AutoPostBack="True" 
                        oncheckedchanged="ckFilter_CheckedChanged" 
            Text="habilitar múltiplos filtros" />
        <asp:DataList ID="DataListFiltros" runat="server" RepeatColumns="6"
            ondatabinding="DataListFiltros_DataBinding" RepeatDirection="Horizontal" 
            OnDeleteCommand="DataListFiltros_DeleteCommand" Height="16px">
            <ItemTemplate>

               <div class="FilterName">

                <%# Eval("property_name") %>&nbsp
                <%# Eval("mode_name")%>&nbsp
                <%# Eval("value")%> &nbsp 
                   <asp:ImageButton ID="btExcluiFiltro" runat="server" 
                       ImageUrl="~/Styles/img/bt_delete.jpg" Width="15px" Height="15px" CommandName="delete"
                        />
                </div>
            </ItemTemplate>
        </asp:DataList>
                <div class="pesquisar">
                    
                    <asp:Label ID="Label1" runat="server" Text="procurar"></asp:Label>
                    &nbsp;<asp:DropDownList ID="ddlOptions" runat="server" 
                        AutoPostBack="True" 
                        onselectedindexchanged="ddlOptions_SelectedIndexChanged">
                        <asp:ListItem Value="id_patrimonio">código</asp:ListItem>
                        <asp:ListItem Value="descricao">descrição</asp:ListItem>
                        <asp:ListItem Value="observacao">observação</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtProcura" runat="server" Width="137px"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlMode" runat="server">
                    </asp:DropDownList>
                    <asp:Label ID="Label2" runat="server" Text="mostrar"></asp:Label>
                    &nbsp;
                    <asp:DropDownList ID="ddlSize" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlSize_SelectedIndexChanged">
                        <asp:ListItem Selected="True">10</asp:ListItem>
                        <asp:ListItem>20</asp:ListItem>
                        <asp:ListItem>50</asp:ListItem>
                        <asp:ListItem Value="0">todos</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="btSearch" runat="server" CausesValidation="False" 
                        onclick="btProcurar_Click" Text="procurar" />
                    <asp:Button ID="btCriar" runat="server" CausesValidation="False" 
                        onclick="btCriar_Click"  Text="novo" Width="80px" />
                </div>
                <asp:Panel ID="panelGrid" runat="server">
                          
                            
                    <asp:GridView ID="grid" runat="server" AllowPaging="True" AllowSorting="True" 
                        AutoGenerateColumns="False" Caption="Lista de Patrimônios" 
                        CssClass="tableView" 
                        onpageindexchanging="grid_PageIndexChanging" onrowcreated="grid_RowCreated" 
                        onrowediting="grid_RowEditing" onsorting="grid_Sorting" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="num_patrimonio" HeaderText="número patrimônio" SortExpression="num_patrimonio">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="inicio" DataFormatString="{0:d}" HeaderText="início" 
                                SortExpression="inicio" />
                            <asp:BoundField DataField="termino" DataFormatString="{0:d}" 
                                HeaderText="término" SortExpression="termino" />
                            <asp:BoundField DataField="nf" HeaderText="nº nota fiscal" 
                                SortExpression="nf" />
                            <asp:BoundField DataField="descricao" HeaderText="descrição" 
                                SortExpression="descricao" />
                            <asp:TemplateField HeaderText="localização" SortExpression="Unidade.nome">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Unidade.nome") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Unidade.nome") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="quantidade" HeaderText="quantidade" SortExpression="quantidade">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:CommandField EditText="selecionar" ShowEditButton="True">
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:CommandField>
                        </Columns>
                        <HeaderStyle HorizontalAlign="Left" />
                        <SortedAscendingCellStyle 
                            CssClass="SortedAscendingCellStyle" />
                        <SortedAscendingHeaderStyle 
                            CssClass="SortedAscendingHeaderStyle" />
                        <SortedDescendingCellStyle 
                            CssClass="SortedDescendingCellStyle" />
                        <SortedDescendingHeaderStyle 
                            CssClass="SortedDescendingHeaderStyle " />
                    </asp:GridView>
                </asp:Panel>
                </div>

             
                <asp:Panel ID="panelCadastro" runat="server">
                    <table class="cadastro" width="100%">
                        <tr>
                            <th colspan="1"  align="left" class="style5">
                                cadastro de patrimônio</th>
                            <th colspan="1" class="style6" >
                            <div id="dGravacao" runat="server" >
                                <asp:Button ID="btInserir" runat="server" onclick="btInserir_Click" 
                                    Text="inserir" ValidationGroup="patrimonio" style="height: 26px" />
                                <asp:Button ID="btAlterar" runat="server" onclick="btAlterar_Click" 
                                    Text="salvar" ValidationGroup="patrimonio" />
                                <asp:Button ID="btExcluir" runat="server" CausesValidation="False" 
                                    onclick="btExcluir_Click" Text="excluir" ValidationGroup="patrimonio" />
                                <asp:Button ID="btCancelar" runat="server" CausesValidation="False" 
                                    onclick="btCancelar_Click" Text="cancelar" ValidationGroup="patrimonio" />
                            </div>
                            </th>
                        </tr>
                        
                        <tr>
                            <td class="style4">
                            
                                &nbsp;patrimônio nº:<asp:Label ID="txtCodigo" runat="server" Text="0" Visible="False"></asp:Label>
                                <asp:Label ID="txtCodigoComodato" runat="server" Text="0" Visible="False"></asp:Label>
                            </td>

                            <td class="style1">
                                
                                <ctx:cTexto ID="cTextoNumPatrimonio" runat="server" ValidationGroup="patrimonio" />
                                
                            </td>
                        </tr>

                        <tr>
                            <td class="style4">
                            
                                período de aquisição:</td>

                            <td class="style1">
                                
                                início:<cdt:cData ID="cDataInicio" runat="server" ValidationGroup="patrimonio" />
                                &nbsp;&nbsp;&nbsp;&nbsp; término:
                                <cdt:cData ID="cDataTermino" runat="server" ValidationGroup="patrimonio" />
                                
                            </td>
                        </tr>

                        <tr>
                            <td class="style4">
                                nº nota fiscal:</td>
                            <td class="style1">
                                <ctx:cTexto ID="cTextoNF" runat="server" ValidationGroup="patrimonio" />
                                &nbsp;Data:
                                <cdt:cData ID="cDataNf" runat="server" ValidationGroup="patrimonio" />
                            </td>
                        </tr>
                        <tr>
                            <td class="style4">
                                valor:&nbsp;</td>
                            <td class="style1">
                                <cvl:cValor ID="cValor" runat="server" ValidationGroup="patrimonio" />
                            </td>
                        </tr>
                        <tr>
                            <td class="style4">
                                localização:</td>
                            <td class="style1">
                                <cDdlUnidades:cDdlUnidades ID="cDdlUnidades" runat="server" ValidationGroup="patrimonio" />
                            </td>
                        </tr>
                        <tr>
                            <td class="style4">
                                descrição:</td>
                            <td class="style1">
                                <ctx:cTexto ID="cTextoDescricao" runat="server" MaxLength="500" 
                                    ValidationGroup="patrimonio" Width="400px" Height="100px" 
                                    TextMode="MultiLine" />
                            </td>
                        </tr>

                        <tr>
                            <td class="style4">
                            
                                observação:</td>

                            <td class="style1">
                                
                                <ctx:cTexto ID="cTextoObs" runat="server" EnableValidator="False" 
                                    MaxLength="200" ValidationGroup="patrimonio" Width="400px" Height="150px" 
                                    TextMode="MultiLine" />
                                
                            </td>
                        </tr>

                            <asp:Panel ID="pQtdade" runat="server" Visible="false">
                           
                        <tr>
                            <td class="style2">
                                quantidade:</td>
                            <td class="direito">
                                <asp:Label ID="lbQuantidade" runat="server" Text="1"></asp:Label>
                            </td>
                        </tr>
                         </asp:Panel>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        
                        <tr>
                            <th colspan="2">
                                <div ID="dGravacao1" >
                                    <asp:Button ID="btInserir0" runat="server" onclick="btInserir_Click" 
                                        Text="inserir" Height="26px" ValidationGroup="patrimonio" />
                                    <asp:Button ID="btAlterar0" runat="server" onclick="btAlterar_Click" 
                                        Text="salvar" ValidationGroup="patrimonio" />
                                    <asp:Button ID="btExcluir0" runat="server" CausesValidation="False" 
                                        onclick="btExcluir_Click" Text="excluir" ValidationGroup="patrimonio" />
                                    <asp:Button ID="btCancelar0" runat="server" CausesValidation="False" 
                                        onclick="btCancelar_Click" Text="cancelar" ValidationGroup="patrimonio" />
                                </div>
                            </th>
                        </tr>   
                             </table>
                             </asp:Panel>



