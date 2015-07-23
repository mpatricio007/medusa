<%@ Page Language="C#" MasterPageFile="~/Styles/Site.Master" AutoEventWireup="true" CodeBehind="REtiquetasComodatos.aspx.cs" Inherits="Medusa.Relatorios.REComodatos.REtiquetasComodatos" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="conteudo">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="pesquisar">                   
                    <div id="updateProgressDiv" style="display:none; position:absolute;">
                        <div style=" margin-left:780px;  float:left">
                        <img src="../../Styles/img/loading.gif" />
                        <span style="margin:3px">Carregando ...</span></div>
                    </div>
                    &nbsp;<asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                </div>
                <asp:Panel ID="panelCadastro" runat="server">

                    <table class="cadastro">
        <tr>
              <th colspan="1">
                 relatório de etiquetas </th>
              <th colspan="1">
              </th>
        </tr>

        <tr>
              <td class="esquerdo">
                  intervalo de códigos:</td>
              <td class="direito">
                  <cint:cInteiro ID="cInteiroDe" runat="server" />
                  à
                  <cint:cInteiro ID="cInteiroAte" runat="server" />
              </td>
         </tr>
         <tr>
             <th colspan="2">
                 <asp:Button ID="btGerar" runat="server" onclick="btImportar_Click" 
                 Text="gerar etiquetas" />
             </th>
         </tr>
                 </table>

              </asp:Panel>
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
