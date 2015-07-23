<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.Master" AutoEventWireup="true" CodeBehind="Saidas.aspx.cs" Inherits="Medusa.Sistemas.SREC.Saidas" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="conteudo">
        <br />
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
                    &nbsp;ano
                    <asp:DropDownList ID="ddlAno" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlAno_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp;&nbsp;<asp:DropDownList ID="ddlOptions" runat="server" CausesValidation="True" 
                        AutoPostBack="True" onselectedindexchanged="ddlOptions_SelectedIndexChanged">
                        <asp:ListItem Value="nprotsai">protocolo de saida</asp:ListItem>
                        <asp:ListItem Value="Entrada.nprotent">protocolo de entrada</asp:ListItem>
                        <asp:ListItem Value="datasai">data</asp:ListItem>
                        <asp:ListItem Value="obssaida">obs de saida</asp:ListItem>
                        <asp:ListItem Value="UsuarioResp.PessoaFisica.nome">responsável pela devolução</asp:ListItem>
                        <asp:ListItem Value="UsuarioSaida.PessoaFisica.nome">usuário</asp:ListItem>
                        <asp:ListItem Value="destinatario">destinatário</asp:ListItem>
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
                        AutoGenerateColumns="False" Caption="Lista de Saidas" 
                        CssClass="tableView" 
                        onpageindexchanging="grid_PageIndexChanging" onrowcreated="grid_RowCreated" 
                        onrowediting="grid_RowEditing" onsorting="grid_Sorting" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="nprotsai" HeaderText="nº protocolo de saída" 
                                SortExpression="nprotsai">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                   
                            <asp:BoundField DataField="datasai" HeaderText="data de saída" 
                                SortExpression="datasai">
                            <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="obssaida" 
                                HeaderText="observação de saida" SortExpression="obssaida" />
                            <asp:BoundField DataField="destinatario" HeaderText="destinatário" 
                                SortExpression="destinatario" />
                                <asp:TemplateField HeaderText="nº de protocolo de entrada" 
                                SortExpression="Entrada.nprotent">                           
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" 
                                        Text='<%# Bind("Entrada.nprotent") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="responsável pela devolução" 
                                SortExpression="UsuarioResp.PessoaFisica.nome">                           
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" 
                                        Text='<%# Bind("UsuarioResp.PessoaFisica.nome") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="usuário" 
                                SortExpression="UsuarioSaida.PessoaFisica.nome">                         
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" 
                                        Text='<%# Bind("UsuarioSaida.PessoaFisica.nome") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField EditText="selecionar" ShowEditButton="True">
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:CommandField>
                        </Columns>
                        <HeaderStyle 
                            HorizontalAlign="Left" />
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
                                cadastro de saidas</th>
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
                            <td class="esquerdo" style="height: 33px">
                                nº protocolo de entrada:<asp:Label ID="id_entrada" runat="server" 
                                    Visible="False"></asp:Label>
                            </td>
                            <td class="direito" style="height: 33px">
                                <asp:TextBox ID="txtNprotent" runat="server" AutoPostBack="True" 
                                    ontextchanged="txtNprotent_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo" style="height: 33px">
                                nº protocolo de saída:<asp:Label ID="txtCodigo" runat="server" Text="0" 
                                    Visible="False"></asp:Label>
                            </td>
                            <td class="direito" style="height: 33px">
                                <cint:cInteiro ID="cTextoProtSaida" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                data da saída:</td>
                            <td class="direito">
                                <cdt:cData ID="cTextoDataSai" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                projeto:</td>
                            <td class="direito">
                                <asp:Label ID="txtProjeto" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                projeto A:</td>
                            <td class="direito">
                                <asp:Label ID="txtProjA" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo" style="height: 25px">
                                tipo documento:</td>
                            <td class="direito" style="height: 25px">
                                <asp:Label ID="txtDocumento" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                número:</td>
                            <td class="direito">
                                <asp:Label ID="txtNumero" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                valor:</td>
                            <td class="direito">
                                <asp:Label ID="txtValor" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                enviado por:</td>
                            <td class="direito">
                                <asp:Label ID="txtEnviadoPor" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                descrição:</td>
                            <td class="direito">
                                <asp:Label ID="txtDescricao" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                encaminhado para:</td>
                            <td class="direito">
                                <asp:Label ID="txtEncaminhadoPara" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                obs:</td>
                            <td class="direito">
                                <asp:Label ID="txtObs" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                observação de saída:</td>
                            <td class="direito">
                                <asp:TextBox ID="cTextoObsSaida" runat="server" Height="50px" MaxLength="100" 
                                    TextMode="MultiLine" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                destinatário:</td>
                            <td class="direito">
                                <asp:TextBox ID="cTextoDestinatario" runat="server" Height="26px" 
                                    TextMode="MultiLine" Width="278px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                responsável pela devolução:</td>
                            <td class="direito">
                                <cDdlUsuarioFUSP:cDdlUsuarioFUSP ID="cTextoUsuarioFUSPResp" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th colspan="2">
                            <div id="dGravacao1">
                                <asp:Button ID="btInserir0" runat="server" onclick="btInserir_Click" 
                                    Text="inserir" />
                                <asp:Button ID="btAlterar0" runat="server" onclick="btAlterar_Click" 
                                    Text="salvar" />
                                <asp:Button ID="btExcluir0" runat="server" CausesValidation="False" 
                                    Text="excluir" />
                                <asp:Button ID="btCancelar0" runat="server" CausesValidation="False" 
                                    onclick="btCancelar_Click" Text="cancelar" />
                            </div>
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
