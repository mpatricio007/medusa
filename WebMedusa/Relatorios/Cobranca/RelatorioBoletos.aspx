<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.master" AutoEventWireup="true" CodeBehind="RelatorioBoletos.aspx.cs" Inherits="Medusa.Relatorios.Cobranca.RelatorioBoletos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


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
                    <th colspan="1">relatório de boletos de cobrança</th>
                    <th colspan="1"></th>
                </tr>
                <tr>
                    <td class="esquerdo">data de pagamento:</td>
                    <td class="direito">de:
                <cdt:cData ID="cDataDe" runat="server" />
                        &nbsp;até:
                <cdt:cData ID="cDataAte" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="esquerdo">evento:</td>
                    <td class="direito">
                        <cDdlEventos:cDdlEventos ID="cDdlEventos1" runat="server" ValidationGroup="0" />
                    </td>
                </tr>
                <tr>
                    <th colspan="2">
                        <asp:Button ID="btGerar" runat="server" OnClick="btImportar_Click"
                            Text="gerar relatório" />
                    </th>
                </tr>
            </table>
            <div id="dRelatorio" runat="server"></div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <ajaxToolkit:UpdatePanelAnimationExtender ID="upae" BehaviorID="animation" runat="server" TargetControlID="UpdatePanel1" Enabled="True">
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
    </ajaxToolkit:UpdatePanelAnimationExtender>
</asp:Content>
