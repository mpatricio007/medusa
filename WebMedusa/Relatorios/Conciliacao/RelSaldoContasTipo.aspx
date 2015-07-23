<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.Master" AutoEventWireup="true" CodeBehind="RelSaldoContasTipo.aspx.cs" Inherits="Medusa.Relatorios.Conciliacao.RelSaldoContasTipo" %>
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
                    &nbsp;<asp:Label ID="lblTitulo" runat="server" 
                        Text="relatório de saldo de contas por tipo de conta"></asp:Label>
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                </div>
                <asp:Panel ID="panelCadastro" runat="server">

                    <table class="cadastro">
        <tr>
            <th colspan="1">
                selecionar filtro</th>
            <th colspan="1">
           </th>
        </tr>
        <tr>
            <td class="esquerdo">
                data do saldo:
            </td>
            <td class="direito">
                <cdt:cData ID="cData1" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                
            </td>
        </tr>
        <tr>
            <th colspan="2">
                <asp:Button ID="btGerar" runat="server" onclick="btImportar_Click" 
                    Text="gerar relatório" />
            </th>
        </tr>
    </table>
                    <div id="dRelatorio" runat="server"></div>
                </asp:Panel>
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
</div>
</asp:Content>
