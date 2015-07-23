<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PesquisaUsuarios.ascx.cs" Inherits="Medusa.Controles.Pesquisa.PesquisaUsuarios" %>


<%@ Register src="../DdlUsuariosFuspEntradas.ascx" tagname="DdlUsuariosFuspEntradas" tagprefix="uc1" %>


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
