<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.Master" AutoEventWireup="true" CodeBehind="EditaisLic.aspx.cs" Inherits="Medusa.Sistemas.EditaisLic.EditaisLic" ValidateRequest="false" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="aspCt" %>

<%@ Register Src="../../Controles/DdlStatusEditaisLic.ascx" TagName="DdlStatusEditaisLic" TagPrefix="uc3" %>

<%@ Register Src="ControleEditais/ControleEditaisLicAnexo.ascx" TagName="ControleEditaisLicAnexo" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" >
      
        function uploadStarted(sender, args) {
            var editor = $find("<%=  HtmlEditorExtender1.ClientID %>");
    editor._editableDiv_submit();
}
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    
    <div class="conteudo">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:CheckBox ID="ckFilter" runat="server" AutoPostBack="True"
                    OnCheckedChanged="ckFilter_CheckedChanged"
                    Text="habilitar múltiplos filtros" />
                <asp:DataList ID="DataListFiltros" runat="server" RepeatColumns="6"
                    OnDataBinding="DataListFiltros_DataBinding" RepeatDirection="Horizontal"
                    OnDeleteCommand="DataListFiltros_DeleteCommand">
                    <ItemTemplate>

                        <div class="FilterName">

                            <%# Eval("property_name") %>&nbsp
                <%# Eval("mode_name")%>&nbsp
                <%# Eval("value")%> &nbsp 
                   <asp:ImageButton ID="btExcluiFiltro" runat="server"
                       ImageUrl="~/Styles/img/bt_delete.jpg" Width="15px" Height="15px" CommandName="delete" />
                        </div>
                    </ItemTemplate>
                </asp:DataList>
                <div class="pesquisar">
                    <div id="updateProgressDiv" style="display: none; position: absolute;">
                        <div style="margin-left: 780px; float: left">
                            <img src="../../Styles/img/loading.gif" alt="" />
                            <span style="margin: 3px">Carregando ...</span>
                        </div>
                    </div>
                    <asp:Label ID="Label1" runat="server" Text="procurar"></asp:Label>
                    &nbsp;<asp:DropDownList ID="ddlOptions" runat="server" CausesValidation="True"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlOptions_SelectedIndexChanged">
                        <asp:ListItem Value="titulo">titulo</asp:ListItem>
                        <asp:ListItem Value="data">data</asp:ListItem>
                        <asp:ListItem Value="StatusEditaisLic.nome">status</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtProcura" runat="server" Width="137px"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlMode" runat="server">
                    </asp:DropDownList>
                    <asp:Label ID="Label2" runat="server" Text="mostrar"></asp:Label>
                    &nbsp;
                    <asp:DropDownList ID="ddlSize" runat="server" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlSize_SelectedIndexChanged">
                        <asp:ListItem Selected="True">10</asp:ListItem>
                        <asp:ListItem>20</asp:ListItem>
                        <asp:ListItem>50</asp:ListItem>
                        <asp:ListItem Value="0">todos</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="btSearch" runat="server" CausesValidation="False"
                        OnClick="btProcurar_Click" Text="procurar" />
                    <asp:Button ID="btCriar" runat="server" CausesValidation="False"
                        OnClick="btCriar_Click" Text="novo" Width="80px" />
                </div>
                <asp:Panel ID="panelGrid" runat="server" HorizontalAlign="Left">
                    <asp:GridView ID="grid" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" Caption="Lista de Editais" CssClass="tableView" OnPageIndexChanging="grid_PageIndexChanging" OnRowCreated="grid_RowCreated" OnRowEditing="grid_RowEditing" OnSorting="grid_Sorting" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="data" DataFormatString="{0:d}" HeaderText="data" SortExpression="data">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="titulo" HeaderText="título" SortExpression="titulo">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="status" SortExpression="StatusEditaisLic.nome">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("StatusEditaisLic.nome") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("StatusEditaisLic.nome") %>'></asp:Label>
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
                        <tr>
                            <th colspan="1" >cadastro de editais</th>
                            <th colspan="1">
                                <div id="dGravacao" runat="server">
                                    <asp:Button ID="btInserir" runat="server" OnClick="btInserir_Click"
                                        Text="inserir" ValidationGroup="edital" />
                                    <asp:Button ID="btAlterar" runat="server" OnClick="btAlterar_Click"
                                        Text="salvar" ValidationGroup="edital" />
                                    <asp:Button ID="btExcluir" runat="server" CausesValidation="False"
                                        OnClick="btExcluir_Click" Text="excluir" ValidationGroup="edital" />
                                    <asp:Button ID="btCancelar" runat="server" CausesValidation="False"
                                        OnClick="btCancelar_Click" Text="cancelar" ValidationGroup="edital" />
                                </div>
                            </th>
                        </tr>
                        <tr>
                            <td class="esquerdo">título:
                                <asp:Label ID="txtCodigo" runat="server" Text="0" Visible="False"></asp:Label>
                            </td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoTitulo" runat="server" EnableValidator="True"
                                    MaxLength="100" Width="580px" ValidationGroup="edital" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo" >descrição:</td>
                            <td class="direito">
                                <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" Columns="80" Rows="23" ValidationGroup="edital" />
                                    <ajaxToolkit:HtmlEditorExtender ID="HtmlEditorExtender1" runat="server" DisplaySourceTab="true" TargetControlID="txtContent" EnableSanitization="true">
                                        <Toolbar>
                                            <ajaxToolkit:Undo />
                                            <ajaxToolkit:Redo />
                                            <ajaxToolkit:Bold />
                                            <ajaxToolkit:Italic />
                                            <ajaxToolkit:Underline />
                                            <ajaxToolkit:StrikeThrough />
                                            <ajaxToolkit:Subscript />
                                            <ajaxToolkit:Superscript />
                                            <ajaxToolkit:JustifyLeft />
                                            <ajaxToolkit:JustifyCenter />
                                            <ajaxToolkit:JustifyRight />
                                            <ajaxToolkit:JustifyFull />
                                            <ajaxToolkit:InsertOrderedList />
                                            <ajaxToolkit:InsertUnorderedList />
                                            <ajaxToolkit:CreateLink />
                                            <ajaxToolkit:UnLink />
                                            <ajaxToolkit:RemoveFormat />
                                            <ajaxToolkit:SelectAll />
                                            <ajaxToolkit:UnSelect />
                                            <ajaxToolkit:Delete />
                                            <ajaxToolkit:Cut />
                                            <ajaxToolkit:Copy />
                                            <ajaxToolkit:Paste />
                                            <ajaxToolkit:BackgroundColorSelector />
                                            <ajaxToolkit:ForeColorSelector />
                                            <ajaxToolkit:FontNameSelector />
                                            <ajaxToolkit:FontSizeSelector />
                                            <ajaxToolkit:Indent />
                                            <ajaxToolkit:Outdent />
                                            <ajaxToolkit:InsertHorizontalRule />
                                            <ajaxToolkit:HorizontalSeparator />
                                            <ajaxToolkit:InsertImage />
                                        </Toolbar>
                                    </ajaxToolkit:HtmlEditorExtender>
                            </td>
                            <tr>
                                <td class="esquerdo" style="width: 25%; height: 34px;">data:</td>
                                <td class="direito" style="height: 34px">
                                    <cdt:cData ID="cData1" runat="server" ValidationGroup="edital" />
                                </td>
                            </tr>
                            <tr>
                                <td class="esquerdo" style="width: 25%; height: 78px;">status:</td>
                                <td class="direito" style="height: 78px">
                                    <uc3:DdlStatusEditaisLic ID="DdlStatusEditaisLic1" runat="server" ValidationGroup="edital" />
                                </td>
                            </tr>
                            <asp:Panel ID="panelAnexos" runat="server">
                                <tr>
                                    <td class="esquerdo" style="width: 25%">anexos:</td>
                                    <td class="direito">
                                        <uc2:ControleEditaisLicAnexo ID="ControleEditaisLicAnexo1" runat="server" />
                                    </td>
                                </tr>
                            </asp:Panel>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th colspan="2">
                                    <div id="dGravacao1">
                                        <asp:Button ID="btInserir0" runat="server" OnClick="btInserir0_Click" Text="inserir" ValidationGroup="edital" />
                                        <asp:Button ID="btAlterar0" runat="server" OnClick="btAlterar_Click" Text="salvar" ValidationGroup="edital" />
                                        <asp:Button ID="btExcluir0" runat="server" CausesValidation="False" OnClick="btExcluir_Click" Text="excluir" ValidationGroup="edital" />
                                        <asp:Button ID="btCancelar0" runat="server" CausesValidation="False" OnClick="btCancelar_Click" Text="cancelar" ValidationGroup="edital" />
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
