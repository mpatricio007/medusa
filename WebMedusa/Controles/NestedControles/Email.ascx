<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Email.ascx.cs" Inherits="Medusa.Controles.Email" %>


<asp:TextBox ID="txt" runat="server" MaxLength="100" Width="350px"></asp:TextBox>
<asp:RequiredFieldValidator ID="rfv" runat="server" ControlToValidate="txt" 
    EnableTheming="False" ErrorMessage="e-mail obrigatório" ForeColor="Red"></asp:RequiredFieldValidator>
<asp:RegularExpressionValidator ID="rex" runat="server" ControlToValidate="txt" ForeColor="Red"
    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>

