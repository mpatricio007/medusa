<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PesquisaConta.ascx.cs" Inherits="Medusa.Controles.Pesquisa.PesquisaConta" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

    <tr>
        <td class="esquerdo">
        projeto:</td>
        <td class="direito">
            <cddlProjeto:cDdlProjeto ID="cDdlProjeto1" runat="server" OnSelectedIndexChanged="cDdlProjeto1_SelectedIndexChanged" AutoPostBack="true" />
        </td>
    </tr>
    <tr>
        <td class="esquerdo">
            conta corrente:</td>
        <td class="direito">
            <asp:DropDownList ID="lista" runat="server" 
                DataTextField="StrContaDigito" DataValueField="Id_conta" 
                AppendDataBoundItems="True">
            </asp:DropDownList>
            <asp:ListSearchExtender ID="lista_ListSearchExtender" runat="server" 
                Enabled="True" PromptCssClass="ListSearchExtenderPrompt" 
                PromptText="digite para procurar" QueryPattern="Contains" QueryTimeout="2000" 
                TargetControlID="lista">
</asp:ListSearchExtender>
            <asp:CompareValidator ID="cv" runat="server" ControlToValidate="lista" 
                ErrorMessage="selecione uma conta..." ForeColor="Red" Operator="NotEqual" 
                ValueToCompare="0"></asp:CompareValidator>
        </td>
    </tr>
