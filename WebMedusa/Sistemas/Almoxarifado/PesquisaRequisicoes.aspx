<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.Master" AutoEventWireup="true"
    CodeBehind="PesquisaRequisicoes.aspx.cs" Inherits="Medusa.Sistemas.Almoxarifado.PesquisaRequisicoes" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="conteudo">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="~/Scripts/FixFocus.js" />
            </Scripts>
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:CheckBox ID="ckFilter" runat="server" AutoPostBack="True" OnCheckedChanged="ckFilter_CheckedChanged"
                    Text="habilitar múltiplos filtros" />
                <asp:DataList ID="DataListFiltros" runat="server" RepeatColumns="6" OnDataBinding="DataListFiltros_DataBinding"
                    RepeatDirection="Horizontal" OnDeleteCommand="DataListFiltros_DeleteCommand">
                    <ItemTemplate>
                        <div class="FilterName">
                            <%# Eval("property_name") %>&nbsp
                            <%# Eval("mode_name")%>&nbsp
                            <%# Eval("value")%>
                            &nbsp
                            <asp:ImageButton ID="btExcluiFiltro" runat="server" ImageUrl="~/Styles/img/bt_delete.jpg"
                                Width="15px" Height="15px" CommandName="delete" />
                        </div>
                    </ItemTemplate>
                </asp:DataList>
                <div class="pesquisar">
                    <div id="updateProgressDiv" style="display: none; position: absolute;">
                        <div style="margin-left: 780px; float: left">
                            <img src="../../Styles/img/loading.gif" alt="" />
                            <span style="margin: 3px">Carregando ...</span></div>
                    </div>
                    <asp:Label ID="Label1" runat="server" Text="procurar"></asp:Label>
                    &nbsp;<asp:DropDownList ID="ddlOptions" runat="server" CausesValidation="True" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlOptions_SelectedIndexChanged">
                        <asp:ListItem Value="data">data</asp:ListItem>
                        <asp:ListItem Value="Usuario.PessoaFisica.nome">solicitante</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtProcura" runat="server" Width="137px"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlMode" runat="server">
                    </asp:DropDownList>
                    <asp:Label ID="Label2" runat="server" Text="mostrar"></asp:Label>
                    &nbsp;
                    <asp:DropDownList ID="ddlSize" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSize_SelectedIndexChanged">
                        <asp:ListItem Selected="True">10</asp:ListItem>
                        <asp:ListItem>20</asp:ListItem>
                        <asp:ListItem>50</asp:ListItem>
                        <asp:ListItem Value="0">todos</asp:ListItem>
                    </asp:DropDownList>
                    <%--<asp:Button ID="btRelatorioMaterial" runat="server" 
                        onclick="btRelatorioMaterial_Click" Text="gerar relatorio por material" />--%>
                    <asp:RadioButtonList ID="rdStatus" runat="server" RepeatDirection="Horizontal" 
                        RepeatLayout="Flow">
                        <asp:ListItem Selected="True" Value="em aberto">em aberto</asp:ListItem>
                        <asp:ListItem>encerrado</asp:ListItem>
                    </asp:RadioButtonList>
                    &nbsp;<asp:Button ID="btSearch" runat="server" CausesValidation="False" OnClick="btProcurar_Click"
                        Text="procurar" />
                </div>
                <asp:Panel ID="panelGrid" runat="server" HorizontalAlign="Left">
                    <asp:GridView ID="grid" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                        Caption="Lista de Requisições de Materiais" CssClass="tableView" OnPageIndexChanging="grid_PageIndexChanging"
                        OnRowCreated="grid_RowCreated" OnRowEditing="grid_RowEditing" OnSorting="grid_Sorting"
                        Width="100%">
                        <Columns>
                            <asp:BoundField DataField="data" HeaderText="data" SortExpression="data" 
                                DataFormatString="{0:d}">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="solicitante" SortExpression="Usuario.PessoaFisica.nome">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("id_usuario") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Usuario.PessoaFisica.nome") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="setor" SortExpression="Usuario.Setor.nome">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Usuario.Setor.nome") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("Usuario.Setor.nome") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:CommandField EditText="selecionar" ShowEditButton="True">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:CommandField>
                        </Columns>
                        <SortedAscendingCellStyle CssClass="SortedAscendingCellStyle" />
                        <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle" />
                        <SortedDescendingCellStyle CssClass="SortedDescendingCellStyle" />
                        <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle " />
                    </asp:GridView>
                </asp:Panel>
                <asp:Panel ID="panelCadastro" runat="server">
                    <table class="cadastro">
                        <%--<tr>
                            <th colspan="1">
                                cadastro de requisição</th>
                            <th colspan="1">
                            <div id="dGravacao" runat="server">
                                <asp:Button ID="btInserir" runat="server" onclick="btInserir_Click" 
                                    Text="inserir" ValidationGroup="requisicao" />
                                <asp:Button ID="btAlterar" runat="server" onclick="btAlterar_Click" 
                                    Text="salvar" ValidationGroup="requisicao" />
                                <asp:Button ID="btExcluir" runat="server" CausesValidation="False" 
                                    onclick="btExcluir_Click" Text="excluir" ValidationGroup="requisicao" />
                                <asp:Button ID="btCancelar" runat="server" CausesValidation="False" 
                                    onclick="btCancelar_Click" Text="cancelar" />
                            </div>
                            </th>
                        </tr>--%>
                        <tr>
                            <td class="esquerdo">
                                data:
                                <asp:Label ID="txtCodigo" runat="server" Text="0" Visible="False"></asp:Label>
                            </td>
                            <td class="direito">
                                <asp:Label ID="lblData" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo" style="height: 25px">
                                solicitante:
                            </td>
                            <td class="direito" style="height: 25px">
                                <asp:Label ID="txtUsuario" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        
                        </table>
                                <asp:GridView ID="gridMateriais" runat="server" AllowSorting="True" CssClass="mGrid" 
                                    AutoGenerateColumns="False" Caption="lista de materiais"
                                    ForeColor="White" GridLines="None" Width="100%" BorderStyle="Solid"
                                      
                        OnSelectedIndexChanging="gridMateriais_SelectedIndexChanging">
                                    <Columns>
                                        <%--<asp:CommandField SelectText="selecionar" ShowSelectButton="True" />--%>

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgAdd" runat="server" CssClass="add" ImageUrl="../../Shared/Images/plus.png" CommandName="Select" CausesValidation="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="material" SortExpression="MaterialConsumo.descricao">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("MaterialConsumo.descricao") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("MaterialConsumo.descricao") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="quantidade" HeaderText="quantidade" SortExpression="quantidade" />
                                        <asp:TemplateField HeaderText="status" 
                                            SortExpression="StatusRequisicaoMaterial.nome">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("StatusRequisicaoMaterial.nome") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("StatusRequisicaoMaterial.nome") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Panel ID="pHistorico" runat="server" Width="100%" Visible="false">
                                                    <tr>
                                                    <td></td>
                                                        <td colspan='999'>
                                                            <cControleHistoricoRequisicao:cControleHistoricoRequisicao ID="cControleHistoricoRequisicao1" runat="server" />
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <SortedAscendingCellStyle CssClass="SortedAscendingCellStyle" />
                                    <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle" />
                                    <SortedDescendingCellStyle CssClass="SortedDescendingCellStyle" />
                                    <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle " />
                                </asp:GridView>
                            
                        <table class="cadastro">
                        <tr>
                            <td>
                                &nbsp;<asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                            
                    </table>
                </asp:Panel>
                <%--<asp:Panel ID="pImprimir" runat="server">
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" 
                    CssClass="aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled" 
                    Enabled="False" Font-Names="Verdana" Font-Size="8pt" 
                    InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana" 
                    WaitMessageFont-Size="14pt" Width="100%">
                    <LocalReport ReportPath="Relatorios\Almoxarifado\RelatorioRequisicaoQuantidade.rdlc">
                    </LocalReport>
                </rsweb:ReportViewer>
                </asp:Panel>--%>
            </ContentTemplate>
        </asp:UpdatePanel>
        <ajaxToolkit:UpdatePanelAnimationExtender ID="upae" BehaviorID="animation" runat="server"
            TargetControlID="UpdatePanel1" Enabled="True">
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
