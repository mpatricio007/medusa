<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.Master" AutoEventWireup="true" CodeBehind="RelatorioPesquisa.aspx.cs" Inherits="Medusa.Relatorios.Recepcao.RelatorioPesquisa" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="conteudo">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
            <asp:CheckBox ID="ckFilter" runat="server" AutoPostBack="True" 
                        oncheckedchanged="ckFilter_CheckedChanged" 
            Text="habilitar múltiplos filtros" />
                
                <asp:DataList ID="DataListFiltros" runat="server" RepeatColumns="6"
            ondatabinding="DataListFiltros_DataBinding" RepeatDirection="Horizontal" 
            OnDeleteCommand="DataListFiltros_DeleteCommand">
            <ItemTemplate>

               <div class="FilterName">

                <%# Eval("property_name") %>&nbsp
                <%# Eval("mode_name")%>&nbsp
                <%# Eval("value")%> &nbsp 
                   <asp:ImageButton ID="btExcluiFiltro" runat="server" 
                       ImageUrl="~/Styles/img/bt_delete.jpg" Width="15px" Height="15px" CommandName="delete"
                        />
                </div>
            </ItemTemplate>
        </asp:DataList>
                <div class="pesquisar">  
                     <asp:Label ID="Label1" runat="server" Text="procurar"></asp:Label>
                     &nbsp;<asp:DropDownList ID="ddlOptions" runat="server" CausesValidation="True" 
                        AutoPostBack="True" onselectedindexchanged="ddlOptions_SelectedIndexChanged">
                         <asp:ListItem>ano</asp:ListItem>
                        <asp:ListItem Value="nprotent">protocolo</asp:ListItem>
                        <asp:ListItem Value="dataprot">data</asp:ListItem>
                        <asp:ListItem Value="codproj">projeto</asp:ListItem>
                        <asp:ListItem Value="codproja">projeto A</asp:ListItem>
                        <asp:ListItem Value="tipodocent">tipo do documento</asp:ListItem>
                        <asp:ListItem Value="numdocent">numero</asp:ListItem>
                        <asp:ListItem Value="valorent">valor</asp:ListItem>
                         <asp:ListItem Value="enviadoent">enviado por</asp:ListItem>
                        <asp:ListItem Value="descrent">descrição</asp:ListItem>
                        <asp:ListItem Value="UsuarioPara.PessoaFisica.nome">encaminhado para</asp:ListItem>
                        <asp:ListItem Value="UsuarioEntrada.PessoaFisica.nome">usuario</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtProcura" runat="server" Width="137px"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlMode" runat="server">
                    </asp:DropDownList>
                     &nbsp; <asp:Button ID="btSearch" runat="server" CausesValidation="False" 
                        onclick="btProcurar_Click" Text="procurar" />
                </div>
                <asp:Panel ID="panelCadastro" runat="server">
                    <table class="cadastro">
                        <tr>
                            <th colspan="2">
                            </div>
                         </th>
                        </tr>
                    </table>
                    <div id="dRelatorio" runat="server"></div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <AjaxToolKit:UpdatePanelAnimationExtender ID="upae" BehaviorID="animation" runat="server" TargetControlID="UpdatePanel1" Enabled="True">
        <Animations>
            <OnUpdating>
                <Parallel duration="0">
                    <FadeOut minimumOpacity=".5" />
                    <ScriptAction Script="onUpdating();" />  
                 </Parallel>
            </OnUpdating>
            <OnUpdated>
                <Parallel duration="0">
                   <FadeIn minimumOpacity=".5" />  
                    <ScriptAction Script="onUpdated();" /> 
                </Parallel> 
            </OnUpdated>
        </Animations>
        </AjaxToolKit:UpdatePanelAnimationExtender>
</div>
</asp:Content>
