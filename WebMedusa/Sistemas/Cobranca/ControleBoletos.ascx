<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControleBoletos.ascx.cs" Inherits="Medusa.Sistemas.SCP.ControleBoletos" %>
 <div class="conteudo">
 <table class="cadastro">
 <tr>
 <th colspan="2">
 sacado
 </th>
 </tr>
 <tr>
 
 <td colspan="2" class="direito">
     
     <asp:Label ID="txtSacado" runat="server" ></asp:Label>
     <asp:Label ID="txtId_evento_sacado" runat="server" Text="0" Visible="False"></asp:Label>
 </td>
 </tr>
 </table>
      <asp:CheckBox ID="ckFilter" runat="server" AutoPostBack="True" 
                        oncheckedchanged="ckFilter_CheckedChanged" 
            Text="habilitar múltiplos filtros" />
        <asp:DataList ID="DataListFiltros" runat="server" RepeatColumns="6"
            ondatabinding="DataListFiltros_DataBinding" RepeatDirection="Horizontal" 
            OnDeleteCommand="DataListFiltros_DeleteCommand">
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
                    <div id="updateProgressDiv" style="display:none; position:absolute;">
                        <div style=" margin-left:780px;  float:left">
                        <img src="../../Styles/img/loading.gif" alt="carregando" />
                        <span style="margin:3px">Carregando ...</span></div>
                    </div>
                    <asp:Label ID="Label1" runat="server" Text="procurar"></asp:Label>
                    &nbsp;<asp:DropDownList ID="ddlOptions" runat="server" CausesValidation="True" 
                        AutoPostBack="True" onselectedindexchanged="ddlOptions_SelectedIndexChanged">
                        <asp:ListItem Value="id_boleto">código</asp:ListItem>
                        <asp:ListItem>valor</asp:ListItem>
                        <asp:ListItem Value="data_vencto">data de vencimento</asp:ListItem>
                        <asp:ListItem Value="data_pgto">data de pagamento</asp:ListItem>
                        <asp:ListItem Value="valor_pgto">valor pago</asp:ListItem>
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
                &nbsp;<asp:ImageButton ID="btEnviarEmail" runat="server" 
                                    ImageUrl="~/Styles/img/icone_email.gif" onclick="btEnviarEmail_Click" 
                                    Width="20px" ToolTip="enviar boletos por e-mail " />
                </div>
       
                <asp:Panel ID="panelGrid" runat="server">
                          
                            
                   <asp:GridView ID="grid" runat="server" AllowPaging="True" AllowSorting="True" 
                        AutoGenerateColumns="False" Caption="Lista de Boletos" 
                    
                        onpageindexchanging="grid_PageIndexChanging" onrowcreated="grid_RowCreated" 
                        onrowediting="grid_RowEditing" onsorting="grid_Sorting" Width="100%" 
                        onselectedindexchanging="grid_SelectedIndexChanging">
                        <Columns>
                            <asp:BoundField DataField="id_boleto" HeaderText="código" 
                                SortExpression="id_boleto">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="num_parcela" HeaderText="parcela" 
                                SortExpression="num_parcela" />
                            <asp:BoundField DataField="valor" HeaderText="valor" 
                                SortExpression="valor" DataFormatString="{0:N2}">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="data_vencto" DataFormatString="{0:d}" 
                                HeaderText="data de vencto" SortExpression="data_vencto" />
                            <asp:BoundField DataField="data_pgto" DataFormatString="{0:d}" 
                                HeaderText="data de pgto" SortExpression="data_pgto" />
                            <asp:BoundField DataField="valor_pgto" DataFormatString="{0:N2}" 
                                HeaderText="valor pago" SortExpression="valor_pgto" />
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
                                         ImageUrl="~/Styles/img/print.gif" Text="imprimir boleto" CommandName="Select"  />
                                </ItemTemplate>
                            </asp:TemplateField>
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
                            <th colspan="1">
                                cadastro de boletos</th>
                            <th colspan="1">
                            <div id="dGravacao" runat="server">
                                <asp:Button ID="btInserir" runat="server" onclick="btInserir_Click" 
                                    Text="inserir" ValidationGroup="boleto" />
                                <asp:Button ID="btAlterar" runat="server" onclick="btAlterar_Click" 
                                    Text="salvar" ValidationGroup="atuacao" />
                                <asp:Button ID="btExcluir" runat="server" CausesValidation="False" 
                                    onclick="btExcluir_Click" Text="excluir" />
                                <asp:Button ID="btCancelar" runat="server" CausesValidation="False" 
                                    onclick="btCancelar_Click" Text="cancelar" />
                                    </div>
                            </th>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                valor:
                                <asp:Label ID="txtCodigo" runat="server" Text="0" Visible="False"></asp:Label>
                                </td>
                            <td class="direito">
                                <cvl:cValor ID="cValor" runat="server" ValidationGroup="boleto" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                parcela:</td>
                            <td class="direito">
                                <cint:cInteiro ID="cInteiroParcela" runat="server" ValidationGroup="boleto" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                data de vencimento:</td>
                            <td class="direito">
                                <cdt:cData ID="cDataVencto" runat="server" ValidationGroup="boleto" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                data de pagamento:</td>
                            <td class="direito">
                                <cdt:cData ID="cDataPagamento" runat="server" EnableValidator="false" 
                                    ValidationGroup="boleto" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                valor pago:</td>
                            <td class="direito">
                                <cvl:cValor ID="cValorPago" runat="server" EnableValidator="false" 
                                    ValidationGroup="boleto"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                observação:</td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoObs" runat="server" EnableValidator="false" 
                                    TextMode="MultiLine" Height="50px" Width="500px" ValidationGroup="boleto"/>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th colspan="2">
                            <div id="dGravacao1" runat="server">
                                <asp:Button ID="btInserir0" runat="server" onclick="btInserir_Click" 
                                     Text="inserir" ValidationGroup="boleto" />
                                <asp:Button ID="btAlterar0" runat="server" onclick="btAlterar_Click" 
                                    Text="salvar" ValidationGroup="atuacao" />
                                <asp:Button ID="btExcluir0" runat="server" CausesValidation="False" 
                                    onclick="btExcluir_Click" Text="excluir" />
                                <asp:Button ID="btCancelar0" runat="server" CausesValidation="False" 
                                    onclick="btCancelar_Click" Text="cancelar" />
                                    &nbsp;</div>
                            </th>
                        </tr>
                    </table>
                           </asp:Panel>
               
    </div>