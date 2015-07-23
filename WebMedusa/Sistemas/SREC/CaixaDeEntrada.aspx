<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.Master" AutoEventWireup="true" CodeBehind="CaixaDeEntrada.aspx.cs" Inherits="Medusa.Sistemas.SREC.CaixaDeEntrada" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../Controles/ListaPessoaTelefones.ascx" TagName="ListaPessoaTelefones" TagPrefix="uc1" %>
<%@ Register Src="ControleHistoricoEntrada.ascx" TagName="ControleHistoricoEntrada" TagPrefix="uc2" %>
<%--<%@ Register Src="../../Controles/Pesquisa/PesquisaUsuarios.ascx" TagName="PesquisaUsuarios" TagPrefix="uc3" %>--%>
<%@ Register src="PesquisaUsuarioEntrada1.ascx" tagname="PesquisaUsuarioEntrada1" tagprefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .btnconfig {
            vertical-align: sub;
        }
    </style>
    <script type="text/javascript">
        function pageLoad() {
            var tab = $("#abas").tabs({
                selected: $('#hfSelectedTAB').val(),
                select: function (event, ui) {
                    $('#hfSelectedTAB').val(ui.index);
                }
            });

            $("#<%= txtTabIndex.ClientID %>").val(String($(".ui-state-active").attr("id")));

            $(".aba-1").click(
                function (event, ui) {
                    $("#<%= txtTabIndex.ClientID %>").val(String($(".ui-state-active").attr("id")));
                    $("#<%= btTabSelect.ClientID %>").click();
                });
        }

        function openDialog() {
            var dlg = $("#dProvidencia").dialog({
                width: 700,
                position: 'top',
                modal: true,
                resizable: false,
                draggable: false,
                title: 'providência'
            });

            dlg.parent().appendTo(jQuery("form:first"));
            $("#dProvidencia").dialog("open");

        }

        function closeDialog() {
            $("#dProvidencia").dialog("close");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="conteudo">
        <p>
            <input type="text" id="hfSelectedTAB" value="0" style="display: none;" />
        </p>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <div id="dProvidencia" style="display: none">
            <asp:UpdatePanel ID="upPara" runat="server">
                <ContentTemplate>
                        <table class="cadastro">
                            <tr>
                                <td class="esquerdo">providência:</td>
                                <td class="direito">
                                    <cDdlPossivelProvidencia:cDdlPossivelProvidencia ID="cDdlPossivelProvidencia1" AutoPostBack="true" runat="server" EnableValidator="false" ValidationGroup="ok" OnSelectedIndexChanged="cDdlPossivelProvidencia1_SelectedIndexChanged" />
                                </td>
                            </tr>
                            <tr>
                                <td class="esquerdo">observação:</td>
                                <td class="direito">
                                    <ctx:cTexto ID="cTextoUltimaObs" runat="server" TextMode="MultiLine" Width="350"
                                        Height="200" EnableValidator="false" ValidationGroup="ok" />
                                </td>
                            </tr>
                            <tr id="trShortUsers" runat="server">
                                <td colspan="2" cellspacing="0">
                                    <uc4:PesquisaUsuarioEntrada1 ID="PesquisaUsuarioEntrada11" runat="server" ValidationGroup="ok" />
                                    <%--<uc3:PesquisaUsuarios ID="PesquisaUsuarios1" runat="server" ValidationGroup="ok" />--%>
                                </td>
                            </tr>

                            <tr>
                                <th colspan="2" bgcolor="Silver">
                                    <asp:Button ID="btOk" runat="server" Text="ok" OnClick="btAlterar_Click" ValidationGroup="ok" />
                                    <asp:Button ID="btCancelProvidencia" OnClientClick="closeDialog();" runat="server" Text="cancelar" />
                                    <asp:Label ID="lblMsgErrorProvidencia" runat="server"></asp:Label>
                                </th>
                            </tr>
                        </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">

            <ContentTemplate>
                <div id="dTabs" style="display: none">
                    <asp:TextBox ID="txtTabIndex" runat="server"></asp:TextBox>
                    <asp:Button ID="btTabSelect" runat="server" OnClick="btTabSelect_Click" CausesValidation="false" />
                </div>
                <asp:CheckBox ID="ckFilter" runat="server" AutoPostBack="True"
                    OnCheckedChanged="ckFilter_CheckedChanged"
                    Text="habilitar múltiplos filtros" />
                <asp:Label ID="txtCodigo" runat="server" Text="0" Visible="False"></asp:Label>
                <asp:DataList ID="DataListFiltros" runat="server" RepeatColumns="6"
                    OnDataBinding="DataListFiltros_DataBinding" RepeatDirection="Horizontal"
                    OnDeleteCommand="DataListFiltros_DeleteCommand">
                    <ItemTemplate>

                        <div class="FilterName">

                            <%# Eval("property_name") %>&nbsp
                <%# Eval("mode_name")%>&nbsp
                <%# Eval("value")%> &nbsp 
                   <asp:ImageButton ID="btExcluiFiltro" runat="server"
                       ImageUrl="~/Styles/img/delete_img.png" Width="15px" Height="15px"
                       CausesValidation="false" CommandName="delete" ToolTip="excluir" />
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
                    ano
                    <asp:DropDownList ID="ddlAno" runat="server" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlAno_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:Label ID="Label1" runat="server" Text="procurar"></asp:Label>
                    &nbsp;<asp:DropDownList ID="ddlOptions" runat="server"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlOptions_SelectedIndexChanged">
                        <asp:ListItem Value="nprotent">protocolo </asp:ListItem>
                        <asp:ListItem Value="dataprot">data</asp:ListItem>
                        <asp:ListItem Value="codproj">projeto</asp:ListItem>
                        <asp:ListItem Value="codproja">projeto A</asp:ListItem>
                        <asp:ListItem Value="tipodocent">tipo do documento</asp:ListItem>
                        <asp:ListItem Value="numdocent">numero</asp:ListItem>
                        <asp:ListItem Value="valorent">valor</asp:ListItem>
                        <asp:ListItem Value="enviadoent">enviado por</asp:ListItem>
                        <asp:ListItem Value="descrent">descrição</asp:ListItem>
                        <asp:ListItem Value="UsuarioPara.PessoaFisica.nome">encaminhado para</asp:ListItem>
                        <asp:ListItem Value="UsuarioEntrada.PessoaFisica.nome">usuario</asp:ListItem>
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
                </div>
                <asp:Panel ID="panelGrid" runat="server">
                    <h2>Caixa de Entrada
                    <asp:Button ID="btProvidencia" runat="server" OnClick="btProvidencia_Click" CausesValidation="false" ToolTip="providência"
                            Style="vertical-align:baseline; border-bottom-style:outset; width:23px; background: transparent url(../../Styles/img/config_img2.png) center" />
                    </h2>   
                    <div id="abas">
                        <ul id="ulAbas" runat="server">
                        </ul>
                        <div id="aba-1">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                            <asp:GridView ID="grid" runat="server" AllowPaging="True" AllowSorting="True"
                                AutoGenerateColumns="False" CellPadding="4" OnSelectedIndexChanging="grid_SelectedIndexChanging"
                                ForeColor="#333333" GridLines="None" OnPageIndexChanging="grid_PageIndexChanging"
                                OnRowCreated="grid_RowCreated" OnSorting="grid_Sorting" Width="100%" EmptyDataText="nenhum documento nesta aba"
                                EnableTheming="False" CssClass="mGrid" PagerStyle-CssClass="pgr">
                                <RowStyle BackColor="#dedede" ForeColor="Black" />
                                <AlternatingRowStyle BackColor="White" ForeColor="Black" />
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:ImageButton ID="imbClear" runat="server" OnClick="imbClear_Click" ValidationGroup="clear" ToolTip="limpar" ImageUrl="~/Styles/img/uncheck_img.png" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ckItem" runat="server" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="10px" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgAdd" runat="server" CssClass="add" ImageUrl="../../Shared/images/plus.png" CommandName="Select" CausesValidation="false" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" Width="10px" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="nprotent" HeaderText="protocolo">
                                        <HeaderStyle HorizontalAlign="Left" ForeColor="White" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="dataent" HeaderText="data">
                                        <HeaderStyle HorizontalAlign="Left" ForeColor="White" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="codproj" HeaderText="projeto">
                                        <HeaderStyle HorizontalAlign="Left" ForeColor="White" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="codproja" HeaderText="projeto a">
                                        <HeaderStyle HorizontalAlign="Left" ForeColor="White" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="tipodocent" HeaderText="tipo documento">
                                        <HeaderStyle HorizontalAlign="Left" ForeColor="White" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="numdocent" HeaderText="nº documento">
                                        <HeaderStyle HorizontalAlign="Left" ForeColor="White" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="valor">
                                        <ItemTemplate>
                                            <asp:Label ID="LabelValor" runat="server" Text='<%# Eval("ValorMoeda.sigla") + " " +Eval("valorent")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" ForeColor="White" />
                                        <ItemStyle Width="100px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="enviadoent" HeaderText="enviado por">
                                        <HeaderStyle HorizontalAlign="Left" ForeColor="White" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="descrent" HeaderText="descrição">
                                        <HeaderStyle HorizontalAlign="Left" ForeColor="White" Width="250px" />
                                        <ItemStyle HorizontalAlign="Left" Width="250px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="obsent" HeaderText="observação">
                                        <HeaderStyle HorizontalAlign="Left" ForeColor="White" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="usuário">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox300" runat="server" 
                                                Text='<%# Bind("UsuarioEntrada.login") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label300" runat="server" 
                                                Text='<%# Bind("UsuarioEntrada.login") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" ForeColor="White" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Panel ID="pHistorico" runat="server" Width="100%" Visible="false">

                                                <tr>
                                                    <td></td>
                                                    <td colspan='999'>
                                                        <asp:GridView ID="gridHistorico" runat="server" AllowSorting="true" Width="100%"
                                                            AutoGenerateColumns="false" BorderStyle="Solid" CellPadding="4" OnPageIndexChanging="grid_PageIndexChanging"
                                                            CssClass="mGrid" EnableTheming="False" GridLines="None">
                                                            <RowStyle BackColor="#dedede" ForeColor="Black" />
                                                            <AlternatingRowStyle BackColor="White" ForeColor="Black" />
                                                            <Columns>
                                                                <asp:BoundField DataField="data" HeaderText="data">
                                                                    <HeaderStyle HorizontalAlign="Left" ForeColor="White" Width="120px" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="status">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="TextBoxSE" runat="server"
                                                                            Text='<%# Bind("StatusEntrada.nome") %>'></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="LabelSE" runat="server"
                                                                            Text='<%# Bind("StatusEntrada.nome") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" ForeColor="White" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="usuário">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="TextBox2" runat="server"
                                                                            Text='<%# Bind("UsuarioDe.PessoaFisica.nome") %>'></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label2" runat="server"
                                                                            Text='<%# Bind("UsuarioDe.PessoaFisica.nome") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" ForeColor="White" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField HtmlEncode="false" DataField="StrDestinatarios" HeaderText="destinatários" DataFormatString="">
                                                                    <HeaderStyle HorizontalAlign="Left" ForeColor="White" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="obs" HeaderText="observação">
                                                                    <HeaderStyle HorizontalAlign="Left" ForeColor="White" Width="500px" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </asp:Panel>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle HorizontalAlign="Left" />
                                <PagerStyle CssClass="pgr" />
                            </asp:GridView>
                        </div>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <ajaxToolkit:UpdatePanelAnimationExtender ID="upae" BehaviorID="animation"
            runat="server" TargetControlID="UpdatePanel1" Enabled="True">
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
