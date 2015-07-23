<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.Master" AutoEventWireup="true" CodeBehind="Devolucao.aspx.cs" Inherits="Medusa.Sistemas.Arquivo.Devolucao" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="conteudo">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
          <Scripts>
            <asp:ScriptReference Path="../../Scripts/FixFocus.js" />       
               
        </Scripts>
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
                        <asp:ListItem Value="dt_retirada">data de retirada</asp:ListItem>
                        <asp:ListItem Value="usuario_retirada.PessoaFisica.nome" Selected="True">retirado por</asp:ListItem>
                        <asp:ListItem Value="dt_devolucao">data de devolução</asp:ListItem>
                        <asp:ListItem Value="usuario_devolucao.PessoaFisica.nome">devolvido por</asp:ListItem>
                        <asp:ListItem Value="volume.nome">volume</asp:ListItem>
                        <asp:ListItem Value="volume.descricao">descrição do volume</asp:ListItem>
                        <asp:ListItem Value="volume.projeto">projeto</asp:ListItem>
                        <asp:ListItem Value="volume.projetoA">projeto A</asp:ListItem>
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
                        AutoGenerateColumns="False" Caption="Lista de Empréstimos" CellPadding="4" 
                        CssClass="tableView" ForeColor="#333333" GridLines="None" 
                        onpageindexchanging="grid_PageIndexChanging" 
                        onrowcreated="grid_RowCreated" onsorting="grid_Sorting" Width="100%">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:BoundField DataField="dt_retirada" HeaderText="data de retirada" 
                                SortExpression="dt_retirada">
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="retirado por" 
                                SortExpression="usuario_retirada.PessoaFisica.nome">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" 
                                        Text='<%# Bind("usuario_retirada.PessoaFisica.nome") %>'></asp:Label>
                                </ItemTemplate>
                                
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="volume" SortExpression="volume.id_volume">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("volume.HtmlPaginaVolume") %>'></asp:Label>
                                </ItemTemplate>                               
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="volume" SortExpression="volume.descricao">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("volume.descricao") %>'></asp:Label>
                                </ItemTemplate>                               
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="projeto" SortExpression="volume.projeto">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("volume.projeto") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("volume.projeto") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="dt_devolucao" HeaderText="data de devolução" 
                                SortExpression="dt_devolucao" >
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="devolvido por" 
                                SortExpression="usuario_devolucao.PessoaFisica.nome">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" 
                                        Text='<%# Bind("usuario_devolucao.PessoaFisica.nome") %>'></asp:Label>
                                </ItemTemplate>
                                
                            </asp:TemplateField>
                            <asp:BoundField DataField="observacao" HeaderText="observação" 
                                SortExpression="observacao" />
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
                                cadastro de devoluções</th>
                          
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                devolvido por:</td>
                            <td class="direito">
                                <asp:TextBox ID="cTextoUsuDevolucao" runat="server" 
                                    ontextchanged="cTextoUsuDevolucao_TextChanged" AutoPostBack="True"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvUsuDevolucao" runat="server" 
                                    ControlToValidate="cTextoUsuDevolucao" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                nome:
                            </td>
                            <td class="direito">
                                <asp:Label ID="txtNome" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                setor:</td>
                            <td class="direito">
                                <asp:Label ID="txtSetor" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                volumes:</td>
                            <td class="direito">
                                <asp:TextBox ID="txtVolume" runat="server" onkeyup="formataInteiro(this,event);" 
                                    ontextchanged="txtVolume_TextChanged" AutoPostBack="True"></asp:TextBox>
                                <asp:GridView ID="gridVolumes" runat="server" AutoGenerateColumns="False" 
                                    ondatabinding="gridVolumes_DataBinding" Width="100%" 
                                    onrowdeleting="gridVolumes_RowDeleting">
                                    <Columns>
                                        <asp:BoundField DataField="id_volume" HeaderText="número">
                                        <ItemStyle Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nome" HeaderText="volume">
                                        <HeaderStyle Width="100px" />
                                        <ItemStyle Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="descricao" HeaderText="descrição" />
                                        <asp:CommandField DeleteText="excluir" ShowDeleteButton="True">
                                        <HeaderStyle HorizontalAlign="Right" Width="70px" />
                                        <ItemStyle HorizontalAlign="Right" Width="70px" />
                                        </asp:CommandField>
                                    </Columns>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                observação:</td>
                            <td class="direito">
                                <asp:TextBox ID="txtObservacao" runat="server" MaxLength="200" Width="600px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th colspan="2">
                                <asp:Button ID="btInserir0" runat="server" onclick="btInserir_Click" 
                                    Text="salvar" />
                                <asp:Button ID="btCancelar0" runat="server" CausesValidation="False" 
                                    onclick="btCancelar_Click" Text="cancelar" />
                                <asp:CustomValidator ID="CustomValidator1" runat="server" 
                                    ErrorMessage="adicione ao menos um volume" ForeColor="Red"></asp:CustomValidator>
                            </th>
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
