<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.master" AutoEventWireup="true"
    CodeBehind="RPagtosComOcorrencia.aspx.cs" Inherits="Medusa.Relatorios.RemessaBancaria.RPagtosComOcorrencia" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="pesquisar">
                <div id="updateProgressDiv" style="display: none; position: absolute;">
                    <div style="margin-left: 780px; float: left">
                        <img src="../../Styles/img/loading.gif" alt="" />
                        <span style="margin: 3px">Carregando ...</span></div>
                </div>
            </div>
            <asp:Panel ID="panelCadastro" runat="server">
                <table class="cadastro">
                    <tr>
                        <th colspan="1">
                            relatório de pagamentos com ocerrência</th>
                        <th colspan="1">
                        </th>
                    </tr>
                    <tr>
                        <td class="esquerdo">
                            intervalo de lotes:
                        </td>
                        <td class="direito">
                            <cint:cInteiro ID="cInteiroDe" runat="server" EnableValidator="false" />
                            à
                            <cint:cInteiro ID="cInteiroAte" runat="server" EnableValidator="false" />
                        </td>
                    </tr>
                    <tr>
                        <td class="esquerdo">
                            data de processamento do retorno:
                        </td>
                        <td class="direito">
                            <cdt:cData ID="cData1" runat="server" EnableValidator="False" />
                        </td>
                    </tr>
                    <tr>
                        <td class="esquerdo">
                            data de pagamento:</td>
                        <td class="direito">
                            de
                            <cdt:cData ID="cData2" runat="server" EnableValidator="False" />
                            até
                            <cdt:cData ID="cData3" runat="server" EnableValidator="False" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th colspan="2">
                            <asp:Button ID="btGerarRelatorio0" runat="server" Text="gerar relatório" 
                                onclick="btGerarRelatorio0_Click" />
                        </th>
                    </tr>
                </table>
                <div id="dRelatorio" runat="server">
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
