<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Data.ascx.cs" Inherits="Medusa.Controles.Data" %>

 
       
  
<asp:TextBox ID="txtData" runat="server" Width="80px" 
    CssClass="date"
    ></asp:TextBox>

<asp:RequiredFieldValidator ID="rfvData" runat="server" 
    ControlToValidate="txtData" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>


