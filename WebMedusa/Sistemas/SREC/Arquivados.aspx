<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.Master" AutoEventWireup="true" CodeBehind="Arquivados.aspx.cs" Inherits="Medusa.Sistemas.SREC.Arquivados" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../../Controles/ListaPessoaTelefones.ascx" tagname="ListaPessoaTelefones" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function openDialog() {
            var dlg = $("#dPara").dialog({
                width: 700,
                height: 400,
                position: 'top',
                modal: true,
                resizable: false,
                draggable: false,
                title: 'encaminhar'
            });

            dlg.parent().appendTo(jQuery("form:first"));
            $("#dPara").dialog("open");

        }

        function closeDialog() {
            $("#dPara").dialog("close");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    

    <div class="conteudo">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div id="dPara" style="display: none">
            <asp:UpdatePanel ID="upPara" runat="server">
                <ContentTemplate>
                    <table class="cadastro">
                        <tr>
                            <td class="esquerdo">observação:
                            </td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoUltimaObs" runat="server" TextMode="MultiLine" Width="350"
                                    Height="200" MaxLength="120" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">para:
                            </td>
                            <td class="direito">
                                <cDdlUsuarioFUSP:cDdlUsuarioFUSP ID="cDdlUsuarioFUSP1" runat="server" />
                            </td>
                        </tr>

                        <tr>
                            <th colspan="2" bgcolor="Silver">
                                <asp:Button ID="btOk" runat="server" Text="encaminhar" OnClick="btOk_Click" />
                            </th>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

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
                        <asp:ListItem Value="nprotent">protocolo </asp:ListItem>
                        <asp:ListItem Value="dataprot">data</asp:ListItem>
                        <asp:ListItem Value="codproj"></asp:ListItem>
                        <asp:ListItem Value="codproja">projeto</asp:ListItem>
                        <asp:ListItem Value="tipodocent">projeto A</asp:ListItem>
                        <asp:ListItem Value="numdocent">tipo do documento</asp:ListItem>
                        <asp:ListItem Value="valorent">numerovalor</asp:ListItem>
                        <asp:ListItem Value="enviadoent">enviado por</asp:ListItem>
                        <asp:ListItem Value="descrent">descrição</asp:ListItem>
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
                    <asp:Label ID="txtCodigo" runat="server" Text="0" Visible="False"></asp:Label>
                </div>
                <asp:Panel ID="panelGrid" runat="server">
                          
                            
                    <asp:GridView ID="grid" runat="server" AllowPaging="True" AllowSorting="True" 
                        AutoGenerateColumns="False" Caption="Lista de Arquivados" CellPadding="4" 
                        CssClass="tableView" ForeColor="#333333" GridLines="None" 
                        onpageindexchanging="grid_PageIndexChanging" onrowcreated="grid_RowCreated" 
                        onrowediting="grid_RowEditing" onsorting="grid_Sorting" Width="100%">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:BoundField DataField="nprotent" HeaderText="protocolo" SortExpression="nprotent" />
                            <asp:BoundField DataField="codproj" HeaderText="projeto" SortExpression="codproj" />
                            <asp:BoundField DataField="codproja" HeaderText="projeto a" SortExpression="codproja" />
                            <asp:BoundField DataField="tipodocent" HeaderText="tipo documento" SortExpression="tipodocent" />
                            <asp:BoundField DataField="numdocent" HeaderText="n° documento" SortExpression="numdocent" />
                            <asp:BoundField DataField="valorent" HeaderText="valor" SortExpression="valorent" />
                            <asp:TemplateField HeaderText="de" SortExpression="UltimoPara.PessoaFisica.nome">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("UltimoDe.PessoaFisica.nome") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("UltimoDe.PessoaFisica.nome") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="para" SortExpression="UltimoPara.PessoaFisica.nome">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("UltimoPara.PessoaFisica.nome") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("UltimoPara.PessoaFisica.nome") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                               
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("HtmlPaginaEntrada") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField EditText="encaminhar" ShowEditButton="True">
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
            </ContentTemplate>
        </asp:UpdatePanel>
        <ajaxtoolkit:UpdatePanelAnimationExtender ID="upae" BehaviorID="animation" runat="server" TargetControlID="UpdatePanel1" Enabled="True">
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
        </ajaxtoolkit:UpdatePanelAnimationExtender>
</div>
</asp:Content>
