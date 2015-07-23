<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/SiteBase.master" AutoEventWireup="true" CodeBehind="EsqueceuSenha.aspx.cs" Inherits="Medusa.EsqueceuSenha" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadBaseMaster" runat="server">  
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContentBaseMaster" runat="server">
    <div>
        <h2>
            ESQUECEU SUA SENHA</h2>
        <p>
            Entre com o seu cpf e clique em enviar. Uma nova senha será gerada e 
            enviada para o email cadastrado.</p>
        <div>
            <fieldset>
                <legend>Recuperar Senha</legend>
                <div class="editor-label">
                    <asp:Label ID="lbLogin" runat="server" Text="CPF"></asp:Label>
                </div>
                <div class="editor-field">
                    <cCpf:cCPF ID="cCPF1" runat="server" />
                </div>
                <br />
                <div class="editor-label">
                    <asp:Button ID="btSendSenha" runat="server" onclick="btSendSenha_Click" 
                        Text="enviar" />
                    <asp:Label ID="lbLog" runat="server" ForeColor="Red"></asp:Label>
                </div>
            </fieldset>
        </div>
    </div>
</asp:Content>
