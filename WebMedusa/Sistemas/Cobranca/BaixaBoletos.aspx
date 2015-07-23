<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.master" AutoEventWireup="true" CodeBehind="BaixaBoletos.aspx.cs" Inherits="Medusa.Sistemas.Cobranca.BaixaBoletos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="conteudo">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        <Scripts>
                <asp:ScriptReference Path="../../Scripts/FixFocus.js" />    
        </Scripts>
      
        </asp:ScriptManager>        
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
          
              <asp:Panel ID="panelCadastro" runat="server">
                    <table class="cadastro">
                        <tr>
                            <th colspan="1">
                                baixa de boletos</th>
                            <th colspan="1">
                            <div id="dGravacao" runat="server">
                                    </div>
                            </th>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                nosso número:</td>
                            <td class="direito">
                                <asp:TextBox ID="txtCodigo" runat="server" onkeyup="formataInteiro(this,event);"
                                    ontextchanged="txtCodigo_TextChanged" AutoPostBack="True"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvCodigo" runat="server" 
                                    ControlToValidate="txtCodigo" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                valor:
                                </td>
                            <td class="direito">
                                <asp:Label ID="lbValor" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                parcela:</td>
                            <td class="direito">
                                <asp:Label ID="lbParcela" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                data de vencimento:</td>
                            <td class="direito">
                                <asp:Label ID="lbDataVencto" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                sacado:</td>
                            <td class="direito">
                                <asp:Label ID="lbSacado" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                data de pagamento:</td>
                            <td class="direito">
                                <cdt:cData ID="cDataPagamento" runat="server" EnableValidator="false" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                valor pago:</td>
                            <td class="direito">
                                <cvl:cValor ID="cValorPago" runat="server" EnableValidator="false"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                observação:</td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoObs" runat="server" EnableValidator="false" TextMode="MultiLine" Height="50px" Width="500px"/>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lbMsg" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th colspan="2">
                            <div id="dGravacao1" runat="server">
                                <asp:Button ID="btAlterar0" runat="server" onclick="btAlterar_Click" 
                                    Text="efetuar baixa" ValidationGroup="atuacao" />
                                <asp:Button ID="btCancelar0" runat="server" CausesValidation="False" 
                                    onclick="btCancelar_Click" Text="cancelar" />
                                    </div>
                            </th>
                        </tr>
                    </table>
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
