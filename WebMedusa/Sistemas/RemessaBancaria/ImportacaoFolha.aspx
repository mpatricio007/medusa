<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.Master" AutoEventWireup="true" CodeBehind="ImportacaoFolha.aspx.cs" Inherits="Medusa.Sistemas.RemessaBancaria.ImportacaoFolha" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

     <script type="text/javascript">       
     
         function showProgressDialog() {
             $("#progressDialog").dialog({
                 autoOpen: false,
                 modal: true,
                 bgiframe: true
             });

             $('#progressDialog').dialog('open');

         }

         function hideProgressDialog() {
             if ($('#progressDialog').dialog('isOpen')) {
                 $('#progressDialog').dialog('close');
             }
         }

         function onProcessar() {
             showProgressDialog();
         }
        </script>


         <div id="progressDialog" title="Processando" style="display: none;">
            <img src="../../Styles/img/loading2.gif" alt="Processing" />
            <p>
                Aguarde...
            </p>
        </div>
    <div class="conteudo">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                
                
                <asp:Panel ID="panelCadastro" runat="server">
                    <table class="cadastro">
                        <tr>
                            <th colspan="1">
                                importar folha de pagamento</th>
                            <th colspan="1">
                            <div id="dGravacao" runat="server">
                                <asp:Button ID="btImportar" runat="server" Text="importar" OnClientClick="onProcessar();"
                                    onclick="btImportar_Click" />
                                <asp:Button ID="btCancelar" runat="server" CausesValidation="False" 
                                    onclick="btCancelar_Click" Text="cancelar" />
                            </div>
                            </th>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                data:<asp:Label ID="txtCodigo" runat="server" Text="0" Visible="False"></asp:Label>
                            </td>
                            <td class="direito">
                                <cdt:cData ID="cData" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                tipo de folha:</td>
                            <td class="direito">
                                <cDdlTipoFolhaPagto:cDdlTipoFolhaPagto ID="cDdlTipoFolhaPagto" runat="server" 
                                    ValidationGroup="0" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th colspan="2">
                            <div id="dGravacao1">
                                <asp:Button ID="btImportar0" runat="server" Text="importar" OnClientClick="onProcessar();"
                                    onclick="btImportar_Click" />
                                <asp:Button ID="btCancelar0" runat="server" CausesValidation="False" 
                                    onclick="btCancelar_Click" Text="cancelar" />
                            </div>
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
