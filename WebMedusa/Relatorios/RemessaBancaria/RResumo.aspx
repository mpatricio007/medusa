<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.Master" AutoEventWireup="true" CodeBehind="RResumo.aspx.cs" Inherits="Medusa.Relatorios.RemessaBancaria.RResumo" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="conteudo">
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
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="pesquisar">
                    <div id="updateProgressDiv" style="display:none; position:absolute;">
                        <div style=" margin-left:780px;  float:left">
                        <img src="../../Styles/img/loading.gif" alt="" />
                        <span style="margin:3px">Carregando ...</span></div>
                    </div>
                </div>
                <asp:Panel ID="panelCadastro" runat="server">
                    <table class="cadastro">
                        <tr>
                            <th colspan="1">
                                resumo das autorizações para liberação de créditos</th>
                            <th colspan="1">
                            </th>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                data:</td>
                            <td class="direito">
                                
                                <cdt:cData ID="cData1" runat="server" EnableValidator="true" ValidationGroup="resumo" />
                                
                            </td>
                        </tr>
                        <tr>
                            <th colspan="2">
                                <asp:Button ID="btGerarRelatorio" runat="server" Text="gerar relatório" 
                                    ValidationGroup="resumo" onclick="btGerarRelatorio_Click" />
                            </th>
                        </tr>
                    </table>
                    <div id="dRelatorio" runat="server"></div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <ajaxtoolkit:UpdatePanelAnimationExtender ID="upae" BehaviorID="animation" runat="server" TargetControlID="UpdatePanel1" Enabled="True">
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
        </ajaxtoolkit:UpdatePanelAnimationExtender>
</div>
</asp:Content>
