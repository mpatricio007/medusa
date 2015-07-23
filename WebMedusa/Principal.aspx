<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.Master" AutoEventWireup="true"
    CodeBehind="Principal.aspx.cs" Inherits="Medusa.Principal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">



    <asp:ScriptManager ID="ScriptManager1" runat="server">
    <Scripts>
            <asp:ScriptReference Path="../../Scripts/FixFocus.js" />      
        </Scripts>
    </asp:ScriptManager>
 
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>


            <h1>
                TAREFAS

                </h1>
            
            <asp:Label ID="msgTarefa" runat="server"></asp:Label>
            <asp:Panel ID="Panel5" runat="server" CssClass="collapsePanelHeader" Height="30px">
                <div style="padding: 5px; cursor: pointer; vertical-align: middle;">
                    <div style="float: left;">
                        Criar Tarefa</div>
                    <div style="float: right; vertical-align: middle;">
                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="Styles/img/expand_blue.jpg"
                            AlternateText="(mostrar detalhes...)" CausesValidation="False" />
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="Panel6" runat="server" CssClass="collapsePanel" Height="0">
                <asp:Panel ID="panelCadastro" runat="server">
                    <table class="cadastro">
                        <tr>
                            <th colspan="1">
                                criar nova tarefa
                            </th>
                            <th colspan="1">
                                &nbsp;
                            </th>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                tarefa:
                            </td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoTarefa" runat="server" EnableValidator="True" MaxLength="500"
                                    ValidationGroup="vgInsTarefa" Width="500px" Height="50px" TextMode="MultiLine" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                para:<asp:TextBox ID="txtCodigo1" runat="server" Enabled="False" Visible="False"
                                    Width="43px">0</asp:TextBox>
                            </td>
                            <td class="direito">
                                <cDdlUsuarioFUSP:cDdlUsuarioFUSP ID="cDdlUsuarioFUSP1" runat="server" ValidationGroup="vgInsTarefaUsuario" />
                                <asp:Button ID="btSelecionar" runat="server" Text="selecionar" OnClick="btSelecionar_Click"
                                    ValidationGroup="vgInsTarefaUsuario" />
                                <asp:DataList ID="dlUsuarios" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                                    OnDataBinding="dlUsuarios_DataBinding" OnDeleteCommand="dlUsuarios_DeleteCommand">
                                    <ItemTemplate>
                                        <div class="FilterName">
                                            <%# Eval("nome") %>&nbsp
                                            <asp:ImageButton ID="btExcluiFiltro" runat="server" ImageUrl="~/Styles/img/bt_delete.jpg"
                                                Width="15px" Height="15px" CommandName="delete" />
                                        </div>
                                    </ItemTemplate>
                                </asp:DataList>
                            </td>
                        </tr>
                        <tr>
                            <th colspan="2">
                                <asp:Button ID="btAlterar0" runat="server" Text="salvar" OnClick="btAlterar0_Click"
                                     ValidationGroup="vgInsTarefa" />
                                <asp:Button ID="btCancelar0" runat="server" CausesValidation="False" Text="cancelar"
                                    ValidationGroup="vgInsTarefa" OnClick="btCancelar0_Click" />
                                <asp:CustomValidator ID="cvUsuarios" runat="server" ErrorMessage="adicione ao menos um destinatário."
                                    OnServerValidate="cvUsuarios_ServerValidate" ValidationGroup="vgInsTarefa"></asp:CustomValidator>
                            </th>
                        </tr>
                    </table>
                </asp:Panel>
            </asp:Panel>
            <asp:Panel ID="Panel2" runat="server" CssClass="collapsePanelHeader" Height="30px">
                <div style="padding: 5px; cursor: pointer; vertical-align: middle;">
                    <div style="float: left;">
                        Minhas Tarefas</div>
                    <div style="float: right; vertical-align: middle;">
                        <asp:ImageButton ID="Image1" runat="server" ImageUrl="Styles/img/expand_blue.jpg"
                            AlternateText="(mostrar detalhes...)" CausesValidation="False" />
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="Panel1" runat="server" CssClass="collapsePanel" Height="0">
                 <asp:Panel ID="PanelGridMinhasTarefas" runat="server">
                    <table>
                        <asp:DataList ID="dtTarefasPendentes" runat="server" Width="100%" OnDataBinding="dtTarefasPendentes_DataBinding" OnDeleteCommand="dtTarefasPendentes_DeleteCommand">
                            <ItemTemplate>
                                <tr>
                                    <td class="TarefaData">
                                        <asp:Label ID="lbData" runat="server" Text='<%# Bind("data") %>'></asp:Label>
                                    </td>
                                    <td class="TarefaTexto">
                                        <asp:Label ID="lbUsuarioDe" runat="server" Text='<%# Bind("UsuarioDe.PessoaFisica.nome") %>' CssClass="TarefaNomeUsuario"></asp:Label>
                                        &nbsp;&nbsp;
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("tarefaToString") %>'></asp:Label>&nbsp;&nbsp;
                                        </td>
                                    <td>
                                         <asp:ImageButton ID="lkExcluir" runat="server" CommandArgument='<%# Bind("id_tarefa") %>' CommandName="Delete"
                                                        ToolTip="excluir tarefa" ImageUrl="~/Styles/img/excluir.png"
                                                        class="TarefaImgLink" />
                                    </td>
                                    </tr>
                                  
                                    <tr>
                                        <td class="TarefaData">
                                        &nbsp;
                                        </td>
                                        <td class="TarefaTexto">
                                         
                                                    <asp:DataList ID="dlProvidencias" runat="server" OnDeleteCommand="dlProvidencias_DeleteCommand"  >
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td class="TarefaData">
                                                                    <asp:Label ID="lbDataProvidencia" runat="server" Text='<%# Bind("data") %>'></asp:Label>
                                                                </td>
                                                                <td class="TarefaNomeUsuario">
                                                                    <asp:Label ID="lbUsu" runat="server" Text='<%# Bind("Usuario.PessoaFisica.Nome") %>'></asp:Label>
                                                                </td>
                                                                <td class="TarefaTexto">
                                                                    <asp:Label ID="lbProvidencia" runat="server" Text='<%# Bind("Providencia") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                    <asp:ImageButton ID="lkExcluir" runat="server" CommandArgument='<%# Bind("id_providencia") %>' CommandName="Delete"
                                                        ToolTip="excluir providência" ImageUrl="~/Styles/img/excluir.png"
                                                        class="TarefaImgLink" />
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:DataList>
                                                   <asp:Panel ID="pComentarios" runat="server" Visible="false">
                                                    <asp:TextBox ID="txtProvidencia" runat="server" Width="300px"></asp:TextBox>
                                                    <asp:ImageButton ID="lkProvidencia" runat="server" CommandArgument='<%# Bind("id_tarefa") %>'
                                                        OnClick="lkProvidencia_Click" ToolTip="enviar comentário" ImageUrl="~/Styles/img/facebook_4142_editprofile.png"
                                                        class="TarefaImgLink" />
                                            </asp:Panel>
                                        </td>

                                        <td>&nbsp;</td>
                                    </tr>        
                                      <tr>
                                        <td class="TarefaData">&nbsp;
                                        </td>
                                        <td class="TarefaTexto">
                                            <asp:ImageButton ID="lkEncerrar" runat="server" CommandArgument='<%# Bind("id_tarefa") %>'
                                                OnClick="lkEncerrar_Click" ToolTip="encerrar" ImageUrl="~/Styles/img/facebook_4146_privacy.png"
                                                class="TarefaImgLink" />
                                            &nbsp;<asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="false"  CommandArgument='<%# Bind("id_tarefa") %>'
                                                OnClick="lkShowPosts_Click" ToolTip="providencias" ImageUrl="~/Styles/img/facebook_4148_wall_post.png"
                                                class="TarefaImgLink" />
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                    </tr>                          
                            </ItemTemplate>
                            <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        </asp:DataList>
                        
                    </table>
                   
                </asp:Panel>
            </asp:Panel>
            <asp:Panel ID="Panel3" runat="server" CssClass="collapsePanelHeader" Height="30px">
                <div style="padding: 5px; cursor: pointer; vertical-align: middle;">
                    <div style="float: left;">
                        Tarefas Encerradas</div>
                    <div style="float: right; vertical-align: middle;">
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="Styles/img/expand_blue.jpg"
                            AlternateText="(mostrar detalhes...)" CausesValidation="False" />
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="Panel4" runat="server" CssClass="collapsePanel" Height="0">
                <table>
                    <asp:DataList ID="dtTarefasEncerradas" runat="server" Width="100%" OnDataBinding="dtTarefasEncerradas_DataBinding">
                        <ItemTemplate>
                            <tr>
                                <td class="TarefaData">
                                    <asp:Label ID="lbData" runat="server" Text='<%# Bind("data") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbUsuarioDe" runat="server" CssClass="TarefaNomeUsuario" Text='<%# Bind("UsuarioDe.PessoaFisica.nome") %>'></asp:Label>
                                    &nbsp;&nbsp;
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("tarefaToString") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgPendente" runat="server" CommandArgument='<%# Bind("id_tarefa") %>'
                                        ImageUrl="Styles/img/facebook_4148_posted_item.png" CausesValidation="false"
                                        OnClick="lkPendente_Click" ToolTip="tornar pendente" class="TarefaImgLink" />
                                    &nbsp;<asp:ImageButton ID="lkShowPosts" runat="server" CausesValidation="false" OnClick="lkShowPosts_Click" 
                                    CommandArgument='<%# Bind("id_tarefa") %>'  ToolTip="providencias" ImageUrl="~/Styles/img/facebook_4148_wall_post.png" class="TarefaImgLink" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:Panel ID="pComentarios" runat="server" Visible="false">
                                        <asp:DataList ID="dlProvidencias" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td class="TarefaData">
                                                        <asp:Label ID="lbDataProvidencia" runat="server" Text='<%# Bind("data") %>'></asp:Label>
                                                    </td>
                                                    <td class="TarefaNomeUsuario">
                                                        <asp:Label ID="lbUsu" runat="server" Text='<%# Bind("Usuario.PessoaFisica.Nome") %>'></asp:Label>
                                                    </td>
                                                    <td class="TarefaTexto">
                                                        <asp:Label ID="lbProvidencia" runat="server" Text='<%# Bind("Providencia") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    </asp:DataList>
                </table>
            </asp:Panel>
            <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" TargetControlID="Panel1"
                ExpandControlID="Panel2" CollapseControlID="Panel2" Collapsed="False" ImageControlID="Image1"
                ExpandedText="(esconder detalhes...)" CollapsedText="(mostrar detalhes...)" ExpandedImage="Styles/img/collapse_blue.jpg"
                CollapsedImage="Styles/img/expand_blue.jpg" SuppressPostBack="true" SkinID="CollapsiblePanelDemo" />
            <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" runat="server" TargetControlID="Panel4"
                ExpandControlID="Panel3" CollapseControlID="Panel3" Collapsed="True" ImageControlID="ImageButton1"
                ExpandedText="(esconder detalhes...)" CollapsedText="(mostrar detalhes...)" ExpandedImage="Styles/img/collapse_blue.jpg"
                CollapsedImage="Styles/img/expand_blue.jpg" SuppressPostBack="true" SkinID="CollapsiblePanel2" />
            <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender3" runat="server" TargetControlID="Panel6"
                ExpandControlID="Panel5" CollapseControlID="Panel5" Collapsed="True" ImageControlID="ImageButton2"
                ExpandedText="(esconder detalhes...)" CollapsedText="(mostrar detalhes...)" ExpandedImage="Styles/img/collapse_blue.jpg"
                CollapsedImage="Styles/img/expand_blue.jpg" SuppressPostBack="true" SkinID="CollapsiblePanel3" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
