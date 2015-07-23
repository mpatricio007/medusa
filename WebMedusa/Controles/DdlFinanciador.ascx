<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DdlFinanciador.ascx.cs" Inherits="Medusa.Controles.DdlFinanciador" %>
                <%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
                
                <asp:DropDownList ID="lista" runat="server" DataTextField="strFinanciador" 
                    DataValueField="id_financiador" 
    AppendDataBoundItems="True" style="margin-bottom: 0px" Width="400px">
                </asp:DropDownList>


<asp:ListSearchExtender ID="lista_ListSearchExtender" runat="server" 
    Enabled="True" PromptCssClass="ListSearchExtenderPrompt" 
    PromptText="digite para procurar" QueryPattern="Contains" QueryTimeout="2000" 
    TargetControlID="lista">
</asp:ListSearchExtender>


<asp:CompareValidator ID="cv" runat="server" 
    ErrorMessage="selecione um financiador..." ForeColor="Red" Operator="NotEqual" 
    ValueToCompare="0" ControlToValidate="lista"></asp:CompareValidator>



