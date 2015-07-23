<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DdlUsuarioFUSP.ascx.cs" Inherits="Medusa.Controles.DdlUsuarioFUSP" %>
<asp:DropDownList ID="lista" runat="server" DataTextField="nome" 
    DataValueField="id_usuario" AppendDataBoundItems="True">
</asp:DropDownList>


<asp:CompareValidator ID="cvUsuario" runat="server" 
    ErrorMessage="selecione um usuario..." ForeColor="Red" Operator="NotEqual" 
    ValueToCompare="0" ControlToValidate="lista"></asp:CompareValidator>



