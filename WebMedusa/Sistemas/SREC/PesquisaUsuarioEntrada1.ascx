<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PesquisaUsuarioEntrada1.ascx.cs" Inherits="Medusa.Sistemas.SREC.PesquisaUsuarioEntrada1" %>

<%@ Register src="../../Controles/DdlUsuariosFuspEntradas.ascx" tagname="DdlUsuariosFuspEntradas" tagprefix="uc1" %>

<asp:Panel ID="pPesq" runat="server" Visible="false">
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
                    <div id="updateProgressDiv" style="display:none; position:absolute;">
                        <div style=" margin-left:780px;  float:left">
                        <img src="../../Styles/img/loading.gif" alt="carregando"/>
                        <span style="margin:3px">Carregando ...</span></div>
                    </div>
                    <asp:Label ID="Label1" runat="server" Text="procurar"></asp:Label>
                    &nbsp;<asp:DropDownList ID="ddlOptions" runat="server" 
                        AutoPostBack="True" 
                        onselectedindexchanged="ddlOptions_SelectedIndexChanged">
                        <asp:ListItem Value="id_usuario">id_usuario</asp:ListItem>
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
</asp:Panel>

<asp:Panel ID="panelCadastro" runat="server">
    <table class="cadastro">
        <tr>
            <td class="esquerdo">para:<asp:Label ID="txtCodigo" runat="server" Text="-1" Visible="False"></asp:Label>
            </td>
            <td class="direito">
                <asp:RadioButtonList ID="rbTipoBusca" runat="server" AutoPostBack="true" AppendDataBoundItems="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbTipoBusca_SelectedIndexChanged"></asp:RadioButtonList>

                <uc1:DdlUsuariosFuspEntradas ID="DdlUsuariosFuspEntradas1" runat="server" OnSelectedIndexChanged="DdlUsuariosFuspEntradas1_SelectedIndexChanged" AutoPostBack="true" EnableValidator="true" />

                <cDdlSetorEntrada:cDdlSetorEntrada ID="cDdlSetorEntrada1" runat="server" OnSelectedIndexChanged="cDdlSetorEntrada1_SelectedIndexChanged" AutoPostBack="true" EnableValidator="true" />
            </td>
        </tr>
    </table>
</asp:Panel>
