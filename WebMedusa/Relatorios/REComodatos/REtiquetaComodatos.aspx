<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.master" AutoEventWireup="true" CodeBehind="REtiquetaComodatos.aspx.cs" Inherits="Medusa.Relatorios.REComodatos.REtiquetaComodatos" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        <Scripts>
            <asp:ScriptReference Path="~/Scripts/FixFocus.js" />
        </Scripts>
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
            
                    <table class="cadastro">
        <tr>
            <th colspan="1">
                relatório de entradas</th>
            <th colspan="1">
           </th>
        </tr>
        <tr>
            <td class="esquerdo">
                ano:</td>
            <td class="direito">
                <asp:DropDownList ID="ddlAno" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
                        <tr>
                            <td class="esquerdo">
                                intervalo de protoclos:
                            </td>
                            <td class="direito">
                                <cint:cInteiro ID="cInteiroDe" runat="server" />
                                à
                                <cint:cInteiro ID="cInteiroAte" runat="server" />
                            </td>
                        </tr>
        <tr>
            <th colspan="2">
                <asp:Button ID="btGerar" runat="server" onclick="btImportar_Click" 
                    Text="gerar relatório" />
            </th>
        </tr>
    </table>
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" 
                    CssClass="aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled" 
                    Enabled="False" Font-Names="Verdana" Font-Size="8pt" 
                    InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana" 
                    WaitMessageFont-Size="14pt" Width="100%">
                    <LocalReport ReportPath="Relatorios\REComodatos\REtiquetaComodato.rdlc">
                    </LocalReport>
                </rsweb:ReportViewer>
            </ContentTemplate>
            </asp:UpdatePanel>
           <asp:UpdatePanelAnimationExtender ID="upae" BehaviorID="animation" runat="server" TargetControlID="UpdatePanel1" Enabled="True">
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
        </asp:UpdatePanelAnimationExtender>
</asp:Content>