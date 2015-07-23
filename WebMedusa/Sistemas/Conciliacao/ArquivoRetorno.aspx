<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.master" AutoEventWireup="true" CodeBehind="ArquivoRetorno.aspx.cs" Inherits="Medusa.Sistemas.Conciliacao.ArquivoRetorno" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
            
                           <table class="cadastro">
                        <tr>
                            <th colspan="2">
                                carregar arquivo de retorno
                                 <div id="updateProgressDiv" style="display:none; position:absolute;">
                        <div style=" margin-left:780px;  float:left">
                        <img src="../../Styles/img/loading.gif" />
                        <span style="margin:3px">Carregando ...</span></div>
                    </div></th>
           
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                selecionar arquivo:
                            </td>
                            <td class="direito">
                                <asp:FileUpload ID="fuArquivo" runat="server" />

                           
                                <asp:Button ID="btCarregarArquivo" runat="server" 
                                    onclick="btCarregarArquivo_Click" Text="importar arquivo" />
                                <br />
                                <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Size="12pt"></asp:Label>
                                  
                            </td>
                        </tr>
                        <tr>
                            <th colspan="2">
                          
                            </th>
                        </tr>
                    </table>
            </ContentTemplate>
             <Triggers><asp:PostBackTrigger ControlID="btCarregarArquivo" />
                </Triggers>
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
