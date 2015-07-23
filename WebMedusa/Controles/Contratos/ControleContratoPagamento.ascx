<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControleContratoPagamento.ascx.cs" Inherits="Medusa.Controles.ControleContratoPagamento" %>
                


<style type="text/css">
    .style1
    {
        width: 188px;
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
                    &nbsp;<asp:DropDownList ID="ddlOptions" runat="server" CausesValidation="True" 
                        AutoPostBack="True" onselectedindexchanged="ddlOptions_SelectedIndexChanged">
                        <asp:ListItem Value="num_parcela">numero da parcela</asp:ListItem>
                        <asp:ListItem Value="data_vencimento">data de vencimento</asp:ListItem>
                        <asp:ListItem Value="data_pagamento">data de pagamento</asp:ListItem>
                        <asp:ListItem Value="valor">valor</asp:ListItem>
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
                        onclick="btProcurar_Click" Text="procurar" ValidationGroup="pagamento" />
                    <asp:Button ID="btCriar" runat="server" CausesValidation="False" 
                        onclick="btCriar_Click"  Text="novo" Width="80px" 
                        ValidationGroup="pagamento" />
                </div>
                <asp:Panel ID="panelGrid" runat="server">
                          
                            
                    <asp:GridView ID="grid" runat="server" AllowPaging="True" AllowSorting="True" 
                        AutoGenerateColumns="False" Caption="Lista de Pagamentos" 
                        CssClass="tableView" 
                        onpageindexchanging="grid_PageIndexChanging" onrowcreated="grid_RowCreated" 
                        onrowediting="grid_RowEditing" onsorting="grid_Sorting" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="num_parcela" HeaderText="numero da parcela" 
                                SortExpression="num_parcela" />
                            <asp:BoundField DataField="data_vencimento" HeaderText="data de vencimento" 
                                SortExpression="data_vencimento" DataFormatString="{0:d}" />
                            <asp:BoundField DataField="data_pagamento" HeaderText="data de pagamento" 
                                SortExpression="data_pagamento" DataFormatString="{0:d}" />
                            <asp:BoundField DataField="valor" HeaderText="valor" SortExpression="valor" />
                            <asp:BoundField DataField="observacao" HeaderText="observação" 
                                SortExpression="observacao" />
                            <asp:CheckBoxField DataField="cancelado" HeaderText="cancelado" 
                                SortExpression="cancelado" />
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

                <asp:Panel ID="panelCadastro" runat="server">
                    <table class="cadastro">
                             <tr>
                            <th colspan="1"  align="left" class="style1">
                                cadastro de pagamentos</th>
                            <th colspan="1" >
                            <div id="dGravacao" runat="server" >
                                <asp:Button ID="btInserir" runat="server" onclick="btInserir_Click" 
                                    Text="inserir" ValidationGroup="pagamento" />
                                <asp:Button ID="btAlterar" runat="server" onclick="btAlterar_Click" 
                                    Text="salvar" ValidationGroup="pagamento" />
                                <asp:Button ID="btExcluir" runat="server" CausesValidation="False" 
                                    onclick="btExcluir_Click" Text="excluir" ValidationGroup="pagamento" />
                                <asp:Button ID="btCancelar" runat="server" CausesValidation="False" 
                                    onclick="btCancelar_Click" Text="cancelar" ValidationGroup="pagamento" />
                            </div>
                            </th>
                        </tr>
                        <tr>
                            <td class="style1">
                                numero da parcela:<asp:Label ID="txtCodigoContrato" runat="server" Text="0" 
                                    Visible="False"></asp:Label>
                            </td>
                            <td>
                                <cint:cInteiro ID="cInteiroParcela" runat="server" EnableValidator="True" 
                                    ValidationGroup="pagamento" />
                            </td>
                        </tr>
                             <tr>
                                 <td class="style1">
                                     data de vencimento:</td>
                                 <td>
                                     <cdt:cData ID="cDataVencimento" runat="server" EnableValidator="True" 
                                         ValidationGroup="pagamento" />
                                 </td>
                             </tr>
                             <tr>
                                 <td class="style1">
                                     data de pagamento:</td>
                                 <td>
                                     <cdt:cData ID="cDataPagamento" runat="server" EnableValidator="False" 
                                         ValidationGroup="pagamento" />
                                 </td>
                             </tr>
                             <tr>
                                 <td class="style1">
                                     valor:</td>
                                 <td>
                                     <cvl:cValor ID="cValor" runat="server" EnableValidator="True" 
                                         ValidationGroup="pagamento" />
                                 </td>
                             </tr>
                             <tr>
                                 <td class="style1">
                                     observação:</td>
                                 <td>
                                     <ctx:cTexto ID="cTextoObs" runat="server" EnableValidator="False" Height="100" 
                                         Width="450" TextMode="MultiLine" ValidationGroup="pagamento" />
                                 </td>
                             </tr>
                             <tr>
                                 <td class="style1">
                                     cancelado:
                                 </td>
                                 <td>
                                     <asp:RadioButtonList ID="RbCancelado" runat="server" 
                                         RepeatDirection="Horizontal" ValidationGroup="pagamento">
                                         <asp:ListItem Value="True">sim</asp:ListItem>
                                         <asp:ListItem Value="False">não</asp:ListItem>
                                     </asp:RadioButtonList>
                                 </td>
                             </tr>
                             <tr>
                            <td colspan="2">
                                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th colspan="2">
                                <div ID="dGravacao1" >
                                    <asp:Button ID="btInserir0" runat="server" onclick="btInserir_Click" 
                                        Text="inserir" ValidationGroup="pagamento" />
                                    <asp:Button ID="btAlterar0" runat="server" onclick="btAlterar_Click" 
                                        Text="salvar" style="width: 49px" ValidationGroup="pagamento" />
                                    <asp:Button ID="btExcluir0" runat="server" CausesValidation="False" 
                                        onclick="btExcluir_Click" Text="excluir" ValidationGroup="pagamento" />
                                    <asp:Button ID="btCancelar0" runat="server" CausesValidation="False" 
                                        onclick="btCancelar_Click" Text="cancelar" ValidationGroup="pagamento" />
                                </div>
                            </th>
                        </tr>                    
                    </table>
                </asp:Panel>
</div>



