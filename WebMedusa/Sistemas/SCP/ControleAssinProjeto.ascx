<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControleAssinProjeto.ascx.cs" Inherits="Medusa.Sistemas.SCP.ControleAssinProjeto" %>

<div id="dConteudo" runat="server">

<asp:Panel ID="panelGrid" runat="server">

<div>
                    <asp:Button ID="btCriar" runat="server" CausesValidation="False" 
                        onclick="btCriar_Click"  Text="adicionar cartão de assinatura"  />
                    <br />
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div id="divAssin" runat="server"></div>  
</div>
</asp:Panel>
<asp:Panel ID="panelCadastro" runat="server"> 
        <table class="mGrid" width="50%">
            <tr>
                <td class="esquerdo">
                    validade:<asp:Label ID="txtCodigo" runat="server" Text="0" Visible="False"></asp:Label>
                </td>
                <td class="direito">
                    <cdt:cData ID="cDataValidade" runat="server" ValidationGroup="assin" />
                </td>
            </tr>
           <tr>
                            <td class="esquerdo">
                                arquivo:</td>
                            <td class="direito">
                                <ajaxToolkit:AsyncFileUpload ID="AsyncFileUpload1" runat="server" UploadingBackColor="Yellow"
            OnUploadedComplete="ProcessUpload" ThrobberID="spanUploading" UploaderStyle="Modern"
                                    Width="300px" />
        <span id="spanUploading" runat="server" visible="false">
                                <img class="style1" 
                                    src="../../Styles/img/loading2.gif" alt="carregando" /></span>
                            </td>
                        </tr>
        <tr>
            <td  class="direito" colspan="2">
                <asp:Label ID="lblMsg0" runat="server" ForeColor="Red"></asp:Label></td>
        </tr>
        <tr>
            <th colspan="2">
               
                       <table>
                    <tr>
                        <td>
                            <div ID="dGravacao" runat="server">
                                <asp:Button ID="btInserir0" runat="server" onclick="btInserir_Click" 
                                    Text="inserir" ValidationGroup="assin" />
                            </div>
                       </td>
                            
                            
                      <td><asp:Button ID="btCancelar0" runat="server" CausesValidation="False" 
                                    onclick="btCancelar_Click" Text="cancelar" /></td>
                      </tr>
                      </table>
                  
            </th>
        </tr>
    </table>
</asp:Panel>
</div>