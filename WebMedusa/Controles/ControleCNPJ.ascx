<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControleCNPJ.ascx.cs" Inherits="Medusa.Controles.ControleCNPJ" %> 


   <asp:TextBox ID="txtCnpj" runat="server" CssClass="cnpj" MaxLength="18" 
    ontextchanged="txtCnpj_TextChanged"></asp:TextBox>
    <asp:CustomValidator ID="cvCnpj" runat="server" 
        ClientValidationFunction="valida_cnpj" ControlToValidate="txtCnpj" 
        Display="Dynamic" 
        EnableTheming="False" ErrorMessage="cnpj inválido" ForeColor="Red" 
    onservervalidate="cvCnpj_ServerValidate" ></asp:CustomValidator>
<asp:RequiredFieldValidator ID="rfv" runat="server" 
    ControlToValidate="txtCnpj" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>