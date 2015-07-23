<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControleContaPessoa.ascx.cs" Inherits="Medusa.Controles.ControleContaPessoa" %>
                <%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
 
                    <table class="cadastro">
                        <tr>
                            <th colspan="2" align="left">
                                dados bancários</th>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                banco:</td>
                            <td class="direito">
                                <cddlBancos:cDdlBancos ID="cDdlBancos1" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                agência:</td>
                            <td class="direito">
                                <ctx:cTexto ID="textAgencia" runat="server" MaxLength="10" />
                                <asp:TextBox ID="textDigitoAgencia" runat="server" MaxLength="1" Width="25px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo" style="height: 27px">
                                conta:</td>
                            <td class="direito" style="height: 27px">
                                <ctx:cTexto ID="textConta" runat="server" MaxLength="10" />
                                <asp:TextBox ID="textDigitoConta" runat="server" MaxLength="2" Width="25px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo" style="height: 27px">
                                tipo conta:</td>
                            <td class="direito" style="height: 27px">
                                <asp:RadioButtonList ID="rbTipoConta" runat="server" 
                                    RepeatDirection="Horizontal">
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                             </table>
                             



