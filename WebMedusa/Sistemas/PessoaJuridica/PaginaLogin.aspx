<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/SiteBase.Master" AutoEventWireup="true" CodeBehind="PaginaLogin.aspx.cs" Inherits="Medusa.Sistemas.PessoaJuridica.PaginaLogin" %>
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
                <asp:Panel ID="panelCadastro" runat="server">
                <fieldset><legend>Login</legend>
                <p>
                    Login:<br /><ctx:cTexto ID="cTextoLogin" runat="server" MaxLength="50" 
                         />
                </p>
                 
                <p>
                                Senha:<br /> <ctx:cTexto ID="cTextoSenha" runat="server" MaxLength="50" 
                                    TextMode="Password" />

                </p>
                <p>
                 <asp:Button ID="btEntrar" runat="server" onclick="btEntrar_Click" Text="Entrar" />
                                &nbsp;<asp:Label ID="lblMsg" runat="server" />
                </p>
                <p>
                    <asp:LinkButton ID="lkEsqueceuSenha" runat="server" CausesValidation="False" 
                        PostBackUrl="~/EsqueceuSenha.aspx">Esqueceu sua senha?</asp:LinkButton>
                </p>
                    <p>
                        <asp:LinkButton ID="lkNew" runat="server" 
                            PostBackUrl="~/Sistemas/Admin/UsuariosExternos.aspx" 
                            CausesValidation="False">Não possui cadastro? Cadastre-se!</asp:LinkButton>
                    </p>
                </fieldset> 
              
                        
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
