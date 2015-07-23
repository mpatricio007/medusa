<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DdlLaboratorios.ascx.cs" Inherits="Medusa.Controles.DdlLaboratorios" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

    <%--<%@ Register src="../Sistemas/SCP/ControleLaboratorio.ascx" tagname="ControleLaboratorio" tagprefix="uc1" %>--%>
  

    <div>

<asp:DropDownList ID="lista" runat="server" AppendDataBoundItems="True" 
    DataTextField="nome" DataValueField="id_laboratorio" 
    onselectedindexchanged="lista_SelectedIndexChanged">
</asp:DropDownList>


<asp:ListSearchExtender ID="lista_ListSearchExtender" runat="server" 
    Enabled="True" PromptCssClass="ListSearchExtenderPrompt" 
    PromptText="digite para procurar" QueryPattern="Contains" QueryTimeout="2000" 
    TargetControlID="lista">
</asp:ListSearchExtender>
    
        
        &nbsp; </asp:ListSearchExtender>


<asp:CompareValidator ID="cv" runat="server" ControlToValidate="lista" 
    ErrorMessage="selecione um laboratório" ForeColor="Red" Operator="NotEqual" 
    ValueToCompare="0"></asp:CompareValidator>
     </div>


