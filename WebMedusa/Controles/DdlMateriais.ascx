<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DdlMateriais.ascx.cs" Inherits="Medusa.Controles.DdlMateriais" %>
                <%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
                material:
                <asp:DropDownList ID="ddlMaterial" runat="server" DataTextField="descricao" 
                    DataValueField="id_material" AppendDataBoundItems="True">
                </asp:DropDownList>


<asp:ListSearchExtender ID="ddlMaterial_ListSearchExtender" runat="server" 
    Enabled="True" PromptCssClass="ListSearchExtenderPrompt" 
    PromptText="digite para procurar" QueryPattern="Contains" QueryTimeout="2000" 
    TargetControlID="ddlMaterial">
</asp:ListSearchExtender>


<asp:CompareValidator ID="cv" runat="server" 
    ErrorMessage="selecione um material..." ForeColor="Red" Operator="NotEqual" 
    ValueToCompare="0" ControlToValidate="lista"></asp:CompareValidator>



quantidade:
<asp:TextBox ID="txtQuantidade" runat="server"></asp:TextBox>





