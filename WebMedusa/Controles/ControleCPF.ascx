<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControleCPF.ascx.cs" Inherits="Medusa.Controles.ControleCPF" %>



 <asp:TextBox ID="txtCpf" runat="server" CssClass="cpf"
    MaxLength="14" ontextchanged="txtCpf_TextChanged"></asp:TextBox>
  

    <asp:CustomValidator ID="cvCpf" runat="server" 
        ClientValidationFunction="valida_cpf" ControlToValidate="txtCpf" 
        Display="Dynamic" ValidateEmptyText="true"
        EnableTheming="False" ErrorMessage="cpf inválido" ForeColor="Red" 
    onservervalidate="cvCpf_ServerValidate"></asp:CustomValidator>

    <asp:RequiredFieldValidator ID="rfv" runat="server" 
    ControlToValidate="txtCpf" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>


   