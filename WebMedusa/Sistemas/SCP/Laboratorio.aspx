﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.Master" AutoEventWireup="true" CodeBehind="Laboratorio.aspx.cs" Inherits="Medusa.Sistemas.SCP.Laboratorio" %>
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
                    <div id="updateProgressDiv" style="display:none; position:absolute;">
                        <div style=" margin-left:780px;  float:left">
                        <img src="../../Styles/img/loading.gif" />
                        <span style="margin:3px">Carregando ...</span></div>
                    </div>
                    <asp:Label ID="Label1" runat="server" Text="procurar"></asp:Label>
                    &nbsp;<asp:DropDownList ID="ddlOptions" runat="server" CausesValidation="True" 
                        AutoPostBack="True" onselectedindexchanged="ddlOptions_SelectedIndexChanged">
                        <asp:ListItem Value="nome">nome</asp:ListItem>
                        <asp:ListItem>sigla</asp:ListItem>
                        <asp:ListItem Value="Departamento.nome">departamento</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtProcura" runat="server" Width="137px"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlMode" runat="server">
                    </asp:DropDownList>
                    <asp:Label ID="Label2" runat="server" Text="mostrar"></asp:Label>
                    &nbsp;
                    <asp:DropDownList ID="ddlSize" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlSize_SelectedIndexChanged">
                        <asp:ListItem Selected="True">10</asp:ListItem>
                        <asp:ListItem>20</asp:ListItem>
                        <asp:ListItem>50</asp:ListItem>
                        <asp:ListItem Value="0">todos</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="btSearch" runat="server" CausesValidation="False" 
                        onclick="btProcurar_Click" Text="procurar" />
                    <asp:Button ID="btCriar" runat="server" CausesValidation="False" 
                        onclick="btCriar_Click"  Text="novo" Width="80px" />
                </div>
                <asp:Panel ID="panelGrid" runat="server">
                          
                            
                    <asp:GridView ID="grid" runat="server" AllowPaging="True" AllowSorting="True" 
                        AutoGenerateColumns="False" Caption="Lista de Laboratórios" 
                        CssClass="tableView" 
                        onpageindexchanging="grid_PageIndexChanging" onrowcreated="grid_RowCreated" 
                        onrowediting="grid_RowEditing" onsorting="grid_Sorting" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="nome" HeaderText="nome" 
                                SortExpression="nome">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="sigla" HeaderText="sigla" SortExpression="sigla" >
                            <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="departamento" SortExpression="Departamento.nome">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" 
                                        Text='<%# Bind("departamento.nome") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("departamento.nome") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ender" HeaderText="endereço" />
                            <asp:CommandField EditText="selecionar" ShowEditButton="True">
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:CommandField>
                        </Columns>
                        <HeaderStyle HorizontalAlign="Left" />
                        <SortedAscendingCellStyle 
                            CssClass="SortedAscendingCellStyle" />
                        <SortedAscendingHeaderStyle 
                            CssClass="SortedAscendingHeaderStyle" />
                        <SortedDescendingCellStyle 
                            CssClass="SortedDescendingCellStyle" />
                        <SortedDescendingHeaderStyle 
                            CssClass="SortedDescendingHeaderStyle " />
                    </asp:GridView>
                </asp:Panel>
                <asp:Panel ID="panelCadastro" runat="server">
                    <table class="cadastro">
                        <tr>
                            <th colspan="1">
                                cadastro de laboratórios</th>
                            <th colspan="1">
                            <div id="dGravacao" runat="server">
                                <asp:Button ID="btInserir" runat="server" onclick="btInserir_Click" 
                                    Text="inserir" />
                                <asp:Button ID="btAlterar" runat="server" onclick="btAlterar_Click" 
                                    Text="salvar" />
                                <asp:Button ID="btExcluir" runat="server" CausesValidation="False" 
                                    onclick="btExcluir_Click" Text="excluir" />
                                    <asp:Button ID="btCancelar" runat="server" CausesValidation="False" 
                                    onclick="btCancelar_Click" Text="cancelar" />
                                    </div>
                            </th>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                código:
                            </td>
                            <td class="direito">
                                <asp:TextBox ID="txtCodigo" runat="server" Width="43px" Enabled="False">0</asp:TextBox>
                            </td>
                        </tr>
                                       <tr>
                            <td class="esquerdo">
                                sigla:</td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoSigla" runat="server" EnableValidator="True" MaxLength="15" 
                                    Width="100px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                nome:
                            </td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoNome" runat="server" EnableValidator="True" 
                                    MaxLength="150" Width="500px" />
                            </td>
                        </tr>
         
                        <tr>
                            <td class="esquerdo">
                                unidade:</td>
                            <td class="direito">
                                <asp:DropDownList ID="listaUnidade" runat="server" DataTextField="nome" 
                    DataValueField="id_unidade" AppendDataBoundItems="True" Width="500px" AutoPostBack="True" 
                                    onselectedindexchanged="listaUnidade_SelectedIndexChanged" >
                </asp:DropDownList>


<asp:ListSearchExtender ID="listaUnidade_ListSearchExtender" runat="server" 
    Enabled="True" PromptCssClass="ListSearchExtenderPrompt" 
    PromptText="digite para procurar" QueryPattern="Contains" QueryTimeout="2000" 
    TargetControlID="listaUnidade">
</asp:ListSearchExtender>


<asp:CompareValidator ID="cvUnidade" runat="server" 
    ErrorMessage="selecione uma unidade..." ForeColor="Red" Operator="NotEqual" 
    ValueToCompare="0" ControlToValidate="listaUnidade"></asp:CompareValidator></tr>
                        <tr>
                            <td class="esquerdo">
                                departamento:</td>
                            <td class="direito">
                                <asp:DropDownList ID="listaDepto" runat="server" AppendDataBoundItems="True" 
                                    DataTextField="nome" DataValueField="id_departamento" 
                                  Width="500px">
                                </asp:DropDownList>
                                <asp:ListSearchExtender ID="listaDepto_ListSearchExtender" runat="server" 
                                    Enabled="True" PromptCssClass="ListSearchExtenderPrompt" 
                                    PromptText="digite para procurar" QueryPattern="Contains" QueryTimeout="2000" 
                                    TargetControlID="listaDepto">
                                </asp:ListSearchExtender>
                                <asp:CompareValidator ID="cv" runat="server" ControlToValidate="listaDepto" 
                                    ErrorMessage="selecione um departamento..." ForeColor="Red" Operator="NotEqual" 
                                    ValueToCompare="0"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <cender:cEnder ID="cEnder1" runat="server" />
                                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th colspan="2">
                            <div id="dGravacao1" runat="server">
                                <asp:Button ID="btInserir0" runat="server" onclick="btInserir_Click" 
                                     Text="inserir" />
                                <asp:Button ID="btAlterar0" runat="server" onclick="btAlterar_Click" 
                                    Text="salvar" />
                                <asp:Button ID="btExcluir0" runat="server" CausesValidation="False" 
                                    onclick="btExcluir_Click" Text="excluir" />
                                    <asp:Button ID="btCancelar0" runat="server" CausesValidation="False" 
                                    onclick="btCancelar_Click" Text="cancelar" />
                                    </div>
                            </th>
                        </tr>
                    </table>
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
