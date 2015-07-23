<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DdlDepartamento.ascx.cs" Inherits="Medusa.Controles.DdlDepartamento" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
               
   
               
  <div>

<asp:DropDownList ID="lista" runat="server" DataTextField="nome" 
    DataValueField="id_departamento" 
    AppendDataBoundItems="True" 
    onselectedindexchanged="lista_SelectedIndexChanged" Width="500px">
</asp:DropDownList>


<asp:ListSearchExtender ID="lista_ListSearchExtender" runat="server" 
    Enabled="True" PromptCssClass="ListSearchExtenderPrompt" 
    PromptText="digite para procurar" QueryPattern="Contains" QueryTimeout="2000" 
    TargetControlID="lista">
</asp:ListSearchExtender>


<asp:CompareValidator ID="cv" runat="server" 
    ErrorMessage="selecione um departamento..." ForeColor="Red" Operator="NotEqual" 
    ValueToCompare="0" ControlToValidate="lista"></asp:CompareValidator>
     </div>
