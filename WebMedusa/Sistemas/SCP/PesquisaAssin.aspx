<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.master" AutoEventWireup="true" CodeBehind="PesquisaAssin.aspx.cs" Inherits="Medusa.Sistemas.SCP.PesquisaAssin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <asp:Panel DefaultButton="btPesquisa" runat="server">
    <div class="conteudo">
    <table class="cadastro">
    <tr>
    <th colspan="2">cartões de assinatura</th>
    </tr>
        <tr>
            <td class="esquerdo">
                projeto:</td>
            <td class="direito">
                <cint:cInteiro ID="cInteiroProjeto" runat="server" 
                    ErrorMsg="digite o número do projeto" MaxLength="6" />
                sub projeto: <ctx:cTexto ID="cTextoSubProj" runat="server" MaxLength="2" 
                                         ValidationGroup="vgIdentificacao" Width="20"  />
                <br />
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
            </td>
        </tr>
     <tr>
    <th colspan="2">
        <asp:Button ID="btPesquisa" runat="server" Text="ok" 
            onclick="btPesquisa_Click" />
        <asp:Button ID="btCancelar" runat="server" Text="cancelar" 
            onclick="btCancelar_Click" />
         </th>
    </tr>
    <tr>
    <td colspan="2">
    <div id="divAssin" runat="server"></div>
    </td>
    </tr>
    </table>
    </div>
    </asp:Panel>
    </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
