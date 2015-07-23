<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Endereco.ascx.cs" Inherits="Medusa.Controles.Endereco" %>
                <%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
              
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<table class="cadastro">
<tr>
<th colspan="2">
    <asp:Label ID="lblTitulo" runat="server" Text="endereço"></asp:Label>
    </th>
</tr>
<tr>
        <td class="esquerdo">
            país:
        </td>
        <td class="direito">
          
            <asp:DropDownList ID="listaPais" runat="server" 
                DataTextField="nome" DataValueField="nome" 
                onselectedindexchanged="listaPais_SelectedIndexChanged" 
                AutoPostBack="True">
            </asp:DropDownList>
            <ajaxToolkit:ListSearchExtender ID="listaPais_ListSearchExtender" 
                runat="server" Enabled="True" PromptCssClass="ListSearchExtenderPrompt" 
                PromptText="digite para procurar" QueryPattern="StartsWith" QueryTimeout="2000" 
                TargetControlID="listaPais">
            </ajaxToolkit:ListSearchExtender>
            &nbsp;<asp:CompareValidator ID="cv" runat="server" ControlToValidate="listaPais" 
                ErrorMessage="selecione um pais..." ForeColor="Red" Operator="NotEqual" 
                ValueToCompare="0"></asp:CompareValidator>
          
        </td>
</tr>
    <tr>
        <td class="esquerdo">
            logradouro:
        </td>
        <td class="direito">
           
            <asp:TextBox  ID="cTextoLogradouro" runat="server" EnableValidator="True" 
                MaxLength="50" Width="400px" />
            <asp:RequiredFieldValidator  ID="rfvLogradouro" runat="server" 
                ErrorMessage="logradouro obrigatório" 
                ControlToValidate="cTextoLogradouro" EnableTheming="False" 
                ForeColor="Red" ></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="esquerdo">
            número:
        </td>
        <td class="direito">
            <asp:TextBox  ID="cTextoNumero" runat="server" MaxLength="10" />
            <asp:RequiredFieldValidator  ID="rfvNumero" runat="server" 
                ErrorMessage="número obrigatório" ControlToValidate="cTextoNumero" 
                EnableTheming="False" ForeColor="Red" ></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="esquerdo">
            complemento</td>
        <td class="direito">
            <asp:TextBox  ID="cTextoComplemento" runat="server" MaxLength="50" 
                Width="400px" />
        </td>
    </tr>
    <tr>
        <td class="esquerdo">
            bairro:</td>
        <td class="direito">
            <asp:TextBox  ID="cTextoBairro" runat="server" MaxLength="30" Width="400px" />
            <asp:RequiredFieldValidator  ID="rfvBairro" runat="server" 
                ErrorMessage="bairro obrigatório" ControlToValidate="cTextoBairro" 
                EnableTheming="False" ForeColor="Red" ></asp:RequiredFieldValidator>
        </td>
    </tr>
 
    <tr>
        <td class="esquerdo">
            uf</td>
        <td class="direito">
         <asp:UpdatePanel ID="upUF" runat="server">
         <ContentTemplate>
            <asp:DropDownList ID="listaUF" runat="server" AppendDataBoundItems="True" 
                AutoPostBack="True" DataTextField="uf" DataValueField="uf" 
                onselectedindexchanged="listaUF_SelectedIndexChanged" Width="50px">
            </asp:DropDownList>
    
            <asp:CompareValidator ID="cvUF" runat="server" ControlToValidate="listaUF" 
                ErrorMessage="selecione um uf..." ForeColor="Red" Operator="NotEqual" 
                ValueToCompare="0" EnableTheming="False"></asp:CompareValidator>
             <ctx:cTexto ID="cTextoUF" runat="server" Visible="False" />
           </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
       <tr>
        <td class="esquerdo">
            cidade</td>
  
        <td class="direito">
         <asp:UpdatePanel ID="upCidade" runat="server">
         <ContentTemplate>
            <asp:DropDownList ID="listaCidade" runat="server" AppendDataBoundItems="True" 
                DataTextField="cidade" DataValueField="cidade" Width="300px">
            </asp:DropDownList>
            <asp:ListSearchExtender ID="listaCidade_ListSearchExtender" runat="server" 
                Enabled="True" PromptCssClass="ListSearchExtenderPrompt" 
                PromptText="digite para procurar" QueryPattern="StartsWith" QueryTimeout="2000" 
                TargetControlID="listaCidade">
            </asp:ListSearchExtender>
            <asp:CompareValidator ID="cvCidade" runat="server" ControlToValidate="listaCidade" 
                ErrorMessage="selecione uma cidade..." ForeColor="Red" Operator="NotEqual" 
                ValueToCompare="0" EnableTheming="False"></asp:CompareValidator>
                
             <ctx:cTexto ID="cTextoCidade" runat="server" Visible="false" Height="20" 
                 Width="300px" />
                </ContentTemplate>
                </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td class="esquerdo">
            cep</td><td class="direito">
            <asp:TextBox  ID="cTextoCep" runat="server" Width="80px" MaxLength="20"/>
            <asp:RequiredFieldValidator  ID="rfvCep" runat="server" 
                ErrorMessage="cep obrigatório" ControlToValidate="cTextoCep" 
                EnableTheming="False" ForeColor="Red" ></asp:RequiredFieldValidator>
        </td>
    </tr>
    </table>

              

</ContentTemplate>
</asp:UpdatePanel>