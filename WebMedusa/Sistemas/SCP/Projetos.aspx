<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.master" AutoEventWireup="true"   CodeBehind="Projetos.aspx.cs" Inherits="Medusa.Sistemas.SCP.Projetos"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

    <div class="conteudo">
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
                   
                    <asp:Label ID="Label1" runat="server" Text="procurar"></asp:Label>
                    &nbsp;<asp:DropDownList ID="ddlOptions" runat="server" CausesValidation="True" 
                        AutoPostBack="True" onselectedindexchanged="ddlOptions_SelectedIndexChanged">
                        <asp:ListItem Value="codigo">projeto A</asp:ListItem>
                        <asp:ListItem Value="data_abertura">data  de abertura</asp:ListItem>
                        <asp:ListItem Value="coordenador">coordenador</asp:ListItem>
                        <asp:ListItem>patrocinador</asp:ListItem>
                        <asp:ListItem Value="referencia">referência</asp:ListItem>
                        <asp:ListItem Value="titulo">título</asp:ListItem>
                        <asp:ListItem Value="InstrumentoContratual.nome">instrumento contratual</asp:ListItem>
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
                        AutoGenerateColumns="False" Caption="Lista de Projetos A" CellPadding="4" 
                        CssClass="tableView" ForeColor="#333333" GridLines="None" 
                        onpageindexchanging="grid_PageIndexChanging" onrowcreated="grid_RowCreated" 
                        onrowediting="grid_RowEditing" onsorting="grid_Sorting" Width="100%">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:BoundField DataField="codigo" HeaderText="projeto A:" 
                                SortExpression="codigo" />
                            <asp:BoundField DataField="data_abertura" DataFormatString="{0:d}" 
                                HeaderText="data de abertura:" SortExpression="data_abertura" />
                            <asp:BoundField DataField="referencia" HeaderText="referência" 
                                SortExpression="referencia" />
                            <asp:BoundField DataField="coordenador" HeaderText="coordenador" 
                                SortExpression="coordenador" />
                            <asp:BoundField DataField="patrocinador" HeaderText="patrocinador" 
                                SortExpression="patrocinador" />
                            <asp:BoundField DataField="endereco" HeaderText="endereço" 
                                SortExpression="endereco" />
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
                                cadastro de projetos A</th>
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
                                nº projeto A:<asp:Label ID="txtCodigo" runat="server" Text="0" Visible="False"></asp:Label>
                            </td>
                            <td class="direito">
                                <cint:cInteiro ID="cInteiroCodigo" runat="server" />
                             
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                referência:</td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoReferencia" runat="server" MaxLength="50" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                coordenador:</td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoCoordenador" runat="server" MaxLength="50"
                                    Width="500px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                patrocinador:</td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoPatrocinador" runat="server" MaxLength="50"
                                     Width="500px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                data de abertura:</td>
                            <td class="direito">
                                <cdt:cData ID="cDataAbertura" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                endereço:</td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoEndereco" runat="server"  MaxLength="50"
                                    Width="500px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                título:</td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoTitulo" runat="server" Height="50px" TextMode="MultiLine" 
                                    Width="500px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                observação:</td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoObservacao" runat="server" Height="50px" 
                                    TextMode="MultiLine" Width="500px" EnableValidator="False" />
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
        
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
    <ajaxToolkit:UpdatePanelAnimationExtender ID="upae" BehaviorID="animation" runat="server"
            TargetControlID="UpdatePanel1" Enabled="True">
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
        </ajaxToolkit:UpdatePanelAnimationExtender>
</asp:Content>
