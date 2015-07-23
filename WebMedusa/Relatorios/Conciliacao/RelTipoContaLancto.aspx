<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.Master" AutoEventWireup="true" CodeBehind="RelTipoContaLancto.aspx.cs" Inherits="Medusa.Relatorios.Conciliacao.RelTipoContaLancto" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="conteudo">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="pesquisar">                   
                    <div id="updateProgressDiv" style="display:none; position:absolute;">
                        <div style=" margin-left:780px;  float:left">
                        <img src="../../Styles/img/loading.gif" alt="carregando" />
                        <span style="margin:3px">Carregando ...</span></div>
                    </div>
                    &nbsp;<asp:Label ID="lblTitulo" runat="server" 
                        Text="relatório de lançamentos no conta corrente"></asp:Label>
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                </div>
                <asp:Panel ID="panelCadastro" runat="server">

                    <table class="cadastro">
        <tr>
            <th colspan="1">
                selecionar filtro</th>
            <th colspan="1">
           </th>
        </tr>
        <tr>
            <td class="esquerdo">
                intervalo de datas:
            </td>
            <td class="direito">
                <cdt:cData ID="cData1" runat="server" />
                &nbsp;à
                <cdt:cData ID="cData2" runat="server" />
            </td>
        </tr>
                        <tr>
                            <td class="esquerdo">
                                escolher tipos de lançamentos:</td>
                            <td class="direito">
                                <asp:DropDownList ID="listaLcto" runat="server" AppendDataBoundItems="True" 
                                    DataTextField="StrTipoLcto" DataValueField="id_tipo_lcto">
                                </asp:DropDownList>
                                <asp:ListSearchExtender ID="listaLcto_ListSearchExtender" runat="server" 
                                    Enabled="True" PromptCssClass="ListSearchExtenderPrompt" 
                                    PromptText="digite para procurar" QueryPattern="Contains" QueryTimeout="2000" 
                                    TargetControlID="listaLcto">
                                </asp:ListSearchExtender>
                                <asp:Button ID="btSelecionar" runat="server" Text="selecionar" 
                                    onclick="btSelecionar_Click" CausesValidation="False" 
                                    ValidationGroup="tipolct" />


                                <asp:Button ID="btLimpar" runat="server" onclick="btLimpar_Click" 
                                    Text="limpar lista" />
                                <asp:CompareValidator ID="cv" runat="server" ControlToValidate="listaLcto" 
                                    ErrorMessage="selecione um tipo de lançamento..." ForeColor="Red" 
                                    Operator="NotEqual" ValidationGroup="tipolct" ValueToCompare="0"></asp:CompareValidator>


                                <asp:DataList ID="dlTipoLancto" runat="server" RepeatColumns="6"
                                ondatabinding="dlTipoLancto_DataBinding" RepeatDirection="Horizontal" 
                                OnDeleteCommand="dlTipoLancto_DeleteCommand">
                            <ItemTemplate>


                               <div class="FilterName">

                                <%# Eval("descricao") %>&nbsp
                                   <asp:ImageButton ID="btExcluiFiltro" runat="server" 
                                       ImageUrl="~/Styles/img/bt_delete.jpg" Width="15px" Height="15px" 
                                       CommandName="delete" CausesValidation="False"
                                        />
                                </div>
                             </ItemTemplate>
                            </asp:DataList>
                            </td>
                        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                
            </td>
        </tr>
        <tr>
            <th colspan="2">
                <asp:Button ID="btGerar" runat="server" onclick="btImportar_Click" 
                    Text="gerar relatório" />
                &nbsp;
            </th>
        </tr>
    </table>
                <div id="dRelatorio" runat="server"></div>
              </asp:Panel>
         </ContentTemplate>
        </asp:UpdatePanel>
          
           
        
        <asp:UpdatePanelAnimationExtender ID="upae" BehaviorID="animation" runat="server" TargetControlID="UpdatePanel1" Enabled="True">
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
        </asp:UpdatePanelAnimationExtender>
</div>
</asp:Content>
