<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="Medusa.Sistemas.Admin.Usuarios" %>
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
                        <asp:ListItem Value="login">login</asp:ListItem>
                        <asp:ListItem Value="Setor.nome">setor</asp:ListItem>
                        <asp:ListItem Value="PessoaFisica.nome">nome</asp:ListItem>
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
                        onclick="btProcurar_Click" Text="procurar" Width="80px" />
                    <asp:Button ID="btCriar" runat="server" CausesValidation="False" 
                        onclick="btCriar_Click"  Text="novo" Width="80px" />
                </div>
                <asp:Panel ID="panelGrid" runat="server">
                          
                            
                    <asp:GridView ID="grid" runat="server" AllowPaging="True" AllowSorting="True" 
                        AutoGenerateColumns="False" Caption="Lista de Usuários" CellPadding="4" 
                        CssClass="tableView" ForeColor="#333333" GridLines="None" 
                        onpageindexchanging="grid_PageIndexChanging" onrowcreated="grid_RowCreated" 
                        onrowediting="grid_RowEditing" onsorting="grid_Sorting" Width="100%">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:BoundField DataField="login" HeaderText="login" 
                                SortExpression="login">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="setor" SortExpression="Setor.nome">
                              
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Setor.nome") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="nome" SortExpression="PessoaFisica.nome">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("PessoaFisica.nome") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" 
                                        Text='<%# Bind("PessoaFisica.nome") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField EditText="selecionar" ShowEditButton="True">
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:CommandField>
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
                </asp:Panel>
                <asp:Panel ID="panelCadastro" runat="server">
                    <table class="cadastro">
                        <tr>
                            <th colspan="1">
                                cadastro de usuários</th>
                            <div id="dGravacao" runat="server">
                            <th colspan="1">
                                <asp:Button ID="btInserir" runat="server" onclick="btInserir_Click" 
                                    Text="inserir" />
                                <asp:Button ID="btAlterar" runat="server" onclick="btAlterar_Click" 
                                    Text="salvar" />
                                <asp:Button ID="btExcluir" runat="server" CausesValidation="False" 
                                    onclick="btExcluir_Click" Text="excluir" />
                                    
                                <asp:Button ID="btCancelar" runat="server" CausesValidation="False" 
                                    onclick="btCancelar_Click" Text="cancelar" />
                            </th>
                            </div>
                        </tr>
                                 <tr>
                            <td class="esquerdo">
                                cpf:<asp:TextBox ID="txtCodigo" runat="server" Enabled="False" Visible="False" >0</asp:TextBox>
                                <asp:Label ID="txtId_pessoa" runat="server" Text="0" Visible="False"></asp:Label>
                            </td>
                            <td class="direito">
                                <cCpf:cCPF ID="cCPF1" runat="server" AutoPostBack="true" OnTextChanged="cCPF1_OnTextChanged" />
                             
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                nome:
                                </td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoNome" runat="server" EnableValidator="True" Width="500px" 
                                    MaxLength="100" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                sexo:</td>
                            <td class="direito">
                                <cddlSexo:cDdlSexo ID="cDdlSexo1" runat="server"  />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                data de nascimento:</td>
                            <td class="direito">
                                <cdt:cData ID="cDataNascto" runat="server" />
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="esquerdo">
                                rg:</td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoRG" runat="server" MaxLength="20" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                e-mails:</td>
                            <td class="direito">
                             
                                <cListaPessoaEmails:cListaPessoaEmails ID="cListaPessoaEmails1" 
                                    runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                login:</td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoLogin" runat="server" EnableValidator="False" 
                                    Width="200px" MaxLength="50" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                ramal:</td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoRamal" runat="server" EnableValidator="False" 
                                    Width="30px" MaxLength="3" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                setor:</td>
                            <td class="direito">
                                <cddlSetor:cDdlSetor ID="cDdlSetor1" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                nível:</td>
                            <td class="direito">
                                <asp:DropDownList ID="ddlNivel" runat="server" Width="115px">
                                    <asp:ListItem Value="0">Selecione</asp:ListItem>
                                    <asp:ListItem Value="5">usuário </asp:ListItem>
                                    <asp:ListItem Value="1">administrador</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                chapa:</td>
                            <td class="direito">
                                <asp:TextBox ID="cTextoChapa" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                         <asp:Panel ID="pSenha" runat="server">
                        <tr>
                            <td class="esquerdo">
                                senha:</td>
                            <td class="direito">
                                    <asp:TextBox ID="txtSenha" runat="server" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                        ControlToValidate="txtSenha" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
                                        &nbsp;
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                        ControlToCompare="txtConfirmarSenha" ControlToValidate="txtSenha" 
                                        ErrorMessage="senhas não coincidem" ForeColor="Red"></asp:CompareValidator>
                            </td>
                        </tr>
                       
                         <tr>
                            <td class="esquerdo">
                                confirme a senha:</td>
                            <td class="direito">
                                <asp:TextBox ID="txtConfirmarSenha" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                    ControlToValidate="txtConfirmarSenha" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        </asp:Panel>
                       
                        <tr>
                            <td class="esquerdo">
                                status:</td>
                            <td class="direito">
                                <asp:CheckBox ID="ckStatus" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <div id="dGravacao1" runat="server">
                            <th colspan="2">
                                <asp:Button ID="btInserir0" runat="server" onclick="btInserir_Click" 
                                     Text="inserir" />
                                <asp:Button ID="btAlterar0" runat="server" onclick="btAlterar_Click" 
                                    Text="salvar" />
                                <asp:Button ID="btExcluir0" runat="server" CausesValidation="False" 
                                    onclick="btExcluir_Click" Text="excluir" />
                                <asp:Button ID="btCancelar0" runat="server" CausesValidation="False" 
                                    onclick="btCancelar_Click" Text="cancelar" />
                            </th>
                            </div>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="panelSistemas" runat="server" Visible="False">
                    <table class="3column">
                        <tr>
                            <td colspan="3" class="esquerdo">
                                <strong>atribuição de sistemas ao usuário</strong>
                            </td>
                        </tr>
                        <tr>
                           <td class="esquerdo">
                           <asp:GridView ID="gridSistemas" runat="server" AutoGenerateColumns="False" CssClass="gridv"
                                   CellPadding="4" DataKeyNames="id_sistema" ForeColor="#333333" 
                                   GridLines="None" onrowcommand="gridSistemas_RowCommand" 
                                   ondatabinding="gridSistemas_DataBinding" Caption="Sistemas" 
                                   EnableTheming="False" Width="300px">
                               <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                               <Columns>
                                   <asp:BoundField DataField="nome" HeaderText="nome" />
                                   <asp:BoundField DataField="sigla" HeaderText="sigla" />
                                   <asp:ButtonField ButtonType="Button" Text="adicionar" />
                               </Columns>
                               <EditRowStyle BackColor="#999999" />
                               <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                               <HeaderStyle BackColor="#CC0000" Font-Bold="True" ForeColor="White" />
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
                           <asp:GridView ID="gridSistemasUsuario" runat="server" AutoGenerateColumns="False" CssClass="gridv" 
                                   CellPadding="4" ForeColor="#333333" GridLines="None" 
                                   DataKeyNames="id_usuario_sistema" 
                                   onrowcommand="gridSistemasUsuario_RowCommand" 
                                   ondatabinding="gridSistemasUsuario_DataBinding" 
                                   Caption="Sistemas Autorizados" EnableTheming="False" Width="300px">
                               <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                               <Columns>
                                   <asp:TemplateField HeaderText="nome">
                                       <EditItemTemplate>
                                           <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Sistema.nome") %>'></asp:TextBox>
                                       </EditItemTemplate>
                                       <ItemTemplate>
                
                                           <asp:Label ID="Label1" runat="server" Text='<%# Bind("Sistema.nome") %>'></asp:Label>
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="sigla">
                                       <EditItemTemplate>
                                           <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Sistema.sigla") %>'></asp:TextBox>
                                       </EditItemTemplate>
                                       <ItemTemplate>
                                           <asp:Label ID="Label2" runat="server" Text='<%# Bind("Sistema.sigla") %>'></asp:Label>
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:ButtonField ButtonType="Button" Text="remover" />
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
                    <ScriptAction Script="waitingDialog({});" />  
                 </Parallel>
            </OnUpdating>
            <OnUpdated>
                <Parallel duration="0">
                   <FadeIn minimumOpacity=".5" />  
                    <ScriptAction Script="closeWaitingDialog()" /> 
                </Parallel> 
            </OnUpdated>
        </Animations>
        </asp:UpdatePanelAnimationExtender>
    </div>
</asp:Content>
