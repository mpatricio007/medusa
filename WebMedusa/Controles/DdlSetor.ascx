<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DdlSetor.ascx.cs" Inherits="Medusa.Controles.DdlSetor" %>
<asp:DropDownList ID="lista" runat="server" DataTextField="nome" 
    DataValueField="id_setor" AppendDataBoundItems="True">
</asp:DropDownList>


<asp:CompareValidator ID="cvSetor" runat="server" 
    ErrorMessage="selecione um setor..." ForeColor="Red" Operator="NotEqual" 
    ValueToCompare="0" ControlToValidate="lista"></asp:CompareValidator>



