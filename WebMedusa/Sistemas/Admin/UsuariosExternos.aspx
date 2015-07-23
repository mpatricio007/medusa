<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/SiteBasePJ.Master" AutoEventWireup="true" CodeBehind="UsuariosExternos.aspx.cs" Inherits="Medusa.Sistemas.Admin.UsuariosExternos" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadBaseMaster" runat="server">  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBaseMaster" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentBaseMaster" runat="server">
    <div class="conteudo">
        <asp:ScriptManager ID="ScriptManager1" runat="server">

        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
      
                    <table class="cadastro">
                        <tr>
                            <th colspan="2">
                                cadastro de usuários</th>
                        </tr>
                                 <tr>
                            <td class="esquerdo">
                                cpf:<asp:TextBox ID="txtCodigo" runat="server" Enabled="False" Visible="False" >0</asp:TextBox>
                                <asp:Label ID="txtId_pessoa" runat="server" Text="0" Visible="False"></asp:Label>
                            </td>
                            <td class="direito">
                                <cCpf:cCPF ID="cCPF1" runat="server" />
                             
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                nome:
                                </td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoNome" runat="server" EnableValidator="True" Width="500px" 
                                    MaxLength="100" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                sexo:</td>
                            <td class="direito">
                                <cddlSexo:cDdlSexo ID="cDdlSexo1" runat="server"  />
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="esquerdo">
                                rg:</td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoRG" runat="server" MaxLength="20" />
                            </td>
                        </tr>
                          <tr>
                            <td class="esquerdo">
                                login:</td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoLogin" runat="server" EnableValidator="False" 
                                    Width="200px" MaxLength="50" />
                            </td>
                        </tr>

                        <tr>
                            <td class="esquerdo">
                                e-mail:</td>
                            <td class="direito">
                             
                                <cem:cEmail ID="cEmail1" runat="server" />
                            </td>
                        </tr>
                         <asp:Panel ID="pSenha" runat="server">
                        <tr>
                            <td class="esquerdo">
                                senha:</td>
                            <td class="direito">
                                    <asp:TextBox ID="txtSenha" runat="server" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                        ControlToValidate="txtSenha" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
                                        &nbsp;
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                        ControlToCompare="txtConfirmarSenha" ControlToValidate="txtSenha" 
                                        ErrorMessage="senhas não coincidem" ForeColor="Red"></asp:CompareValidator>
                            </td>
                        </tr>
                       
                         <tr>
                            <td class="esquerdo">
                                confirme a senha:</td>
                            <td class="direito">
                                <asp:TextBox ID="txtConfirmarSenha" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                    ControlToValidate="txtConfirmarSenha" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        </asp:Panel>
                       
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <div id="dGravacao1" runat="server">
                            <th colspan="2">
                                
                                <asp:Button ID="btInserir0" runat="server" onclick="btInserir_Click" 
                                    Text="inserir" />
                            </th>
                            </div>
                        </tr>
                    </table>
            
            </ContentTemplate>
        </asp:UpdatePanel>
        <ajaxToolkit:UpdatePanelAnimationExtender ID="upae" BehaviorID="animation" runat="server" TargetControlID="UpdatePanel1" Enabled="True">
        <Animations>
            <OnUpdating>
                <Parallel duration="0">
                    <FadeOut minimumOpacity=".5" />
                    <ScriptAction Script="waitingDialog({});" />  
                 </Parallel>
            </OnUpdating>
            <OnUpdated>
                <Parallel duration="0">
                   <FadeIn minimumOpacity=".5" />  
                    <ScriptAction Script="closeWaitingDialog()" /> 
                </Parallel> 
            </OnUpdated>
        </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>
    </div>
</asp:Content>
