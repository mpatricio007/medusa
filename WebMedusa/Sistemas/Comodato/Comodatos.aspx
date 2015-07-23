
<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.Master" AutoEventWireup="true" CodeBehind="Comodatos.aspx.cs" Inherits="Medusa.Sistemas.Comodato.Comodatos" %>

<%@ Register src="ControleHistoricoComodato.ascx" tagname="ControleHistoricoComodato" tagprefix="uc1" %>

<%@ Register src="ControlePatrimonio.ascx" tagname="ControlePatrimonio" tagprefix="uc2" %>

<%@ Register src="../../Controles/DdlTiposAdiantamentos.ascx" tagname="DdlTiposAdiantamentos" tagprefix="uc3" %>

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
                
                    <asp:Label ID="Label1" runat="server" Text="procurar"></asp:Label>
                    &nbsp;<asp:DropDownList ID="ddlOptions" runat="server" CausesValidation="True" 
                        AutoPostBack="True" onselectedindexchanged="ddlOptions_SelectedIndexChanged">
                        <asp:ListItem Value="id_comodato" Selected="True">número</asp:ListItem>
                        <asp:ListItem Value="Projeto.cod_def_projeto.Value">projeto</asp:ListItem>
                        <asp:ListItem Value="Projeto.Unidade.nome">unidade</asp:ListItem>
                        <asp:ListItem Value="financiador">patrocinador(es)</asp:ListItem>
                        <asp:ListItem Value="StatusComodatos.nome">status</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="listaFinanciador" runat="server" AppendDataBoundItems="True" DataTextField="strFinanciador" DataValueField="id_financiador" Style="margin-bottom: 0px" Width="400px">
                    </asp:DropDownList>
                    <ajaxToolkit:ListSearchExtender ID="listaFinanciador_ListSearchExtender" runat="server" Enabled="True" PromptCssClass="ListSearchExtenderPrompt" PromptText="digite para procurar" QueryPattern="Contains" QueryTimeout="2000" TargetControlID="listaFinanciador">
                    </ajaxToolkit:ListSearchExtender>
                    <asp:TextBox ID="txtProcura" runat="server" Width="137px"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlMode" runat="server">
                    </asp:DropDownList>
                    &nbsp;<asp:Label ID="Label2" runat="server" Text="mostrar"></asp:Label>
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
                    <%--<asp:Button ID="btImprimir" runat="server" onclick="btImprimir_Click" 
                        Text="imprimir etiquetas" />--%>
                </div>
                <asp:Panel ID="panelGrid" runat="server">
                          
                            
                    <asp:GridView ID="grid" runat="server" AllowPaging="True" AllowSorting="True" 
                        AutoGenerateColumns="False" Caption="Lista de Comodatos" 
                        CssClass="tableView" 
                        onpageindexchanging="grid_PageIndexChanging" onrowcreated="grid_RowCreated" 
                        onrowediting="grid_RowEditing" onsorting="grid_Sorting" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="id_comodato" HeaderText="código" 
                                SortExpression="id_comodato">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Width="50px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="num_comodato" HeaderText="número comodato" SortExpression="num_comodato">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="projeto" 
                                SortExpression="Projeto.codigo.Value">
                            
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" 
                                        Text='<%# Bind("Projeto.HtmlPaginaProjeto") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="unidade" SortExpression="Projeto.Unidade.nome">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox4" runat="server" 
                                        Text='<%# Bind("Projeto.Unidade.nome") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" 
                                        Text='<%# Bind("Projeto.Unidade.nome") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" Width="150px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="patrocinador(es)">
                                <ItemTemplate>
                                    <asp:DataList runat="server" ID="dlFinanciadores" DataSource='<%# Bind("Projeto.Financiadores") %>'
                                        BorderStyle="None">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("HtmlPaginaFinanciador") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </ItemTemplate>
                            </asp:TemplateField>

                           <%-- <asp:TemplateField HeaderText="patrocinador(es)">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox5" runat="server" 
                                        Text='<%# Bind("Projeto.Financiadores") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:DataList runat="server" ID="dlFinanciadores" DataSource='<%# Bind("Projeto.Financiadores") %>'
                                        BorderStyle="None">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("HtmlPaginaFinanciador") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </ItemTemplate>
                            </asp:TemplateField>--%>

                            <asp:TemplateField HeaderText="arquivo" SortExpression="arquivo">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("arquivo") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("arquivo") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="70px" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="status" SortExpression="StatusComodatos.nome">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" 
                                        Text='<%# Bind("StatusComodatos.nome") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" 
                                        Text='<%# Bind("StatusComodatos.nome") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
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
                                cadastro de comodatos</th>
                            <th colspan="1">
                            <div id="dGravacao" runat="server">
                                <asp:Button ID="btInserir" runat="server" onclick="btInserir_Click" 
                                    Text="inserir" ValidationGroup="comodato" />
                                <asp:Button ID="btAlterar" runat="server" onclick="btAlterar_Click" 
                                    Text="salvar" ValidationGroup="comodato" />
                                <asp:Button ID="btCancelar" runat="server" CausesValidation="False" 
                                    onclick="btCancelar_Click" Text="cancelar" ValidationGroup="comodato" />
                                    </div>
                            </th>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                código:</td>
                            <td class="direito">
                                <asp:Label ID="txtCodigo" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">comodato nº:</td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoNumComodato" runat="server" ValidationGroup="comodato" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                projeto:</td>
                            <td class="direito">

                                <cddlProjeto:cDdlProjeto ID="cDdlProjeto1" runat="server" OnSelectedIndexChanged="cDdlProjeto1_SelectedIndexChanged" AutoPostBack="true" ValidationGroup="comodato" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                patrocinador(es):</td>
                            <td class="direito">
                                <asp:GridView ID="GridPatrocinadores" runat="server" 
                                    AutoGenerateColumns="False" ShowHeader="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="patrocinador(es)" 
                                            SortExpression="Financiador.nome">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox1" runat="server" 
                                                    Text='<%# Bind("Financiador.nome") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Financiador.nome") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo" style="height: 27px">
                                arquivo:</td>
                            <td class="direito" style="height: 27px">
                                <asp:Label ID="lbArquivo" runat="server"></asp:Label>
                                &nbsp;<asp:LinkButton ID="lkAddFile" runat="server" onclick="lkAddFile_Click" 
                                    CausesValidation="False" ValidationGroup="comodato">adicionar arquivo</asp:LinkButton>
                                <ajaxToolkit:AsyncFileUpload ID="AsyncFileUpload1" runat="server" UploadingBackColor="Yellow"
            OnUploadedComplete="ProcessUpload" ThrobberID="spanUploading" UploaderStyle="Modern" 
                                    Width="300px" Visible="False"  />
        <span id="spanUploading" runat="server" visible="false">
                                <img 
                                    src="../../Styles/img/loading2.gif" alt="carregando" /></span>
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
                                    Text="inserir" ValidationGroup="comodato" />
                                <asp:Button ID="btAlterar0" runat="server" onclick="btAlterar_Click" 
                                    Text="salvar" ValidationGroup="comodato" />
                                <asp:Button ID="btCancelar0" runat="server" CausesValidation="False" 
                                    onclick="btCancelar_Click" Text="cancelar" ValidationGroup="comodato" />
                                    
                                    &nbsp;<asp:ImageButton ID="btImprimir" runat="server" 
                                    ImageUrl="~/Styles/img/print.gif" onclick="btImprimir_Click" 
                                    ToolTip="imprimir" ValidationGroup="comodato" />
                                    
                                    </div>
                            </th>
                        </tr>
                        </asp:Panel>
                        <tr>
                        <td colspan="2">
                               
                        </td>
                        </tr>
                        <asp:Panel ID="panelPatrimonio" runat="server">
                                    <tr>
                                        <th colspan="2">
                                            cadastro de patrimônio
                                        </th>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <div>
                                                <uc2:ControlePatrimonio ID="ControlePatrimonio1" runat="server" 
                                                    Visible="True" />
                                            </div>
                                        </td>
                                    </tr>

                        </asp:Panel>
                        
                          
                                   <tr>
                                        <td colspan="2">
                                            <div>
                                                <uc1:ControleHistoricoComodato ID="ControleHistoricoComodato1" runat="server" />
                                            </div>
                                        </td>
                                    </tr>
                            </table>
                            
                </ContentTemplate>
                  </asp:UpdatePanel>

        <ajaxToolkit:UpdatePanelAnimationExtender ID="upae" BehaviorID="animation" runat="server" TargetControlID="UpdatePanel1" Enabled="True">
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
</div>
</asp:Content>
