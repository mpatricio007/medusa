<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Texto.ascx.cs" Inherits="Medusa.Controles.Texto" %>
<asp:TextBox ID="txt" runat="server" ontextchanged="txt_TextChanged" ></asp:TextBox>
<asp:RequiredFieldValidator  ID="rfv" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="txt" ForeColor="Red" > </asp:RequiredFieldValidator>