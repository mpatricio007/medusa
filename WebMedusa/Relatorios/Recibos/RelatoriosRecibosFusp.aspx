<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.Master" AutoEventWireup="true" CodeBehind="RelatoriosRecibosFusp.aspx.cs" Inherits="Medusa.Relatorios.Recibos.RelatoriosRecibosFusp" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="conteudo">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        <Scripts>
            <asp:ScriptReference Path="~/Scripts/FixFocus.js" />
        </Scripts>
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                    <table class="cadastro">
                        <tr>
                            <th colspan="2">
                                relatório de recibos
                            </th>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                intervalo de recibos</td>
                            <td class="direito">
                                de
                                <cint:cInteiro ID="cInteiroDe" runat="server" />
                                &nbsp;até
                                <cint:cInteiro ID="cInteiroAte" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <th colspan="2">
                                    <asp:Button ID="btGerar" runat="server" Text="gerar recibos" 
                                        onclick="btGerar_Click" />
                            </th>
                        </tr>
                    </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <AjaxToolKit:UpdatePanelAnimationExtender ID="upae" BehaviorID="animation" runat="server" TargetControlID="UpdatePanel1" Enabled="True">
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
        </AjaxToolKit:UpdatePanelAnimationExtender>
</div>
</asp:Content>
