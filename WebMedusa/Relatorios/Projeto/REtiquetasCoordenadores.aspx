<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.Master" AutoEventWireup="true" CodeBehind="REtiquetasCoordenadores.aspx.cs" Inherits="Medusa.Relatorios.Projeto.REtiquetasCoordenadores" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="conteudo">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                <asp:Panel ID="panelCadastro" runat="server">

                    <table class="cadastro">
                        <tr>
                            <th colspan="1">relatório de etiquetas</th>
                            <th colspan="1"></th>
                        </tr>
                        <tr>
                            <td class="esquerdo">status do projeto:
                            </td>
                            <td class="direito">
                                <asp:RadioButtonList ID="rdStatus" runat="server" DataTextField="nome"
                                    DataValueField="id_status_projeto" RepeatDirection="Horizontal">
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">projetos:</td>
                            <td class="direito">
                                <asp:RadioButtonList ID="rdDefinitivo" runat="server"
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem Value="False">provisórios</asp:ListItem>
                                    <asp:ListItem Value="True">definitivos</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">tipo:</td>
                            <td class="direito">
                                <asp:RadioButtonList ID="rdTipo" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <th colspan="2">
                                <asp:Button ID="btGerar" runat="server" OnClick="btImportar_Click" CausesValidation="false"
                                    Text="gerar relatório" />
                            </th>
                        </tr>
                    </table>
                         <div id="dRelatorio" runat="server"></div>
                </asp:Panel>
                
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
