 <%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.master" AutoEventWireup="true" CodeBehind="REtiquetas.aspx.cs" Inherits="Medusa.Sistemas.Arquivo.REtiquetas" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="conteudo">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
             <table class="cadastro">
                        <tr>
                            <th colspan="2" >relatório de etiquetas de volumes</th>
                        </tr>
        
                        <tr>
                            <td class="esquerdo">
                               imprimir etiquetas:</td>
                            <td class="direito">de<cint:cInteiro ID="cInteiroDe" runat="server" />
                             até<cint:cInteiro ID="cInteiroAte" runat="server" />
                            </td>
                        </tr>

                        <tr>
                            <th colspan="2">
                                <asp:Button ID="btGerarRelatorio" runat="server" onclick="btGerarRelatorio_Click" Text="imprimir etiquetas" />
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