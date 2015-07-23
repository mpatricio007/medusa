<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.Master" AutoEventWireup="true"
    CodeBehind="LotePagtos.aspx.cs" Inherits="Medusa.Sistemas.RemessaBancaria.LotePagtos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style1 {
            height: 8px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="conteudo">
        &nbsp;<script type="text/javascript">
                  function pageLoad() {
                      $(function () {
                          $('.date').datepicker({
                              showOtherMonths: true,
                              changeMonth: true,
                              changeYear: true
                          });
                          $('.date').dateEntry({ spinnerImage: '' });
                          $('.cep').mask("99999-999");
                          $(".cpf").mask("999.999.999-99");
                          $(".telefone").mask("(99)99999999?9");
                          $(".cnpj").mask("99.999.999/9999-99");
                          $(".valor").focusout(function () {
                              __doPostBack($(".valor").attr('name'), '');
                          });
                          $(".valor").maskMoney({ showSymbol: false, decimal: ",", thousands: "." });
                          // create the loading window and set autoOpen to false

                      });
                  }

                  function openRejeicaoDialog() {
                      $("#dRejeitado").dialog({
                          width: 500,
                          position: 'middle',
                          modal: true,
                          resizable: false,
                          draggable: false,
                          title: 'Atenção'
                      }).parent().appendTo(jQuery("form:first"));
                      $("#dRejeitado").dialog("open");
                  }

                  function closeRejeicaoDialog() {
                      $("#dRejeitado").dialog("close");
                  }
        </script><asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="../../Scripts/FixFocus.js" />
            </Scripts>
        </asp:ScriptManager>
        <div id="dRejeitado" style="display: none" title="Rejeitados">
            <asp:UpdatePanel ID="upRejeitado" runat="server">
                <ContentTemplate>
                    <table class="cadastro">
                        <tr>
                            <td class="esquerdo">
                                deseja realmente rejeitar este pagamento?:
                            </td>
                            <td class="direito">
                                <asp:RadioButtonList ID="rbRejeicao" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                                    OnSelectedIndexChanged="rbRejeicao_SelectedIndexChanged">
                                    <asp:ListItem Value="True">sim</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="False">não</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr id="trMotivo" runat="server">
                            <td class="esquerdo">
                                motivo:
                            </td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoMotivo" runat="server" TextMode="MultiLine" Width="350px" Height="200px"
                                    ValidationGroup="rejeitado" EnableValidator="true" />
                            </td>
                        </tr>
                        <tr>
                            <th colspan="2">
                                <asp:Button ID="btRejeitar" runat="server" Text="ok" OnClick="btRejeitar_Click" ValidationGroup="rejeitado" />
                            </th>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:CheckBox ID="ckFilter" runat="server" AutoPostBack="True" OnCheckedChanged="ckFilter_CheckedChanged"
                    Text="habilitar múltiplos filtros" />
                <asp:DataList ID="DataListFiltros" runat="server" RepeatColumns="6" OnDataBinding="DataListFiltros_DataBinding"
                    RepeatDirection="Horizontal" OnDeleteCommand="DataListFiltros_DeleteCommand">
                    <ItemTemplate>
                        <div class="FilterName">
                            <%# Eval("property_name") %>&nbsp
                            <%# Eval("mode_name")%>&nbsp
                            <%# Eval("value")%>
                            &nbsp
                            <asp:ImageButton ID="btExcluiFiltro" runat="server" ImageUrl="~/Styles/img/bt_delete.jpg"
                                Width="15px" Height="15px" CommandName="delete" />
                        </div>
                    </ItemTemplate>
                </asp:DataList>
                <div class="pesquisar">
                    <div id="updateProgressDiv" style="display: none; position: absolute;">
                        <div style="margin-left: 780px; float: left">
                            <img src="../../Styles/img/loading.gif" alt="carregando..." />
                            <span style="margin: 3px">Carregando ...</span></div>
                    </div>
                    <asp:Label ID="Label1" runat="server" Text="procurar"></asp:Label>
                    <asp:DropDownList ID="ddlOptions" runat="server" CausesValidation="True" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlOptions_SelectedIndexChanged">
                        <asp:ListItem Value="id_lote" Selected="True">código</asp:ListItem>
                        <asp:ListItem Value="data_pgto">data de pagamento</asp:ListItem>
                        <asp:ListItem Value="data_envio">data de envio</asp:ListItem>
                        <asp:ListItem Value="Conta.numero">conta</asp:ListItem>
                        <asp:ListItem Value="data_processamento">data de processamento</asp:ListItem>
                        <asp:ListItem>descrição</asp:ListItem>
                        <asp:ListItem Value="Conta.Projeto.codigo">projeto</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtProcura" runat="server" Width="137px"></asp:TextBox>
                    <asp:DropDownList ID="ddlMode" runat="server">
                    </asp:DropDownList>
                    <asp:Label ID="Label2" runat="server" Text="mostrar"></asp:Label>
                    <asp:DropDownList ID="ddlSize" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSize_SelectedIndexChanged">
                        <asp:ListItem Selected="True">10</asp:ListItem>
                        <asp:ListItem>20</asp:ListItem>
                        <asp:ListItem>50</asp:ListItem>
                        <asp:ListItem Value="0">todos</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="btSearch" runat="server" CausesValidation="False" OnClick="btProcurar_Click"
                        Text="procurar" />
                    <asp:Button ID="btCriar" runat="server" CausesValidation="False" OnClick="btCriar_Click"
                        Text="novo" Width="80px" />
                </div>
                <asp:Panel ID="panelGrid" runat="server">
                    <asp:GridView ID="grid" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                        Caption="Lista de Lotes de Pagamentos" CellPadding="4" CssClass="tableView" ForeColor="#333333"
                        GridLines="None" OnPageIndexChanging="grid_PageIndexChanging" OnRowCreated="grid_RowCreated"
                        OnRowEditing="grid_RowEditing" OnSorting="grid_Sorting" Width="100%">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:BoundField DataField="id_lote" HeaderText="código" SortExpression="id_lote">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="data_pgto" HeaderText="data de pagamento" SortExpression="data_pgto"
                                DataFormatString="{0:d}">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="data_envio" HeaderText="data de envio" SortExpression="data_envio" />
                            <asp:BoundField DataField="data_processamento" HeaderText="data de processamento"
                                SortExpression="data_processamento" />
                            <asp:BoundField DataField="descricao" HeaderText="descrição" SortExpression="descricao" />
                            <asp:TemplateField HeaderText="projeto" SortExpression="Conta.Projeto.codigo">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Conta.Projeto.codigo") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Conta.Projeto.codigo") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="conta" SortExpression="Conta.numero">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("Conta.StrContaDigito") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField EditText="selecionar" ShowEditButton="True">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:CommandField>
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" CssClass="SortedAscendingCellStyle" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" CssClass="SortedAscendingHeaderStyle" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" CssClass="SortedDescendingCellStyle" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" CssClass="SortedDescendingHeaderStyle " />
                    </asp:GridView>
                </asp:Panel>
                <asp:Panel ID="panelCadastro" runat="server">
                    <ajaxToolkit:TabContainer ID="tabs" runat="server" ActiveTabIndex="1" AutoPostBack="true"
                        OnActiveTabChanged="tabs_OnActiveTabChanged" Width="100%">
                        <asp:TabPanel ID="tbCadastro" runat="server" HeaderText="cadastro">
                            <ContentTemplate>
                                <table class="cadastro">
                                    <tr>
                                        <th colspan="1">
                                            cadastro de lotes de pagamentos
                                        </th>
                                        <th colspan="1">
                                            <div id="dGravacao" runat="server">
                                                <asp:Button ID="btInserir" runat="server" OnClick="btInserir_Click" Text="inserir" /><asp:Button
                                                    ID="btAlterar" runat="server" OnClick="btAlterar_Click" Text="salvar" />
                                                <asp:Button ID="btExcluir" runat="server" CausesValidation="False" OnClick="btExcluir_Click"
                                                    Text="excluir" /><asp:Button ID="btCancelar" runat="server" CausesValidation="False"
                                                        OnClick="btCancelar_Click" Text="cancelar" /></div>
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
                                    <cPesqConta:cPesqConta ID="cPesqConta1" runat="server" />
                                    <tr>
                                        <td class="esquerdo">
                                            descrição
                                        </td>
                                        <td class="direito">
                                            <ctx:cTexto ID="cTextoDescricaoLote" runat="server" MaxLength="50" Width="200px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="esquerdo">
                                            data de pagamento:
                                        </td>
                                        <td class="direito">
                                            <cdt:cData ID="cDataPagto" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="esquerdo">
                                            data de envio:
                                        </td>
                                        <td class="direito">
                                            <cdt:cData ID="cDataEnvio" runat="server" Enabled="False" EnableValidator="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="esquerdo">
                                            data de processamento:
                                        </td>
                                        <td class="direito">
                                            <cdt:cData ID="cDataProcessamento" runat="server" Enabled="False" EnableValidator="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th colspan="2">
                                            <div id="dGravacao1" runat="server">
                                                <asp:Button ID="btInserir0" runat="server" OnClick="btInserir_Click" Text="inserir" /><asp:Button
                                                    ID="btAlterar0" runat="server" OnClick="btAlterar_Click" Text="salvar" />
                                                <asp:Button ID="btExcluir0" runat="server" CausesValidation="False" OnClick="btExcluir_Click"
                                                    Text="excluir" /><asp:Button ID="btCancelar0" runat="server" CausesValidation="False"
                                                        OnClick="btCancelar_Click" Text="cancelar" />
                                            </div>
                                        </th>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:TabPanel>
                        <asp:TabPanel ID="tbPagamentos" runat="server" HeaderText="pagamentos">
                            <ContentTemplate>
                                <table class="cadastro">
                                    <tr>
                                        <th colspan="1">
                                            cadastro de pagamentos
                                        </th>
                                        <th colspan="1">
                                            <div id="dGravacao2" runat="server">
                                                <asp:Button ID="btInserirPagamento" runat="server" OnClick="btInserirPagamento_Click"
                                                    Text="agendar" ValidationGroup="pagamentos" />
                                                <asp:Button ID="btAlterarPagamento" runat="server" OnClick="btAlterarPagamento_Click"
                                                    Text="salvar" ValidationGroup="pagamentos" />
                                                <asp:Button ID="btExcluirPagamento" runat="server" CausesValidation="False" OnClick="btExcluirPagamento_Click"
                                                    Text="estornar" />
                                                <asp:Button ID="btDialogRejeitado" runat="server" Text="rejeitar" OnClientClick="openRejeicaoDialog();" />
                                                <asp:Button ID="btCancelarPagamento" runat="server" CausesValidation="False" OnClick="btCancelarPagamento_Click"
                                                    Text="cancelar" />
                                            </div>
                                        </th>
                                    </tr>
                                    <tr>
                                        <td class="esquerdo">
                                            lote:<asp:Label ID="txtId_pagamento" runat="server" Text="0" Visible="False"></asp:Label>
                                        </td>
                                        <td class="direito">
                                            <asp:Label ID="lbLote" runat="server">0</asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="esquerdo">
                                            tipo inscrição:
                                        </td>
                                        <td class="direito">
                                            <asp:RadioButtonList ID="rbDocumento" runat="server" OnSelectedIndexChanged="rbDocumento_SelectedIndexChanged"
                                                RepeatDirection="Horizontal" AutoPostBack="True">
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <asp:Panel ID="pCPF" runat="server">
                                        <tr>
                                            <td class="esquerdo">
                                                cpf:
                                            </td>
                                            <td class="direito">
                                                <cCpf:cCPF ID="cCPF1" runat="server" OnTextChanged="cCPF1_OnTextChanged" AutoPostBack="true"
                                                    ValidationGroup="pagamentos" />
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                    <asp:Panel ID="pCNPJ" runat="server">
                                        <tr>
                                            <td class="esquerdo">
                                                cnpj:
                                            </td>
                                            <td class="direito">
                                                <cCnpj:cCNPJ ID="cCNPJ1" runat="server" OnTextChanged="cCNPJ1_OnTextChanged" AutoPostBack="true"
                                                    ValidationGroup="pagamentos" />
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                    <tr>
                                        <td class="esquerdo">
                                            nome favorecido:
                                        </td>
                                        <td class="direito">
                                            <ctx:cTexto runat="server" ID="cTextoFavorecido" ValidationGroup="pagamentos" Width="500px"
                                                MaxLength="30"></ctx:cTexto>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="esquerdo">
                                            valor:
                                        </td>
                                        <td class="direito">
                                            <asp:TextBox ID="txtValor" runat="server" AutoPostBack="True" CssClass="valor" OnTextChanged="cValor1_TextChanged"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvValor" runat="server" ControlToValidate="txtValor"
                                                ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="esquerdo">
                                            projeto:
                                        </td>
                                        <td class="direito">
                                            <cddlProjeto:cDdlProjeto ID="cDdlProjeto1" runat="server" ValidationGroup="pagamentos" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="esquerdo">
                                            descrição:
                                        </td>
                                        <td class="direito">
                                            <ctx:cTexto runat="server" ID="cTextoDescricao" MaxLength="20" ValidationGroup="pagamentos"
                                                Width="500px"></ctx:cTexto>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="esquerdo">
                                            banco:
                                        </td>
                                        <td class="direito">
                                            <asp:DropDownList ID="listaBanco" runat="server" DataTextField="StrCodigoNome" DataValueField="id_banco"
                                                AppendDataBoundItems="True" OnSelectedIndexChanged="listaBancos_SelectedIndexChanged"
                                                AutoPostBack="True">
                                            </asp:DropDownList>
                                            <asp:ListSearchExtender ID="lista_ListSearchExtender" runat="server" Enabled="True"
                                                PromptCssClass="ListSearchExtenderPrompt" PromptText="digite para procurar" QueryPattern="Contains"
                                                QueryTimeout="2000" TargetControlID="listaBanco">
                                            </asp:ListSearchExtender>
                                            <asp:CompareValidator ID="cv" runat="server" ErrorMessage="selecione um banco..."
                                                ForeColor="Red" Operator="NotEqual" ValueToCompare="0" ControlToValidate="listaBanco"></asp:CompareValidator>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="esquerdo">
                                            agência:
                                        </td>
                                        <td class="direito">
                                            <ctx:cTexto runat="server" ID="cTextoAgencia" MaxLength="5" ValidationGroup="pagamentos">
                                            </ctx:cTexto>
                                            <ctx:cTexto runat="server" ID="cTextoDigitoAgencia" MaxLength="1" Width="15px" ValidationGroup="pagamentos">
                                            </ctx:cTexto>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="esquerdo">
                                            conta:
                                        </td>
                                        <td class="direito">
                                            <ctx:cTexto runat="server" ID="cTextoConta" MaxLength="12" ValidationGroup="pagamentos">
                                            </ctx:cTexto>
                                            <ctx:cTexto runat="server" ID="cTextoDigitoConta" MaxLength="1" Width="15px" ValidationGroup="pagamentos">
                                            </ctx:cTexto>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="esquerdo">
                                            forma de pagamento:
                                        </td>
                                        <td class="direito">
                                            <cddlFormaPagto:cDdlFormaPagto runat="server" ID="cDdlFormaPagto1" ValidationGroup="pagamentos">
                                            </cddlFormaPagto:cDdlFormaPagto>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="esquerdo">
                                            retorno do banco:
                                        </td>
                                        <td class="direito">
                                            <cddlTiposRetorno:cDdlTiposRetorno ID="cDdlTiposRetorno1" runat="server" Enabled="False"
                                                ValidationGroup="nada" />
                                        </td>
                                    </tr>
                                    <tr runat="server" id="trExibeMotivo">
                                        <td class="esquerdo" runat="server">
                                            motivo:
                                        </td>
                                        <td class="direito" runat="server">
                                            <asp:Label ID="lblMotivo" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label ID="lbMsgPagamento" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th colspan="2">
                                            <div id="dGravacao3" runat="server">
                                                <asp:Button ID="btInserirPagamento0" runat="server" OnClick="btInserirPagamento_Click"
                                                    Text="agendar" ValidationGroup="pagamentos" />
                                                <asp:Button ID="btAlterarPagamento0" runat="server" OnClick="btAlterarPagamento_Click"
                                                    Text="salvar" ValidationGroup="pagamentos" />
                                                <asp:Button ID="btExcluirPagamento0" runat="server" CausesValidation="False" OnClick="btExcluirPagamento_Click"
                                                    Text="estornar" />
                                                <asp:Button ID="btDialogRejeitar0" runat="server" Text="cancelar pagamento" OnClientClick="openRejeicaoDialog();" />
                                                <asp:Button ID="btCancelarPagamento0" runat="server" CausesValidation="False" OnClick="btCancelarPagamento_Click"
                                                    Text="cancelar" />
                                            </div>
                                        </th>
                                    </tr>
                                    <tr>
                                        <th class="style1" colspan="2">
                                            nome do favorecido
                                            <asp:TextBox ID="txtNomeFavorecido" runat="server" Width="500px"></asp:TextBox>
                                            <asp:Button ID="btFiltrar" runat="server" CausesValidation="False" OnClick="btFiltrar_Click"
                                                Text="filtrar" />
                                        </th>
                                    </tr>
                                    <tr>
                                        <td class="direito" colspan="2">
                                            <asp:GridView ID="gridPagamentos" runat="server" AutoGenerateColumns="False" OnRowEditing="gridPagamentos_RowEditing">
                                                <Columns>
                                                    <asp:BoundField DataField="id_remessa" HeaderText="código" />
                                                    <asp:TemplateField HeaderText="projeto">
                                                       
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Projeto.codigo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="nome_fav_cedente" HeaderText="favorecido" />
                                                    <asp:BoundField DataField="inscricao" HeaderText="cpf/cnpj" />
                                                    <asp:BoundField DataField="valor" DataFormatString="{0:N2}" HeaderText="valor" />
                                                    <asp:TemplateField HeaderText="tipo retorno">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("TipoRetorno.descricao") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("TipoRetorno.descricao") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:CommandField EditText="selecionar" ShowEditButton="True" />
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:TabPanel>
                    </ajaxToolkit:TabContainer>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <ajaxToolkit:UpdatePanelAnimationExtender ID="upae" BehaviorID="animation" runat="server"
            TargetControlID="UpdatePanel1" Enabled="True">
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
        </ajaxToolkit:UpdatePanelAnimationExtender>
    </div>
</asp:Content>
