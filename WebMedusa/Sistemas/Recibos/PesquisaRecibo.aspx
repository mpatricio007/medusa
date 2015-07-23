<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.Master" AutoEventWireup="true" CodeBehind="PesquisaRecibo.aspx.cs" Inherits="Medusa.Sistemas.Recibos.PesquisaRecibo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register src="ControleReciboCheques.ascx" tagname="ControleReciboCheques" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
                <%# Eval("displayValue")%> &nbsp 
                   <asp:ImageButton ID="btExcluiFiltro" runat="server"
                       ImageUrl="~/Styles/img/bt_delete.jpg" Width="15px" Height="15px" CommandName="delete" />
                        </div>
                    </ItemTemplate>
                </asp:DataList>
                <div class="pesquisar">
                    <div id="updateProgressDiv" style="display: none; position: absolute;">
                        <div style="margin-left: 780px; float: left">
                            <img src="../../Styles/img/loading.gif" />
                            <span style="margin: 3px">Carregando ...</span>
                        </div>
                    </div>
                    <asp:Label ID="Label1" runat="server" Text="procurar"></asp:Label>
                    &nbsp;<asp:DropDownList ID="ddlOptions" runat="server" CausesValidation="True"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlOptions_SelectedIndexChanged">
                        <asp:ListItem Value="curso">curso</asp:ListItem>
                        <asp:ListItem Value="id_recibo">recibo nº</asp:ListItem>
                        <asp:ListItem Value="data">data</asp:ListItem>
                        <asp:ListItem Value="ReciboCurso.Projeto.codigo">projeto</asp:ListItem>
                        <asp:ListItem Value="valor">valor</asp:ListItem>
                        <asp:ListItem Value="nome">recebemos de</asp:ListItem>
                        <asp:ListItem Value="descricao">referente a</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtProcura" runat="server" Width="137px"></asp:TextBox>
                    &nbsp;<asp:DropDownList ID="listaCurso" runat="server"
                        AppendDataBoundItems="True" DataTextField="nome"
                        DataValueField="id_recibo_curso" Style="margin-bottom: 0px" Width="400px">
                    </asp:DropDownList>
                    <ajaxToolkit:ListSearchExtender ID="listaCurso_ListSearchExtender"
                        runat="server" Enabled="True" PromptCssClass="ListSearchExtenderPrompt"
                        PromptText="digite para procurar" QueryPattern="Contains" QueryTimeout="2000"
                        TargetControlID="listaCurso">
                    </ajaxToolkit:ListSearchExtender>
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

                    <asp:GridView ID="grid" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" Caption="Lista de Recibos"
                        CssClass="tableView"
                        OnPageIndexChanging="grid_PageIndexChanging" OnRowCreated="grid_RowCreated"
                        OnRowEditing="grid_RowEditing" OnSorting="grid_Sorting" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="id_recibo" HeaderText="nº"
                                SortExpression="id_recibo">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>

                            <asp:BoundField DataField="data" HeaderText="data"
                                SortExpression="data" DataFormatString="{0:d}">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="projeto" SortExpression="ReciboCurso.Projeto.codigo">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ReciboCurso.Projeto.codigo") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("ReciboCurso.Projeto.codigo") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="curso" SortExpression="ReciboCurso.nome">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server"
                                        Text='<%# Bind("ReciboCurso.nome") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("ReciboCurso.nome") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="valor" HeaderText="valor" SortExpression="valor"
                                DataFormatString="{0:c}">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nome" HeaderText="recebemos de"
                                SortExpression="nome">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="descricao" HeaderText="referente a"
                                SortExpression="descricao">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="strDocumentos" HeaderText="cpf/cnpj">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="usuário"
                                SortExpression="Usuario.nome">

                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server"
                                        Text='<%# Bind("Usuario.nome") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:CheckBoxField DataField="status_recibo" HeaderText="status"
                                SortExpression="status_recibo">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:CheckBoxField>
                            <asp:CommandField EditText="selecionar" ShowEditButton="True">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:CommandField>
                        </Columns>
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
                            <th colspan="2">cadastro de recibos</th>
                        </tr>
                        <%--<asp:Panel ID="panelCampos" runat="server">--%>
                            <tr>
                                <td class="esquerdo">Recibo Nº:
                                </td>
                                <td class="direito">
                                    <asp:TextBox ID="txtCodigo" runat="server" Enabled="False"
                                        ValidationGroup="recibo" Width="80px">0</asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="esquerdo">data:</td>
                                <td class="direito">
                                    <asp:Label ID="txtData" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="esquerdo">usuário:</td>
                                <td class="direito">
                                    <asp:Label ID="txtUsuario" runat="server" Text="0"></asp:Label>
                                </td>
                            </tr>
                            <%--<tr>
                                <td class="esquerdo">tipo inscrição:</td>
                                <td class="direito">
                                   <asp:Label ID="txtTipoInscricao" runat="server"></asp:Label>
                                </td>
                            </tr>--%>

                            <asp:Panel ID="divCnpj" runat="server">
                                <tr>
                                    <td class="esquerdo">cnpj:</td>
                                    <td class="direito">
                                       <asp:Label ID="txtCnpj" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </asp:Panel>
                            <asp:Panel ID="divCpf" runat="server">
                                <tr>
                                    <td class="esquerdo">cpf:</td>

                                    <td class="direito">
                                       <asp:Label ID="txtCpf" runat="server"></asp:Label>
                                    </td>
                                </tr>

                            </asp:Panel>
                            <tr>
                                <td class="esquerdo">recebemos de:</td>
                                <td class="direito">
                                    <asp:Label ID="txtNome" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="esquerdo">referente a:</td>
                                <td class="direito">
                                    <asp:Label ID="txtDescricao" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="esquerdo">do curso/evento de:</td>
                                <td class="direito">
                                    <asp:Label ID="txtCurso" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="esquerdo">pagamento através de:</td>
                                <td class="direito">
                                    <asp:Label ID="txtTipoPagamento" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="esquerdo">a importância supra de:</td>
                                <td class="direito">
                                    <asp:Label ID="txtValor" runat="server"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td class="esquerdo">observação:</td>
                                <td class="direito">
                                    <asp:Label ID="txtObs" runat="server"></asp:Label>
                                </td>
                            </tr>
                        <%--</asp:Panel>--%>
                        <tr>
                            <td colspan="2">
                                <div id="dMotivo" runat="server">
                                    <table width="100%">
                                        <tr>
                                            <td class="esquerdo">motivo do cancelamento:
                                            </td>
                                            <td class="direito">
                                                <asp:Label ID="txtMotivo" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <tr>
                    <td colspan="2"></td>
                </tr>

                <tr>
                    <td colspan="2"></td>
                </tr>
                    <tr>
                        <td colspan="2">
                            <div>
                                <asp:GridView ID="gridCheque" runat="server" AutoGenerateColumns="False" Caption="lista de cheques" CssClass="tableView" Width="100%">
                                    <Columns>
                                        <asp:BoundField DataField="num_cheque" HeaderText="cheque nº" SortExpression="num_cheque">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="data" DataFormatString="{0:d}" HeaderText="data" SortExpression="data">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="valor" DataFormatString="{0:c}" HeaderText="valor" SortExpression="valor">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                    </Columns>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <SortedAscendingCellStyle CssClass="SortedAscendingCellStyle" />
                                    <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle" />
                                    <SortedDescendingCellStyle CssClass="SortedDescendingCellStyle" />
                                    <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle " />
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>