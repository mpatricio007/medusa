<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.master" AutoEventWireup="true" CodeBehind="ResgateConta.aspx.cs" Inherits="Medusa.Sistemas.Conciliacao.ResgateConta" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="conteudo">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
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
                        <img src="../../Styles/img/loading.gif" />
                        <span style="margin:3px">Carregando ...</span></div>
                    </div>
                    <asp:Label ID="Label1" runat="server" Text="procurar"></asp:Label>
                    &nbsp;<asp:DropDownList ID="ddlOptions" runat="server" CausesValidation="True" 
                        AutoPostBack="True" onselectedindexchanged="ddlOptions_SelectedIndexChanged">
                        <asp:ListItem Value="numero">número</asp:ListItem>
                        <asp:ListItem Value="banco">banco</asp:ListItem>
                        <asp:ListItem Value="agencia">agência</asp:ListItem>
                        <asp:ListItem Value="cod_def_projeto">projeto</asp:ListItem>
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
                    <asp:Button ID="btSearch" runat="server" 
                        onclick="btProcurar_Click" Text="procurar" ValidationGroup="procurar" />
                </div>
                <asp:Panel ID="panelGrid" runat="server">
                  <table class="cadastro">
                 <tr>
                 <th>
                            
                          saldo na data:
                                <cdt:cData ID="cDataTermino" runat="server" ValidationGroup="procurar" />
                          <asp:Button ID="btSearch0" runat="server" onclick="btProcurar_Click" 
                              Text="atualizar saldo" ValidationGroup="procurar" />
                   </th>         
                   </tr>
                   </table>
                    <asp:GridView ID="grid" runat="server" AllowPaging="True" AllowSorting="True" 
                        AutoGenerateColumns="False" Caption="Escolha a Conta Corrente para Resgate" 
                        CellPadding="4" CssClass="tableView" ForeColor="#333333" GridLines="None" 
                        onpageindexchanging="grid_PageIndexChanging" onrowcreated="grid_RowCreated" 
                        onrowediting="grid_RowEditing" onsorting="grid_Sorting" Width="100%">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                             <asp:TemplateField HeaderText="nº da conta"
                                SortExpression="numero">                                
                                <ItemTemplate>
                              
                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("numero") + "-" +Eval("digito")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                 <ItemStyle Width="100px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="banco" HeaderText="banco" SortExpression="banco" />
                            <asp:BoundField DataField="agencia" HeaderText="agência" 
                                SortExpression="agencia" />
                            <asp:BoundField DataField="cod_def_projeto" HeaderText="projeto" 
                                SortExpression="cod_def_projeto" />
                             <asp:BoundField DataField="saldo_anterior" DataFormatString="{0:N2}" 
                                 HeaderText="saldo anterior">
                             <ItemStyle HorizontalAlign="Right" />
                             </asp:BoundField>
                            <asp:BoundField DataField="creditos" HeaderText="créditos" 
                                DataFormatString="{0:N2}">
                            <ItemStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="debitos" DataFormatString="{0:N2}" 
                                HeaderText="débitos">
                            </asp:BoundField>
                            <asp:BoundField DataField="saldo_final" DataFormatString="{0:N2}" 
                                HeaderText="saldo final">
                            <ItemStyle Width="120px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:CommandField EditText="selecionar" ShowEditButton="True">
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:CommandField>
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" 
                            HorizontalAlign="Left" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" 
                            CssClass="SortedAscendingCellStyle" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" 
                            CssClass="SortedAscendingHeaderStyle" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" 
                            CssClass="SortedDescendingCellStyle" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" 
                            CssClass="SortedDescendingHeaderStyle " />
                    </asp:GridView>
                </asp:Panel>
                 <asp:Panel ID="panelCadastro" runat="server">
                    <table class="cadastro">
                        <tr>
                            <th colspan="2">
                                lançamentos de conta corrente</th>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                conta corrente:
                                <asp:Label ID="txtCodigo" runat="server" Text="0" Visible="False"></asp:Label>
                            </td>
                            <td class="direito">
                                <asp:Label ID="txtConta" runat="server"></asp:Label>
                               
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>

                    <asp:Panel ID="panelResgates" runat="server" Visible="False">
                    <table class="cadastro">
                       <tr>
                            <td class="esquerdo">
                                resgate:
                                </td>
                            <td class="direito">
                                <asp:Label ID="txtId_aplicacao" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                data:
                                </td>
                            <td class="direito">
                                <cdt:cData ID="cDataAplicacao" runat="server" EnableValidator="True" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                descrição:</td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoDescricao" runat="server" MaxLength="30" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                valor:</td>
                            <td class="direito">
                                <cvl:cValor ID="cValorAplicacao" runat="server" EnableValidator="True" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                tipo de aplicação:</td>
                            <td class="direito">
                                <asp:RadioButtonList ID="rdTipo" runat="server" RepeatDirection="Horizontal">
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <th colspan="1">
                            <div id="dGravacao" runat="server">
                                <asp:Button ID="btInsereAplicacao" runat="server" 
                                     Text="inserir" onclick="btInsereAplicacao_Click" />
                                <asp:Button ID="btAlterarAplicacao" runat="server" Text="alterar" 
                                    onclick="btSalvaAplicacao_Click" />
                                <asp:Button ID="btExcluiAplicacao" runat="server" CausesValidation="False" 
                                    Text="excluir" onclick="btExcluiAplicacao_Click" />
                             
                                <asp:Button ID="btCancelar" runat="server" onclick="btCancelar_Click" 
                                    Text="cancelar" CausesValidation="False" />
                                      </div>
                            </th>
                            <th>
                            
                                inicial:
                                <cdt:cData ID="cData1" runat="server" ValidationGroup="filtrar" />
                                &nbsp;final:
                                <cdt:cData ID="cData2" runat="server" ValidationGroup="filtrar" />
                                <asp:Button ID="btFiltrar" runat="server" onclick="btFiltrar_Click" 
                                    Text="filtrar" ValidationGroup="filtrar" />
                            
                            </th>
                        </tr>
                      <tr>                       
                            <td class="direito" colspan="2">
                                <asp:GridView ID="gridResgates" runat="server"  AllowSorting="True" CssClass="gridv" 
                                    AutoGenerateColumns="False" Caption="Aplicações na Conta Corrente" CellPadding="4" 
                                     ForeColor="#333333" GridLines="None" Width="100%" 
                                    onrowediting="gridResgates_RowEditing" 
                                    ondatabinding="gridResgates_DataBinding">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="Data" HeaderText="data" 
                                            DataFormatString="{0:dd/MM/yyyy}" />
                                               <asp:TemplateField HeaderText="tipo">
                                            
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("TipoLcto.descricao") %>'>
                                                
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="descrição">
                                            
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("descricao") %> '>
                                                
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Valor" HeaderText="valor" >
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:CommandField EditText="Selecionar" ShowEditButton="True" />
                                    </Columns>
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" 
                                        CssClass="SortedAscendingCellStyle" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" 
                                        CssClass="SortedAscendingHeaderStyle" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" 
                                        CssClass="SortedDescendingCellStyle" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" 
                                        CssClass="SortedDescendingHeaderStyle " />
                                </asp:GridView>
                                </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblMsgPag" runat="server"></asp:Label>
                            </td>
                        </tr>
                      </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanelAnimationExtender ID="upae" BehaviorID="animation" runat="server" TargetControlID="UpdatePanel1" Enabled="True">
        <Animations>
            <OnUpdating>
                <Parallel duration="0">
                    <FadeOut minimumOpacity=".5" />
                    <ScriptAction Script="onUpdating();" />  
                 </Parallel>
            </OnUpdating>
            <OnUpdated>
                <Parallel duration="0">
                   <FadeIn minimumOpacity=".5" />  
                    <ScriptAction Script="onUpdated();" /> 
                </Parallel> 
            </OnUpdated>
        </Animations>
        </asp:UpdatePanelAnimationExtender>
</div>
</asp:Content>
