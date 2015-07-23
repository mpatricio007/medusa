<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.Master" AutoEventWireup="true" CodeBehind="RelTaxasReceitasRm.aspx.cs" Inherits="Medusa.Relatorios.TaxasReceitasRm.RelTaxasReceitasRm" %>


<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
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
                relatório de taxas e receitas do Rm</th>
            <th colspan="1">
           </th>
        </tr>
        <tr>
            <td class="esquerdo">
                intervalo de datas:
            </td>
            <td class="direito">
                <cdt:cData ID="cDataDe" runat="server" />
                &nbsp;à
                <cdt:cData ID="cDataAte" runat="server" />
            </td>
        </tr>
                        <tr>
                            <td class="esquerdo">
                                projeto:</td>
                            <td class="direito">
                                <cint:cInteiro ID="cInteiroDe" runat="server" />
                                &nbsp;até
                                <cint:cInteiro ID="cInteiroAte" runat="server" />
                            </td>
                        </tr>
        <tr>
            <th colspan="2">
                <asp:Button ID="btGerar" runat="server" 
                    Text="Gerar Relatório" onclick="btGerar_Click" />
            </th>
        </tr>
    </table>
                     <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
                    Font-Size="8pt" InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana" 
                    WaitMessageFont-Size="14pt" Width="800px" Enabled="False" CssClass="aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled" 
                        
                        
                        
                       >
                    <LocalReport ReportPath="Relatorios\TaxasReceitasRm\RelatorioTaxasReceitasRm.rdlc">
                    </LocalReport>
                </rsweb:ReportViewer>

              </asp:Panel>
         </ContentTemplate>
        </asp:UpdatePanel>
          
           
        
        
</div>
</asp:Content>
