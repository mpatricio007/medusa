<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.Master" AutoEventWireup="true" CodeBehind="LogEditais.aspx.cs" Inherits="Medusa.Sistemas.EditaisLic.LogEditais" %>
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
                    &nbsp;<asp:DropDownList ID="ddlOptions" runat="server" AutoPostBack="True" 
                        CausesValidation="True" 
                        onselectedindexchanged="ddlOptions_SelectedIndexChanged">
                        <asp:ListItem Value="InscricaoPregao.razao_social">razão social</asp:ListItem>
                        <asp:ListItem Value="InscricaoPregao.nome">nome</asp:ListItem>
                        <asp:ListItem>data</asp:ListItem>
                        <asp:ListItem Value="EditalLicAnexo.EditalLic.titulo">edital</asp:ListItem>
                        <asp:ListItem Value="EditalLicAnexo.descricao">anexo</asp:ListItem>
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
                </div>
                <asp:Panel ID="panelGrid" runat="server">
                          
                            
                    <asp:GridView ID="grid" runat="server" AllowPaging="True" AllowSorting="True" 
                        AutoGenerateColumns="False" Caption="Lista de Logs" CellPadding="4" 
                        CssClass="tableView" ForeColor="#333333" GridLines="None" 
                        onpageindexchanging="grid_PageIndexChanging" onrowcreated="grid_RowCreated" 
                        onrowediting="grid_RowEditing" onsorting="grid_Sorting" Width="100%">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            
                            <asp:BoundField DataField="acao" HeaderText="ação" SortExpression="acao" >
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="edital" SortExpression="EditalLicAnexo.EditalLic.titulo">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("EditalLicAnexo.EditalLic.titulo") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("EditalLicAnexo.EditalLic.titulo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="arquivo" SortExpression="EditalLicAnexo.descricao">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("EditalLicAnexo.descricao") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("EditalLicAnexo.descricao") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="strTipoInscricao" HeaderText="usuário" SortExpression="strTipoInscricao" />
                            <asp:BoundField DataField="data" HeaderText="data" SortExpression="data" />
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
                                consulta de log</th>
                            <th colspan="1">
                                &nbsp;</th>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                id_log_edital:
                                </td>
                            <td class="direito">
                                <asp:Label ID="lbId_log_edital" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                acao:
                                </td>
                            <td class="direito">
                                <asp:Label ID="lbAcao" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">edital:</td>
                            <td class="direito">
                                <asp:Label ID="lbEdital" runat="server" Text="lbEdital"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">arquivo:</td>
                            <td class="direito">
                                <asp:Label ID="lbArquivo" runat="server" Text="lbArquivo"></asp:Label>
                            </td>
                        </tr>
                       <tr>
                            <td class="esquerdo" >tipo:</td>
                            <td class="direito">
                                <asp:RadioButtonList ID="rbTipo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rbTipo_SelectedIndexChanged" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Value="Pessoa_Fisica">Pessoa Física</asp:ListItem>
                                    <asp:ListItem Value="Pessoa_Juridica">Pessoa Jurídica</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>

                        <asp:Panel ID="divPessoaJuridica" runat="server">
                            <tr>
                                <td class="esquerdo">cnpj:
                                </td>
                                <td class="direito">
                                    <cCnpj:cCNPJ ID="cCNPJ2" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="esquerdo"><span class="error">*</span> razão social:</td>
                                <td class="direito">
                                    <ctx:cTexto ID="cTextoRazaoSocial" runat="server" MaxLength="300" Width="500px" />
                                </td>
                            </tr>
                            <tr>
                                <td class="esquerdo"><span class="error">*</span> nome fantasia:</td>
                                <td class="direito">
                                    <ctx:cTexto ID="cTextoNomeFantasia" runat="server" MaxLength="300" Width="500px" />
                                </td>
                            </tr>
                        </asp:Panel>
                        <asp:Panel ID="divPessoaFisica" runat="server">
                            <tr>
                                <td class="esquerdo">cpf:</td>

                                <td class="direito">
                                    <cCpf:cCPF ID="cCPF" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="esquerdo"><span class="error">*</span> nome responsável:</td>
                                <td class="direito">
                                    <ctx:cTexto ID="cTextoNome" runat="server" MaxLength="300" Width="500px" />
                                </td>
                            </tr>
                        </asp:Panel>
                        <tr>
                            <td class="esquerdo">
                                ip:</td>
                            <td class="direito">
                                <asp:Label ID="lbIp" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                data:</td>
                            <td class="direito">
                                <asp:Label ID="lbData" runat="server"></asp:Label>
                            </td>
                        </tr>
                      
                       
                    
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <AjaxToolKit:UpdatePanelAnimationExtender ID="upae" BehaviorID="animation" runat="server" TargetControlID="UpdatePanel1" Enabled="True">
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
        </AjaxToolKit:UpdatePanelAnimationExtender>
    </div>
</asp:Content>
