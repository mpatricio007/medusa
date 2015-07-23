<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.Master" AutoEventWireup="true" CodeBehind="Inscricoes.aspx.cs" Inherits="Medusa.Sistemas.SISPS.Inscricao" %>

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
                        <asp:ListItem Value="nome">nome</asp:ListItem>
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
                        AutoGenerateColumns="False" Caption="Lista de Inscricao" CellPadding="4" 
                        CssClass="tableView" ForeColor="#333333" GridLines="None" 
                        onpageindexchanging="grid_PageIndexChanging" onrowcreated="grid_RowCreated" 
                        onrowediting="grid_RowEditing" onsorting="grid_Sorting" Width="100%">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:BoundField DataField="codigo" HeaderText="número" SortExpression="codigo" >
                            <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nome" HeaderText="nome" 
                                SortExpression="nome">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
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
                                Ficha de inscrição</th>
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
                            
                                 <th colspan="2">
                                 Informações da vaga
                                 </th>
                            
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                função:</td>
                            <td class="direito">
                                <asp:Label ID="lbVaga" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th colspan="2">
                                
                                    Dados Pessoais
                            </th>
                            <tr>
                                <td class="esquerdo">
                                    Código:</td>
                                <td class="direito">
                                    <asp:Label ID="txtCodigo" runat="server" Text="0"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="esquerdo">
                                    CPF:
                                </td>
                                <td class="direito">
                                    <cCpf:cCPF ID="cCPF1" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="esquerdo">
                                    Nome:
                                </td>
                                <td class="direito">
                                    <ctx:cTexto ID="cTextoNome" runat="server" EnableValidator="True" 
                                        MaxLength="30" Width="300" />
                                    <ctx:cTexto ID="cTexto1" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="esquerdo">
                                    E-mail:
                                </td>
                                <td class="direito">
                                    <ctx:cTexto ID="cTextoNumero" runat="server" EnableValidator="True" 
                                        MaxLength="3" Width="30" />
                                    <cem:cEmail ID="cEmail1" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="esquerdo">
                                    Data de Naxcimento:</td>
                                <td class="direito">
                                    <cdt:cData ID="cData1" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="esquerdo">
                                    RG/RNE:</td>
                                <td class="direito">
                                    <ctx:cTexto ID="cTexto2" runat="server" />
                                    &nbsp;Órgão expeditor/uf:
                                    <ctx:cTexto ID="cTexto3" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="esquerdo">
                                    Titulo eleitor:</td>
                                <td class="direito">
                                    numero:
                                    <ctx:cTexto ID="cTexto4" runat="server" />
                                    &nbsp;seção:
                                    <ctx:cTexto ID="cTexto5" runat="server" />
                                    &nbsp;zona:
                                    <ctx:cTexto ID="cTexto6" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="esquerdo">
                                    Sexo:
                                </td>
                                <td class="direito">
                                    <cddlSexo:cDdlSexo ID="cDdlSexo1" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="esquerdo">
                                    Nacionalidade:</td>
                                <td class="direito">
                                    <ctx:cTexto ID="cTexto7" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    
                                    
                                    <cender:cEnder ID="cEnder1" runat="server" />
                                    
                                </td>
                            </tr>
                            <tr>
                                <td class="esquerdo">
                                    Telefones:</td>
                                <td class="direito">
                                    <cListaPessoaTelefones:cListaPessoaTelefones ID="cListaPessoaTelefones1" 
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="esquerdo">
                                    Nome do pai:</td>
                                <td class="direito">
                                    <ctx:cTexto ID="cTexto12" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="esquerdo">
                                    Nome da mãe:</td>
                                <td class="direito">
                                    <ctx:cTexto ID="cTexto13" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="esquerdo">
                                    Portador de necessidades especiais?:</td>
                                <td class="direito">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:CheckBox ID="CheckBox1" runat="server" Text="
                                    &quot;Declaro sob as penas da lei, que li integralmente o Edital de Abertura deste 
                                    Processo Seletivo e que estou de pleno acordo com os seus termos. Declaro 
                                    também, que todas as informações por mim prestadas são verdadeiras e estou 
                                    ciente de que a falcidade das mesmas implicará na minha exclusão deste Prodesso 
                                    Seletivo&quot;." Font-Italic="True"/></td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                                    <asp:RadioButton ID="RadioButton1" runat="server" />
                                    <asp:RadioButton ID="RadioButton2" runat="server" />
                                    &nbsp;Não Sim - ver instruções no edital.</td>
                            </tr>
                            <tr>
                                <th colspan="2">
                                    <div ID="dGravacao1" runat="server">
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
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <ajaxToolkit:UpdatePanelAnimationExtender ID="upae" BehaviorID="animation" runat="server" TargetControlID="UpdatePanel1" Enabled="True">
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
        </ajaxToolkit:UpdatePanelAnimationExtender>
</div>
</asp:Content>
