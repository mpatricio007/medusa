<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Log.ascx.cs" Inherits="Medusa.Controles.Log" %>
<style type="text/css">
    .tableView caption
    {
        background-color: #5D7B9D;
        color: White;
        font-size: 14pt;
    }
    
    a:link, a:visited
    {
        color: #034af3;
    }
</style>
<div class="conteudo">
    <div id="dPesquisa" runat="server">
        <asp:CheckBox ID="ckFilter" runat="server" AutoPostBack="True" OnCheckedChanged="ckFilter_CheckedChanged"
            Text="habilitar múltiplos filtros" />
        <asp:DataList ID="DataListFiltros" runat="server" OnDataBinding="DataListFiltros_DataBinding"
            OnDeleteCommand="DataListFiltros_DeleteCommand" RepeatColumns="6" RepeatDirection="Horizontal">
            <ItemTemplate>
                <div class="FilterName">
                    <%# Eval("property_name") %>&nbsp;
                    <%# Eval("mode_name")%>&nbsp;
                    <%# Eval("value")%>
                    &nbsp;
                    <asp:ImageButton ID="btExcluiFiltro" runat="server" CommandName="delete" Height="15px"
                        ImageUrl="~/Styles/img/bt_delete.jpg" Width="15px" />
                </div>
            </ItemTemplate>
        </asp:DataList>
        <div class="pesquisar">
            <div id="updateProgressDiv" style="display: none; position: absolute;">
                <div style="margin-left: 780px; float: left">
                    <img src="../../Styles/img/loading.gif" />
                    <span style="margin: 3px">Carregando ...</span></div>
            </div>
            <asp:Label ID="Label1" runat="server" Text="procurar"></asp:Label>
            &nbsp;<asp:DropDownList ID="ddlOptions" runat="server" AutoPostBack="True" CausesValidation="True"
                OnSelectedIndexChanged="ddlOptions_SelectedIndexChanged">
                <asp:ListItem Value="acao">ação</asp:ListItem>
                <asp:ListItem>ip</asp:ListItem>
                <asp:ListItem Value="Usuario.PessoaFisica.nome">usuário</asp:ListItem>
                <asp:ListItem>data</asp:ListItem>
                <asp:ListItem Value="descricao">descrição</asp:ListItem>
                <asp:ListItem Value="alteracoes">alterações</asp:ListItem>
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
            <asp:Button ID="btSearch" runat="server" CausesValidation="False" OnClick="btProcurar_Click"
                Text="procurar" />
        </div>
    </div>
    <asp:Panel ID="panelGrid" runat="server">
        <asp:GridView ID="grid" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
            Caption="Lista de Logs" CellPadding="4" CssClass="tableView" ForeColor="#333333"
            GridLines="None" OnPageIndexChanging="grid_PageIndexChanging" OnRowCreated="grid_RowCreated"
            OnRowEditing="grid_RowEditing" OnSorting="grid_Sorting" Width="100%">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField DataField="id_log" HeaderText="operação nº" SortExpression="id_log">
                </asp:BoundField>
                <asp:BoundField DataField="acao" HeaderText="ação" SortExpression="acao"></asp:BoundField>
                <asp:BoundField DataField="ip" HeaderText="ip" SortExpression="ip" />
                <asp:TemplateField HeaderText="usuário" SortExpression="Usuario.PessoaFisica.nome">
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("Usuario.PessoaFisica.nome") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="data" HeaderText="data" SortExpression="data" />
                <asp:TemplateField HeaderText="alterações" SortExpression="alteracoes">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("alteracoes") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("alteracoes") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" CssClass="SortedAscendingCellStyle" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" CssClass="SortedAscendingHeaderStyle" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" CssClass="SortedDescendingCellStyle" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" CssClass="SortedDescendingHeaderStyle " />
        </asp:GridView>
    </asp:Panel>
</div>
