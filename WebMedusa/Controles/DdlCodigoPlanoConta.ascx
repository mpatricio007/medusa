<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DdlCodigoPlanoConta.ascx.cs" Inherits="Medusa.Controles.DdlCodigoPlanoConta" %>
                <%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
                <asp:DropDownList ID="lista" runat="server" DataTextField="codigo" 
                    DataValueField="id_plano_conta" 
    AppendDataBoundItems="True" onselectedindexchanged="lista_SelectedIndexChanged">
                </asp:DropDownList>


<asp:ListSearchExtender ID="lista_ListSearchExtender" runat="server" 
    Enabled="True" PromptCssClass="ListSearchExtenderPrompt" 
    PromptText="digite para procurar" QueryPattern="Contains" QueryTimeout="2000" 
    TargetControlID="lista">
</asp:ListSearchExtender>


<asp:CompareValidator ID="cv" runat="server" 
    ErrorMessage="selecione um código de plano de conta..." ForeColor="Red" Operator="NotEqual" 
    ValueToCompare="0" ControlToValidate="lista"></asp:CompareValidator>



