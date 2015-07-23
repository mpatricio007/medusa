<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Telefone.ascx.cs" Inherits="Medusa.Controles.Telefone" %>


<asp:TextBox ID="txt" runat="server" MaxLength="20" Width="150px" CssClass="telefone"></asp:TextBox>
&nbsp;ramal
<asp:TextBox ID="txtRamal" runat="server" MaxLength="10"></asp:TextBox>
&nbsp;<asp:DropDownList ID="ddlTipo" runat="server">
</asp:DropDownList>
<asp:RequiredFieldValidator ID="rfv" runat="server" ControlToValidate="txt" 
    ErrorMessage="telefone obrigatório" EnableTheming="False" ForeColor="Red"></asp:RequiredFieldValidator>






