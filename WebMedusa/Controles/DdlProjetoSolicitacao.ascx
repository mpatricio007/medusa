<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DdlProjetoSolicitacao.ascx.cs" Inherits="Medusa.Controles.DdlProjetoSolicitacao" %>
                <%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
                <asp:DropDownList ID="lista" runat="server" 
                    DataValueField="id_sol_proj" AppendDataBoundItems="True" 
                   DataTextField="strSolicitacao" Width="500px"  >
                </asp:DropDownList>


<asp:ListSearchExtender ID="lista_ListSearchExtender" runat="server" 
    Enabled="True" PromptCssClass="ListSearchExtenderPrompt" 
    PromptText="digite para procurar" QueryPattern="Contains" QueryTimeout="2000" 
    TargetControlID="lista">
</asp:ListSearchExtender>


<asp:CompareValidator ID="cv" runat="server" 
    ErrorMessage="selecione uma solicitação de projeto..." ForeColor="Red" Operator="NotEqual" 
    ValueToCompare="0" ControlToValidate="lista"></asp:CompareValidator>



