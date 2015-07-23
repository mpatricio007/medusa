<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DdlUnidades.ascx.cs" Inherits="Medusa.Controles.DdlUnidades" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

    <div>

<asp:DropDownList ID="lista" runat="server" DataTextField="StrUnidade" 
   DataValueField="id_unidade" AppendDataBoundItems="True" Width="500px" >
</asp:DropDownList>


<asp:ListSearchExtender ID="lista_ListSearchExtender" runat="server" 
    Enabled="True" PromptCssClass="ListSearchExtenderPrompt" 
    PromptText="digite para procurar" QueryPattern="Contains" QueryTimeout="2000" 
    TargetControlID="lista">
</asp:ListSearchExtender>


<asp:CompareValidator ID="cv" runat="server" 
    ErrorMessage="selecione uma unidade..." ForeColor="Red" Operator="NotEqual" 
    ValueToCompare="0" ControlToValidate="lista"></asp:CompareValidator>
    </div>



