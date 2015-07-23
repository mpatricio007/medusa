<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DdlTaxaProjeto.ascx.cs" Inherits="Medusa.Controles.DdlTaxaProjeto" %>
                <%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
                <asp:DropDownList ID="lista" runat="server" DataTextField="strTaxaProjeto" 
                    DataValueField="id_taxa" AppendDataBoundItems="True" 
    ValidationGroup="taxaProjeto">
                </asp:DropDownList>


<asp:ListSearchExtender ID="lista_ListSearchExtender" runat="server" 
    Enabled="True" PromptCssClass="ListSearchExtenderPrompt" 
    PromptText="digite para procurar" QueryPattern="Contains" QueryTimeout="2000" 
    TargetControlID="lista">
</asp:ListSearchExtender>


<asp:CompareValidator ID="cv" runat="server" 
    ErrorMessage="selecione uma taxa para o projeto..." ForeColor="Red" Operator="NotEqual" 
    ValueToCompare="0" ControlToValidate="lista"></asp:CompareValidator>



