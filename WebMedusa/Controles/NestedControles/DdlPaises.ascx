<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DdlPaises.ascx.cs" Inherits="Medusa.Controles.DdlPaises" %>
                <asp:DropDownList ID="lista" runat="server" DataTextField="nome" 
                    DataValueField="nome" AppendDataBoundItems="True" 
    onselectedindexchanged="lista_SelectedIndexChanged">
                </asp:DropDownList>


<ajaxToolkit:ListSearchExtender ID="lista_ListSearchExtender" runat="server" 
    Enabled="True" PromptCssClass="ListSearchExtenderPrompt" 
    PromptText="digite para procurar" QueryPattern="Contains" QueryTimeout="2000" 
    TargetControlID="lista">
</ajaxToolkit:ListSearchExtender>


<asp:CompareValidator ID="cv" runat="server" 
    ErrorMessage="selecione um pais..." ForeColor="Red" Operator="NotEqual" 
    ValueToCompare="0" ControlToValidate="lista"></asp:CompareValidator>




