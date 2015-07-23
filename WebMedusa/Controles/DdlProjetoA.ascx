<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DdlProjetoA.ascx.cs" Inherits="Medusa.Controles.DdlProjetoA" %>
<asp:DropDownList ID="lista" runat="server" AppendDataBoundItems="True" 
    DataTextField="codigo" DataValueField="id_projetoa" Width="100px">
</asp:DropDownList>

<ajaxToolkit:ListSearchExtender ID="lista_ListSearchExtender" runat="server" 
    Enabled="True" PromptCssClass="ListSearchExtenderPrompt" 
    PromptText="digite para procurar" QueryPattern="Contains" QueryTimeout="2000" 
    TargetControlID="lista">
</ajaxToolkit:ListSearchExtender>

<asp:CompareValidator ID="cv" runat="server" 
    ErrorMessage="selecione um projeto A..." ForeColor="Red" Operator="NotEqual" 
    ValueToCompare="0" ControlToValidate="lista">
</asp:CompareValidator>

