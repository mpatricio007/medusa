<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.Master" AutoEventWireup="true" CodeBehind="ReportAdiantamentoAtraso.aspx.cs" Inherits="Medusa.Sistemas.Adiantamentos.ReportAdiantamentoAtraso" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../../Controles/DdlStatusAdiantamentos.ascx" tagname="DdlStatusAdiantamentos" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="conteudo">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                
    
                    <asp:Panel ID="panelCadastro" runat="server">

                    <table class="cadastro">
        <tr>
            <th colspan="1">
                lista de adiantamentos</th> 
            <th colspan="1">
           </th>
        </tr>
                        <tr>
                            <td class="esquerdo">
                                projeto:</td>
                            <td class="direito">
                                <cddlProjeto:cDdlProjeto ID="cDdlProjeto" runat="server" 
                                    causesvalidation="false" ValidationGroup="relatorio"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                status:</td>
                            <td class="direito">
                                <uc1:DdlStatusAdiantamentos ID="DdlStatusAdiantamentos1" runat="server" ValidationGroup="status" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                vencimento até:</td>
                            <td class="direito">
                                <cdt:cData ID="cData1" runat="server" EnableValidator="false" />
                            </td>
                        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                
            </td>
        </tr>
        <tr>
            <th colspan="2">
                <asp:Button ID="btGerar" runat="server" onclick="btGerar_Click" 
                    Text="gerar relatório" />
            </th>
        </tr>
    </table>
                        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
                            Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
                            WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%">
                            <LocalReport ReportPath="Relatorios\Adiantamentos\RelatorioAdiantamentoAtraso.rdlc">
                            </LocalReport>
                        </rsweb:ReportViewer>
                 </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
     
</div>
</asp:Content>
