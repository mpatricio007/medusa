<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.Master" AutoEventWireup="true" CodeBehind="TiposLcto.aspx.cs" Inherits="Medusa.Sistemas.Conciliacao.TiposLcto" %>
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
                        <asp:ListItem Value="codigo">código</asp:ListItem>
                        <asp:ListItem Value="descricao">descricao</asp:ListItem>
                        <asp:ListItem Value="Banco.nome">banco</asp:ListItem>
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
                        AutoGenerateColumns="False" Caption="Lista de Tipos de Lançamento" CellPadding="4" 
                        CssClass="tableView" ForeColor="#333333" GridLines="None" 
                        onpageindexchanging="grid_PageIndexChanging" onrowcreated="grid_RowCreated" 
                        onrowediting="grid_RowEditing" onsorting="grid_Sorting" Width="100%">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:BoundField DataField="codigo" HeaderText="código" SortExpression="codigo">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="banco" SortExpression="Banco.nome">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Banco.nome") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Banco.nome") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="descricao" HeaderText="descrição" 
                                SortExpression="descricao" >
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="dc" HeaderText="D/C" SortExpression="dc">
                            <HeaderStyle HorizontalAlign="Left" />
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
                            <th colspan="1">
                                cadastro de tipos de lançamento</th>
                            <th colspan="1">
                            <div id="dGravacao" runat="server">
                                <asp:Button ID="btInserir" runat="server" onclick="btInserir_Click" 
                                    Text="inserir" />
                                <asp:Button ID="btAlterar" runat="server" onclick="btAlterar_Click" 
                                    Text="salvar" />
                                <asp:Button ID="btExcluir" runat="server" CausesValidation="False" 
                                    onclick="btExcluir_Click" Text="excluir" />

                                <asp:Button ID="btCancelar" runat="server" CausesValidation="False" 
                                    onclick="btCancelar_Click" Text="cancelar" />
                                                                        </div>
                            </th>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                código:
                                <asp:Label ID="txtId_tipolcto" runat="server" Text="0" Visible="False"></asp:Label>
                            </td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoCodigo" runat="server" MaxLength="10" Width="100px" />
                                &nbsp;<asp:CheckBox ID="chkPertenceAdmin" runat="server" 
                                    Text="tipo de lançamento pertence ao administrador do sistema" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo" style="height: 25px">
                                banco:</td>
                            <td class="direito" style="height: 25px">
                                <cddlBancos:cDdlBancos ID="cDdlBancos1" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                descrição:
                            </td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoDescricao" runat="server" EnableValidator="True" 
                                    MaxLength="20" Width="300" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                débito/crédito:</td>
                            <td class="direito">
                                <asp:RadioButtonList ID="rblDC" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Value="D">débito</asp:ListItem>
                                    <asp:ListItem Value="C">crédito</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                importar do arquivo de retorno:</td>
                            <td class="direito">
                                <asp:RadioButtonList ID="rbImportar" runat="server" 
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Value="True">sim</asp:ListItem>
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
                            <div id="dGravacao1" runat="server">
                                <asp:Button ID="btInserir0" runat="server" onclick="btInserir_Click" 
                                    Text="inserir" />
                                <asp:Button ID="btAlterar0" runat="server" onclick="btAlterar_Click" 
                                    Text="salvar" />
                                <asp:Button ID="btExcluir0" runat="server" CausesValidation="False" 
                                    onclick="btExcluir_Click" Text="excluir" />
                                  
                                <asp:Button ID="btCancelar0" runat="server" CausesValidation="False" 
                                    onclick="btCancelar_Click" Text="cancelar" />
                             </div>
                            </th>
                        </tr>
                    </table>
                </asp:Panel>
                       <asp:Panel ID="panelUsuarios" runat="server" Visible="False">
                    <table class="3column">
                        <tr>
                            <td colspan="3" class="esquerdo">
                                <strong>atribuição de usuários ao tipo de lançamento</strong>
                            </td>
                        </tr>
                        <tr>
                           <td class="esquerdo">
                           <asp:GridView ID="gridUsuarios" runat="server" AutoGenerateColumns="False" CssClass="gridv"
                                   CellPadding="4" DataKeyNames="id_pessoa" ForeColor="#333333" 
                                   GridLines="None" 
                                   ondatabinding="gridUsuarios_DataBinding" AllowPaging="True" 
                                   onpageindexchanging="gridUsuarios_PageIndexChanging" 
                                   onselectedindexchanging="gridUsuarios_SelectedIndexChanging">
                               <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                               <Columns>
                                   <asp:TemplateField HeaderText="usuários" SortExpression="PessoaFisica.nome">
                                       <ItemTemplate>
                                           <asp:Label ID="Label1" runat="server" Text='<%# Bind("PessoaFisica.nome") %>'></asp:Label>
                                       </ItemTemplate>
                                       <EditItemTemplate>
                                           <asp:TextBox ID="TextBox1" runat="server" 
                                               Text='<%# Bind("PessoaFisica.nome") %>'></asp:TextBox>
                                       </EditItemTemplate>
                                   </asp:TemplateField>
                                   <asp:CommandField ButtonType="Button" SelectText="adicionar" 
                                       ShowSelectButton="True" />
                               </Columns>
                               <EditRowStyle BackColor="#999999" />
                               <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                               <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                               <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                               <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                               <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                               <SortedAscendingCellStyle BackColor="#E9E7E2" />
                               <SortedAscendingHeaderStyle BackColor="#506C8C" />
                               <SortedDescendingCellStyle BackColor="#FFFDF8" />
                               <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                           </asp:GridView></td>
                           <td class="centro" style="width: 64px">
                               <br /><br />
                           </td> 
                           <td class="direito">
                           <asp:GridView ID="gridUsuariosTipoLcto" runat="server" AutoGenerateColumns="False" CssClass="gridv" 
                                   CellPadding="4" ForeColor="#333333" GridLines="None" 
                                   ondatabinding="gridUsuariosTipoLcto_DataBinding" AllowPaging="True" 
                                   onpageindexchanging="gridUsuariosTipoLcto_PageIndexChanging" 
                                   onselectedindexchanging="gridUsuariosTipoLcto_SelectedIndexChanging">
                               <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                               <Columns>
                                   <asp:TemplateField HeaderText="usuários responsáveis" 
                                       SortExpression="Usuario.PessoaFisica.nome">
                                       <EditItemTemplate>
                                           <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Usuario.PessoaFisica.nome") %>'></asp:TextBox>
                                       </EditItemTemplate>
                                       <ItemTemplate>
                                           <asp:Label ID="Label1" runat="server" Text='<%# Bind("Usuario.PessoaFisica.nome") %>'></asp:Label>
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:CommandField ButtonType="Button" SelectText="remover" 
                                       ShowSelectButton="True" />
                               </Columns>
                               <EditRowStyle BackColor="#999999" />
                               <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                               <HeaderStyle BackColor="Green" Font-Bold="True" ForeColor="White" />
                               <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                               <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                               <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                               <SortedAscendingCellStyle BackColor="#E9E7E2" />
                               <SortedAscendingHeaderStyle BackColor="#506C8C" />
                               <SortedDescendingCellStyle BackColor="#FFFDF8" />
                               <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                           </asp:GridView>
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
