<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PesquisaCoordenador.ascx.cs"
    Inherits="Medusa.Controles.Pesquisa.PesquisaCoordenador" %>
<asp:Panel ID="panEdit" runat="server" >
    <asp:CheckBox ID="ckFilter" runat="server" AutoPostBack="True" OnCheckedChanged="ckFilter_CheckedChanged"
        Text="habilitar múltiplos filtros" Checked="True" Visible="False" />
    <asp:DataList ID="DataListFiltros" runat="server" RepeatColumns="6" OnDataBinding="DataListFiltros_DataBinding"
        RepeatDirection="Horizontal" OnDeleteCommand="DataListFiltros_DeleteCommand">
        <ItemTemplate>
            <div class="FilterName">
                <%# Eval("property_name") %>&nbsp
                <%# Eval("mode_name")%>&nbsp
                <%# Eval("value")%>
                &nbsp
                <asp:ImageButton ID="btExcluiFiltro" runat="server" ImageUrl="~/Styles/img/bt_delete.jpg"
                    Width="15px" Height="15px" CommandName="delete" CausesValidation="false" />
            </div>
        </ItemTemplate>
    </asp:DataList>  
    <div class="pesquisar">
        <asp:Label ID="Label3" runat="server" Text="filtrar por"></asp:Label>
        &nbsp;<asp:DropDownList ID="ddlOptions" runat="server" CausesValidation="True" 
                        AutoPostBack="True" onselectedindexchanged="ddlOptions_SelectedIndexChanged">
                        <asp:ListItem Value="PessoaFisica.nome">nome</asp:ListItem>
                        <asp:ListItem Value="PessoaFisica.cpf.Value">cpf</asp:ListItem>
                    </asp:DropDownList>
        <asp:TextBox ID="txtProcura" runat="server" Width="137px"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="ddlMode" runat="server">
        </asp:DropDownList>
        &nbsp;<asp:Label ID="Label4" runat="server" Text="mostrar"></asp:Label>
        &nbsp;
        <asp:DropDownList ID="ddlSize" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSize_SelectedIndexChanged">
            <asp:ListItem Selected="True">10</asp:ListItem>
            <asp:ListItem>20</asp:ListItem>
            <asp:ListItem>50</asp:ListItem>
            <asp:ListItem Value="0">todos</asp:ListItem>
        </asp:DropDownList>
        <asp:Button ID="btSearch" runat="server" CausesValidation="False" OnClick="btProcurar_Click"
            Text="adicionar filtro" />
    </div>
    <asp:Panel ID="panelGrid" runat="server">
        <asp:GridView ID="grid" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
            Caption="coordenadores" CellPadding="4" CssClass="tableView" 
            ForeColor="#333333" GridLines="None"
            OnPageIndexChanging="grid_PageIndexChanging" OnSorting="grid_Sorting" 
            Width="100%" onrowediting="grid_RowEditing">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:TemplateField HeaderText="nome" SortExpression="PessoaFisica.nome">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("PessoaFisica.nome") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("PessoaFisica.nome") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="cpf" SortExpression="PessoaFisica.cpf.Value">
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("PessoaFisica.cpf.Value") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField EditText="selecionar" ShowEditButton="True" />
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
</asp:Panel>

