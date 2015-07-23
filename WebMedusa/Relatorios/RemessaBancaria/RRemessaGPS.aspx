<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.Master" AutoEventWireup="true" CodeBehind="RRemessaGPS.aspx.cs" Inherits="Medusa.Relatorios.RemessaBancaria.RRemessaGPS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="conteudo">
        <script type="text/javascript">
            function openLotesDialog() {
                $("#dLotes").dialog({
                    width: 500,
                    position: 'top',
                    modal: true,
                    resizable: false,
                    draggable: false,
                    title: 'lotes enviados'
                }).parent().appendTo(jQuery("form:first"));
                $("#dLotes").dialog("open");
            }

            function closeLotesDialog() {
                $("#dLotes").dialog("close");
            }

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
             <div id="dLotes" title="teste" style="display: none;">
            <asp:UpdatePanel ID="uLotes" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="gridLotesEnviados" runat="server" AutoGenerateColumns="False" EmptyDataText="lotes enviados com sucesso" Height="100%"
                        OnPageIndexChanging="gridLotesEnviados_PageIndexChanging" OnRowDeleting="gridLotesEnviados_RowDeleting">
                        <Columns>
                            <asp:BoundField DataField="id_lote" HeaderText="lote" SortExpression="id_lote" />
                            <asp:BoundField DataField="data_envio" DataFormatString="{0:d}" HeaderText="data envio"
                                SortExpression="data_envio" />
                            <asp:CommandField DeleteText="excluir" ShowDeleteButton="True" />
                        </Columns>
                    </asp:GridView>
                    <table class="cadastro" id="tbReenvio" runat="server">
                        <tr>
                            <th colspan="2">
                                reenvio de lotes
                            </th>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                reenviar lotes acima?:
                            </td>
                            <td class="direito">
                                <asp:RadioButtonList ID="rbNovoEnvioLote" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                                    OnSelectedIndexChanged="rbNovoEnvioLote_SelectedIndexChanged" CausesValidation="false" AppendDataBoundItems="true">
                                    <asp:ListItem Value="True" Selected="False" Text="sim" />
                                    <asp:ListItem Value="False" Selected="True" Text="não" />
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr id="trObs" runat="server">
                            <td class="esquerdo">
                                justificativa:
                            </td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoJustificativa" runat="server" TextMode="MultiLine" Width="300px" MaxLength="150"
                                    Height="100px" ValidationGroup="reenvio" EnableRegularValidator="false" EnableValidator="true" />
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <th colspan="2">
                                <asp:Button ID="btOk" runat="server" Text="ok" ValidationGroup="reenvio"
                                    onclick="btOk_Click" />
                            </th>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
   
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="pesquisar">
                    <div id="updateProgressDiv" style="display: none; position: absolute;">
                        <div style="margin-left: 780px; float: left">
                            <img src="../../Styles/img/loading.gif" alt="" />
                            <span style="margin: 3px">Carregando ...</span>
                        </div>
                    </div>
                </div>
                <asp:Panel ID="panelCadastro" runat="server">
                    <table class="cadastro">
                        <tr>
                            <th colspan="1">relatório de remessas de gps</th>
                            <th colspan="1"></th>
                        </tr>

                        <tr>
                            <td class="esquerdo">intervalo de códigos:</td>
                            <td class="direito">
                                <cint:cInteiro ID="cInteiroDe" runat="server" EnableValidator="false" />
                                à
                  <cint:cInteiro ID="cInteiroAte" runat="server" EnableValidator="false" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">data de pagamento:</td>
                            <td class="direito">
                                <cdt:cData ID="cData1" runat="server" EnableValidator="False" />
                            </td>
                        </tr>
                        <tr>
                            <th colspan="2">
                                <table class="tabcadastro">
                                    <tr>
                                        <td>
                                            <asp:Button ID="btGerarRelatorio0" runat="server" OnClick="btGerarRelatorio_Click"
                                                OnClientClick="onProcessar();" Text="gerar relatório" />
                                            <asp:Button ID="btGerarArquivo" runat="server" OnClick="btGerarArquivo_Click" OnClientClick="onProcessar();" Text="gerar arquivos" />
                                        </td>
                                    </tr>
                                </table>
                            </th>
                        </tr>
                    </table>
                    <div id="dRelatorio" runat="server"></div>
                </asp:Panel>
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
    </div>
</asp:Content>
